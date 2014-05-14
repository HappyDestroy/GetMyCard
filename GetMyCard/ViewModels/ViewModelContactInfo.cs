using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using WP.Core;
using System.Net;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Phone.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using GetMyCard.Model;
using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;

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

       public ImageSource PhotoBox
       {
           get { return _PhotoBox; }
           set { Assign(ref _PhotoBox, value); }
       }

       #endregion


       #region Constructeur
       
       public ViewModelContactInfo()
       {
           Contact c = (Contact)PhoneApplicationService.Current.State["contact"];

           Nom = c.Nom;
           Prenom = c.Prenom;

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
   }
}
