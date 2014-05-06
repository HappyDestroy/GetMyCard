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


        #endregion



        #region Constructeur
        public MainPage()
        {
            InitializeComponent();


            myContacts = new List<Contact>
            {
                new Contact("Jean", "Pierre", "Angers", 0123456789),
                new Contact("Nico", "Sabou", "Cholet", 0123456789),
                new Contact("René", "Bernard", "Nantes", 0123456789)
            };

            ContactList.ItemsSource = myContacts.Select(e => new ContactBinding { Nom = e.Nom, Image = getContactImage(), Prenom = e.Prenom });


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

        private BitmapImage getContactImage()
        {
            return new BitmapImage(new Uri("/Images/contact.png", UriKind.Relative));
        }


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

        
        private void ContactList_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("lol");
        }
        #endregion

        
    }
}
