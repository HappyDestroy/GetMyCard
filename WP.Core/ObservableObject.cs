using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WP.Core
{
    /// <summary>
    ///     Classe de base pour les objets source de binding.
    ///     Implémente INotifyPropertyChanged et INotifyPropertyChanging.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Events

        /// <summary>
        ///     Se produit lorsqu'une propriété de l'objet a changée de valeur.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Se produit lorsqu'une propriété de l'objet est sur le point de changer.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region Methods

        /// <summary>
        ///     Permet de déclencher l'évènement PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changée.</param>
        //L'attribut CallerMemberName permet de donner comme valeur par défaut le nom du membre appelant.
        // = "" permet de rendre optionnel le paramètre propertyName
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler tempHandler = PropertyChanged;

            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///     Permet de déclencher l'évènement PropertyChanging.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui va changée.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            PropertyChangingEventHandler tempHandler = PropertyChanging;

            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangingEventArgs(propertyName));
            }
        }


        protected virtual void Assign<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (field == null || !field.Equals(newValue))
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OnPropertyChanging(propertyName));
                field = newValue;
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OnPropertyChanged(propertyName));
            }
        }

        #endregion


    }
}
