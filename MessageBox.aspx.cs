using System;
using System.Text;

namespace Valvetwebb
{
    public partial class MessageBox : PageBase
    {
        private string NavigateUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Navigation"] == null && Session["Referencepage"].ToString() != "LogIn.aspx" )
            {
                Redirect("~/LogIn.aspx");
            }

            NavigateUrl = Session["Referencepage"].ToString();
            knappTabort.Visible = false;
            knappAvbryt.Visible = false;
            string buttons = Session["Buttons"].ToString();

            if (!IsPostBack)
            {
                VisaKnappar();
                VisaMessage();
            }
        }

        private void VisaMessage()
        {
            StringBuilder headertext = new StringBuilder();

            try
            {
                Titel.Text = Session["MessageTitle"].ToString();
                headertext.Append(Session["MessageText"].ToString());
                lblMessagetext.Text = headertext.ToString();
                //lblMessagetext.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                //Logging.WriteLog("E", "WebUser: " + WebUser.UserId + "\n\n" + methodName +
                //    " misslyckades för ProductID:" + WebUser.ProductId + "\n\n" + ex.Message, 1006);
                throw ex;
            }
        }

        private void VisaKnappar()
        {
            knappTabort.Visible = false;
            knappAvbryt.Visible = false;

            if (Session["Buttons"].ToString() == "Ja/Nej")
            {
                knappOK.Text = "Ja";
                knappTabort.Visible = true;
            }
        }


        protected void knappOK_Click(object sender, EventArgs e)
        {
            //Session["Referencepage"] = "MessageBox.aspx";
            Response.Redirect(NavigateUrl);
        }

        protected void knappTabort_Click(object sender, EventArgs e)
        {
            //Session["Referencepage"] = "MessageBox.aspx";
            Response.Redirect(NavigateUrl);
        }

        protected void knappAvbryt_Click(object sender, EventArgs e)
        {
            //Session["Referencepage"] = "MessageBox.aspx";
            Response.Redirect(NavigateUrl);
        }
    }
}