//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipmentTicketItemView
    {
        public int ID { get; set; }
        public int ShipmentTicketID { get; set; }
        public Nullable<int> StockInfoID { get; set; }
        public Nullable<decimal> ShipmentAmount { get; set; }
        public Nullable<System.DateTime> OnlineTime { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string RequirePackageNo { get; set; }
        public string TargetPlace { get; set; }
        public string InnerShipmentPath { get; set; }
        public Nullable<decimal> LookBoardCount { get; set; }
        public string Unit { get; set; }
        public string ProjectName { get; set; }
        public string WarehouseName { get; set; }
        public Nullable<int> ReceiptTicketItemReceiptTicketID { get; set; }
        public Nullable<int> ReceiptTicketItemComponentID { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> ComponentProjectID { get; set; }
        public Nullable<int> ComponentWarehouseID { get; set; }
        public string ComponentNo { get; set; }
        public string ComponentName { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public string ReturnReason { get; set; }
        public string ShipmentTicketNumber { get; set; }
        public string ShipmentTicketNo { get; set; }
        public string ComponentNumber { get; set; }
        public string SupplierNumber { get; set; }
        public Nullable<int> JobPersonID { get; set; }
        public Nullable<int> ConfirmPersonID { get; set; }
        public string JobPersonName { get; set; }
        public string ConfirmPersonName { get; set; }
        public Nullable<decimal> ScheduledJobAmount { get; set; }
        public Nullable<decimal> UnitAmount { get; set; }
    }
}
