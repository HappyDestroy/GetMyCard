using GetMyCard.Model;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Storage;
using WP.Core;

namespace GetMyCard.ViewModels
{
    public class ViewModelMainPage : ObservableObject
    {
        #region Fields

        private DelegateCommand _DeleteContactCommand;
        private DelegateCommand _SelectedContact;
        private DelegateCommand _SelectedContact;
        private ObservableCollection<Contact> _Contacts;
        private MaCarteVisite _MaCarteVisite;

        private ImageSource _PhotoMoi;
        private string _NomMoi;
        private string _PrenomMoi;
        private Contact _Cont ;
        #endregion



        #region Properties

        public DelegateCommand DeleteContactCommand
        {
            get { return _DeleteContactCommand; }
        }

        public DelegateCommand SelectedContact
        {
            get { return _SelectedContact; }
        }
        public DelegateCommand SelectedContact
        {
            get { return _SelectedContact; }
            set { _SelectedContact = value; }
        }


        public ObservableCollection<Contact> Contacts
        {
            get { return _Contacts; }
            private set { Assign(ref _Contacts, value); }
        }

        public MaCarteVisite MaCarteVisite
        {
            get { return _MaCarteVisite; }
            set { Assign(ref _MaCarteVisite, value); }
        }

        public ImageSource PhotoMoi
        {
            get { return _PhotoMoi; }
            set { Assign(ref _PhotoMoi, value); }
        }

        public string NomMoi
        {
            get { return _NomMoi; }
            set { Assign(ref _NomMoi, value); }
        }

        public string PrenomMoi
        {
            get { return _MaCarteVisite.Prenom; }
            set { Assign(ref _PrenomMoi, value); }
        }

        #endregion



        #region Constructeur

        public ViewModelMainPage()
        {
            Contact addContact = new Contact();
            addContact.Photo = "/Images/contact.png";
            addContact.Nom = "Nico";
            addContact.Prenom = "Sabou";

            GetMyCardDataContext.Instance.Contact.InsertOnSubmit(addContact);
            GetMyCardDataContext.Instance.SubmitChanges();



            _DeleteContactCommand = new DelegateCommand(ExecuteDeleteContact, CanExecuteDeleteContact);
            _SelectedContact = new DelegateCommand(ExecuteSelectedContact, CanExecuteSelectContact);
            _Contacts = new ObservableCollection<Contact>();
           
            Contact c;
            c = new Contact();          
            c.Nom = "Sab";
            c.Prenom = "Nini";
            c.Mail = "Nicolassab@nigwa.com";

            GetMyCardDataContext.Instance.Contact.InsertOnSubmit(c);
            GetMyCardDataContext.Instance.SubmitChanges();

            if(GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                MaCarteVisite = GetMyCardDataContext.Instance.MaCarteVisite.First();

                BitmapImage retrievedImage = new BitmapImage();

                //On récupère l'image depuis l'isolated storage
                using(var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using(var isoFileStream = isoStore.OpenFile(MaCarteVisite.Photo, System.IO.FileMode.Open))
                    {
                        retrievedImage.SetSource(isoFileStream);
                    }

                    PhotoMoi = retrievedImage;
                }

                NomMoi = MaCarteVisite.Nom;
                PrenomMoi = MaCarteVisite.Prenom;
            }
            else
            {
                MaCarteVisite = new MaCarteVisite();
                MaCarteVisite.Photo = "Images/contact.png";
                NomMoi = "Vous n'avez pas encore enregistré votre carte de visite";
            }
        }


        #endregion



        #region Methods

        private bool CanExecuteDeleteContact(object parameters)
        {
            //TODO : Vérifier que le contact existe
            return true;
        }

        private bool CanExecuteSelectContact(object parameters)
        {
            return true;
        }
        private bool CanExecuteSelectedContact(object parameters)
        {
            return true;
        }

        private void ExecuteDeleteContact(object parameters)
        {
            try
            {
                if(MessageBoxResult.OK == MessageBox.Show("Êtes-vous sûr de supprimer ce contact ?", "Suppression", MessageBoxButton.OKCancel))
                {
                    GetMyCardDataContext.Instance.Contact.DeleteOnSubmit((Contact)parameters);
                    GetMyCardDataContext.Instance.SubmitChanges();
                    _Contacts.Remove((Contact)parameters);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur de suppresion.");
            }
        }


        private void ExecuteSelectedContact(object parameters)
        {
            MessageBox.Show(((Contact)parameters).Nom.ToString());
            string idContact = ((Contact)parameters).Identifiant.ToString();

            PhoneApplicationService.Current.State["contact"] = parameters;
            App.RootFrame.Navigate(new Uri("/Views/ContactInfo.xaml", UriKind.Relative));
        }
        private void ExecuteSelectedContact(object parameters)
        {
            MessageBox.Show(SelectedContact.Name);
        }

        public void LoadData()
        {
            foreach (Contact contact in GetMyCardDataContext.Instance.Contact)
            {
                Contacts.Add(contact);
            }
        }

        #endregion
    }
}
