using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using WMS.DataAccess;
using WMS.UI.FormReceipt;
using unvell.ReoGrid.Events;
using System.Threading;
using System.Reflection;

namespace WMS.UI
{
    public partial class FormImport : Form
    {
        private string[] columnName;
        public FormImport()
        {
            InitializeComponent();
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            InitComponents();

        }

        private void InitComponents()
        {
            //初始化
            //string[] columnNames = (from kn in ReceiptMetaData.itemsKeyName select kn.Name).ToArray();
            //初始化表格
            string[] columnNames = (from kn in ReceiptMetaData.itemsKeyName where kn.Visible = true && kn.Editable == true && (kn.Key != "SupplyNo" && kn.Key != "ComponentName") select kn.Name).ToArray();
            List<string> columnNamesList = columnNames.ToList();
            columnNamesList.Insert(0, "零件编号/名称");
            columnNamesList.Insert(0, "零件ID");
            columnNames = columnNamesList.ToArray();
            this.columnName = columnNames;
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Cell;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                //worksheet.ColumnHeaders[i].IsVisible = columnName[i].Visible;
                //worksheet.ColumnHeaders[i].IsVisible = columnNamees[i].Visible;
            }
            //worksheet.ColumnHeaders[columnNames.Length].Text = "是否送检";
            worksheet.ColumnHeaders[0].IsVisible = false;
            worksheet.Columns = columnNames.Length;
            worksheet.AfterCellEdit += test;
        }

