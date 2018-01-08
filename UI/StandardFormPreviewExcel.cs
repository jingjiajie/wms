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

namespace WMS.UI
{
    public partial class StandardFormPreviewExcel : Form
    {
        ExcelGenerator excelGenerator = new ExcelGenerator();

        public StandardFormPreviewExcel(string formTitle)
        {
            InitializeComponent();
            this.Text = formTitle;
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
    }
}