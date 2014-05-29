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
using GetMyCard.ViewModels;

namespace GetMyCard
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SystemTray.SetProgressIndicator(this, new ProgressIndicator());
        }

        //Lors du click sur le bouton "Ma carte" du menu
        private void GoToMaCarte(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Views/MaCarte.xaml", UriKind.Relative));
        }

        //Lors du click sur le bouton "Paramètres" sur menu
        private void GoToParams(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Views/Params.xaml", UriKind.Relative));
        }

        //Pour charcer les donnée de la BDD à l'arrivé sur la page
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.DataContext is ViewModelMainPage)
            {
                ((ViewModelMainPage)this.DataContext).LoadData();
            }
        }
    }
}
