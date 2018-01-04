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
    public partial class FormSupplierAnnualInfo : Form
    {
        private int id;
        private int projectID = -1;
        private int warehouseID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private PagerWidget<SupplierAnnualInfoView > pagerWidget = null;
        public FormSupplierAnnualInfo(int supplierid)
        {
            InitializeComponent();
            this.id = supplierid;
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

            string[] visibleColumnNames = (from kn in SupplierAnnualInfoMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化查询框
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;

            //初始化分页控件
            this.pagerWidget = new PagerWidget<SupplierAnnualInfoView>(this.reoGridControlUser, SupplierAnnualInfoMetaData.KeyNames, this.projectID, this.warehouseID);
            this.panelPager.Controls.Add(pagerWidget);
            pagerWidget.Show();

        }

        private void FormSupplierAnnualInfo_Load(object sender, EventArgs e)
        {
          InitializeComponent1();
        }
    }
}
