using System;

namespace Valvetwebb.Objekt
{
    /// <summary>
    /// Valvpost innehåller information om en valvpost
    /// </summary>
    [Serializable]
    public class ValvPost
    {
        #region Egenskaper
        /// <summary>
        /// Aktuellt PostID
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// Aktuellt AnvandareID
        /// </summary>
        public int AnvandarID { get; set; }

        /// <summary>
        /// Aktuellt Konto
        /// </summary>
        public string Konto { get; set; }

        /// <summary>
        /// Aktuellt Usernamn
        /// </summary>
        public string Usernamn { get; set; }

        /// <summary>
        /// Aktuellt Lösenord
        /// </summary>
        public string Losenord { get; set; }

        /// <summary>
        /// Aktuellt Postnamn
        /// </summary>
        public string Postnamn { get; set; }

        /// <summary>
        /// Aktuellt Webbadress
        /// </summary>
        public string Webbadress { get; set; }

        /// <summary>
        /// Aktuellt Anteckningar
        /// </summary>
        public string Anteckningar { get; set; }

        /// <summary>
        /// Aktuellt AnvandarNamn skapad
        /// </summary>
        public string AnvandarNamnSkapad { get; set; }

        /// <summary>
        /// Aktuellt Skapat datum
        /// </summary>
        public string SkapadDatum { get; set; }

        /// <summary>
        /// Aktuellt AnvandarNamn uppdaterad
        /// </summary>
        public string AnvandarNamnUppdat { get; set; }

        /// <summary>
        /// Aktuellt Uppdateringsdatum
        /// </summary>
        public string UppdatDatum { get; set; }
        #endregion

        /// <summary>
        /// Defaultkonstruktor
        /// </summary>
        public ValvPost()
        {
        }

    }
}