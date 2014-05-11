﻿using Microsoft.Phone.Tasks;
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

        private DelegateCommand _ValidateCommand;
        private DelegateCommand _ImportPhotoCommand;
        private DelegateCommand _CameraPhotoCommand;
        private PhotoChooserTask photoChooserTask;
        private CameraCaptureTask cameraCaptureTask;
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
        }
        public DelegateCommand ImportPhotoCommand
        {
            get { return _ImportPhotoCommand; }
            set { _ImportPhotoCommand = value; }
        }
        public DelegateCommand ImportCameraCommand
        {
            get { return _CameraPhotoCommand; }
            set { _CameraPhotoCommand = value; }
        }
        #endregion



        #region Constructeur
        
        public ViewModelMaCarte()
        {
            _ValidateCommand = new DelegateCommand(ExecuteValidate, CanExecuteValidate);
            _ImportPhotoCommand = new DelegateCommand(ExecuteImportPhoto, CanExecuteImportPhoto);
            _CameraPhotoCommand = new DelegateCommand(ExecutecameraPhoto, CanExecuteCameraPhoto);
          
        }

        #endregion




        #region Methods

        private bool CanExecuteValidate(object parameters)
        {
            return !string.IsNullOrWhiteSpace(Nom) || !string.IsNullOrWhiteSpace(Prenom);
        }

        private void ExecuteValidate(object parameters)
        {
            //TODO : Enregistrer en base
            MessageBox.Show("Nom : " + Nom + 
                "\nPrénom : " + Prenom + 
                "\nPhoto : " + Photo +
                "\nMail : " + Mail +
                "\nTéléphone fixe : " + TelFixe +
                "\nTéléphone portable : " + TelPort +
                "\nNationalité : " + Nationalite +
                "\nLogo : " + Logo + 
                "\nPoste : " + Poste +
                "\nSite web : " + SiteWeb +
                "\nAdresse : " + Adresse +
                "\nVille : " + Ville +
                "\nCode postal : " + CP +
                "\n Pays : " + Pays);
        }

        public void ExecuteImportPhoto(object parameters)
        {
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }
        public bool CanExecuteImportPhoto(object parameters)
        {
            return true;
        }

        void photoChooserTask_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult MaPhoto)
        {
            if (MaPhoto.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                //MessageBox.Show(e.ChosenPhoto.ToString());
                //Photo e
                MessageBox.Show(MaPhoto.ChosenPhoto.ToString() + MaPhoto.OriginalFileName);
                //Image à sauvegarder
                //BinaryReader reader = new BinaryReader(e.ChosenPhoto);
                //image1.Source = new BitmapImage(new Uri(e.OriginalFileName));

                //Code to display the photo on the page in an image control named myImage.
                //System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                //bmp.SetSource(MaPhoto.ChosenPhoto);
                //MaPhotoBox.Source = bmp;
            }
        }
        public bool CanExecuteCameraPhoto(object parameters)
        {
            return true;
        }
        public void ExecutecameraPhoto(object parameters)
        {
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
            cameraCaptureTask.Show();
        }
        public void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show(e.ChosenPhoto.Length.ToString());

                //Code to display the photo on the page in an image control named myImage.
                //System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                //bmp.SetSource(e.ChosenPhoto);
                //myImage.Source = bmp;
            }
        }
        #endregion
    }
}
