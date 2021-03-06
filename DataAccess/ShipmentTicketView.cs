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
    
    public partial class ShipmentTicketView
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int WarehouseID { get; set; }
        public string No { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> RequireArriveDate { get; set; }
        public string State { get; set; }
        public string Station { get; set; }
        public string ReceivingPersonName { get; set; }
        public string ContactAddress { get; set; }
        public string DeliveryPath { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> LastUpdateUserID { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<int> PrintTimes { get; set; }
        public string DeliveryTicketNo { get; set; }
        public string OuterPhysicalDistributionPath { get; set; }
        public Nullable<int> Emergency { get; set; }
        public string ShipmentPlaceNo { get; set; }
        public Nullable<int> BoardPrintedTimes { get; set; }
        public string ProjectName { get; set; }
        public string WarehouseName { get; set; }
        public string CreateUserUsername { get; set; }
        public string CreateUserPassword { get; set; }
        public Nullable<int> CreateUserAuthority { get; set; }
        public string CreateUserAuthorityName { get; set; }
        public Nullable<int> CreateUserSupplierID { get; set; }
        public string LastUpdateUserUsername { get; set; }
        public string LastUpdateUserPassword { get; set; }
        public Nullable<int> LastUpdateUserAuthority { get; set; }
        public string LastUpdateUserAuthorityName { get; set; }
        public Nullable<int> LastUpdateUserSupplierID { get; set; }
        public string Number { get; set; }
        public string ReturnTicketNo { get; set; }
        public Nullable<System.DateTime> ReturnTicketDate { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> PersonID { get; set; }
        public string PersonName { get; set; }
    }
}
