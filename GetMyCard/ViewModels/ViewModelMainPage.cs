using GetMyCard.Model;
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

        private ObservableCollection<Contact> _Contact;

        #endregion



        #region Properties

        public DelegateCommand DeleteContactCommand
        {
            get { return _DeleteContactCommand; }
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return _Contact; }
            private set { Assign(ref _Contact, value); }
        }

        #endregion



        #region Constructeur

        public ViewModelMainPage()
        {
            _DeleteContactCommand = new DelegateCommand(ExecuteDeleteContact, CanExecuteDeleteContact);
        }


        #endregion



        #region Methods

        private bool CanExecuteDeleteContact(object parameters)
        {
            //TODO : Vérifier que le contact existe
            return true;
        }

        private void ExecuteDeleteContact(object parameters)
        {
            try
            {
                //TOTO : suppr le contat

                if(MessageBoxResult.OK == MessageBox.Show("Êtes-vous sûr de supprimer ce contact ?", "Suppression", MessageBoxButton.OKCancel))
                {
                    _Contact.Remove((Contact)parameters);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur de suppresion.");
            }
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
