using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.UI.FormReceipt;
using unvell.ReoGrid;
using System.Threading;
using System.Data.SqlClient;
using WMS.DataAccess;


namespace WMS.UI
{
    public partial class FormSubmissionManage : Form
    {
        private int projectID;
        private int warehouseID;
        private int userID;

        public FormSubmissionManage()
        {
            InitializeComponent();
        }

        public FormSubmissionManage(int projectID, int warehouseID, int userID)
        {
            InitializeComponent();
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;
        }

        private void FormSubmissionManage_Load(object sender, EventArgs e)
        {
            InitComponents();
            Search(null, null);
        }

        private void InitComponents()
        {
            //初始化
            this.comboBoxSelect.Items.Add("无");
            string[] columnNames = (from kn in ReceiptMetaData.submissionTicketKeyName where kn.Visible == true select kn.Name).ToArray();
            this.comboBoxSelect.Items.AddRange(columnNames);
            this.comboBoxSelect.SelectedIndex = 0;

            //初始化表格
            var worksheet = this.reoGridControl1.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < ReceiptMetaData.submissionTicketKeyName.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ReceiptMetaData.submissionTicketKeyName[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.submissionTicketKeyName[i].Visible;
            }
            worksheet.Columns = ReceiptMetaData.submissionTicketKeyName.Length;
        }

