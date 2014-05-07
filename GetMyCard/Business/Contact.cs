using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMyCard
{
    public class Contact
    {
        #region Fields

        private string _nom;
        private string _prenom;
        private string _adresse;
        private int _telephone;
        private string _imageProfil;

        #endregion



        #region Constructeur

        public Contact()
        {

        }

        public Contact(string nom, string prenom, string adresse, int telephone, string imageProfil)
        {
            this._nom = nom;
            this._prenom = prenom;
            this._adresse = adresse;
            this._telephone = telephone;
            this._imageProfil = imageProfil;
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

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        public int Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        public string ImageProfil
        {
            get { return _imageProfil; }
            set { _imageProfil = value; }
        }

        #endregion



        #region Override

        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

        #endregion

    }
}
