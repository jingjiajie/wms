using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoCheckTicksModifyMetaDate
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="ComponentName",Name="零件",Editable=false,Save=false},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false},
            //new KeyName(){Key="ReceiptAreaAmount",Name="收货区数量",Editable=false ,Save=true},
            //new KeyName(){Key="SubmissionAreaAmount",Name="送检区数量",Editable=false ,Save=true},
            //new KeyName(){Key="OverflowAreaAmount",Name="溢库区数量",Editable=false ,Save=false },
            //new KeyName(){Key="ShipmentAreaAmount",Name="发货区数量",Editable=false ,Save=false },
            new KeyName(){Key="ExcpetedOverflowAreaAmount",Name="溢库区数量",Editable=false ,Save=false },
            new KeyName(){Key="ExpectedShipmentAreaAmount",Name="发货区数量",Editable=false ,Save=false },

            new KeyName(){Key="RealOverflowAreaAmount",Name="实际溢库区数量",Editable=true ,Save=true }
            ,
            new KeyName(){Key="RealShipmentAreaAmount",Name="实际发货区数量",Editable=true ,Save=true }
            ,


        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
