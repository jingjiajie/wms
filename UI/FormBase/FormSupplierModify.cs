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
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormSupplierModify(int supplierID = -1)
        {
            InitializeComponent(); 
            this.supplierID = supplierID;
        }

        private void FormSupplierModify_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            if (this.mode == FormMode.ALTER&&this.supplierID == -1)
            { 
                throw new Exception("未设置源库存信息");
            }
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < SupplierMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = SupplierMetaData.KeyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false) //&& curKeyName.Name != "ID")
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanel1.Controls.Add(textBox);
            }
            if (this.mode == FormMode.ALTER)
            {
                SupplierView SupplierView = (from s in this.wmsEntities.SupplierView
                                       where s.ID == this.supplierID
                                        select s).Single();
                Utilities.CopyPropertiesToTextBoxes(SupplierView, this);
            }
       

        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            TextBox textBoxSupplierName = (TextBox)this.Controls.Find("textBoxName",true)[0];
            if (textBoxSupplierName.Text != String.Empty)
            {

                Supplier supplier = null;

                //若修改，则查询原对象。若添加，则新建对象。
                if (this.mode == FormMode.ALTER)
                {
                    supplier = (from s in this.wmsEntities.Supplier
                                where s.ID == this.supplierID
                                select s).Single();
                }
                else if (mode == FormMode.ADD)
                {
                    supplier = new Supplier();
                    this.wmsEntities.Supplier.Add(supplier);
                }
                //开始数据库操作
                if (Utilities.CopyTextBoxTextsToProperties(this, supplier, SupplierMetaData.KeyNames, out string errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                wmsEntities.SaveChanges();
                //调用回调函数
                if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
                {
                    this.modifyFinishedCallback();
                }
                else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
                {
                    this.addFinishedCallback();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("供应商名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


        }

        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
        }
        public void SetAddFinishedCallback(Action callback)
        {
            this.addFinishedCallback = callback;
        }


        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改库存信息";
                this.buttonModify.Text = "修改库存信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加库存信息";
                this.buttonModify.Text = "添加库存信息";
            }
        }
    }
}
