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
    public partial class ComponentShipmentInfoModify : Form
    {

        private int componenID = -1;
        private int supplierID = -1;
        private int userID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action<int> modifyFinishedCallback = null;
        private Action<int> addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public ComponentShipmentInfoModify(int userID, int componenID = -1)
        {
            InitializeComponent();
            this.componenID = componenID;
            this.userID = userID;
        }
        
        private void ComponentShipmentInfoModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.componenID == -1)
            {
                throw new Exception("未设置源零件信息");
            }

            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ComponenViewMetaData.ComponentShipmentInfokeyNames);

            if (this.mode == FormMode.ALTER|| this.mode == FormMode.CHECK)
            {
               
                try
                {
                    DataAccess.Component componen = (from s in this.wmsEntities.Component
                                                  where s.ID == this.componenID
                                                  select s).Single();
                    Utilities.CopyPropertiesToTextBoxes(componen, this);
                }
                catch (Exception)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

            }
            if (this.mode == FormMode.CHECK)
            {
                this.buttonOK.Visible = false;
                //foreach (Control control in this.Controls)
                //{
                //    if (control is TextBox)
                //    {
                //        (control as TextBox).ReadOnly = true;
                //    }
                //}
            }
        }
       

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult = DialogResult.No;//设置对话框的返回值

            //询问是否保留历史信息
            if (this.mode == FormMode.ALTER)
            {


                MsgBoxResult = MessageBox.Show("是否要保留历史信息", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,

                MessageBoxDefaultButton.Button2);
            }

            DataAccess.Component componen = null;
            if (this.mode == FormMode.ALTER)
            {

                try
                {
                    componen = (from s in this.wmsEntities.Component
                                where s.ID == this.componenID
                                select s).Single();
                }
                catch
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (componen == null)
                {
                    MessageBox.Show("历史零件信息不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
                {
                    //新建零件保留历史信息
                    this.wmsEntities.Component.Add(componen);

                    try
                    {
                        componen.ID = -1;
                        //componen.IsHistory = 1;
                        //componen.NewestComponentID = this.componenID;
                        //componen.LastUpdateUserID = this.userID;
                        //componen.LastUpdateTime = DateTime.Now;
                        wmsEntities.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    try
                    {
                        componen = (from s in this.wmsEntities.Component
                                    where s.ID == this.componenID
                                    select s).Single();
                    }
                    catch
                    {
                        MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (componen == null)
                    {
                        MessageBox.Show("零件信息不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }


            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, componen, ComponenViewMetaData.ComponentShipmentInfokeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Utilities.CopyComboBoxsToProperties(this, componen, ComponenViewMetaData.ComponentShipmentInfokeyNames);
            }
            //componen.LastUpdateUserID = this.userID;
            //componen.LastUpdateTime = DateTime.Now;
            //componen.IsHistory = 0;
            wmsEntities.SaveChanges();
            //调用回调函数
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback(componen.ID);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                this.Text = "修改零件出货信息";
                this.groupBox1.Text = "修改零件出货信息";
                this.buttonOK.Text = "修改零件出货信息";
            }
            else if (mode == FormMode.CHECK)
            {
                this.Text = "零件出货信息";
                this.groupBox1.Text = "零件出货信息";
                this.buttonOK.Text = "零件出货信息";
            }
            
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonOK_MouseDown(object sender, MouseEventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
