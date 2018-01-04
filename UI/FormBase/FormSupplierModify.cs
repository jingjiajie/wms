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
        private Action<int> modifyFinishedCallback = null;
        private Action<int>  addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;
        private int contract_change;

        public FormSupplierModify(int supplierID = -1,int contract_change=1)
        {
            InitializeComponent(); 
            this.supplierID = supplierID;
            this.contract_change = contract_change;
        }

        private void FormSupplierModify_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            if (this.mode == FormMode.ALTER&&this.supplierID == -1)
            { 
                throw new Exception("未设置源供应商信息");
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
                if(this.contract_change ==0)
                { 
                    TextBox textBoxContractState = (TextBox)this.Controls.Find("textBoxContractState", true)[0];
                    textBoxContractState.Enabled = false;
                }
                SupplierView SupplierView = new SupplierView();

                try
                {
                    SupplierView = (from s in this.wmsEntities.SupplierView
                                                 where s.ID == this.supplierID
                                                 select s).Single();
                    
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (SupplierView == null)
                {
                    MessageBox.Show("修改失败，发货单不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                Utilities.CopyPropertiesToTextBoxes(SupplierView, this);
            }
       

        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            TextBox textBoxSupplierName = (TextBox)this.Controls.Find("textBoxName",true)[0];
            TextBox StartDate = (TextBox)this.Controls.Find("textBoxStartDate", true)[0];
            TextBox EndDate = (TextBox)this.Controls.Find("textBoxEndDate", true)[0];
            
            if (StartDate.Text != String.Empty)
            {
                try
                {
                    DateTime.Parse(StartDate.Text);

                }
                catch
                {
                    MessageBox.Show("起始有效日期格式错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (EndDate.Text != string.Empty)
            {
                try
                {
                    DateTime.Parse(EndDate.Text);

                }
                catch
                {
                    MessageBox.Show("结束有效日期格式错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
           


            if (textBoxSupplierName.Text != String.Empty)
            {

                Supplier supplier = null;

                //若修改，则查询原对象。若添加，则新建对象。
                if (this.mode == FormMode.ALTER)
                {
                    try
                    {
                        supplier = (from s in this.wmsEntities.Supplier
                                    where s.ID == this.supplierID
                                    select s).Single();
                    }
                    catch
                    {
                        MessageBox.Show("加载数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.Close();
                        return;
                    }
                    if (supplier == null)
                    {
                        MessageBox.Show("修改失败，发货单不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
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
                try
                {
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //调用回调函数
                if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
                {
                    this.modifyFinishedCallback(supplier.ID );
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
                {
                    this.addFinishedCallback(supplier.ID );
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                this.Close();
            }
            else
            {
                MessageBox.Show("供应商名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
                this.Text = "修改供应商信息";
                this.buttonModify.Text = "修改供应商信息";
                this.groupBox1.Text = "修改供应商信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加供应商信息";
                this.buttonModify.Text = "添加供应商信息";
                this.groupBox1.Text = "添加供应商信息";
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
