using System;
using System.Collections.Generic;
using System.Data;
using Valvetwebb.Datalager;
using Valvetwebb.Kontroller;
using Valvetwebb.Objekt;

namespace Valvetwebb.Aktivitet
{
    /// <summary>
    /// Klass för ValvPost aktivitet
    /// 
    /// Innehåller alla metoder för klassen ValvPosts verksamhetslogik.
    /// </summary>
    public sealed class ValvPostAktivitet : SökVillkor
    {
        /// <summary>
        /// Hämta alla Valvposter från tabellen Valvpost i aktuell databas för inloggad användare
        /// </summary>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public List<ValvPost> HämtaAlla(string konto)
        {
            DataSet valvPostDS = new DataSet();
            ValvPostData ValvPostData = new ValvPostData();

            try
            {
                valvPostDS = ValvPostData.HämtaAlla(konto);
                List<ValvPost> ValvPost = new List<ValvPost>(valvPostDS.Tables["ValvPost"].Rows.Count);
                foreach (DataRow rad in valvPostDS.Tables["ValvPost"].Rows)
                {
                    ValvPost.Add(new ValvPost()
                    {
                        PostID = (int)rad["PostID"],
                        AnvandarID = (int)rad["AnvandarID"],
                        Konto = rad["Konto"].ToString(),
                        Usernamn = rad["Usernamn"].ToString(),
                        Losenord = rad["Losenord"].ToString(),
                        Postnamn = rad["Postnamn"].ToString(),
                        Webbadress = rad["Webbadress"].ToString(),
                        Anteckningar = rad["Anteckningar"].ToString(),
                        AnvandarNamnSkapad = rad["AnvandarNamnSkapad"].ToString(),
                        SkapadDatum = rad["SkapadDatum"].ToString(),
                        AnvandarNamnUppdat = rad["AnvandarNamnUppdat"].ToString(),
                        UppdatDatum = rad["UppdatDatum"].ToString()
                    }); 
                }
                return ValvPost;
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
            ValvPost ValvPost = null;

            if (ValvpostDS.ValvPost.Count == 1)
            {
                //Skapa Användarobjektet
                ValvPost = new ValvPost();
                ValvPost.PostID = ValvpostDS.ValvPost[0].PostID;
                ValvPost.AnvandarID = ValvpostDS.ValvPost[0].AnvandarID;
                ValvPost.Konto = ValvpostDS.ValvPost[0].Konto;
                ValvPost.Usernamn = ValvpostDS.ValvPost[0].Usernamn;
                ValvPost.Losenord = ValvpostDS.ValvPost[0].Losenord;
                ValvPost.Postnamn = ValvpostDS.ValvPost[0].Postnamn;
                ValvPost.Webbadress = (ValvpostDS.ValvPost[0].IsWebbadressNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].Webbadress;
                ValvPost.Anteckningar = (ValvpostDS.ValvPost[0].IsAnteckningarNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].Anteckningar;
                ValvPost.AnvandarNamnSkapad = ValvpostDS.ValvPost[0].AnvandarNamnSkapad;
                ValvPost.SkapadDatum = ValvpostDS.ValvPost[0].SkapadDatum;
                ValvPost.AnvandarNamnUppdat = (ValvpostDS.ValvPost[0].IsAnvandarNamnUppdatNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].AnvandarNamnUppdat.ToString();
                ValvPost.UppdatDatum = (ValvpostDS.ValvPost[0].IsUppdatDatumNull()) ?
                    string.Empty : ValvpostDS.ValvPost[0].UppdatDatum.ToString();
            }
            return ValvPost;
        }

        /// <summary>
        /// Söker rad/-er från tabellen ValvPost i aktuell databas med angivet sökvillkor.
        /// </summary>
        /// <param name="namn">Aktuell namn</param>
        /// <returns>Golfklubbobjekt med efterfrågat data</returns>
        public List<ValvPost> SökValvPost(string konto, string namn)
        {
            DataSet valvPostDS = new DataSet();
            ValvPostData valvPostData = new ValvPostData();
            //List<ValvPost> ValvPost = null;
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";

            try
            {
                if (!string.IsNullOrEmpty(namn))
                {
                    WhereMedLikeEfter(namn, "Postnamn", ref sqlSok, ref antArgument);
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE Konto = @Konto AND " + sqlSok;
                }
                else
                {
                    sql = sql + " WHERE Konto = @Konto ";
                }

                valvPostDS = valvPostData.SökValvPost(konto, sql);
                List<ValvPost> valvPost = new List<ValvPost>(valvPostDS.Tables["ValvPost"].Rows.Count);

                foreach (DataRow rad in valvPostDS.Tables["ValvPost"].Rows)
                {
                    valvPost.Add(new ValvPost()
                    {
                        PostID = (int)rad["PostID"],
                        AnvandarID = (int)rad["AnvandarID"],
                        Konto = rad["Konto"].ToString(),
                        Usernamn = rad["Usernamn"].ToString(),
                        Losenord = rad["Losenord"].ToString(),
                        Postnamn = rad["Postnamn"].ToString(),
                        Webbadress = rad["Webbadress"].ToString(),
                        Anteckningar = rad["Anteckningar"].ToString(),
                        AnvandarNamnSkapad = rad["AnvandarNamnSkapad"].ToString(),
                        SkapadDatum = rad["SkapadDatum"].ToString(),
                        AnvandarNamnUppdat = rad["AnvandarNamnUppdat"].ToString(),
                        UppdatDatum = rad["UppdatDatum"].ToString()
                    });
                }
                return valvPost;
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Sparar alla förändringar i ValvPost i databasen 
        /// </summary>
        /// <param name="valvPost">Aktuell bokning</param>
        /// <param name="nyBokning">Ny Anvandare, true or false</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public int Spara(ValvPost valvPost, bool nyPost, ref string felID, ref string feltext)
        {
            int nyttPostID = 0;
            bool kollaOK = true;

            if (kollaOK)
            {
                ValvPostData valvPostData = new ValvPostData();
                if (nyPost)
                {
                    valvPostData.SparaNyValvPost(valvPost, ref felID, ref feltext);
                    nyttPostID = Convert.ToInt32(valvPostData.HämtaMaxPostID());
                    valvPost.PostID = nyttPostID;
                }
                else
                {
                    valvPostData.SparaValvPost(valvPost, ref felID, ref feltext);
                }
            }
            else
            {
                throw new ValvetException();
            }

            return nyttPostID;
        }

        /// <summary>
        /// Ta bort BokningDag i databasen 
        /// </summary>
        /// <param name="BokningDag">Aktuell bokning</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void TaBortValvPost(ValvPost valvPost, ref string felID, ref string feltext)
        {
            ValvPostData valvPostData = new ValvPostData();
            valvPostData.TaborValvPost(valvPost, ref felID, ref feltext);
        }
    }
}