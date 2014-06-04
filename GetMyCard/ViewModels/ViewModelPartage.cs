using GetMyCard.Model;
using GetMyCard.Resources;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using WP.Core;

namespace GetMyCard.ViewModels
{
    public class ViewModelPartage : ObservableObject
    {
        #region Fields

        private DelegateCommand _ShareCDVCommand;
        private DelegateCommand _SearchPeerCommand;
        private DelegateCommand _CancelCommand;
        private DelegateCommand _CloseConnectionCommand;

        private bool _IsConnecting;

        private MaCarteVisite _MaCarte;

        private string _PeerSearchInfo;

        //La liste des appareils appairé
        private ObservableCollection<PeerAppInfo> _PeerApps;

        //L'objet socket pour communiquer avec un autre appareil
        private StreamSocket _Socket;

        //Le nom du Peer actuel (Luke, Je suis ton peer)
        private string _PeerName = string.Empty;

        //Codes des erreurs
        private const uint ERR_BLUETOOTH_OFF = 0x8007048F;      // Le bluetooth est désactivé
        private const uint ERR_MISSING_CAPS = 0x80070005;       // Il manque une définition dans le manifest
        private const uint ERR_NOT_ADVERTISING = 0x8000000E;    // On n'indique pas notre présence avec PeerFinder.Start()


        private PeerAppInfo _SelectedContact;

        private DataReader _DataReader;
        private DataWriter _DataWriter;

        #endregion

        #region Properties

        #region Commands

        public DelegateCommand ShareCDVCommand
        {
            get { return _ShareCDVCommand; }
            set { Assign(ref _ShareCDVCommand, value); }
        }

        public DelegateCommand SearchPeerCommand
        {
            get { return _SearchPeerCommand; }
            set { Assign(ref _SearchPeerCommand, value); }
        }

        public DelegateCommand CancelCommand
        {
            get { return _CancelCommand; }
            set { Assign(ref _CancelCommand, value); }
        }

        public DelegateCommand CloseConnectionCommand
        {
            get { return _CloseConnectionCommand; }
            set { Assign(ref _CloseConnectionCommand, value); }
        }

        #endregion

        public MaCarteVisite MaCarte
        {
            get { return _MaCarte; }
            set { Assign(ref _MaCarte, value); }
        }

        #region Bluetooth

        public ObservableCollection<PeerAppInfo> PeerApps
        {
            get { return _PeerApps; }
            set { Assign(ref _PeerApps, value); }
        }

        public PeerAppInfo SelectedContact
        {
            get { return _SelectedContact; }
            set { Assign(ref _SelectedContact, value); }
        }

        public bool IsConnecting
        {
            get { return _IsConnecting; }
            set { Assign(ref _IsConnecting, value); }
        }

        public StreamSocket Socket
        {
            get { return _Socket; }
            set { Assign(ref _Socket, value); }
        }

        public string PeerName
        {
            get { return _PeerName; }
            set { Assign(ref _PeerName, value); }
        }

        public string PeerSearchInfo
        {
            get { return _PeerSearchInfo; }
            set { Assign(ref _PeerSearchInfo, value); }
        }

        public DataReader DataReader
        {
            get { return _DataReader; }
            set { Assign(ref _DataReader, value); }
        }

        public DataWriter DataWriter
        {
            get { return _DataWriter; }
            set { Assign(ref _DataWriter, value); }
        }

        #endregion

        #endregion

        #region Constructeur

        public ViewModelPartage()
        {
            _IsConnecting = false;
            _ShareCDVCommand = new DelegateCommand(ExecuteShareCDV, CanExecuteShareCDV);
            _SearchPeerCommand = new DelegateCommand(ExecuteSearchPeer, CanExecuteSearchPeer);
            _CloseConnectionCommand = new DelegateCommand(ExecuteCloseConnection, CanExecuteCloseConnection);
            _CancelCommand = new DelegateCommand(ExecuteCancel, CanExecuteCancel);

            _PeerApps = new ObservableCollection<PeerAppInfo>();

            MaCarte = new MaCarteVisite();

            if (GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                MaCarte = GetMyCardDataContext.Instance.MaCarteVisite.First();

                //On commence a partager notre bluetooth
                PeerFinder.DisplayName = MaCarte.Nom + " " + MaCarte.Prenom;
                PeerFinder.Start();
            }
        }

