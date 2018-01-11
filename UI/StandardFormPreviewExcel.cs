using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMacro;
using System.IO;

using unvell.ReoGrid;
using unvell.ReoGrid.Print;

namespace WMS.UI
{
    public partial class StandardFormPreviewExcel : Form
    {
        ExcelGenerator excelGenerator = new ExcelGenerator();
        float printScale = 1;

        public StandardFormPreviewExcel(string formTitle,float printScale = 1.0F)
        {
            InitializeComponent();
            this.Text = formTitle;
            this.printScale = printScale;
        }

        public void SetPrintScale(float scale)
        {
            this.printScale = scale;
        }

        private void StandardFormPreviewExcel_Load(object sender, EventArgs e)
        {
            try
            {
                this.reoGridControlMain.Worksheets.Clear();
                this.reoGridControlMain.Worksheets.Add(this.excelGenerator.Generate());
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成报表错误：" + ex.Message,"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            worksheet.EnableSettings(WorksheetSettings.View_ShowPageBreaks);
            worksheet.SetSettings(WorksheetSettings.Behavior_AllowUserChangingPageBreaks, true);
            worksheet.PrintSettings.PageScaling = this.printScale;
            worksheet.AutoSplitPage();
        }

        public void SetPatternTable(byte[] patternTableExcelFile)
        {
            ReoGridControl tmpReoGrid = new ReoGridControl();
            tmpReoGrid.Load(new MemoryStream(patternTableExcelFile), unvell.ReoGrid.IO.FileFormat.Excel2007);
            this.excelGenerator.SetPatternTable(tmpReoGrid.Worksheets[0]);
        }

        public void AddData<T>(string name, T data)
        {
            this.excelGenerator.AddData(name,data);
        }

        private void StandardFormPreviewExcel_Shown(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel文件|.xlsx";
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            this.reoGridControlMain.Save(filePath);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;

            System.Drawing.Printing.PrintDocument doc = null;
            try
            {
                doc = worksheet.CreatePrintSession().PrintDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var pd = new System.Windows.Forms.PrintDialog())
            {
                pd.Document = doc;
                pd.UseEXDialog = true;  // in 64bit Windows

                if (pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    doc.PrinterSettings = pd.PrinterSettings;
                    doc.Print();
                }
            }

            if (doc != null) doc.Dispose();
        }
    }
}