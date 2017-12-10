using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Data.SqlClient;
using WMS.UI.FormReceipt;
using System.Threading;

namespace WMS.UI.FormReceipt
{
    public partial class FormReceiptArrivalCheck : Form
    {
        public SubmissionTicket submissionTicket;
        WMSEntities wmsEntities = new WMSEntities();
        private int receiptTicketID;
        private FormMode formMode;
        Action finishedAction = null;
        private AllOrPartial allOrPartial;
        public FormReceiptArrivalCheck()
        {
            InitializeComponent();
        }
        public FormReceiptArrivalCheck(int receiptTicketID, AllOrPartial allOrPartial)
        {
            InitializeComponent();
            this.receiptTicketID = receiptTicketID;
            this.allOrPartial = allOrPartial;
            this.formMode = FormMode.ADD;
        }

        public FormReceiptArrivalCheck(int submissionTicketID)
        {
            InitializeComponent();
            this.submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ID == submissionTicketID && st.State != "作废" select st).Single();
            this.formMode = FormMode.ALTER;
        }

        public void SetFinishedAction(Action action)
        {
            this.finishedAction = action;
        }

        private void FormReceiptArrivalCheck_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < ReceiptMetaData.checkKeyName.Length; i++)
            {
                KeyName curKeyName = ReceiptMetaData.checkKeyName[i];

                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;

                this.tableLayoutPanel1.Controls.Add(textBox);
            }
            this.Controls.Find("textBoxReceiptTicketID", true)[0].Text = receiptTicketID.ToString();
            this.Controls.Find("textBoxReceiptTicketID", true)[0].Enabled = false;
            if (this.formMode == FormMode.ADD)
            {
                this.Controls.Find("textBoxState", true)[0].Text = "待检";
            }
            else if (this.formMode == FormMode.ALTER)
            {
                Utilities.CopyPropertiesToTextBoxes(this.submissionTicket, this);
            }
            this.Controls.Find("textBoxState", true)[0].Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.formMode == FormMode.ADD)
            {
                SubmissionTicket submissionTicket = new SubmissionTicket();
                WMSEntities wmsEntities = new WMSEntities();
                string errorInfo;
                if (Utilities.CopyTextBoxTextsToProperties(this, submissionTicket, ReceiptMetaData.checkKeyName, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo);
                }
                //wmsEntities.ReceiptTicket.Add(receiptTicket);
                else
                {
                    wmsEntities.SubmissionTicket.Add(submissionTicket);
                    wmsEntities.SaveChanges();
                    if (this.allOrPartial == AllOrPartial.ALL)
                    {
                        wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State = '送检中' WHERE ID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID));

                        ReceiptTicketItem[] receiptTicketItem = (from rt in wmsEntities.ReceiptTicketItem where rt.ReceiptTicketID == receiptTicketID select rt).ToArray();
                        int i = 0;
                        ReceiptUtilities receiptUtilities = new ReceiptUtilities();
                        foreach (ReceiptTicketItem rti in receiptTicketItem)
                        {
                            SubmissionTicketItem submissionTicketItem = ReceiptUtilities.ReceiptTicketItemToSubmissionTicketItem(rti, submissionTicket.ID);
                            wmsEntities.SubmissionTicketItem.Add(submissionTicketItem);
                        }
                        wmsEntities.SaveChanges();
                    }
                    else
                    {
                        wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State = '部分送检中' WHERE ID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID));

                    }
                }
            }
            else if (this.formMode == FormMode.ALTER)
            {
                string errorInfo;
                if (Utilities.CopyTextBoxTextsToProperties(this, this.submissionTicket, ReceiptMetaData.checkKeyName, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                else
                {
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                    }).Start();
                }
            }
            //this.submissionTicket = submissionTicket;
            MessageBox.Show("Successful!");
            this.finishedAction();
            this.Close();


        }
    }
}
