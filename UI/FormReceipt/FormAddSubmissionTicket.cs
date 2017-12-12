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

namespace WMS.UI.FormReceipt
{
    public partial class FormAddSubmissionTicket : Form
    {
        private int receiptTicketID;
        private Action CallBack;
        public FormAddSubmissionTicket()
        {
            InitializeComponent();
        }

        public FormAddSubmissionTicket(int receiptTicketID)
        {
            InitializeComponent();
            this.receiptTicketID = receiptTicketID;
        }

        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private void FormAddSubmissionTicket_Load(object sender, EventArgs e)
        {
            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ReceiptMetaData.submissionTicketKeyName);
            this.Controls.Find("textBoxID", true)[0].Text = "0";
            TextBox textBoxReceiptTicketID = (TextBox)this.Controls.Find("textBoxReceiptTicketID", true)[0];
            textBoxReceiptTicketID.Text = receiptTicketID.ToString();
            textBoxReceiptTicketID.Enabled = false;
            TextBox textBoxState = (TextBox)this.Controls.Find("textBoxState", true)[0];
            textBoxState.Text = "待检";
            textBoxState.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicket submissionTicket = new SubmissionTicket();
            string errorInfo;
            if (Utilities.CopyTextBoxTextsToProperties(this, submissionTicket, ReceiptMetaData.submissionTicketKeyName, out errorInfo) == false)
            {
                MessageBox.Show(errorInfo);
                return;
            }
            else
            {
                wmsEntities.SubmissionTicket.Add(submissionTicket);
            }
            ReceiptTicketItem[] receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ReceiptTicketID == this.receiptTicketID select rti).ToArray();
            if (receiptTicketItem.Length == 0)
            {
                MessageBox.Show("该收货单没有符合要求的条目");
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
                    wmsEntities.SaveChanges();
                    MessageBox.Show("成功");
                }).Start();
            }
            CallBack();
        }
    }
}
