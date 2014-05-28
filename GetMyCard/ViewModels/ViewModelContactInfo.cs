using GetMyCard.Model;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WP.Core;


namespace GetMyCard.ViewModels
{
   public class ViewModelContactInfo : ObservableObject
   {
       #region Fields

       private string _NomContact;
       private string _PrenomContact;
       private string _PhotoContact;
       private string _MailContact;
       private string _TelFixeContact;
       private string _TelPortContact;
       private string _NationaliteContact;
       private string _SocieteContact;
       private string _LogoContact;
       private string _PosteContact;
       private string _SiteWebContact;
       private string _AdresseContact;
       private string _VilleContact;
       private string _CPContact;
       private string _PaysContact;

       private DelegateCommand _AddContact;
       private SaveContactTask _saveContactTask;

       private ImageSource _ContactPhotoBox;
       private ImageSource _ContactLogoBox;

       #endregion



       #region Properties

       public string NomContact
       {
           get { return _NomContact; }
           set { Assign(ref _NomContact, value); }
       }

       public string PrenomContact
       {
           get { return _PrenomContact; }
           set { Assign(ref _PrenomContact, value); }
       }

       public string PhotoContact
       {
           get { return _PhotoContact; }
           set { Assign(ref _PhotoContact, value); }
       }

       public string MailContact
       {
           get { return _MailContact; }
           set { Assign(ref _MailContact, value); }
       }

       public string TelFixeContact
       {
           get { return _TelFixeContact; }
           set { Assign(ref _TelFixeContact, value); }
       }

       public string TelPortContact
       {
           get { return _TelPortContact; }
           set { Assign(ref _TelPortContact, value); }
       }

       public string NationaliteContact
       {
           get { return _NationaliteContact; }
           set { Assign(ref _NationaliteContact, value); }
       }

       public string SocieteContact
       {
           get { return _SocieteContact; }
           set { Assign(ref _SocieteContact, value); }
       }

       public string LogoContact
       {
           get { return _LogoContact; }
           set { Assign(ref _LogoContact, value); }
       }

       public string PosteContact
       {
           get { return _PosteContact; }
           set { Assign(ref _PosteContact, value); }
       }

       public string SiteWebContact
       {
           get { return _SiteWebContact; }
           set { Assign(ref _SiteWebContact, value); }
       }

       public string AdresseContact
       {
           get { return _AdresseContact; }
           set { Assign(ref _AdresseContact, value); }
       }

       public string VilleContact
       {
           get { return _VilleContact; }
           set { Assign(ref _VilleContact, value); }
       }

       public string CPContact
       {
           get { return _CPContact; }
           set { Assign(ref _CPContact, value); }
       }

       public string PaysContact
       {
           get { return _PaysContact; }
           set { Assign(ref _PaysContact, value); }
       }
       public DelegateCommand AddContact
       {
           get { return _AddContact; }
       }

       public ImageSource ContactPhotoBox
        {
            get { return _ContactPhotoBox; }
            set { Assign(ref _ContactPhotoBox, value); }
        }

       public ImageSource ContactLogoBox
       {
           get { return _ContactLogoBox; }
           set { Assign(ref _ContactLogoBox, value); }
       }
       #endregion


       #region Constructeur
       
