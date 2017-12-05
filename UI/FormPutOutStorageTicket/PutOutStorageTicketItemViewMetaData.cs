using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.PutOutStorageTicket
{
    class PutOutStorageTicketItemViewMetaData
    {

            private static KeyName[] keyNames = {

            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="StockInfoID",Name="库存信息ID",Editable=true},
            new KeyName(){Key="PutOutStorageTicketID",Name="出库单ID",Editable=true},
            new KeyName(){Key="Amount",Name="装车单号",Editable=true},
            new KeyName(){Key="ExceedStockAmount",Name="超库存数量",Editable=true}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
