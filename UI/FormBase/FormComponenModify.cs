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
        private Action<int> modifyFinishedCallback = null;
        private Action<int> addFinishedCallback = null;
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

            if (this.mode == FormMode.ALTER)
            {
               
                try
                {
                    ComponentView componenView = (from s in this.wmsEntities.ComponentView
                                                  where s.ID == this.componenID
                                                  select s).Single();
                    Utilities.CopyPropertiesToTextBoxes(componenView, this);
                }
                catch (Exception)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

            }

            this.Controls.Find("textBoxSupplierName", true)[0].Click += textBoxSupplierName_Click;
        }
        private void textBoxSupplierName_Click(object sender, EventArgs e)
        {
           
            var formSelectSupplier = new FormBase.FormSelectSupplier();
            formSelectSupplier.SetSelectFinishCallback((selectedID) =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                string supplierName = (from s in wmsEntities.SupplierView
                                       where s.ID == selectedID
                                       select s.Name).FirstOrDefault();
                if (supplierName == null)
                {
                    MessageBox.Show("选择供应商失败，供应商不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.supplierID = selectedID;
                this.Controls.Find("textBoxSupplierName", true)[0].Text = supplierName;
            });
            formSelectSupplier.Show();
            
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {

            var textBoxSupplierName = this.Controls.Find("textBoxSupplierName", true)[0];
            var textBoxNo = this.Controls.Find("textBoxNo", true)[0];
            var textBoxName = this.Controls.Find("textBoxName", true)[0];
            if (textBoxNo.Text == string.Empty)
            {
                MessageBox.Show("零件代号不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxName.Text == string.Empty)
            {
                MessageBox.Show("零件名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxSupplierName.Text == string.Empty)
            {
                MessageBox.Show("请选择供应商！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //if (this.supplierID == -1)
            //{
            //    MessageBox.Show("请输入正确的供应商名称！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            DataAccess.Component componen = null;
            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    componen = (from s in this.wmsEntities.Component
                                where s.ID == this.componenID
                                select s).Single();
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
                catch
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (componen == null)
                {
                    MessageBox.Show("库存信息不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                this.modifyFinishedCallback(componen.ID);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if(this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback(componen.ID);
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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
            if(mode == FormMode.ALTER)
            {
                this.Text = "修改零件信息";
                this.groupBox1.Text = "修改零件信息";
                this.buttonOK.Text = "修改零件信息";
            }else if (mode == FormMode.ADD)
            {
                this.Text = "添加零件信息";
                this.groupBox1.Text = "添加零件信息";
                this.buttonOK.Text = "添加零件信息";
            }
        }

    }
}
