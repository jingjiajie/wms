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
        private int stockInfoID = -1;
        private WMSEntities wmsEntities = new WMSEntities();

        public FormStockInfoModify(int stockInfoID)
        {
            InitializeComponent();
            this.stockInfoID = stockInfoID;
        }

        private void FormStockInfoModify_Load(object sender, EventArgs e)
        {
            if (this.stockInfoID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            StockInfo stockInfo = (from s in this.wmsEntities.StockInfo
                                   where s.ID == this.stockInfoID
                                   select s).Single();
            Utilities.CopyPropertiesToTextBoxes(stockInfo, this);
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (this.stockInfoID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            var wmsEntities = new WMSEntities();
            StockInfo stockInfo = (from s in wmsEntities.StockInfo where s.ID == this.stockInfoID select s).Single();
            PropertyInfo[] stockInfoProperties = typeof(StockInfo).GetProperties();
            foreach (PropertyInfo p in stockInfoProperties)
            {
                Control[] foundControls = this.Controls.Find("textBox" + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                TextBox curTextBox = (TextBox)foundControls[0];
                if (curTextBox.Text.Length == 0)
                {
                    try
                    {
                        p.SetValue(stockInfo, null, null);
                        continue;
                    }
                    catch
                    {
                        MessageBox.Show("属性" + p.Name + "不允许为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //根据源类型不同，将编辑框中的文本转换成合适的类型
                Type originType = p.PropertyType;
                if (originType == typeof(String))
                {
                    p.SetValue(stockInfo, curTextBox.Text, null);
                }else if (originType == typeof(int?) || originType == typeof(int))
                {
                    try
                    {
                        p.SetValue(stockInfo, int.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        MessageBox.Show("编辑框textBox"+p.Name+"只接受整数值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }else if(originType == typeof(decimal) || originType == typeof(decimal?))
                {
                    try
                    {
                        p.SetValue(stockInfo, decimal.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        MessageBox.Show("编辑框textBox" + p.Name + "只接受小数值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }else if (originType == typeof(DateTime?) || originType == typeof(DateTime))
                {
                    try
                    {
                        p.SetValue(stockInfo, DateTime.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        MessageBox.Show("编辑框textBox" + p.Name + "只接受日期字符串(年-月-日)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("内部错误：未知源类型"+ originType, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //更新数据库
            wmsEntities.SaveChanges();
            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
