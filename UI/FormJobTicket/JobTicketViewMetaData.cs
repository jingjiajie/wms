using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class JobTicketViewMetaData
    {
        public const string STRING_STATE_UNFINISHED = "未完成";
        public const string STRING_STATE_FINISHED = "已完成";

        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false},
            new KeyName(){Key="JobTicketNo",Name="作业单号",Editable=false,Save=false},
            new KeyName(){Key="ShipmentTicketNo",Name="关联发货单号",Visible=true,Editable=false,Save=false},
            new KeyName(){Key="JobType",Name="作业类型",ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem("翻包"),
                new ComboBoxItem("其他")
            } },
            new KeyName(){Key="JobGroupName",Name="作业组名称"},
            new KeyName(){Key="ScheduledAmount",Name="计划作业数量"},
            new KeyName(){Key="RealAmount",Name="实际作业数量"},
            new KeyName(){Key="State",Name="状态",Editable=false,Save=false},
            new KeyName(){Key="PrintedTimes",Name="打印次数"},
            new KeyName(){Key="AssignmentArea",Name="作业指派区"},
            new KeyName(){Key="PersonInCharge",Name="责任人"},
            new KeyName(){Key="CreateUserUsername",Name="创建用户",Editable=false,Save=false},
            new KeyName(){Key="CreateTime",Name="创建时间",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户",Editable=false,Save=false},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间",Editable=false,Save=false}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