       public ViewModelContactInfo()
       {
           _AddContact = new DelegateCommand(ExecuteAdd, CanExecuteAdd);

           Contact c = (Contact)PhoneApplicationService.Current.State["contact"];
    
               NomContact = c.Nom;
               PrenomContact = c.Prenom;
               
    
               #region verification des info de l'utilisateur
               if (!string.IsNullOrEmpty(c.Photo))
               {
                   BitmapImage retrievedImage = new BitmapImage();
    
                   if(c.Photo == "/Images/contact.png")
                   {
                       retrievedImage.UriSource = new Uri(c.Photo, UriKind.RelativeOrAbsolute);
                       ContactPhotoBox = retrievedImage;
                   }
                   else
                   {
                       using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                       {
                           using (var isoFileStream = isoStore.OpenFile(c.Photo, System.IO.FileMode.Open))
                           {
                               retrievedImage.SetSource(isoFileStream);
                               ContactPhotoBox = retrievedImage;
                           }

                           
                       }
                   }
               }
               if (!string.IsNullOrEmpty(c.Mail))
               {
                   MailContact = c.Mail;
               }
               if (c.TelFixe != 0)
               {
                   TelFixeContact = c.TelFixe.ToString();
               }
               if (c.TelPort != 0)
               {
                   TelPortContact = c.TelPort.ToString();
               }
               if (!string.IsNullOrEmpty(c.Nationalite))
               {
                   NationaliteContact = c.Nationalite;
               }
               if (!string.IsNullOrEmpty(c.Societe))
               {
                   SocieteContact = c.Societe;
               }
               if (!string.IsNullOrEmpty(c.Logo))
               {
                   BitmapImage retrievedImage = new BitmapImage();

                   if (c.Logo == "/Images/contact.png")
                   {
                       retrievedImage.UriSource = new Uri(c.Logo, UriKind.RelativeOrAbsolute);
                       ContactLogoBox = retrievedImage;
                   }
                   else
                   {
                       using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                       {
                           using (var isoFileStream = isoStore.OpenFile(c.Logo, System.IO.FileMode.Open))
                           {
                               retrievedImage.SetSource(isoFileStream);
                               ContactLogoBox = retrievedImage;
                           }


                       }
                   }
               }
               if (!string.IsNullOrEmpty(c.Poste))
               {
                   PosteContact = c.Poste;
               }
               if (!string.IsNullOrEmpty(c.SiteWeb))
               {
                   SiteWebContact = c.SiteWeb;
               }
               if (!string.IsNullOrEmpty(c.Adresse))
               {
                   AdresseContact = c.Adresse;
               }
               if (!string.IsNullOrEmpty(c.Ville))
               {
                   VilleContact = c.Ville;
               }
               if (c.CP != 0)
               {
                   CPContact = c.CP.ToString();
               }
               if (!string.IsNullOrEmpty(c.Pays))
               {
                   PaysContact = c.Pays;
               }
               #endregion
       }
       #endregion



       #region Methods

       private bool CanExecuteAdd(object parameters)
       {
           return true;
       }

       private void ExecuteAdd(object parameters)
       {
           _saveContactTask = new SaveContactTask();
           _saveContactTask.Completed += new EventHandler<SaveContactResult>(saveContactTask_Completed);
           _saveContactTask.FirstName = _PrenomContact;
           _saveContactTask.LastName = _NomContact;
           _saveContactTask.MobilePhone = _TelPortContact;
           _saveContactTask.HomePhone = _TelFixeContact;
           _saveContactTask.PersonalEmail = _MailContact;
           _saveContactTask.Company = _SocieteContact;
           _saveContactTask.JobTitle = _PosteContact;
           _saveContactTask.Website = _MailContact;
           _saveContactTask.WorkAddressCity = _VilleContact;
           _saveContactTask.WorkAddressStreet = _AdresseContact;
           _saveContactTask.WorkAddressZipCode = _CPContact;
           _saveContactTask.WorkAddressCountry = _PaysContact;
           _saveContactTask.Show();
       }

       void saveContactTask_Completed(object sender, SaveContactResult e)
       {
           switch (e.TaskResult)
           {
               //Logic for when the contact was saved successfully
               case TaskResult.OK:
                   MessageBox.Show("Contact saved.");
                   break;

               //Logic for when the task was cancelled by the user
               case TaskResult.Cancel:
                   MessageBox.Show("Save cancelled.");
                   break;

               //Logic for when the contact could not be saved
               case TaskResult.None:
                   MessageBox.Show("Contact could not be saved.");
                   break;
           }
       }

       #endregion
   }
}
