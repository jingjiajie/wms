using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI.FormReceipt
{
    public partial class FormAddSubmissionTicket : Form
    {
        private int receiptTicketID;
        private int submissionTicketID;
        private Action CallBack;
        private int userID;
        private FormMode formMode;
        private WMSEntities wmsEntities = new WMSEntities();
        private SubmissionTicket submissionTicket;
        public FormAddSubmissionTicket()
        {
            InitializeComponent();
        }

        public FormAddSubmissionTicket(int ID, int userID, FormMode formMode)
        {
            InitializeComponent();
            //this.receiptTicketID = receiptTicketID;
            this.userID = userID;
            this.formMode = formMode;
            if (formMode == FormMode.ADD)
            {
                this.receiptTicketID = ID;
            }
            else if (formMode == FormMode.ALTER)
            {
                this.submissionTicketID = ID;
            }
        }

        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private void FormAddSubmissionTicket_Load(object sender, EventArgs e)
        {
            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ReceiptMetaData.submissionTicketKeyName);
            //this.Controls.Find("textBoxID", true)[0].Text = "0";
            //TextBox textBoxReceiptTicketID = (TextBox)this.Controls.Find("textBoxReceiptTicketID", true)[0];
            //textBoxReceiptTicketID.Text = receiptTicketID.ToString();
            //textBoxReceiptTicketID.Enabled = false;
            if (this.formMode == FormMode.ADD)
            {
                TextBox textBoxState = (TextBox)this.Controls.Find("textBoxState", true)[0];
                textBoxState.Text = "待检";
                textBoxState.Enabled = false;
            }
            else
            {
                this.submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ID == this.submissionTicketID select st).FirstOrDefault();
                if (submissionTicket == null)
                {
                    MessageBox.Show("该送检单已被删除！");
                    CallBack();
                    this.Close();
                }
                else
                {
                    Utilities.CopyPropertiesToTextBoxes(submissionTicket, this);
                }
            }
            
           
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            if (this.formMode == FormMode.ADD)
            {
                this.submissionTicket = new SubmissionTicket();
            }
            string errorInfo;
            if (Utilities.CopyTextBoxTextsToProperties(this, submissionTicket, ReceiptMetaData.submissionTicketKeyName, out errorInfo) == false)
            {
                MessageBox.Show(errorInfo);
                return;
            }
            else
            {
                if (this.formMode == FormMode.ADD)
                {
                    ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).FirstOrDefault();
                    if (receiptTicket != null)
                    {
                        submissionTicket.ProjectID = receiptTicket.ProjectID;
                        submissionTicket.WarehouseID = receiptTicket.Warehouse;
                        submissionTicket.ReceiptTicketID = receiptTicket.ID;
                    }
                    submissionTicket.CreateTime = DateTime.Now.ToString();
                    submissionTicket.CreateUserID = this.userID;
                    submissionTicket.LastUpdateTime = DateTime.Now;
                    submissionTicket.LastUpdateUserID = this.userID;
                    wmsEntities.SubmissionTicket.Add(submissionTicket);
                }
                else if (this.formMode == FormMode.ALTER)
                {
                    submissionTicket.LastUpdateUserID = this.userID;
                    submissionTicket.LastUpdateTime = DateTime.Now;
                    new Thread(()=> 
                    {
                        try
                        {
                            this.wmsEntities.SaveChanges();
                            MessageBox.Show("修改成功");
                        }
                        catch
                        {
                            MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            return;
                        }
                    }).Start();
                    CallBack();
                    this.Close();
                    return;
                }
            }
            ReceiptTicketItem[] receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ReceiptTicketID == this.receiptTicketID select rti).ToArray();
            if (receiptTicketItem.Length == 0)
            {
                MessageBox.Show("该收货单未添加收货单条目");
            }
            else
            {
                foreach(ReceiptTicketItem rti in receiptTicketItem)
                {
                    rti.State = "送检中";
                    SubmissionTicketItem submissionTicketItem = new SubmissionTicketItem();
                    submissionTicketItem.ReceiptTicketItemID = rti.ID;
                    submissionTicketItem.State = "待检";
                    submissionTicket.SubmissionTicketItem.Add(submissionTicketItem);
                }
                new Thread(() =>
                {
                    try
                    {
                        wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State = '送检中' WHERE ID = @receiptTicket", new SqlParameter("receiptTicket", receiptTicketID));
                        wmsEntities.SaveChanges();
                        submissionTicket.No = Utilities.GenerateNo("J", submissionTicket.ID);
                        wmsEntities.SaveChanges();
                        MessageBox.Show("成功");
                    }
                    catch
                    {
                        MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        return;
                    }
                }).Start();
            }
            CallBack();
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
