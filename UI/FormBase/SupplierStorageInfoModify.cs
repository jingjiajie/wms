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
    public partial class SupplierStorageInfoModify : Form
    {
        private int supplierID = -1;
        public SupplierStorageInfoModify(int supplierid)
        {
            InitializeComponent();
            this.supplierID = supplierid;
        }

        private void FormSupplierAnnualInfoModify_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < SupplierStorageInfoMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = SupplierStorageInfoMetaData.KeyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false)
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanel1.Controls.Add(textBox);
            }
        }
    }
}