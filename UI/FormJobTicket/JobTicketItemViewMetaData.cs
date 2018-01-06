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
            new KeyName(){Key="State",Name="状态",
                         ComboBoxItems = new ComboBoxItem[]{
                             new ComboBoxItem(STRING_STATE_UNFINISHED),
                             new ComboBoxItem( STRING_STATE_FINISHED),
                         } },
            new KeyName(){Key="ScheduledAmount",Name="计划翻包数量",NotNull=true},
            new KeyName(){Key="RealAmount",Name="实际翻包数量"},
            new KeyName(){Key="Unit",Name="翻包单位",DefaultValueFunc=(()=>"个")},
            new KeyName(){Key="JobPersonName",Name="实际作业人员",Save=false,EditPlaceHolder="点击选择人员"},
            new KeyName(){Key="ConfirmPersonName",Name="确认人",Save=false,EditPlaceHolder="点击选择人员"},
            new KeyName(){Key="HappenTime",Name="发生时间",DefaultValueFunc=(()=>DateTime.Now.ToString())},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
