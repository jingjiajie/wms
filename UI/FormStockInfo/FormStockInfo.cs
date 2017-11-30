using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using WMS.DataAccess;
using System.Threading;

namespace WMS.UI
{
    public partial class FormStockInfo : Form
    {
        class KeyName
        {
            public string Key;
            public string Name;
            public bool Visible = true;
        }

        private KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="ComponentID",Name="零件ID"},
            new KeyName(){Key="ReceiptTicketID",Name="收货单ID"},
            new KeyName(){Key="StockDate",Name="存货日期"},
            new KeyName(){Key="ManufatureDate",Name="生产日期"},
            new KeyName(){Key="ExpireDate",Name="失效日期"},
            new KeyName(){Key="WarehouseArea",Name="库区"},
            new KeyName(){Key="TargetStorageLocation",Name="目标库位"},
            new KeyName(){Key="PackagingUnit",Name="包装单位"},
            new KeyName(){Key="ReceivingSpaceArea",Name="收货区 库位"},
            new KeyName(){Key="OverflowArea",Name="溢库区 库位"},
            new KeyName(){Key="ShipmentArea",Name="出库区 库位"},
            new KeyName(){Key="ReceivingSpaceAreaCount",Name="收货区数量"},
            new KeyName(){Key="OverflowAreaCount",Name="溢库区数量"},
            new KeyName(){Key="ShipmentAreaCount",Name="出货区数量"},
            new KeyName(){Key="PackagingToolCount",Name="翻包器具数量"},
            new KeyName(){Key="RecycleBoxCount",Name="回收箱体"},
            new KeyName(){Key="NonOrderAreaCount",Name="无订单区"},
            new KeyName(){Key="UnacceptedProductAreaCount",Name="不合格品区"},
            new KeyName(){Key="PlannedBoardCount",Name="规划拖位"},
            new KeyName(){Key="PlannedPackagingToolCount",Name="规划翻包器具数量"},
            new KeyName(){Key="BoardNo",Name="托盘号"},
            new KeyName(){Key="Batch",Name="批次"},
            new KeyName(){Key="StorageState",Name="库存状态"},
            new KeyName(){Key="ManufactureNo",Name="厂商批号"},
            new KeyName(){Key="ProjectInfo",Name="项目信息"},
            new KeyName(){Key="ProjectStageInfo",Name="项目阶段信息"},
            new KeyName(){Key="RealRightProperty",Name="物权属性"},
            new KeyName(){Key="CarModel",Name="车型"},
            new KeyName(){Key="BoxNo",Name="箱号"},
        };

        public FormStockInfo()
        {
            InitializeComponent();
        }

        private void FormStockInfo_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search(null, null);
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in this.keyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < this.keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = this.keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = this.keyNames[i].Visible;
            }
            worksheet.Columns = this.keyNames.Length;
            Console.WriteLine("表格行数："+this.keyNames.Length);
        }

        private void reoGridControlMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(this.comboBoxSearchCondition.SelectedIndex == 0)
            {
                this.Search(null,null);
                return;
            }
            else
            {
                string key = (from kn in this.keyNames
                              where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                              select kn.Key).First();
                string value = this.textBoxSearchValue.Text;
                this.Search(key, value);
                return;
            }
        }

        private void Search(string key, string value)
        {
            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();

                StockInfo[] stockInfos = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    stockInfos = wmsEntities.Database.SqlQuery<StockInfo>("SELECT * FROM StockInfo").ToArray();
                }
                else
                {
                    if (Double.TryParse(value, out double tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        stockInfos = wmsEntities.Database.SqlQuery<StockInfo>(String.Format("SELECT * FROM StockInfo WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if(stockInfos.Length == 0)
                    {
                        worksheet[0, 0] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < stockInfos.Length; i++)
                    {
                        StockInfo curStockInfo = stockInfos[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curStockInfo, (from kn in this.keyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            if(worksheet.SelectionRange.Rows != 1)
            {
                MessageBox.Show("请选择一项进行修改","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int stockInfoID = Convert.ToInt32(worksheet[worksheet.SelectionRange.Row, 0]);
            new FormStockInfoModify(stockInfoID).Show();
        }
    }
}
