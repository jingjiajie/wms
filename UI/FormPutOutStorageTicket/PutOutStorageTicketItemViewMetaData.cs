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
            new KeyName(){Key="PutOutStorageTicketNo",Name="出库单号",Editable=false,Save=false},
            new KeyName(){Key="ComponentName",Name="零件",Editable=false,Save=false},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false},
            new KeyName(){Key="Amount",Name="装车单号",Editable=true},
            new KeyName(){Key="ExceedStockAmount",Name="超库存数量",Editable=true}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
