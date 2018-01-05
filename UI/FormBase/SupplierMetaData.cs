using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class SupplierMetaData
    {

       private static KeyName[]  keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible = false, Editable = false },
            new KeyName(){Key="Name",Name="供货商名称",Visible = true, Editable = true},
            //new KeyName(){Key="WarehouseID",Name="仓库ID"},
            new KeyName(){Key="ContractNo",Name="合同编码",Visible = true, Editable = true},
            new KeyName(){Key="StartDate",Name="起始有效日期",Visible = true, Editable = true},
            new KeyName(){Key="EndDate",Name="结束有效日期",Visible = true, Editable = true},
            new KeyName(){Key="InvoiceDelayMonth",Name="开票延迟月",Visible = true, Editable = true},
            new KeyName(){Key="BalanceDelayMonth",Name="结算延迟月",Visible = true, Editable = true},
            new KeyName(){Key="FullName",Name="供货商全称",Visible = true, Editable = true},
            new KeyName(){Key="TaxpayerNumber",Name="纳税人识别号",Visible = true, Editable = true},
            new KeyName(){Key="Address",Name="地址",Visible = true, Editable = true},
            new KeyName(){Key="Tel",Name="电话",Visible = true, Editable = true},
            new KeyName(){Key="BankName",Name="开户行",Visible = true, Editable = true},
            new KeyName(){Key="BankAccount",Name="帐号",Visible = true, Editable = true},
            new KeyName(){Key="BankNo",Name="开户行行号",Visible = true, Editable = true},
            new KeyName(){Key="ZipCode",Name="邮编",Visible = true, Editable = true},
            new KeyName(){Key="RecipientName",Name="收件人",Visible = true, Editable = true},
            new KeyName(){Key="Number",Name="编号",Visible = true, Editable = true},
            new KeyName(){Key="ContractState",Name="合同状态",Visible = true, Editable = true},
            new KeyName(){Key="IsHistory",Name="历史信息",Visible = false , Editable = false },
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
