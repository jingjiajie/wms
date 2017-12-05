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
    public partial class FormStockInfo : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        public FormStockInfo()
        {
            InitializeComponent();
        }


        private void FormStockInfo_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in StockInfoMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < StockInfoMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = StockInfoMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = StockInfoMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = StockInfoMetaData.KeyNames.Length; //限制表的长度
        }

        private void reoGridControlMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in StockInfoMetaData.KeyNames
                              where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                              select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                StockInfoView[] stockInfoViews = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    stockInfoViews = wmsEntities.Database.SqlQuery<StockInfoView>("SELECT * FROM StockInfoView").ToArray();
                    Console.WriteLine(stockInfoViews.Length);
                }
                else
                {
                    if(decimal.TryParse(value,out decimal result) == false)
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        stockInfoViews = wmsEntities.Database.SqlQuery<StockInfoView>(String.Format("SELECT * FROM StockInfoView WHERE {0} = {1}",key,value)).ToArray();
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
                    if(stockInfoViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < stockInfoViews.Length; i++)
                    {
                        StockInfoView curStockInfoView = stockInfoViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curStockInfoView, (from kn in StockInfoMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
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
                int stockInfoID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formStockInfoModify = new FormStockInfoModify(stockInfoID);
                formStockInfoModify.SetModifyFinishedCallback(()=>
                {
                    this.Search();
                });
                formStockInfoModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void comboBoxSearchCondition_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormStockInfoModify();
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
                this.Search();
            });
            form.Show();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
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
            if(deleteIDs.Count == 0)
            {
                MessageBox.Show("请选择您要删除的记录","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除...";


            new Thread(new ThreadStart(()=>
            {
                foreach(int id in deleteIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM StockInfo WHERE ID = @stockInfoID", new SqlParameter("stockInfoID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();
        }
    }
}
