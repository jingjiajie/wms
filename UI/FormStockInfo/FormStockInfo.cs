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
        struct KeyAndName
        {
            public string Key;
            public string Name;
        }

        private KeyAndName[] keyAndNames = {
            //new KeyAndName(){Key="ID",Name="ID"},
            new KeyAndName(){Key="ComponentID",Name="零件ID"},
            new KeyAndName(){Key="ReceiptTicketID",Name="收货单ID"},
            new KeyAndName(){Key="StockDate",Name="存货日期"},
            new KeyAndName(){Key="ManufatureDate",Name="生产日期"},
            new KeyAndName(){Key="ExpireDate",Name="失效日期"},
            new KeyAndName(){Key="WarehouseArea",Name="库区"},
            new KeyAndName(){Key="TargetStorageLocation",Name="目标库位"},
            new KeyAndName(){Key="PackagingUnit",Name="包装单位"},
            new KeyAndName(){Key="ReceivingSpaceArea",Name="收货区 库位"},
            new KeyAndName(){Key="OverflowArea",Name="溢库区 库位"},
            new KeyAndName(){Key="ShipmentArea",Name="出库区 库位"},
            new KeyAndName(){Key="ReceivingSpaceAreaCount",Name="收货区数量"},
            new KeyAndName(){Key="OverflowAreaCount",Name="溢库区数量"},
            new KeyAndName(){Key="ShipmentAreaCount",Name="出货区数量"},
            new KeyAndName(){Key="PackagingToolCount",Name="翻包器具数量"},
            new KeyAndName(){Key="RecycleBoxCount",Name="回收箱体"},
            new KeyAndName(){Key="NonOrderAreaCount",Name="无订单区"},
            new KeyAndName(){Key="UnacceptedProductAreaCount",Name="不合格品区"},
            new KeyAndName(){Key="PlannedBoardCount",Name="规划拖位"},
            new KeyAndName(){Key="PlannedPackagingToolCount",Name="规划翻包器具数量"},
            new KeyAndName(){Key="BoardNo",Name="托盘号"},
            new KeyAndName(){Key="Batch",Name="批次"},
            new KeyAndName(){Key="StorageState",Name="库存状态"},
            new KeyAndName(){Key="ManufactureNo",Name="厂商批号"},
            new KeyAndName(){Key="ProjectInfo",Name="项目信息"},
            new KeyAndName(){Key="ProjectStageInfo",Name="项目阶段信息"},
            new KeyAndName(){Key="RealRightProperty",Name="物权属性"},
            new KeyAndName(){Key="CarModel",Name="车型"},
            new KeyAndName(){Key="BoxNo",Name="箱号"},
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
            string[] columnNames = (from kn in this.keyAndNames select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(columnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
            }
            worksheet.Columns = columnNames.Length;
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
                string key = (from kn in this.keyAndNames
                              where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                              select kn.Key).First();
                string value = this.textBoxSearchValue.Text;
                this.Search(key, value);
                return;
            }
        }

        private void Search(string key, string value)
        {
            var wmsEntities = new WMSEntities();

            StockInfo[] stockInfos = null;
            if (key==null || value == null) //查询条件为null则查询全部内容
            {
                stockInfos = wmsEntities.Database.SqlQuery<StockInfo>("SELECT * FROM StockInfo").ToArray();
            }
            else
            {
                double tmp;
                if (Double.TryParse(value, out tmp) == false) //不是数字则加上单引号
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
            this.reoGridControlMain.Invoke(new Action(()=>
            {
                var worksheet = this.reoGridControlMain.Worksheets[0];
                worksheet.DeleteRangeData(RangePosition.EntireRange);
                for (int i = 0; i < stockInfos.Length; i++)
                {
                    StockInfo curStockInfo = stockInfos[i];
                    object[] columns = Utilities.GetValuesByPropertieNames(curStockInfo, (from kn in this.keyAndNames select kn.Key).ToArray());
                    for (int j = 0; j < worksheet.Columns; j++)
                    {
                        worksheet[i, j] = columns[j];
                    }
                }
            }));
        }
    }
}
