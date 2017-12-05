using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WMS.DataAccess;
using unvell.ReoGrid;

namespace WMS.UI
{
    public partial class FormJobTicketItem : Form
    {
        private int jobTicketID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        Action jobTicketStateChangedCallback = null;

        private KeyName[] visibleColumns = (from kn in JobTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true
                                            select kn).ToArray();

        public FormJobTicketItem(int jobTicketID)
        {
            InitializeComponent();
            this.jobTicketID = jobTicketID;
        }

        public void SetJobTicketStateChangedCallback(Action jobTicketStateChangedCallback)
        {
            this.jobTicketStateChangedCallback = jobTicketStateChangedCallback;
        }

        private void FormJobTicketItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < JobTicketItemViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = JobTicketItemViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = JobTicketItemViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = JobTicketItemViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void Search()
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                JobTicketItemView[] jobTicketItemViews = (from j in wmsEntities.JobTicketItemView
                                                          where j.JobTicketID == this.jobTicketID
                                                          select j).ToArray();

                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "加载完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (jobTicketItemViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有符合条件的记录";
                    }
                    for (int i = 0; i < jobTicketItemViews.Length; i++)
                    {
                        var curJobTicketViews = jobTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curJobTicketViews, (from kn in JobTicketItemViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < columns.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            const string STRING_FINISHED = "已完成";
            int[] selectedIDs = this.GetSelectedIDs();
            if(selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(()=>
            {
                //将状态置为已完成
                foreach (int id in selectedIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE JobTicketItem SET State = '{0}' WHERE ID = {1};", STRING_FINISHED, id));
                }
                this.wmsEntities.SaveChanges();
                
                //如果作业单中所有条目都完成，询问是否将作业单标记为完成
                int unfinishedJobTicketItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM JobTicketItem WHERE JobTicketID = {0} AND State <> '{1}'", this.jobTicketID, STRING_FINISHED)).Single();
                if (unfinishedJobTicketItemCount == 0)
                {
                    if (MessageBox.Show("检测到所有的作业任务都已经完成，是否将作业单状态更新为完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE JobTicket SET State = '{0}' WHERE ID = {1}",STRING_FINISHED,this.jobTicketID));
                        this.wmsEntities.SaveChanges();
                    }
                    this.jobTicketStateChangedCallback?.Invoke();
                }
                this.Invoke(new Action(this.Search));
                MessageBox.Show("操作成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }



        private int[] GetSelectedIDs()
        {
            List<int> ids = new List<int>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for(int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
            {
                if (worksheet[row, 0] == null) continue;
                if(int.TryParse(worksheet[row, 0].ToString(),out int jobTicketID))
                {
                    ids.Add(jobTicketID);
                }
            }
            return ids.ToArray();
        }

        private void buttonUnfinish_Click(object sender, EventArgs e)
        {
            const string STRING_UNFINISHED = "未完成";
            int[] selectedIDs = this.GetSelectedIDs();
            if (selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                foreach (int id in selectedIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE JobTicketItem SET State = '{0}' WHERE ID = {1};", STRING_UNFINISHED, id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }
    }
}
