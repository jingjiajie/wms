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

namespace WMS.UI.FormBase
{
    public partial class FormBaseWarehouseModify : Form
    {
        private int warehouseID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action<int> modifyFinishedCallback = null;
        private Action<int> addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormBaseWarehouseModify(int warehouseID = -1)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void FormBaseWarehouseModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.warehouseID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 1; i < BaseWarehouseMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = BaseWarehouseMetaData.KeyNames[i];
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                this.tableLayoutPanel1.Controls.Add(textBox);
            }
            if (this.mode == FormMode.ALTER)
            {
                try
                {

                    Warehouse Warehouse = (from s in this.wmsEntities.Warehouse
                                           where s.ID == this.warehouseID
                                           select s).Single();
                    Utilities.CopyPropertiesToTextBoxes(Warehouse, this);
                }
                catch (Exception)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
        }


        public void SetModifyFinishedCallback(Action<int> callback)
        {
            this.modifyFinishedCallback = callback;
        }
        public void SetAddFinishedCallback(Action<int> callback)
        {
            this.addFinishedCallback = callback;
        }

        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改仓库信息";
                this.buttonModify.Text = "修改仓库信息";
                this.groupBox1.Text = "修改仓库信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加仓库信息";
                this.buttonModify.Text = "添加仓库信息";
                this.groupBox1.Text = "添加仓库信息";
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            var textBoxName = this.Controls.Find("textBoxName", true)[0];
            if (textBoxName.Text == string.Empty)
            {
                MessageBox.Show("仓库名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Warehouse warehouse = null;

            //若修改，则查询原对象。若添加，则新建对象。
            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    warehouse = (from s in this.wmsEntities.Warehouse
                                 where s.ID == this.warehouseID
                                 select s).Single();
                }
                catch
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (warehouse == null)
                {
                    MessageBox.Show("项目不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
            else if (mode == FormMode.ADD)
            {
                warehouse = new Warehouse();
                this.wmsEntities.Warehouse.Add(warehouse);
            }
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, warehouse, BaseWarehouseMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
         try
            {
                wmsEntities.SaveChanges();
            }
            catch (Exception)
            {
                if (this.mode == FormMode.ALTER)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (mode == FormMode.ADD)
                {
                    MessageBox.Show("添加失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
            //调用回调函数

            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback(warehouse.ID);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback(warehouse.ID);
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void buttonModify_MouseEnter(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonModify_MouseLeave(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonModify_MouseDown(object sender, MouseEventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
