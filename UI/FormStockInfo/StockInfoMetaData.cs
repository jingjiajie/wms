using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoMetaData
    {
        private static KeyName[] keyNames = {
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

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
