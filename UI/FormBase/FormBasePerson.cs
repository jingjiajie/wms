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
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI.FormBase
{
    public partial class FormBasePerson : Form
    {
        private PagerWidget<PersonView> pagerWidget = null;
        private WMSEntities wmsEntities = new WMSEntities();
        public FormBasePerson()
        {
            InitializeComponent();
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in BasePersonMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            this.pagerWidget = new PagerWidget<PersonView>(this.reoGridControlPerson, BasePersonMetaData.KeyNames);
            this.panelPager.Controls.Add(pagerWidget);
            pagerWidget.Show();
        }

        private void FormBasePerson_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.pagerWidget.Search();
        }


        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.pagerWidget.ClearCondition();
            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.toolStripComboBoxSelect.SelectedItem.ToString(), this.toolStripTextBoxSelect.Text);
            }
            this.pagerWidget.Search();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var formBasePersonModify = new FormBasePersonModify();
            formBasePersonModify.SetMode(FormMode.ADD);

            formBasePersonModify.SetAddFinishedCallback((addedID) =>
            {
                this.pagerWidget.Search(false, addedID);
                var worksheet = this.reoGridControlPerson.Worksheets[0];
            });
            formBasePersonModify.Show();
        }


        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPerson.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int personID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formBasePersonModify = new FormBasePersonModify(personID);
                formBasePersonModify.SetModifyFinishedCallback((addedID) =>
                {
                    this.pagerWidget.Search(false, addedID);
                });
                formBasePersonModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPerson.Worksheets[0];
            List<int> deleteIDs = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    deleteIDs.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            if (deleteIDs.Count == 0)
            {
                MessageBox.Show("请选择您要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除...";
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    foreach (int id in deleteIDs)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Person WHERE ID = @personID", new SqlParameter("personID", id));
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(() =>
                {
                    this.pagerWidget.Search();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            })).Start();
        }

        private void toolStripTextBoxSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.toolStripButtonSelect.PerformClick();
            }
        }

        private void toolStripComboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.toolStripTextBoxSelect.Text = "";
                this.toolStripTextBoxSelect.Enabled = false;
                this.toolStripTextBoxSelect.BackColor = Color.LightGray;

            }
            else
            {
                this.toolStripTextBoxSelect.Enabled = true;
                this.toolStripTextBoxSelect.BackColor = Color.White;
            }
        }
    }
}
