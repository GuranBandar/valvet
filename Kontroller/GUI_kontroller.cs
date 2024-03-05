using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Valvetwebb.Kontroller
{
    public class GUI_kontroller
    {
        List<DropDownList> allControls;
        List<TextBox> txtControls;
        Style style;

        public object ClientScript { get; private set; }

        /// <summary>
        /// Alla textboxar fixas, enabled eller inte
        /// </summary>
        /// <param name="controlCollection"></param>
        /// <param name="enable"></param>
        public void LoopTextboxes(ControlCollection controlCollection, bool enable)
        {
            txtControls = new List<TextBox>();
            GetControlList<TextBox>(controlCollection, txtControls);

            foreach (var textControl in txtControls)
            {
                textControl.Text = string.Empty;
            }
        }

        /// <summary>
        /// Hämta kontroller ur ControlCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlCollection"></param>
        /// <param name="resultCollection"></param>
        public void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
            where T : Control
        {
            foreach (Control control in controlCollection)
            {
                //if (control.GetType() == typeof(T))
                if (control is T) 
                    resultCollection.Add((T)control);

                if (control.HasControls())
                    GetControlList(control.Controls, resultCollection);
            }
        }

        ///// <summary>
        ///// Tabellen BokningsLista initieras om ny Bokning.
        ///// </summary>
        //public void InitieraBokningsLista(BokningDag bokningDag)
        //{
        //    BokningsLista bokningsLista;
        //    for (int i = 0; i < 12; i++)
        //    {
        //        bokningsLista = new BokningsLista();
        //        bokningsLista.BokningID = bokningDag.BokningID;
        //        bokningsLista.BollNr = (i + 1).ToString();
        //        bokningsLista.SpelareNamn = string.Empty;
        //        bokningDag.AddBokningsLista(bokningsLista);
        //    }
        //}

        ///// <summary>
        ///// Hämta spelarens namn i dropdownlistan för bollarna och addera de till
        ///// BokningsListan
        ///// </summary>
        ///// <param name="controlCollection"></param>
        ///// <param name="bokningDag">BokningDag</param>
        //public void BollTextboxes(ControlCollection controlCollection,
        //    BokningDag bokningDag)
        //{
        //    int radNr = 0;
        //    BokningsLista bokningsLista = new BokningsLista();

        //    allControls = new List<DropDownList>();
        //    GetControlList<DropDownList>(controlCollection, allControls);
        //    foreach (var childControl in allControls)
        //    {
        //        childControl.DataBind();
        //        radNr++;
        //        bokningsLista = new BokningsLista();
        //        bokningsLista.BokningID = bokningDag.BokningID;
        //        bokningsLista.BollNr = radNr.ToString();
        //        bokningsLista.SpelareNamn = childControl.SelectedValue.ToString();
        //        bokningDag.AddBokningsLista(bokningsLista);
        //    }
        //}
        
        ///// <summary>
        ///// Hämta alla Portugalgolfare och fyll combon.
        ///// </summary>
        //public void FyllSpelareCombo(ControlCollection controlCollection)
        //{
        //    SpelareAktivitet spelareAktivitet = new SpelareAktivitet();
        //    allControls = new List<DropDownList>();
        //    style = new Style();

        //    try
        //    {
        //        List<Spelare> spelarLista = spelareAktivitet.HämtaPortugalgolfare("1");
        //        if (spelarLista.Count > 0)
        //        {
        //            GetControlList<DropDownList>(controlCollection, allControls);
        //            foreach (var childControl in allControls)
        //            {
        //                childControl.Items.Clear();
        //                for (int i = 0; i < spelarLista.Count; i++)
        //                {
        //                    childControl.Items.Add(new ListItem(spelarLista[i].Namn));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Visa spelarens namn i dropdownListan för bollarna
        ///// </summary>
        ///// <param name="controlCollection"></param>
        ///// <param name="bokningDag">Bokningslistan</param>
        //public void SpelareTextboxes(ControlCollection controlCollection, BokningDag bokningDag)
        //{
        //    int radNr = 0;

        //    allControls = new List<DropDownList>();
        //    GetControlList<DropDownList>(controlCollection, allControls);
        //    foreach (var childControl in allControls)
        //    {
        //        childControl.SelectedValue = bokningDag.bokningListas[radNr].SpelareNamn;
        //        childControl.BackColor = System.Drawing.Color.White;

        //        if (childControl.SelectedValue == " Ingen")
        //        {
        //            childControl.BackColor = System.Drawing.Color.LightGray;
        //        }
        //        radNr++;
        //    }
        //}

        /// <summary>
        /// Värdet i en DropDownList är ändrat
        /// </summary>
        /// <param name="dropDown"></param>
        /// <param name="selectedIndex"></param>
        public void DDLitemChanged(DropDownList dropDown, int selectedIndex)
        {
            dropDown.BackColor = System.Drawing.Color.White;

            if (dropDown.SelectedValue == " Ingen")
            {
                dropDown.BackColor = System.Drawing.Color.LightGray;
            }
       }

        public void MsgBox(string message, Page pg, Object obj)
        {

            //string message = "Order Placed Successfully.";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }
    }
}