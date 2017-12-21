using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using unvell.ReoGrid;
using WMS.UI;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI.FormReceipt
{
    public partial class FormReceiptShelves : Form
    {
        private FormMode formMode;
        private int receiptTicketID;
        private WMSEntities wmsEntities = new WMSEntities();
        private int userID;
        private int warehouseID;
        private int projectID;

        public FormReceiptShelves()
        {
            InitializeComponent();
        }

        public FormReceiptShelves(int projectID, int warehouseID, int userID)
        {
            InitializeComponent();
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;
        }

        public FormReceiptShelves(FormMode formMode, int receiptTicketID)
        {
            InitializeComponent();
            this.formMode = formMode;
            this.receiptTicketID = receiptTicketID;
        }

        private void FormReceiptShelves_Load(object sender, EventArgs e)
        {
            InitComponents();
            Search(null, null);
        }

        private void InitComponents()
        {
            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            string[] columnNames = (from kn in ReceiptMetaData.putawayTicketKeyName select kn.Name).ToArray();
            this.toolStripComboBoxSelect.Items.AddRange(columnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;

            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.putawayTicketKeyName[i].Visible;
            }
            worksheet.Columns = columnNames.Length;
        }

        private void Search(string key, string value)
        {
            //this.lableStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                PutawayTicketView[] putawayTicketView = null;
                if (key == null || value == null)        //搜索所有
                {
                    putawayTicketView = wmsEntities.Database.SqlQuery<PutawayTicketView>("SELECT * FROM PutawayTicketView WHERE WarehouseID = @warehouseID AND ProjectID = @projectID", new SqlParameter[] { new SqlParameter("warehouseID", this.warehouseID), new SqlParameter("projectID", this.projectID)}).ToArray();
                }
                else
                {
                    double tmp;
                    //if (Double.TryParse(value, out tmp) == false) //不是数字则加上单引号
                    //{
                    //    value = "'" + value + "'";
                    //}
                    try
                    {
                        putawayTicketView = wmsEntities.Database.SqlQuery<PutawayTicketView>(String.Format("SELECT * FROM PutawayTicketView WHERE {0} = @key AND WarehouseID = @warehouseID AND ProjectID = @projectID ", key), new SqlParameter[] { new SqlParameter("@key", value), new SqlParameter("@warehouseID", this.warehouseID), new SqlParameter("@projectID", this.projectID) }).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlUser.Invoke(new Action(() =>
                {
                    //this.lableStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlUser.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < putawayTicketView.Length; i++)
                    {

                        PutawayTicketView curReceiptTicketView = putawayTicketView[i];
                        if (curReceiptTicketView.State == "作废")
                        {
                            continue;
                        }
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketView, (from kn in ReceiptMetaData.putawayTicketKeyName select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {

                            worksheet[n, j] = columns[j];
                        }
                        n++;

                    }
                }));
                if (putawayTicketView.Length == 0)
                {
                    int m = ReceiptUtilities.GetFirstColumnIndex(ReceiptMetaData.submissionTicketKeyName);

                    //this.reoGridControl1.Worksheets[0][6, 8] = "32323";
                    this.reoGridControlUser.Worksheets[0][0, m] = "无查询结果";
                }

            })).Start();

        }

        private void toolStripButtonItem_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int putawayTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                FormShelvesItem formShelvesItem = new FormShelvesItem(putawayTicketID);
                formShelvesItem.SetCallBack(new Action(() =>
                {
                    this.Search(null, null);
                }));
                formShelvesItem.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                Search(null, null);
            }
            else
            {
                string condition = this.toolStripComboBoxSelect.Text;
                string key = "";
                foreach (KeyName kn in ReceiptMetaData.putawayTicketKeyName)
                {
                    if (condition == kn.Name)
                    {
                        key = kn.Key;
                        break;
                    }
                }
                string value = this.toolStripTextBoxSelect.Text;
                Search(key, value);
            }
        }

        private void toolStripComboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.toolStripTextBoxSelect.Text = "";
                this.toolStripTextBoxSelect.Enabled = false;
            }
            else
            {
                this.toolStripTextBoxSelect.Text = "";
                this.toolStripTextBoxSelect.Enabled = true;
            }
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int putawayTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                //FormShelvesItem formShelvesItem = new FormShelvesItem(putawayTicketID);
                if (MessageBox.Show("确定删除该上架单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM PutawayTicket WHERE ID = @putawayTicketID", new SqlParameter("putawayTicketID", putawayTicketID));
                    }
                    catch
                    {
                        MessageBox.Show("该上架单已被删除!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Search(null, null);
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
