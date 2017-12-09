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

namespace WMS.UI
{
    public partial class FormJobTicket : Form
    {
        WMSEntities wmsEntities = new WMSEntities();
        private int userID;

        public FormJobTicket(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void FormJobTicket_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in JobTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < JobTicketViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = JobTicketViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = JobTicketViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = JobTicketViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in JobTicketViewMetaData.KeyNames
                       where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                JobTicketView[] jobTicketViews = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    jobTicketViews = wmsEntities.Database.SqlQuery<JobTicketView>("SELECT * FROM JobTicketView").ToArray();
                }
                else
                {
                    
                    if (Utilities.IsQuotateType(typeof(JobTicketView).GetProperty(key).PropertyType))
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        jobTicketViews = wmsEntities.Database.SqlQuery<JobTicketView>(String.Format("SELECT * FROM JobTicketView WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (jobTicketViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < jobTicketViews.Length; i++)
                    {
                        var curJobTicketViews = jobTicketViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curJobTicketViews, (from kn in JobTicketViewMetaData.KeyNames select kn.Key).ToArray());
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

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = this.GetSelectedIDs();
            if(ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(MessageBox.Show("确定要删除选中项吗？","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!= DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除";
            new Thread(new ThreadStart(()=>
            {
                foreach (int id in ids)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(string.Format("DELETE FROM JobTicket WHERE ID = {0}", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
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

        private void buttonGeneratePutOutStorageTicket_Click(object sender, EventArgs e)
        {
            int[] ids = this.GetSelectedIDs();
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行操作","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = ids[0];

            new Thread(new ThreadStart(()=>
            {
                JobTicket jobTicket = (from j in this.wmsEntities.JobTicket where j.ID == id select j).FirstOrDefault();
                if (jobTicket == null)
                {
                    MessageBox.Show("未找到作业单信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                jobTicket.ShipmentTicket.State = ShipmentTicketViewMetaData.STRING_STATE_DELIVERING;
                PutOutStorageTicket putOutStorageTicket = new PutOutStorageTicket();
                wmsEntities.PutOutStorageTicket.Add(putOutStorageTicket);
                putOutStorageTicket.CreateUserID = this.userID;
                putOutStorageTicket.CreateTime = DateTime.Now;
                putOutStorageTicket.LastUpdateUserID = this.userID;
                putOutStorageTicket.LastUpdateTime = DateTime.Now;
                putOutStorageTicket.JobTicketID = id;
                putOutStorageTicket.No = "";

                foreach (JobTicketItem jobTicketItem in jobTicket.JobTicketItem)
                {
                    PutOutStorageTicketItem putOutStorageTicketItem = new PutOutStorageTicketItem();
                    putOutStorageTicket.PutOutStorageTicketItem.Add(putOutStorageTicketItem);
                    putOutStorageTicketItem.StockInfoID = jobTicketItem.StockInfoID;
                }

                wmsEntities.SaveChanges();
                putOutStorageTicket.No = Utilities.GenerateNo("CKD",putOutStorageTicket.ID);
                wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
                MessageBox.Show("操作成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }
    }
}
