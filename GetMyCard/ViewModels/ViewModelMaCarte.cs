using GetMyCard.Model;
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
using System.Windows.Data;

namespace GetMyCard.ViewModels
{
    public class ViewModelMaCarte : ObservableObject
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

        private BitmapImage _MaPhotoBox;

        private DelegateCommand _ValidateCommand;
        private DelegateCommand _ImportPhotoCommand;
        private PhotoChooserTask _PhotoChooserTask;
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

        public DelegateCommand ValidateCommand
        {
            get { return _ValidateCommand; }
            set { Assign(ref _ValidateCommand, value); }
        }

        public DelegateCommand ImportPhotoCommand
        {
            get { return _ImportPhotoCommand; }
            set { Assign(ref _ImportPhotoCommand, value); }
        }


        public BitmapImage MaPhotoBox
        {
            get { return _MaPhotoBox; }
            set { Assign(ref _MaPhotoBox, value); }
        }


        #endregion



        #region Constructeur

        public ViewModelMaCarte()
        {
            _ValidateCommand = new DelegateCommand(ExecuteValidate, CanExecuteValidate);
            _ImportPhotoCommand = new DelegateCommand(ExecuteImportPhoto, CanExecuteImportPhoto);

            _MaPhotoBox = new BitmapImage();

            #region Remplissage des champs avec les valeurs de la BDD

            if (GetMyCardDataContext.Instance.MaCarteVisite.Count() > 0)
            {
                Nom = GetMyCardDataContext.Instance.MaCarteVisite.First().Nom;
                Prenom = GetMyCardDataContext.Instance.MaCarteVisite.First().Prenom;

                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Photo))
                {
                    Photo = GetMyCardDataContext.Instance.MaCarteVisite.First().Photo;
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Mail))
                {
                    Mail = GetMyCardDataContext.Instance.MaCarteVisite.First().Mail;
                }
                if (GetMyCardDataContext.Instance.MaCarteVisite.First().TelFixe != 0)
                {
                    TelFixe = GetMyCardDataContext.Instance.MaCarteVisite.First().TelFixe.ToString();
                }
                if (GetMyCardDataContext.Instance.MaCarteVisite.First().TelPort != 0)
                {
                    TelPort = GetMyCardDataContext.Instance.MaCarteVisite.First().TelPort.ToString();
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Nationalite))
                {
                    Nationalite = GetMyCardDataContext.Instance.MaCarteVisite.First().Nationalite;
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Societe))
                {
                    Societe = GetMyCardDataContext.Instance.MaCarteVisite.First().Societe;
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Logo))
                {
                    Logo = GetMyCardDataContext.Instance.MaCarteVisite.First().Logo;
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Poste))
                {
                    Poste = GetMyCardDataContext.Instance.MaCarteVisite.First().Poste;
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().SiteWeb))
                {
                    SiteWeb = GetMyCardDataContext.Instance.MaCarteVisite.First().SiteWeb;
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Adresse))
                {
                    Adresse = GetMyCardDataContext.Instance.MaCarteVisite.First().Adresse;
                }
                if (GetMyCardDataContext.Instance.MaCarteVisite.First().CP != 0)
                {
                    CP = GetMyCardDataContext.Instance.MaCarteVisite.First().CP.ToString();
                }
                if (string.IsNullOrEmpty(GetMyCardDataContext.Instance.MaCarteVisite.First().Pays))
                {
                    Pays = GetMyCardDataContext.Instance.MaCarteVisite.First().Pays;
                }

            #endregion
            }

        }

        #endregion


        #region Methods

        private bool CanExecuteValidate(object parameters)
        {
            //return (!string.IsNullOrEmpty(Nom) && !string.IsNullOrEmpty(Prenom));
            return true;
        }

        private void ExecuteValidate(object parameters)
        {
            MaCarteVisite c = new MaCarteVisite();

            //Si il y a déjà un enregistreement
            if (GetMyCardDataContext.Instance.MaCarteVisite.Count() > 0)
            {
                #region Changement des propriétées

                c = GetMyCardDataContext.Instance.MaCarteVisite.First();

                c.Nom = Nom;
                c.Prenom = Prenom;

                GetMyCardDataContext.Instance.SubmitChanges();

                /*
                if (!string.IsNullOrEmpty(Photo))
                {
                    GetMyCardDataContext.Instance.Contact.First().Photo = Photo;
                }
                if (!string.IsNullOrEmpty(Mail))
                {
                    GetMyCardDataContext.Instance.Contact.First().Mail = Mail;
                }
                if (!string.IsNullOrEmpty(TelFixe))
                {
                    GetMyCardDataContext.Instance.Contact.First().TelFixe = int.Parse(TelFixe);
                }
                if (!string.IsNullOrEmpty(TelPort))
                {
                    GetMyCardDataContext.Instance.Contact.First().TelPort = int.Parse(TelPort);
                }
                if (!string.IsNullOrEmpty(Nationalite))
                {
                    GetMyCardDataContext.Instance.Contact.First().Nationalite = Nationalite;
                }
                if (!string.IsNullOrEmpty(Logo))
                {
                    GetMyCardDataContext.Instance.Contact.First().Logo = Logo;
                }
                if (!string.IsNullOrEmpty(Poste))
                {
                    GetMyCardDataContext.Instance.Contact.First().Poste = Poste;
                }
                if (!string.IsNullOrEmpty(SiteWeb))
                {
                    GetMyCardDataContext.Instance.Contact.First().SiteWeb = SiteWeb;
                }
                if (!string.IsNullOrEmpty(Adresse))
                {
                    GetMyCardDataContext.Instance.Contact.First().Adresse = Adresse;
                }
                if (!string.IsNullOrEmpty(Ville))
                {
                    GetMyCardDataContext.Instance.Contact.First().Ville = Ville;
                }
                if (!string.IsNullOrEmpty(CP))
                {
                    GetMyCardDataContext.Instance.Contact.First().CP = int.Parse(CP);
                }
                if (!string.IsNullOrEmpty(Pays))
                {
                    GetMyCardDataContext.Instance.Contact.First().Pays = Pays;
                }
                */


                App.RootFrame.GoBack();

                #endregion
            }
            else
            {
                #region Ajout en BDD

                c.Nom = Nom;
                c.Prenom = Prenom;

                if (!string.IsNullOrEmpty(Photo))
                {
                    c.Photo = Photo;
                }
                if (!string.IsNullOrEmpty(Mail))
                {
                    c.Mail = Mail;
                }
                if (!string.IsNullOrEmpty(TelFixe))
                {
                    c.TelFixe = int.Parse(TelFixe);
                }
                if (!string.IsNullOrEmpty(TelPort))
                {
                    c.TelPort = int.Parse(TelPort);
                }
                if (!string.IsNullOrEmpty(Nationalite))
                {
                    c.Nationalite = Nationalite;
                }
                if (!string.IsNullOrEmpty(Logo))
                {
                    c.Logo = Logo;
                }
                if (!string.IsNullOrEmpty(Poste))
                {
                    c.Poste = Poste;
                }
                if (!string.IsNullOrEmpty(SiteWeb))
                {
                    c.SiteWeb = SiteWeb;
                }
                if (!string.IsNullOrEmpty(Adresse))
                {
                    c.Adresse = Adresse;
                }
                if (!string.IsNullOrEmpty(Ville))
                {
                    c.Ville = Ville;
                }
                if (!string.IsNullOrEmpty(CP))
                {
                    c.CP = int.Parse(CP);
                }
                if (!string.IsNullOrEmpty(Pays))
                {
                    c.Pays = Pays;
                }

                GetMyCardDataContext.Instance.MaCarteVisite.InsertOnSubmit(c);
                GetMyCardDataContext.Instance.SubmitChanges();

                App.RootFrame.GoBack();

                #endregion
            }
        }


        public void ExecuteImportPhoto(object parameters)
        {
            _PhotoChooserTask = new PhotoChooserTask();
            _PhotoChooserTask.ShowCamera = true;
            _PhotoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            _PhotoChooserTask.Show();
        }


        public bool CanExecuteImportPhoto(object parameters)
        {
            return true;
        }

        void photoChooserTask_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult MaPhoto)
        {
            if (MaPhoto.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(MaPhoto.OriginalFileName);

            }
        }
        #endregion
    }
}