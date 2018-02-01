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
namespace WMS.UI
{
    public partial class FormSubmissionChooseExcelType : Form
    {
        private SubmissionTicket submissionTicket;

        public FormSubmissionChooseExcelType()
        {
            InitializeComponent();
        }
        public FormSubmissionChooseExcelType(SubmissionTicket submissionTicket)
        {
            InitializeComponent();
            this.submissionTicket = submissionTicket;
        }

        private void FormSubmissionChooseExcelType_Load(object sender, EventArgs e)
        {

        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            
            SubmissionTicket submissionTicket;
            try
            {
                submissionTicket = AddPreviewTime(this.submissionTicket.ID);
            }
            catch
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicketItemView[] submissionTicketItemView =
                (from p in wmsEntities.SubmissionTicketItemView
                 where p.SubmissionTicketID == submissionTicket.ID
                 select p).ToArray();
            StartPreview(submissionTicketItemView);
        }

        private SubmissionTicket AddPreviewTime(int submissionTicketID)
        {
            WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicket submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ID == submissionTicketID select st).FirstOrDefault();
            if (submissionTicket != null)
            {
                submissionTicket.PaintTime = DateTime.Now;
            }
            
            wmsEntities.SaveChanges();
           

            return submissionTicket;
        }

        private void StartPreview(SubmissionTicketItemView[] submissionTicketItemView)
        {
            WMSEntities wmsEntities = new WMSEntities();
            StandardFormPreviewExcel formPreview = new StandardFormPreviewExcel("送检单预览", (float)0.7);
            if (formPreview.SetPatternTable(@"Excel\SubmissionTicket.xlsx") == false)
            {
                this.Close();
                return;
            }
            SubmissionTicketView submissionTicketView = (from stv in wmsEntities.SubmissionTicketView where stv.ID == this.submissionTicket.ID select stv).FirstOrDefault();
            ReceiptTicketView receiptTicketView = (from rtv in wmsEntities.ReceiptTicketView where rtv.ID == submissionTicketView.ReceiptTicketID select rtv).FirstOrDefault();
            if (receiptTicketView != null)
            {
                formPreview.AddData("ReceiptTicket", receiptTicketView);
            }
            if (submissionTicketView != null)
            {
                formPreview.AddData("SubmissionTicket", submissionTicketView);
            }
            formPreview.AddData("SubmissionTicketItem", submissionTicketItemView);
            formPreview.Show();
            this.Close();
        }

        private void buttonPass_Click(object sender, EventArgs e)
        {
            SubmissionTicket submissionTicket;
            try
            {
                submissionTicket = AddPreviewTime(this.submissionTicket.ID);
            }
            catch
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicketItemView[] submissionTicketItemView =
                (from p in wmsEntities.SubmissionTicketItemView
                 where p.SubmissionTicketID == submissionTicket.ID && p.State == "合格"
                 select p).ToArray();
            StartPreview(submissionTicketItemView);
        }

        private void buttonNoPass_Click(object sender, EventArgs e)
        {
            SubmissionTicket submissionTicket;
            try
            {
                submissionTicket = AddPreviewTime(this.submissionTicket.ID);
            }
            catch
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicketItemView[] submissionTicketItemView =
                (from p in wmsEntities.SubmissionTicketItemView
                 where p.SubmissionTicketID == submissionTicket.ID && (p.State == "不合格" || p.State == "部分合格")
                 select p).ToArray();
            StartPreview(submissionTicketItemView);
        }

        private void buttonPass_MouseEnter(object sender, EventArgs e)
        {
            buttonPass.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonPass_MouseLeave(object sender, EventArgs e)
        {
            buttonPass.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonPass_MouseDown(object sender, MouseEventArgs e)
        {
            buttonPass.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonNoPass_MouseEnter(object sender, EventArgs e)
        {
            buttonNoPass.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonNoPass_MouseLeave(object sender, EventArgs e)
        {
            buttonNoPass.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonNoPass_MouseDown(object sender, MouseEventArgs e)
        {
            buttonNoPass.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonAll_MouseEnter(object sender, EventArgs e)
        {
            buttonAll.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonAll_MouseLeave(object sender, EventArgs e)
        {
            buttonAll.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonAll_MouseDown(object sender, MouseEventArgs e)
        {
            buttonAll.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
