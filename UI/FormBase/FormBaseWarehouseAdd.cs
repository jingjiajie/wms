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
    public partial class FormBaseWarehouseAdd : Form
    {
        public FormBaseWarehouseAdd()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == string.Empty)
            {
                MessageBox.Show("仓库名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //添加
            Warehouse obj = new Warehouse();
            {
                obj.Name = textBoxName.Text;
            }
            WMSEntities wms = new WMSEntities();
            wms.Warehouse.Add(obj);
            wms.SaveChanges();
            MessageBox.Show("添加仓库成功");
            this.Close();
        }

        private void buttonClosing_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBaseWarehouseAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
