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
            new KeyName(){Key="JobTicketJobTicketNo",Name="关联作业单号",Editable=false,Save=false},
            new KeyName(){Key="TruckLoadingTicketNo",Name="装车单号",Editable=true},
            new KeyName(){Key="State",Name="状态",ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem("装车中"),
                new ComboBoxItem("发运中"),
                new ComboBoxItem("发运成功"),
            } },
            new KeyName(){Key="CarNum",Name="车牌号",Editable=true},
            new KeyName(){Key="Driver",Name="司机",Editable=true},
            new KeyName(){Key="OriginalTicketType",Name="原始单据类型",Editable=true},
            new KeyName(){Key="ReceiverNo",Name="收货方编码",Editable=true},
            new KeyName(){Key="SortTypeNo",Name="排序类型编码",Editable=true},
            new KeyName(){Key="TruckLoadingTime",Name="装车时间",Editable=true},
            new KeyName(){Key="DeliverTime",Name="发运时间",Editable=true},
            new KeyName(){Key="PersonName",Name="责任人",Save=false},
            new KeyName(){Key="CreateUserUsername",Name="创建用户",Editable=false,Save=false},
            new KeyName(){Key="CreateTime",Name="创建时间",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间",Editable=false,Save=false}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
