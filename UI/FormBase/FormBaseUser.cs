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
            toolStripComboBoxSelect.Items.Add("用户名");//设置 combobox下拉控件 会显示的信息
            toolStripComboBoxSelect.Items.Add("权限");
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)//添加 //点击添加按钮 打开FormBaseUserAdd页面
        {
            FormBaseUserAdd ad = new FormBaseUserAdd();
            ad.ShowDialog();//显示后继续进行操作
            Fresh();//刷新
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)//查询 //点击查询按钮
        {
            ShowReoGridControl();//显示所有数据
            if (toolStripComboBoxSelect.Text == "用户名" && toolStripTextBoxSelect.Text != string.Empty)//如果combobox选择为-用户名，且 toolStripTextBoxSelect 中信息不为空
            {
                SearchuUserReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "权限" && toolStripTextBoxSelect.Text != string.Empty)//如果combobox选择为-权限，且 toolStripTextBoxSelect 中信息不为空
            {
                SearchAuthReoGridControl();
            }
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)//修改 //点击修改按钮
        {
            ReoGridControl grid = this.reoGridControlUser;//定义reoGridControl控件
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            //MessageBox.Show(a+"+"+b);
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            string usrname = worksheet1[i - 1, 0].ToString();//选中行的第一列的cell中信息，以string格式提取出来

            WMSEntities wms = new WMSEntities();
            //定义数据库
            User allUsers = (from s in wms.User
                             where s.Username == usrname
                             select s).First<User>();

            string a= allUsers.Username;
            string b= allUsers.Password;

            FormBaseUserAlter al = new FormBaseUserAlter(a,b);//打卡FormBaseUserAlter页面 传递两个参数
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
            wms.SaveChanges();//保存
            worksheet1.Reset();//清空表格
            ShowReoGridControl();//显示所有数据
        }

        private void ShowReoGridControl()//表格显示
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            //设定表头信息
            worksheet1.ColumnHeaders[0].Text = "用户名";
            worksheet1.ColumnHeaders[1].Text = "密码";
            worksheet1.ColumnHeaders[2].Text = "权限";

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            WMSEntities wms = new WMSEntities();
            var allUsers = (from s in wms.User select s).ToArray();
            for (int i = 0; i < allUsers.Count(); i++)
            {
                User user = allUsers[i];
                worksheet1[i, 0] = user.Username;//第一列显示
                worksheet1[i, 1] = user.Password;//第一列显示
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

        private void SearchuUserReoGridControl()//查找用户名
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
