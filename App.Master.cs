using System;
using System.Configuration;
using System.Text;
using System.Web;
using Valvetwebb.Objekt;

namespace Valvetwebb

{
    /// <summary>
    /// Master web page
    /// </summary>
    public partial class App : System.Web.UI.MasterPage
    {
        private string navigateUrl;

        #region "Properties"

        /// <summary>
        /// The Webuser
        /// </summary>
        protected Anvandare WebUser
        {
            get
            {
                if (Session["WebUser"] == null)
                {
                    return null;
                }
                else
                {
                    return (Anvandare)Session["WebUser"];
                }
            }
            set
            {
                if (value == null)
                {
                    Session["WebUser"] = null;
                }
                else
                {
                    Session["WebUser"] = value;
                }
            }
        }
        #endregion

        /// <summary>
        /// Check if the SessionID still is valid, if not return the user to "Portalen" for a new login.
        /// 
        /// 1. Kolla att sessionen finns, görs i ASPState.
        /// 2. Fixa till ett User-objekt (WebUser).
        /// 3. Product dropdown:en ska initieras. 
        /// 4. Tabbarna/flikarna ska fixas.
        /// 
        /// </summary>
        /// <param name="e">Arguments</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            navigateUrl = "LogIn.aspx";

            CreateWebUser();

        }

        /// <summary>
        /// The Page_Load event.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //string myPageUrl;
            //string logOutUrl;
            //string statJS;
            StringBuilder clientScript = new StringBuilder();

            //myPageUrl = ConfigurationManager.AppSettings["MyPageUrl"].ToString();
            //logOutUrl = ConfigurationManager.AppSettings["LogOutUrl"].ToString();
            //statJS = ConfigurationManager.AppSettings["StatJS"].ToString();

            //Register client script on page
            Page.ClientScript.GetPostBackEventReference(this, "");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SessionKeepAlive", clientScript.ToString(), true);
        }

        /// <summary>
        /// Skapa en "WebUser". Om det ska göras på detta sätt får vi väl återkomma till, får duga så länge...
        /// </summary>
        private void CreateWebUser()
        {
            if (Session["WebUser"] != null)
            {
                litUser.Text = "Användare: " + WebUser.Anvandarnamn; // display user name
                string database = ConfigurationManager.AppSettings["Default_Databas"];
                litDatabas.Text = " Database: " + database;
            }
        }
    }
}
