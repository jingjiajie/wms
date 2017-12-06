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

namespace WMS.UI.FormReceipt
{
    public partial class FormReceiptArrivalCheck : Form
    {
        private int ID;
        Action finishedAction = null;
        public FormReceiptArrivalCheck()
        {
            InitializeComponent();
        }
        public FormReceiptArrivalCheck(int ID)
        {
            InitializeComponent();
            this.ID = ID;
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
            this.Controls.Find("textBoxReceiptTicketID", true)[0].Text = ID.ToString();
            this.Controls.Find("textBoxReceiptTicketID", true)[0].Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubmissionTicket submissionTicket = new SubmissionTicket();
            WMSEntities wmsEntities = new WMSEntities();
            string errorInfo;
            if (Utilities.CopyTextBoxTextsToProperties(this, submissionTicket, ReceiptMetaData.checkKeyName, out errorInfo) == false)
            {
                MessageBox.Show(errorInfo);
                this.finishedAction();
            }
            //wmsEntities.ReceiptTicket.Add(receiptTicket);
            else
            {
                wmsEntities.SubmissionTicket.Add(submissionTicket);
                wmsEntities.SaveChanges();
                wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State = '送检中' WHERE ID = @receiptTicketID", new SqlParameter("receiptTicketID", ID));
                MessageBox.Show("Successful!");
                this.finishedAction();
            }
            
            this.Close();
        }
    }
}
