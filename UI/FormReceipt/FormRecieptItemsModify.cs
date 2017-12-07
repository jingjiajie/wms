using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using WMS.UI.FormReceipt;

namespace WMS.UI
{
    public partial class FormReceiptItemsModify : Form
    {
        FormMode formMode;
        WMSEntities wmsEntities = new WMSEntities();
        int receiptTicketItemID;
        Action CallBack = null;
        public FormReceiptItemsModify()
        {
            InitializeComponent();
        }
        public FormReceiptItemsModify(FormMode formMode, int receiptTicketItemsID)
        {
            InitializeComponent();
            this.formMode = formMode;
            this.receiptTicketItemID = receiptTicketItemsID;
        }

        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private void FormRecieptTicketItemsModify_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < ReceiptMetaData.receiptNameKeys.Length; i++)
            {
                KeyName curKeyName = ReceiptMetaData.receiptNameKeys[i];

                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (this.formMode == FormMode.ALTER && curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }

                this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            }
            if (this.formMode == FormMode.ALTER)
            {

                ReceiptTicketItemView receiptTicketItemView = (from s in this.wmsEntities.ReceiptTicketItemView
                                                               where s.ID == this.receiptTicketItemID
                                                               select s).Single();
                Utilities.CopyPropertiesToTextBoxes(receiptTicketItemView, this);

            }
        }
    }
}
