using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FormBase
{
    class BaseProjectMetaData
    {

     private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="Name",Name="项目名称"},
        };
            public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
        }
}
