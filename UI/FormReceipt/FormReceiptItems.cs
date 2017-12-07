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

namespace WMS.UI
{
    public partial class FormReceiptItems : Form
    {
        private int receiptTicketItemsID;
        private FormMode formMode;
        private int receiptTicketID;
        Action callBack = null;
        public FormReceiptItems()
        {
            InitializeComponent();
        }

        public void SetCallback(Action action)
        {
            callBack = action;
        }

        public FormReceiptItems(FormMode formMode, int receiptTicketItemsID, int receiptTicketID)
        {
            InitializeComponent();
            this.receiptTicketItemsID = receiptTicketItemsID;
            this.formMode = formMode;
            this.receiptTicketID = receiptTicketID;
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
            worksheet.Columns = columnNames.Length;
        }

        private void FormReceiptArrivalItems_Load(object sender, EventArgs e)
        {
            InitComponents();
            WMSEntities wmsEntities = new WMSEntities();
            ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketItemsID select rt).Single();
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
                receiptTicketItemViews = wmsEntities.Database.SqlQuery<ReceiptTicketItemView>("SELECT * FROM ReceiptTicketItemView").ToArray();
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
    }
}
