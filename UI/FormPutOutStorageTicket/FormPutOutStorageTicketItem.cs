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
    public partial class FormPutOutStorageTicketItem : Form
    {
        private int putOutStorageTicketID = -1;
        private int curStockInfoID = -1;

        private KeyName[] visibleColumns = (from kn in PutOutStorageTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true
                                            select kn).ToArray();

        public FormPutOutStorageTicketItem(int putOutStorageTicketID = -1)
        {
            InitializeComponent();
            this.putOutStorageTicketID = putOutStorageTicketID;
        }

        private void FormPutOutStorageTicketItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < PutOutStorageTicketItemViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = PutOutStorageTicketItemViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = PutOutStorageTicketItemViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = PutOutStorageTicketItemViewMetaData.KeyNames.Length; //限制表的长度

            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, PutOutStorageTicketItemViewMetaData.KeyNames);
            worksheet.SelectionRangeChanged += worksheet_SelectionRangeChanged;

            TextBox textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
            textBoxComponentName.Click += textBoxComponentName_Click;
            textBoxComponentName.BackColor = Color.White;

        }

        private void textBoxComponentName_Click(object sender, EventArgs e)
        {
            TextBox textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
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

        private void worksheet_SelectionRangeChanged(object sender, unvell.ReoGrid.Events.RangeEventArgs e)
        {
            this.RefreshTextBoxes();
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in this.tableLayoutPanelProperties.Controls)
            {
                if (control is TextBox)
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
            int[] ids = this.GetSelectedIDs();
            if(ids.Length == 0)
            {
                this.curStockInfoID = -1; //如果没有选择任何一项，则库存信息ID也为空
                return;
            }

            int id = ids[0];
            this.labelStatus.Text = "加载中...";
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                PutOutStorageTicketItemView putOutStorageTicketItemView = (from p in wmsEntities.PutOutStorageTicketItemView
                                                                           where p.ID == id
                                                                           select p).FirstOrDefault();
                if (putOutStorageTicketItemView == null)
                {
                    MessageBox.Show("系统错误，未找到相应出库单项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.curStockInfoID = putOutStorageTicketItemView.StockInfoID;
                this.Invoke(new Action(()=>
                {
                    this.labelStatus.Text = "加载完成";
                    Utilities.CopyPropertiesToTextBoxes(putOutStorageTicketItemView, this);
                    Utilities.CopyPropertiesToComboBoxes(putOutStorageTicketItemView, this);
                }));
            })).Start();
        }

        private void Search(int selectID = -1)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                PutOutStorageTicketItemView[] putOutStorageTicketItemViews = (from j in wmsEntities.PutOutStorageTicketItemView
                                                                              where j.PutOutStorageTicketID == this.putOutStorageTicketID
                                                                              orderby j.ID descending
                                                                              select j).ToArray();

                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "加载完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (putOutStorageTicketItemViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有符合条件的记录";
                    }
                    for (int i = 0; i < putOutStorageTicketItemViews.Length; i++)
                    {
                        var curPutOutStorageTicketViews = putOutStorageTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curPutOutStorageTicketViews, (from kn in PutOutStorageTicketItemViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < columns.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                    if(selectID != -1)
                    {
                        Utilities.SelectLineByID(this.reoGridControlMain, selectID);
                    }
                    this.RefreshTextBoxes();
                }));
            })).Start();
        }

      
        private int[] GetSelectedIDs()
        {
            List<int> ids = new List<int>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for (int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
            {
                if (worksheet[row, 0] == null) continue;
                if (int.TryParse(worksheet[row, 0].ToString(), out int putOutStorageTicketID))
                {
                    ids.Add(putOutStorageTicketID);
                }
            }
            return ids.ToArray();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.curStockInfoID == -1)
            {
                MessageBox.Show("未选择零件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PutOutStorageTicketItem putOutStorageTicketItem = new PutOutStorageTicketItem();
            putOutStorageTicketItem.StockInfoID = this.curStockInfoID;
            putOutStorageTicketItem.PutOutStorageTicketID = this.putOutStorageTicketID;

            if (Utilities.CopyTextBoxTextsToProperties(this, putOutStorageTicketItem, PutOutStorageTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                wmsEntities.PutOutStorageTicketItem.Add(putOutStorageTicketItem);
                wmsEntities.SaveChanges();
                this.Invoke(new Action(()=>
                {
                    this.Search(putOutStorageTicketItem.ID);
                }));
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (this.curStockInfoID == -1)
            {
                MessageBox.Show("未选择零件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = ids[0];

            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                PutOutStorageTicketItem putOutStorageTicketItem = (from p in wmsEntities.PutOutStorageTicketItem where p.ID == id select p).FirstOrDefault();
                if (Utilities.CopyTextBoxTextsToProperties(this, putOutStorageTicketItem, PutOutStorageTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                putOutStorageTicketItem.StockInfoID = this.curStockInfoID;
                wmsEntities.SaveChanges();
                this.Invoke(new Action(() =>
                {
                    this.Search(putOutStorageTicketItem.ID);
                }));
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.labelStatus.Text = "正在删除...";
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                foreach (int id in ids)
                {
                    wmsEntities.Database.ExecuteSqlCommand("DELETE FROM PutOutStorageTicketItem WHERE ID = @id", new SqlParameter("@id", id));
                }
                wmsEntities.SaveChanges();
                this.Search();
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            })).Start();
        }
    }
}
