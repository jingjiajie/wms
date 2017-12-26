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
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormJobTicketItem : Form
    {
        private int jobTicketID = -1;
        private int curStockInfoID = -1;
        Action jobTicketStateChangedCallback = null;

        TextBox textBoxComponentName = null;

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
            WMSEntities wmsEntities = new WMSEntities();
            JobTicket jobTicket = null;
            try
            {
                jobTicket = (from j in wmsEntities.JobTicket where j.ID == jobTicketID select j).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("加载数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (jobTicket == null)
            {
                MessageBox.Show("作业单信息不存在，请刷新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            this.Search();
        }

        private void InitComponents()
        {
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            worksheet.SelectionRangeChanged += this.worksheet_SelectionRangeChanged;

            for (int i = 0; i < JobTicketItemViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = JobTicketItemViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = JobTicketItemViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = JobTicketItemViewMetaData.KeyNames.Length; //限制表的长度

            Utilities.CreateEditPanel(this.tableLayoutPanelProperties,JobTicketItemViewMetaData.KeyNames);
            this.textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName",true)[0];
            this.textBoxComponentName.BackColor = Color.White;
            this.textBoxComponentName.Click += textBoxComponentName_Click;
        }

        private void textBoxComponentName_Click(object sender, EventArgs e)
        {
            var formSelectStockInfo = new FormSelectStockInfo(this.curStockInfoID);
            formSelectStockInfo.SetSelectFinishCallback((selectedStockInfoID) =>
            {
                this.curStockInfoID = selectedStockInfoID;
                new Thread(new ThreadStart(() =>
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    StockInfoView stockInfoView = (from s in wmsEntities.StockInfoView
                                                   where s.ID == selectedStockInfoID
                                                   select s).Single();
                    this.Invoke(new Action(() =>
                    {
                        Utilities.CopyPropertiesToTextBoxes(stockInfoView, this);
                    }));
                })).Start();
            });
            formSelectStockInfo.Show();
        }

        private JobTicketView GetJobTicketViewByNo(string jobTicketNo)
        {
            WMSEntities wmsEntities = new WMSEntities();
            return (from jt in wmsEntities.JobTicketView
                    where jt.JobTicketNo == jobTicketNo
                    select jt).FirstOrDefault();
        }

        private void Search(int selectID = -1)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            JobTicketItemView[] jobTicketItemViews = null;
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    jobTicketItemViews = (from j in wmsEntities.JobTicketItemView
                                                              where j.JobTicketID == this.jobTicketID
                                                              orderby j.ID descending
                                                              select j).ToArray();
                }
                catch
                {
                    MessageBox.Show("加载数据失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                    if (selectID != -1)
                    {
                        Utilities.SelectLineByID(this.reoGridControlMain, selectID);
                    }
                    this.RefreshTextBoxes();
                }));
            })).Start();
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            const string STRING_FINISHED = "已完成";
            int[] selectedIDs = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    //将状态置为已完成
                    foreach (int id in selectedIDs)
                    {
                        wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE JobTicketItem SET State = '{0}' WHERE ID = {1};", STRING_FINISHED, id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                //如果作业单中所有条目都完成，询问是否将作业单标记为完成
                int unfinishedJobTicketItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM JobTicketItem WHERE JobTicketID = {0} AND State <> '{1}'", this.jobTicketID, STRING_FINISHED)).Single();
                if (unfinishedJobTicketItemCount == 0)
                {
                    if (MessageBox.Show("检测到所有的作业任务都已经完成，是否将作业单状态更新为完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE JobTicket SET State = '{0}' WHERE ID = {1}", STRING_FINISHED, this.jobTicketID));
                            wmsEntities.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    this.jobTicketStateChangedCallback?.Invoke();
                }
                this.Invoke(new Action(()=> this.Search()));
                MessageBox.Show("操作成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonUnfinish_Click(object sender, EventArgs e)
        {
            const string STRING_UNFINISHED = "未完成";
            int[] selectedIDs = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    foreach (int id in selectedIDs)
                    {
                        wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE JobTicketItem SET State = '{0}' WHERE ID = {1};", STRING_UNFINISHED, id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(() => this.Search()));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in this.tableLayoutPanelProperties.Controls)
            {
                if(control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.Text = "";
                }
            }
        }

        private void RefreshTextBoxes()
        {
            this.ClearTextBoxes();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length == 0)
            {
                this.curStockInfoID = -1;
                return;
            }
            int id = ids[0];
            JobTicketItemView jobTicketItemView = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                jobTicketItemView = (from jti in wmsEntities.JobTicketItemView
                                                       where jti.ID == id
                                                       select jti).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("加载数据失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (jobTicketItemView == null)
            {
                MessageBox.Show("作业单项目不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(jobTicketItemView.StockInfoID != null)
            {
                this.curStockInfoID = jobTicketItemView.StockInfoID.Value;
            }
            else
            {
                this.curStockInfoID = -1;
            }
            Utilities.CopyPropertiesToTextBoxes(jobTicketItemView, this);
            Utilities.CopyPropertiesToComboBoxes(jobTicketItemView, this);
        }

        private void worksheet_SelectionRangeChanged(object sender, EventArgs e)
        {
            RefreshTextBoxes();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.curStockInfoID == -1)
            {
                MessageBox.Show("未选择零件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            JobTicketItem newItem = new JobTicketItem();
            if (Utilities.CopyTextBoxTextsToProperties(this, newItem, JobTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(Utilities.CopyComboBoxsToProperties(this,newItem, JobTicketItemViewMetaData.KeyNames) == false)
            {
                MessageBox.Show("内部错误：拷贝单选框数据失败","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            newItem.JobTicketID = this.jobTicketID;
            newItem.StockInfoID = this.curStockInfoID;
            new Thread(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                wmsEntities.JobTicketItem.Add(newItem);
                try
                {
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("添加失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(()=>
                {
                    this.Search(newItem.ID);
                }));
                MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }).Start();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = ids[0];
            new Thread(() =>
            {
                WMSEntities wmsEntities1 = new WMSEntities();
                JobTicketItem jobTicketItem = null;
                try
                {
                    jobTicketItem = (from j in wmsEntities1.JobTicketItem where j.ID == id select j).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("修改失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(jobTicketItem == null)
                {
                    MessageBox.Show("作业任务不存在，请重新查询","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(()=>
                {
                    if (Utilities.CopyTextBoxTextsToProperties(this, jobTicketItem, JobTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
                    {
                        MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Utilities.CopyComboBoxsToProperties(this, jobTicketItem, JobTicketItemViewMetaData.KeyNames) == false)
                    {
                        MessageBox.Show("内部错误：拷贝单选框数据失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    new Thread(()=>
                    {
                        try
                        {
                            wmsEntities1.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("修改失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        this.Invoke(new Action(() => this.Search()));
                        MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }).Start();
                }));
            }).Start();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确定要删除选中项吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            new Thread(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    foreach (int id in ids)
                    {
                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM JobTicketItem WHERE ID = @id", new SqlParameter("id", id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(() => this.Search()));
                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }).Start();
        }

        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            buttonAdd.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            buttonAdd.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonAdd_MouseDown(object sender, MouseEventArgs e)
        {
            buttonAdd.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }



        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            buttonDelete.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonDelete.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonDelete_MouseDown(object sender, MouseEventArgs e)
        {
            buttonDelete.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonModify_MouseEnter(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonModify_MouseLeave(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonModify_MouseDown(object sender, MouseEventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }


        private void buttonFinish_MouseEnter(object sender, EventArgs e)
        {
            buttonFinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB1_s;
        }

        private void buttonFinish_MouseLeave(object sender, EventArgs e)
        {
            buttonFinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonFinish_MouseDown(object sender, MouseEventArgs e)
        {
            buttonFinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonUnfinish_MouseEnter(object sender, EventArgs e)
        {
            buttonUnfinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB4_s;
        }

        private void buttonUnfinish_MouseLeave(object sender, EventArgs e)
        {
            buttonUnfinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB4_q;
        }

        private void buttonUnfinish_MouseDown(object sender, MouseEventArgs e)
        {
            buttonUnfinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_s;
        }


    }
}
