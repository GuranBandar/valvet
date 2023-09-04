using System;
using System.Threading;

namespace Valvet
{
    /// <summary>
    /// Klass för att logga in på Bokningswebben
    /// </summary>
    public partial class LogIn : PageBase
    {
        /// <summary>
        /// FelID från metodanrop till GUI:et
        /// </summary>
        public static string FelID = "";
        /// <summary>
        /// FelText från metodanrop till GUI:et
        /// </summary>
        public static string Feltext = "";
        /// <summary>
        /// Påloggad användare
        /// </summary>
        //protected static Anvandare AppUser { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
            txtAnvandarNamn.Focus();
            GetCurrentCulture();
        }

        protected void knappOK_Click(object sender, EventArgs e)
        {
            Anvandare anvandare = new Anvandare();
            AnvandareAktivitet anvandareAktivitet = new AnvandareAktivitet();

            try
            {
                lblErrorMessage.Visible = true;

                if (txtAnvandarNamn.Text.Length > 0)
                {
                    anvandare = anvandareAktivitet.LoggaIn(txtAnvandarNamn.Text, txtLosenord.Text);

                    if (anvandare != null)
                    {
                        //Inloggningen lyckades, spara nu användarobjektet
                        string inloggadDatum = DateTime.Now.ToString("yyyy-MM-dd");
                        anvandare.SenastInloggadDatum = inloggadDatum;
                        AppUser = anvandare;
                        anvandareAktivitet.Spara(anvandare, false, ref FelID, ref Feltext);
                                                anvandareAktivitet.InloggningOK(anvandare.AnvandarID, anvandare.SenastInloggadDatum,
                                                    ref FelID, ref Feltext);

                        Session["AnvandarNamn"] = txtAnvandarNamn.Text;
                        Session["WebUser"] = anvandare;
                        Session["NyBokning"] = string.Empty;
                        Response.Redirect("Meny.aspx");
                    }
                    else
                    {
                        txtAnvandarNamn.Text = "";
                        txtLosenord.Text = "";
                        lblErrorMessage.Text = "Felaktig inloggning";
                    }
                }
                else
                {
                    lblErrorMessage.Text = "Användare saknas";
                }
            }
            catch (ThreadAbortException tex)
            {
                return;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message + " Source: " + ex.Source + 
                    " i metoden Login.aspx.cs.kanppOK";
            }
        }
        protected void knappAvbryt_Click(object sender, EventArgs e)
        {
            knappAvbryt.Attributes.Add("OnClick", "window.close();");
            //Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
            //Session.RemoveAll();
            //FormsAuthentication.SignOut();
            //Response.End();
            //Session.Clear();
        }
    }
}