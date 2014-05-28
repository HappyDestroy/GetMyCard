using GetMyCard.Model;
using GetMyCard.Resources;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
        
        #endregion





        #region Properties

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

        public MaCarteVisite MaCarte
        {
            get { return _MaCarte; }
            set { Assign(ref _MaCarte, value); }
        }

        public ObservableCollection<PeerAppInfo> PeerApps
        {
            get { return _PeerApps; }
            set { Assign(ref _PeerApps, value); }
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

        #endregion




        #region Constructeur

        public ViewModelPartage()
        {
            _ShareCDVCommand = new DelegateCommand(ExecuteShareCDV, CanExecuteShareCDV);
            _SearchPeerCommand = new DelegateCommand(ExecuteSearchPeer, CanExecuteSearchPeer);


            _PeerApps = new ObservableCollection<PeerAppInfo>();

            //PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;


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

        private bool CanExecuteShareCDV(object parameters)
        {
            return true;
        }

        private bool CanExecuteSearchPeer(object paramters)
        {
            return true;
        }

        private void ExecuteShareCDV(object parameters)
        {

        }

        private void ExecuteSearchPeer(object parameters)
        {
            RefreshPeerAppList();
        }


        /// <summary>
        /// Asynchronous call to re-populate the ListBox of peers.
        /// </summary>
        private async void RefreshPeerAppList()
        {
            try
            {
                StartProgress("Finding peers ...");

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
                    var result = MessageBox.Show("Le bluetooth est désactivé.", "Bluetooth desactivé", MessageBoxButton.OKCancel);
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
                    MessageBox.Show("Notre présence n'est pas indiqué (PeerFind.start())");
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
