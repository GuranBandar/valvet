using System;
using System.Threading;
using Valvetwebb.Aktivitet;
using Valvetwebb.Objekt;

namespace Valvetwebb

{
    /// <summary>
    /// Klass för att logga in till Valvet
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
            if (!IsPostBack)
            {
                lblErrorMessage.Visible = false;
                txtAnvandarNamn.Text = string.Empty;
                txtLosenord.Text = string.Empty;
                txtAnvandarNamn.Focus();
                GetCurrentCulture();
                Session["MessageTitle"] = "Inloggning";
                Session["MessageText"] = string.Empty;
                Session["Referencepage"] = "LogIn.aspx";
                Session["Buttons"] = "OK";
            }
        }

        protected void knappOK_Click(object sender, EventArgs e)
        {
            Anvandare Anvandare = new Anvandare();
            AnvandareAktivitet anvandareAktivitet = new AnvandareAktivitet();

            try
            {
                lblErrorMessage.Visible = true;

                if (txtAnvandarNamn.Text.Length > 0)
                {
                    Anvandare = anvandareAktivitet.LoggaIn(txtAnvandarNamn.Text, txtLosenord.Text);

                    if (Anvandare != null)
                    {
                        //Inloggningen lyckades, spara nu användarobjektet
                        string inloggadDatum = DateTime.Now.ToString("yyyy-MM-dd");
                        Anvandare.SenastInloggadDatum = inloggadDatum;
                        AppUser = Anvandare;
                        anvandareAktivitet.Spara(Anvandare, false, ref FelID, ref Feltext);
                                                anvandareAktivitet.InloggningOK(Anvandare.AnvandarID, Anvandare.SenastInloggadDatum,
                                                    ref FelID, ref Feltext);

                        Session["AnvandarNamn"] = txtAnvandarNamn.Text;
                        Session["WebUser"] = Anvandare;
                        Session["Losenord"] = txtLosenord.Text;
                        Session["Navigation"] = "Yes";
                        Response.Redirect("Valvlista.aspx");
                    }
                    else
                    {
                        txtAnvandarNamn.Text = "";
                        txtLosenord.Text = "";
                        Session["MessageText"] = "Felaktig inloggning";
                        Response.Redirect("MessageBox.aspx");
                    }
                }
                else
                {
                    Session["MessageText"] = "Användare saknas eller lösenord felaktigt";
                    Response.Redirect("MessageBox.aspx");
                }
            }
            catch (ThreadAbortException tex)
            {
                return;
            }
            catch (Exception ex)
            {
                Session["MessageText"] = ex.Message + " Source: " + ex.Source +
                    " i metoden Login.aspx.cs.kanppOK";
                Response.Redirect("MessageBox.aspx");
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