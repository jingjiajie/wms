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
    public partial class FormShipmentTicketModify : Form
    {
        private int shipmentTicketID = -1;
        private int userID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormShipmentTicketModify(int userID,int shipmentTicketID = -1)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
            this.userID = userID;
        }

        private void FormShipmentTicketModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.shipmentTicketID == -1)
            {
                throw new Exception("未设置源发货单信息");
            }

            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ShipmentTicketViewMetaData.KeyNames);

            if (this.mode == FormMode.ALTER)
            {
                this.Text = "修改发货单信息";
                ShipmentTicketView ShipmentTicketView = (from s in this.wmsEntities.ShipmentTicketView
                                                                 where s.ID == this.shipmentTicketID
                                                         select s).Single();
                Utilities.CopyPropertiesToTextBoxes(ShipmentTicketView, this);
                Utilities.CopyPropertiesToComboBoxes(ShipmentTicketView, this);
            }
            else if(this.mode == FormMode.ADD)
            {
                this.Text = "添加发货单";
            }
            //this.Controls.Find("textBoxShipmentTicketID", true)[0].LostFocus += textBoxShipmentTicketID_LostFocus;
            //this.Controls.Find("textBoxStockInfoID", true)[0].LostFocus += textBoxStockInfoID_LostFocus;
        }

        private void textBoxShipmentTicketID_LostFocus(object sender, EventArgs e)
        {
            TextBox textBoxShipmentTicketID = (TextBox)this.Controls.Find("textBoxShipmentTicketID", true)[0];
            if (textBoxShipmentTicketID.Text.Length == 0) return;
            CheckForeignKeyShipmentTicketID();
        }
        private void textBoxStockInfoID_LostFocus(object sender, EventArgs e)
        {
            TextBox textBoxStockInfoID = (TextBox)this.Controls.Find("textBoxStockInfoID", true)[0];
            if (textBoxStockInfoID.Text.Length == 0) return;
            CheckForeignKeyStockInfoID();
        }

        private bool CheckForeignKeyShipmentTicketID()
        {
            TextBox textBoxShipmentTicketID = (TextBox)this.Controls.Find("textBoxShipmentTicketID", true)[0];
            if (textBoxShipmentTicketID.Text.Length == 0)
            {
                MessageBox.Show("发货单ID 不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int shipmentTicketID;
            if (int.TryParse(textBoxShipmentTicketID.Text, out shipmentTicketID) == false)
            {
                MessageBox.Show("发货单ID 只接受数值类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            ShipmentTicketView[] result = (from p in wmsEntities.ShipmentTicketView
                                    where p.ID == shipmentTicketID
                                           select p).ToArray();
            if (result.Length == 0)
            {
                MessageBox.Show("未找到发货单ID为" + shipmentTicketID + "的单据，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            ShipmentTicketView shipmentTicketView = result[0];
            Utilities.CopyPropertiesToTextBoxes(shipmentTicketView, this, "textBox");
            Utilities.CopyPropertiesToTextBoxes(shipmentTicketView, this, "textBoxShipmentTicket");
            return true;
        }

        private bool CheckForeignKeyStockInfoID()
        {
            TextBox textBoxStockInfoID = (TextBox)this.Controls.Find("textBoxStockInfoID", true)[0];
            if (textBoxStockInfoID.Text.Length == 0)
            {
                MessageBox.Show("库存零件ID 不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int stockInfoID;
            if (int.TryParse(textBoxStockInfoID.Text, out stockInfoID) == false)
            {
                MessageBox.Show("库存零件ID 只接受数值类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            StockInfoView[] result = (from p in wmsEntities.StockInfoView
                                      where p.ID == stockInfoID
                                      select p).ToArray();
            if (result.Length == 0)
            {
                MessageBox.Show("未找到库存零件ID为" + stockInfoID + "的零件，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            StockInfoView stockInfoView = result[0];
            Utilities.CopyPropertiesToTextBoxes(stockInfoView, this, "textBox");
            Utilities.CopyPropertiesToTextBoxes(stockInfoView, this, "textBoxStockInfo");
            return true;
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            ShipmentTicket shipmentTicket = null;

            //若修改，则查询原对象。若添加，则新建一个对象。
            if (this.mode == FormMode.ALTER)
            {
                shipmentTicket = (from s in this.wmsEntities.ShipmentTicket
                                      where s.ID == this.shipmentTicketID
                                      select s).Single();
                shipmentTicket.LastUpdateUserID = this.userID;
                shipmentTicket.LastUpdateTime = DateTime.Now;
            }
            else if (mode == FormMode.ADD)
            {
                shipmentTicket = new ShipmentTicket();
                shipmentTicket.CreateTime = DateTime.Now;
                shipmentTicket.CreateUserID = this.userID;
                this.wmsEntities.ShipmentTicket.Add(shipmentTicket);
            }

            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, shipmentTicket, ShipmentTicketViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Utilities.CopyComboBoxsToProperties(this, shipmentTicket, ShipmentTicketViewMetaData.KeyNames);
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
                this.Text = "修改发货单零件信息";
                this.buttonOK.Text = "修改发货单零件信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加发货单零件信息";
                this.buttonOK.Text = "添加发货单零件信息";
            }
        }
    }
}
