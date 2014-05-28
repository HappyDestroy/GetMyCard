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

        private BitmapImage _ChoosenImage;
        private BitmapImage _ChoosenLogo;
        private ImageSource _MaPhotoBox;
        private ImageSource _MonLogoBox;

        private string _SrcPhoto;
        private string _PathPhoto;
        private string _SrcLogo;
        private string _PathLogo;

        private DelegateCommand _ValidateCommand;
        private DelegateCommand _ImportPhotoCommand;
        private DelegateCommand _ImportLogoCommand;
        private PhotoChooserTask _PhotoChooserTask;
        private PhotoChooserTask _LogoChooserTask;

        #endregion



        #region Properties

        public string Nom
        {
            get { return _Nom; }
            set { Assign(ref _Nom, value); ValidateCommand.OnCanExecuteChanged(); }
        }

        public string Prenom
        {
            get { return _Prenom; }
            set { Assign(ref _Prenom, value); ValidateCommand.OnCanExecuteChanged(); }
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
        }

        public DelegateCommand ImportPhotoCommand
        {
            get { return _ImportPhotoCommand; }
            set { _ImportPhotoCommand = value; }
        }

        public DelegateCommand ImportLogoCommand
        {
            get { return _ImportLogoCommand; }
            set { _ImportLogoCommand = value; }
        }
        public BitmapImage ChoosenImage
        {
            get { return _ChoosenImage; }
            set { _ChoosenImage = value; }
        }

        public ImageSource MaPhotoBox
        {
            get { return _MaPhotoBox; }
            set { Assign(ref _MaPhotoBox, value); }
        }

        public string SrcPhoto
        {
            get { return _SrcPhoto; }
            set { Assign(ref _SrcPhoto, value); }
        }

        public string PathPhoto
        {
            get { return _PathPhoto; }
            set { Assign(ref _PathPhoto, value); }
        }

        public BitmapImage ChoosenLogo
        {
            get { return _ChoosenLogo; }
            set { _ChoosenLogo = value; }
        }
        public ImageSource MonLogoBox
        {
            get { return _MonLogoBox; }
            set { Assign(ref _MonLogoBox, value); }
        }

        public string SrcLogo
        {
            get { return _SrcLogo; }
            set { Assign(ref _SrcLogo, value); }
        }

        public string PathLogo
        {
            get { return _PathLogo; }
            set { Assign(ref _PathLogo, value); }
        }

        #endregion



        #region Constructeur

        public ViewModelMaCarte()
        {
            _ValidateCommand = new DelegateCommand(ExecuteValidate, CanExecuteValidate);
            _ImportPhotoCommand = new DelegateCommand(ExecuteImportPhoto, CanExecuteImportPhoto);
            _ImportLogoCommand = new DelegateCommand(ExecuteImportLogo, CanExecuteImportLogo);
            _ChoosenImage = new BitmapImage();
            _ChoosenLogo = new BitmapImage();

            if(GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                MaCarteVisite c = GetMyCardDataContext.Instance.MaCarteVisite.First();

                Nom = c.Nom;
                Prenom = c.Prenom;

                #region verification des info de l'utilisateur

                //image avatar
                if (!string.IsNullOrEmpty(c.Photo))
                {
                    BitmapImage retrievedImage = new BitmapImage();
                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var isoFileStream = isoStore.OpenFile(c.Photo, System.IO.FileMode.Open))
                        {
                            retrievedImage.SetSource(isoFileStream);
                        }

                        MaPhotoBox = retrievedImage;
                    }
                }

                //image Logo
                if (!string.IsNullOrEmpty(c.Logo))
                {
                    BitmapImage retrievedImage = new BitmapImage();
                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var isoFileStream = isoStore.OpenFile(c.Logo, System.IO.FileMode.Open))
                        {
                            retrievedImage.SetSource(isoFileStream);
                        }

                        MonLogoBox = retrievedImage;
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
        }

        #endregion


        #region Methods

        private bool CanExecuteValidate(object parameters)
        {
            return !string.IsNullOrWhiteSpace(Nom) || !string.IsNullOrWhiteSpace(Prenom);
        }

        private void ExecuteValidate(object parameters)
        {
            MaCarteVisite c;

            if(GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                c = GetMyCardDataContext.Instance.MaCarteVisite.First();

                c.Nom = Nom;
                c.Prenom = Prenom;

                if (!string.IsNullOrEmpty(SrcPhoto))
                {
                    Random random = new Random();
                    int num = random.Next(1, 1000);

                    string ext = SrcPhoto.Split(".".ToCharArray()).Last();
                    PathPhoto = "photo_" + Nom + "-" + Prenom + "-" + num + "." + ext;
                    c.Photo = PathPhoto;

                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var wb = new WriteableBitmap(ChoosenImage);

                        using (var isoFileStream = isoStore.CreateFile(PathPhoto))
                        {
                            Extensions.SaveJpeg(wb, isoFileStream, wb.PixelHeight, wb.PixelWidth, 0, 100);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(SrcLogo))
                {
                    Random random = new Random();
                    int num = random.Next(1, 1000);

                    string ext = SrcLogo.Split(".".ToCharArray()).Last();
                    PathLogo = "logo_" + Nom + "-" + Prenom + "-" + num + "." + ext;
                    c.Photo = PathLogo;

                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var wb = new WriteableBitmap(ChoosenLogo);

                        using (var isoFileStream = isoStore.CreateFile(PathLogo))
                        {
                            Extensions.SaveJpeg(wb, isoFileStream, wb.PixelHeight, wb.PixelWidth, 0, 100);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Mail))
                {
                    c.Mail = Mail;
                }
                if (TelFixe != null)
                {
                    c.TelFixe = int.Parse(TelFixe);
                }
                if (TelPort != null)
                {
                    c.TelPort = int.Parse(TelPort);
                }
                if (!string.IsNullOrEmpty(Nationalite))
                {
                    c.Nationalite = Nationalite;
                }
                if (!string.IsNullOrEmpty(Societe))
                {
                    c.Societe = Societe;
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
                if (CP != null)
                {
                    c.CP = int.Parse(CP);
                }
                if (!string.IsNullOrEmpty(Pays))
                {
                    c.Pays = Pays;
                }
                GetMyCardDataContext.Instance.SubmitChanges();
            }
            else
            {
                c = new MaCarteVisite();

                c.Nom = Nom;
                c.Prenom = Prenom;

                #region verification des champs

                if (!string.IsNullOrEmpty(SrcPhoto))
                {
                    Random random = new Random();
                    int num = random.Next(1, 1000);

                    string ext = SrcPhoto.Split(".".ToCharArray()).Last();
                    PathPhoto = "photo_" + Nom + "-" + Prenom + "-" + num + "." + ext;
                    c.Photo = PathPhoto;

                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var wb = new WriteableBitmap(ChoosenImage);

                        using (var isoFileStream = isoStore.CreateFile(PathPhoto))
                        {
                            Extensions.SaveJpeg(wb, isoFileStream, wb.PixelHeight, wb.PixelWidth, 0, 100);
                        }
                    }
                }


                if (!string.IsNullOrEmpty(SrcLogo))
                {
                    Random random = new Random();
                    int num = random.Next(1, 1000);

                    string ext = SrcLogo.Split(".".ToCharArray()).Last();
                    PathLogo = "logo_" + Nom + "-" + Prenom + "-" + num + "." + ext;
                    c.Logo = PathLogo;

                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var wb = new WriteableBitmap(ChoosenLogo);

                        using (var isoFileStream = isoStore.CreateFile(PathLogo))
                        {
                            Extensions.SaveJpeg(wb, isoFileStream, wb.PixelHeight, wb.PixelWidth, 0, 100);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Mail))
                {
                    c.Mail = Mail;
                }
                if (TelFixe != null)
                {
                    c.TelFixe = int.Parse(TelFixe);
                }
                if (TelPort != null)
                {
                    c.TelPort = int.Parse(TelPort);
                }
                if (!string.IsNullOrEmpty(Nationalite))
                {
                    c.Nationalite = Nationalite;
                }
                if (!string.IsNullOrEmpty(Societe))
                {
                    c.Societe = Societe;
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
                if (CP != null)
                {
                    c.CP = int.Parse(CP);
                }
                if (!string.IsNullOrEmpty(Pays))
                {
                    c.Pays = Pays;
                }

                #endregion

                GetMyCardDataContext.Instance.MaCarteVisite.InsertOnSubmit(c);
                GetMyCardDataContext.Instance.SubmitChanges();
            }


            
           
            App.RootFrame.GoBack();
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
                BitmapImage img = new BitmapImage();
                img.SetSource(MaPhoto.ChosenPhoto);

                //On affiche l'image
                MaPhotoBox = img;

                //On sauvegarde l'objet image
                ChoosenImage = img;

                //On sauvegarde le chemin d'accès de l'image
                SrcPhoto = System.IO.Path.GetFileName(MaPhoto.OriginalFileName); ;
            }
        }

        public void ExecuteImportLogo(object parameters)
        {
            _LogoChooserTask = new PhotoChooserTask();
            _LogoChooserTask.ShowCamera = true;
            _LogoChooserTask.Completed += new EventHandler<PhotoResult>(logoChooserTask_Completed);
            _LogoChooserTask.Show();
        }


        public bool CanExecuteImportLogo(object parameters)
        {
            return true;
        }

        void logoChooserTask_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult MonLogo)
        {
            if (MonLogo.TaskResult == TaskResult.OK)
            {
                BitmapImage img = new BitmapImage();
                img.SetSource(MonLogo.ChosenPhoto);

                //On affiche le logo
                MonLogoBox = img;

                //On sauvegarde l'objet image
                ChoosenLogo = img;

                //On sauvegarde le chemin d'accès du logo
                SrcLogo = System.IO.Path.GetFileName(MonLogo.OriginalFileName); ;
            }
        }
        #endregion
    }
}