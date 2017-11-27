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

namespace WMS.UI.frm_base
{
    public partial class base_user : Form
    {
        public base_user()
        {
            InitializeComponent();
        }

        private void base_user_Load(object sender, EventArgs e)
        {
            showreoGridControl();//显示所有数据
            toolStripComboBoxSelect.Items.Add("用户名");
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            base_useradd ad = new base_useradd();
            ad.Show();
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            showreoGridControl();//显示所有数据
            if (toolStripComboBoxSelect.Text == "用户名" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchreoGridControl();
            }
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {

        }

        private void showreoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            WMSEntities wms = new WMSEntities();
            var allUsers = (from s in wms.User select s).ToArray();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                User user = allUsers[i];
                worksheet1[i, 0] = user.UserName;
                worksheet1[i, 1] = user.PassWord;
                if (user.Authority == 0)
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
            }
        }

        private void clearreoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            WMSEntities wms = new WMSEntities();
            var allUsers = (from s in wms.User select s).ToArray();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                User user = allUsers[i];
                worksheet1[i, 0] = user.UserName;
                worksheet1[i, 1] = user.PassWord;
                worksheet1[i, 2] = user.Authority;
            }
        }

        private void searchreoGridControl()
        {
            
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();


            WMSEntities wms = new WMSEntities();

            if (toolStripComboBoxSelect.Text == "用户名" && toolStripTextBoxSelect.Text!= string.Empty)
            {
                
                var nameUsers = (from s in wms.User
                                 where s.UserName == toolStripTextBoxSelect.Text
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
            if(toolStripTextBoxSelect.Text == string.Empty)
            {
                showreoGridControl();//显示所有数据
            }

        }
    }
}
