using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID"},
            new KeyName(){Key="PutawayTicketItemID",Name="上架单条目ID",Visible=false,Editable=true},
            new KeyName(){Key="ComponentName",Name="零件名称"},
            new KeyName(){Key="ReceiptTicketNo",Name="收货单号"},
            new KeyName(){Key="ReceiptTicketItemInventoryDate",Name="存货日期"},
            new KeyName(){Key="ReceiptTicketItemManufactureDate",Name="生产日期"},
            new KeyName(){Key="ReceiptTicketItemExpiryDate",Name="失效日期"},
            new KeyName(){Key="PutawayTicketItemTargetStorageLocation",Name="目标库位"},
            new KeyName(){Key="ReceiptTicketItemPackageName",Name="包装"},
            //new KeyName(){Key="ReceivingSpaceArea",Name="收货区 库位"},
            //new KeyName(){Key="OverflowArea",Name="溢库区 库位"},
            //new KeyName(){Key="ShipmentArea",Name="出库区 库位"},
            //new KeyName(){Key="ReceivingSpaceAreaCount",Name="收货区数量"},
            //new KeyName(){Key="OverflowAreaCount",Name="溢库区数量"},
            //new KeyName(){Key="ShipmentAreaCount",Name="出货区数量"},
            //new KeyName(){Key="PackagingToolCount",Name="翻包器具数量"},
            //new KeyName(){Key="RecycleBoxCount",Name="回收箱体"},
            //new KeyName(){Key="NonOrderAreaCount",Name="无订单区"},
            //new KeyName(){Key="UnacceptedProductAreaCount",Name="不合格品区"},
            //new KeyName(){Key="PlannedBoardCount",Name="规划拖位"},
            //new KeyName(){Key="PlannedPackagingToolCount",Name="规划翻包器具数量"},
            new KeyName(){Key="ReceiptTicketBoardNo",Name="托盘号"},
            //new KeyName(){Key="Batch",Name="批次"},
            new KeyName(){Key="PutawayTicketItemState",Name="上架状态"},
            new KeyName(){Key="ReceiptTicketItemManufactureNo",Name="厂商批号"},
            new KeyName(){Key="ProjectName",Name="项目"},
            new KeyName(){Key="ReceiptTicketItemRealRightProperty",Name="物权属性"},
            //new KeyName(){Key="CarModel",Name="车型"},
            new KeyName(){Key="ReceiptTicketItemBoxNo",Name="箱号"},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
