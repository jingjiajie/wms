using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    public class ComponenViewMetaData
    {
        public static KeyName[] componenkeyNames = {
            new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false,Save=false},
            //new KeyName(){Name = "项目ID", Key = "ProjectID",Visible = false, Editable = false,Save=false},
            //new KeyName(){Name = "仓库ID", Key = "WarehouseID", Visible = false, Editable = false,Save=false},
            new KeyName(){Name = "供货商名称", Key = "SupplierName" , Visible = true, Editable = true,Save=false},
            new KeyName(){Name = "供货商编号", Key = "SupplierNumber" , Visible = true, Editable = true,Save=false},
            new KeyName(){Key="No",Name="零件代号",Visible = true, Editable = true},
            new KeyName(){Key="Name",Name="零件名称",Visible = true, Editable = true},
            new KeyName(){Key="ContainerNo",Name="容器号",Visible = true, Editable = true},
            new KeyName(){Key="Factroy",Name="工厂",Visible = true, Editable = true},
            new KeyName(){Key="WorkPosition",Name="工位",Visible = true, Editable = true},
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
            new KeyName(){Name = "创建用户", Key = "CreateUserUsername" , Visible = true, Editable = true,Save=false},
            new KeyName(){Name = "创建时间", Key = "CreateTime" , Visible = true, Editable = false,Save=false},
            new KeyName(){Name = "最后修改用户", Key = "LastUpdateUserUsername" , Visible = true, Editable = true,Save=false},
            new KeyName(){Name = "最后修改时间", Key = "LastUpdateTime" , Visible = true, Editable = false,Save=false},
            //new KeyName(){Name = "最新零件信息ID", Key = "NewestComponentID" , Visible = true, Editable = true,Save=true},
            new KeyName(){Name = "历史信息", Key = "IsHistory" , Visible = true, Editable = true,Save=true},

        };
        public static KeyName[] KeyNames { get => componenkeyNames; set => componenkeyNames = value; }
        public static KeyName[] pluscomponenkeyNames = {
            //new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false,Save=false},
            new KeyName(){Name = "项目ID", Key = "ProjectID",Visible = false, Editable = false,Save=false},
            new KeyName(){Name = "仓库ID", Key = "WarehouseID", Visible = false, Editable = false,Save=false},
            //new KeyName(){Name = "供货商名称", Key = "SupplierName" , Visible = true, Editable = true,Save=false},
            //new KeyName(){Name = "供货商编号", Key = "SupplierNumber" , Visible = true, Editable = true,Save=false},
            new KeyName(){Key="No",Name="零件代号",Visible = true, Editable = true},
            new KeyName(){Key="Name",Name="零件名称",Visible = true, Editable = true},
            new KeyName(){Key="ContainerNo",Name="容器号",Visible = true, Editable = true},
            new KeyName(){Key="Factroy",Name="工厂",Visible = true, Editable = true},
            new KeyName(){Key="WorkPosition",Name="工位",Visible = true, Editable = true},
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

            new KeyName(){Key="SingleBoxPhotoIndex",Name="单箱照片索引（单体，一拖现状）",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxPackagingBoxType",Name="单箱包装箱类型",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxLength",Name="单箱长",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxWidth",Name="单箱宽",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxHeight",Name="单箱高",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxSNP",Name="单箱SNP",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxRatedMinimumBoxCount",Name="单箱额定托拍对应小箱数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxWeight",Name="单箱重量",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxLayerCount",Name="单箱包装现状码放层数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxStorageCount",Name="单箱包装现状码放个数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxTheoreticalLayerCount",Name="单箱理论码放层数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxTheoreticalStorageHeight",Name="单箱理论码放高度",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxThroreticalStorageCount",Name="单箱理论码放个数",Visible = true, Editable = true},

            new KeyName(){Key="OuterPackingPhotoIndex",Name="外包装照片索引（标签，单包）",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingBoxType",Name="外包装包装箱类型",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingLength",Name="外包装长",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingWidth",Name="外包装宽",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingHeight",Name="外包装高",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingSNP",Name="外包装SNP",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingComment",Name="外包装备注（SNP）",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingRequiredLayers",Name="外包装包装要求码放层数",Visible = true, Editable = true},

            new KeyName(){Key="ShipmentInfoBoxType",Name="出货箱型",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoBoxLength",Name="出货箱体长度",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoBoxWidth",Name="出货箱体宽度",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoBoxHeight",Name="出货箱体高度",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoUnitAmount",Name="出货单车（箱）数量",Visible = true, Editable = true},

        };


        public static KeyName[] ComponentShipmentInfokeyNames = {
            new KeyName(){Key="ShipmentInfoBoxType",Name="出货箱型",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoBoxLength",Name="出货箱体长度",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoBoxWidth",Name="出货箱体宽度",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoBoxHeight",Name="出货箱体高度",Visible = true, Editable = true},
            new KeyName(){Key="ShipmentInfoUnitAmount",Name="出货单车（箱）数量",Visible = true, Editable = true},

        };
        public static KeyName[] KeyNames1 { get => ComponentShipmentInfokeyNames; set => ComponentShipmentInfokeyNames = value; }

        public static KeyName[] ComponentSingleBoxTranPackingInfokeyNames = {
            new KeyName(){Key="SingleBoxPhotoIndex",Name="单箱照片索引（单体，一拖现状）",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxPackagingBoxType",Name="单箱包装箱类型",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxLength",Name="单箱长",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxWidth",Name="单箱宽",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxHeight",Name="单箱高",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxSNP",Name="单箱SNP",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxRatedMinimumBoxCount",Name="单箱额定托拍对应小箱数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxWeight",Name="单箱重量",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxLayerCount",Name="单箱包装现状码放层数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxStorageCount",Name="单箱包装现状码放个数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxTheoreticalLayerCount",Name="单箱理论码放层数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxTheoreticalStorageHeight",Name="单箱理论码放高度",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxThroreticalStorageCount",Name="单箱理论码放个数",Visible = true, Editable = true},


        };
        public static KeyName[] KeyNames2 { get => ComponentSingleBoxTranPackingInfokeyNames; set => ComponentSingleBoxTranPackingInfokeyNames = value; }

        public static KeyName[] ComponentOuterPackingSizekeyNames = {
            new KeyName(){Key="OuterPackingPhotoIndex",Name="外包装照片索引（标签，单包）",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingBoxType",Name="外包装包装箱类型",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingLength",Name="外包装长",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingWidth",Name="外包装宽",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingHeight",Name="外包装高",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingSNP",Name="外包装SNP",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingComment",Name="外包装备注（SNP）",Visible = true, Editable = true},
            new KeyName(){Key="OuterPackingRequiredLayers",Name="外包装包装要求码放层数",Visible = true, Editable = true},


        };
        public static KeyName[] KeyNames3 { get => ComponentOuterPackingSizekeyNames; set => ComponentOuterPackingSizekeyNames = value; }


    }
}
