using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FormBase
{
    class UserMetaData
    {
        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="用户ID"},
            new KeyName(){Key="Username",Name="用户名",Editable=true},

            new KeyName(){Key="Password",Name="密码",Editable=true},
            new KeyName(){Key="Authority",Name="权限",Editable=true},
            new KeyName(){Key="AuthorityName",Name="权限名称",Editable=true},

            new KeyName(){Key="SupplierID",Name="供应商ID",Editable=true},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
