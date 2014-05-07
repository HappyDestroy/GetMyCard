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

        private string _Nom;
        private string _Prenom;
        private string _Adresse;
        private int _Telephone;
        private string _ImageProfil;

        #endregion



        #region Constructeur

        public Contact()
        {

        }

        public Contact(string nom, string prenom, string adresse, int telephone, string imageProfil)
        {
            this._Nom = nom;
            this._Prenom = prenom;
            this._Adresse = adresse;
            this._Telephone = telephone;
            this._ImageProfil = imageProfil;
        }

        #endregion



        #region Accesseurs

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public string Prenom
        {
            get { return _Prenom; }
            set { _Prenom = value; }
        }

        public string Adresse
        {
            get { return _Adresse; }
            set { _Adresse = value; }
        }

        public int Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        public string ImageProfil
        {
            get { return _ImageProfil; }
            set { _ImageProfil = value; }
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
