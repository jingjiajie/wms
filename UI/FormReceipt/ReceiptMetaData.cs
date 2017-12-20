using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.UI.FormReceipt
{

    public class ReceiptMetaData
    {
        public static KeyName[] receiptNameKeys =
        {
            new KeyName(){Name = "ID", Key = "ID", Visible = false, Editable = false},
            new KeyName(){Name = "项目ID", Key = "ProjectID",Visible = false, Editable = false},
            new KeyName(){Name = "仓库ID", Key = "Warehouse", Visible = false, Editable = false},
            new KeyName(){Name = "供应商ID", Key = "SupplierID", Visible = false, Editable = true},
            new KeyName(){Name = "单号", Key = "No", Visible = true, Editable = true},
            new KeyName(){Name = "单据类型", Key = "Type", Visible = true, Editable = true},
            new KeyName(){Name = "状态", Key = "State", Visible = true, Editable = false},
            new KeyName(){Name = "送货单号（SRM)", Key = "DeliverTicketNoSRM", Visible = true, Editable = true},
            new KeyName(){Name = "凭证来源", Key = "VoucherSource", Visible = true, Editable = true},
            new KeyName(){Name = "凭证号", Key = "VoucherNo", Visible = true, Editable = true},
            new KeyName(){Name = "凭证行号", Key = "VoucherLineNo", Visible = true, Editable = true},
            new KeyName(){Name = "凭证年", Key = "VoucherYear", Visible = true, Editable = true},
            new KeyName(){Name = "关联凭证号", Key = "ReletedVoucherNo", Visible = true, Editable = true},
            new KeyName(){Name = "关联凭证行号", Key = "ReletedVoucherLineNo", Visible = true, Editable = true},
            new KeyName(){Name = "关联凭证年", Key = "ReletedVoucherYear", Visible = true, Editable = true},
            new KeyName(){Name = "抬头文本", Key = "HeadingText", Visible = true, Editable = true},
            new KeyName(){Name = "过账日期", Key = "PostCountDate", Visible = true, Editable = true},
            new KeyName(){Name = "内向交货单号", Key = "InwardDeliverTicketNo", Visible = true, Editable = true},
            new KeyName(){Name = "内向交货行号", Key = "InwardDeliverLineNo", Visible = true, Editable = true},
            new KeyName(){Name = "外向交货单号", Key = "OutwardDeliverTicketNo", Visible = true, Editable = true},
            new KeyName(){Name = "外向交货行号", Key = "OutwardDeliverLineNo", Visible = true, Editable = true},
            new KeyName(){Name = "采购订单", Key = "PurchaseTicketNo", Visible = true, Editable = true},
            new KeyName(){Name = "采购订单行号", Key = "PurchaseTicketLineNo", Visible = true, Editable = true},
            new KeyName(){Name = "订单日期", Key = "OrderDate", Visible = true, Editable = true},
            new KeyName(){Name = "收货库位", Key = "ReceiptStorageLocation", Visible = true, Editable = true},
            new KeyName(){Name = "托盘号", Key = "BoardNo", Visible = true, Editable = true},
            new KeyName(){Name = "收货包装", Key = "ReceiptPackage", Visible = true, Editable = true},
            new KeyName(){Name = "期待数量", Key = "ExpectedAmount", Visible = true, Editable = true},
            new KeyName(){Name = "收货数量", Key = "ReceiptCount", Visible = true, Editable = true},
            new KeyName(){Name = "移动类型", Key = "MoveType", Visible = true, Editable = true},
            new KeyName(){Name = "单据来源", Key = "Source", Visible = true, Editable = true},
            new KeyName(){Name = "作业人员", Key = "AssignmentPerson", Visible = true, Editable = true},
            new KeyName(){Name = "是否过账", Key = "PostedCount", Visible = true, Editable = true},
            new KeyName(){Name = "箱号", Key = "BoxNo", Visible = true, Editable = true},
            new KeyName(){Name = "创建用户ID", Key = "CreateUserID", Visible = true, Editable = false},
            new KeyName(){Name = "创建时间", Key = "CreateTime", Visible = true, Editable = true},
            new KeyName(){Name = "最后修改用户ID", Key = "LastUpdateUserID", Visible = true, Editable = false},
            new KeyName(){Name = "最后修改时间", Key = "LastUpdateTime", Visible = true, Editable = true},
            new KeyName(){Name = "项目名称", Key = "ProjectName" , Visible = false, Editable = false, Save=false},
            new KeyName(){Name = "供货商名称", Key = "SupplierName" , Visible = false, Editable = false, Save=false},
            new KeyName(){Name = "仓库名", Key = "WarehouseName" , Visible = false, Editable = false, Save=false}
        };
        public static KeyName[] submissionTicketKeyName =
        {
            new KeyName(){Name="ID", Key="ID",Visible=false,Editable=false},
            new KeyName(){Name="收货单ID", Key="ReceiptTicketID",Visible=false,Editable=false},
            new KeyName(){Name="送检单单号", Key="No",Visible=true,Editable=true},
            new KeyName(){Name="状态", Key="State",Visible=true,Editable=false},
            new KeyName(){Name="有无自检报告", Key="HasSelfInspectionReport",Visible=true,Editable=true},
            new KeyName(){Name="送检员", Key="DeliverSubmissionPerson",Visible=true,Editable=true},
            new KeyName(){Name="质检接收", Key="ReceivePerson",Visible=true,Editable=true},
            new KeyName(){Name="质检员", Key="SubmissionPerson",Visible=true,Editable=true},
            new KeyName(){Name="库房返回", Key="Result",Visible=true,Editable=true},
            new KeyName(){Name="创建用户ID", Key="CreateUserID",Visible=true,Editable=false},
            new KeyName(){Name="创建日期", Key="CreateTime",Visible=true,Editable=true},
            new KeyName(){Name="最后修改用户ID", Key="LastUpdateUserID",Visible=true,Editable=false},
            new KeyName(){Name="最后修改时间", Key="LastUpdateTime",Visible=true,Editable=true}
        };

        public static KeyName[] itemsKeyName =
        {
            new KeyName(){Name="ID", Key="ID",Visible=false,Editable=false,Save=true},
            new KeyName(){Name="收货单ID", Key="ReceiptTicketID",Visible=false,Editable=false,Save=true},
            new KeyName(){Name="零件ID", Key="ComponentID",Visible=false,Editable=false,Save=true},
            new KeyName(){Name="零件编号", Key="ComponentNo", Visible = true, Editable = true, Save = false},
            new KeyName(){Name="零件名称", Key="ComponentName", Visible=true, Editable = true, Save = false},
            new KeyName(){Name="包装名称", Key="PackageName",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="期待包装数量", Key="ExpectedPackageAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="期待数量", Key="ExpectedAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="收货数量", Key="ReceiviptAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="错件数", Key="WrongComponentAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="短缺数量", Key="ShortageAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="不合格数量", Key="DisqualifiedAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="厂商批号", Key="ManufactureNo",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="存货日期", Key="InventoryDate",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="生产日期", Key="ManufactureDate",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="失效日期", Key="ExpiryDate",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="物权属性", Key="RealRightProperty",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="箱号", Key="BoxNo",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="零件名称", Key="ComponentName", Visible=false,Editable=false,Save=false},
            new KeyName(){Name="是否送检", Key="State", Visible=true, Editable=false,Save=true},
        };

        public static KeyName[] submissionTicketItemKeyName =
        {
            new KeyName(){Name="ID", Key="ID",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="送检单ID", Key="SubmissionTicketID",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="行项目", Key="LineItem",Visible=true,Editable=true,Save=true},
            //new KeyName(){Name="物料ID", Key="ComponentID",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="状态", Key="State",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="到货数量", Key="ArriveAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="单位", Key="Unit",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="送检数量", Key="SubmissionAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="返回数量", Key="ReturnAmount",Visible=true,Editable=true,Save=true},
            new KeyName(){Name="备注", Key="Comment",Visible=true,Editable=true,Save=true},
        };

        public static KeyName[] putawayTicketKeyName =
        {
            new KeyName{Name = "ID", Key = "ID", Visible = false, Editable = true, Save = true},
            new KeyName{Name = "单据号", Key = "No", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "单据类型", Key = "Type", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "收货单ID", Key = "ReceiptTicketID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "状态", Key = "State", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "计划移位数量", Key = "ScheduledDisplacementAmount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "实际移位数量", Key = "DisplacementAmount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "分配数量", Key = "DistributeAmount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "打印次数", Key = "PrintTimes", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "作业组名称", Key = "JobGroupName", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "创建用户ID", Key = "CreateUserID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "创建时间", Key = "CreateTime", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "最后操作员姓名", Key = "LastUpdateUserID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "最后更新时间", Key = "LastUpdateTime", Visible = true, Editable = true, Save = true}
        };

        public static KeyName[] putawayTicketItemKeyName =
        {
            new KeyName{Name = "ID", Key = "ID", Visible = true, Editable = false, Save = true},
            new KeyName{Name = "上架单ID", Key = "PutawayTicketID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "收货单条目ID", Key = "ReceiptTicketItemID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "移位库位编码", Key = "DisplacementPositionNo", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "目标库位", Key = "TargetStorageLocation", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "托盘号", Key = "BoardNo", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "状态", Key = "State", Visible = true, Editable = false, Save = true},
            new KeyName{Name = "计划移位数量", Key = "ScheduledMoveCount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "分配数量", Key = "DistrabuteCount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "移位数量", Key = "MoveCount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "实际作业人", Key = "OperatePerson", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "作业时间", Key = "OperateTime", Visible = true, Editable = true, Save = true},

        };

        public static KeyName[] supplierKeyName =
        {
            new KeyName{Name = "ID", Key = "ID", Visible = false, Editable = true, Save = true},
            new KeyName{Name = "供货商名称", Key = "Name", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "合同编码", Key = "ContractNo", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "起始有效日期", Key = "StartDate", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "结束有效日期", Key = "EndDate", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "开票日期", Key = "InvoiceDate", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "结算日期", Key = "BalanceDate", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "供货商全称", Key = "FullName", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "纳税人识别号", Key = "TaxpayerNumber", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "地址", Key = "Address", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "电话", Key = "Tel", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "开户行", Key = "BankName", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "帐号", Key = "BankAccount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "开户行行号", Key = "BankNo", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "邮编", Key = "ZipCode", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "收件人", Key = "RecipientName", Visible = true, Editable = true, Save = true}
        };

        public static KeyName[] componentKeyName =
        {
            new KeyName{Name = "ID", Key = "ID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "项目ID", Key = "ProjectID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "仓库ID", Key = "WarehouseID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "供货商ID", Key = "SupplierID", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "容器号", Key = "ContainerNo", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "工厂", Key = "Factroy", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "工位", Key = "WorkPosition", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "零件代号", Key = "No", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "零件名称", Key = "Name", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "A系列/B系列供应商", Key = "SupplierType", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "机型区分", Key = "Type", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "尺寸（大件/小件）", Key = "Size", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "分类", Key = "Category", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "分组责任人", Key = "GroupPrincipal", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "单台用量", Key = "SingleCarUsageAmount", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "物流服务费1", Key = "Charge1", Visible = true, Editable = true, Save = true},
            new KeyName{Name = "物流服务费2", Key = "Charge2", Visible = true, Editable = true, Save = true},
        };
    }
}