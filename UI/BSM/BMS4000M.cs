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

namespace WMS.UI.BSM
{
    public partial class BMS4000M : Form
    {
        User userdb = new User();

        public BMS4000M()
        {
            InitializeComponent();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BMS4100M b41 = new BMS4100M();
            b41.Show();
        }


        private void btnAlter_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void BMS4000M_Load(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlMain;
            var worksheet1 = grid.Worksheets[0];
            //grid.Visible = false;

            WMSEntities wms = new WMSEntities();
            var allUsers = (from s in wms.User select s).ToArray();
            for(int i = 0; i < allUsers.Count(); i++)
            {
                User user = allUsers[i];
                worksheet1[i, 0] = user.UserName;
                worksheet1[i, 1] = user.PassWord;
                worksheet1[i, 2] = user.Authority;
            }
        }

    }
}
