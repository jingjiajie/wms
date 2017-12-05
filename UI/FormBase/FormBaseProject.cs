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
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI.FormBase
{
    public partial class FormBaseProject : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        public FormBaseProject()
        {
            InitializeComponent();
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in BaseProjectMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlProject.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < BaseProjectMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = BaseProjectMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = BaseProjectMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = BaseProjectMetaData.KeyNames.Length;//限制表的长度
            //Console.WriteLine("表格行数：" + BaseProjectMetaData.KeyNames.Length);
        }

        private void FormBaseProject_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                key = (from kn in StockInfoMetaData.KeyNames
                       where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.toolStripTextBoxSelect.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlProject.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();

                Project[] Projects = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    Projects = wmsEntities.Database.SqlQuery<DataAccess.Project>("SELECT * FROM Project").ToArray();
                }
                else
                {
                    if (Double.TryParse(value, out double tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        Projects = wmsEntities.Database.SqlQuery<DataAccess.Project>(String.Format("SELECT * FROM Project WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlProject.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (Projects.Length == 0)
                    {
                        worksheet[1, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < Projects.Length; i++)
                    {
                        DataAccess.Project curProject = Projects[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curProject, (from kn in BaseProjectMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var a1 = new FormBaseProjectModify();
            a1.SetMode(FormMode.ADD);

            a1.SetAddFinishedCallback(() =>
            {
                this.Search();
            });
            a1.Show();
        }


        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlProject.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int projectID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1 = new FormBaseProjectModify(projectID);
                a1.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlProject.Worksheets[0];
            List<int> deleteIDs = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    deleteIDs.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            if (deleteIDs.Count == 0)
            {
                MessageBox.Show("请选择您要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除...";
            new Thread(new ThreadStart(() =>
            {
                foreach (int id in deleteIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Project WHERE ID = @projectID", new SqlParameter("projectID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();
        }
    }
}
