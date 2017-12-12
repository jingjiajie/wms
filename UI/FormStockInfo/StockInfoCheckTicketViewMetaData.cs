using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoCheckTicketViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="StockCheckTicketID",Name="盘点单号",Visible=true ,Editable=true},
            new KeyName(){Key="CreateTime",Name="创建时间",Visible=true,Editable=true,Save=false},
            new KeyName(){Key="CreateUserUsername",Name="创建用户",Visible=true,Editable=true,Save=false},
            new KeyName(){Key="PersonInCharge",Name="责任人",Visible=true,Editable=true,Save=false},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户",Visible=true,Editable=true,Save=false},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间",Visible=true,Editable=true,Save=false},
            new KeyName(){Key="State",Name="状态",Visible=true,Editable=true,Save=false},

        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
