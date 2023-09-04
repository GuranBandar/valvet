using System;
using System.Collections.Generic;
using System.Data;

namespace Valvet
{
    /// <summary>
    /// Klass för Användare
    /// 
    /// Innehåller alla metoder för klassen Användares verksamhetslogik.
    /// </summary>
    public sealed class AnvandareAktivitet : SökVillkor
    {
        //private DateTime DatumMinValue = new DateTime(1900,01,01,12,0,0);

        // SELECT CAST('12:00:00' AS datetime)

        /// <summary>
        /// Metoden anropas vid inloggning
        /// </summary>
        /// <param name="anvandarnamn">Användarnamn som ska kontrolleras</param>
        /// <param name="losenord">Lösenord som ska kontrolleras</param>
        /// <returns>Anvandarens ID vid en lyckad inloggning</returns>
        public Anvandare LoggaIn(string anvandarnamn, string losenord)
        {
            //kolla i databasen
            AnvandareData anvandareData;
            AnvandareDS anvandareDS = new AnvandareDS();
            Anvandare anvandare = null;
            Spelare spelare = null;
            try
            {
                anvandareData = new AnvandareData();
                anvandareDS = anvandareData.LoggaIn(anvandarnamn);
                if (anvandareDS.Anvandare.Count == 1 &&
                    anvandareDS.Anvandare[0].Losenord.Equals(losenord))
                {
                    //Inloggningen lyckades, skapa nu användarobjektet
                    anvandare = new Anvandare();
                    spelare = new Spelare();
                    anvandare.AnvandarID = anvandareDS.Anvandare[0].AnvandarID;
                    anvandare.Anvandarnamn = anvandareDS.Anvandare[0].Anvandarnamn;
                    anvandare.Anvandargrupp = anvandareDS.Anvandare[0].Anvandargrupp;
                    anvandare.Epostadress = (anvandareDS.Anvandare[0].IsEpostadressNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].Epostadress;
                    anvandare.Losenord = anvandareDS.Anvandare[0].Losenord;
                    anvandare.SenastInloggadDatum = anvandareDS.Anvandare[0].SenastByttLosenordDatum;
                    anvandare.SenastByttLosenordDatum = anvandareDS.Anvandare[0].SenastByttLosenordDatum;
                    anvandare.GIR = (anvandareDS.Anvandare[0].IsGIRNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].GIR;
                    anvandare.WebBrowser = (anvandareDS.Anvandare[0].IsWebBrowserNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].WebBrowser;
                    anvandare.Sprakkod = (anvandareDS.Anvandare[0].IsSprakkodNull()) ?
                        "SE" : anvandareDS.Anvandare[0].Sprakkod;
                    anvandare.Epostmeddelande = (anvandareDS.Anvandare[0].IsEpostmeddelandeNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].Epostmeddelande;
                    anvandare.SpelarID = (anvandareDS.Anvandare[0].IsSpelarIDNull()) ?
                        0 : anvandareDS.Anvandare[0].SpelarID;

                    //if (spelare.AktuelltSpelarID != 0)
                    //{ 
                    ////och läs nu aktuell Anvandare för att komplettera användarobjektet
                    //SpelareAktivitet spelareAktivitet = new SpelareAktivitet();
                    //spelare = spelareAktivitet.HämtaSpelare(spelare.AktuelltSpelarID);
                    //if (spelare != null)
                    //{
                    //    anvandare.AktuelltSpelarID = spelare.AktuelltSpelarID;
                    //    anvandare.Namn = spelare.Namn;
                    //    anvandare.ExaktHcp = spelare.ExaktHcp;
                    //    anvandare.Klass = spelare.Klass;
                    //    anvandare.Kön = spelare.Kön;
                    //    anvandare.Revisionsdatum = spelare.Revisionsdatum;
                    //    anvandare.HemmabanaNr = spelare.HemmabanaNr;
                    //    anvandare.GolfID = spelare.GolfID;
                    //    anvandare.UppdatDatum = spelare.UppdatDatum;
                    //    if (spelare.GolfklubbNr != 0)
                    //    {
                    //        anvandare.GolfklubbNr = spelare.GolfklubbNr;
                    //    }
                    //}
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return anvandare;
        }

        /// <summary>
        /// Hämtar rad från tabellen Användare i aktuell databas med angiven nyckel.
        /// </summary>
        /// <param name="anvandarID">Aktuell användarID</param>
        /// <returns>Objekt med efterfrågat data</returns>
        public Anvandare HämtaAnvandare(int anvandarID)
        {
            AnvandareData anvandareData = new AnvandareData();
            AnvandareDS anvandareDS = anvandareData.HämtaAnvandare(anvandarID);
            Anvandare anvandare = null;

            if (anvandareDS.Anvandare.Count == 1)
            {
                //Skapa Användarobjektet
                anvandare = new Anvandare();
                anvandare.Anvandarnamn = anvandareDS.Anvandare[0].Anvandarnamn;
                anvandare.AnvandarID = anvandareDS.Anvandare[0].AnvandarID;
                anvandare.Losenord = anvandareDS.Anvandare[0].Losenord;
                anvandare.SpelarID = anvandareDS.Anvandare[0].SpelarID;
                anvandare.SenastInloggadDatum = anvandareDS.Anvandare[0].SenastInloggadDatum;
                anvandare.SenastByttLosenordDatum = anvandareDS.Anvandare[0].SenastByttLosenordDatum;
                anvandare.Anvandargrupp = (anvandareDS.Anvandare[0].IsAnvandargruppNull())
                    ? string.Empty : anvandareDS.Anvandare[0].Anvandargrupp;
                anvandare.Epostadress = (anvandareDS.Anvandare[0].IsEpostadressNull())
                    ? string.Empty : anvandareDS.Anvandare[0].Epostadress;
                anvandare.GIR = (anvandareDS.Anvandare[0].IsGIRNull())
                    ? string.Empty : anvandareDS.Anvandare[0].GIR;
                anvandare.WebBrowser = (anvandareDS.Anvandare[0].IsWebBrowserNull())
                    ? string.Empty : anvandareDS.Anvandare[0].WebBrowser;
                anvandare.Sprakkod = anvandareDS.Anvandare[0].Sprakkod;
                anvandare.Epostmeddelande = (anvandareDS.Anvandare[0].IsEpostmeddelandeNull())
                    ? string.Empty : anvandareDS.Anvandare[0].Epostmeddelande;
            }
            return anvandare;
        }

        /// <summary>
        /// Söker rad/-er från tabellen Anvandare i aktuell databas med angivet sökvillkor.
        /// </summary>
        /// <param name="namn">Aktuell namn</param>
        /// <param name="anvandarGrupp">Ev anvädargrupp i sökningen</param>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public List<Anvandare> SökAnvandare(string namn, string anvandarGrupp)
        {
            DataSet anvandareDS = new DataSet();
            AnvandareData AnvandareData = new AnvandareData();
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (namn.ToString() != "")
                {
                    WhereMedLikeEfter(namn, "a.Anvandarnamn", ref sqlSok, ref antArgument);
                }

                if (anvandarGrupp.ToString() != "")
                {
                    WhereMedLikeEfter(anvandarGrupp, "a.Anvandargrupp", ref sqlSok, ref antArgument);
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE " + sqlSok;
                }

                anvandareDS = AnvandareData.SökAnvandare(sql);
                List<Anvandare> Anvandare = new List<Anvandare>(anvandareDS.Tables["Anvandare"].Rows.Count);
                foreach (DataRow rad in anvandareDS.Tables["Anvandare"].Rows)
                {
                    Anvandare.Add(new Anvandare()
                    {
                        AnvandarID = (int)rad["AnvandarID"],
                        Anvandarnamn = rad["AnvandarNamn"].ToString(),
                        Losenord = rad["Losenord"].ToString(),
                        SpelarID = (int)rad["SpelarID"],
                        SpelarNamn = rad["SpelareNamn"].ToString(),
                        SenastInloggadDatum = rad["SenastInloggadDatum"].ToString(),
                        SenastByttLosenordDatum = rad["SenastByttLosenordDatum"].ToString(),
                        Anvandargrupp = rad["Anvandargrupp"].ToString(),
                        Epostadress = rad["Epostadress"].ToString(),
                        GIR = rad["GIR"].ToString(),
                        WebBrowser = rad["WebBrowser"].ToString(),
                        Sprakkod = rad["Sprakkod"].ToString(),
                        Epostmeddelande = rad["Epostmeddelande"].ToString()
                    });
                }
                return Anvandare;
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Söker rad/-er från tabellen Anvandare i aktuell databas med angivet sökvillkor.
        /// </summary>
        /// <param name="namn">Aktuell namn</param>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public List<Anvandare> SökSpelareIAnvandare(string namn)
        {
            DataSet anvandareDS = new DataSet();
            AnvandareData AnvandareData = new AnvandareData();
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (namn.ToString() != "")
                {
                    WhereMedLikeEfter(namn, "s.Namn", ref sqlSok, ref antArgument);
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE " + sqlSok;
                }

                anvandareDS = AnvandareData.SökAnvandare(sql);
                List<Anvandare> Anvandare = new List<Anvandare>(anvandareDS.Tables["Anvandare"].Rows.Count);
                foreach (DataRow rad in anvandareDS.Tables["Anvandare"].Rows)
                {
                    Anvandare.Add(new Anvandare()
                    {
                        AnvandarID = (int)rad["AnvandarID"],
                        Anvandarnamn = rad["AnvandarNamn"].ToString(),
                        Losenord = rad["Losenord"].ToString(),
                        SpelarID = (int)rad["SpelarID"],
                        SpelarNamn = rad["SpelareNamn"].ToString(),
                        SenastInloggadDatum = rad["SenastInloggadDatum"].ToString(),
                        SenastByttLosenordDatum = rad["SenastByttLosenordDatum"].ToString(),
                        Anvandargrupp = rad["Anvandargrupp"].ToString(),
                        Epostadress = rad["Epostadress"].ToString(),
                        GIR = rad["GIR"].ToString(),
                        WebBrowser = rad["WebBrowser"].ToString(),
                        Sprakkod = rad["Sprakkod"].ToString(),
                        Epostmeddelande = rad["Epostmeddelande"].ToString()
                    });
                }
                return Anvandare;
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Sparar alla förändringar i Anvandare i databasen 
        /// </summary>
        /// <param name="anvandare">Aktuell användare</param>
        /// <param name="nyAnvandare">Ny Anvandare, true or false</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public int Spara(Anvandare anvandare, bool nyAnvandare, ref string felID, ref string feltext)
        {
            int nyttAnvandarID = 0;
            bool kollaOK = Kolla(anvandare, ref felID, ref feltext);

            if (kollaOK)
            {
                AnvandareData anvandarData = new AnvandareData();
                if (nyAnvandare)
                {
                    nyttAnvandarID = anvandarData.SparaNyAnvandare(anvandare, ref felID, ref feltext);
                }
                else
                {
                    anvandarData.SparaAnvandare(anvandare, ref felID, ref feltext);
                }
            }
            else
            {
                throw new ValvetException();
            }

            return nyttAnvandarID;
        }

        /// <summary>
        ///     Metoden kollar informationen innan uppdatering ska göras
        /// </summary>
        /// <param name="Golfklubb">Golfklubbobjekt med informationen som ska kollas</param>
        /// <param name="felID">Ev felID som returneras</param>
        /// <param name="felmeddelande">Ev felmeddelande som returneras</param>
        private bool Kolla(Anvandare anvandare, ref string felID, ref string felmeddelande)
        {
            if (string.IsNullOrEmpty(anvandare.Anvandarnamn))
            {
                felID = "ANVANDAREMISSING";
                felmeddelande = "";
                return false;
            }
            if (string.IsNullOrEmpty(anvandare.Losenord))
            {
                felID = "FELLOSEN";
                felmeddelande = "";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Ta bort Användare i databasen 
        /// </summary>
        /// <param name="Anvandare">Aktuell Anvandare</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void TaBort(Anvandare anvandare, ref string felID, ref string feltext)
        {
            AnvandareData anvandareData = new AnvandareData();
            anvandareData.TabortAnvandare(anvandare, ref felID, ref feltext);
        }

        /// <summary>
        /// Datum för senaste inloggning uppdateras
        /// </summary>
        /// <param name="inloggadDatum">Inloggningsdatum</param>
        public void InloggningOK(int anvandarID, string inloggadDatum, ref string felID, ref string feltext)
        {
            AnvandareData anvandareData = new AnvandareData();
            anvandareData.InloggningOK(anvandarID, inloggadDatum, ref felID, ref feltext);
        }
    }
}
