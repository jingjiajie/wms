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

namespace WMS.UI
{
    public partial class FormShipmentTicketItem : Form
    {
        private int shipmentTicketID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        Action shipmentTicketStateChangedCallback = null;

        private KeyName[] visibleColumns = (from kn in ShipmentTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true
                                            select kn).ToArray();

        public FormShipmentTicketItem(int shipmentTicketID)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
        }

        public void SetShipmentTicketStateChangedCallback(Action jobTicketStateChangedCallback)
        {
            this.shipmentTicketStateChangedCallback = jobTicketStateChangedCallback;
        }

        private void FormShipmentTicketItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < ShipmentTicketItemViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ShipmentTicketItemViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ShipmentTicketItemViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = ShipmentTicketItemViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void Search()
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                ShipmentTicketItemView[] shipmentTicketItemViews = (from j in wmsEntities.ShipmentTicketItemView
                                                          where j.ShipmentTicketID == this.shipmentTicketID
                                                          select j).ToArray();

                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "加载完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (shipmentTicketItemViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有符合条件的记录";
                    }
                    for (int i = 0; i < shipmentTicketItemViews.Length; i++)
                    {
                        var curShipmentTicketViews = shipmentTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curShipmentTicketViews, (from kn in ShipmentTicketItemViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < columns.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            const string STRING_FINISHED = "已完成";
            int[] selectedIDs = this.GetSelectedIDs();
            if (selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                //将状态置为已完成
                foreach (int id in selectedIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_FINISHED, id));
                }
                this.wmsEntities.SaveChanges();

                //如果作业单中所有条目都完成，询问是否将作业单标记为完成
                int unfinishedShipmentTicketItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM ShipmentTicketItem WHERE ShipmentTicketID = {0} AND State <> '{1}'", this.shipmentTicketID, STRING_FINISHED)).Single();
                if (unfinishedShipmentTicketItemCount == 0)
                {
                    if (MessageBox.Show("检测到所有的零件都已经收货完成，是否将出库单状态更新为完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", STRING_FINISHED, this.shipmentTicketID));
                        this.wmsEntities.SaveChanges();
                    }
                    this.shipmentTicketStateChangedCallback?.Invoke();
                }
                this.Invoke(new Action(this.Search));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();

        }

        private int[] GetSelectedIDs()
        {
            List<int> ids = new List<int>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for (int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
            {
                if (worksheet[row, 0] == null) continue;
                if (int.TryParse(worksheet[row, 0].ToString(), out int shipmentTicketID))
                {
                    ids.Add(shipmentTicketID);
                }
            }
            return ids.ToArray();
        }

        private void buttonUnfinish_Click(object sender, EventArgs e)
        {
            const string STRING_UNFINISHED = "未完成";
            int[] selectedIDs = this.GetSelectedIDs();
            if (selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                foreach (int id in selectedIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_UNFINISHED, id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //var form = new FormShipmentTicketModify();
            //form.SetMode(FormMode.ADD);
            //form.SetAddFinishedCallback(() =>
            //{
            //    this.Search();
            //});
            //form.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            //var worksheet = this.reoGridControlMain.Worksheets[0];
            //try
            //{
            //    if (worksheet.SelectionRange.Rows != 1)
            //    {
            //        throw new Exception();
            //    }
            //    int shipmentTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
            //    var formShipmentTicketItemModify = new FormShipmentTicketModify(shipmentTicketItemID);
            //    formShipmentTicketItemModify.SetModifyFinishedCallback(() =>
            //    {
            //        this.Search();
            //    });
            //    formShipmentTicketItemModify.Show();
            //}
            //catch
            //{
            //    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }
    }
}
