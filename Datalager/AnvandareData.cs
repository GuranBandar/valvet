using GemensamService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Valvet
{
    /// <summary>
    /// Datalagerklass för Användare
    /// </summary>
    public sealed class AnvandareData : AbstractDataLager
    {
        /// <summary>
        /// Hämtar rad från tabellen Anvandare i aktuell databas med angiven nyckel.
        /// </summary>
        /// <param name="anvandarnamn">Aktuellt anvandarnamn</param>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public AnvandareDS LoggaIn(string anvandarnamn)
        {
            AnvandareDS anvandareDS = new AnvandareDS();

            try
            {
                anvandareDS.EnforceConstraints = false;
                string sql = "SELECT a.* FROM Anvandare a WHERE a.Anvandarnamn = @Anvandarnamn";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@Anvandarnamn", anvandarnamn)
                };
                DatabasAccess.FyllEnkeltDataSet(sql, dbParameters, anvandareDS);
                return anvandareDS;
            }
            catch (SqlException sex)
            {
                throw sex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
        }

        /// <summary>
        /// Hämta användare
        /// </summary>
        /// <param name="anvandarID">Aktuellt anvandarID</param>
        /// <returns>Anvandare DS</returns>
        public AnvandareDS HämtaAnvandare(int anvandarID)
        {
            AnvandareDS anvandareDS = new AnvandareDS();

            try
            {
                anvandareDS.EnforceConstraints = false;
                string sql = "SELECT a.* FROM Anvandare a WHERE AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@AnvandarID", DataTyp.Int, anvandarID.ToString())
                };
                DatabasAccess.FyllEnkeltDataSet(sql, dbParameters, anvandareDS);
            }
            catch (HookerException hex)
            {
                throw hex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
            return anvandareDS;
        }

        /// <summary>
        /// Datum för senaste inloggning uppdateras
        /// </summary>
        /// <param name="anvandarID">Aktuellt användarID</param>
        /// <param name="inloggadDatum">Inloggningsdatum</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void InloggningOK(int anvandarID, string inloggadDatum, ref string felID, ref string feltext)
        {
            string sql;
            DatabasAccess.SkapaTransaktion();

            try
            {
                sql = "UPDATE Anvandare SET SenastInloggadDatum = @SenastInloggadDatum " +
                    "WHERE AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@AnvandarID", DataTyp.Int, anvandarID.ToString()),
                    new DatabasParameters("@SenastInloggadDatum", DataTyp.VarChar, inloggadDatum.ToString()),
                };
                DatabasAccess.RunSql(sql, dbParameters);
                DatabasAccess.BekräftaTransaktion();
            }
            catch (ValvetException hex)
            {
                felID = "SQLERROR";
                feltext = hex.Message.ToString();
                DatabasAccess.ÅngraTransaktion();
                throw hex;
            }
            catch (Exception ex)
            {
                DatabasAccess.ÅngraTransaktion();
                throw ex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
        }

        /// <summary>
        /// Hämtar rad/-er från tabellen Bana i aktuell databas med angiven nyckel.
        /// </summary>
        /// <param name="sqlSok">Eventuellt where-villkor</param>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public DataSet SökAnvandare(string sqlSok)
        {
            DataSet anvandareDS = new DataSet();
            string sql;

            try
            {
                sql = "SELECT a.*, s.Namn AS SpelareNamn " +
                        "FROM Anvandare a " +
                        "LEFT OUTER JOIN Spelare s ON s.SpelarId = a.SpelarID" +
                    sqlSok.ToString() +
                    " ORDER BY a.Anvandarnamn";
                anvandareDS = DatabasAccess.RunSql(sql);
                anvandareDS.Tables[0].TableName = "Anvandare";
                return anvandareDS;
            }
            catch (ValvetException hex)
            {
                throw hex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
        }

        /// <summary>
        /// Ta bort Användare.
        /// </summary>
        /// <param name="anvandare">Anvandare</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void TabortAnvandare(Anvandare anvandare, ref string felID, ref string feltext)
        {
            string sql;
            DatabasAccess.SkapaTransaktion();

            try
            {
                sql = "DELETE FROM Anvandare WHERE AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@AnvandarID", DataTyp.Int, anvandare.AnvandarID.ToString())
                };
                DatabasAccess.RunSql(sql, dbParameters);
                DatabasAccess.BekräftaTransaktion();
            }
            catch (ValvetException hex)
            {
                felID = "SQLERROR";
                feltext = hex.Message.ToString();
                DatabasAccess.ÅngraTransaktion();
                throw hex;
            }
            catch (Exception ex)
            {
                DatabasAccess.ÅngraTransaktion();
                throw ex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
        }

        /// <summary>
        /// Ny Användare.
        /// </summary>
        /// <param name="anvandare">Anvandare</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public int SparaNyAnvandare(Anvandare anvandare, ref string felID, ref string feltext)
        {
            string sql;
            int nyttAnvandarID;

            try
            {
                DatabasAccess.SkapaTransaktion();
                sql = "INSERT INTO Anvandare(Anvandarnamn, Losenord, SpelarID, " +
                    "SenastInloggadDatum, " +
                    "SenastByttLosenordDatum, Anvandargrupp, Epostadress, GIR, WebBrowser, " +
                    "Sprakkod, Epostmeddelande)" +
                    "VALUES " +
                    "(@Anvandarnamn, @Losenord, @SpelarID, @SenastInloggadDatum, " +
                    "@SenastByttLosenordDatum, @Anvandargrupp, @Epostadress, @GIR, @WebBrowser, " +
                    "@Sprakkod, @Epostmeddelande)";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@Anvandarnamn", DataTyp.VarChar, anvandare.Anvandarnamn.ToString()),
                    new DatabasParameters("@Losenord", DataTyp.VarChar, anvandare.Losenord.ToString()),
                    new DatabasParameters("@SpelarID", DataTyp.Int, anvandare.SpelarID.ToString()),
                    new DatabasParameters("@SenastInloggadDatum", DataTyp.VarChar, anvandare.SenastByttLosenordDatum.ToString()),
                    new DatabasParameters("@SenastByttLosenordDatum", DataTyp.VarChar, anvandare.SenastByttLosenordDatum.ToString()),
                    new DatabasParameters("@Anvandargrupp", DataTyp.Char, anvandare.Anvandargrupp.ToString()),
                    new DatabasParameters("@Epostadress", DataTyp.VarChar, anvandare.Epostadress.ToString()),
                    new DatabasParameters("@GIR", DataTyp.Char, anvandare.GIR.ToString()),
                    new DatabasParameters("@WebBrowser", DataTyp.VarChar, anvandare.WebBrowser.ToString()),
                    new DatabasParameters("@Sprakkod", DataTyp.Char, anvandare.Sprakkod.ToString()),
                    new DatabasParameters("@Epostmeddelande", DataTyp.Char, anvandare.Epostmeddelande.ToString())
                };
                DatabasAccess.RunSql(sql, dbParameters);
                sql = "SELECT LAST_INSERT_ID()";
                nyttAnvandarID = Convert.ToInt32(DatabasAccess.ExecuteScalar(sql));
                DatabasAccess.BekräftaTransaktion();
            }
            catch (ValvetException hex)
            {
                felID = "SQLERROR";
                feltext = hex.Message.ToString();
                DatabasAccess.ÅngraTransaktion();
                throw hex;
            }
            catch (Exception ex)
            {
                DatabasAccess.ÅngraTransaktion();
                throw ex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
            return nyttAnvandarID;
        }

        /// <summary>
        /// Sparar i Anvandare.
        /// </summary>
        /// <param name="anvandare">Anvandare</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void SparaAnvandare(Anvandare anvandare, ref string felID, ref string feltext)
        {
            string sql;
            DatabasAccess.SkapaTransaktion();

            try
            {
                sql = "UPDATE Anvandare " +
                    "SET Anvandarnamn = @Anvandarnamn, Losenord = @Losenord, " +
                    "SpelarID = @SpelarID, SenastInloggadDatum = @SenastInloggadDatum, " +
                    "SenastByttLosenordDatum = @SenastByttLosenordDatum, " +
                    "Anvandargrupp = @Anvandargrupp, Epostadress = @Epostadress, GIR = @GIR, WebBrowser = @WebBrowser, " +
                    "Sprakkod = @Sprakkod, Epostmeddelande = @Epostmeddelande " +
                    "WHERE AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@AnvandarID", DataTyp.Int, anvandare.AnvandarID.ToString()),
                    new DatabasParameters("@Anvandarnamn", DataTyp.VarChar, anvandare.Anvandarnamn.ToString()),
                    new DatabasParameters("@Losenord", DataTyp.VarChar, anvandare.Losenord.ToString()),
                    new DatabasParameters("@SpelarID", DataTyp.Int, anvandare.SpelarID.ToString()),
                    new DatabasParameters("@SenastInloggadDatum", DataTyp.VarChar, anvandare.SenastInloggadDatum.ToString()),
                    new DatabasParameters("@SenastByttLosenordDatum", DataTyp.VarChar, anvandare.SenastByttLosenordDatum.ToString()),
                    new DatabasParameters("@Anvandargrupp", DataTyp.Char, anvandare.Anvandargrupp.ToString()),
                    new DatabasParameters("@Epostadress", DataTyp.VarChar, anvandare.Epostadress.ToString()),
                    new DatabasParameters("@GIR", DataTyp.Char, anvandare.GIR.ToString()),
                    new DatabasParameters("@WebBrowser", DataTyp.VarChar, anvandare.WebBrowser.ToString()),
                    new DatabasParameters("@Sprakkod", DataTyp.Char, anvandare.Sprakkod.ToString()),
                    new DatabasParameters("@Epostmeddelande", DataTyp.Char, anvandare.Epostmeddelande.ToString())
                };
                DatabasAccess.RunSql(sql, dbParameters);
                DatabasAccess.BekräftaTransaktion();
            }
            catch (ValvetException hex)
            {
                felID = "SQLERROR";
                feltext = hex.Message.ToString();
                DatabasAccess.ÅngraTransaktion();
                throw hex;
            }
            catch (Exception ex)
            {
                DatabasAccess.ÅngraTransaktion();
                throw ex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
        }
    }
}
