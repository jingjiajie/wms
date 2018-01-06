using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {

        }

        private void reoGridControlMain_LostFocus(object sender, EventArgs e)
        {

        }

        private void FormTest_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.EndEdit(new unvell.ReoGrid.EndEditReason());
        }
    }
}
