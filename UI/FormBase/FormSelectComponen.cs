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

namespace WMS.UI
{
    public partial class FormSelectComponen : Form
    {
        private Action<int> selectFinishCallback = null;
        private int defaultComponenID = -1;

        public FormSelectComponen()
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
            for (int i = 0; i < ComponenViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ComponenViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ComponenViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = ComponenViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void FormSelectComponen_Load(object sender, EventArgs e)
        {
            InitComponents();
            if (this.defaultComponenID != -1)
            {
                WMSEntities wmsEntities = new WMSEntities();
                this.textBoxComponenName.Text = (from s in wmsEntities.ComponentView where s.ID == defaultComponenID select s.Name).FirstOrDefault();
                this.Search();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search()
        {
            string componenName = this.textBoxComponenName.Text;
            this.labelStatus.Text = "正在搜索...";
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                   
                    ComponentView[] componenViews = (from s in wmsEntities.ComponentView
                                                    where s.Name.Contains(componenName)
                                                    orderby s.Name ascending
                                                    select s).ToArray();
                    this.Invoke(new Action(() =>
                    {
                        var worksheet = this.reoGridControlMain.Worksheets[0];
                        worksheet.DeleteRangeData(RangePosition.EntireRange);
                        for (int i = 0; i < componenViews.Length; i++)
                        {
                            ComponentView curComponenView = componenViews[i];
                            object[] columns = Utilities.GetValuesByPropertieNames(curComponenView, (from kn in ComponenViewMetaData.KeyNames select kn.Key).ToArray());
                            for (int j = 0; j < worksheet.Columns; j++)
                            {
                                worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                            }
                        }
                        if (componenViews.Length == 0)
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

        private void textBoxComponenName_Click(object sender, EventArgs e)
        {

        }

        private void textBoxComponenName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.Search();
            }
        }
    }
}
