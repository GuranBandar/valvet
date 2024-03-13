﻿using System;
using Valvetwebb.Aktivitet;
using Valvetwebb.Objekt;

namespace Valvetwebb
{
    public partial class ValvPostInfo : PageBase
    {
        private bool NyPost { get; set; }

        public ValvPost valvPost { get; set; }

        public int PostID { get; set; }

        public int AnvandarID { get; set; }

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
                    AnvandarID = WebUser.AnvandarID;
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
                valvPost = valvpostAktivitet.HämtaValvPost(PostID, AnvandarID);

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
            txtPostnamn.Text = valvPost.Postnamn;
            txtUsernamn.Text = valvPost.Usernamn;
            txtLosenord.Text = valvPost.Losenord;
            txtWebadress.Text = valvPost.Webbadress;
            txtAnteckningar.Text = valvPost.Anteckningar;
            Session["PostID"] = valvPost.PostID;
            Session["AnvandarID"] = valvPost.AnvandarID;
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
            ValvPostAktivitet valvPostAktivitet;
            int nyttPostID;

            try
            {
                if (Session["hfiNyPost"].ToString() == "Ja")
                {
                    NyPost = true;
                }

                if (AllaFältValvPostOK())
                {
                    valvPostAktivitet = new ValvPostAktivitet();
                    nyttPostID = valvPostAktivitet.Spara(valvPost, NyPost, ref FelID, ref Feltext);

                    if (NyPost)
                    {
                        PostID = nyttPostID;
                        Session["hfiNyPost"] = "Nej";
                    }
                    else
                    {
                        PostID = int.Parse(Session["PostID"].ToString());
                        AnvandarID = (int)Session["AnvandarID"];
                    }

                    VisaValvPost();

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
        /// Ta bort knappen är tryckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappTaBort_Click(object sender, EventArgs e)
        {
            ValvPostAktivitet valvPostAktivitet;
            string confirmValue = Request.Form["confirm_Value"];
            Anvandare webUser = (Anvandare)Session["WebUser"];

            if (confirmValue == "Ja")
            {
                valvPostAktivitet = new ValvPostAktivitet();
            }
            else
            {
                return;
            }

            try
            {
                PostID = int.Parse(Session["hfiPostID"].ToString());
                valvPost = valvPostAktivitet.HämtaValvPost(PostID, webUser.AnvandarID);

                if (valvPost != null)
                {
                    valvPostAktivitet.TaBortValvPost(valvPost, ref FelID, ref Feltext);
                }
                else
                {
                    Session["MessageText"] = "Valvpost saknas";
                    MessageBox();
                }
                Session["Referencepage"] = "Valvlista.aspx";
                Session["MessageText"] = "Borttag av posten lyckades";
                MessageBox();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Nyknappen är tryckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappNy_Click(object sender, EventArgs e)
        {
            this.VisaTomValvPost();
        }

        /// <summary>
        /// Sparaknappen är tryckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void knappTillbaka_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Referencepage"].ToString());
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
            ValvPostAktivitet valvPostAktivitet = new ValvPostAktivitet();
            DateTime dagensDatum = DateTime.Today;
            Anvandare webUser = (Anvandare)Session["WebUser"];

            if (txtUsernamn.Text == string.Empty)
            {
                Session["MessageText"] = "Usernamn saknas";
                //RensaInmatatdata();
                return false;
            }

            if (txtLosenord.Text == string.Empty)
            {
                Session["MessageText"] = "Lösenord saknas";
                return false;
            }

            valvPost = new ValvPost();
            valvPost.AnvandarID = webUser.AnvandarID;
            valvPost.Usernamn = txtUsernamn.Text;
            valvPost.Losenord = txtLosenord.Text;
            valvPost.Webbadress = txtWebadress.Text;
            valvPost.Postnamn = txtPostnamn.Text;
            valvPost.Anteckningar = txtAnteckningar.Text;
            valvPost.Konto = webUser.Konto;

            if (Session["hfiNyPost"].ToString() == "Ja")
            {
                valvPost.AnvandarNamnSkapad = webUser.Anvandarnamn.ToString();
                valvPost.SkapadDatum = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                valvPost.PostID = int.Parse(Session["PostID"].ToString());
                valvPost.AnvandarNamnUppdat = webUser.Anvandarnamn;
                valvPost.UppdatDatum = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return true;
        }
    }
}