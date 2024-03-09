using System;
using System.Drawing.Imaging;

namespace Valvetwebb.Objekt
{
    /// <summary>
    /// Valvpost innehåller information om en valvpost
    /// </summary>
    [Serializable]
    public class ValvPost
    {
        #region Egenskaper
        public int PostID { get; set; }

        public int AnvandarID { get; set; }
        public string Konto { get; set; }
        public string Inloggning { get; set; }
        public string Losenord { get; set; }
        public string Postnamn { get; set; }
        public string Webbadress { get; set; }
        public string Anteckningar { get; set; }
        public string AnvandarNamnSkapad { get; set; }

        public string SkapadDatum { get; set; }

        public string AnvandarNamnUppdat { get; set; }
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