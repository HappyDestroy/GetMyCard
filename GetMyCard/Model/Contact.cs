using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using WP.Core;
using System.Data.Linq;

namespace GetMyCard.Model
{
    [Table]
    public class Contact : ObservableObject
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
        private string   _Ville;
        private int     _CP;
        private string _Pays;
       // private EntityRef<Categorie> _CategorieRef;
       #endregion

       #region Properties
        [Column(IsPrimaryKey = true, DbType = "BigInt NOT NULL IDENTITY", CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Identifiant
        {
            get { return _Identifiant; }
            set { Assign(ref _Identifiant, value); }
        }
        [Column(DbType = "NVarChar(250) NOT NULL", CanBeNull=false)]
        public string Nom
        {
            get { return _Nom; }
            set { Assign(ref _Nom, value); }
        }
        [Column(DbType = "NVarChar(250) NOT NULL", CanBeNull = false)]
        public string Prenom
        {
            get { return _Prenom; }
            set { Assign(ref _Prenom, value); }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Photo
        {
            get { return _Photo; }
            set { Assign(ref _Photo, value); }
        }
        [Column(DbType = "NVarChar(250)")]
        public string Mail
        {
            get { return _Mail; }
            set { Assign(ref _Mail, value); }
        }
        [Column(DbType="Int")]
        public int TelFixe
        {
            get { return _TelFixe; }
            set { Assign(ref _TelFixe, value); }
        }
        [Column(DbType="Int")]
        public int TelPort
        {
            get { return _TelPort; }
            set { Assign(ref _TelPort, value); }
        }
        [Column(DbType="NVarChar(250)")]
        public string Nationalite
        {
            get { return _Nationalite; }
            set { Assign(ref _Nationalite, value); }
        }
        [Column(DbType="NVarChar(250)")]
        public string Societe
        {
            get { return _Societe; }
            set { Assign(ref _Societe, value); }
        }
        [Column(DbType="NVarChar(250)")]
        public string Poste
        {
            get { return _Poste; }
            set { Assign(ref _Poste, value); }
        }
        [Column(DbType="NVarchar(250)")]
        public string Logo
        {
            get { return _Logo; }
            set { Assign(ref _Logo, value); }
        }
        [Column(DbType="NVarchar(250)")]
        public string SiteWeb
        {
            get { return _SiteWeb; }
            set { Assign(ref _SiteWeb, value); }
        }
        [Column(DbType="NVarChar(250)")]
        public string Adresse
        {
            get { return _Adresse; }
            set { Assign(ref _Adresse, value); }
        }
        [Column(DbType="NVarChar(250)")]
        public string Ville
        {
            get { return _Ville; }
            set { Assign(ref _Ville, value); }
        }
        [Column(DbType="Int")]
        public int CP
        {
            get { return _CP; }
            set { Assign(ref _CP, value); }
        }
        [Column(DbType="NVarChar(250)")]
        public string Pays
        {
            get { return _Pays; }
            set { Assign(ref _Pays, value); }
        }

       #endregion
   }
}
