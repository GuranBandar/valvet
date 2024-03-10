using System;
using Valvetwebb.Kontroller;
using Valvetwebb.Objekt;

namespace Valvetwebb
{
    public partial class Meny : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Navigation"] == null)
            {
                Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblErrorMessage.Visible = false;
                SetSessionVariabler();
            }
        }

        protected void SetSessionVariabler()
        {
            Session["hfiNyBokning"] = string.Empty;
            Session["Referencepage"] = string.Empty;
            Session["hfiBokningID"] = string.Empty;
            Session["hfiAnvandarNamnSkapad"] = string.Empty;
            Session["hfiSkapadDatum"] = string.Empty;
            Session["MessageTitle"] = "Menyval";
            Session["MessageText"] = string.Empty;
            Session["Referencepage"] = "Meny.aspx";
            Session["Bokningsdag"] = string.Empty;
        }

        protected void KnappValvlista_Click(object sender, EventArgs e)
        {
            //Session["Ny session"] = "Ja";
            Response.Redirect("Valvlista.aspx");
        }

        protected void knappValvpost_Click(object sender, EventArgs e)
        {
            Session["Ny session"] = "Ja";
            Anvandare anvandare = (Anvandare)Session["Webuser"];
            string anvandarID = anvandare.AnvandarID.ToString();
            Session["Bokningsdag"] = "Tisdag";
            Response.Redirect("ValvPostInfo.aspx");
        }

        /// <summary>
        /// Knapp nytt lösenord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappNyLösen_Click(object sender, EventArgs e)
        {
            Session["Ny session"] = "Ja";
            //Response.Redirect("NyttLösenord.aspx");
        }
        protected void knappAvbryt_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
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
    }
}