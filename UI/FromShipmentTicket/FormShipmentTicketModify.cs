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

namespace WMS.UI.FromShipmentTicket
{
    public partial class FormShipmentTicketModify : Form
    {
        private int shipmentTicketID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormShipmentTicketModify(int shipmentTicketID = -1)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
        }

        private void FormShipmentTicketModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.shipmentTicketID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < ShipmentTicketViewMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = ShipmentTicketViewMetaData.KeyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false)
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

            if (this.mode == FormMode.ALTER)
            {
                ShipmentTicketView shipmentTicketView = (from s in this.wmsEntities.ShipmentTicketView
                                               where s.ID == this.shipmentTicketID
                                                         select s).Single();
                Utilities.CopyPropertiesToTextBoxes(shipmentTicketView, this);
            }
            
            this.Controls.Find("textBoxProjectID", true)[0].LostFocus += textBoxProjectID_LostFocus;
            this.Controls.Find("textBoxWarehouseID", true)[0].LostFocus += textBoxWarehouseID_LostFocus;
        }

        private void textBoxProjectID_LostFocus(object sender, EventArgs e)
        {
            TextBox textBoxProjectID = (TextBox)this.Controls.Find("textBoxProjectID", true)[0];
            if (textBoxProjectID.Text.Length == 0) return;
            CheckForeignKeyProjectID();
        }
        private void textBoxWarehouseID_LostFocus(object sender, EventArgs e)
        {
            TextBox textBoxWarehouseID = (TextBox)this.Controls.Find("textBoxWarehouseID", true)[0];
            if (textBoxWarehouseID.Text.Length == 0) return;
            CheckForeignKeyWarehouseID();
        }

        private bool CheckForeignKeyProjectID()
        {
            TextBox textBoxProjectID = (TextBox)this.Controls.Find("textBoxProjectID", true)[0];
            if (textBoxProjectID.Text.Length == 0)
            {
                MessageBox.Show("项目ID 不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int projectID;
            if (int.TryParse(textBoxProjectID.Text, out projectID) == false)
            {
                MessageBox.Show("项目ID 只接受数值类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            ProjectView[] result = (from p in wmsEntities.ProjectView
                                              where p.ID == projectID
                                              select p).ToArray();
            if (result.Length == 0)
            {
                MessageBox.Show("未找到项目ID为" + projectID + "的项目，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            ProjectView projectView = result[0];
            Utilities.CopyPropertiesToTextBoxes(projectView, this, "textBox");
            Utilities.CopyPropertiesToTextBoxes(projectView, this, "textBoxProject");
            return true;
        }

        private bool CheckForeignKeyWarehouseID()
        {
            TextBox textBoxWarehouseID = (TextBox)this.Controls.Find("textBoxWarehouseID", true)[0];
            if (textBoxWarehouseID.Text.Length == 0)
            {
                MessageBox.Show("仓库ID 不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int warehouseID;
            if (int.TryParse(textBoxWarehouseID.Text, out warehouseID) == false)
            {
                MessageBox.Show("仓库ID 只接受数值类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            WarehouseView[] result = (from p in wmsEntities.WarehouseView
                                    where p.ID == warehouseID
                                    select p).ToArray();
            if (result.Length == 0)
            {
                MessageBox.Show("未找到仓库ID为" + warehouseID + "的仓库，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            WarehouseView warehouseView = result[0];
            Utilities.CopyPropertiesToTextBoxes(warehouseView, this, "textBox");
            Utilities.CopyPropertiesToTextBoxes(warehouseView, this, "textBoxWarehouse");
            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ShipmentTicket shipmentTicket = null;
            //StockInfo stockInfo = null;

            //若修改，则查询原StockInfo对象。若添加，则新建一个StockInfo对象。
            if (this.mode == FormMode.ALTER)
            {
                shipmentTicket = (from s in this.wmsEntities.ShipmentTicket
                             where s.ID == this.shipmentTicketID
                             select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                shipmentTicket = new ShipmentTicket();
                this.wmsEntities.ShipmentTicket.Add(shipmentTicket);
            }

            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, shipmentTicket, ShipmentTicketViewMetaData.KeyNames, out string errorMessage) == false)
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
                this.Text = "修改发货单信息";
                this.buttonOK.Text = "修改发货单信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加发货单信息";
                this.buttonOK.Text = "添加发货单信息";
            }
        }

        private void FormShipmentTicketModify_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
        }
    }
}
