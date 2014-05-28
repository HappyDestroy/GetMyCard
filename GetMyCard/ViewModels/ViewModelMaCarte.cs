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

        private BitmapImage _ChosenImage;
        private BitmapImage _ChosenLogo;
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

        private MaCarteVisite _MaCarteVisite;

        #endregion

        #region Properties
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
            get { return _ChosenImage; }
            set { _ChosenImage = value; }
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
            get { return _ChosenLogo; }
            set { _ChosenLogo = value; }
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

        public MaCarteVisite MaCarteVisite
        {
            get { return _MaCarteVisite; }
            set
            {
                if (_MaCarteVisite != null)
                    _MaCarteVisite.PropertyChanged -= value_PropertyChanged;

                Assign(ref _MaCarteVisite, value); 
                ValidateCommand.OnCanExecuteChanged();

                if (value != null)
                    value.PropertyChanged += value_PropertyChanged;
            }
        }

        private void value_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ValidateCommand.OnCanExecuteChanged();
        }

        #endregion


        #region Constructeur

        public ViewModelMaCarte()
        {
            _ValidateCommand = new DelegateCommand(ExecuteValidate, CanExecuteValidate);
            _ImportPhotoCommand = new DelegateCommand(ExecuteImportPhoto, CanExecuteImportPhoto);
            _ImportLogoCommand = new DelegateCommand(ExecuteImportLogo, CanExecuteImportLogo);
            _ChosenImage = new BitmapImage();
            _ChosenLogo = new BitmapImage();
            MaCarteVisite = new MaCarteVisite();

            #region Récupération des infos MaCarte

            if (GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                MaCarteVisite = GetMyCardDataContext.Instance.MaCarteVisite.First();

                //image avatar
                if (!string.IsNullOrEmpty(MaCarteVisite.Photo))
                {
                    BitmapImage retrievedImage = new BitmapImage();
                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var isoFileStream = isoStore.OpenFile(MaCarteVisite.Photo, System.IO.FileMode.Open))
                        {
                            retrievedImage.SetSource(isoFileStream);
                        }

                        MaPhotoBox = retrievedImage;
                    }
                }

                //image Logo
                if (!string.IsNullOrEmpty(MaCarteVisite.Logo))
                {
                    BitmapImage retrievedImage = new BitmapImage();
                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var isoFileStream = isoStore.OpenFile(MaCarteVisite.Logo, System.IO.FileMode.Open))
                        {
                            retrievedImage.SetSource(isoFileStream);
                        }

                        MonLogoBox = retrievedImage;
                    }
                }
            }

            #endregion

        }

        #endregion


        #region Methods

        #region ExecuteValidate
        private bool CanExecuteValidate(object parameters)
        {
            return !string.IsNullOrWhiteSpace(MaCarteVisite.Nom) && !string.IsNullOrWhiteSpace(MaCarteVisite.Prenom);
        }


        #region Validation
        private void ExecuteValidate(object parameters)
        {
            if (GetMyCardDataContext.Instance.MaCarteVisite.Any())
            {
                MaCarteVisite c = GetMyCardDataContext.Instance.MaCarteVisite.First();

                c = MaCarteVisite;


                if (!string.IsNullOrEmpty(SrcPhoto))
                {
                    Random random = new Random();
                    int num = random.Next(1, 1000);

                    string ext = SrcPhoto.Split(".".ToCharArray()).Last();
                    PathPhoto = "photo_" + MaCarteVisite.Nom + "-" + MaCarteVisite.Prenom + "-" + num + "." + ext;
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
                    PathLogo = "logo_" + MaCarteVisite.Nom + "-" + MaCarteVisite.Prenom + "-" + num + "." + ext;
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

                GetMyCardDataContext.Instance.SubmitChanges();
            }
            else
            {
                if (!string.IsNullOrEmpty(SrcPhoto))
                {
                    Random random = new Random();
                    int num = random.Next(1, 1000);

                    string ext = SrcPhoto.Split(".".ToCharArray()).Last();
                    PathPhoto = "photo_" + MaCarteVisite.Nom + "-" + MaCarteVisite.Prenom + "-" + num + "." + ext;
                    MaCarteVisite.Photo = PathPhoto;

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
                    PathLogo = "logo_" + MaCarteVisite.Nom + "-" + MaCarteVisite.Prenom + "-" + num + "." + ext;
                    MaCarteVisite.Logo = PathLogo;

                    using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        var wb = new WriteableBitmap(ChoosenLogo);

                        using (var isoFileStream = isoStore.CreateFile(PathLogo))
                        {
                            Extensions.SaveJpeg(wb, isoFileStream, wb.PixelHeight, wb.PixelWidth, 0, 100);
                        }
                    }
                }

                GetMyCardDataContext.Instance.MaCarteVisite.InsertOnSubmit(MaCarteVisite);
                GetMyCardDataContext.Instance.SubmitChanges();
            }




            App.RootFrame.GoBack();
        }
        #endregion

        #endregion

        #region Import Photo
        public bool CanExecuteImportPhoto(object parameters)
        {
            return true;
        }

        public void ExecuteImportPhoto(object parameters)
        {
            _PhotoChooserTask = new PhotoChooserTask();
            _PhotoChooserTask.ShowCamera = true;
            _PhotoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            _PhotoChooserTask.Show();
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
        #endregion

        #region Import Logo

        public bool CanExecuteImportLogo(object parameters)
        {
            return true;
        }

        public void ExecuteImportLogo(object parameters)
        {
            _LogoChooserTask = new PhotoChooserTask();
            _LogoChooserTask.ShowCamera = true;
            _LogoChooserTask.Completed += new EventHandler<PhotoResult>(logoChooserTask_Completed);
            _LogoChooserTask.Show();
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

        #endregion
    }
}