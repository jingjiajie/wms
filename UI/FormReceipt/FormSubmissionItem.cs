using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.UI;
using unvell.ReoGrid;
using WMS.DataAccess;
using System.Threading;

namespace WMS.UI.FormReceipt
{
    public partial class FormSubmissionItem : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        private int submissionTicketID;
        public FormSubmissionItem()
        {
            InitializeComponent();
        }

        public FormSubmissionItem(int submissionTicketID)
        {
            InitializeComponent();
            this.submissionTicketID = submissionTicketID;
        }

        private void FormSubmissionItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            InitPanel();
            WMSEntities wmsEntities = new WMSEntities();
            
            Search();
        }

        private void InitPanel()
        {
            WMSEntities wmsEntities = new WMSEntities();
            //this.Controls.Clear();
            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, ReceiptMetaData.submissionTicketItemKeyName);

            this.reoGridControlSubmissionItems.Worksheets[0].SelectionRangeChanged += worksheet_SelectionRangeChanged;

            //TextBox textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
            //textBoxComponentName.Click += textBoxComponentName_Click;
            //textBoxComponentName.ReadOnly = true;
            //textBoxComponentName.BackColor = Color.White;
        }

        private void worksheet_SelectionRangeChanged(object sender, unvell.ReoGrid.Events.RangeEventArgs e)
        {
            this.RefreshTextBoxes();
        }

        private void RefreshTextBoxes()
        {
            this.ClearTextBoxes();
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlSubmissionItems);
            if (ids.Length == 0)
            {
                this.submissionTicketID = -1;
                return;
            }
            int id = ids[0];
            SubmissionTicketItemView submissionTicketItemView = (from s in this.wmsEntities.SubmissionTicketItemView
                                                             where s.ID == id
                                                             select s).FirstOrDefault();
            if (submissionTicketItemView == null)
            {
                MessageBox.Show("系统错误，未找到相应送检单项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.submissionTicketID = int.Parse(submissionTicketItemView.SubmissionTicketID.ToString());
            Utilities.CopyPropertiesToTextBoxes(submissionTicketItemView, this);
            //Utilities.CopyPropertiesToComboBoxes(shipmentTicketItemView, this);
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

        private void InitComponents()
        {
            //初始化
            string[] columnNames = (from kn in ReceiptMetaData.submissionTicketItemKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.submissionTicketItemKeyName[i].Visible;
            }
            worksheet.Columns = columnNames.Length;
        }

        private void Search()
        {
            this.labelStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                //ReceiptTicketView[] receiptTicketViews = null;
                SubmissionTicketItemView[] submissionTicketItemView = null;
                
                submissionTicketItemView = wmsEntities.Database.SqlQuery<SubmissionTicketItemView>(String.Format("SELECT * FROM SubmissionTicketItemView WHERE SubmissionTicketID={0}", submissionTicketID)).ToArray();
                               
                this.reoGridControlSubmissionItems.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < submissionTicketItemView.Length; i++)
                    {
                        if (submissionTicketItemView[i].State == "作废")
                        {
                            continue;
                        }
                        SubmissionTicketItemView curSubmissionTicketItemView = submissionTicketItemView[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curSubmissionTicketItemView, (from kn in ReceiptMetaData.submissionTicketItemKeyName select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[n, j] = columns[j];
                        }
                        n++;
                    }
                }));
                this.Invoke(new Action(this.RefreshTextBoxes));
            })).Start();

        }

        private void buttonItemPass_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int submissionTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                SubmissionTicket submissionTicket = (from st in wmsEntities.SubmissionTicket where st.ID == submissionTicketID select st).Single();
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicketItem SET State='合格' WHERE ID={0}", submissionTicketItemID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicket SET State='部分合格' WHERE ID={0}", submissionTicketID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ReceiptTicket SET State='部分合格' WHERE ID={0}", submissionTicket.ID));

            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Search();
            this.RefreshTextBoxes();
        }

        private void buttonItemNoPass_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int submissionTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicketItem SET State='不合格' WHERE ID={0}", submissionTicketItemID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicket SET State='部分合格' WHERE ID={0}", submissionTicketID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ReceiptTicket SET State='部分合格' WHERE ID={0}", submissionTicketID));
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Search();
            this.RefreshTextBoxes();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlSubmissionItems);
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            new Thread(new ThreadStart(() =>
            {
                int id = ids[0];
                var submissionTicketItem = (from s in this.wmsEntities.SubmissionTicketItem where s.ID == id select s).FirstOrDefault();

                if (submissionTicketItem == null)
                {
                    MessageBox.Show("未找到此送检单单条目信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                submissionTicketItem.SubmissionTicketID = this.submissionTicketID;

                if (Utilities.CopyTextBoxTextsToProperties(this, submissionTicketItem, ReceiptMetaData.submissionTicketItemKeyName, out string errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlSubmissionItems.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int submissionTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE SubmissionTicketItem SET State='作废' WHERE ID={0}", submissionTicketItemID));
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ReceiptTicket SET State='部分送检' WHERE ID={0}", submissionTicketID));
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Search();
            this.RefreshTextBoxes();
        }
    }
}
