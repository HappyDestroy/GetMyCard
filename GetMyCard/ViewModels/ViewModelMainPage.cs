﻿using GetMyCard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WP.Core;

namespace GetMyCard.ViewModels
{
    public class ViewModelMainPage : ObservableObject
    {
        #region Fields

        private DelegateCommand _DeleteContactCommand;
        private DelegateCommand _AddContactCommand;

        private ObservableCollection<Contact> _Contacts;
        private MaCarteVisite _MaCarteVisite;

        #endregion



        #region Properties

        public DelegateCommand DeleteContactCommand
        {
            get { return _DeleteContactCommand; }
        }

        public DelegateCommand AddContactCommand
        {
            get { return _AddContactCommand; }
            set { _AddContactCommand = value; }
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return _Contacts; }
            private set { Assign(ref _Contacts, value); }
        }

        public MaCarteVisite MaCarteVisite
        {
            get { return _MaCarteVisite; }
            set { _MaCarteVisite = value; }
        }

        public string PhotoMoi
        {
            get { return _MaCarteVisite.Photo; }
        }

        public string NomMoi
        {
            get { return _MaCarteVisite.Nom; }
        }

        public string PrenomMoi
        {
            get { return _MaCarteVisite.Prenom; }
        }

        #endregion



        #region Constructeur

        public ViewModelMainPage()
        {
            Contact c = new Contact();
            c.Nom = "Nico";
            c.Prenom = "Sabou";
            c.Photo = "Images/contact.png";
            GetMyCardDataContext.Instance.Contact.InsertOnSubmit(c);
            GetMyCardDataContext.Instance.SubmitChanges();

            _DeleteContactCommand = new DelegateCommand(ExecuteDeleteContact, CanExecuteDeleteContact);
            _AddContactCommand = new DelegateCommand(ExecuteAddContact, CanExecuteAddContact);
            _Contacts = new ObservableCollection<Contact>();

            if(GetMyCardDataContext.Instance.MaCarteVisite.Count() > 0)
            {
                _MaCarteVisite = GetMyCardDataContext.Instance.MaCarteVisite.First();
            }
            else
            {
                MaCarteVisite = new MaCarteVisite();
                MaCarteVisite.Photo = "Images/contact.png";
                MaCarteVisite.Nom = "test";
                MaCarteVisite.Prenom = "test";
            }
            
        }


        #endregion



        #region Methods

        private bool CanExecuteDeleteContact(object parameters)
        {
            //TODO : Vérifier que le contact existe

            return true;
        }

        private bool CanExecuteAddContact(object parameters)
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

        private void ExecuteAddContact(object parameters)
        {
            Contact c = new Contact();
            c.Nom = "Nico";
            c.Prenom = "Sabou";
            c.Photo = "Images/contact.png";
            GetMyCardDataContext.Instance.Contact.InsertOnSubmit(c);
            GetMyCardDataContext.Instance.SubmitChanges();

            _Contacts.Add(c);
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
