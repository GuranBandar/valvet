using System;
using Valvetwebb.Kontroller;

namespace Valvetwebb.Aktivitet
{
    /// <summary>
    /// Klass för skapande av wherevillkor för statistiksökningar
    /// </summary>
    public abstract class SökVillkor
    {
        /// <summary>
        ///     Skapar sökvillkor för statistiken
        /// </summary>
        /// <param name="banaNr">Bananummer</param>
        /// <param name="spelarID">Aktuellt spelarID</param>
        /// <param name="fromDatum">Ev from datum</param>
        /// <param name="tomDatum">Ev tom datum</param>
        /// <param name="hcprond">Ev markering för hcprond</param>
        /// <param name="niohalsrond">Ev markering för niohålsrond</param>
        /// <param name="sallskapsrond">Ev markering för sällskapsrond</param>
        /// <param name="tavlingsrond">Ev markering för tävlingsrond</param>
        /// <returns>Färdigknåpat sökvillkor för statistiken</returns>
        protected static string SkapaSökvillkor(string golfklubbNr, string banaNr, string spelarID,
            DateTime fromDatum, DateTime tomDatum, bool hcprond, bool niohalsrond, bool sallskapsrond,
            bool tavlingsrond)
        {
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (spelarID.ToString() != "")
                {
                    WhereFörInteger(spelarID, "r.SpelarID", ref sqlSok, ref antArgument, " = ");
                }

                if (golfklubbNr.ToString() != "")
                {
                    WhereFörInteger(golfklubbNr, "g.GolfklubbNr", ref sqlSok, ref antArgument, " = ");
                }

                if (banaNr.ToString() != "")
                {
                    WhereFörInteger(banaNr, "r.BanaNr", ref sqlSok, ref antArgument, " = ");
                }

                if (fromDatum.ToString() != "")
                {
                    WhereFörSträng(fromDatum.ToString(), "r.Datum", ref sqlSok, ref antArgument, " >= ");
                }

                if (tomDatum.ToString() != "")
                {
                    WhereFörSträng(tomDatum.ToString(), "r.Datum", ref sqlSok, ref antArgument, " <= ");
                }

                if (hcprond)
                {
                    WhereFörSträng("X", "Hcprond", ref sqlSok, ref antArgument, " = ");
                }

                if (niohalsrond)
                {
                    WhereFörSträng("X", "Niohalsrond", ref sqlSok, ref antArgument, " = ");
                }

                if (sallskapsrond)
                {
                    WhereFörSträng("X", "Sallskapsrond", ref sqlSok, ref antArgument, " = ");
                }

                if (tavlingsrond)
                {
                    WhereFörSträng("X", "Tavlingsrond", ref sqlSok, ref antArgument, " = ");
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE " + sqlSok;
                }
                return sql.ToString();
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        ///     Skapar sökvillkor för statistiken
        /// </summary>
        /// <param name="redovisningsTyp">RedovisningsTyp</param>
        /// <param name="spelarID">Aktuellt spelarID</param>
        /// <param name="fromDatum">Ev from datum</param>
        /// <param name="tomDatum">Ev tom datum</param>
        /// <param name="detaljerad">Ev markering för detaljerad redovisning</param>
        /// <param name="summerad">Ev markering för summerad redovisning</param>
        /// <returns>Färdigknåpat sökvillkor för statistiken</returns>
        protected static string SkapaSökvillkor(string redovisningsTyp, string spelarID, DateTime fromDatum,
            DateTime tomDatum, bool detaljerad, bool summerad)
        {
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (!string.IsNullOrEmpty(spelarID.ToString()) & spelarID != "0")
                {
                    WhereFörInteger(spelarID, "re.SpelarID", ref sqlSok, ref antArgument, " = ");
                }

                if (!string.IsNullOrEmpty(redovisningsTyp.ToString()) & redovisningsTyp != "0")
                { //  om typ angivits
                    WhereFörSträng(redovisningsTyp, "re.typ ", ref sqlSok, ref antArgument, "= ");
                }

                if (!string.IsNullOrEmpty(fromDatum.ToString()))
                {
                    WhereFörSträng(fromDatum, "re.Datum", ref sqlSok, ref antArgument, " >= ");
                }

                if (!string.IsNullOrEmpty(tomDatum.ToString()))
                {
                    WhereFörSträng(tomDatum, "re.Datum", ref sqlSok, ref antArgument, " <= ");
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE " + sqlSok;
                }
                return sql.ToString();
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Skapar sökvillkor för tävlingar
        /// </summary>
        /// <param name="tavlingNamn">Aktuell tavlingNamn</param>
        /// <param name="spelsatt">Spelsätt</param>
        /// <param name="speltyp">Speltyp</param>
        /// <param name="fromDatum">From datum</param>
        /// <param name="tomDatum">Tom datum</param>
        /// <returns>Färdigknåpat sökvillkor för statistiken</returns>
        protected static string SkapaSökvillkor(string tavlingNamn, string spelsatt, string speltyp, DateTime fromDatum,
            DateTime tomDatum)
        {
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (!string.IsNullOrEmpty(tavlingNamn.ToString()))
                {
                    WhereMedLikeEfter(tavlingNamn, "t.Namn", ref sqlSok, ref antArgument);
                }

                if (!string.IsNullOrEmpty(spelsatt.ToString()) & spelsatt != "00")
                {
                    WhereFörSträng(spelsatt, "t.Spelsatt", ref sqlSok, ref antArgument, " = ");
                }

                if (!string.IsNullOrEmpty(speltyp.ToString()) & speltyp != "00")
                {
                    WhereFörSträng(speltyp, "t.Speltyp", ref sqlSok, ref antArgument, " = ");
                }

                if (!string.IsNullOrEmpty(fromDatum.ToString()))
                {
                    WhereFörSträng(fromDatum, "t.StartDatum", ref sqlSok, ref antArgument, " >= ");
                }

                if (!string.IsNullOrEmpty(tomDatum.ToString()))
                {
                    WhereFörSträng(tomDatum, "t.StartDatum", ref sqlSok, ref antArgument, " <= ");
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE " + sqlSok;
                }
                return sql.ToString();
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Skapar sökvillkor för tävlingar
        /// </summary>
        /// <param name="fromDatum">From datum</param>
        /// <param name="tomDatum">Tom datum</param>
        /// <returns>Färdigknåpat sökvillkor för statistiken</returns>
        protected static string SkapaSökvillkor(DateTime fromDatum, DateTime tomDatum)
        {
            short antArgument = 0;
            string sqlSok = "";
            string sql = "";
            try
            {
                if (!string.IsNullOrEmpty(fromDatum.ToString()))
                {
                    WhereFörSträng(fromDatum, "t.StartDatum", ref sqlSok, ref antArgument, " >= ");
                }

                if (!string.IsNullOrEmpty(tomDatum.ToString()))
                {
                    WhereFörSträng(tomDatum, "t.StartDatum", ref sqlSok, ref antArgument, " <= ");
                }

                if (antArgument > 0)
                {
                    sql = sql + " WHERE " + sqlSok;
                }
                return sql.ToString();
            }
            catch (ValvetException)
            {
                throw;
            }
        }

        /// <summary>
        /// Skapar dynamisk frågesträng för sträng, lägger först på ett AND om det finns andra sökkriterier.
        /// </summary>
        /// <param name="fältVärde">Vad, värdet, term nedan ska sätta till</param>
        /// <param name="fältNamn">Vad termen heter som ska sättas</param>
        /// <param name="kriteria">Sträng med tidigare wherevillkor</param>
        /// <param name="argRäknare">Antal argument i kriteria</param>
        /// <param name="operand">Typ av operand, t ex större än</param>
        /// <returns>Söksträng efter behandling, räknaren upptaggad med 1 och true, om allt gick bra</returns>
        protected static void WhereFörSträng(object fältVärde, string fältNamn, ref string kriteria,
            ref short argRäknare, string operand)
        {
            try
            {
                if (string.IsNullOrEmpty(fältVärde.ToString()))
                {
                    // Om villkoret är blankt
                    throw new ValvetException();
                }
                else
                {
                    // Om villkoret <> blankt
                    // Lägg på ''AND'' om det finns andra kriterier
                    if (argRäknare > 0)
                    {
                        kriteria += " AND ";
                    }

                    // Lägger till fnuttar (')
                    kriteria = (kriteria + fältNamn + operand + "'" + fältVärde + "'");

                }

                argRäknare++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Skapar dynamisk frågesträng för sträng med "LIKE" och med wildcard (%) efter inmatat värde. 
        /// Finns fler sökkriterier läggs först på ett "AND".
        /// </summary>
        /// <param name="fältVärde">Vad, värdet, term nedan ska sätta till</param>
        /// <param name="fältNamn">Vad termen heter som ska sättas</param>
        /// <param name="kriteria">Sträng med tidigare wherevillkor</param>
        /// <param name="argRäknare">Antal argument i kriteria</param>
        /// <returns>Söksträng efter behandling, räknaren upptaggad med 1 och true, om allt gick bra</returns>
        protected static void WhereMedLikeEfter(object fältVärde, string fältNamn, ref string kriteria,
            ref short argRäknare)
        {
            try
            {
                if (string.IsNullOrEmpty(fältVärde.ToString()))
                {
                    // Om villkoret är blankt
                    throw new ValvetException();
                }
                else
                {
                    // Om villkoret <> blankt
                    // Lägg på ''AND'' om det finns andra kriterier
                    if (argRäknare > 0)
                    {
                        kriteria += " AND ";
                    }

                    // Lägger till '=39
                    kriteria = (kriteria + fältNamn + " LIKE " + "'" + fältVärde + "%'");
                }

                argRäknare++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Skapar dynamisk frågesträng för sträng med "LIKE" och med wildcard (%) efter inmatat värde. 
        /// Finns fler sökkriterier läggs först på ett "OR".
        /// </summary>
        /// <param name="fältVärde">Vad, värdet, term nedan ska sätta till</param>
        /// <param name="fältNamn">Vad termen heter som ska sättas</param>
        /// <param name="kriteria">Sträng med tidigare wherevillkor</param>
        /// <param name="argRäknare">Antal argument i kriteria</param>
        /// <returns>Söksträng efter behandling, räknaren upptaggad med 1 och true, om allt gick bra</returns>
        protected static void WhereMedORochLikeEfter(object fältVärde, string fältNamn, ref string kriteria,
            ref short argRäknare)
        {
            try
            {
                if (string.IsNullOrEmpty(fältVärde.ToString()))
                {
                    // Om villkoret är blankt
                    throw new ValvetException();
                }
                else
                {
                    // Om villkoret <> blankt
                    // Lägg på ''OR'' om det finns andra kriterier
                    if (argRäknare > 0)
                    {
                        kriteria += " OR ";
                    }

                    // Lägger till '=39
                    kriteria = (kriteria + fältNamn + " LIKE " + "'" + fältVärde + "%'");
                }

                argRäknare++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Skapar dynamisk frågesträng för en integer (numeriskt värde). Finns fler
        /// sökvillkor läggs först till ett "AND".
        /// </summary>
        /// <param name="fältVärde">Vad term ska sätta till</param>
        /// <param name="fältNamn">Vad termen heter som ska sättas</param>
        /// <param name="kriteria">Sträng med tidigare wherevillkor</param>
        /// <param name="argRäknare">Antal argument i strKriteria</param>
        /// <param name="operand">Lika med, större än, etc</param>
        /// <returns>Söksträng efter behandling, räknaren upptaggad med 1 och true, om allt gick bra</returns>
        protected static void WhereFörInteger(object fältVärde, string fältNamn, ref string kriteria,
            ref short argRäknare, string operand)
        {
            try
            {
                if (string.IsNullOrEmpty(fältVärde.ToString()))
                {
                    // Om villkoret är blankt
                    throw new ValvetException();
                }
                else
                {
                    // Om villkoret <> blankt
                    // Lägg på ''AND'' om det finns andra kriterier
                    if (argRäknare > 0)
                    {
                        kriteria += " AND ";
                    }

                    kriteria = (kriteria + fältNamn + operand + fältVärde);

                }

                argRäknare++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
