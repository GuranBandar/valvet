using System;
using Valvetwebb.Aktivitet;
using Valvetwebb.Objekt;

namespace Valvetwebb
{
    public partial class NyttLösenord : PageBase
    {
        private bool NyLosen { get; set; }

        public Anvandare Anvandare { get; set; }

        /// <summary>
        /// FelID från metodanrop till GUI:et
        /// </summary>
        public static string FelID = "";
        /// <summary>
        /// FelText från metodanrop till GUI:et
        /// </summary>
        public static string Feltext = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Navigation"] == null)
            {
                Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                Anvandare = new Anvandare();
                Anvandare = (Anvandare)Session["WebUser"];
                txtLoginName.Text = Anvandare.Anvandarnamn.ToString();
                txtOldPassword.Text = Anvandare.Losenord.ToString();
                txtNewPassword.Focus();
            }
        }

        protected void knappOK_Click(object sender, EventArgs e)
        {
            AnvandareAktivitet anvandareAktivitet = new AnvandareAktivitet();
            string navigateUrl = "MessageBox.aspx";
            Session["MessageTitle"] = "Ny inloggning";
            Session["MessageText"] = string.Empty;

            if (AllaFältOK())
            {
                Anvandare = anvandareAktivitet.HämtaAnvandare(Anvandare.AnvandarID);
                if (Anvandare != null)
                {
                    //Uppdateriingen lyckades, spara nu användarobjektet
                    string inloggadDatum = DateTime.Now.ToString("yyyy-MM-dd");
                    Anvandare.Losenord = txtNewPassword.Text;
                    Anvandare.SenastByttLosenordDatum = DateTime.Now.ToString("yyyy-MM-dd");
                    anvandareAktivitet.Spara(Anvandare, false, ref FelID, ref Feltext);
                    Session["MessageText"] = "Uppdatering av nytt lösenord lyckades";
                    Session["Referencepage"] = "Meny.aspx";
                    MessageBoxOKButton(Session["MessageText"].ToString());
                    //Response.Redirect("MessageBox.aspx");
                }
                else
                {
                    Session["MessageText"] = "Felaktigt lösenord";
                    Session["Referencepage"] = "NyttLösenord.aspx";
                    MessageBoxOKButton(Session["MessageText"].ToString());
                    //Response.Redirect("MessageBox.aspx");
                }
            }
            try
            {
                Response.Redirect(navigateUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Nu har användaren ändrat lösenord, aktivera textboxen för Konfirmera lösenord
        /// </summary>
        protected void txtNewPasswordChanged(object sender, EventArgs e)
        {
            NyLosen = true;
        }

        protected void knappAvbryt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Meny.aspx");
        }

        /// <summary>
        /// Gå igenom alla fält i bilden för Anvandare innan uppdatering
        /// </summary>
        /// <returns>True om OK annars false</returns>
        private bool AllaFältOK()
        {
            bool nyLosenOK;
            Anvandare = (Anvandare)Session["WebUser"];

            if (NyLosen)
            {
                nyLosenOK = this.KollaNyLosen(txtNewPassword.Text, txtNewPasswordKonfirmera.Text);
                if (!nyLosenOK)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Kollar att nytt lösen är korrekt
        /// </summary>
        /// <param name="losen"></param>
        /// <param name="konfirmera"></param>
        /// <returns>True eller false</returns>
        private bool KollaNyLosen(string losen, string konfirmera)
        {
            bool result = false;

            if (losen.Equals(konfirmera))
            {
                result = true;
            }
            else
            {
                Session["Referencepage"] = "NyttLösenord.aspx";
                Session["MessageText"] = "Felaktigt lösenord";
                result = false;
            }

            return result;
        }
    }
}