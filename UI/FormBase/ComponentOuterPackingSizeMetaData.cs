using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    public class ComponentOuterPackingSizeMetaData
    {
        public static KeyName[] ComponentSingleBoxTranPackingInfokeyNames = {
            new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false,Save=false},
            new KeyName(){Key="PhotoIndex",Name="照片索引（标签，单包）",Visible = true, Editable = true},
            new KeyName(){Key="BoxType",Name="包装箱类型",Visible = true, Editable = true},
            new KeyName(){Key="Length",Name="长",Visible = true, Editable = true},
            new KeyName(){Key="Width",Name="宽",Visible = true, Editable = true},
            new KeyName(){Key="Height",Name="高",Visible = true, Editable = true},
            new KeyName(){Key="SNP",Name="SNP",Visible = true, Editable = true},
            new KeyName(){Key="Comment",Name="备注（SNP）",Visible = true, Editable = true},
            new KeyName(){Key="RequiredLayers",Name="包装要求码放层数",Visible = true, Editable = true},


        };
        public static KeyName[] KeyNames { get => ComponentSingleBoxTranPackingInfokeyNames; set => ComponentSingleBoxTranPackingInfokeyNames = value; }
    }
}
