using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI.FormBase
{
    public partial class FormBaseComponenAdd : Form
    {
        public FormBaseComponenAdd()
        {
            InitializeComponent();
        }

        private void FormBaseComponenAdd_Load(object sender, EventArgs e)
        {

        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (ID.Text == string.Empty)
            {
                MessageBox.Show("零件ID不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (WarehouseID.Text == string.Empty)
            {
                MessageBox.Show("仓库ID不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (SupplierID.Text == string.Empty)
            {
                MessageBox.Show("供应商ID不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //添加
            DataAccess.Component objuser = new DataAccess.Component();
            {
                objuser.ID = Convert.ToInt32(ID.Text);
                objuser.WarehouseID = Convert.ToInt32(WarehouseID.Text);
                objuser.SupplierID = Convert.ToInt32(SupplierID.Text);
                objuser.ContainerNo = ContainerNo.Text;
                objuser.Factroy = Factroy.Text;
                objuser.WorkPosition = WorkPosition.Text;
                objuser.No = No.Text;
                objuser.Name = Name1.Text;
                objuser.SupplierType = SupplierType.Text;
                objuser.Type = Type.Text;
                objuser.Size = Size.Text;
                objuser.Category = Category.Text;
                objuser.GroupPrincipal = GroupPrincipal.Text;
                objuser.SingleCarUsageAmount = Convert.ToDecimal(SingleCarUsageAmount.Text);
                objuser.ChargeBelow50000 = Convert.ToDecimal(ChargeBelow50000.Text);
                objuser.ChargeAbove50000 = Convert.ToDecimal(ChargeAbove50000.Text);
                objuser.InventoryRequirement1Day = Convert.ToDecimal(InventoryRequirement1Day.Text);
                objuser.InventoryRequirement3Day = Convert.ToDecimal(InventoryRequirement3Day.Text);
                objuser.InventoryRequirement5Day = Convert.ToDecimal(InventoryRequirement5Day.Text);
                objuser.InventoryRequirement10Day = Convert.ToDecimal(InventoryRequirement10Day.Text);

            }
            WMSEntities wms = new WMSEntities();
            wms.Component.Add(objuser);
            wms.SaveChanges();
            MessageBox.Show("添加零件成功");
            this.Close();
        }


        private void buttonClosing_Click(object sender, EventArgs e)
        {
            this.Close();
        }//关闭窗口
    }
}
