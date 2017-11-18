using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WMS.TableGenerate;
using unvell.ReoGrid;

namespace WMS.UI
{
    public partial class FormExcelGenerate : Form
    {
        public FormExcelGenerate()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            //打开选取文件对话框，让用户选择文件

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel文件|*.xlsx;*.xls";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFilePath.Text = openFileDialog.FileName;
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            //打开保存文件对话框，用户选取保存位置
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            if(saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string outputFilePath = saveFileDialog.FileName; //输入文件路径
            string inputFilePath = this.textBoxFilePath.Text; //输出文件路径

            //新建Reogrid的工作簿
            var patternTableWorkbook = new ReoGridControl();

            //从输入文件路径读取模式表
            try
            {
                patternTableWorkbook.Load(inputFilePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show("生成失败：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //取了工作簿中的第一个表作为模式表
            var patternTable = patternTableWorkbook.Worksheets[0];
            
            //新建一个表格生成器对象
            TableGenerator tg = new TableGenerator(patternTable,null);

            ////生成目标表
            //Table result;
            //string errMsg = tg.TryGenerateTable(out result);
            //if (result == null)
            //{
            //    MessageBox.Show("生成失败：" + errMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            var result = tg.GenerateTable();

            //新建ReoGrid工作簿并清空工作簿
            var resultTableWorkbook = new ReoGridControl();
            resultTableWorkbook.Worksheets.Clear();

            //将结果表加入到新的工作簿
            resultTableWorkbook.Worksheets.Add(result);

            //将工作簿存储到输出文件路径
            resultTableWorkbook.Save(outputFilePath);

            //提示成功
            MessageBox.Show("生成成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}