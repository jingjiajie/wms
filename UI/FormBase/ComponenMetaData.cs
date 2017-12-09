using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    public class ComponenMetaData
    {
        public static KeyName[] componenkeyNames = {
            new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false,Save=false},
            //new KeyName(){Name = "项目ID", Key = "ProjectID",Visible = false, Editable = true},
            //new KeyName(){Name = "仓库ID", Key = "WarehouseID", Visible = false, Editable = true},
            new KeyName(){Name = "供应商ID", Key = "SupplierID", Visible = false, Editable = true},
            new KeyName(){Key="ContainerNo",Name="容器号",Visible = true, Editable = true},
            new KeyName(){Key="Factroy",Name="工厂",Visible = true, Editable = true},
            new KeyName(){Key="WorkPosition",Name="工位",Visible = true, Editable = true},
            new KeyName(){Key="No",Name="零件代号",Visible = true, Editable = true},
            new KeyName(){Key="Name",Name="零件名称",Visible = true, Editable = true},
            new KeyName(){Key="SupplierType",Name="A系列/B系列供应商",Visible = true, Editable = true},
            new KeyName(){Key="Type",Name="机型区分",Visible = true, Editable = true},
            new KeyName(){Key="Size",Name="尺寸（大件/小件）",Visible = true, Editable = true},
            new KeyName(){Key="Category",Name="分类",Visible = true, Editable = true},
            new KeyName(){Key="GroupPrincipal",Name="分组负责人",Visible = true, Editable = true},
            new KeyName(){Key="SingleCarUsageAmount",Name="单台用量",Visible = true, Editable = true},
            new KeyName(){Key="Charge1",Name="物流服务费1",Visible = true, Editable = true},
            new KeyName(){Key="Charge2",Name="物流服务费2",Visible = true, Editable = true},
            new KeyName(){Key="InventoryRequirement1Day",Name="1天库存要求",Visible = true, Editable = true},
            new KeyName(){Key="InventoryRequirement3Day",Name="3天库存要求",Visible = true, Editable = true},
            new KeyName(){Key="InventoryRequirement5Day",Name="5天库存要求",Visible = true, Editable = true},
            new KeyName(){Key="InventoryRequirement10Day",Name="10天库存要求",Visible = true, Editable = true},
            //new KeyName(){Name = "项目名称", Key = "ProjectName" , Visible = false, Editable = false},
            //new KeyName(){Name = "供货商名称", Key = "SupplierName" , Visible = false, Editable = false},
            //new KeyName(){Name = "仓库名", Key = "WarehouseName" , Visible = false, Editable = false},

        };
        public static KeyName[] KeyNames { get => componenkeyNames; set => componenkeyNames = value; }
    }
}