        #endregion

        #region Methods

        #region System

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case "SelectedContact":
                    if (SelectedContact != null)
                    {
                        PeerInformation peer = SelectedContact.PeerInfo;
                        ConnectToPeer(peer);
                        StartProgress("Connexion en cours ...");
                    }
                    else if (Socket != null)
                    {
                        CloseConnection(true);
                    }
                    break;
                case "IsConnecting":
                    this.CancelCommand.OnCanExecuteChanged();
                    break;
                case "Socket":
                    this.CloseConnectionCommand.OnCanExecuteChanged();
                    this.ShareCDVCommand.OnCanExecuteChanged();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Commands

        #region ShareCDV

        private bool CanExecuteShareCDV(object parameters)
        {
            return Socket != null;
        }

        private void ExecuteShareCDV(object parameters)
        {
            //On se connecte à l'appareil selectionné
            PeerInformation peer = SelectedContact.PeerInfo;

            SendMessage(MaCarte);


        }

        #endregion

        #region SearchPeer

        private bool CanExecuteSearchPeer(object paramters)
        {
            return true;
        }

        private void ExecuteSearchPeer(object parameters)
        {
            RefreshPeerAppList();
        }

        #endregion

        #region Cancel

        private bool CanExecuteCancel(object paramters)
        {
            return this._IsConnecting;
        }

        private void ExecuteCancel(object parameters)
        {
            this.IsConnecting = false;
            this.SelectedContact = null;
            StopProgress();
        }

        #endregion

        #region CloseConnection

        private bool CanExecuteCloseConnection(object paramters)
        {
            return this.Socket != null;
        }

        private void ExecuteCloseConnection(object parameters)
        {
            this.CloseConnection(true);
            this.SelectedContact = null;
        }

        #endregion

        #endregion

