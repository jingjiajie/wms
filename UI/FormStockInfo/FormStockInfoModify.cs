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
            string errorMessage = null;
            if(Utilities.CopyTextBoxTextsToProperties(this, stockInfo, StockInfoMetaData.KeyNames,out errorMessage) == false)
            {
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //更新数据库
            wmsEntities.SaveChanges();
            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
