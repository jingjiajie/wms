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
        bool loadFinished = false;
        float defaultPrintScale = 1;

        public StandardFormPreviewExcel(string formTitle,float printScale = 1.0F)
        {
            InitializeComponent();
            this.Text = formTitle;
            this.SetPrintScale(printScale);
        }

        public void SetPrintScale(float scale)
        {
            if (this.loadFinished == false)
            {
                this.defaultPrintScale = scale;
            }
            else
            {
                var worksheet = this.reoGridControlMain.CurrentWorksheet;
                worksheet.PrintSettings.PageScaling = scale;
                worksheet.AutoSplitPage();
                this.textBoxScale.Text = scale.ToString();
            }
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
                MessageBox.Show("生成报表错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            worksheet.EnableSettings(WorksheetSettings.View_ShowPageBreaks);
            worksheet.SetSettings(WorksheetSettings.Behavior_AllowUserChangingPageBreaks, true);
            loadFinished = true;
            this.SetPrintScale(this.defaultPrintScale);
        }

        public bool SetPatternTable(byte[] patternTableExcelFile)
        {
            ReoGridControl tmpReoGrid = new ReoGridControl();
            tmpReoGrid.Load(new MemoryStream(patternTableExcelFile), unvell.ReoGrid.IO.FileFormat.Excel2007);
            this.excelGenerator.SetPatternTable(tmpReoGrid.Worksheets[0]);
            return true;
        }

        public bool SetPatternTable(string filePath)
        {
            ReoGridControl tmpReoGrid = new ReoGridControl();
            try
            {
                tmpReoGrid.Load(filePath);
            }catch(Exception e)
            {
                MessageBox.Show("加载Excel文件失败！详细信息：\n"+e.Message);
                return false;
            }
            this.excelGenerator.SetPatternTable(tmpReoGrid.Worksheets[0]);
            return true;
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

        private void textBoxScale_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(this.textBoxScale.Text,out float printScale) == false)
            {
                return;
            }
            this.SetPrintScale(printScale);
        }

        private void textBoxScale_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxScale_Click(object sender, EventArgs e)
        {
            this.textBoxScale.SelectAll();
        }

        private void textBoxScale_KeyDown(object sender, KeyEventArgs e)
        {
            if (float.TryParse(this.textBoxScale.Text, out float printScale) == false)
            {
                return;
            }
            if(printScale <= 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
            {
                printScale += 0.1F;
                this.textBoxScale.Text = string.Format("{0:0.##}",printScale);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
            {
                printScale -= 0.1F;
                this.textBoxScale.Text = string.Format("{0:0.##}", printScale);
            }
        }

        private void textBoxScale_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}