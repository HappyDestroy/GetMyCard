using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking.Proximity;
using WP.Core;

namespace GetMyCard.ViewModels
{
    public class ViewModelPartage : ObservableObject
    {
        #region Fields

        private DelegateCommand _ShareCDVCommand;

        private ProximityDevice _ProximityDevice;

        #endregion




        #region Properties
        
        public DelegateCommand ShareCDVCommand
        {
            get{return _ShareCDVCommand;}
        }

        public ProximityDevice ProximityDevice
        {
            get { return _ProximityDevice; }
            set { _ProximityDevice = value; }
        }

        #endregion




        #region Constructeur

        public ViewModelPartage()
        {
            _ShareCDVCommand = new DelegateCommand(ExecuteShareCDV, CanExecuteShareCDV);
        }


        #endregion




        #region Methods

        private bool CanExecuteShareCDV(object parameters)
        {
            //TODO : check NFC activé et periphérique trouvé
            bool ret = false;

            ProximityDevice = ProximityDevice.GetDefault();

            if (ProximityDevice != null)
            {
                // Votre appareil supporte le NFC
                ret = true;
            }
            else
            {
                MessageBox.Show("Votre appareil ne supporte pas la technologie NFC");
            }


            return ret;
        }


        private void ExecuteShareCDV(object paramters)
        {
            var publicationId = ProximityDevice.PublishMessage("Windows.CustomType", // Type du message
                                                               "Hello World!", // Message
                                                               MessageTransmittedHandler); // Callback
        }


        private void MessageTransmittedHandler(Windows.Networking.Proximity.ProximityDevice sender, long messageId)
        {
            //Le message à été envoyé
        }

        #endregion
    }
}
