﻿using GetMyCard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WP.Core;

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
        
        #endregion



        #region Constructeur
        
        public ViewModelMaCarte()
        {
            _ValidateCommand = new DelegateCommand(ExecuteValidate, CanExecuteValidate);
        }

        #endregion




        #region Methods

        private bool CanExecuteValidate(object parameters)
        {
            return !string.IsNullOrWhiteSpace(Nom) || !string.IsNullOrWhiteSpace(Prenom);
        }

        private void ExecuteValidate(object parameters)
        {
            MaCarteVisite c = new MaCarteVisite();
            //Si il n'y a rien dans la table MaCarte, on ajoute la notre
            if(GetMyCardDataContext.Instance.MaCarte.First() == null)
            {
                c.Nom = Nom;
                c.Prenom = Prenom;

                #region Test des champs
                if (!string.IsNullOrWhiteSpace(Photo))
                {
                    c.Photo = Photo;
                }

                if (!string.IsNullOrWhiteSpace(Mail))
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

                if (!string.IsNullOrWhiteSpace(Nationalite))
                {
                    c.Nationalite = Nationalite;
                }

                if (!string.IsNullOrWhiteSpace(Logo))
                {
                    c.Logo = Logo;
                }

                if (!string.IsNullOrWhiteSpace(Poste))
                {
                    c.Poste = Poste;
                }

                if (!string.IsNullOrWhiteSpace(SiteWeb))
                {
                    c.SiteWeb = SiteWeb;
                }

                if (!string.IsNullOrWhiteSpace(Adresse))
                {
                    c.Adresse = Adresse;
                }

                if (!string.IsNullOrWhiteSpace(Ville))
                {
                    c.Ville = Ville;
                }

                if (CP != null)
                {
                    c.CP = int.Parse(CP);
                }

                if (!string.IsNullOrWhiteSpace(Pays))
                {
                    c.Pays = Pays;
                }
                #endregion

                GetMyCardDataContext.Instance.MaCarte.InsertOnSubmit(c);
                GetMyCardDataContext.Instance.SubmitChanges();
            }
            else
            {
                c = GetMyCardDataContext.Instance.MaCarte.First();

                c.Nom = Nom;
                c.Prenom = Prenom;

                #region Test des champs
                if (!string.IsNullOrWhiteSpace(Photo))
                {
                    c.Photo = Photo;
                }

                if (!string.IsNullOrWhiteSpace(Mail))
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

                if (!string.IsNullOrWhiteSpace(Nationalite))
                {
                    c.Nationalite = Nationalite;
                }

                if (!string.IsNullOrWhiteSpace(Logo))
                {
                    c.Logo = Logo;
                }

                if (!string.IsNullOrWhiteSpace(Poste))
                {
                    c.Poste = Poste;
                }

                if (!string.IsNullOrWhiteSpace(SiteWeb))
                {
                    c.SiteWeb = SiteWeb;
                }

                if (!string.IsNullOrWhiteSpace(Adresse))
                {
                    c.Adresse = Adresse;
                }

                if (!string.IsNullOrWhiteSpace(Ville))
                {
                    c.Ville = Ville;
                }

                if (CP != null)
                {
                    c.CP = int.Parse(CP);
                }

                if (!string.IsNullOrWhiteSpace(Pays))
                {
                    c.Pays = Pays;
                }
                #endregion

                GetMyCardDataContext.Instance.SubmitChanges();
            }

            
        }

        #endregion
    }
}
