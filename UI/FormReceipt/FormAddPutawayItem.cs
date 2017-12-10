using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using unvell.ReoGrid;
using System.Windows.Forms;
using System.Threading;
using WMS.DataAccess;

namespace WMS.UI.FormReceipt
{
    public partial class FormAddPutawayItem : Form
    {
        private int putawayTicketID;
        private int receiptTicketID;
        public FormAddPutawayItem()
        {
            InitializeComponent();
        }
        public FormAddPutawayItem(int putawayTicketID, int receiptTicketID)
        {
            InitializeComponent();
            this.putawayTicketID = putawayTicketID;
            this.receiptTicketID = receiptTicketID;
        }

        private void Search()
        {
            //this.labelStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                ReceiptTicketItemView[] receiptTicketItemViews = null;
                receiptTicketItemViews = wmsEntities.Database.SqlQuery<ReceiptTicketItemView>(String.Format("SELECT * FROM ReceiptTicketItemView WHERE ReceiptTicketID = {0}", this.receiptTicketID)).ToArray();
                this.reoGridControlUser.Invoke(new Action(() =>
                {
                    //this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlUser.Worksheets[0];
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

        private void InitComponents()
        {
            //初始化
            string[] columnNames = (from kn in ReceiptMetaData.itemsKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.itemsKeyName[i].Visible;
            }
            //worksheet.ColumnHeaders[columnNames.Length].Text = "是否送检";
            worksheet.Columns = columnNames.Length;
        }

        private void FormAddPutawayItem_Load(object sender, EventArgs e)
        {

        }
    }
}