        private void test(object sender, CellAfterEditEventArgs e)
        {
            if (e.Cell.Position.Col == 1)
            {
                Worksheet worksheet = this.reoGridControlMain.Worksheets[0];
                string date = e.NewData.ToString();
                if (date == null)
                {
                    return;
                }
                new Thread(() =>
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    SupplyView[] supplyViews = (from sv in wmsEntities.SupplyView where sv.No == date select sv).ToArray();
                    if (supplyViews.Length == 0)
                    {
                        supplyViews = (from sv in wmsEntities.SupplyView where sv.ComponentName == date select sv).ToArray();
                        if (supplyViews.Length == 0)
                        {
                            MessageBox.Show("找不到此零件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (supplyViews.Length > 1)
                        {
                            MessageBox.Show("该供应商提供两种重名零件，请填写零件编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    SupplyView supplyView = supplyViews[0];
                    Dictionary<string, string> NameKey = new Dictionary<string, string>();
                    NameKey.Add("单位名称", "DefaultReceiptUnit");
                    NameKey.Add("单位数量", "DefaultReceiptUnitAmount");

                    //{ new KeyValuePair<string, string>("单位名称", "DefaultReceiptUnit"), new KeyValuePair<string, string>("单位数量", "DefaultReceiptUnitAmount") };
                    string[] Name = { "单位名称", "单位数量", "拒收单位名称", "拒收单位数量" };

                    string[] Name2 = { "拒收单位名称", "拒收单位数量" };
                    string[] key = { "DefaultReceiptUnit", "DefaultReceiptUnitAmount" };
                    SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();
                    PropertyInfo[] propertyInfo = supplyView.GetType().GetProperties();
                    foreach (KeyValuePair<string, string> s in NameKey)
                    {
                        int value = Array.IndexOf(this.columnName, s.Key);
                        //string key = (from ikn in ReceiptMetaData.itemsKeyName where ikn.Name == s select ikn.Key).FirstOrDefault();
                        if (value == -1)
                        {
                            throw new Exception("value找不到Name:" + s);
                        }
                        string n = supplyView.GetType().GetProperty(s.Value).GetValue(supplyView, null).ToString();
                        sortedDictionary.Add(value, n);
                    }
                    this.Invoke(new Action(() =>
                    {
                        int col = e.Cell.Position.Col;
                        int row = e.Cell.Position.Row;
                        foreach (KeyValuePair<int, string> kv in sortedDictionary)
                        {
                            worksheet[row, kv.Key] = kv.Value;
                        }
                        int index1 = Array.IndexOf(this.columnName, "拒收单位");
                        int index2 = Array.IndexOf(this.columnName, "拒收单位数量");
                        worksheet[row, index1] = "个";
                        worksheet[row, index2] = "1";
                        worksheet[row, 0] = supplyView.SupplierID;
                    }));
                }).Start();


            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            Worksheet worksheet = this.reoGridControlMain.Worksheets[0];
            ReceiptTicketItem[] receiptTicketItem = GetDataFromReoGrid();

        }

        private ReceiptTicketItem[] GetDataFromReoGrid()
        {
            Worksheet worksheet = this.reoGridControlMain.Worksheets[0];
            List<ReceiptTicketItem> receiptTicketItems = new List<ReceiptTicketItem>();
            //ReceiptTicketItem receiptTicketItem = new ReceiptTicketItem();
            PropertyInfo[] receiptTicketItemPropertyInfo;
            string[] key = (from kn in ReceiptMetaData.itemsKeyName where kn.Visible = true && kn.Editable == true && (kn.Key != "SupplyNo" && kn.Key != "ComponentName") select kn.Key).ToArray();
            for (int row = 0; row < worksheet.RowCount; row++)
            {
                if (IsEmptyLine(this.reoGridControlMain, row))
                {
                    continue;
                }
                ReceiptTicketItem receiptTicketItem = new ReceiptTicketItem();
                for (int col = 0; col < this.columnName.Length; col++)
                {
                    //如果字段对导入不可见，或者可见但不导入，则跳过
                    string name = columnName[col];
                    string key1 = (from k in ReceiptMetaData.itemsKeyName where k.Name == name select k.Key).FirstOrDefault();
                    if (key1 == null)
                    {
                        continue;
                    }
                    Cell curCell = worksheet.GetCell(row, col);
                    string cellString; //单元格字符串
                    //如果单元格为null，则认为是零长字符串，否则取单元格数据
                    if (curCell == null || curCell.Data == null)
                    {
                        cellString = "";
                    }
                    else
                    {
                        cellString = curCell.Data.ToString();
                    }
                    if (name != "零件编号/名称" && name != "作业人" && name != "确认人")
                    {
                        
                        copyToProperty(ref receiptTicketItem, name, cellString);
                    }
                }
                receiptTicketItems.Add(receiptTicketItem);
            }

            return receiptTicketItems.ToArray();
        }

        private void copyToProperty(ref ReceiptTicketItem receiptTicketItem, string name, string text)
        {
            receiptTicketItem = new ReceiptTicketItem();
            string key = (from k in ReceiptMetaData.itemsKeyName where k.Name == name select k.Key).FirstOrDefault();
            //string[] key = (from kn in ReceiptMetaData.itemsKeyName where kn.Visible = true && kn.Editable == true && (kn.Key != "SupplyNo" && kn.Key != "ComponentName") select kn.Key).ToArray();
            PropertyInfo propertyInfo = receiptTicketItem.GetType().GetProperty(key);
            //propertyInfo.SetValue(receiptTicketItem, text, null);
            if (text == null)
            {
                propertyInfo.SetValue(receiptTicketItem, null, null);
                return;
            }
            if (propertyInfo.PropertyType == typeof(decimal?) || propertyInfo.PropertyType == typeof(decimal))
            {
                try
                {
                    propertyInfo.SetValue(receiptTicketItem, decimal.Parse(text), null);
                }
                catch
                {
                    MessageBox.Show("您输的数值有误！");
                    return;
                }
            }
            if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
            {
                try
                {
                    propertyInfo.SetValue(receiptTicketItem, DateTime.Parse(text), null);
                }
                catch
                {
                    MessageBox.Show("请输入正确的时间！");
                    return;
                }
            }
            if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
            {
                try
                {
                    propertyInfo.SetValue(receiptTicketItem, int.Parse(text), null);
                }
                catch
                {
                    MessageBox.Show("只能输入整数！");
                    return;
                }
            }
        }

        private bool IsEmptyLine(ReoGridControl reoGridControl, int line)
        {
            var worksheet = reoGridControl.Worksheets[0];
            for (int col = 0; col < worksheet.Columns; col++)
            {
                //只要有单元格有字，就判定不为空行。如果所有单元格都是null或者没有字，判定为空行。
                if (worksheet.GetCell(line, col) != null &&
                    worksheet.GetCell(line, col).Data != null &&
                    worksheet.GetCell(line, col).Data.ToString().Length != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
