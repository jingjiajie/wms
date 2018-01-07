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
        private int userid = -1;
      

        public FormSupplierModify(int supplierID = -1,int contract_change=1,int userid=-1)
        {
            InitializeComponent(); 
            this.supplierID = supplierID;
            this.contract_change = contract_change;
            this.userid = userid;
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

                if (this.contract_change ==0)
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
            DialogResult MsgBoxResult= DialogResult.No;//设置对话框的返回值
            TextBox textBoxSupplierName = (TextBox)this.Controls.Find("textBoxName", true)[0];
         
            TextBox textBoxName = (TextBox)this.Controls.Find("textBoxName", true)[0];
            if (this.mode == FormMode.ALTER)
            {
                

                MsgBoxResult = MessageBox.Show("是否要保留历史信息","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,

                MessageBoxDefaultButton.Button2);
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
                        MessageBox.Show("修改失败，供应商不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }


                    //开始数据库操作
                    //将原供应商存为历史信息
                    var sameNameUsers = (from u in wmsEntities.Supplier
                                         where u.Name == textBoxName.Text
                                            && u.ID != supplier.ID&&u.IsHistory ==0
                                         select u).ToArray();
                    if (sameNameUsers.Length > 0)
                    {
                        MessageBox.Show("修改供应商名失败，已存在同名供应商：" + textBoxName.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
                    {


                       
                        this.wmsEntities.Supplier.Add(supplier);
                        try
                        {
                            supplier.ID = -1;
                            supplier.IsHistory = 1;
                            supplier.NewestSupplierID =this.supplierID ;
                            wmsEntities.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var supplierstorge = (from u in wmsEntities.SupplierStorageInfo 
                                             where u.SupplierID  == this.supplierID 
                                             select u).ToArray();

                        if (supplierstorge.Length > 0)
                        {

                            for (int i = 0; i < supplierstorge.Length; i++)
                            {
                                try
                                {
                                    supplierstorge[i].ExecuteSupplierID = supplier.ID ;
                                    wmsEntities.SaveChanges();
                                }
                                catch
                                {
                                    continue;
                                }
                            }

                       }






                        //继续查找
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
                    


                    if (Utilities.CopyTextBoxTextsToProperties(this, supplier, SupplierMetaData.KeyNames, out string errorMessage) == false)
                    {
                        MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                   
                    try
                    {
                        supplier.IsHistory = 0;
                        wmsEntities.SaveChanges();

                    }
                    catch
                    {
                        MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                }


                else if (mode == FormMode.ADD)
                {
                    var sameNameUsers = (from u in wmsEntities.Supplier 
                                         where u.Name == textBoxName.Text
                                         select u).ToArray();
                    if (sameNameUsers.Length > 0)
                    {
                        MessageBox.Show("添加供应商失败，已存在同名供应商：" + textBoxName.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    supplier = new Supplier();
                    this.wmsEntities.Supplier.Add(supplier);
                   
                    //开始数据库操作
                    if (Utilities.CopyTextBoxTextsToProperties(this, supplier, SupplierMetaData.KeyNames, out string errorMessage) == false)
                    {
                        MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    try
                    {
                        supplier.CreateUserID = this.userid;
                        supplier.CreateTime = DateTime.Now;
                        supplier.LastUpdateUserID = this.userid;
                        supplier.LastUpdateTime = DateTime.Now;

                        supplier.IsHistory = 0;
                        
                    wmsEntities.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

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
