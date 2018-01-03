using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class JobTicketItemViewMetaData
    {
        public const string STRING_STATE_UNFINISHED = "未完成";
        public const string STRING_STATE_FINISHED = "已完成";

        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="JobTicketJobTicketNo",Name="关联作业单号",Editable=false,Save=false},
            //new KeyName(){Key="StockInfoID",Name="库存信息ID",Editable=true,Save=true},
            new KeyName(){Key="ComponentName",Name="零件名",Editable=false,Save=false},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false},
            new KeyName(){Key="Type",Name="任务类型"},
            new KeyName(){Key="MoveOutStorageLocation",Name="移出库位"},
            new KeyName(){Key="SourceBoardNo",Name="源托盘号"},
            new KeyName(){Key="MoveInStorageLocation",Name="移入库位"},
            new KeyName(){Key="State",Name="状态",
                         ComboBoxItems = new ComboBoxItem[]{
                             new ComboBoxItem(STRING_STATE_UNFINISHED),
                             new ComboBoxItem( STRING_STATE_FINISHED),
                         } },
            new KeyName(){Key="ScheduledMoveCount",Name="计划移位数量"},
            new KeyName(){Key="MoveCount",Name="实际移位数量"},
            new KeyName(){Key="HaveBackedCount",Name="已退拣数量"},
            new KeyName(){Key="ActualJobPerson",Name="实际作业人员"},
            new KeyName(){Key="HappenTime",Name="发生时间"},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
