using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GetMyCard.Resources;
using System.Windows.Media.Imaging;

namespace GetMyCard
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Fields

        private string _nom;
        private string _prenom;
        private string _telephone;

        private List<Contact> myContacts;
        private Contact _SelectedContact;

        #endregion

        public Contact SelectedContact
        {
            get { return _SelectedContact; }
            set { _SelectedContact = value; }
        }


        #region Constructeur
        public MainPage()
        {
            InitializeComponent();


            myContacts = new List<Contact>
            {
                new Contact("Jean", "Pierre", "Angers", 0123456789, "/Images/contact.png"),
                new Contact("Nico", "Sabou", "Cholet", 0123456789, "/Images/contact.png"),
                new Contact("René", "Bernard", "Nantes", 0123456789, "/Images/contact.png")
            };

            ContactList.ItemsSource = myContacts;


        }
        #endregion
        


        #region Accesseurs

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        #endregion


        #region Methods


         private void ApplicationBarMenuItem_Click_MaCarte(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/MaCarte.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click_Param(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Param.xaml", UriKind.Relative));
        }


        private void ContactList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("ok");
        }
        #endregion

        
    }
}
