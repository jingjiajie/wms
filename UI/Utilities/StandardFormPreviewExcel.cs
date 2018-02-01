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
using System.Drawing.Printing;

namespace WMS.UI
{
    public partial class StandardFormPreviewExcel : Form
    {
        ExcelGenerator excelGenerator = new ExcelGenerator();
        bool loadFinished = false;

        Dictionary<string, float> defaultPrintScales = new Dictionary<string, float>();
        Dictionary<string, Worksheet> patternTables = new Dictionary<string, Worksheet>();
        Dictionary<string, Dictionary<string, object>> data = new Dictionary<string, Dictionary<string, object>>();

        public StandardFormPreviewExcel(string formTitle, float printScale = 1.0F)
        {
            InitializeComponent();
            this.Text = formTitle;
            this.SetPrintScale(printScale);
        }

        public void SetPrintScale(float scale, string worksheetName = "sheet1")
        {
            if (this.loadFinished == false)
            {
                if (this.defaultPrintScales.ContainsKey(worksheetName))
                {
                    this.defaultPrintScales[worksheetName] = scale;
                }
                else
                {
                    this.defaultPrintScales.Add(worksheetName, scale);
                }
            }
            else
            {
                foreach (var worksheet in this.reoGridControlMain.Worksheets)
                {
                    if (worksheet.Name == worksheetName)
                    {
                        if (worksheet.PrintSettings.PageScaling == scale) break; //如果放大倍数没变，就不用再调整一次了
                        worksheet.PrintSettings.PageScaling = scale;
                        worksheet.AutoSplitPage();
                        break;
                    }
                }
                this.textBoxScale.Text = this.reoGridControlMain.CurrentWorksheet.PrintSettings.PageScaling.ToString();
            }
        }

        private void StandardFormPreviewExcel_Load(object sender, EventArgs e)
        {
            loadFinished = true;
            try
            {
                this.reoGridControlMain.Worksheets.Clear();
                foreach (var item in this.patternTables)
                {
                    string sheetName = item.Key;
                    Worksheet patternTable = item.Value;
                    this.excelGenerator.SetPatternTable(patternTable);
                    if (this.data.ContainsKey(sheetName))
                    {
                        foreach (var datum in this.data[sheetName])
                        {
                            this.excelGenerator.AddData(datum.Key, datum.Value);
                        }
                    }
                    Worksheet newWorksheet = this.excelGenerator.Generate();
                    newWorksheet.Name = sheetName;
                    newWorksheet.EnableSettings(WorksheetSettings.View_ShowPageBreaks);
                    newWorksheet.SetSettings(WorksheetSettings.Behavior_AllowUserChangingPageBreaks, true);
                    this.reoGridControlMain.Worksheets.Add(newWorksheet);
                    if (this.defaultPrintScales.ContainsKey(sheetName))
                    {
                        this.SetPrintScale(this.defaultPrintScales[sheetName], sheetName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成报表错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public bool AddPatternTable(string filePath, string sheetName = "sheet1")
        {
            if (this.patternTables.ContainsKey(sheetName))
            {
                return false;
            }
            ReoGridControl tmpReoGrid = new ReoGridControl();
            try
            {
                tmpReoGrid.Load(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("加载Excel文件失败！详细信息：\n" + e.Message);
                return false;
            }
            this.patternTables.Add(sheetName, tmpReoGrid.Worksheets[0]);
            return true;
        }

        public bool SetPatternTable(string filePath)
        {
            return this.AddPatternTable(filePath);
        }

        public void AddData<T>(string name, T data, string sheetName = "sheet1")
        {
            if (this.data.ContainsKey(sheetName) == false)
            {
                this.data.Add(sheetName, new Dictionary<string, object>());
            }
            if (this.data[sheetName].ContainsKey(name) == false)
            {
                this.data[sheetName].Add(name, data);
            }
            else
            {
                this.data[sheetName][name] = data;
            }
        }

        private void StandardFormPreviewExcel_Shown(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel文件|.xlsx";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string filePath = dialog.FileName;
            this.reoGridControlMain.Save(filePath);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintDocument doc = null;
            try
            {
                doc = this.reoGridControlMain.CreatePrintSession().PrintDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("Custom", (int)(9.448 * 100),(int)(5.511 * 100));
            //doc.PrinterSettings.DefaultPageSettings.PaperSize = ps;
            //doc.DefaultPageSettings.PaperSize = ps;
            doc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.Document = doc;
            pageSetupDialog.ShowDialog();

            //PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            //printPreviewDialog.Document = doc;
            //printPreviewDialog.ShowDialog();

            using (var pd = new System.Windows.Forms.PrintDialog())
            {
                pd.Document = doc;
                pd.UseEXDialog = true;  // in 64bit Windows

                if (pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    doc.PrinterSettings = pd.PrinterSettings;
                    try
                    {
                        doc.Print();
                    }
                    catch
                    {
                        if (doc != null) doc.Dispose();
                    }
                }
            }

            if (doc != null) doc.Dispose();
        }

        private void textBoxScale_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(this.textBoxScale.Text, out float printScale) == false)
            {
                return;
            }
            this.SetPrintScale(printScale, this.reoGridControlMain.CurrentWorksheet.Name);
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
            if (printScale <= 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
            {
                printScale += 0.1F;
                this.textBoxScale.Text = string.Format("{0:0.##}", printScale);
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

        private void reoGridControlMain_CurrentWorksheetChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBoxScale.Text = this.reoGridControlMain.CurrentWorksheet.PrintSettings.PageScaling.ToString();
            }
            catch
            {
                //如果已经关闭窗口，这里会抛异常。直接返回即可
            }
        }
    }
}