using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FromShipmentTicket
{
    class ShipmentTicketViewMetaData
    {
        private static KeyName[] keyNames = {



            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="ProjectID",Name="项目ID",Editable=true},
            new KeyName(){Key="WarehouseID",Name="仓库ID",Editable=true},
            new KeyName(){Key="No",Name="单号",Editable=true},
            new KeyName(){Key="Type",Name="单据类型名称",Editable=true},
            new KeyName(){Key="TypeNo",Name="单据类型编码",Editable=true},
            new KeyName(){Key="Source",Name="单据来源",Editable=true},
            new KeyName(){Key="SegmentationChainCount",Name="分割链数",Editable=true},
            new KeyName(){Key="RelatedTicketNo",Name="相关单号",Editable=true},
            new KeyName(){Key="Date",Name="订单日期",Editable=true},
            new KeyName(){Key="TicketNum",Name="第几单",Editable=true},
            new KeyName(){Key="RequireArriveDate",Name="要求到达日期",Editable=true},
            new KeyName(){Key="State",Name="状态"},
            new KeyName(){Key="ScheduledAmount",Name="计划数量",Editable=true},
            new KeyName(){Key="AllocatedAmount",Name="分配数量",Editable=true},
            new KeyName(){Key="PickingAmount",Name="拣货数量",Editable=true},
            new KeyName(){Key="ShipmentAmount",Name="发货数量",Editable=true},
            new KeyName(){Key="ExceedStorageAmount",Name="超库存数量",Editable=true},
            new KeyName(){Key="Station",Name="工位",Editable=true},
            new KeyName(){Key="ReverseTicketNo",Name="红冲单号",Editable=true},
            new KeyName(){Key="SortType",Name="排序大类",Editable=true},
            new KeyName(){Key="ProductionLine",Name="生产线",Editable=true},
            new KeyName(){Key="ReceivingPersonName",Name="收货人姓名",Editable=true},
            new KeyName(){Key="ContactAddress",Name="联系地址",Editable=true},
            new KeyName(){Key="DeliveryPath",Name="配送线路",Editable=true},
            new KeyName(){Key="Description",Name="描述",Editable=true},
            new KeyName(){Key="CloseReason",Name="关闭原因",Editable=true},
            new KeyName(){Key="CreateUserID",Name="创建用户ID"},
            new KeyName(){Key="CreateTime",Name="创建时间"},
            new KeyName(){Key="LastUpdateUserID",Name="最后修改用户ID"},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间"},
            new KeyName(){Key="PrintTimes",Name="打印次数 "},
            new KeyName(){Key="WaitingToBeDone",Name="是否待分配",Editable=true},
            new KeyName(){Key="DeliveryTicketNo",Name="配送单号",Editable=true},
            new KeyName(){Key="OuterPhysicalDistributionPath",Name="外物流路线 ",Editable=true},
            new KeyName(){Key="DeliveryPoint",Name="交货地点",Editable=true},
            new KeyName(){Key="Emergency",Name="是否紧急",Editable=true},
            new KeyName(){Key="ShipmentPlaceNo",Name="发货地编码",Editable=true},
            new KeyName(){Key="BoardPrintedTimes",Name="看板打印次数",Editable=true}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
