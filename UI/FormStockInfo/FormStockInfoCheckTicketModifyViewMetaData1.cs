using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class StockInfoCheckTickettModifyViewMetaData1
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="ComponentName",Name="零件",Editable=false,Save=false},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false},
            new KeyName(){Key="ReceiptAreaAmount",Name="收货区数量",Editable=true,Save=true},
            new KeyName(){Key="SubmissionAreaAmount",Name="送检区数量",Editable=true,Save=true},
            new KeyName(){Key="OverflowAreaAmount",Name="溢库区数量",Editable=true,Save=true},
            new KeyName(){Key="ShipmentAreaAmount",Name="发货区数量",Editable=true,Save=true},
             new KeyName(){Key="RealOverflowAreaAmount",Name="实际溢库区数量",Editable=true,Save=true},
            new KeyName(){Key="RealShipmentAreaAmount",Name="实际发货区数量",Editable=true,Save=true},
            //new KeyName(){Key="ReceiptTicketNo",Name="收货单号",Editable=false,Save=false},
           // new KeyName(){Key="ReceiptTicketItemInventoryDate",Name="存货日期",Editable=false,Save=false},
           // new KeyName(){Key="ReceiptTicketItemManufactureDate",Name="生产日期",Editable=false,Save=false},
           // new KeyName(){Key="ReceiptTicketItemExpiryDate",Name="失效日期",Editable=false,Save=false},
            //new KeyName(){Key="ReceiptTicketItemPackageName",Name="包装",Editable=false,Save=false},
           // new KeyName(){Key="ReceiptTicketBoardNo",Name="托盘号",Editable=false,Save=false},
            //new KeyName(){Key="ReceiptTicketItemManufactureNo",Name="厂商批号",Editable=false,Save=false},
            //new KeyName(){Key="ProjectName",Name="项目",Editable=false,Save=false},
            //new KeyName(){Key="ReceiptTicketItemRealRightProperty",Name="物权属性",Editable=false,Save=false},
            //new KeyName(){Key="ReceiptTicketItemBoxNo",Name="箱号",Editable=false,Save=false},




        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
