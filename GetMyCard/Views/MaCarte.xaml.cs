using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GetMyCard
{
    public partial class MaCarte : PhoneApplicationPage
    {
        public MaCarte()
        {
            InitializeComponent();
        }

        private void ApplicationBarMenuItem_Click_MaCarte(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/MaCarte.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click_Param(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Param.xaml", UriKind.Relative));
        }
    }
}