        #region Rafraichissement des appareils
        private async void RefreshPeerAppList()
        {
            try
            {
                StartProgress("Recheche d'appareils ...");

                var peers = await PeerFinder.FindAllPeersAsync();

                PeerApps.Clear();

                if (peers.Count == 0)
                {
                    PeerSearchInfo = AppResources.NoPeersTexte;
                }
                else
                {
                    PeerSearchInfo = String.Format(AppResources.PeersFoundTexte, peers.Count);

                    //On affiche les appareils dans la liste
                    foreach (var peer in peers)
                    {
                        PeerApps.Add(new PeerAppInfo(peer));
                    }
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == ERR_BLUETOOTH_OFF)
                {
                    var result = MessageBox.Show("Le bluetooth est désactivé, cliquez sur \"OK\" pour l'activer.", "Bluetooth desactivé", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        ShowBluetoothControlPanel();
                    }
                }
                else if ((uint)ex.HResult == ERR_MISSING_CAPS)
                {
                    MessageBox.Show("Il manque des droit pour l'application (manifest)");
                }
                else if ((uint)ex.HResult == ERR_NOT_ADVERTISING)
                {
                    MessageBox.Show("Vous n'avez pas de carte de visite.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            finally
            {
                StopProgress();
            }
        }
        #endregion

        #region Connexion management

        async Task ConnectToPeer(PeerInformation peer)
        {
            try
            {
                IsConnecting = true;
                Socket = await PeerFinder.ConnectAsync(peer);
                IsConnecting = false;
                StopProgress();

                //On arrête d'indiquer notre présence
                //TODO : Proposer un bouton pour arrêter / démarrer le PeerFinder
                //PeerFinder.Stop();

                PeerName = peer.DisplayName;

                ListenForIncomingMessage();
                _ShareCDVCommand.OnCanExecuteChanged();
            }
            catch (Exception)
            {
                MessageBox.Show("Connexion non établie : La personne n'a pas accepté.", "Erreur", MessageBoxButton.OK);
                CloseConnection(true);

                this.IsConnecting = false;
                this.SelectedContact = null;
                StopProgress();
            }
        }

        private void CloseConnection(bool continueAdvertise)
        {
            if (DataReader != null)
            {
                DataReader.Dispose();
                DataReader = null;
            }

            if (DataWriter != null)
            {
                DataWriter.Dispose();
                DataWriter = null;
            }

            if (Socket != null)
            {
                Socket.Dispose();
                Socket = null;
                this._ShareCDVCommand.OnCanExecuteChanged();
            }

            if (continueAdvertise)
            {
                PeerFinder.Start();
            }
            else
            {
                PeerFinder.Stop();
            }
        }

        private async void ListenForIncomingMessage()
        {
            try
            {
                var jsonMessage = await GetMessage();
                if (!string.IsNullOrWhiteSpace(jsonMessage))
                {
                    Contact c = JsonConvert.DeserializeObject<Contact>(jsonMessage);

                    GetMyCardDataContext.Instance.Contact.InsertOnSubmit(c);
                    GetMyCardDataContext.Instance.SubmitChanges();
                }

                ListenForIncomingMessage();
            }
            catch (Exception)
            {
                MessageBox.Show("Déconnecté");
                CloseConnection(true);
            }
        }

        #endregion

        #region ProgressBar
        private void StartProgress(string message)
        {
            SystemTray.ProgressIndicator.Text = message;
            SystemTray.ProgressIndicator.IsIndeterminate = true;
            SystemTray.ProgressIndicator.IsVisible = true;
        }

        private void StopProgress()
        {
            if (SystemTray.ProgressIndicator != null)
            {
                SystemTray.ProgressIndicator.IsVisible = false;
                SystemTray.ProgressIndicator.IsIndeterminate = false;
            }
        }
        #endregion

        #region SendAndGetMessage

        private async Task SendMessage(MaCarteVisite MaCarte)
        {
            if (MaCarte == null)
            {
                MessageBox.Show("Pas de carte à envoyer", "Erreur", MessageBoxButton.OK);
                return;
            }

            if (Socket == null)
            {
                MessageBox.Show("Non connecté", "Erreur", MessageBoxButton.OK);
                return;
            }

            if (DataWriter == null)
                DataWriter = new DataWriter(Socket.OutputStream);

            // Chaque message est envoyé en 2 blocs.
            // Le premier est la taille du message.
            // Et le 2ème est le message.

            //On sérialise notre objet en Json
            string MaCarteJson = JsonConvert.SerializeObject(MaCarte, Formatting.Indented);

            DataWriter.WriteInt32(MaCarteJson.Length);
            await DataWriter.StoreAsync();


            DataWriter.WriteString(MaCarteJson);
            await DataWriter.StoreAsync();
        }

        private async Task<string> GetMessage()
        {
            if (DataReader == null)
                DataReader = new DataReader(Socket.InputStream);

            // Chaque message est envoyé en 2 blocs.
            // Le premier est la taille du message.
            // Et le 2ème est le message.
            await DataReader.LoadAsync(4);
            uint messageLen = (uint)DataReader.ReadInt32();
            await DataReader.LoadAsync(messageLen);
            return DataReader.ReadString(messageLen);
        }

        #endregion

        private void ShowBluetoothControlPanel()
        {
            ConnectionSettingsTask connectionSettingsTask = new ConnectionSettingsTask();
            connectionSettingsTask.ConnectionSettingsType = ConnectionSettingsType.Bluetooth;
            connectionSettingsTask.Show();
        }

        #endregion
    }
























    /// <summary>
    ///  Classe permettant de sauvegarder les infos des peers
    /// </summary>
    public class PeerAppInfo
    {
        internal PeerAppInfo(PeerInformation peerInformation)
        {
            this.PeerInfo = peerInformation;
            this.DisplayName = this.PeerInfo.DisplayName;
        }

        public string DisplayName { get; private set; }
        public PeerInformation PeerInfo { get; private set; }
    }
}
