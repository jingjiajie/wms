using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    public class ComponentSingleBoxTranPackingInfoMetaData
    {
        public static KeyName[] ComponentSingleBoxTranPackingInfokeyNames = {
            new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false,Save=false},
            new KeyName(){Key="PhotoIndex",Name="照片索引（单体，一拖现状）",Visible = true, Editable = true},
            new KeyName(){Key="PackagingBoxType",Name="包装箱类型",Visible = true, Editable = true},
            new KeyName(){Key="Length",Name="长",Visible = true, Editable = true},
            new KeyName(){Key="Width",Name="宽",Visible = true, Editable = true},
            new KeyName(){Key="Height",Name="高",Visible = true, Editable = true},
            new KeyName(){Key="SNP",Name="SNP",Visible = true, Editable = true},
            new KeyName(){Key="RatedMinimumBoxCount",Name="额定托拍对应小箱数",Visible = true, Editable = true},
            new KeyName(){Key="SingleBoxWeight",Name="单箱重量",Visible = true, Editable = true},
            new KeyName(){Key="LayerCount",Name="包装现状码放层数",Visible = true, Editable = true},
            new KeyName(){Key="StorageCount",Name="包装现状码放个数",Visible = true, Editable = true},
            new KeyName(){Key="TheoreticalLayerCount",Name="理论码放层数",Visible = true, Editable = true},
            new KeyName(){Key="TheoreticalStorageHeight",Name="理论码放高度",Visible = true, Editable = true},
            new KeyName(){Key="ThroreticalStorageCount",Name="理论码放个数",Visible = true, Editable = true},


        };
        public static KeyName[] KeyNames { get => ComponentSingleBoxTranPackingInfokeyNames; set => ComponentSingleBoxTranPackingInfokeyNames = value; }
    }
}
