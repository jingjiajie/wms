using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FormBase
{
    class BaseProjectMetaData
    {

        public static KeyName[] keyNames = {
            new KeyName(){Name = "ID", Key = "ID", Editable = false},
            new KeyName(){Name = "项目名称", Key = "Name"}

        };
        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
