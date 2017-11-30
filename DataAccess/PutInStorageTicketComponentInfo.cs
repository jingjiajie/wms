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
    
    public partial class PutInStorageTicketComponentInfo
    {
        public int ID { get; set; }
        public int WarehouseID { get; set; }
        public int ComponentID { get; set; }
        public int ReceiptTicketID { get; set; }
        public string ComponentNo { get; set; }
        public string ComponentName { get; set; }
        public string PackageName { get; set; }
        public Nullable<decimal> ExpectedPackageAmount { get; set; }
        public Nullable<decimal> ExpectedAmount { get; set; }
        public Nullable<decimal> ReceiviptAmount { get; set; }
        public Nullable<decimal> WrongComponentAmount { get; set; }
        public Nullable<decimal> ShortageAmount { get; set; }
        public Nullable<decimal> DisqualifiedAmount { get; set; }
        public Nullable<System.DateTime> InventoryDate { get; set; }
        public string ReceiptNo { get; set; }
        public string ManufactureNo { get; set; }
        public Nullable<System.DateTime> ManufactureDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public int SupplierID { get; set; }
        public string Supplier { get; set; }
        public string ProjectInfo { get; set; }
        public string ProjectPhaseInfo { get; set; }
        public string RealRightProperty { get; set; }
        public string BoxNo { get; set; }
    
        public virtual Component Component { get; set; }
        public virtual ReceiptTicket ReceiptTicket { get; set; }
        public virtual Supplier Supplier1 { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}