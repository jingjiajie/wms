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
    public partial class FormStockInfoModify : Form
    {
        private int stockInfoID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormStockInfoModify(int stockInfoID = -1)
        {
            InitializeComponent();
            this.stockInfoID = stockInfoID;
        }
        

        private void FormStockInfoModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.stockInfoID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < StockInfoMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = StockInfoMetaData.KeyNames[i];
                if(curKeyName.Visible == false && curKeyName.Editable == false)
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            }

            if(this.mode == FormMode.ALTER)
            {
                StockInfoView stockInfoView = (from s in this.wmsEntities.StockInfoView
                                               where s.ID == this.stockInfoID
                                               select s).Single();
                Utilities.CopyPropertiesToTextBoxes(stockInfoView, this);
            }

            this.Controls.Find("textBoxPutawayTicketID", true)[0].LostFocus += textBoxPutawayTicketID_LostFocus;
        }

        private void textBoxPutawayTicketID_LostFocus(object sender, EventArgs e)
        {
            TextBox textBoxPutawayTicketID = (TextBox)this.Controls.Find("textBoxPutawayTicketID", true)[0];
            if (textBoxPutawayTicketID.Text.Length == 0)
            {
                return;
            }
            int putawayTicketID;
            if(int.TryParse(textBoxPutawayTicketID.Text,out putawayTicketID) == false)
            {
                MessageBox.Show("上架单ID 只接受数值类型","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PutawayTicket[] result = (from p in wmsEntities.PutawayTicket
                                      where p.ID == putawayTicketID
                                      select p).ToArray();
            if(result.Length == 0)
            {
                MessageBox.Show("未找到上架单ID为"+putawayTicketID+"的上架单，请重新输入","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PutawayTicket putawayTicket = result[0];
            Utilities.CopyPropertiesToTextBoxes(putawayTicket, this, "textBoxPutawayTicket");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            StockInfo stockInfo = null;
            
            //若修改，则查询原StockInfo对象。若添加，则新建一个StockInfo对象。
            if (this.mode == FormMode.ALTER)
            {
                stockInfo = (from s in this.wmsEntities.StockInfo
                             where s.ID == this.stockInfoID
                             select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                stockInfo = new StockInfo();
                this.wmsEntities.StockInfo.Add(stockInfo);
            }
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, stockInfo, StockInfoMetaData.KeyNames, out string errorMessage) == false)
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
