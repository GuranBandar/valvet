using GemensamService;
using System;
using System.Collections.Generic;
using System.Data;
using Valvetwebb.Aktivitet;
using Valvetwebb.Kontroller;
using Valvetwebb.Objekt;

namespace Valvetwebb.Datalager
{
    public sealed class ValvPostData : AbstractDataLager
    {
        public DataSet HämtaAlla(string konto)
        {
            DataSet valvpostDS = new DataSet();

            try
            {
                valvpostDS.EnforceConstraints = false;
                string sql = "SELECT * FROM ValvPost WHERE Konto = @Konto ORDER BY Postnamn";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@Konto", DataTyp.Int, konto.ToString())
                };
                valvpostDS = DatabasAccess.FyllDataSet(sql, dbParameters);
                valvpostDS.Tables[0].TableName = "ValvPost";
            }
            catch (ValvetException hex)
            {
                throw hex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
            return valvpostDS;
        }

        /// <summary>
        /// Hämta valvpost
        /// </summary>
        /// <param name="PostID">Aktuellt valvpostID</param>
        /// <returns>Valvpost DS</returns>
        public ValvPostDS HämtaValvPost(int postID, int anvandarID)
        {
            ValvPostDS valvPostDS = new ValvPostDS();

            try
            {
                valvPostDS.EnforceConstraints = false;
                string sql = "SELECT a.* FROM ValvPost a WHERE PostID = @PostID AND AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@PostID", DataTyp.Int, postID.ToString()),
                    new DatabasParameters("@AnvandarID", DataTyp.Int, anvandarID.ToString())
                };
                DatabasAccess.FyllEnkeltDataSet(sql, dbParameters, valvPostDS);
            }
            catch (ValvetException hex)
            {
                throw hex;
            }
            finally
            {
                DatabasAccess.Dispose();
            }
            return valvPostDS;
        }

        /// <summary>
        /// Hämtar max postid från tabellen Valvpost i aktuell databas med SQL.
        /// </summary>
        /// <returns>Typat dataset med efterfrågat data</returns>
        public string HämtaMaxPostID()
        {
            DataSet valvpostDS = new DataSet();
            string nyttPostID = string.Empty;
            string sql;

            try
            {
                sql = "SELECT v.PostID FROM ValvPost v " +
                    " ORDER BY v.PostID DESC";
                valvpostDS = DatabasAccess.RunSql(sql);
            }
            catch (ValvetException hex)
            {
                throw hex;
            }
            finally
            {
                if (DatabasAccess != null)
                {
                    DatabasAccess.Dispose();
                }
            }
            nyttPostID = valvpostDS.Tables[0].Rows[0]["PostID"].ToString();
            return nyttPostID;
        }

        /// <summary>
        /// Hämtar rad/-er från tabellen Golfklubb i aktuell databas med angiven nyckel.
        /// </summary>
        /// <param name="sqlSok">Eventuellt where-villkor</param>
        /// <returns>Otypat dataset med efterfrågat data</returns>
        public DataSet SökValvPost(string konto, string sqlSok)
        {
            DataSet ValvPostDS = new DataSet();
            List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@Konto", DataTyp.String, konto.ToString())
                };

            ValvPostDS.EnforceConstraints = false;
            string sql = "SELECT * " +
                "FROM ValvPost " +
                sqlSok.ToString() +
                " ORDER BY Postnamn";
            try
            {
                ValvPostDS = DatabasAccess.FyllDataSet(sql, dbParameters);
                ValvPostDS.Tables[0].TableName = "ValvPost";
                return ValvPostDS;
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
        /// Ta bort ValvPost.
        /// </summary>
        /// <param name="Valvpost">Valvpost</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void TaborValvPost(ValvPost ValvPost, ref string felID, ref string feltext)
        {
            string sql;
            DatabasAccess.SkapaTransaktion();

            try
            {
                sql = "DELETE FROM ValvPost WHERE PostID = @PostID AND AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@PostID", DataTyp.Int, ValvPost.PostID.ToString()),
                    new DatabasParameters("@AnvandarID", DataTyp.Int, ValvPost.AnvandarID.ToString())
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
        /// Ny ValvPost.
        /// 
        /// Valvpost användarnamn uppdaterad finne inte, samma lika med uppdaterad datum
        /// 
        /// </summary>
        /// <param name="Valvpost">Valvpost</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void SparaNyValvPost(ValvPost ValvPost, ref string felID, ref string feltext)
        {
            string sql;
            int nyttPostID;

            try
            {
                DatabasAccess.SkapaTransaktion();
                sql = "INSERT INTO ValvPost (AnvandarID, Konto, Usernamn, Losenord, Postnamn, " +
                    "Webbadress, Anteckningar, AnvandarNamnSkapad, SkapadDatum) " + 
                    "Values " +
                    "(@AnvandarID, @Konto, @Usernamn, @Losenord, @Postnamn, " +
                    "@Webbadress, @Anteckningar, @AnvandarNamnSkapad, @SkapadDatum)";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@AnvandarID", DataTyp.Int, ValvPost.AnvandarID.ToString()),
                    new DatabasParameters("@Konto", DataTyp.VarChar, ValvPost.Konto.ToString()),
                    new DatabasParameters("@Usernamn", DataTyp.VarChar, ValvPost.Usernamn.ToString()),
                    new DatabasParameters("@Losenord", DataTyp.VarChar, ValvPost.Losenord.ToString()),
                    new DatabasParameters("@Postnamn", DataTyp.VarChar, ValvPost.Postnamn.ToString()),
                    new DatabasParameters("@Webbadress", DataTyp.VarChar, ValvPost.Webbadress.ToString()),
                    new DatabasParameters("@Anteckningar", DataTyp.VarChar, ValvPost.Anteckningar.ToString()),
                    new DatabasParameters("@AnvandarNamnSkapad", DataTyp.VarChar, ValvPost.AnvandarNamnSkapad.ToString()),
                    new DatabasParameters("@SkapadDatum", DataTyp.VarChar, ValvPost.SkapadDatum.ToString())
                };
                DatabasAccess.RunSql(sql, dbParameters);
                //sql = "SELECT LAST_INSERT_ID()";
                //nyttPostID = Convert.ToInt32(DatabasAccess.ExecuteScalar(sql));
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
            //return nyttPostID;
        }

        /// <summary>
        /// Sparar i ValvPost.
        /// 
        /// Valvpost användarnamn skapad finne redan, samma lika med skapad datum
        /// 
        /// </summary>
        /// <param name="ValvPost">ValvPost</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public void SparaValvPost(ValvPost ValvPost, ref string felID, ref string feltext)
        {
            string sql;
            DatabasAccess.SkapaTransaktion();

            try
            {
                sql = "UPDATE ValvPost " +
                    "SET AnvandarID = @AnvandarID, Konto = @Konto, Usernamn = @Usernamn, Losenord = @Losenord, Postnamn = @Postnamn, " +
                    "Webbadress = @Webbadress, Anteckningar = @Anteckningar, " +
                    "AnvandarNamnUppdat = @AnvandarNamnUppdat, UppdatDatum = @UppdatDatum " +
                    "WHERE PostID = @PostID AND AnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@PostID", DataTyp.Int, ValvPost.PostID.ToString()),
                    new DatabasParameters("@AnvandarID", DataTyp.Int, ValvPost.AnvandarID.ToString()),
                    new DatabasParameters("@Konto", DataTyp.VarChar, ValvPost.Konto.ToString()),
                    new DatabasParameters("@Usernamn", DataTyp.VarChar, ValvPost.Usernamn.ToString()),
                    new DatabasParameters("@Losenord", DataTyp.VarChar, ValvPost.Losenord.ToString()),
                    new DatabasParameters("@Postnamn", DataTyp.VarChar, ValvPost.Postnamn.ToString()),
                    new DatabasParameters("@Webbadress", DataTyp.VarChar, ValvPost.Webbadress.ToString()),
                    new DatabasParameters("@Anteckningar", DataTyp.VarChar, ValvPost.Anteckningar.ToString()),
                    new DatabasParameters("@AnvandarNamnUppdat", DataTyp.VarChar, ValvPost.AnvandarNamnUppdat.ToString()),
                    new DatabasParameters("@UppdatDatum", DataTyp.VarChar, ValvPost.UppdatDatum.ToString())
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