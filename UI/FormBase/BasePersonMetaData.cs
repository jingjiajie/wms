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
            new KeyName(){Key="Name",Name="ÈËÔ±ÐÕÃû",Visible=true,Editable=true},
        };
        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
