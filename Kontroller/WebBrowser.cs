using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using static System.Runtime.InteropServices.RuntimeInformation;
using static System.Runtime.InteropServices.OSPlatform;
using System.Threading;
using System.IO;
using System.Web.Http;
using System.Runtime.InteropServices;

namespace Valvetwebb.Kontroller
{
    public class WebBrowser
    {
        public static void Main(string[] args)
        {
            //var host = new WebBrowser()
            //    .UseKestrel().UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();

            //host.Start();
            //OpenBrowser("http://localhost:5000/");
            System.Diagnostics.Process.Start("cmd", "/C start http://google.com");
        }

        //private static void OpenBrowser(string url)
        //{
        //    Process.Start(
        //        new ProcessStartInfo("cmd", $"/c start {url}")
        //        {
        //            CreateNoWindow = true
        //        });
        //}

        public static void OpenBrowser(string url)
        {
            System.Diagnostics.Process.Start("cmd", "/C start http://google.com");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                // throw 
            }
        }
    }
}