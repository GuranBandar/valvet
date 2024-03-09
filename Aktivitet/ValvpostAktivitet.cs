using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Valvetwebb.Datalager;
using Valvetwebb.Kontroller;
using Valvetwebb.Objekt;

namespace Valvetwebb.Aktivitet
{
    /// <summary>
    /// Klass för Vavpost aktivitet
    /// 
    /// Innehåller alla metoder för klassen Valvposts verksamhetslogik.
    /// </summary>
    public sealed class ValvpostAktivitet : SökVillkor
    {
        /// <summary>
        /// Hämta alla Valvposter från tabellen Valvpost i aktuell databas
        /// </summary>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public List<ValvPost> HämtaAlla()
        {
            DataSet valvpostDS = new DataSet();
            ValvPostData ValvpostData = new ValvPostData();

            try
            {
                valvpostDS = ValvpostData.HämtaAlla();
                List<ValvPost> Valvpost = new List<ValvPost>(valvpostDS.Tables["ValvPost"].Rows.Count);
                foreach (DataRow rad in valvpostDS.Tables["ValvPost"].Rows)
                {
                    Valvpost.Add(new ValvPost()
                    {
                        PostID = (int)rad["PostID"],
                        AnvandarID = (int)rad["AnvandarID"],
                        Konto = rad["Konto"].ToString(),
                        Inloggning = rad["Inloggning"].ToString(),
                        Losenord = rad["Losenord"].ToString(),
                        Postnamn = rad["Postnamn"].ToString(),
                        Webbadress = rad["Webbadress"].ToString(),
                        Anteckningar = rad["Anteckningar"].ToString(),
                        AnvandarNamnSkapad = rad["AnvandarNamnSkapad"].ToString(),
                        SkapadDatum = rad["SkapadDatum"].ToString(),
                        AnvandarNamnUppdat = rad["AnvandarNamnUppdat"].ToString(),
                        UppdatDatum = rad["UppdatDatum"].ToString()
                    }); ;
                }
                return Valvpost;
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Hämtar rad från tabellen ValvPost i aktuell databas med angiven nyckel.
        /// </summary>
        /// <param name="postID">Aktuellt postID</param>
        /// <param name="anvandarID">Aktuell användarID</param>
        /// <returns>Objekt med efterfrågat data</returns>
        public ValvPost HämtaValvPost(int postID, int anvandarID)
        {
            ValvPostData ValvPostData = new ValvPostData();
            ValvPostDS ValvpostDS = ValvPostData.HämtaValvPost(postID, anvandarID);
            ValvPost Valvpost = null;

            if (ValvpostDS.ValvPost.Count == 1)
            {
                //Skapa Användarobjektet
                Valvpost = new ValvPost();
                Valvpost.PostID = ValvpostDS.ValvPost[0].PostID;
                Valvpost.AnvandarID = ValvpostDS.ValvPost[0].AnvandarID;
                Valvpost.Konto = ValvpostDS.ValvPost[0].Konto;
                Valvpost.Inloggning = ValvpostDS.ValvPost[0].Inloggning;
                Valvpost.Losenord = ValvpostDS.ValvPost[0].Losenord;
                Valvpost.Postnamn = ValvpostDS.ValvPost[0].Postnamn;
                Valvpost.Webbadress = (ValvpostDS.ValvPost[0].IsWebbadressNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].Webbadress;
                Valvpost.AnvandarNamnSkapad = ValvpostDS.ValvPost[0].AnvandarNamnSkapad;
                Valvpost.SkapadDatum = ValvpostDS.ValvPost[0].SkapadDatum;
                Valvpost.AnvandarNamnUppdat = (ValvpostDS.ValvPost[0].IsAnvandarNamnUppdatNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].AnvandarNamnUppdat.ToString();
                Valvpost.UppdatDatum = (ValvpostDS.ValvPost[0].IsUppdatDatumNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].UppdatDatum.ToString();
            }
            return Valvpost;
        }
    }
}