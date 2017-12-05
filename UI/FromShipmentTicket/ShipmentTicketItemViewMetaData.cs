using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FromShipmentTicket
{
    class ShipmentTicketItemViewMetaData
    {

        private static KeyName[] keyNames = {


            new KeyName(){Key="ID",Name="ID"},
            new KeyName(){Key="ShipmentTicketID",Name="发货单ID"},
            new KeyName(){Key="StockInfoID",Name="库存信息ID "},
            new KeyName(){Key="ExpectedShipmentAmount",Name="预计发货数量"},
            new KeyName(){Key="AssignedAmount",Name="分配数量"},
            new KeyName(){Key="PickingAmount",Name="拣货数量"},
            new KeyName(){Key="ShipmentInAdvanceAmount",Name="预发货数量"},
            new KeyName(){Key="ShipmentAmount",Name="发运数量"},
            new KeyName(){Key="ExceedStockAmount",Name="超库存数量"},
            new KeyName(){Key="OnlineTime",Name="上线时间"},
            new KeyName(){Key="Description",Name="描述"},
            new KeyName(){Key="RequirePackageNo",Name="需求包裹编号"},
            new KeyName(){Key="TargetPlace",Name="落点地址"},
            new KeyName(){Key="InnerShipmentPath",Name="内物流路线"},
            new KeyName(){Key="LookBoardCount",Name="看板数量"},
            new KeyName(){Key="Unit",Name="基本单位"}
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
