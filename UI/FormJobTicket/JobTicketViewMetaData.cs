using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class JobTicketViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID"},
            new KeyName(){Key="ShipmentTicketID",Name="发货单ID"},
            new KeyName(){Key="JobTicketNo",Name="作业单号"},
            new KeyName(){Key="JobType",Name="作业类型"},
            new KeyName(){Key="JobGroupName",Name="作业组名称"},
            new KeyName(){Key="ScheduledAmount",Name="计划作业数量"},
            new KeyName(){Key="RealAmount",Name="实际作业数量"},
            new KeyName(){Key="State",Name="状态"},
            new KeyName(){Key="PrintedTimes",Name="打印次数"},
            new KeyName(){Key="AssignmentArea",Name="作业指派区"},
            new KeyName(){Key="PersonInCharge",Name="责任人"},
            new KeyName(){Key="CreateUserUsername",Name="创建用户"},
            new KeyName(){Key="CreateTime",Name="创建时间"},
            new KeyName(){Key="LastUpdateUserUsername",Name="最后修改用户"},
            new KeyName(){Key="LastUpdateTime",Name="最后修改时间"}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
