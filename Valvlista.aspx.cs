using Valvetwebb.Kontroller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Valvetwebb
{
    public partial class Valvlista : PageBase
    {

        public GUI_kontroller gUI_Kontroller;
        public int BokningID { get; set; }
        /// <summary>
        /// FelID från metodanrop till GUI:et
        /// </summary>
        public static string FelID = "";
        /// <summary>
        /// FelText från metodanrop till GUI:et
        /// </summary>
        public static string Feltext = "";

        private string FromDatum { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Navigation"] == null)
            {
                Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                gUI_Kontroller = new GUI_kontroller();
                GetCurrentCulture();
                //lblErrorMessage.Text = string.Empty;
                Session["Referencepage"] = "Valvlista.aspx";
                Session["MessageTitle"] = "Valvlista";
                Session["MessageText"] = string.Empty;

                //if (Session["FromDatum"] == null)
                //{
                //    //Visa kalender från dagens datum
                //    //DatePicker.SelectedDate = DateTime.Today;
                //    DateTime selectedDate = DateTime.Today;
                //    //txtDatum.Text = selectedDate.ToString("yyyy-MM-dd");
                //    this.knappSearch_Click(sender, e);
                //}
                //else
                //{
                //    //txtDatum.Text = Session["FromDatum"].ToString();
                //    this.knappSearch_Click(sender, e);
                //}
            }
        }

        /// <summary>
        /// Visa-knappen i listan här 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgList_CellContentClick(object sender, DataGridCommandEventArgs e)
        {
            TableCell itemCell = e.Item.Cells[0];
            string item = itemCell.Text;
            TableCell itemDag = e.Item.Cells[4];
            string dag;
            string sidan;
            try
            {
                dag = itemDag.Text;
                switch (dag)
                {
                    case "Tisdagsgäng":
                        Session["Bokningsdag"] = "Tisdag";
                        sidan = "Tisdagslista.aspx";
                        break;
                    case "Torsdagsgubs":
                        Session["Bokningsdag"] = "Torsdag";
                        sidan = "Tisdagslista.aspx";
                        break;
                    default:
                        sidan = "Kalender.aspx";
                        break;
                }
                VisaSida(sidan, "BokningID", item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void VisaSida(string sidan, string idFalt, string id)
        {
            string NavigateUrl;
            NavigateUrl = sidan + "?" + idFalt + "= " + id;
            Response.Redirect(NavigateUrl);
        }

        protected void knappSearch_Click(object sender, EventArgs e)
        {
            //if (txtDatum.Text == string.Empty)
            //{
            //    DateTime selectedDate = DateTime.Today;
            //    txtDatum.Text = selectedDate.ToString("yyyy-MM-dd");
            //}
            //Session["FromDatum"] = txtDatum.Text;
            //FromDatum = txtDatum.Text;
            dgList.DataSource = InitieraValvlista();
            dgList.DataBind();
        }

        protected void knappAvbryt_Click(object sender, EventArgs e)
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

        private DataView VisaTomKalender()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Bokningsnummer", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Datum", typeof(string)));
            dt.Columns.Add(new DataColumn("Bokade tider", typeof(string)));
            dt.Columns.Add(new DataColumn("Bana", typeof(string)));
            dt.Columns.Add(new DataColumn("Dag", typeof(string)));

            DataView dv = new DataView(dt);
            return dv;
        }

        /// <summary>
        /// Sök bokningar med bokningsdatum
        /// </summary>
        /// <returns></returns>
        private DataView InitieraValvlista()
        {
            //Loggning.SkrivaPaLoggfil("Nu ska kalendern initieras med sökdatum = "
            //    + FromDatum.ToString());            
            //BokningAktivitet bokningAktivitet = new BokningAktivitet();
            //List<BokningDag> bokningDagList = 
            //    bokningAktivitet.SearchBokning(FromDatum);
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Bokningsnummer", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Datum", typeof(string)));
            dt.Columns.Add(new DataColumn("Bokade tider", typeof(string)));
            dt.Columns.Add(new DataColumn("Bana", typeof(string)));
            dt.Columns.Add(new DataColumn("Dag", typeof(string)));

            //if (bokningDagList.Count > 0)
            //{
            //    foreach (BokningDag bokning in bokningDagList)
            //    {
            //        dr = dt.NewRow();
            //        dr[0] = bokning.BokningID;
            //        dr[1] = bokning.Datum;
            //        dr[2] = bokning.Tider;
            //        dr[3] = bokning.Bana;

            //        switch (bokning.TisdagTorsdag)
            //        {
            //            case 2:
            //                dr[4] = "Tisdagsgäng";
            //                break;
            //            case 4:
            //                dr[4] = "Torsdagsgubs";
            //                break;
            //            default:
            //                break;
            //        }

            //        dt.Rows.Add(dr);
            //    }
            //}
            //else
            //{
            //    //Visa felmeddelande och ta bort ev From-datum
            //    Session["FromDatum"] = string.Empty;
            //    //MessageBoxOKButton("Finns inga kommande bokningar");
            //}

            DataView dv = new DataView(dt);
            return dv;
        }

        protected void lnkPickDate_Click(object sender, EventArgs e)
        {
            //DatePicker.Visible = true;
        }

        protected void DatePicker_selection_changed(object sender, EventArgs e)
        {
            //DateTime selectedDate = Convert.ToDateTime(DatePicker.SelectedDate.ToShortDateString());
            //txtDatum.Text = selectedDate.ToString("yyyy-MM-dd");
            //DatePicker.Visible = false;
        }
    }
}