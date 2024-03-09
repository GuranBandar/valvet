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
        public DataSet HämtaAlla()
        {
            DataSet valvpostDS = new DataSet();

            try
            {
                valvpostDS.EnforceConstraints = false;
                string sql = "SELECT * FROM ValvPost ORDER BY Postnamn";
                valvpostDS = DatabasAccess.RunSql(sql);
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
        /// </summary>
        /// <param name="Valvpost">Valvpost</param>
        /// <param name="felID">Felmeddelande i Ordlistan som ska visas</param>
        /// <param name="feltext">Ev kompletterande felmeddelande som returneras</param>
        public int SparaNyValvPost(ValvPost ValvPost, ref string felID, ref string feltext)
        {
            string sql;
            int nyttPostID;

            try
            {
                DatabasAccess.SkapaTransaktion();
                sql = "INSERT INTO ValvPost (AnvandarID, Konto, Inloggning, Losenord, Postnamn, " +
                    "Webbadress, Anteckningar, AnvandarNamnSkapad, SkapadDatum, " +
                    "AnvandarNamnUppdat, UppdatDatum) " + 
                    "Values " +
                    "(@AnvandarID, @Konto, @Inloggning, @Losenord, @Postnamn, " +
                    "@Webbadress, @Anteckningar, @AnvandarNamnSkapad, @SkapadDatum, " +
                    "@AnvandarNamnUppdat, @UppdatDatum)";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@AnvandarID", DataTyp.Int, ValvPost.AnvandarID.ToString()),
                    new DatabasParameters("@Konto", DataTyp.VarChar, ValvPost.Konto.ToString()),
                    new DatabasParameters("@Inloggning", DataTyp.VarChar, ValvPost.Inloggning.ToString()),
                    new DatabasParameters("@Losenord", DataTyp.VarChar, ValvPost.Losenord.ToString()),
                    new DatabasParameters("@Postnamn", DataTyp.VarChar, ValvPost.Postnamn.ToString()),
                    new DatabasParameters("@Webbadress", DataTyp.VarChar, ValvPost.Webbadress.ToString()),
                    new DatabasParameters("@Anteckningar", DataTyp.VarChar, ValvPost.Anteckningar.ToString()),
                    new DatabasParameters("@AnvandarNamnSkapad", DataTyp.VarChar, ValvPost.AnvandarNamnSkapad.ToString()),
                    new DatabasParameters("@SkapadDatum", DataTyp.VarChar, ValvPost.SkapadDatum.ToString()),
                    new DatabasParameters("@AnvandarNamnUppdat", DataTyp.VarChar, ValvPost.AnvandarNamnUppdat.ToString()),
                    new DatabasParameters("@UppdatDatum", DataTyp.VarChar, ValvPost.UppdatDatum.ToString())
                };
                DatabasAccess.RunSql(sql, dbParameters);
                sql = "SELECT LAST_INSERT_ID()";
                nyttPostID = Convert.ToInt32(DatabasAccess.ExecuteScalar(sql));
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
            return nyttPostID;
        }

        /// <summary>
        /// Sparar i ValvPost.
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
                    "SET AnvandarID = @AnvandarID, Konto = @Konto, Inloggning = @Inloggning, Losenord = @Losenord, Postnamn = @Postnamn, " +
                    "Webbadress = @Webbadress, Anteckningar = @Anteckningar, nvandarNamnSkapad = @AnvandarNamnSkapad, " +
                    "SkapadDatum = @SkapadDatum, AnvandarNamnUppdat = @AnvandarNamnUppdat, UppdatDatum = @UppdatDatum) " +
                    "WHERE PostID = @PostID ANDAnvandarID = @AnvandarID";
                List<DatabasParameters> dbParameters = new List<DatabasParameters>()
                {
                    new DatabasParameters("@PostID", DataTyp.Int, ValvPost.PostID.ToString()),
                    new DatabasParameters("@AnvandarID", DataTyp.Int, ValvPost.AnvandarID.ToString()),
                    new DatabasParameters("@Konto", DataTyp.VarChar, ValvPost.Konto.ToString()),
                    new DatabasParameters("@Inloggning", DataTyp.VarChar, ValvPost.Inloggning.ToString()),
                    new DatabasParameters("@Losenord", DataTyp.VarChar, ValvPost.Losenord.ToString()),
                    new DatabasParameters("@Postnamn", DataTyp.VarChar, ValvPost.Postnamn.ToString()),
                    new DatabasParameters("@Webbadress", DataTyp.VarChar, ValvPost.Webbadress.ToString()),
                    new DatabasParameters("@Anteckningar", DataTyp.VarChar, ValvPost.Anteckningar.ToString()),
                    new DatabasParameters("@AnvandarNamnSkapad", DataTyp.VarChar, ValvPost.AnvandarNamnSkapad.ToString()),
                    new DatabasParameters("@SkapadDatum", DataTyp.VarChar, ValvPost.SkapadDatum.ToString()),
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