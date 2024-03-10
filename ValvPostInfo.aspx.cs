using System;
using Valvetwebb.Aktivitet;
using Valvetwebb.Objekt;

namespace Valvetwebb
{
    public partial class ValvPostInfo : PageBase
    {
        private bool NyPost { get; set; }

        public ValvPost valvPost { get; set; }

        public int PostID { get; set; }

        public Anvandare WebUser { get; set; }
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
                valvPost = new ValvPost();

                if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
                {
                    PostID = int.Parse(Request["PostID"].ToString());
                    WebUser = (Anvandare)Session["WebUser"];
                }

                if (PostID != 0)
                {
                    //Då kommer det från ValvListan
                    VisaValvPost();
                }
                else
                {
                    VisaTomValvPost();
                    //this.HämtaSistaBokning();
                }
            }
        }

        /// <summary>
        /// Visa ValvPost
        /// </summary>
        public void VisaValvPost()
        {
            ValvPostAktivitet valvpostAktivitet = new ValvPostAktivitet();

            try
            {
                valvPost = valvpostAktivitet.HämtaValvPost(PostID, WebUser.AnvandarID);

                if (valvPost != null)
                {
                    FyllBild();
                }
                else
                {
                    Session["MessageText"] = "Valvpost saknas";
                    MessageBox();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fyll bilden med data från tabellen
        /// </summary>
        private void FyllBild()
        {
            lblPostnamn.Text = valvPost.Postnamn;
            txtUsernamn.Text = valvPost.Usernamn;
            txtLosenord.Text = valvPost.Losenord;
            txtWebadress.Text = valvPost.Webbadress;
            txtAnteckningar.Text = valvPost.Anteckningar;
        }

        /// <summary>
        /// Fyll bilden med data från tabellen
        /// </summary>
        private void VisaTomValvPost()
        {
        }

        /// <summary>
        /// Sparaknappen är tryckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappSpara_Click(object sender, EventArgs e)
        {
            ValvPostAktivitet valv;
            int nyttPostID;
            lblMessage.Text = string.Empty;

            try
            {
                if (Session["hfiNyPost"].ToString() == "Ja")
                {
                    NyPost = true;
                }

                if (AllaFältBokningDagOK() && AllaFältBokningListaOK())
                {
                    bokningAktivitet = new BokningAktivitet();
                    nyttBokningID = bokningAktivitet.Spara(BokningDag, NyBokning, ref FelID, ref Feltext);

                    if (NyBokning)
                    {
                        BokningID = nyttBokningID;
                        Session["hfiNyBokning"] = "Nej";
                    }
                    else
                    {
                        BokningID = int.Parse(Session["hfiBokningID"].ToString());
                    }

                    VisaBokning();

                    List<DropDownList> allControls = new List<DropDownList>();
                    gUI_Kontroller.GetControlList<DropDownList>(Page.Controls, allControls);

                    foreach (var childControl in allControls)
                    {
                        childControl.Enabled = true;
                    }

                    MessageBoxOKButton("Uppdatateringen lyckades");
                    knappNy.Enabled = true;
                }
                else
                {
                    MessageBoxOKButton(Session["MessageText"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sparaknappen är tryckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappTillbaka_Click(object sender, EventArgs e)
        {
            Response.Redirect("Meny.aspx");
        }

        /// <summary>
        /// Dags att logga ut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappLogout_Click(object sender, EventArgs e)
        {
            SessionLogout();
            Response.Redirect("LogIn.aspx");
        }

        /// <summary>
        /// Gå igenom alla fält i bilden för ValvPost innan uppdatering
        /// </summary>
        /// <returns>True om OK annars false</returns>
        private bool AllaFältValvPostOK()
        {
            ValvPostAktivitet valvPostAktivitet;
            DateTime dagensDatum = DateTime.Today;

            if (txtUsernamn.Text == string.Empty)
            {
                Session["MessageText"] = "Usernamn saknas";
                //RensaInmatatdata();
                return false;
            }

            DateTime bokningsdatum = DateTime.Parse(DateTime.Today.ToString());
            string sokdatum = bokningsdatum.ToString("yyyy-MM-dd");

            valvPost = new ValvPost();

            //Alla fält från bilden flyttas till objektet
            if (Session["hfiNyPost"].ToString() == "Nej")
            {
                //BokningDag.BokningID = Convert.ToInt32(Session["hfiBokningID"]);
            }

            BokningDag.Datum = txtDatum.Text.ToString().Trim();
            BokningDag.Tider = txtTider.Text.ToString().Trim();
            BokningDag.Bana = txtBana.Text.ToString().Trim();

            switch (Session["Bokningsdag"].ToString())
            {
                case "Tisdag":
                    BokningDag.TisdagTorsdag = 2;
                    break;
                case "Torsdag":
                    BokningDag.TisdagTorsdag = 4;
                    break;
                default:
                    //Session["MessageText"].ToString() = "ja, vad fan gör vi då?";
                    break;
            }

            if (Session["hfiNyBokning"].ToString() == "Ja")
            {
                BokningDag.AnvandarNamnSkapad = Session["AnvandarNamn"].ToString();
                BokningDag.SkapadDatum = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                BokningDag.AnvandarNamnSkapad = Session["hfiAnvandarNamnSkapad"].ToString();
                BokningDag.SkapadDatum = Session["hfiSkapadDatum"].ToString();
            }
            BokningDag.AnvandarNamnUppdat = Session["AnvandarNamn"].ToString();
            BokningDag.UppdatDatum = DateTime.Now.ToString("yyyy-MM-dd");
            BokningDag.Notering = txtNotering.Text.ToString().Trim();
            return true;
        }
    }
}