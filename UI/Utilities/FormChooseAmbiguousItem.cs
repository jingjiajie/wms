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
    public partial class FormChooseAmbiguousSupplyOrComponent : Form
    {
        private class ItemAndString
        {
            public object Item;
            public string String;

            public override string ToString()
            {
                return this.String;
            }
        }

        private object selectedItem = null;

        private FormChooseAmbiguousSupplyOrComponent()
        {
            InitializeComponent();
        }

        private void FormChooseAmbiguousItem_Load(object sender, EventArgs e)
        {

        }

        public static object ChooseAmbiguousSupplyOrComponent(DataAccess.Component[] components,Supply[] supplies,string supplyNoOrComponentName)
        {
            if((components == null || components.Length == 0) && (supplies == null ||supplies.Length == 0))
            {
                return null;
            }
            FormChooseAmbiguousSupplyOrComponent form = new FormChooseAmbiguousSupplyOrComponent();
            if (components != null && components.Length > 0)
            {
                form.listBox.Items.AddRange((from c in components
                                             select new ItemAndString()
                                             {
                                                 Item = c,
                                                 String = c.Name
                                             }).ToArray());
            }
            if (supplies != null && supplies.Length > 0)
            {
                form.listBox.Items.AddRange((from s in supplies
                                             select new ItemAndString()
                                             {
                                                 Item = s,
                                                 String = s.No
                                             }).ToArray());
            }
            //form.listBox.SelectedIndex = 0;
            form.label1.Text = string.Format("您输入的零件\"{0}\"不明确，请选择下列零件中的一个",supplyNoOrComponentName);
            form.ShowDialog();
            return form.selectedItem;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedItem = (this.listBox.SelectedItem as ItemAndString).Item;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
