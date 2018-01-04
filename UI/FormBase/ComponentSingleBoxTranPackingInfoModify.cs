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
    public partial class ComponentSingleBoxTranPackingInfoModify : Form
    {

        private int componenID = -1;
        private int supplierID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action<int> modifyFinishedCallback = null;
        private Action<int> addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public ComponentSingleBoxTranPackingInfoModify(int componenID = -1)
        {
            InitializeComponent();
            this.componenID = componenID;
        }
        
        private void ComponentSingleBoxTranPackingInfoModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.componenID == -1)
            {
                throw new Exception("未设置源零件信息");
            }

            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ComponenViewMetaData.KeyNames2);

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
            }

        }
       

        private void buttonOK_Click(object sender, EventArgs e)
        {

            DataAccess.Component componen = null;
            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    //询问是否保留历史信息
                    if (MessageBox.Show("是否保留历史信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataAccess.Component historycomponen = null;
                        historycomponen = (from s in this.wmsEntities.Component
                                    where s.ID == this.componenID
                                    select s).Single();
                        historycomponen.IsHistory = 1;
                        componen = new DataAccess.Component();
                        this.wmsEntities.Component.Add(componen);
                    }
                    else
                    {
                        componen = (from s in this.wmsEntities.Component
                                    where s.ID == this.componenID
                                    select s).Single();
                        componen.IsHistory = 0;
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


            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, componen, ComponenViewMetaData.ComponentOuterPackingSizekeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Utilities.CopyComboBoxsToProperties(this, componen, ComponenViewMetaData.KeyNames2);
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
            else if (mode == FormMode.CHECK)
            {
                this.Text = "零件单箱运输包装信息";
                this.groupBox1.Text = "零件单箱运输包装信息";
                this.buttonOK.Text = "零件单箱运输包装信息";
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
