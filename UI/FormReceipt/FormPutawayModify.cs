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
    public partial class FormPutawayModify : Form
    {
        private Action CallBack;
        private int putawayTicketID;
        private WMSEntities wmsEntities = new WMSEntities();
        private PutawayTicket putawayTicket;
        public FormPutawayModify()
        {
            InitializeComponent();
        }

        public FormPutawayModify(int putawayTicketID)
        {
            InitializeComponent();
            this.putawayTicketID = putawayTicketID;
        }

        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private void FormPutawayModify_Load(object sender, EventArgs e)
        {
            InitComponent();
            putawayTicket = (from pt in wmsEntities.PutawayTicket where pt.ID == this.putawayTicketID select pt).FirstOrDefault();
            if (putawayTicket == null)
            {
                MessageBox.Show("找不到该上架单");
                return;
            }
            else
            {
                Utilities.CopyPropertiesToTextBoxes(putawayTicket, this);
            }
        }

        private void InitComponent()
        {
            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < ReceiptMetaData.putawayTicketKeyName.Length; i++)
            {
                KeyName curKeyName = ReceiptMetaData.putawayTicketKeyName[i];

                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;

                //Console.WriteLine("{0}  {1}", label.Text, textBox.Name);


                this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            }
            /*
            this.Controls.Find("textBoxSubmissionTicketID", true)[0].Text = this.submissionTicketID.ToString();
            this.Controls.Find("textBoxSubmissionTicketID", true)[0].Enabled = false;
            this.Controls.Find("textBoxComponentID", true)[0].Enabled = false;
            this.Controls.Find("textBoxComponentID", true)[0].Text = this.receiptTicketItem.ComponentID.ToString();
            */
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string errorInfo;
            //PutawayTicket putawayTicket = (from pt in wmsEntities.PutawayTicket where pt.ID == this.putawayTicketID select pt).FirstOrDefault();
            //if (putwayTicket)
            if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName, out errorInfo) == true)
            {
                //wmsEntities.PutawayTicket.Add(putawayTicket);
                new Thread(() =>
                {
                    wmsEntities.SaveChanges();
                    MessageBox.Show("成功");
                    this.Invoke(new Action(() =>
                    {
                        CallBack();
                    }));
                }).Start();
            }
            else
            {
                MessageBox.Show(errorInfo);
            }
        }
    }
}
