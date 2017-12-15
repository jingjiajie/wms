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
    public partial class FormStockInfoCheckTicketComponentModify : Form
    {
        private Action<int> selectFinishCallback = null;
        private WMSEntities wmsEntities = new WMSEntities();
        public FormStockInfoCheckTicketComponentModify()
        {
            InitializeComponent();

        }
        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in StockInfoViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect1.Items.Add("无");
            this.toolStripComboBoxSelect1.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect1.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            worksheet.SelectionMode = unvell.ReoGrid.WorksheetSelectionMode.Row;
            for (int i = 0; i < StockInfoViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = StockInfoViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = StockInfoViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = StockInfoViewMetaData.KeyNames.Length;//限制表的长度
            Console.WriteLine("表格行数：" + StockInfoViewMetaData.KeyNames.Length);

            this.tableLayoutPanel2.Controls.Clear();
            for (int i = 0; i < StockInfoCheckTicksModifyMetaDate.KeyNames.Length; i++)
            {
                KeyName curKeyName = StockInfoCheckTicksModifyMetaDate.KeyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false)
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel2.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanel2.Controls.Add(textBox);
            }



        }

        private void FormStockInfoCheckTicketComponentModify_Load(object sender, EventArgs e)
        {
            this.InitComponents();
            this.Search();


        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
        private void Search()
        {
            string key = null;
            string value = null;

            if (this.toolStripComboBoxSelect1.SelectedIndex != 0)
            {
                key = (from kn in StockInfoCheckTickettModifyViewMetaData1.KeyNames
                       where kn.Name == this.toolStripComboBoxSelect1.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";


            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                var wmsEntities = new WMS.DataAccess.WMSEntities();

                WMS.DataAccess.StockInfoView [] componentViews = null;


               
                    if (key == null || value == null)        //搜索所有
                    {
                        componentViews = wmsEntities.Database.SqlQuery<WMS.DataAccess.StockInfoView >("SELECT * FROM StockInfoView").ToArray();
                    }
                    else
                    {
                      
                        value = "'" + value + "'";
                      
                        try
                        {
                            componentViews = wmsEntities.Database.SqlQuery<WMS.DataAccess.StockInfoView>(String.Format("SELECT * FROM StockInfoView WHERE {0} = {1}", key, value)).ToArray();
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
                    worksheet.DeleteRangeData(unvell.ReoGrid.RangePosition.EntireRange);
                    for (int i = 0; i < componentViews.Length; i++)
                    {

                        WMS.DataAccess.StockInfoView curComponentView = componentViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curComponentView, (from kn in StockInfoViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j];
                        }
                    }
                }));

            })).Start();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
        //    this.SelectItem();
        }

        //private void SelectItem()
        //{
        //    int[] ids = Utilities.GetSelectedIDs(this.reoGridControlComponen);
        //    if (ids.Length != 1)
        //    {
        //        MessageBox.Show("请选择一项");
        //        return;
        //    }
        //    this.selectFinishCallback?.Invoke(ids[0]);

        //}



        //private int[] GetSelectedIDs()
        //{
        //    List<int> ids = new List<int>();
        //    var worksheet = this.reoGridControlComponen.Worksheets[0];
        //    for (int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
        //    {
        //        if (worksheet[row, 0] == null) continue;
        //        if (int.TryParse(worksheet[row, 0].ToString(), out int shipmentTicketID))
        //        {
        //            ids.Add(shipmentTicketID);
        //        }
        //    }
        //    return ids.ToArray();


        //}

            private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reoGridControlComponen_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlComponen);
            
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项");
                return;
            }
            int b = ids[0];
            WMS.DataAccess.StockInfoView a = (from s in this.wmsEntities.StockInfoView 
                                             where s.ID == (b)
                                             select s).Single();
            Utilities.CopyPropertiesToTextBoxes(a, this);

        }
    }
}
