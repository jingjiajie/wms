using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class PutOutStorageTicketItemViewMetaData
    {
        public const string STRING_STATE_WAIT_FOR_LOAD = "待装车";
        public const string STRING_STATE_PART_LOADED = "部分装车";
        public const string STRING_STATE_ALL_LOADED = "全部装车";

        private static KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false,Editable=false,Save=false},
            //new KeyName(){Key="StockInfoID",Name="库存信息ID",Editable=true},
            //new KeyName(){Key="PutOutStorageTicketNo",Name="出库单号",Editable=false,Save=false},
            new KeyName(){Key="ComponentName",Name="零件",Editable=false,Save=false},
            new KeyName(){Key="SupplierName",Name="供应商",Editable=false,Save=false},
            new KeyName(){Key="ScheduledAmount",Name="装车数量",Editable=false},
                        new KeyName(){Key="State",Name="状态",Editable=false,ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem(STRING_STATE_WAIT_FOR_LOAD),
                new ComboBoxItem(STRING_STATE_PART_LOADED),
                new ComboBoxItem(STRING_STATE_ALL_LOADED)
            } },
            new KeyName(){Key="RealAmount",Name="实际装车数量"},
            new KeyName(){Key="ExceedStockAmount",Name="超库存数量",Editable=true},
            new KeyName(){Key="Unit",Name="单位",Editable=false,Save=false },
            new KeyName(){Key="UnitAmount",Name="单位数量",Editable=false,Save=false},
            new KeyName(){Key="ReturnAmount",Name="退回数量",EditPlaceHolder="退回时填写"},
            new KeyName(){Key="ReturnUnit",Name="退回单位",EditPlaceHolder="退回时填写"},
            new KeyName(){Key="ReturnUnitAmount",Name="退回单位数量",EditPlaceHolder="退回时填写"},
            new KeyName(){Key="ReturnType",Name="退回类型",Editable=false,ComboBoxItems=new ComboBoxItem[]{
                new ComboBoxItem("正品退回"),
                new ComboBoxItem("不良品退回")
            } },
            new KeyName(){Key="ReturnTime",Name="退回时间",EditPlaceHolder="退回时生成"},
            new KeyName(){Key="LoadingTime",Name="装车时间",Editable=true,DefaultValueFunc=(()=>DateTime.Now.ToString())},
            new KeyName(){Key="JobPersonName",Name="实际作业人员",Save=false,EditPlaceHolder="点击选择人员"},
            new KeyName(){Key="ConfirmPersonName",Name="确认人",Save=false,EditPlaceHolder="点击选择人员"},
        };


        public static KeyName[] KeyNames { get => keyNames; set => keyNames = value; }
    }
}
