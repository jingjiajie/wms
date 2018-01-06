using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class JobTicketViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false},
            new KeyName(){Key="JobTicketNo",Name="作业单号",EditPlaceHolder="留空自动生成"},
            new KeyName(){Key="ShipmentTicketNo",Name="关联发货单号",Editable=false,Save=false},
            new KeyName(){Key="JobType",Name="作业类型",ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem("翻包"),
                new ComboBoxItem("其他")
            } },
            new KeyName(){Key="JobGroupName",Name="作业组名称"},
            new KeyName(){Key="State",Name="状态",ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem("未完成"),
                new ComboBoxItem("已完成"),
            } },
            new KeyName(){Key="PrintedTimes",Name="打印次数"},
            new KeyName(){Key="AssignmentArea",Name="作业指派区"},
            new KeyName(){Key="PersonName",Name="责任人",Save=false},
            new KeyName(){Key="CreateUserUsername",Name="创建用户",Editable=false,Save=false},
            new KeyName(){Key="CreateTime",Name="创建时间",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间",Editable=false,Save=false}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
