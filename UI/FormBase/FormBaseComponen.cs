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
    public partial class FormBaseComponent : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        public FormBaseComponent()
        {
            InitializeComponent();
        }
        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in ComponenMetaData.componenkeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < ComponenMetaData.componenkeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ComponenMetaData.componenkeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ComponenMetaData.componenkeyNames[i].Visible;
            }
            worksheet.Columns = ComponenMetaData.componenkeyNames.Length;//限制表的长度
            Console.WriteLine("表格行数：" + ComponenMetaData.componenkeyNames.Length);
        }

        private void FormBaseComponent_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.Search();
        }

    private void Search()
        {
            string key = null;
            string value = null;

            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                key = (from kn in ComponenMetaData.componenkeyNames
                       where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.toolStripTextBoxSelect.Text;
            }
            
            this.labelStatus.Text = "正在搜索中...";
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                ComponentView[] componentViews = null;
                if (key == null || value == null)        //搜索所有
                {
                    componentViews = wmsEntities.Database.SqlQuery<ComponentView>("SELECT * FROM ComponentView").ToArray();
                }
                else
                {
                    double tmp;
                    if (Double.TryParse(value, out tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        componentViews = wmsEntities.Database.SqlQuery<ComponentView>(String.Format("SELECT * FROM ComponentView WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlComponen.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlComponen.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    for (int i = 0; i < componentViews.Length; i++)
                    {

                        ComponentView curComponentView = componentViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curComponentView, (from kn in ComponenMetaData.componenkeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j];
                        }
                    }
                }));

            })).Start();
            //    DataAccess.Component[] Components = null;
            //    if (key == null || value == null) //查询条件为null则查询全部内容
            //    {
            //        Components = wmsEntities.Database.SqlQuery<DataAccess.Component>("SELECT * FROM Component").ToArray();
            //    }
            //    else
            //    {
            //        if (Double.TryParse(value, out double tmp) == false) //不是数字则加上单引号
            //        {
            //            value = "'" + value + "'";
            //        }
            //        try
            //        {
            //            Components = wmsEntities.Database.SqlQuery<DataAccess.Component>(String.Format("SELECT * FROM Component WHERE {0} = {1}", key, value)).ToArray();
            //        }
            //        catch
            //        {
            //            MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //    this.reoGridControlComponen.Invoke(new Action(() =>
            //    {
            //        this.labelStatus.Text = "搜索完成";
            //        worksheet.DeleteRangeData(RangePosition.EntireRange);
            //        if (Components.Length == 0)
            //        {
            //            worksheet[1, 1] = "没有查询到符合条件的记录";
            //        }
            //        for (int i = 0; i < Components.Length; i++)
            //        {
            //            DataAccess.Component curComponent = Components[i];
            //            object[] columns = Utilities.GetValuesByPropertieNames(curComponent, (from kn in ComponenMetaData.KeyNames select kn.Key).ToArray());
            //            for (int j = 0; j < ComponenMetaData.KeyNames.Length; j++)
            //            {
            //                worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
            //            }
            //        }
            //    }));
            //})).Start();
        }


        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormComponenModify();
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
                this.Search();
            });
            form.Show();

        }//添加
  

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int componenID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formComponenModify = new FormComponenModify(componenID);
                formComponenModify.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                formComponenModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }//修改

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
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
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM StockInfo WHERE ID = @componenID", new SqlParameter("componenID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();

        }//删除
    }
}
