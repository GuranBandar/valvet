using System;

namespace Valvetwebb.Objekt
{
    /// <summary>
    /// Anvandare innehåller information om en användare
    /// </summary>
    [Serializable]
    public class Anvandare
    {
        #region "Egenskaper"
        /// <summary>
        /// Aktuellt anvandareID
        /// </summary>
        public int AnvandarID { get; set; }

        /// <summary>
        /// Aktuellt användarnamn
        /// </summary>
        public string Anvandarnamn { get; set; }

        /// <summary>
        /// Aktuellt lösenord
        /// </summary>
        public string Losenord { get; set; }

        /// <summary>
        /// Senaste inloggningsdatum
        /// </summary>
        public string SenastInloggadDatum { get; set; }

        /// <summary>
        /// Senast bytt lösenord datum
        /// </summary>
        public string SenastByttLosenordDatum { get; set; }

        /// <summary>
        /// Användarens epostadress
        /// </summary>
        public string Epostadress { get; set; }

        /// <summary>
        /// Användaren aktiv
        /// </summary>
        public string Aktiv { get; set; }

        /// <summary>
        /// Användarens konto
        /// </summary>
        public string Konto { get; set; }
        #endregion

        /// <summary>
        /// Antal misslyckade inloggningsförsök
        /// </summary>
        public int MisslyckadeInloggningar { get; set; }
        /// <summary>
        /// Defaultkonstruktor
        /// </summary>
        public Anvandare()
        {
        }
    }
}
