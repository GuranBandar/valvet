using System;
using System.Collections.Generic;
using System.Data;
using Valvetwebb.Datalager;
using Valvetwebb.Kontroller;
using Valvetwebb.Objekt;

namespace Valvetwebb.Aktivitet
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
            try
            {
                anvandareData = new AnvandareData();
                anvandareDS = anvandareData.LoggaIn(anvandarnamn);
                if (anvandareDS.Anvandare.Count == 1 &&
                    anvandareDS.Anvandare[0].Losenord.Equals(losenord))
                {
                    //Inloggningen lyckades, skapa nu användarobjektet
                    anvandare = new Anvandare();
                    anvandare.AnvandarID = anvandareDS.Anvandare[0].AnvandarID;
                    anvandare.Anvandarnamn = anvandareDS.Anvandare[0].Anvandarnamn;
                    anvandare.Epostadress = (anvandareDS.Anvandare[0].IsEpostadressNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].Epostadress;
                    anvandare.Losenord = anvandareDS.Anvandare[0].Losenord;
                    anvandare.SenastInloggadDatum = (anvandareDS.Anvandare[0].IsSenastInloggadDatumNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].SenastByttLosenordDatum.ToString();
                    anvandare.SenastByttLosenordDatum = (anvandareDS.Anvandare[0].IsSenastByttLosenordDatumNull()) ?
                        string.Empty : anvandareDS.Anvandare[0].SenastByttLosenordDatum.ToString();
                    anvandare.Aktiv = anvandareDS.Anvandare[0].Aktiv.ToString();
                    anvandare.Konto = anvandareDS.Anvandare[0].Konto.ToString();
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
                anvandare.SenastInloggadDatum = (anvandareDS.Anvandare[0].IsSenastInloggadDatumNull()) ?
                    string.Empty : anvandareDS.Anvandare[0].SenastByttLosenordDatum.ToString();
                anvandare.SenastByttLosenordDatum = (anvandareDS.Anvandare[0].IsSenastByttLosenordDatumNull()) ?
                    string.Empty : anvandareDS.Anvandare[0].SenastByttLosenordDatum.ToString();
                anvandare.Epostadress = (anvandareDS.Anvandare[0].IsEpostadressNull())
                    ? string.Empty : anvandareDS.Anvandare[0].Epostadress;
                anvandare.Aktiv = anvandareDS.Anvandare[0].Aktiv.ToString();
            }
            return anvandare;
        }

        /// <summary>
        /// Söker rad/-er från tabellen Anvandare i aktuell databas med angivet sökvillkor.
        /// </summary>
        /// <param name="namn">Aktuell namn</param>
        /// <param name="anvandarGrupp">Ev anvädargrupp i sökningen</param>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public List<Anvandare> SökAnvandare(string anvandarnamn)
        {
            DataSet anvandareDS = new DataSet();
            AnvandareData AnvandareData = new AnvandareData();
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (anvandarnamn.ToString() != "")
                {
                    WhereMedLikeEfter(anvandarnamn, "a.Anvandarnamn", ref sqlSok, ref antArgument);
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
                        SenastInloggadDatum = rad["SenastInloggadDatum"].ToString(),
                        SenastByttLosenordDatum = rad["SenastByttLosenordDatum"].ToString(),
                        Epostadress = rad["Epostadress"].ToString(),
                        Aktiv = rad["Aktiv"].ToString()
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
        /// <param name="anvandare">Objekt Anvandare som ska kollas</param>
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
