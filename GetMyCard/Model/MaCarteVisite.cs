using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using WP.Core;
using System.Data.Linq;
using System.Windows.Media;

namespace GetMyCard.Model
{
    [Table]
    public class MaCarteVisite : ObservableObject
    {
        #region Fields
        private long    _Identifiant;
        private string  _Nom;
        private string  _Prenom;
        private string  _Photo;
        private string  _Mail;
        private int     _TelFixe;
        private int     _TelPort;
        private string  _Nationalite;
        private string  _Societe;
        private string  _Poste;
        private string  _Logo;
        private string  _SiteWeb;
        private string  _Adresse;
        private string  _Ville;
        private int     _CP;
        private string  _Pays;

        #endregion

        #region Properties
        [Column(IsPrimaryKey = true, DbType = "BigInt NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Identifiant
        {
            get { return _Identifiant; }
            set { _Identifiant = value; }
        }
        [Column(DbType = "NVarChar(250) NOT NULL", CanBeNull = false)]
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }
        [Column(DbType = "NVarChar(250) NOT NULL", CanBeNull = false)]
        public string Prenom
        {
            get { return _Prenom; }
            set { _Prenom = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Photo
        {
            get { return _Photo; }
            set { _Photo = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }
        [Column(DbType = "Int")]
        public int TelFixe
        {
            get { return _TelFixe; }
            set { _TelFixe = value; }
        }
        [Column(DbType = "Int")]
        public int TelPort
        {
            get { return _TelPort; }
            set { _TelPort = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Nationalite
        {
            get { return _Nationalite; }
            set { _Nationalite = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Societe
        {
            get { return _Societe; }
            set { _Societe = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Poste
        {
            get { return _Poste; }
            set { _Poste = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string SiteWeb
        {
            get { return _SiteWeb; }
            set { _SiteWeb = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Adresse
        {
            get { return _Adresse; }
            set { _Adresse = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Ville
        {
            get { return _Ville; }
            set { _Ville = value; }
        }
        [Column(DbType = "Int")]
        public int CP
        {
            get { return _CP; }
            set { _CP = value; }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Pays
        {
            get { return _Pays; }
            set { _Pays = value; }
        }
        //[Association(Storage = "Categorie", ThisKey = "IdentifiantCategory", IsForeignKey = true)]
        //public Categorie Categoryie
        //{
        //    get { return _CategorieRef.Entity; }
        //    set { _CategorieRef.Entity = value; }

        //}
        #endregion

        #region Constructors
        //public Contact()
        //{
        //    _CategorieRef = new EntityRef<Categorie>();
        //}
        #endregion
    }
}