        private void Search(string key, string value)
        {
            this.toolStripStatusLabel2.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                //ReceiptTicketView[] receiptTicketViews = null;
                SubmissionTicketView[] submissionTicketView = null;
                if (key == null || value == null)        //搜索所有
                {
                    try
                    {
                        submissionTicketView = wmsEntities.Database.SqlQuery<SubmissionTicketView>("SELECT * FROM SubmissionTicketView WHERE ReceiptTicketWarehouse = @warehouseID AND ReceiptTicketProjectID = @projectID ORDER BY ID DESC", new SqlParameter[] { new SqlParameter("warehouseID", this.warehouseID), new SqlParameter("projectID", this.projectID) }).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    //double tmp;
                    //if (Double.TryParse(value, out tmp) == false) //不是数字则加上单引号
                    //{
                    //    value = "'" + value + "'";
                    //}
                    try
                    {
                        submissionTicketView = wmsEntities.Database.SqlQuery<SubmissionTicketView>(String.Format("SELECT * FROM SubmissionTicketView WHERE {0} = @key AND ReceiptTicketWarehouse = @warehouseID AND ReceiptTicketProjectID = @projectID ORDER BY ID DESC", key), new SqlParameter[] { new SqlParameter("@key", value), new SqlParameter("@warehouseID", this.warehouseID), new SqlParameter("@projectID", this.projectID) }).ToArray();
                    }
                    catch (EntityException)
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        return;
                    }
                }
                
                this.reoGridControl1.Invoke(new Action(() =>
                {
                    this.toolStripStatusLabel2.Text = "搜索完成";
                    var worksheet = this.reoGridControl1.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < submissionTicketView.Length; i++)
                    {
                        if (submissionTicketView[i].State == "作废")
                        {
                            continue;
                        }
                        SubmissionTicketView curSubmissionTicketView = submissionTicketView[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curSubmissionTicketView, (from kn in ReceiptMetaData.submissionTicketKeyName select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            if (columns[j] == null)
                            {  
                                worksheet[n, j] = columns[j];
                            }
                            else
                            {
                                worksheet[n, j] = columns[j].ToString();
                            }
                        }
                        n++;
                    }
                    if (submissionTicketView.Length == 0)
                    {
                        int m = ReceiptUtilities.GetFirstColumnIndex(ReceiptMetaData.submissionTicketKeyName);

                        //this.reoGridControl1.Worksheets[0][6, 8] = "32323";
                        this.reoGridControl1.Worksheets[0][0, m] = "无查询结果";
                    }
                }));

            })).Start();

        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (comboBoxSelect.SelectedIndex == 0)
            {
                Search(null, null);
            }
            else
            {
                string condition = this.comboBoxSelect.Text;
                string key = "";
                foreach (KeyName kn in ReceiptMetaData.submissionTicketKeyName)
                {
                    if (condition == kn.Name)
                    {
                        key = kn.Key;
                        break;
                    }
                }
                string value = this.textBoxSelect.Text;
                Search(key, value);
            }
        }

        private void buttonPass_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControl1.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int submissionTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                SubmissionTicket submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ID == submissionTicketID select st).Single();
                if (submissionTicket.State == "合格")
                {
                    MessageBox.Show("该送检单状态已置为合格");
                }
                else
                {
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE SubmissionTicket SET State='合格' WHERE ID=@submissionTicketID", new SqlParameter("submissionTicketID", submissionTicketID));
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE SubmissionTicketItem SET State='合格' WHERE SubmissionTicketID=@submissionTicketID", new SqlParameter("submissionTicketID", submissionTicketID));
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State='过检' WHERE ID=@receiptTicket", new SqlParameter("receiptTicket", submissionTicket.ReceiptTicketID));
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State='过检' WHERE ReceiptTicketID=@receiptTicket", new SqlParameter("receiptTicket", submissionTicket.ReceiptTicketID));

                    if (MessageBox.Show("是否同时收货？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == submissionTicket.ReceiptTicketID select rt).FirstOrDefault();
                        if (receiptTicket != null)
                        {
                            if (receiptTicket.State != "已收货")
                            {
                                wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State='已收货' WHERE ID=@receiptTicket", new SqlParameter("receiptTicket", submissionTicket.ReceiptTicketID));
                                wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State='已收货' WHERE ReceiptTicketID=@receiptTicket", new SqlParameter("receiptTicket", submissionTicket.ReceiptTicketID));
                            }
                        }
                    }
                }
            }
            catch(EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            this.Search(null, null);
        }

        private void buttonNoPass_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControl1.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int submissionTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                SubmissionTicket submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ID == submissionTicketID select st).Single();
                wmsEntities.Database.ExecuteSqlCommand("UPDATE SubmissionTicket SET State='不合格' WHERE ID=@submissionTicketID", new SqlParameter("submissionTicketID", submissionTicketID));
                wmsEntities.Database.ExecuteSqlCommand("UPDATE SubmissionTicketItem SET State='不合格' WHERE SubmissionTicketID=@submissionTicketID", new SqlParameter("submissionTicketID", submissionTicketID));
                if (MessageBox.Show("是否同时拒收?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State='拒收' WHERE ID=@receiptTicket", new SqlParameter("receiptTicket", submissionTicket.ReceiptTicketID));
                    wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State='拒收' WHERE ReceiptTicketID=@receiptTicket", new SqlParameter("receiptTicket", submissionTicket.ReceiptTicketID));
                }
            }
            catch(EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            this.Search(null, null);
        }

        private void buttonItem_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControl1.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int submissionTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                FormSubmissionItem formSubmissionItem = new FormSubmissionItem(submissionTicketID);
                formSubmissionItem.SetCallBack(new Action(() =>
                {
                    this.Search(null, null);
                }));
                formSubmissionItem.Show();
            }
            catch(EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            this.Search(null, null);
        }

        private void buttonItems_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControl1.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int submissionTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                FormAddSubmissionTicket formAddSubmissionTicket = new FormAddSubmissionTicket(submissionTicketID, this.userID, FormMode.ALTER);
                formAddSubmissionTicket.SetCallBack(new Action(() =>
                {
                    this.Search(null, null);
                }));
                formAddSubmissionTicket.Show();
                /*
                FormReceiptArrivalCheck formReceiptArrivalCheck = new FormReceiptArrivalCheck(submissionTicketID, this.userID);
                formReceiptArrivalCheck.SetFinishedAction(()=> {
                    Search(null, null);
                });
                formReceiptArrivalCheck.Show();
                */
            }
            catch(EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            this.Search(null, null);
        }

        private void comboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSelect.SelectedIndex == 0)
            {
                this.textBoxSelect.Text = "";
                this.textBoxSelect.Enabled = false;
            }
            else
            {
                this.textBoxSelect.Text = "";
                this.textBoxSelect.Enabled = true;
            }
        }

        private void toolStripTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
