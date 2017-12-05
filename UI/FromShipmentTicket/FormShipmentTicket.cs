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
using System.Data.SqlClient;

namespace WMS.UI.FromShipmentTicket
{
    public partial class FormShipmentTicket : Form
    {
        WMSEntities wmsEntities = new WMSEntities();

        public FormShipmentTicket()
        {
            InitializeComponent();
        }

        private void FormShipmentTicket_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in ShipmentTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < ShipmentTicketViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ShipmentTicketViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ShipmentTicketViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = ShipmentTicketViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in ShipmentTicketViewMetaData.KeyNames
                       where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                ShipmentTicketView[] shipmentTicketViews = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    shipmentTicketViews = wmsEntities.Database.SqlQuery<ShipmentTicketView>("SELECT * FROM ShipmentTicketView").ToArray();
                }
                else
                {

                    if (Utilities.IsQuotateType(typeof(ShipmentTicketView).GetProperty(key).PropertyType))
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        shipmentTicketViews = wmsEntities.Database.SqlQuery<ShipmentTicketView>(String.Format("SELECT * FROM ShipmentTicketView WHERE {0} = {1}", key, value)).ToArray();
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
                    if (shipmentTicketViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < shipmentTicketViews.Length; i++)
                    {
                        var curShipmentTicketViews = shipmentTicketViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curShipmentTicketViews, (from kn in ShipmentTicketViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int shipmentTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formShipmentTicketItem = new FormShipmentTicketItem(shipmentTicketID);
                formShipmentTicketItem.SetShipmentTicketStateChangedCallback(() =>
                {
                    this.Invoke(new Action(this.Search));
                });
                formShipmentTicketItem.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormShipmentTicketModify();
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
                this.Search();
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
                int shipmentTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formShipmentTicketModify = new FormShipmentTicketModify(shipmentTicketID);
                formShipmentTicketModify.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                formShipmentTicketModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
