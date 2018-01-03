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
    public partial class FormUserImport : Form
    {
        public FormUserImport()
        {
            InitializeComponent();
        }

        private void FormUserImport_Load(object sender, EventArgs e)
        {
            Utilities.InitReoGridImport(this.reoGridControlMain, UserMetaData.KeyNames);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            User[] users = Utilities.MakeObjectByReoGridImport<User>(this.reoGridControlMain, UserMetaData.KeyNames, out string errorMessage);
            if (users == null)
            {
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Console.WriteLine(users.Length);
        }
    }
}
