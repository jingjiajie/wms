using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class PutOutStorageTicketItemViewMetaData
    {

            private static KeyName[] keyNames = {

            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            //new KeyName(){Key="StockInfoID",Name="库存信息ID",Editable=true},
            //new KeyName(){Key="PutOutStorageTicketNo",Name="出库单号",Editable=false,Save=false},
            new KeyName(){Key="ComponentName",Name="零件",Editable=false,Save=false},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false},
            new KeyName(){Key="ScheduledAmount",Name="计划数量"},
            new KeyName(){Key="RealAmount",Name="实际数量"},
            new KeyName(){Key="ExceedStockAmount",Name="超库存数量",Editable=true},
            new KeyName(){Key="Unit",Name="单位",Editable=false,Save=false },
            new KeyName(){Key="State",Name="状态",ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem("待出库"),
                new ComboBoxItem("装车"),
                new ComboBoxItem("发运中"),
                new ComboBoxItem("发运成功"),
            } },
            new KeyName(){Key="JobPersonName",Name="实际作业人员",Save=false},
            new KeyName(){Key="ConfirmPersonName",Name="确认人",Save=false},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
