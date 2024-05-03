using System;
using System.Collections.Generic;
using System.Data;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Valvetwebb.Aktivitet;
using Valvetwebb.Objekt;
using System.IO;
using System.Drawing;
using iText.Layout;

namespace Valvetwebb.Kontroller
{
    public static class PDFLista
    {
        private static string PDFFileName { get; set; }

        private static float f;

        private static string stringstrAttachment;

        private static Stream workStream;

        public static object PdfPTabletableLayout { get; set; }

        public static object tableLayout { get; set; }

        public static Anvandare WebUser { get;set; }

        private static List<ValvPost> GetData()
        {
            List<ValvPost> valvpostList = null;
            ValvPostAktivitet ValvpostAktivitet = new ValvPostAktivitet();
            valvpostList = ValvpostAktivitet.SökValvPost(WebUser.Konto, string.Empty);

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("PostID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Postnummer", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Postnamn", typeof(string)));
            int postnummer = 0;

            if (valvpostList.Count > 0)
            {
                foreach (ValvPost valvpost in valvpostList)
                {
                    postnummer++;
                    dr = dt.NewRow();
                    dr[0] = valvpost.PostID;
                    dr[1] = postnummer;
                    dr[2] = valvpost.Postnamn;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
            }

            DataView dv = new DataView(dt);
            return valvpostList;
        }


        public static void CreatePdf()
        {
            //List<ValvPost> valvpostList = GetData();

            //Document document = new Document();
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"C:\\Mina program\\Valvet\\" + "ValvetLista.pdf", FileMode.Create));
            //DataTable dataTable = GenereraDataTable(valvpostList);

            //var header = new PDFFooter();
            //document.Open();
            //writer.PageEvent = header;
            //header.HeaderText = "Valvlista";
            //iTextSharp.text.Font fontH = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 7, 2);
            //iTextSharp.text.Font fontP = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);
            //iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

            //fontH.Color = BaseColor.GRAY;

            //PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            //table.HeaderRows = 1; /*---->> this property repeats the headers of an iTextSharp PdfPTable on each page */
            //PdfPRow row = null;
            //float[] widths = new float[dataTable.Columns.Count];
            //for (int i = 0; i < dataTable.Columns.Count; i++)
            //    widths[i] = 4f;

            //table.SetWidths(widths);
            //table.WidthPercentage = 100;
            //int iCol = 0;
            //string colname = "";
            //PdfPCell cell = new PdfPCell(new Phrase("Valvposter"));

            //cell.Colspan = dataTable.Columns.Count;

            //foreach (DataColumn c in dataTable.Columns)
            //{

            //    table.AddCell(new Phrase(c.ColumnName, fontH));
            //}

            //if (valvpostList.Count > 0)
            //{
            //    foreach (ValvPost valvpost in valvpostList)
            //    {
            //        table.AddCell(new Phrase(valvpost.Postnamn.ToString(), fontP));
            //        table.AddCell(new Phrase(valvpost.Usernamn.ToString(), font5));
            //        table.AddCell(new Phrase(valvpost.Losenord.ToString(), font5));
            //        table.AddCell(new Phrase(valvpost.Anteckningar.ToString(), font5));
            //    }
            //}

            //document.Add(table);
            //document.Close();
        }

        private static DataTable GenereraDataTable(List<ValvPost> dt)
        {
            var table = new DataTable();
            var columns = table.Columns;
            columns.Add("Postnamn", typeof(string));
            columns.Add("Usernamn", typeof(string));
            columns.Add("Losenord", typeof(string));
            columns.Add("Anteckningar", typeof(string));
            return table;
        }
    }
}