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
            
            ReoGridControl grid = this.reoGridControlMain;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            //var allUsers = (from s in wms.User select s).ToArray();
            //for (int i = 0; i < allUsers.Count(); i++)
            //{
            //    User user = allUsers[i];
            //    worksheet1[i, 0] = null;
            //    worksheet1[i, 1] = null;
            //    worksheet1[i, 2] = null;
            //}

            if (toolStripComboBox1.Text == "用户名")
                {
                    //查找
                    var nameUsers = (from s in wms.User
                                     where s.UserName == toolStripTextBox1.Text
                                     select s).ToArray();
                    for (int i = 0; i < nameUsers.Count(); i++)
                    {
                        User userb = nameUsers[i];
                        worksheet1[i, 0] = userb.UserName;
                        worksheet1[i, 1] = userb.PassWord;
                        if (userb.Authority == 0)
                        {
                            worksheet1[i, 2] = "无";

                        }
                        if (userb.Authority == 15)
                        {
                            worksheet1[i, 2] = "管理员";

                        }
                        if (userb.Authority == 2)
                        {
                            worksheet1[i, 2] = "收货员";

                        }
                        if (userb.Authority == 4)
                        {
                            worksheet1[i, 2] = "发货员";

                        }
                        if (userb.Authority == 8)
                        {
                            worksheet1[i, 2] = "结算员";

                        }
                    }
                }
            
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
            toolStripComboBox1.Items.Add("用户名");
            toolStripComboBox1.Items.Add("权限");


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
                if(user.Authority==0)
                {
                    worksheet1[i, 2] = "无";

                }
                if (user.Authority == 15)
                {
                    worksheet1[i, 2] = "管理员";

                }
                if (user.Authority == 2)
                {
                    worksheet1[i, 2] = "收货员";

                }
                if (user.Authority == 4)
                {
                    worksheet1[i, 2] = "发货员";

                }
                if (user.Authority == 8)
                {
                    worksheet1[i, 2] = "结算员";

                }
                //worksheet1[i, 2] = user.Authority;
            }
        }

    }
}
