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

namespace WMS.UI
{
    public partial class FormBaseWarehouse : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        public FormBaseWarehouse()
        {
            InitializeComponent();
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in FormBase.BaseWarehouseMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < FormBase.BaseWarehouseMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = FormBase.BaseWarehouseMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = FormBase.BaseWarehouseMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = FormBase.BaseWarehouseMetaData.KeyNames.Length;//限制表的长度
        }


        private void base_Warehouse_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void Search(int selectedID = -1)
        {
            string key = null;
            string value = null;

            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                key = (from kn in FormBase.BaseWarehouseMetaData.KeyNames
                       where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.toolStripTextBoxSelect.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            worksheet[0, 1] = "加载中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                WarehouseView[] warehouseViews = null;
                string sql = "SELECT * FROM WarehouseView WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();

                    if (key != null && value != null) //查询条件不为null则增加查询条件
                    {
                        sql += "AND " + key + " = @value ";
                        parameters.Add(new SqlParameter("value", value));
                    }
                    sql += " ORDER BY ID DESC"; //倒序排序
                    try
                    {
                    warehouseViews = wmsEntities.Database.SqlQuery<WarehouseView>(sql, parameters.ToArray()).ToArray();
                    }
                    catch (EntityCommandExecutionException)
                    {
                        MessageBox.Show("查询失败，请检查输入查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                this.reoGridControlWarehouse.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (warehouseViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < warehouseViews.Length; i++)
                    {

                        WarehouseView curWarehouseView = warehouseViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curWarehouseView, (from kn in FormBase.BaseWarehouseMetaData.keyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j];
                        }
                    }

                }));
            })).Start();
            if (selectedID != -1)
            {
                Utilities.SelectLineByID(this.reoGridControlWarehouse, selectedID);
            }
        }


        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var a1 = new FormBase.FormBaseWarehouseModify();
            a1.SetMode(FormMode.ADD);

            a1.SetAddFinishedCallback((addedID) =>
            {
                this.Search(addedID);
                var worksheet = this.reoGridControlWarehouse.Worksheets[0];

                worksheet.SelectionRange = new RangePosition("A1:A1");
            });
            a1.Show();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
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
                try
                {
                    foreach (int id in deleteIDs)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Warehouse WHERE ID = @warehouseID", new SqlParameter("warehouseID", id));
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(() =>
                {
                    this.Search();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            })).Start();
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int warehouseID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1 = new FormBase.FormBaseWarehouseModify(warehouseID);
                a1.SetModifyFinishedCallback((addedID) =>
                {
                    this.Search(addedID);
                });
                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void toolStripTextBoxSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Search();
            }
        }

        private void toolStripComboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.toolStripTextBoxSelect.Text = "";
                this.toolStripTextBoxSelect.Enabled = false;
                this.toolStripTextBoxSelect.BackColor = Color.LightGray;

            }
            else
            {
                this.toolStripTextBoxSelect.Enabled = true;
                this.toolStripTextBoxSelect.BackColor = Color.White;
            }
        }
    }
}
