using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace GetMyCard
{
    public partial class MaCarte : PhoneApplicationPage
    {
        #region Fields
        PhotoChooserTask photoChooserTask;
        #endregion
        public MaCarte()
        {
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            InitializeComponent();
        }



        public EventHandler<PhotoResult> photoChooserTask_Completed { get; set; }
    }
}