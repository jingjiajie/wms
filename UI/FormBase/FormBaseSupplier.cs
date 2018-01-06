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
using unvell.ReoGrid.DataFormat;


namespace WMS.UI
{
    public partial class FormBaseSupplier : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        private int authority;
        private int authority_supplier = Convert.ToInt32(Authority.BASE_SUPPLIER);
        private int authority_supplierself = Convert.ToInt32(Authority.BASE_SUPPLIER_SUPPLIER_SELFONLY);
        private PagerWidget<SupplierView > pagerWidget = null;
        private Supplier supplier = null;
        private int contractst;
        private int check_history = 0;
       
        private int projectID = -1;
        private int warehouseID = -1;
        private int contract_change = 1;


        private int id=-1;
       
        public FormBaseSupplier(int authority,int supplierid)
        {
            InitializeComponent();
            this.authority = authority;
            this.id = supplierid;
        } 

        private void FormBaseSupplier_Load(object sender, EventArgs e)
        {
            
            if ((this.authority & authority_supplier) != authority_supplier)
            {
                this.contract_change = 0;
                Supplier supplier = (from u in this.wmsEntities.Supplier
                                     where u.ID == id
                                     select u).Single();
                this.supplier = supplier;
                this.contractst = Convert.ToInt32(supplier.ContractState);
                this.toolStripButtonAdd.Enabled = false;
                this.toolStripButtonDelete.Enabled = false;
                this.buttonCheck.Enabled = false;
                if (this.contractst ==0)
                {
                    this.toolStripButtonAlter.Enabled = false;
                }

                InitSupplier();
                //List<SqlParameter> parameters = new List<SqlParameter>();
                //string sql = "SELECT * FROM SupplierView WHERE 1=1";
                //sql += "AND ID = @ID ";
                //parameters.Add(new SqlParameter("ID", id ));
                this.pagerWidget.AddCondition("ID",Convert.ToString(id));
                this.pagerWidget.AddCondition("是否历史信息", "0");
                this.pagerWidget.Search();

            }
            if ((this.authority & authority_supplier) == authority_supplier)
            {
                

                InitSupplier();
                this.pagerWidget.AddCondition("是否历史信息", "0");
                this.pagerWidget.Search();
            }


        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void InitSupplier ()
        {
          
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in SupplierMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化查询框
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;

            //初始化分页控件
            this.pagerWidget = new PagerWidget<SupplierView>(this.reoGridControlUser, SupplierMetaData.KeyNames, this.projectID, this.warehouseID);
            this.panelPager.Controls.Add(pagerWidget);
            pagerWidget.Show();

        }




    

        

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var a1  = new FormSupplierModify();
            
            a1.SetMode(FormMode.ADD);

            a1.SetAddFinishedCallback((AddID ) =>
            {
                this.pagerWidget.Search(false ,AddID );
                //var worksheet = this.reoGridControlUser.Worksheets[0];
                
                //worksheet.SelectionRange = new RangePosition("A1:A1");
            });
            a1.Show();  
        }



        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            if(check_history ==1)
            {
                MessageBox.Show("已经显示历史信息了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.pagerWidget.ClearCondition();

            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int supplierID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                this.pagerWidget.AddCondition("NewestSupplierID", Convert.ToString(supplierID));

            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }      
            
            
            this.pagerWidget.AddCondition("是否历史信息", "1");
            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.toolStripComboBoxSelect.SelectedItem.ToString(), this.toolStripTextBoxSelect.Text);
            }

            if ((this.authority & authority_supplier) != authority_supplier)
            {
            this.pagerWidget.AddCondition("ID", Convert.ToString(id));

                this.check_history = 1;
                this.pagerWidget.Search();
            }
            if ((this.authority & authority_supplier) == authority_supplier)
            {
                this.check_history = 1;
                this.pagerWidget.Search();
            }

            
        }

        private void Search()
        {
            string key = null;
            string value = null;


            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                key = (from kn in SupplierMetaData.KeyNames
                       where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.toolStripTextBoxSelect.Text;
            }
            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet[0, 0] = "加载中...";


            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                SupplierView[] SupplierView = null;
                string sql = "SELECT * FROM SupplierView WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();
                if ((this.authority & authority_supplier) == authority_supplier)
                {
                    if (key != null && value != null) //查询条件不为null则增加查询条件
                    {
                        sql += "AND " + key + " = @value ";
                        parameters.Add(new SqlParameter("value", value));
                    }
                    sql += " ORDER BY ID DESC"; //倒序排序
                    try
                    {
                        SupplierView = wmsEntities.Database.SqlQuery<SupplierView>(sql, parameters.ToArray()).ToArray();
                    }
                    catch (EntityCommandExecutionException)
                    {
                        MessageBox.Show("查询失败，请检查输入条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }



                if ((this.authority & authority_supplier) != authority_supplier)
                {
                    if (id != -1)
                    {
                        sql += "AND ID = @ID ";
                        parameters.Add(new SqlParameter("ID", id));
                    }
                    if (key != null && value != null) //查询条件不为null则增加查询条件
                    {
                        sql += "AND " + key + " = @value ";
                        parameters.Add(new SqlParameter("value", value));
                    }
                    sql += " ORDER BY ID DESC"; //倒序排序
                    SupplierView = wmsEntities.Database.SqlQuery<SupplierView>(sql, parameters.ToArray()).ToArray();

                }






                this.reoGridControlUser.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet1 = this.reoGridControlUser.Worksheets[0];
                    worksheet1.DeleteRangeData(RangePosition.EntireRange);

                    if (SupplierView.Length == 0)
                    {
                        worksheet1[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < SupplierView.Length; i++)
                    {
                        SupplierView curComponent = SupplierView[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curComponent, (from kn in SupplierMetaData.KeyNames


                                                                                              select kn.Key).ToArray());

                        for (int j = 0; j < worksheet1.Columns; j++)
                        {

                            worksheet1[i, j] = columns[j] == null ? "" : columns[j].ToString();
                            worksheet1.SetRangeDataFormat(RangePosition.EntireRange, CellDataFormatFlag.Text, null);
                        }
                    }

                }));
            }

        )).Start();


        }









        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int supplierID  = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1= new FormSupplierModify(supplierID, this.contract_change);
                a1.SetModifyFinishedCallback((AlterID) =>
                {
                    this.pagerWidget.Search(false ,AlterID );
                });
                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }






        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
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
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    foreach (int id in deleteIDs)
                    {
                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Supplier WHERE ID = @supplierID", new SqlParameter("supplierID", id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    this.pagerWidget.Search();
                }));
                
            })).Start();


        }
        

       

        private void toolStripComboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.toolStripTextBoxSelect.Text = "";
                this.toolStripTextBoxSelect.Enabled = false;
                //this.Search();
            }
            else
            {
                this.toolStripTextBoxSelect.Enabled = true;
            }
        }

        private void toolStripTextBoxSelect_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                this.pagerWidget.Search();
            }

        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            //var a1 = new FormSupplierAnnualInfo(1);
            //a1.Show();
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int supplierID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());

                var a1 = new SupplierStorageInfo(supplierID,this.check_history );
                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.pagerWidget.ClearCondition();
            this.pagerWidget.AddCondition("是否历史信息", "0");
            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.toolStripComboBoxSelect.SelectedItem.ToString(), this.toolStripTextBoxSelect.Text);
            }

            if ((this.authority & authority_supplier) != authority_supplier)
            {
                this.pagerWidget.AddCondition("ID", Convert.ToString(id));
                this.check_history = 0;
                this.pagerWidget.Search();
            }
            if ((this.authority & authority_supplier) == authority_supplier)
            {
                this.check_history = 0;
                this.pagerWidget.Search();
            }
        }
    }
    

}
