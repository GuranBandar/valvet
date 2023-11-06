using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using Valvet.Objekt;

namespace Valvetwebb
{
    /// <summary>
    /// Summary description for PageBase.
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
//        CultureResourceReader resourceReader = new CultureResourceReader();
        private string defaultLanguage;
        protected string dateTimeFormat;

        #region "Properties"
        /// <summary>
        /// The actual WebUser.
        /// </summary>
        protected Anvandare AppUser { get; set; }

        /// <summary>
        /// Property for DataSetToUse
        /// </summary>
        protected DataSet DataSetToUse
        {
            get
            {
                if (Session["DataSetToUse"] == null)
                {
                    return null;
                }
                else
                {
                    return (DataSet)Session["DataSetToUse"];
                }
            }
            set
            {
                if (value == null)
                {
                    Session["DataSetToUse"] = null;
                }
                else
                {
                    Session["DataSetToUse"] = value;
                }
            }
        }

        protected CultureInfo Culture { get; set; }

        #endregion

        /// <summary>
        /// Retrieves the control that caused the postback.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public string GetControlThatCausedPostBack(Page page)
        {
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by       the controls
            //which used "_doPostBack" function also available in Request.Form collection.

            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
            }

            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {
                    //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                    //mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y") ||
                        ctl.StartsWith("knapp"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length);
                        c = Page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = Page.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                             c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }

            if (control != null)
                return control.ID;
            else
                return string.Empty;
        }

        public void GetCurrentCulture()
        {
            defaultLanguage = ConfigurationManager.AppSettings["GlobalEnvironmentLanguage"];
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            Culture = CultureInfo.CurrentCulture;
            defaultLanguage = Culture.DisplayName;
            Culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
        }

        protected void Logga(string logmessage)
        {
            //Loggning.SkrivaPaLoggfil("Nu ska vi logga lite");
        }

        /// <summary>
        /// The date will be formatted as yyyy-MM-dd
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns>A formatted date</returns>
        protected string FormatDate(string date)
        {
            return date.Substring(0, 4) + "-" + date.Substring(5, 2) + "-" + date.Substring(8, 2);
        }

        /// <summary>
        /// Redirect to the given url
        /// </summary>
        /// <param name="navigateUrl"></param>
        protected void Redirect(string navigateUrl)
        {
            Server.Transfer(navigateUrl);
        }

        /// <summary>
        /// Get text from the Global Resourcefile.
        /// </summary>
        /// <param name="name">Actual text</param>
        /// <returns>A string with the actual text.</returns>
        public string GetText(string name)
        {
            string foo = null;
            //defaultLanguage = ConfigurationManager.AppSettings["GlobalEnvironmentLanguage"];

            //try
            //{
            //    foo = Översätt(name, defaultLanguage);

            //    if (foo == null)
            //    {
            //        foo = "Text saknas: " + name;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Logga skiten
            //    Logging.WriteLog("E", "Textfel i " + ObjectInformation.GetMethodName() + "\n\n" + ex.ToString(),
            //        1998);
            //}

            return foo;
        }


    }
}