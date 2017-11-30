using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Reflection;

namespace WMS.UI
{
    public partial class FormStockInfoModify : Form
    {
        private StockInfo stockInfo = null;

        public FormStockInfoModify(StockInfo stockInfo)
        {
            InitializeComponent();
            this.stockInfo = stockInfo;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FormStockInfoModify_Load(object sender, EventArgs e)
        {
            if (this.stockInfo == null)
            {
                throw new Exception("未设置源库存信息对象");
            }
            PropertyInfo[] stockInfoProperties = this.stockInfo.GetType().GetProperties();
            foreach(PropertyInfo p in stockInfoProperties)
            {
                Control[] foundControls = this.Controls.Find("textBox" + p.Name,true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                TextBox curTextBox = (TextBox)foundControls[0];
                object value = p.GetValue(this.stockInfo, null);
                curTextBox.Text = value == null ? "" : value.ToString();
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (this.stockInfo == null)
            {
                throw new Exception("未设置源库存信息对象");
            }
            PropertyInfo[] stockInfoProperties = this.stockInfo.GetType().GetProperties();
            foreach (PropertyInfo p in stockInfoProperties)
            {
                Control[] foundControls = this.Controls.Find("textBox" + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                TextBox curTextBox = (TextBox)foundControls[0];
                //根据源类型不同，将编辑框中的文本转换成合适的类型
                object originValue = p.GetValue(this.stockInfo, null);
                if (originValue is String)
                {
                    p.SetValue(this.stockInfo, curTextBox.Text, null);
                }else if (originValue is int)
                {
                    try
                    {
                        p.SetValue(this.stockInfo, int.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        MessageBox.Show("编辑框textBox"+p.Name+"只接受整数值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }else if(originValue is double)
                {
                    try
                    {
                        p.SetValue(this.stockInfo, int.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        MessageBox.Show("编辑框textBox" + p.Name + "只接受小数值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }else if (originValue is DateTime)
                {
                    try
                    {
                        p.SetValue(this.stockInfo, DateTime.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        MessageBox.Show("编辑框textBox" + p.Name + "只接受日期字符串(年-月-日)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("内部错误：未知源类型","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //更新数据库
            var wmsEntities = new WMSEntities();
            wmsEntities.SaveChanges();
            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
