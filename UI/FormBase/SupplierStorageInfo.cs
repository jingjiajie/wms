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
using unvell.ReoGrid.DataFormat;

namespace WMS.UI
{
    public partial class SupplierStorageInfo : Form
    {
        private int supplierid;
        private int projectID = -1;
        private int warehouseID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private PagerWidget<SupplierStorageInfo> pagerWidget = null;
        public SupplierStorageInfo(int supplierid=-1)
        {
            InitializeComponent();
            this.supplierid = supplierid;
        }

      
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void InitializeComponent1()
        {

            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in SupplierStorageInfoMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化查询框
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;

            //初始化分页控件
            //this.pagerWidget = new PagerWidget<SupplierStorageInfoView>(this.reoGridControlUser, SupplierStorageInfoMetaData.KeyNames, this.projectID, this.warehouseID);
            this.panelPager.Controls.Add(pagerWidget);
            pagerWidget.Show();

        }

        private void FormSupplierAnnualInfo_Load(object sender, EventArgs e)
        {
          InitializeComponent1();
          this.pagerWidget.Search();
        }

        private void toolStripComboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.toolStripTextBoxSelect.Text = "";
                this.toolStripTextBoxSelect.Enabled = false;
                
            }
            else
            {
                this.toolStripTextBoxSelect.Enabled = true;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var a1 = new SupplierStorageInfoModify(this.supplierid );

            //a1.SetMode(FormMode.ADD);

            //a1.SetAddFinishedCallback((AddID) =>
            //{
            //    this.pagerWidget.Search(false, AddID);
                
            //});
            a1.Show();
        }
    }
}
