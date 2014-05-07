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

        private DelegateCommand _validateCommand;
        //private ObservableCollection<Category> _categories;

        #endregion


        #region Properties

        public DelegateCommand ValidateCommand
        {
            get { return _validateCommand; }
        }

        /*public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            private set { Assign(ref _categories, value); }
        }*/

        #endregion



        #region Constructeur

        public ViewModelMainPage()
        {
            /*_validateCommand = new DelegateCommand(ExecuteValidate, CanExecuteValidate);
            _deleteBDDCommand = new DelegateCommand(ExecuteDeleteBDD, CanExecuteDeleteBDD);
            _categories = new ObservableCollection<Category>();*/
        }


        #endregion


        #region Methods

        /*private bool CanExecuteValidate(object parameters)
        {
            return !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteValidate(object parameters)
        {
            try
            {
                PasswordBoxDataContext.Initialize();

                App.RootFrame.Navigate(new Uri("/View/ViewPasswordBox.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                MessageBox.Show("Le mot de passe n'est pas bon.");
            }
        }

        public void LoadData()
        {
            foreach (Category category in PasswordBoxDataContext.Instance.Categories)
            {
                Categories.Add(category);
            }
        }*/

        #endregion
    }
}
