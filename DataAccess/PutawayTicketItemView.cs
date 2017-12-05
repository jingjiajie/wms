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
    
    public partial class PutawayTicketItemView
    {
        public int ID { get; set; }
        public int PutawayTicketID { get; set; }
        public int ReceiptTicketItemID { get; set; }
        public string DisplacementPositionNo { get; set; }
        public string TargetStorageLocation { get; set; }
        public string BoardNo { get; set; }
        public string State { get; set; }
        public Nullable<decimal> ScheduledMoveCount { get; set; }
        public Nullable<decimal> DistrabuteCount { get; set; }
        public Nullable<decimal> MoveCount { get; set; }
        public string OperatePerson { get; set; }
        public string OperateTime { get; set; }
        public string PutawayTicketNo { get; set; }
        public string PutawayTicketType { get; set; }
        public Nullable<int> PutawayTicketReceiptTicketID { get; set; }
        public string PutawayTicketState { get; set; }
        public Nullable<decimal> PutawayTicketScheduledDisplacementAmount { get; set; }
        public Nullable<decimal> PutawayTicketDisplacementAmount { get; set; }
        public Nullable<decimal> PutawayTicketDistributeAmount { get; set; }
        public Nullable<decimal> PutawayTicketPrintTimes { get; set; }
        public string PutawayTicketJobGroupName { get; set; }
        public Nullable<int> PutawayTicketCreateUserID { get; set; }
        public Nullable<System.DateTime> PutawayTicketCreateTime { get; set; }
        public Nullable<int> PutawayTicketLastUpdateUserID { get; set; }
        public Nullable<System.DateTime> PutawayTicketLastUpdateTime { get; set; }
        public Nullable<int> ReceiptTicketItemReceiptTicketID { get; set; }
        public Nullable<int> ReceiptTicketItemComponentID { get; set; }
        public string ReceiptTicketItemPackageName { get; set; }
        public Nullable<decimal> ReceiptTicketItemExpectedPackageAmount { get; set; }
        public Nullable<decimal> ReceiptTicketItemExpectedAmount { get; set; }
        public Nullable<decimal> ReceiptTicketItemReceiviptAmount { get; set; }
        public Nullable<decimal> ReceiptTicketItemWrongComponentAmount { get; set; }
        public Nullable<decimal> ReceiptTicketItemShortageAmount { get; set; }
        public Nullable<decimal> ReceiptTicketItemDisqualifiedAmount { get; set; }
        public string ReceiptTicketItemManufactureNo { get; set; }
        public Nullable<System.DateTime> ReceiptTicketItemInventoryDate { get; set; }
        public Nullable<System.DateTime> ReceiptTicketItemManufactureDate { get; set; }
        public Nullable<System.DateTime> ReceiptTicketItemExpiryDate { get; set; }
        public string ReceiptTicketItemRealRightProperty { get; set; }
        public string ReceiptTicketItemBoxNo { get; set; }
        public Nullable<int> ReceiptTicketProjectID { get; set; }
        public Nullable<int> ReceiptTicketWarehouseID { get; set; }
        public Nullable<int> ReceiptTicketSupplierID { get; set; }
        public string ReceiptTicketNo { get; set; }
        public string ReceiptTicketType { get; set; }
        public string ReceiptTicketState { get; set; }
        public string ReceiptTicketDeliverTicketNoSRM { get; set; }
        public string ReceiptTicketVoucherSource { get; set; }
        public string ReceiptTicketVoucherNo { get; set; }
        public string ReceiptTicketVoucherLineNo { get; set; }
        public Nullable<System.DateTime> ReceiptTicketVoucherYear { get; set; }
        public string ReceiptTicketReletedVoucherNo { get; set; }
        public string ReceiptTicketReletedVoucherLineNo { get; set; }
        public Nullable<System.DateTime> ReceiptTicketReletedVoucherYear { get; set; }
        public string ReceiptTicketHeadingText { get; set; }
        public Nullable<System.DateTime> ReceiptTicketPostCountDate { get; set; }
        public string ReceiptTicketInwardDeliverTicketNo { get; set; }
        public string ReceiptTicketInwardDeliverLineNo { get; set; }
        public string ReceiptTicketOutwardDeliverTicketNo { get; set; }
        public string ReceiptTicketOutwardDeliverLineNo { get; set; }
        public string ReceiptTicketPurchaseTicketNo { get; set; }
        public string ReceiptTicketPurchaseTicketLineNo { get; set; }
        public Nullable<System.DateTime> ReceiptTicketOrderDate { get; set; }
        public string ReceiptTicketReceiptStorageLocation { get; set; }
        public string ReceiptTicketBoardNo { get; set; }
        public string ReceiptTicketReceiptPackage { get; set; }
        public Nullable<decimal> ReceiptTicketExpectedAmount { get; set; }
        public Nullable<decimal> ReceiptTicketReceiptCount { get; set; }
        public string ReceiptTicketMoveType { get; set; }
        public string ReceiptTicketSource { get; set; }
        public string ReceiptTicketAssignmentPerson { get; set; }
        public Nullable<int> ReceiptTicketPostedCount { get; set; }
        public string ReceiptTicketBoxNo { get; set; }
        public Nullable<int> ReceiptTicketCreateUserID { get; set; }
        public Nullable<System.DateTime> ReceiptTicketCreateTime { get; set; }
        public Nullable<int> ReceiptTicketLastUpdateUserID { get; set; }
        public Nullable<System.DateTime> ReceiptTicketLastUpdateTime { get; set; }
        public string ProjectName { get; set; }
        public string WarehouseName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierContractNo { get; set; }
        public Nullable<System.DateTime> SupplierStartDate { get; set; }
        public Nullable<System.DateTime> SupplierEndDate { get; set; }
        public Nullable<System.DateTime> SupplierInvoiceDate { get; set; }
        public Nullable<System.DateTime> SupplierBalanceDate { get; set; }
        public string SupplierFullName { get; set; }
        public string SupplierTaxpayerNumber { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierTel { get; set; }
        public string SupplierBankName { get; set; }
        public string SupplierBankAccount { get; set; }
        public string SupplierBankNo { get; set; }
        public string SupplierZipCode { get; set; }
        public string SupplierRecipientName { get; set; }
        public string ComponentContainerNo { get; set; }
        public string ComponentFactroy { get; set; }
        public string ComponentWorkPosition { get; set; }
        public string ComponentNo { get; set; }
        public string ComponentName { get; set; }
        public string ComponentSupplierType { get; set; }
        public string ComponentType { get; set; }
        public string ComponentSize { get; set; }
        public string ComponentCategory { get; set; }
        public string ComponentGroupPrincipal { get; set; }
        public Nullable<decimal> ComponentSingleCarUsageAmount { get; set; }
        public Nullable<decimal> ComponentCharge1 { get; set; }
        public Nullable<decimal> ComponentCharge2 { get; set; }
        public Nullable<decimal> ComponentInventoryRequirement1Day { get; set; }
        public Nullable<decimal> ComponentInventoryRequirement3Day { get; set; }
        public Nullable<decimal> ComponentInventoryRequirement5Day { get; set; }
        public Nullable<decimal> ComponentInventoryRequirement10Day { get; set; }
    }
}
