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
        private int componenID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormComponenModify(int componenID = -1)
        {
            InitializeComponent();
            this.componenID = componenID;
        }
        
        private void FormComponenModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.componenID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < StockInfoMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = StockInfoMetaData.KeyNames[i];
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            }

            if(this.mode == FormMode.ALTER)
            {
                DataAccess.Component componen = (from s in this.wmsEntities.Component
                                       where s.ID == this.componenID
                                       select s).Single();
                Utilities.CopyPropertiesToTextBoxes(componen, this);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DataAccess.Component componen = null;
            
            //若修改，则查询原StockInfo对象。若添加，则新建一个StockInfo对象。
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
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, componen, ComponenMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            wmsEntities.SaveChanges();
            //调用回调函数
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }else if(this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback();
            }
            this.Close();
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
                this.Text = "修改库存信息";
                this.buttonOK.Text = "修改库存信息";
            }else if (mode == FormMode.ADD)
            {
                this.Text = "添加库存信息";
                this.buttonOK.Text = "添加库存信息";
            }
        }

    }
}
