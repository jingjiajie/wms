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
using System.Threading;
using System.Data.SqlClient;


namespace WMS.UI
{
    public partial class FormPutOutStorageTicket : Form
    {
        WMSEntities wmsEntities = new WMSEntities();

        int userID = -1;
        int projectID = -1;
        int warehouseID = -1;

        public FormPutOutStorageTicket(int userID, int projectID, int warehouseID)
        {
            InitializeComponent();
            InitComponents();
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void FormPutOutStorageTicket_Load(object sender, EventArgs e)
        {
            this.Search();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in PutOutStorageTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < PutOutStorageTicketViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = PutOutStorageTicketViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = PutOutStorageTicketViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = PutOutStorageTicketViewMetaData.KeyNames.Length; //限制表的长度
        }

        public void SetSearchCondition(string key, string value)
        {
            string name = (from kn in PutOutStorageTicketViewMetaData.KeyNames
                           where kn.Key == key
                           select kn.Name).FirstOrDefault();
            if (name == null)
            {
                return;
            }
            for (int i = 0; i < this.comboBoxSearchCondition.Items.Count; i++)
            {
                var item = comboBoxSearchCondition.Items[i];
                if (item.ToString() == name)
                {
                    this.comboBoxSearchCondition.SelectedIndex = i;
                }
            }
            this.textBoxSearchValue.Text = value;
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in PutOutStorageTicketViewMetaData.KeyNames
                       where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                PutOutStorageTicketView[] putOutStorageTicketViews = null;
                string sql = "SELECT * FROM PutOutStorageTicketView WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (this.projectID != -1)
                {
                    sql += "AND ShipmentTicketProjectID = @projectID ";
                    parameters.Add(new SqlParameter("projectID", this.projectID));
                }
                if (warehouseID != -1)
                {
                    sql += "AND ShipmentTicketWarehouseID = @warehouseID ";
                    parameters.Add(new SqlParameter("warehouseID", this.warehouseID));
                }
                if (key != null && value != null) //查询条件不为null则增加查询条件
                {
                    sql += "AND " + key + " = @value ";
                    parameters.Add(new SqlParameter("value", value));
                }
                sql += " ORDER BY ID DESC";
                try
                {
                    putOutStorageTicketViews = wmsEntities.Database.SqlQuery<PutOutStorageTicketView>(sql, parameters.ToArray()).ToArray();
                }
                catch (EntityCommandExecutionException)
                {
                    MessageBox.Show("查询失败，请检查输入条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                catch (Exception)
                {
                    MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (putOutStorageTicketViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < putOutStorageTicketViews.Length; i++)
                    {
                        var curPutOutStorageTicketViews = putOutStorageTicketViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curPutOutStorageTicketViews, (from kn in PutOutStorageTicketViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);

            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int putOutStorageTicketID = ids[0];
            var formPutOutStorageTicketItem = new FormPutOutStorageTicketItem(putOutStorageTicketID);
            formPutOutStorageTicketItem.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int putOutStorageTicketID = ids[0];
            var formPutOutStorageTicketModify = new FormPutOutStorageTicketModify(this.userID, putOutStorageTicketID);
            formPutOutStorageTicketModify.SetModifyFinishedCallback(() =>
            {
                this.Search();
            });
            formPutOutStorageTicketModify.Show();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = this.GetSelectedIDs();
            if(ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确定删除选中的项目吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    foreach (int id in ids)
                    {
                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM PutOutStorageTicket WHERE ID = @id", new SqlParameter("@id", id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(this.Search));
                MessageBox.Show("删除成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private int[] GetSelectedIDs()
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            List<int> ids = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    ids.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            return ids.ToArray();
        }

        private void comboBoxSearchCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSearchCondition.SelectedIndex == 0)
            {
                this.textBoxSearchValue.Text = "";
                this.textBoxSearchValue.Enabled = false;
            }
            else
            {
                this.textBoxSearchValue.Enabled = true;
            }
        }

        private void textBoxSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Search();
            }
        }
    }
}
