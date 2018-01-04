using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class ShipmentTicketViewMetaData
    {
        public const string STRING_STATE_NOT_ASSIGNED = "未分配";
        public const string STRING_STATE_WAITING_PUTOUT = "待出库";
        public const string STRING_STATE_DELIVERING = "送货中";
        public const string STRING_STATE_FINISHED = "已完成";
        public const string STRING_STATE_CANCELED = "作废";

        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            //new KeyName(){Key="ProjectName",Name="项目",Editable=false,Save=false},
            //new KeyName(){Key="WarehouseName",Name="仓库",Editable=false,Save=false},
            new KeyName(){Key="Number",Name="编号",EditPlaceHolder="留空自动生成"},
            new KeyName(){Key="No",Name="发货单号",EditPlaceHolder="留空自动生成"},
            new KeyName(){Key="Type",Name="出库类型",Editable=true},
            new KeyName(){Key="Date",Name="订单日期",Editable=true},
            new KeyName(){Key="RequireArriveDate",Name="要求到达日期",Editable=true},
            new KeyName(){Key="State",Name="状态",Editable=true,ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem(STRING_STATE_NOT_ASSIGNED),
                new ComboBoxItem(STRING_STATE_WAITING_PUTOUT),
                new ComboBoxItem(STRING_STATE_DELIVERING),
                new ComboBoxItem(STRING_STATE_FINISHED),
                new ComboBoxItem(STRING_STATE_CANCELED)
             } },
            new KeyName(){Key="ReturnTicketNo",Name="回单号"},
            new KeyName(){Key="ReturnTicketDate",Name="回单时间"},
            new KeyName(){Key="Station",Name="工位",Editable=true},
            new KeyName(){Key="ReceivingPersonName",Name="收货人姓名",Editable=true},
            new KeyName(){Key="ContactAddress",Name="联系地址",Editable=true},
            new KeyName(){Key="Source",Name="单据来源",Editable=true},
            new KeyName(){Key="DeliveryPath",Name="配送线路",Editable=true},
            new KeyName(){Key="Description",Name="描述",Editable=true},
            new KeyName(){Key="DeliveryTicketNo",Name="配送单号",Editable=true},
            new KeyName(){Key="OuterPhysicalDistributionPath",Name="外物流路线",Editable=true},
            new KeyName(){Key="DeliveryPoint",Name="出库目的地",Editable=true},
            new KeyName(){Key="Emergency",Name="是否紧急",Editable=true},
            new KeyName(){Key="ShipmentPlaceNo",Name="发货地编码",Editable=true},
            //new KeyName(){Key="PrintTimes",Name="打印次数"},
            //new KeyName(){Key="BoardPrintedTimes",Name="看板打印次数",Editable=true},
            new KeyName(){Key="CreateUserUsername",Name="创建用户",Editable=false,Save=false},
            new KeyName(){Key="CreateTime",Name="创建时间",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间",Editable=false,Save=false},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
