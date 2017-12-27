using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormStockInfoCheckTicketModify : Form
    {
        private FormMode mode = FormMode.ALTER;
        private int stockInfoCheckID = -1;
        private int projectID = -1;
        private int warehouseID = -1;
        private int userID = -1;
        private WMS.DataAccess.WMSEntities wmsEntities = new WMS.DataAccess.WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        //private Action checkFinishedCallback = null;
       



        public FormStockInfoCheckTicketModify(int projectID, int warehouseID,int userID ,int stockInfoCheckID=-1)
        {
            InitializeComponent();
            this.stockInfoCheckID = stockInfoCheckID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;

        }

        private void FormStockCheckModify_Load(object sender, EventArgs e)
        {

            if (this.mode == FormMode.ALTER && this.stockInfoCheckID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            if(this.mode==FormMode.ADD||this.mode==FormMode.ALTER)
            {
                this.reoGridControlMain .Visible = false;
                this.buttonAdd.Visible = false;

                this.buttonDelete.Visible = false;
                this.buttonfinish.Visible = false;
                this.Size = new Size(500, 300);
                this.MinimizeBox = false;
                this.MaximizeBox = false;
               

            }
            if (this.mode == FormMode.ADD)
                {
                this.labelStatus.Text = "添加盘点单";

                 }
            if (this.mode == FormMode.ALTER)
            {
                this.labelStatus.Text = "修改盘点单";

            }
            if (this.mode==FormMode.CHECK)
            {
                this.buttonOK.Visible = false;
                this.buttonCancel.Visible = false;
                this.labelStatus.Text = "盘点单条目";
                // this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;


            }

            for (int i = 0; i < StockInfoCheckTicketViewMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = StockInfoCheckTicketViewMetaData.KeyNames[i];
                
                if (curKeyName.Visible == false && curKeyName.Editable == false) //&& curKeyName.Name != "ID")
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel2.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false||this.mode==FormMode.CHECK)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanel2.Controls.Add(textBox);
            }



            if (this.mode == FormMode.ALTER||this.mode==FormMode.CHECK)
            {
                WMS.DataAccess.StockInfoCheckTicketView  stockInfoCheckView = (from s in this.wmsEntities.StockInfoCheckTicketView
                                               where s.ID == this.stockInfoCheckID
                                               select s).Single();

                Utilities.CopyPropertiesToTextBoxes(stockInfoCheckView, this);
            }


            this.InitComponents();
            this.Search();

        }
        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in StockInfoCheckTicksModifyMetaDateDisplay.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            //this.toolStripComboBoxSelect1.Items.Add("无");
            //this.toolStripComboBoxSelect1.Items.AddRange(visibleColumnNames);
            //this.toolStripComboBoxSelect1.SelectedIndex = 0;

           
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = unvell.ReoGrid.WorksheetSelectionMode.Row;
            for (int i = 0; i < StockInfoCheckTicksModifyMetaDateDisplay.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = StockInfoCheckTicksModifyMetaDateDisplay.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = StockInfoCheckTicksModifyMetaDateDisplay.KeyNames[i].Visible;
            }
            worksheet.Columns = StockInfoCheckTicksModifyMetaDateDisplay.KeyNames.Length;//限制表的长度
           
        }



            private void buttonDelete_Click(object sender, EventArgs e)
        {
            WMS.DataAccess.StockInfoCheckTicket   stockInfoCheck = null;
            
            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    stockInfoCheck = (from s in this.wmsEntities.StockInfoCheckTicket
                                      where s.ID == this.stockInfoCheckID
                                      select s).Single();
                    stockInfoCheck.LastUpdateUserID = Convert.ToString(userID);
                }
                catch
                {
                    MessageBox.Show("要修改的项目已不存在，请确认后操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                    return;
                }
            }
            else if (mode == FormMode.ADD)
            {
                stockInfoCheck = new WMS.DataAccess.StockInfoCheckTicket();
                this.wmsEntities.StockInfoCheckTicket.Add(stockInfoCheck);
                stockInfoCheck.CreateUserID = userID;
            }
            stockInfoCheck.WarehouseID = warehouseID;
            stockInfoCheck.ProjectID  = projectID;
            
            stockInfoCheck.LastUpdateUserID = Convert.ToString( userID);
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, stockInfoCheck, StockInfoCheckTicketViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            wmsEntities.SaveChanges();
            //调用回调函数
          
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }
            else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback();
            }


            this.Close();


        }
        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
        }

        public void SetAddFinishedCallback(Action callback)
        {
            this.addFinishedCallback = callback;
        }
     

        private void tableLayoutPanelProperties_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormStockInfoCheckTicketComponentModify a1 = new FormStockInfoCheckTicketComponentModify(this.stockInfoCheckID);
            a1.SetAddFinishedCallback(() =>
            {
                this.Search();
               
            });
            a1.Show();
        }




        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改盘点单信息";
                this.buttonOK.Text = "修改盘点单信息";
               
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加盘点单信息";
                this.buttonOK.Text = "添加盘点单信息";
               
            }
            else if (mode == FormMode.CHECK)
                this.Text = "盘点单条目";
                

        }

        private void Search()
        {
           var worksheet = this.reoGridControlMain .Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                WMS.DataAccess.StockInfoCheckTicketItemView[] stockInfoViews = null;
                string sql = "SELECT * FROM StockInfoCheckTicketItemView WHERE 1=1";
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (this.stockInfoCheckID != -1)
                {
                    sql += "AND StockInfoCheckTicketID = @StockInfoCheckTicketID ";
                    parameters.Add(new SqlParameter("StockInfoCheckTicketID", this.stockInfoCheckID));
                }


                sql += " ORDER BY ID DESC"; //倒序排序
                stockInfoViews = wmsEntities.Database.SqlQuery<WMS.DataAccess.StockInfoCheckTicketItemView>(sql, parameters.ToArray()).ToArray();
                this.reoGridControlMain .Invoke(new Action(() =>
                {
                    
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (stockInfoViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < stockInfoViews.Length; i++)
                    {
                        WMS.DataAccess.StockInfoCheckTicketItemView curStockInfoView = stockInfoViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curStockInfoView, (from kn in StockInfoCheckTicksModifyMetaDateDisplay.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                    if(this.mode ==FormMode.CHECK )
                    this.labelStatus.Text = "搜索完成";
                }));

            })).Start();
        }

        

        private void buttonfinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
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
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM StockInfoCheckTicketItem WHERE ID = @stockCheckID", new SqlParameter("stockCheckID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB1_s;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonOK_MouseDown(object sender, MouseEventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonCancel_MouseEnter(object sender, EventArgs e)
        {
            buttonCancel.BackgroundImage = WMS.UI.Properties.Resources.bottonB4_s;
        }

        private void buttonCancel_MouseLeave(object sender, EventArgs e)
        {
            buttonCancel.BackgroundImage = WMS.UI.Properties.Resources.bottonB4_q;
        }

        private void buttonCancel_MouseDown(object sender, MouseEventArgs e)
        {
            buttonCancel.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_s;
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

        private void buttonfinish_MouseEnter(object sender, EventArgs e)
        {
            buttonfinish.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonfinish_MouseLeave(object sender, EventArgs e)
        {
            buttonfinish.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonfinish_MouseDown(object sender, MouseEventArgs e)
        {
            buttonfinish.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
    
}
