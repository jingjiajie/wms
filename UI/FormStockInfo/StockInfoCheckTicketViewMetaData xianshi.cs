﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoCheckTicketViewMetaDataxianshi
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false ,Editable=false },
            new KeyName(){Key="CheckDate",Name="盘点日期",Visible=true    ,Editable=true  },
            new KeyName(){Key="CreateUserID",Name="创建用户ID",Visible=false ,Editable=false },
            new KeyName(){Key="CreateTime",Name="创建时间",Visible=true,Editable=true},
            new KeyName(){Key="LastUpdateUserID",Name="最后更新用户ID",Visible=false ,Editable=false },
            new KeyName(){Key="LastUpdateTime",Name="最后更新时间",Visible=true,Editable=true},
            new KeyName(){Key="ProjectID",Name="项目ID",Visible=false ,Editable=false },
            new KeyName(){Key="WarehouseID",Name="仓库ID",Visible=false ,Editable=false },
            new KeyName(){Key="CreateUserUsername",Name="创建用户名称",Visible=true   ,Editable=false },
            new KeyName(){Key="LastUpdateUserUsername",Name="最后更新用户名称",Visible=true   ,Editable=false },

        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
