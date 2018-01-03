using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    public class ComponentShipmentInfoMetaData
    {
        public static KeyName[] ComponentShipmentInfokeyNames = {
            new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false,Save=false},
            new KeyName(){Key="BoxType",Name="箱型",Visible = true, Editable = true},
            new KeyName(){Key="BoxLength",Name="箱体长度",Visible = true, Editable = true},
            new KeyName(){Key="BoxWidth",Name="箱体宽度",Visible = true, Editable = true},
            new KeyName(){Key="BoxHeight",Name="箱体高度",Visible = true, Editable = true},
            new KeyName(){Key="UnitAmount",Name="单车（箱）数量",Visible = true, Editable = true},

        };
        public static KeyName[] KeyNames { get => ComponentShipmentInfokeyNames; set => ComponentShipmentInfokeyNames = value; }
    }
}
