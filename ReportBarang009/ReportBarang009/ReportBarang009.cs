using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ReportBarang009
{
    public partial class ReportBarang009 : Form
    {
        public ReportBarang009()
        {
            InitializeComponent();
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            exportgridtopdf(dgView, "text");
        }

        private void ReportBarang009_Load(object sender, EventArgs e)
        {
            exportgridtoexcel(dgView, "coba");
        }
        public void exportgridtopdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            //header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);

            }

            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }

            }

            var safefiledialoge = new SaveFileDialog();
            safefiledialoge.FileName = filename;
            safefiledialoge.DefaultExt = ".Pdf";
            if (safefiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(safefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }
        public void exportgridtoexcel(DataGridView dgw, string filename)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "detail Data";

            for (int i = 1; i < dgView.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgView.Columns[i - 1].HeaderText;

            }
            for (int i = 0; i < dgView.Rows.Count; i++)
            {
                for (int j = 0; j < dgView.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgView.Rows[i].Cells[j].Value.ToString();

                }
            }

            //save excel
            var savedialogexcel = new SaveFileDialog();
            savedialogexcel.FileName = "output Excel";
            savedialogexcel.DefaultExt = ".xlsx";

            if (savedialogexcel.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(savedialogexcel.FileName, Type.Missing, Type.Missing, "Aris Homo");
            }

            app.Quit();

        }
    }
}
