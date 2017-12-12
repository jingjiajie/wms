﻿using System;
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

namespace WMS.UI.FormReceipt
{
    public partial class FormShelvesItem : Form
    {
        private int putawayTicketItemID;
        private int putawayTicketID;
        WMSEntities wmsEntities = new WMSEntities();
        public FormShelvesItem()
        {
            InitializeComponent();
        }

        public FormShelvesItem(int putawayTicketID)
        {
            InitializeComponent();
            this.putawayTicketID = putawayTicketID;
        }

        private void FormShelvesItem_Load(object sender, EventArgs e)
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
            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, ReceiptMetaData.putawayTicketItemKeyName);

            this.reoGridControlPutaway.Worksheets[0].SelectionRangeChanged += worksheet_SelectionRangeChanged;

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
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlPutaway);
            if (ids.Length == 0)
            {
                this.putawayTicketItemID = -1;
                return;
            }
            int id = ids[0];
            PutawayTicketItemView putawayTicketItemView = (from s in this.wmsEntities.PutawayTicketItemView
                                                           where s.ID == id
                                                           select s).FirstOrDefault();
            if (putawayTicketItemView == null)
            {
                MessageBox.Show("系统错误，未找到相应上架单项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.putawayTicketItemID = int.Parse(putawayTicketItemView.ID.ToString());
            Utilities.CopyPropertiesToTextBoxes(putawayTicketItemView, this);
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
            string[] columnNames = (from kn in ReceiptMetaData.putawayTicketItemKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.putawayTicketItemKeyName[i].Visible;
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
                PutawayTicketItemView[] putawayTicketItemView = null;

                putawayTicketItemView = wmsEntities.Database.SqlQuery<PutawayTicketItemView>(String.Format("SELECT * FROM PutawayTicketItemView WHERE PutawayTicketID={0}", putawayTicketID)).ToArray();

                this.reoGridControlPutaway.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlPutaway.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < putawayTicketItemView.Length; i++)
                    {
                        if (putawayTicketItemView[i].State == "作废")
                        {
                            continue;
                        }
                        PutawayTicketItemView curputawayTicketItemView = putawayTicketItemView[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curputawayTicketItemView, (from kn in ReceiptMetaData.putawayTicketItemKeyName select kn.Key).ToArray());
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

        private void buttonModify_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                if (putawayTicketItem == null)
                {
                    MessageBox.Show("错误 无法修改此条目");
                }
                else
                {
                    string errorInfo;
                    if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicketItem, ReceiptMetaData.putawayTicketItemKeyName, out errorInfo) == false)
                    {
                        MessageBox.Show(errorInfo);
                        return;
                    }
                    else
                    {
                        new Thread(() =>
                        {
                            wmsEntities.SaveChanges();
                            this.Invoke(new Action(() =>
                            {
                                Search();
                            }));
                            MessageBox.Show("成功");
                        }).Start();
                    }
                }
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                if (putawayTicketItem == null)
                {
                    MessageBox.Show("错误 无法修改此条目");
                }
                else
                {
                    putawayTicketItem.State = "作废";
                    ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == putawayTicketItem.ReceiptTicketItemID select rti).FirstOrDefault();
                    if (receiptTicketItem != null)
                    {
                        receiptTicketItem.State = "待收货";
                    }
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                        this.Invoke(new Action(() =>
                        {
                            Search();
                        }));
                        MessageBox.Show("成功");
                    }).Start();
                }
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonFInished_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                if (putawayTicketItem != null)
                {
                    putawayTicketItem.State = "已上架";
                    StockInfo stockInfo = ReceiptUtilities.PutawayTicketItemToStockInfo(putawayTicketItem);
                    wmsEntities.StockInfo.Add(stockInfo);
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                        MessageBox.Show("成功");
                        this.Invoke(new Action(() =>
                        {
                            this.Search();
                        }));
                    }).Start();
                }
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                if (putawayTicketItem != null)
                {
                    if (putawayTicketItem.State != "已上架")
                    {
                        MessageBox.Show("该上架单项目未上架");
                        this.Search();
                    }
                    else
                    {
                        putawayTicketItem.State = "待上架";
                       // StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.PutawayTicketItemID == putawayTicketItem.ID select si).FirstOrDefault();
                        
                    }
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                        MessageBox.Show("成功");
                        this.Invoke(new Action(() =>
                        {
                            this.Search();
                        }));
                    }).Start();
                }
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}