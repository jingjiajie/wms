using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class ComponenMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="ProjectID",Name="项目ID"},
            new KeyName(){Key="WarehouseID",Name="仓库ID"},
            new KeyName(){Key="SupplierID",Name="供货商ID"},
            new KeyName(){Key="ContainerNo",Name="容器号"},
            new KeyName(){Key="Factroy",Name="工厂"},
            new KeyName(){Key="WorkPosition",Name="工位"},
            new KeyName(){Key="No",Name="零件代号"},
            new KeyName(){Key="Name",Name="零件名称"},
            new KeyName(){Key="SupplierType",Name="A系列/B系列供应商"},
            new KeyName(){Key="Type",Name="机型区分"},
            new KeyName(){Key="Size",Name="尺寸（大件/小件）"},
            new KeyName(){Key="Category",Name="分类"},
            new KeyName(){Key="GroupPrincipal",Name="分组负责人"},
            new KeyName(){Key="SingleCarUsageAmount",Name="单台用量"},
            new KeyName(){Key="Charge1",Name="物流服务费1"},
            new KeyName(){Key="Charge2",Name="物流服务费2"},
            new KeyName(){Key="InventoryRequirement1Day",Name="1天库存要求"},
            new KeyName(){Key="InventoryRequirement3Day",Name="3天库存要求"},
            new KeyName(){Key="InventoryRequirement5Day",Name="5天库存要求"},
            new KeyName(){Key="InventoryRequirement10Day",Name="10天库存要求"},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
