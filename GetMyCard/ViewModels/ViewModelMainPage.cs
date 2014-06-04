using GetMyCard.Model;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;
using WP.Core;

namespace GetMyCard.ViewModels
{
    public class ViewModelMainPage : ObservableObject
    {
        #region Fields

        private DelegateCommand _DeleteContactCommand;
        private DelegateCommand _SelectedContact;

        private ObservableCollection<Contact> _Contacts;
        private MaCarteVisite _MaCarteVisite;

        private ImageSource _PhotoMoi;
        private ImageSource _LogoMoi;
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

        public ImageSource LogoMoi
        {
            get { return _LogoMoi; }
            set { Assign(ref _LogoMoi, value); }
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
        }


        #endregion



        #region Methods

        private bool CanExecuteDeleteContact(object parameters)
        {
            return true;
        }

        private bool CanExecuteSelectContact(object parameters)
        {
            return true;
        }

        private void ExecuteDeleteContact(object parameters)
        {
            try
            {
                if (MessageBoxResult.OK == MessageBox.Show("Êtes-vous sûr de supprimer ce contact ?", "Suppression", MessageBoxButton.OKCancel))
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
            PhoneApplicationService.Current.State["contact"] = parameters;
            App.RootFrame.Navigate(new Uri("/Views/ContactInfo.xaml", UriKind.Relative));
        }


        public void LoadData()
        {
            //On charge tous les contacts
            foreach (Contact contact in GetMyCardDataContext.Instance.Contact)
            {
                Contacts.Add(contact);
            }

            //On charge les infos de sa carte
            if (GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                MaCarteVisite = GetMyCardDataContext.Instance.MaCarteVisite.First();

                if (!string.IsNullOrEmpty(MaCarteVisite.Photo))
                {
                    if (MaCarteVisite.Photo.Equals("/Images/contact.png"))
                    {
                        BitmapImage retrievedPhoto = new BitmapImage();

                        retrievedPhoto.UriSource = new Uri(MaCarteVisite.Photo, UriKind.RelativeOrAbsolute);
                        PhotoMoi = retrievedPhoto;
                    }
                    else
                    {
                        BitmapImage retrievedPhoto = new BitmapImage();

                        //On récupère l'image depuis l'isolated storage
                        using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            using (var isoFileStream = isoStore.OpenFile(MaCarteVisite.Photo, System.IO.FileMode.Open))
                            {
                                retrievedPhoto.SetSource(isoFileStream);
                                PhotoMoi = retrievedPhoto;
                            }
                        }
                    }
                }

                if(!string.IsNullOrEmpty(MaCarteVisite.Logo))
                {
                    BitmapImage retrievedLogo = new BitmapImage();

                    //On récupère le logo depuis l'isolated storage
                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        // Récupération du logo
                        using (var isoFileStream = isoStore.OpenFile(MaCarteVisite.Logo, System.IO.FileMode.Open))
                        {
                            retrievedLogo.SetSource(isoFileStream);
                            LogoMoi = retrievedLogo;
                        }

                    }
                }
            }
            else
            {
                MaCarteVisite = new MaCarteVisite();

                BitmapImage retrievedImage = new BitmapImage();
                retrievedImage.UriSource = new Uri("/Images/contact.png", UriKind.RelativeOrAbsolute);
                PhotoMoi = retrievedImage;
                MaCarteVisite.Nom = "Vous n'avez pas de carte de visite";
            }
        }
        #endregion
    }
}
