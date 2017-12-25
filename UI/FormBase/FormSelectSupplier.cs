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

namespace WMS.UI.FormBase
{
    public partial class FormSelectSupplier : Form
    {
        private Action<int> selectFinishCallback = null;
        private int defaultSupplierID = -1;

        public FormSelectSupplier()
        {
            InitializeComponent();
        }

        public void SetSelectFinishCallback(Action<int> selectFinishedCallback)
        {
            this.selectFinishCallback = selectFinishedCallback;
        }

        private void InitComponents()
        {
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < SupplierMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = SupplierMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = SupplierMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = SupplierMetaData.KeyNames.Length; //限制表的长度
        }

        private void FormSelectSupplier_Load(object sender, EventArgs e)
        {
            InitComponents();
            if (this.defaultSupplierID != -1)
            {
                WMSEntities wmsEntities = new WMSEntities();
                this.textBoxSupplierName.Text = (from s in wmsEntities.SupplierView where s.ID == defaultSupplierID select s.Name).FirstOrDefault();
                this.Search();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search()
        {
            string supplierName = this.textBoxSupplierName.Text;
            this.labelStatus.Text = "正在搜索...";
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                   
                    SupplierView[] supplierViews = (from s in wmsEntities.SupplierView
                                                    where s.Name.Contains(supplierName)
                                                    orderby s.Name ascending
                                                    select s).ToArray();
                    this.Invoke(new Action(() =>
                    {
                        var worksheet = this.reoGridControlMain.Worksheets[0];
                        worksheet.DeleteRangeData(RangePosition.EntireRange);
                        for (int i = 0; i < supplierViews.Length; i++)
                        {
                            SupplierView curSupplierView = supplierViews[i];
                            object[] columns = Utilities.GetValuesByPropertieNames(curSupplierView, (from kn in SupplierMetaData.KeyNames select kn.Key).ToArray());
                            for (int j = 0; j < worksheet.Columns; j++)
                            {
                                worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                            }
                        }
                        if (supplierViews.Length == 0)
                        {
                            worksheet[0, 2] = "没有查询到符合条件的记录";
                        }
                        this.labelStatus.Text = "搜索完成";
                    }));
                }
                catch (Exception)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            })).Start();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            this.SelectItem();
        }

        private void SelectItem()
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项");
                return;
            }
            this.selectFinishCallback?.Invoke(ids[0]);
            this.Close();
        }

        private void textBoxSupplierName_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSupplierName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.Search();
            }
        }
    }
}
