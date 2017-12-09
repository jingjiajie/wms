using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class PutOutStorageTicketViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="No",Name="出库单号",Editable=false,Save=false},
            new KeyName(){Key="TruckLoadingTicketNo",Name="装车单号",Editable=true},
            //new KeyName(){Key="Source",Name="单据来源",Editable=true},
            new KeyName(){Key="WorkFlow",Name="作业流程",Editable=true},
            new KeyName(){Key="State",Name="状态"},
            new KeyName(){Key="CarNum",Name="车牌号",Editable=true},
            new KeyName(){Key="Driver",Name="司机",Editable=true},
            new KeyName(){Key="SerialNo",Name="序列号",Editable=true},
            new KeyName(){Key="OriginalTicketType",Name="原始单据类型",Editable=true},
            new KeyName(){Key="PullTicketNo",Name="拉动单号",Editable=true},
            new KeyName(){Key="CrossingNo",Name="道口编码",Editable=true},
            new KeyName(){Key="ReceiverNo",Name="收货方编码",Editable=true},
            new KeyName(){Key="SortTypeNo",Name="排序类型编码",Editable=true},
            new KeyName(){Key="TruckLoadingTime",Name="装车时间",Editable=true},
            new KeyName(){Key="DeliverTime",Name="发运时间",Editable=true},
            new KeyName(){Key="CreateUserUsername",Name="创建用户",Editable=false,Save=false},
            new KeyName(){Key="CreateTime",Name="创建时间"},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间"}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
