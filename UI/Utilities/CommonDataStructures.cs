using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI
{
    public class KeyName
    {
        public string Key;
        public string Name;
        public bool Visible = true;
        public bool NotNull = false;
        public bool NotNegative = false;
        public bool Positive = false;
        public Func<object,object> Translator = null;
        public Func<string> DefaultValueFunc = null;
        public bool Editable = true;
        public ComboBoxItem[] ComboBoxItems = null;
        public string GetAllValueToComboBox = null;
        public string EditPlaceHolder = null;
        public bool Save = true;
        public bool ImportVisible = true;
        public bool ImportEditable = false;
        public bool Import = true;
    }

    public enum FormMode
    {
        ADD,ALTER,CHECK
    }

    public enum AllOrPartial
    {
        ALL, PARTIAL
    }

    public enum Authority
    {
        SUBMISSION_TICKET = 67108864,
        SUBMISSION_TICKET_SUPPLIER_SELFONLY = 33554432,
        SETTLEMENT = 16777216,
        SETTLEMENT_SUPPLIER_SELFONLY = 8388608,
        STOCK_CHECK = 4194304,
        STOCK_CHECK_SUPPLIER_SELFONLY = 2097152,
        STOCKINFO = 1048576,
        STOCKINFO_SUPPLIER_SELFONLY = 524288,
        DELIVERY_SEND = 262144,
        DELIVERY_SEND_SUPPLIER_SELFONLY = 131072,
        DELIVERY_OUTPUT = 65536,
        DELIVERY_OUTPUT_SUPPLIER_SELFONLY = 32768,
        DELIVERY_JOB = 16384,
        DELIVERY_JOB_SUPPLIER_SELFONLY = 8192,
        RECEIPT_SHELVES = 4096,
        RECEIPT_SHELVES_SUPPLIER_SELFONLY = 2048,
        RECEIPT_ARRIVAL = 1024,
        RECEIPT_ARRIVAL_SUPPLIER_SELFONLY = 512,
        BASE_USER = 256,
        BASE_OTHER = 128,
        BASE_COMPONENT = 32,
        BASE_PERSON = 8,
        BASE_SUPPLIER = 4,
        BASE_SUPPLIER_SUPPLIER_SELFONLY = 2,
        BASE_SUPPLY = 268435456,
        BASE_SUPPLY_SUPPLIER_SELFONLY = 134217728,
    }

    public class FunctionAuthorities
    {
        public string FunctionName;
        public Authority[] Authorities;
    }

    public class ComboBoxItem
    {
        public string Text = "";
        public object Value = null;
        public ComboBoxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }
        public ComboBoxItem(object value)
        {
            Text = value.ToString();
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
    public enum SupplierOrComponent
    {
        SUPPLIER, COMPONENT
    }

    public interface IFormSelect
    {
        void SetSelectFinishedCallback(Action<int> callback);
    }
}
