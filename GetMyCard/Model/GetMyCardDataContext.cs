using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMyCard.Model
{
    public class GetMyCardDataContext : DataContext
    {
        #region Fields

        private static GetMyCardDataContext  _Instance;
        private static object                _InstanceLocker;

        #endregion



        #region Properties

        /// <summary>
        /// Table des contacts
        /// </summary>
        public Table<Contact> Contact
        {
            get { return GetTable<Contact>();  }
        }
        
        /// <summary>
        /// Table MaCarte
        /// </summary>
        public Table<MaCarte> MaCarte
        {
            get { return GetTable<MaCarte>();  }
        }


        public static GetMyCardDataContext Instance
        {
            get
            {
                lock(_InstanceLocker)
                {
                    if (_Instance == null)
                    {
                        Initialize();
                    }
                    return _Instance;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur static. Est appelé lors de l'initialisation de la première instance ou au premier accès
        /// à une propriété ou une méthode statique. Permet entre autre l'inisialisation des champs statiques.
        /// </summary>
        static GetMyCardDataContext()
        {
            _InstanceLocker = new object();
        }

        /// <summary>
        ///  Initialise une nouvelle instance de la classe
        /// </summary>
        /// <param name="connection">Chaine de connexion à utiliser</param>
        /// <param name="wipe">Détermine si la base de données existante doit être supprimée</param>
        private GetMyCardDataContext(string connection, bool wipe = false)
            : base(connection) //Permet d'appeler le constructeur parent en lui passant la chaine de connexion
        {
            if (wipe && this.DatabaseExists())
            {
                this.DeleteDatabase();
            }
            if (! this.DatabaseExists())
            {
                this.CreateDatabase();
            }
            this.SubmitChanges(); //Permet de sauvegarder et / ou de forcer l'ouverture de la connexion
        }
    
    
        #endregion
    
        #region Methods
        public static void Initialize(string connection = "", bool wipe = false)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "Data Source='isostore:/GetMyCardDB.sdf';";
            }
            _Instance = new GetMyCardDataContext(connection, wipe);
        }
        #endregion
   }
}

