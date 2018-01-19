using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FormBase
{
    class BasePersonMetaData
    {

        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false},
            new KeyName(){Key="Name",Name="人员姓名",Visible=true,Editable=true},
            new KeyName(){Key="Position",Name="岗位",Visible=true,Editable=true},
        };
        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
