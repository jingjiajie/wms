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
    public partial class FormComponenModify : Form
    {
        private int projectID = -1;
        private int warehouseID = -1;
        private int userID = -1;
        private int componenID = -1;
        private int supplierID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormComponenModify(int projectID, int warehouseID, int userID, int componenID = -1)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
            this.userID = userID;
            this.projectID = projectID;
            this.componenID = componenID;
        }
        
        private void FormComponenModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.componenID == -1)
            {
                throw new Exception("未设置源零件信息");
            }

            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ComponenMetaData.KeyNames);

            //this.tableLayoutPanelTextBoxes.Controls.Clear();
            //for (int i = 0; i < ComponenMetaData.componenkeyNames.Length; i++)
            //{
            //    KeyName curKeyName = ComponenMetaData.componenkeyNames[i];

            //    Label label = new Label();
            //    label.Text = curKeyName.Name;
            //    this.tableLayoutPanelTextBoxes.Controls.Add(label);

            //    TextBox textBox = new TextBox();
            //    textBox.Name = "textBox" + curKeyName.Key;
            //    if (curKeyName.Editable == false)
            //    {
            //        textBox.Enabled = false;
            //    }
            //    this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            //}

            if (this.mode == FormMode.ALTER)
            {
                ComponentView componenView = (from s in this.wmsEntities.ComponentView
                                              where s.ID == this.componenID
                                       select s).Single();
                Utilities.CopyPropertiesToTextBoxes(componenView, this);
            }
            //this.Controls.Find("textBoxProjectID", true)[0].TextChanged += textBoxProjectID_TextChanged;
            //this.Controls.Find("textBoxWarehouseID", true)[0].TextChanged += textBoxWarehouseID_TextChanged;
            //this.Controls.Find("textBoxSupplierName", true)[0].TextChanged += textBoxSupplierName_TextChanged;
        }
        private void textBoxSupplierName_TextChanged(object sender, EventArgs e)
        {
            var textBoxSupplierName = this.Controls.Find("textBoxSupplierName", true)[0];
            string supplierName = textBoxSupplierName.Text;
            try
            {
                Supplier supplierID = (from s in this.wmsEntities.Supplier where s.Name == supplierName select s).Single();
                this.supplierID = supplierID.ID;
            }
            catch
            {

            }
        }

        private void textBoxWarehouseID_TextChanged(object sender, EventArgs e)
        {
            var textBoxWarehouseID = this.Controls.Find("textBoxWarehouseID", true)[0];
            string warehouseID = textBoxWarehouseID.Text;
            int iWarehouseID;
            if (int.TryParse(warehouseID, out iWarehouseID) == false)
            {
                return;
            }
            try
            {
                Warehouse warehouseName = (from s in this.wmsEntities.Warehouse where s.ID == iWarehouseID select s).Single();
                this.Controls.Find("TextBoxWarehouseName", true)[0].Text = warehouseName.Name;
            }
            catch
            {

            }

        }

        private void textBoxProjectID_TextChanged(object sender, EventArgs e)
        {
            var textBoxProjectID = this.Controls.Find("textBoxProjectID", true)[0];
            string projectID = textBoxProjectID.Text;
            int iProjectID;
            if (int.TryParse(projectID, out iProjectID) == false)
            {
                return;
            }
            try
            {
                Project projectName = (from s in this.wmsEntities.Project where s.ID == iProjectID select s).Single();
                this.Controls.Find("TextBoxProjectName", true)[0].Text = projectName.Name;
            }
            catch
            {

            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            var textBoxSupplierName = this.Controls.Find("textBoxSupplierName", true)[0];
            if (textBoxSupplierName.Text == string.Empty)
            {
                MessageBox.Show("供应商名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string supplierName = textBoxSupplierName.Text;
            try
            {
                Supplier supplierID = (from s in this.wmsEntities.Supplier where s.Name == supplierName select s).Single();

                this.supplierID = supplierID.ID;
            }
            catch
            {

            }

            if (this.supplierID == -1)
            {
                MessageBox.Show("请输入正确的供应商名称！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataAccess.Component componen = null;           
            if (this.mode == FormMode.ALTER)
            {
                componen = (from s in this.wmsEntities.Component
                             where s.ID == this.componenID
                             select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                componen = new DataAccess.Component();
                this.wmsEntities.Component.Add(componen);
            }

            componen.ProjectID = this.projectID;
            componen.WarehouseID = this.warehouseID;
            componen.SupplierID = this.supplierID;

            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, componen, ComponenMetaData.componenkeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Utilities.CopyComboBoxsToProperties(this, componen, ComponenMetaData.KeyNames);
            }
            wmsEntities.SaveChanges();
            //调用回调函数
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if(this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback();
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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
            if(mode == FormMode.ALTER)
            {
                this.Text = "修改零件信息";
                this.buttonOK.Text = "修改零件信息";
            }else if (mode == FormMode.ADD)
            {
                this.Text = "添加零件信息";
                this.buttonOK.Text = "添加零件信息";
            }
        }

    }
}
