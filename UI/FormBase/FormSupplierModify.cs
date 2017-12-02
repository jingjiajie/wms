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
    public partial class FormSupplierModify : Form
    {
        private int supplierID = -1; 
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        public FormSupplierModify(int supplierID)
        {
            InitializeComponent();
            this.supplierID = supplierID;
        }

        private void FormSupplierModify_Load(object sender, EventArgs e)
        {
            if (this.supplierID == -1)
            { 
                throw new Exception("未设置源库存信息");
            }
            Supplier supplier = (from s in this.wmsEntities.Supplier
                                   where s.ID == this.supplierID
                                   select s).Single();
            Utilities.CopyPropertiesToTextBoxes(supplier, this);
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (this.supplierID == -1)
            {
                throw new Exception("未设置源供应商信息");
            }
            var wmsEntities = new WMSEntities();
            Supplier supplier = (from s in wmsEntities.Supplier where s.ID == this.supplierID select s).Single();
            string errorMessage = null;


            if (Utilities.CopyTextBoxTextsToProperties(this, supplier, SupplierInfoMetaData.KeyNames, out errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //更新数据库
            wmsEntities.SaveChanges();
            if (this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback(); //调用修改完成的回调函数
            }
            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
        }
    }
}
