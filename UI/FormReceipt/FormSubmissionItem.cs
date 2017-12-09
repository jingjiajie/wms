using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.UI;
using unvell.ReoGrid;
using WMS.DataAccess;
using System.Threading;

namespace WMS.UI.FormReceipt
{
    public partial class FormSubmissionItem : Form
    {
        private int submissionTicketID;
        public FormSubmissionItem()
        {
            InitializeComponent();
        }

        public FormSubmissionItem(int submissionTicketID)
        {
            InitializeComponent();
            this.submissionTicketID = submissionTicketID;
        }

        private void FormSubmissionItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            WMSEntities wmsEntities = new WMSEntities();
            this.textBoxID.Text = this.submissionTicketID.ToString();
            Search();
        }

        private void InitComponents()
        {
            //初始化
            string[] columnNames = (from kn in ReceiptMetaData.submissionTicketItemKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.submissionTicketItemKeyName[i].Visible;
            }
            worksheet.Columns = columnNames.Length;
        }

        private void Search()
        {
            this.labelStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                //ReceiptTicketView[] receiptTicketViews = null;
                SubmissionTicketItemView[] submissionTicketItemView = null;
                
                submissionTicketItemView = wmsEntities.Database.SqlQuery<SubmissionTicketItemView>(String.Format("SELECT * FROM SubmissionTicketItemView WHERE SubmissionTicketID={0}", submissionTicketID)).ToArray();
                               
                this.reoGridControlSubmissionItems.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < submissionTicketItemView.Length; i++)
                    {
                        if (submissionTicketItemView[i].State == "作废")
                        {
                            continue;
                        }
                        SubmissionTicketItemView curSubmissionTicketItemView = submissionTicketItemView[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curSubmissionTicketItemView, (from kn in ReceiptMetaData.submissionTicketItemKeyName select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[n, j] = columns[j];
                        }
                        n++;
                    }
                }));

            })).Start();

        }

        private void buttonItemPass_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int submissionTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicketItem SET State='合格' WHERE ID={0}", submissionTicketItemID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicket SET State='部分合格' WHERE ID={0}", submissionTicketID));
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Search();
        }

        private void buttonItemNoPass_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int submissionTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicketItem SET State='不合格' WHERE ID={0}", submissionTicketItemID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicket SET State='部分合格' WHERE ID={0}", submissionTicketID));
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Search();
        }
    }
}
