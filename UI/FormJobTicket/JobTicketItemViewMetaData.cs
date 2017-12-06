using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class JobTicketItemViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="No",Name="编号",Editable=true},
            new KeyName(){Key="JobTicketJobTicketNo",Name="作业单号",Editable=true},
            new KeyName(){Key="ComponentName",Name="零件名"},
            new KeyName(){Key="SupplierName",Name="供应商"},
            new KeyName(){Key="Type",Name="任务类型",Editable=true},
            new KeyName(){Key="MoveOutStorageLocation",Name="移出库位",Editable=true},
            new KeyName(){Key="SourceBoardNo",Name="源托盘号",Editable=true},
            new KeyName(){Key="MoveInStorageLocation",Name="移入库位",Editable=true},
            new KeyName(){Key="State",Name="状态",Editable=true},
            new KeyName(){Key="ScheduledMoveCount",Name="计划移位数量",Editable=true},
            new KeyName(){Key="MoveCount",Name="实际移位数量",Editable=true},
            new KeyName(){Key="HaveBackedCount",Name="已退拣数量",Editable=true},
            new KeyName(){Key="ActualJobPerson",Name="实际作业人员",Editable=true},
            new KeyName(){Key="HappenTime",Name="发生时间",Editable=true},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
