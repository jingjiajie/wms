using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using unvell.ReoGrid;
using System.Threading;

namespace WMS.UI
{
    public partial class FormSelectSupplier : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        private int defaultStockInfoID = -1;
        private Action<int> selectFinishCallback = null;

        public FormSelectSupplier(int defaultStockInfoID = -1)
        {
            InitializeComponent();
            this.defaultStockInfoID = defaultStockInfoID;
        }

        public void SetSelectFinishCallback(Action<int> selectFinishedCallback)
        {
            this.selectFinishCallback = selectFinishedCallback;
        }

        private void InitComponents()
        {
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < StockInfoViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = StockInfoViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = StockInfoViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = StockInfoViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void FormJobTicketSelectStockInfo_Load(object sender, EventArgs e)
        {
            InitComponents();
            if(this.defaultStockInfoID != -1)
            {
                WMSEntities wmsEntities = new WMSEntities();
                this.textBoxComponentNo.Text = (from s in wmsEntities.StockInfoView where s.ID == defaultStockInfoID select s.ComponentNo).FirstOrDefault();
                this.Search();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search()
        {
            string ComponentNo = this.textBoxComponentNo.Text;
            this.labelStatus.Text = "正在搜索...";
            new Thread(new ThreadStart(()=>
            {
                StockInfoView[] stockInfoViews = (from s in this.wmsEntities.StockInfoView
                                                  where s.ComponentNo == ComponentNo
                                                  orderby s.ReceiptTicketItemManufactureDate ascending,
                                                            s.ReceiptTicketItemInventoryDate ascending,
                                                            s.ReceiptTicketItemExpiryDate descending
                                                  select s).ToArray();
                this.Invoke(new Action(()=>
                {
                    var worksheet = this.reoGridControlMain.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    for (int i = 0; i < stockInfoViews.Length; i++)
                    {
                        StockInfoView curStockInfoView = stockInfoViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curStockInfoView, (from kn in StockInfoViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                    if (stockInfoViews.Length == 0)
                    {
                        worksheet[0, 2] = "没有查询到符合条件的记录";
                    }
                    this.labelStatus.Text = "搜索完成";
                }));
            })).Start();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            this.SelectItem();
        }

        private void SelectItem()
        {
            int[] ids = this.GetSelectedIDs();
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项");
                return;
            }
            this.selectFinishCallback?.Invoke(ids[0]);
            this.Close();
        }

        private int[] GetSelectedIDs()
        {
            List<int> ids = new List<int>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for (int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
            {
                if (worksheet[row, 0] == null) continue;
                if (int.TryParse(worksheet[row, 0].ToString(), out int shipmentTicketID))
                {
                    ids.Add(shipmentTicketID);
                }
            }
            return ids.ToArray();
        }

        private void textBoxComponentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.buttonSearch.PerformClick();
            }
        }
    }
}
