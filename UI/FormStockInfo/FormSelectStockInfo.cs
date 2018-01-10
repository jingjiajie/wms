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
    public partial class FormSelectStockInfo : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        private int defaultStockInfoID = -1;
        private Action<int> selectFinishCallback = null;

        static int staticSelectedID = -1;
        static int staticComboBoxSelectedIndex = 0;
        static string staticSearchCondition = "";
        static Point staticLocation = new Point(-1,-1);
        

        public FormSelectStockInfo(int defaultStockInfoID = -1)
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
            this.comboBoxSearchCondition.SelectedIndex = staticComboBoxSelectedIndex;
            this.textBoxSearchContition.Text = staticSearchCondition;
            if(staticLocation != new Point(-1, -1))
            {
                this.Location = staticLocation;
            }
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            this.reoGridControlMain.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);
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
            if (this.defaultStockInfoID != -1)
            {
                try
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    this.comboBoxSearchCondition.SelectedIndex = 1; //自动选中零件编号选项
                    this.textBoxSearchContition.Text = (from s in wmsEntities.StockInfoView where s.ID == defaultStockInfoID select s.SupplyNo).FirstOrDefault();
                    this.Search(defaultStockInfoID);
                }
                catch
                {
                    MessageBox.Show("加载数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
            else
            {
                this.Search(staticSelectedID);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search(int selectID = -1)
        {
            string value = this.textBoxSearchContition.Text;
            string key = this.comboBoxSearchCondition.SelectedItem.ToString();
            this.labelStatus.Text = "正在搜索...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.DeleteRangeData(RangePosition.EntireRange);
            worksheet[0, 2] = "正在搜索中...";

            new Thread(new ThreadStart(()=>
            {
                StockInfoView[] stockInfoViews = null;
                try
                {
                    if (key == "供货编号")
                    {
                        stockInfoViews = (from s in this.wmsEntities.StockInfoView
                                          where s.SupplyNo == value
                                          orderby s.ReceiptTicketItemManufactureDate ascending,
                                                    s.ReceiptTicketItemInventoryDate ascending,
                                                    s.ReceiptTicketItemExpiryDate descending
                                          select s).ToArray();
                    }
                    else if (key == "零件名称")
                    {
                        stockInfoViews = (from s in this.wmsEntities.StockInfoView
                                          where s.ComponentName.Contains(value)
                                          orderby s.ReceiptTicketItemManufactureDate ascending,
                                                    s.ReceiptTicketItemInventoryDate ascending,
                                                    s.ReceiptTicketItemExpiryDate descending
                                          select s).ToArray();
                    }
                    else
                    {
                        MessageBox.Show("内部错误，无法识别查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("查询失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(()=>
                {
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
                    if (selectID != -1)
                    {
                        Utilities.SelectLineByID(this.reoGridControlMain, selectID);
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
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项");
                return;
            }
            this.selectFinishCallback?.Invoke(ids[0]);
            this.Close();
        }

        private void textBoxComponentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.buttonSearch.PerformClick();
            }
        }

        private void FormSelectStockInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            staticComboBoxSelectedIndex = this.comboBoxSearchCondition.SelectedIndex;
            staticSearchCondition = this.textBoxSearchContition.Text;
            staticLocation = this.Location;
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(ids.Length > 0)
            {
                staticSelectedID = ids[0];
            }
        }
    }
}
