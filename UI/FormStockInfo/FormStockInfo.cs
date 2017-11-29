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

namespace WMS.UI
{
    struct KeyAndName
    {
        public string Key;
        public string Name;
    }

    public partial class FormStockInfo : Form
    {
        private KeyAndName[] keyAndNames = {
            new KeyAndName(){Key="ID",Name="ID"},
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
            string key = (from kn in this.keyAndNames
                          where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                          select kn.Key).First();
            string value = this.textBoxSearchValue.Text;
            this.Search(key,value);
        }

        private void Search(string key,string value)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            var wmsEntities = new WMSEntities();
            double tmp;
            if(Double.TryParse(value,out tmp) == false) //不是数字则加上单引号
            {
                value = "'" + value + "'";
            }
            var stockInfos = wmsEntities.Database.SqlQuery<StockInfo>(String.Format("SELECT * FROM StockInfo WHERE {0} = {1}",key,value)).ToArray();

            worksheet.DeleteRangeData(RangePosition.EntireRange);
            for (int i = 0; i < stockInfos.Length; i++)
            {
                StockInfo curStockInfo = stockInfos[i];
                object[] columns = { curStockInfo.ID, curStockInfo.ComponentID, curStockInfo.ReceiptTicketID, curStockInfo.StockDate, curStockInfo.ManufatureDate, curStockInfo.ExpireDate, curStockInfo.WarehouseArea, curStockInfo.TargetStorageLocation, curStockInfo.PackagingUnit, curStockInfo.ReceivingSpaceArea, curStockInfo.OverflowArea, curStockInfo.ShipmentArea, curStockInfo.ReceivingSpaceAreaCount, curStockInfo.OverflowAreaCount, curStockInfo.ShipmentAreaCount, curStockInfo.PackagingToolCount, curStockInfo.RecycleBoxCount, curStockInfo.NonOrderAreaCount, curStockInfo.UnacceptedProductAreaCount, curStockInfo.PlannedBoardCount, curStockInfo.PlannedPackagingToolCount, curStockInfo.BoardNo, curStockInfo.Batch, curStockInfo.StorageState, curStockInfo.ManufactureNo, curStockInfo.ProjectInfo, curStockInfo.ProjectStageInfo, curStockInfo.RealRightProperty, curStockInfo.CarModel, curStockInfo.BoxNo };
                for(int j = 0; j < worksheet.Columns; j++)
                {
                    worksheet[i, j] = columns[j];
                }
            }
        }
    }
}
