using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Valvetwebb
{
    public class Global : HttpApplication
    {
        PageBase pageBase = new PageBase();

        /// <summary>
        /// The appliction start event.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Arguments</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Läs in alla systemvaribler
            //pageBase.InitieraSystemvariabler();
            //Session["Application_Start"] = "Yes";
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            string sUserAccount = HttpContext.Current.User.Identity.Name.ToString();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetNoStore();
            //HttpContext.Current.Session["AnvandarNamn"] = sUserAccount;
        }

        /// <summary>
        /// The application end event.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Arguments</param>
        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}