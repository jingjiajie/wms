using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using WMS.UI.FormReceipt;
using WMS.DataAccess;
using System.Threading;
using System.Data.SqlClient;
namespace WMS.UI
{
    public partial class FormReceiptItems : Form
    {
        private int receiptTicketItemsID;
        private FormMode formMode;
        private int receiptTicketID;
        Action callBack = null;
        const string WAIT_CHECK = "待检";
        const string CHECK = "送检中";
        SubmissionTicket submissionTicket;
        public FormReceiptItems()
        {
            InitializeComponent();
        }

        public void SetCallback(Action action)
        {
            callBack = action;
        }

        public FormReceiptItems(FormMode formMode, int receiptTicketID)
        {
            InitializeComponent();
            this.formMode = formMode;
            this.receiptTicketID = receiptTicketID;
        }

        private bool SubmissionTicketIsExist()
        {
            WMSEntities wmsEntities = new WMSEntities();
            SubmissionTicket[] submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ReceiptTicketID == receiptTicketID && st.State != "作废" select st).ToArray();
            if (submissionTicket.Length == 0)
            {
                return false;
            }
            else
            {
                this.submissionTicket = submissionTicket[0];
                return true;
            }
        }

        private void InitComponents()
        {
            //初始化
            string[] columnNames = (from kn in ReceiptMetaData.itemsKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlReceiptItems.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.itemsKeyName[i].Visible;
            }
            //worksheet.ColumnHeaders[columnNames.Length].Text = "是否送检";
            worksheet.Columns = columnNames.Length;
        }

        private void FormReceiptArrivalItems_Load(object sender, EventArgs e)
        {
            InitComponents();
            WMSEntities wmsEntities = new WMSEntities();
            ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
            Utilities.CopyPropertiesToTextBoxes(receiptTicket, this);
            Search();
        }

        private void Search()
        {
            this.labelStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                ReceiptTicketItemView[] receiptTicketItemViews = null;
                receiptTicketItemViews = wmsEntities.Database.SqlQuery<ReceiptTicketItemView>(String.Format("SELECT * FROM ReceiptTicketItemView WHERE ReceiptTicketID = {0}", this.receiptTicketID)).ToArray();
                this.reoGridControlReceiptItems.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlReceiptItems.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    for (int i = 0; i < receiptTicketItemViews.Length; i++)
                    {

                        ReceiptTicketItemView curReceiptTicketItemView = receiptTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketItemView, (from kn in ReceiptMetaData.itemsKeyName select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j];
                        }
                        //worksheet[i, worksheet.Columns-1] = new CheckBox();
                        
                    }
                }));

            })).Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormReceiptItemsModify formReceiptItemsModify = new FormReceiptItemsModify(FormMode.ADD, receiptTicketItemsID, receiptTicketID);
            formReceiptItemsModify.SetCallBack(() =>
            {
                Search();
            });
            formReceiptItemsModify.Show();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlReceiptItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int receiptItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formReceiptItemsModify = new FormReceiptItemsModify(FormMode.ALTER, receiptItemID, receiptTicketID);
                formReceiptItemsModify.SetCallBack(() =>
                {
                    this.Search();
                });
                formReceiptItemsModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonItemCheck_Click(object sender, EventArgs e)
        {
            FormReceiptArrivalCheck formReceiptArrivalCheck = new FormReceiptArrivalCheck(receiptTicketID, 1,AllOrPartial.PARTIAL);
            formReceiptArrivalCheck.SetFinishedAction(() =>
            {
                this.Close();
                FormReceiptItems formReceiptItems2 = new FormReceiptItems(FormMode.ALTER, this.receiptTicketID);
                formReceiptItems2.Show();
            });
            formReceiptArrivalCheck.Show();
        }
        private void receiptItemToSubmissionItem(ReceiptTicketItem receiptTicketItem)
        {
            if (submissionTicket == null)
            {
                FormReceiptArrivalCheck formReceiptArrivalCheck = new FormReceiptArrivalCheck(this.receiptTicketID, 1,AllOrPartial.PARTIAL);
                formReceiptArrivalCheck.Show();
                this.submissionTicket = formReceiptArrivalCheck.submissionTicket;
                //submissionTicket = new SubmissionTicket();
            }
            SubmissionTicketItem submissionTicketItem = new SubmissionTicketItem();
            //submissionTicketItem.
            FormSubmissionTicketItemModify formSubmissionTicketItemModify = new FormSubmissionTicketItemModify(submissionTicket.ID, receiptTicketItem);
            formSubmissionTicketItemModify.Show();
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlReceiptItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int receiptItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == receiptItemID select rti).Single();
                if (receiptTicketItem.State == "送检中")
                {
                    MessageBox.Show("该条目已送检");
                }
                else
                { 
                    SubmissionTicketItem submissionTicketItem = ReceiptUtilities.ReceiptTicketItemToSubmissionTicketItem(receiptTicketItem, submissionTicket.ID);
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State = '送检中' WHERE ID = @receiptTicketItemID", new SqlParameter("receiptTicketItemID", receiptTicketItem.ID));
                    wmsEntities.SubmissionTicketItem.Add(submissionTicketItem);
                    wmsEntities.SaveChanges();
                    MessageBox.Show("成功");
                }
            }
            catch
            {
                MessageBox.Show("请选择一项送检", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void reoGridControlReceiptItems_Click(object sender, EventArgs e)
        {

        }
    }
}
