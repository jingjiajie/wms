using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class FormMainMetaData
    {
        //版块和权限的对应关系
        public static FunctionAuthorities[] FunctionAuthorities = new FunctionAuthorities[]
        {
            MakeItem("用户管理",Authority.BASE_USER),
            MakeItem("供应商管理",Authority.BASE_SUPPLIER,Authority.BASE_SUPPLIER_SUPPLIER_SELFONLY),
            MakeItem("零件管理",Authority.BASE_COMPONENT),
            MakeItem("供货管理",Authority.BASE_SUPPLY,Authority.BASE_SUPPLIER_SUPPLIER_SELFONLY),
            MakeItem("人员管理",Authority.BASE_PERSON),
            MakeItem("其他",Authority.BASE_OTHER),

            MakeItem("到货单管理",Authority.RECEIPT_ARRIVAL,Authority.RECEIPT_ARRIVAL_SUPPLIER_SELFONLY),
            MakeItem("送检单管理",Authority.SUBMISSION_TICKET),
            MakeItem("上架单管理",Authority.RECEIPT_SHELVES),
            MakeItem("上架零件管理",Authority.RECEIPT_SHELVES),

            MakeItem("工作任务单管理",Authority.DELIVERY_SEND,Authority.DELIVERY_SEND_SUPPLIER_SELFONLY),
            MakeItem("翻包作业单管理",Authority.DELIVERY_JOB,Authority.DELIVERY_JOB_SUPPLIER_SELFONLY),
            MakeItem("出库单管理",Authority.DELIVERY_OUTPUT,Authority.DELIVERY_OUTPUT_SUPPLIER_SELFONLY),
        
            MakeItem("库存批次",Authority.STOCKINFO,Authority.STOCKINFO_SUPPLIER_SELFONLY),
            MakeItem("库存盘点",Authority.STOCK_CHECK),
        };

        //只要符合权限参数中的任意一个权限，即视为有相应版块访问权限
        private static FunctionAuthorities MakeItem(string functionName,params Authority[] posibleAuthorities)
        {
            return new FunctionAuthorities()
            {
                FunctionName = functionName,
                Authorities = posibleAuthorities
            };
        }
    }
}
