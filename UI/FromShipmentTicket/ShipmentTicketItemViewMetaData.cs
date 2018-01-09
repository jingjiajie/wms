using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class ShipmentTicketItemViewMetaData
    {

        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            new KeyName(){Key="ComponentName",Name="零件",Save=false,EditPlaceHolder="点击选择库存零件"},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false,EditPlaceHolder="请选择零件"},
            new KeyName(){Key="ShipmentAmount",Name="发货数量",NotNull=true,EditPlaceHolder="必填项"},
            new KeyName(){Key="Unit",Name="单位",ComboBoxItems=new ComboBoxItem[0] },
            //new KeyName(){Key="UnitAmount",Name="单位数量",Editable=false,Save=false},
            new KeyName(){Key="ScheduledJobAmount",Name="已分配翻包数量",Editable=false,DefaultValueFunc=(()=>"0")},
            new KeyName(){Key="ReturnAmount",Name="退回数量（个）",DefaultValueFunc=(()=>"0")},
            new KeyName(){Key="ReturnReason",Name="退回原因"},
            new KeyName(){Key="OnlineTime",Name="上线时间",DefaultValueFunc=(()=>DateTime.Now.ToString()) },
            new KeyName(){Key="Description",Name="描述"},
            //new KeyName(){Key="RequirePackageNo",Name="需求包裹编号"},
            new KeyName(){Key="TargetPlace",Name="落点地址"},
            new KeyName(){Key="LookBoardCount",Name="看板数量"},
            new KeyName(){Key="JobPersonName",Name="实际作业人员",Save=false},
            new KeyName(){Key="ConfirmPersonName",Name="确认人",Save=false},
        };

        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
