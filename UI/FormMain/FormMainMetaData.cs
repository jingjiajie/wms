using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI
{
    class FormMainMetaData
    {
        public static FunctionAuthorities[] FunctionAuthorities = new FunctionAuthorities[]
        {
            MakeItem("用户管理",Authority.BASE_USER,Authority.BASE_USER_SUPPLIER_SELFONLY),
            MakeItem("供应商管理",Authority.BASE_SUPPLIER,Authority.BASE_SUPPLIER_SUPPLIER_SELFONLY),
            MakeItem("零件管理",Authority.BASE_COMPONENT,Authority.BASE_COMPONENT_SUPPLIER_SELFONLY),
            MakeItem("其他",Authority.BASE_WAREHOUSE| Authority.BASE_PROJECT,
                Authority.BASE_WAREHOUSE_SUPPLIER_SELFONLY | Authority.BASE_PROJECT_SUPPLIER_SELFONLY),
            
            //先建立以进行调试

            MakeItem("到货管理",Authority.RECEIPT_ARRIVAL,Authority.RECEIPT_ARRIVAL_SUPPLIER_SELFONLY),
            MakeItem("上架管理",Authority.RECEIPT_SHELVES,Authority.RECEIPT_SHELVES_SUPPLIER_SELFONLY),

            MakeItem("发货单管理",Authority.DELIVERY_SEND,Authority.DELIVERY_SEND_SUPPLIER_SELFONLY),
            MakeItem("作业单管理",Authority.DELIVERY_JOB,Authority.DELIVERY_JOB_SUPPLIER_SELFONLY),
            MakeItem("出库单管理",Authority.DELIVERY_OUTPUT,Authority.DELIVERY_OUTPUT_SUPPLIER_SELFONLY),
        
            MakeItem("库存信息",Authority.STOCKINFO,Authority.STOCKINFO_SUPPLIER_SELFONLY),
            MakeItem("库存盘点",Authority.STOCK_CHECK,Authority.STOCK_CHECK_SUPPLIER_SELFONLY),

            MakeItem("送检单管理",Authority.SUBMISSION_TICKET,Authority.SUBMISSION_TICKET_SUPPLIER_SELFONLY),
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
