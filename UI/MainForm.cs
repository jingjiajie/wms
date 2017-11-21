using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“wmsDataSet.User”中。您可以根据需要移动或删除它。
            this.userTableAdapter.Fill(this.wmsDataSet.User);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var wmsEntities = new WMSEntities();
            var res = wmsEntities.User.Where((user) => true);
            foreach(User u in res)
            {
                Console.WriteLine(u.UserName);
            }
        }
    }
}
