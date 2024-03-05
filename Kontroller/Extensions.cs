using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Valvetwebb.Kontroller
{
    public static class Extensions
    {
        public static IEnumerable<DropDownList> DropDownLists(this ControlCollection controlCollection)
        {
            return controlCollection.OfType<DropDownList>();
        }

        public static IEnumerable<T> GetControlList<T>(this ControlCollection controlCollection) where T : Control
        {
            foreach (Control control in controlCollection)
            {
                if (control is T)
                {
                    yield return (T)control;
                }

                if (control.HasControls())
                {
                    foreach (T childControl in control.Controls.GetControlList<T>())
                    {
                        yield return childControl;
                    }
                }
            }
        }

        /// <summary>
        /// Skicka mail
        /// <paramref name="Mailet">Det mail som ska skickas</paramref>/>
        /// </summary>
        //public static string Skicka_Mail(this Hooker.Affärsobjekt.Mail Mailet)
        //{
        //    string resultat = string.Empty;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    try
        //    {
        //        var fromAddress = new MailAddress(Mailet.MailFrom);
        //        var fromPassword = Mailet.Password;
        //        var toAddress = new MailAddress(Mailet.MailTo);

        //        string subject = Mailet.Subject;
        //        string body = Mailet.Body;

        //        MailMessage mailMessage = new MailMessage(fromAddress.Address,
        //            toAddress.Address);

        //        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient
        //        {
        //            UseDefaultCredentials = false,
        //            Host = Mailet.SmtpHost,
        //            Port = Mailet.Port,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //        };

        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
        //            SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        mailMessage.IsBodyHtml = Mailet.IsHTML;

        //        client.Send(mailMessage);
        //        return resultat = "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message.ToString();
        //        //return ex.Message.ToString() + Environment.NewLine + ex.InnerException.ToString();
        //    }
        //}

        /// <summary>
        /// Veckodag för datum
        /// </summary>
        /// <param name="datum"></param>
        /// <returns>Veckodag</returns>
        public static string Veckodag(this DateTime datum)
        {
            string veckodag = datum.DayOfWeek.ToString();
            return veckodag;
        }

        /// <summary>
        /// Dagnummer för datum
        /// </summary>
        /// <param name="datum"></param>
        /// <returns>Dagnummer</returns>
        public static int Dagnummer(this DateTime datum)
        {
            int dagnummer = (int)datum.DayOfWeek;
            return dagnummer;
        }

        /// <summary>
        /// Messagebox med text och OK knapp
        /// </summary>
        /// <param name="page"></param>
        /// <param name="strMsg"></param>
        public static void MessageBoxOKButton(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);
            //Response.Write("<script>alert('Uppdateringen lyckades');</script>");
        }

        public static void MessageBoxYesNoButton(System.Web.UI.Page page, string text)
        {

        }
    }
}