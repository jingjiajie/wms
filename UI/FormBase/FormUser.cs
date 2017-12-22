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
    public partial class FormUser : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        private int userID = -1;
        private int projectID = -1;
        private int warehouseID = -1;
        public FormUser(int userID,int projectID,int warehouseID)
        {
            InitializeComponent();
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in UserMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.Items.Remove("密码"); //不能按密码搜索
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < UserMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = UserMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = UserMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = UserMetaData.KeyNames.Length; //限制表的长度
        }

        private void Search(int selectedID = -1)
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in UserMetaData.KeyNames
                       where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {

                UserView[] userViews = null;
                string sql = "SELECT * FROM UserView WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (key != null && value != null) //查询条件不为null则增加查询条件
                {
                    sql += "AND " + key + " = @value ";
                    parameters.Add(new SqlParameter("value", value));
                }
                sql += " ORDER BY ID DESC"; //倒序排序

                try
                {
                    userViews = wmsEntities.Database.SqlQuery<UserView>(sql, parameters.ToArray()).ToArray();
                }
                catch
                {
                    MessageBox.Show("查询失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (userViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < userViews.Length; i++)
                    {
                        UserView curUserView = userViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curUserView, (from kn in UserMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                    if(selectedID != -1)
                    {
                        Utilities.SelectLineByID(this.reoGridControlMain, selectedID);
                    }
                }));
            })).Start();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormUserModify();
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback((addedID) =>
            {
                this.Search(addedID);
            });
            form.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int userID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formUserModify = new FormUserModify(userID);
                formUserModify.SetModifyFinishedCallback((addedID) =>
                {
                    this.Search(addedID);
                });
                formUserModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            int[] deleteIDs = Utilities.GetSelectedIDs(this.reoGridControlMain);

            if (deleteIDs.Length == 0)
            {
                MessageBox.Show("请选择您要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (deleteIDs.Contains(this.userID))
            {
                MessageBox.Show("登录用户不可删除自己！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.labelStatus.Text = "正在删除...";


            new Thread(new ThreadStart(() =>
            {
                try
                {
                    foreach (int id in deleteIDs)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM [User] WHERE ID = @userID", new SqlParameter("@userID", id));
                    }
                    this.wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    this.Search();
                }));
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void comboBoxSearchCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxSearchCondition.SelectedIndex == 0)
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
            if(e.KeyChar == 13)
            {
                this.Search();
            }
        }
    }
}
