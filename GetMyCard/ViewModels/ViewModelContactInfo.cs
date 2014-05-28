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

       private string _Nom;
       private string _Prenom;
       private string _Photo;
       private string _Mail;
       private string _TelFixe;
       private string _TelPort;
       private string _Nationalite;
       private string _Societe;
       private string _Logo;
       private string _Poste;
       private string _SiteWeb;
       private string _Adresse;
       private string _Ville;
       private string _CP;
       private string _Pays;

       private DelegateCommand _AddContact;
       private SaveContactTask _saveContactTask;

       private ImageSource _PhotoBox;

       #endregion



       #region Properties

       public string Nom
       {
           get { return _Nom; }
           set { Assign(ref _Nom, value); }
       }

       public string Prenom
       {
           get { return _Prenom; }
           set { Assign(ref _Prenom, value); }
       }

       public string Photo
       {
           get { return _Photo; }
           set { Assign(ref _Photo, value); }
       }

       public string Mail
       {
           get { return _Mail; }
           set { Assign(ref _Mail, value); }
       }

       public string TelFixe
       {
           get { return _TelFixe; }
           set { Assign(ref _TelFixe, value); }
       }

       public string TelPort
       {
           get { return _TelPort; }
           set { Assign(ref _TelPort, value); }
       }

       public string Nationalite
       {
           get { return _Nationalite; }
           set { Assign(ref _Nationalite, value); }
       }

       public string Societe
       {
           get { return _Societe; }
           set { Assign(ref _Societe, value); }
       }

       public string Logo
       {
           get { return _Logo; }
           set { Assign(ref _Logo, value); }
       }

       public string Poste
       {
           get { return _Poste; }
           set { Assign(ref _Poste, value); }
       }

       public string SiteWeb
       {
           get { return _SiteWeb; }
           set { Assign(ref _SiteWeb, value); }
       }

       public string Adresse
       {
           get { return _Adresse; }
           set { Assign(ref _Adresse, value); }
       }

       public string Ville
       {
           get { return _Ville; }
           set { Assign(ref _Ville, value); }
       }

       public string CP
       {
           get { return _CP; }
           set { Assign(ref _CP, value); }
       }

       public string Pays
       {
           get { return _Pays; }
           set { Assign(ref _Pays, value); }
       }
       public DelegateCommand AddContact
       {
           get { return _AddContact; }
       }

        public ImageSource PhotoBox
        {
            get { return _PhotoBox; }
            set { Assign(ref _PhotoBox, value); }
        }

       #endregion


       #region Constructeur
       
       public ViewModelContactInfo()
       {
           _AddContact = new DelegateCommand(ExecuteAdd, CanExecuteAdd);

           Contact c = (Contact)PhoneApplicationService.Current.State["contact"];
    
               Nom = c.Nom;
               Prenom = c.Prenom;
               MessageBox.Show(Nom + Prenom);
    
               #region verification des info de l'utilisateur
               if (!string.IsNullOrEmpty(c.Photo))
               {
                   BitmapImage retrievedImage = new BitmapImage();
    
                   if(c.Photo == "/Images/contact.png")
                   {
                       retrievedImage.UriSource = new Uri(c.Photo, UriKind.RelativeOrAbsolute);
                       PhotoBox = retrievedImage;
                   }
                   else
                   {
                       using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                       {
                           using (var isoFileStream = isoStore.OpenFile(c.Photo, System.IO.FileMode.Open))
                           {
                               retrievedImage.SetSource(isoFileStream);
                           }
    
                           PhotoBox = retrievedImage;
                       }
                   }
               }
               if (!string.IsNullOrEmpty(c.Mail))
               {
                   Mail = c.Mail;
               }
               if (c.TelFixe != 0)
               {
                   TelFixe = c.TelFixe.ToString();
               }
               if (c.TelPort != 0)
               {
                   Mail = c.TelPort.ToString();
               }
               if (!string.IsNullOrEmpty(c.Nationalite))
               {
                   Nationalite = c.Nationalite;
               }
               if (!string.IsNullOrEmpty(c.Societe))
               {
                   Societe = c.Societe;
               }
               if (!string.IsNullOrEmpty(c.Logo))
               {
                   Logo = c.Logo;
               }
               if (!string.IsNullOrEmpty(c.Poste))
               {
                   Poste = c.Poste;
               }
               if (!string.IsNullOrEmpty(c.SiteWeb))
               {
                   SiteWeb = c.SiteWeb;
               }
               if (!string.IsNullOrEmpty(c.Adresse))
               {
                   Adresse = c.Adresse;
               }
               if (!string.IsNullOrEmpty(c.Ville))
               {
                   Ville = c.Ville;
               }
               if (c.CP != 0)
               {
                   CP = c.CP.ToString();
               }
               if (!string.IsNullOrEmpty(c.Pays))
               {
                   Pays = c.Pays;
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
           _saveContactTask.FirstName = _Prenom;
           _saveContactTask.LastName = _Nom;
           _saveContactTask.MobilePhone = _TelPort;
           _saveContactTask.HomePhone = _TelFixe;
           _saveContactTask.PersonalEmail = _Mail;
           _saveContactTask.Company = _Societe;
           _saveContactTask.JobTitle = _Poste;
           _saveContactTask.Website = _Mail;
           _saveContactTask.WorkAddressCity = _Ville;
           _saveContactTask.WorkAddressStreet = _Adresse;
           _saveContactTask.WorkAddressZipCode = _CP;
           _saveContactTask.WorkAddressCountry = _Pays;
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
