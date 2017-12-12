using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoCheckTickettModifyViewMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="CheckDate",Name="盘点日期",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="StockInfoID",Name="库存信息ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="DisplacementPositionNo",Name="移位库位编码",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="TargetStorageLocation",Name="目标库位",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="BoardNo",Name="托盘号",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="State",Name="状态",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="ScheduledMoveCount",Name="计划移位数量",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="DistrabuteCount",Name="分配数量",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="MoveCount",Name="移位数量",Visible=false,Editable=false,Save=false},





        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
