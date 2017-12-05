using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FromShipmentTicket
{
    class ShipmentTicketViewMetaData
    {
        private static KeyName[] keyNames = {



            new KeyName(){Key="ID",Name="ID"},
            new KeyName(){Key="ProjectID",Name="项目ID"},
            new KeyName(){Key="WarehouseID",Name="仓库ID"},
            new KeyName(){Key="No",Name="单号"},
            new KeyName(){Key="Type",Name="单据类型名称"},
            new KeyName(){Key="TypeNo",Name="单据类型编码"},
            new KeyName(){Key="Source",Name="单据来源"},
            new KeyName(){Key="SegmentationChainCount",Name="分割链数"},
            new KeyName(){Key="RelatedTicketNo",Name="相关单号"},
            new KeyName(){Key="Date",Name="订单日期"},
            new KeyName(){Key="TicketNum",Name="第几单"},
            new KeyName(){Key="RequireArriveDate",Name="要求到达日期"},
            new KeyName(){Key="State",Name="状态"},
            new KeyName(){Key="ScheduledAmount",Name="计划数量"},
            new KeyName(){Key="AllocatedAmount",Name="分配数量"},
            new KeyName(){Key="PickingAmount",Name="拣货数量"},
            new KeyName(){Key="ShipmentAmount",Name="发货数量"},
            new KeyName(){Key="ExceedStorageAmount",Name="超库存数量"},
            new KeyName(){Key="Station",Name="工位"},
            new KeyName(){Key="ReverseTicketNo",Name="红冲单号"},
            new KeyName(){Key="SortType",Name="排序大类"},
            new KeyName(){Key="ProductionLine",Name="生产线"},
            new KeyName(){Key="ReceivingPersonName",Name="收货人姓名"},
            new KeyName(){Key="ContactAddress",Name="联系地址"},
            new KeyName(){Key="DeliveryPath",Name="配送线路"},
            new KeyName(){Key="Description",Name="描述"},
            new KeyName(){Key="CloseReason",Name="关闭原因"},
            new KeyName(){Key="CreateUserID",Name="创建用户ID"},
            new KeyName(){Key="CreateTime",Name="创建时间"},
            new KeyName(){Key="LastUpdateUserID",Name="最后修改用户ID"},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间"},
            new KeyName(){Key="PrintTimes",Name="打印次数 "},
            new KeyName(){Key="WaitingToBeDone",Name="是否待分配"},
            new KeyName(){Key="DeliveryTicketNo",Name="配送单号"},
            new KeyName(){Key="OuterPhysicalDistributionPath",Name="外物流路线 "},
            new KeyName(){Key="DeliveryPoint",Name="交货地点"},
            new KeyName(){Key="Emergency",Name="是否紧急"},
            new KeyName(){Key="ShipmentPlaceNo",Name="发货地编码"},
            new KeyName(){Key="BoardPrintedTimes",Name="看板打印次数"}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
