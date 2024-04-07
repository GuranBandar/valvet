using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.UI;
using Valvetwebb.Kontroller;
using Valvetwebb.Objekt;

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

        /// <summary>
        /// Rensa cachen
        /// </summary>
        public void ClearCacheItems()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();

            while (enumerator.MoveNext())
                keys.Add(enumerator.Key.ToString());

            for (int i = 0; i < keys.Count; i++)
                Cache.Remove(keys[i]);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "cle", "windows.history.clear", true);
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
        /// Skriv meddelande
        /// </summary>
        /// <param name="text">Actual text</param>
        /// <returns>A string with the actual text.</returns>
        public void MessageBoxOKButton(string text)
        {
            Extensions.MessageBoxOKButton(this, text);
        }

        /// <summary>
        /// Skriv meddelande
        /// </summary>
        /// <param name="text">Actual text</param>
        /// <returns>A string with the actual text.</returns>
        public void MessageBox()
        {
            Response.Redirect("MessageBox.aspx");
        }

        /// <summary>
        /// Dags att logga ut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SessionLogout()
        {
            Session.Clear();
            Session["Navigation"] = null;
            Session.Abandon();
            Session["Referencepage"] = null;
            Response.Cookies.Clear();
            ClearCacheItems();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }


        /// <summary>
        /// Användaren har klickat på en länken, kör nu den i den Web Browser användaren föredrar.
        /// Funkar inte, vet inte hur jag ska lösa det heller.
        /// </summary>
        /// <param name="url">Länken som ska köras</param>
        protected void StartWebbrowser(string url)
        {
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            string browserType = browser.Browser;
            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

            try
            {
                string fullUrl = HttpContext.Current.Request.Url.AbsoluteUri; // Full URL (including query parameters)
                Response.Redirect("http://google.com");

                //Process.Start(new ProcessStartInfo
                //{
                //    UseShellExecute = true,
                //    FileName = url
                //});
                //    ProcessStartInfo startInfo = new ProcessStartInfo();
                //    switch (browserType)
                //    {
                //        case "Chrome":
                //            if (RunningProcessPaths.Contains("chrome.exe"))
                //            {
                //                startInfo.FileName = "chrome.exe";
                //                startInfo.Arguments = url;
                //                Process.Start(startInfo);
                //            }
                //            else
                //            {
                //                Process.Start(new ProcessStartInfo
                //                {
                //                    UseShellExecute = true,
                //                    FileName = url
                //                });
                //            }
                //            break;
                //        case "Edge":
                //            Process.Start("msedge.exe", url);
                //            break;
                //        case "Firefox":
                //            if (RunningProcessPaths.Contains("firefox.exe"))
                //            {
                //                startInfo.FileName = "firefox.exe";
                //                startInfo.Arguments = url;
                //                Process.Start(startInfo);
                //            }
                //            else
                //            {
                //                Process.Start("firefox.exe", "https://" + url);
                //            }
                //            break;
                //        case "IExplore":
                //            if (RunningProcessPaths.Contains("IExplore.exe"))
                //            {
                //                startInfo.FileName = "IExplore.exe";
                //                startInfo.Arguments = url;
                //                Process.Start(startInfo);
                //            }
                //            else
                //            {
                //                Process.Start("IExplore.exe", "https://" + url);
                //            }
                //            break;
                //        case "Opera":
                //            if (RunningProcessPaths.Contains("opera.exe"))
                //            {
                //                startInfo.FileName = "opera.exe";
                //                startInfo.Arguments = url;
                //                Process.Start(startInfo);
                //            }
                //            else
                //            {
                //                Process.Start("opera.exe", "https://" + url);
                //            }
                //            break;
                //        default:
                //            Process.Start("explorer.exe", "http://google.com");
                //            break;
                //    }
            }
            catch (System.ComponentModel.Win32Exception w)
            {
                Session["MessageText"] = "Unable to find the Web Browser... " + browserType + " not found!" + Environment.NewLine + w.InnerException;
                MessageBox();
            }
            catch (Exception)
            {
                Session["MessageText"] = "Failed to open url: " + url ;
                MessageBox();
            }
        }
    }
}