﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using WMS.DataAccess;

namespace WMS.UI
{
  
    public partial class FromBaseUser : Form
    {
        public FromBaseUser()
        {
            InitializeComponent();
        }

        private void base_user_Load(object sender, EventArgs e)
        {
            ShowReoGridControl();//显示所有数据
            toolStripComboBoxSelect.Items.Add("用户名");
            toolStripComboBoxSelect.Items.Add("权限");
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)//添加
        {
            FormBaseUserAdd ad = new FormBaseUserAdd();
            ad.ShowDialog();
            Fresh();
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)//查询
        {
            ShowReoGridControl();//显示所有数据
            if (toolStripComboBoxSelect.Text == "用户名" && toolStripTextBoxSelect.Text != string.Empty)
            {
                SearchuUserReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "权限" && toolStripTextBoxSelect.Text != string.Empty)
            {
                SearchAuthReoGridControl();
            }
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)//修改
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            //MessageBox.Show(a+"+"+b);
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            string usrname = worksheet1[i - 1, 0].ToString();


            WMSEntities wms = new WMSEntities();
            User allUsers = (from s in wms.User
                             where s.Username == usrname
                             select s).First<User>();

            string a= allUsers.Username;
            string b= allUsers.Password;

            FormBaseUserAlter al = new FormBaseUserAlter(a,b);
            al.ShowDialog();
            Fresh();


        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)//删除
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            string usrname = worksheet1[i - 1, 0].ToString();

            WMSEntities wms = new WMSEntities();
            User allUsers = (from s in wms.User
                              where s.Username == usrname
                              select s).First<User>();
            wms.User.Remove(allUsers);//删除
            wms.SaveChanges();
            worksheet1.Reset();
            ShowReoGridControl();//显示所有数据
        }

        private void ShowReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.ColumnHeaders[0].Text = "用户名";
            worksheet1.ColumnHeaders[1].Text = "密码";
            worksheet1.ColumnHeaders[2].Text = "权限";

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            WMSEntities wms = new WMSEntities();
            var allUsers = (from s in wms.User select s).ToArray();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                User user = allUsers[i];
                worksheet1[i, 0] = user.Username;
                worksheet1[i, 1] = user.Password;
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
        }//表格显示

        private void ClearReoGridControl()//没用函数
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            WMSEntities wms = new WMSEntities();
            var allUsers = (from s in wms.User select s).ToArray();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                User user = allUsers[i];
                worksheet1[i, 0] = user.Username;
                worksheet1[i, 1] = user.Password;
                worksheet1[i, 2] = user.Authority;
            }
        }

        private void SearchuUserReoGridControl()
        {        
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();
            WMSEntities wms = new WMSEntities();
                var allUsers = (from s in wms.User
                                 where s.Username == toolStripTextBoxSelect.Text
                                 select s).ToArray();
                for (int i = 0; i < allUsers.Count(); i++)
                {
                    User userb = allUsers[i];
                    worksheet1[i, 0] = userb.Username;
                    worksheet1[i, 1] = userb.Password;
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
        }//查找用户名

        private void SearchAuthReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();
            WMSEntities wms = new WMSEntities();

            int authority=0;
            if(toolStripTextBoxSelect.Text=="无")
            {
                authority = 0;
            }
            if (toolStripTextBoxSelect.Text == "管理员")
            {
                authority = 15;
            }
            if (toolStripTextBoxSelect.Text == "收货员")
            {
                authority = 2;
            }
            if (toolStripTextBoxSelect.Text == "发货员")
            {
                authority = 4;
            }
            if (toolStripTextBoxSelect.Text == "结算员")
            {
                authority = 8;
            }
            var allUsers = (from s in wms.User
                             where s.Authority == authority
                             select s).ToArray();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                User userb = allUsers[i];
                worksheet1[i, 0] = userb.Username;
                worksheet1[i, 1] = userb.Password;
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
        }//查找权限

        private void Fresh()//刷新表格
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();
            ShowReoGridControl();//显示所有数据
        }

    }
}
