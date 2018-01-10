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

namespace WMS.UI
{
    public partial class FormBaseComponent : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        private int authority;
        private int authority_self = (int)Authority.BASE_COMPONENT;
        int supplierID = -1;
        private int check_history = 0;
        int projectID = -1;
        int warehouseID = -1;
        int userID = -1;
        private Supplier supplier = null;
        private int contractst;   //合同状态
        private int contract_change = 1;
        private PagerWidget<ComponentView> pagerWidget = null;

        public FormBaseComponent(int authority, int supplierID, int projectID, int warehouseID, int userID)
        {
            InitializeComponent();
            this.authority = authority;
            this.supplierID = supplierID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;
        }
        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in ComponenViewMetaData.componenkeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;

            this.pagerWidget = new PagerWidget<ComponentView>(this.reoGridControlComponen, ComponenViewMetaData.componenkeyNames, this.projectID, this.warehouseID);
            this.panelPager.Controls.Add(pagerWidget);
            pagerWidget.Show();
        }

        private void FormBaseComponent_Load(object sender, EventArgs e)
        {
            if(supplierID !=0)
            { this.toolStripButtonAdd.Enabled = false;
                this.toolStripButtonDelete.Enabled = false;
                this.toolStripButtonAlter.Enabled  = false;
            }

            if ((this.authority & authority_self) != authority_self)
            {
                this.contract_change = 0;
                Supplier supplier = (from u in this.wmsEntities.Supplier
                                     where u.ID == supplierID
                                     select u).Single();
                this.supplier = supplier;
                this.contractst = Convert.ToInt32(supplier.ContractState);
                this.toolStripButtonAdd.Enabled = false;
                this.toolStripButtonDelete.Enabled = false;
                if (this.contractst == 0)
                {
                    this.toolStripButtonAlter.Enabled = false;
                }

                InitComponents();

                this.pagerWidget.AddCondition("ID", Convert.ToString(supplierID));
                this.pagerWidget.AddCondition("IsHistory", "0");
                this.pagerWidget.Search();

            }
            if ((this.authority & authority_self) == authority_self)
            {
                InitComponents();
                this.pagerWidget.AddCondition("IsHistory", "0");
                this.pagerWidget.Search();
            }
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            if (this.buttonSearch.Text == "全部信息")
            {
                this.buttonSearch.Text = "查询";
                this.buttonSearch.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                this.toolStripButtonAdd.Enabled = true;
                this.toolStripButtonAlter.Enabled = true;
                this.toolStripComboBoxSelect.Enabled = true;
                this.buttonImport.Enabled = true;
            }

            this.pagerWidget.ClearCondition();
            this.pagerWidget.AddCondition("IsHistory", "0");

            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.toolStripComboBoxSelect.SelectedItem.ToString(), this.textBoxSearchValue.Text);
            }
            if ((this.authority & authority_self) != authority_self)
            {
                this.pagerWidget.AddCondition("ID", Convert.ToString(supplierID));
                this.check_history = 0;
                this.pagerWidget.Search();
            }
            if ((this.authority & authority_self) == authority_self)
            {
                this.check_history = 0;
                this.pagerWidget.Search();
            }
        }


        private void buttonHistorySearch_Click(object sender, EventArgs e)
        {
            if (check_history == 1)
            {
                MessageBox.Show("已经显示历史信息了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.toolStripButtonAdd.Enabled = false;
            this.toolStripButtonAlter.Enabled = false;
            this.toolStripComboBoxSelect.Enabled = false;
            this.buttonImport.Enabled = false;
            this.buttonSearch.DisplayStyle= ToolStripItemDisplayStyle.Text;
            this.buttonSearch.Text = "全部信息";

            this.pagerWidget.ClearCondition();

            var worksheet = this.reoGridControlComponen.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int componenID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                this.pagerWidget.AddCondition("NewestComponentID", Convert.ToString(componenID));
            }

            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.pagerWidget.AddCondition("IsHistory", "1");

            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {

                this.pagerWidget.AddCondition(this.toolStripComboBoxSelect.SelectedItem.ToString(), this.textBoxSearchValue.Text);
            }
            if ((this.authority & authority_self) != authority_self)
            {
                this.pagerWidget.AddCondition("SupplierID", Convert.ToString(supplierID));
                this.check_history = 1;
                this.pagerWidget.Search();
            }
            if ((this.authority & authority_self) == authority_self)
            {
                this.check_history = 1;
                this.pagerWidget.Search();
            }




        }

        //private void Search(int selectedID = -1)
        //    {
        //        string key = null;
        //        string value = null;

        //        if (this.toolStripComboBoxSelect.SelectedIndex != 0)
        //        {
        //            key = (from kn in ComponenViewMetaData.componenkeyNames
        //                   where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
        //                   select kn.Key).First();
        //            value = this.textBoxSearchValue.Text;
        //        }

        //        this.labelStatus.Text = "正在搜索中...";


        //        new Thread(new ThreadStart(() =>
        //        {
        //            var wmsEntities = new WMSEntities();
        //            ComponentView[] componentViews = null;
        //            string sql = "SELECT * FROM ComponentView WHERE 1=1 ";
        //            List<SqlParameter> parameters = new List<SqlParameter>();

        //            if ((this.authority & authority_self) == authority_self)
        //            {

        //                if (this.projectID != -1)
        //                {
        //                    sql += "AND ProjectID = @projectID ";
        //                    parameters.Add(new SqlParameter("projectID", this.projectID));
        //                }
        //                if (warehouseID != -1)
        //                {
        //                    sql += "AND WarehouseID = @warehouseID ";
        //                    parameters.Add(new SqlParameter("warehouseID", this.warehouseID));
        //                }
        //                if (key != null && value != null) //查询条件不为null则增加查询条件
        //                {
        //                    sql += "AND " + key + " = @value ";
        //                    parameters.Add(new SqlParameter("value", value));
        //                }
        //                sql += " ORDER BY ID DESC"; //倒序排序
        //                try
        //                {
        //                    componentViews = wmsEntities.Database.SqlQuery<ComponentView>(sql, parameters.ToArray()).ToArray();
        //                }
        //                catch (EntityCommandExecutionException)
        //                {
        //                    MessageBox.Show("查询失败，请检查输入查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //            }
        //            if ((this.authority & authority_self) == 0)
        //            {

        //                sql += "AND SupplierID = @supplierID ";
        //                parameters.Add(new SqlParameter("supplierID", this.supplierID));

        //                if (this.projectID != -1)
        //                {
        //                    sql += "AND ProjectID = @projectID ";
        //                    parameters.Add(new SqlParameter("projectID", this.projectID));
        //                }
        //                if (warehouseID != -1)
        //                {
        //                    sql += "AND WarehouseID = @warehouseID ";
        //                    parameters.Add(new SqlParameter("warehouseID", this.warehouseID));
        //                }
        //                if (key != null && value != null) //查询条件不为null则增加查询条件
        //                {
        //                    sql += "AND " + key + " = @value ";
        //                    parameters.Add(new SqlParameter("value", value));
        //                }
        //                sql += " ORDER BY ID DESC"; //倒序排序
        //                try
        //                {
        //                    componentViews = wmsEntities.Database.SqlQuery<ComponentView>(sql, parameters.ToArray()).ToArray();
        //                }
        //                catch (EntityCommandExecutionException)
        //                {
        //                    MessageBox.Show("查询失败，请检查输入查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }
        //            }

        //            this.reoGridControlComponen.Invoke(new Action(() =>
        //            {
        //                this.labelStatus.Text = "搜索完成";
        //                var worksheet = this.reoGridControlComponen.Worksheets[0];
        //                worksheet.DeleteRangeData(RangePosition.EntireRange);
        //                if (componentViews.Length == 0)
        //                {
        //                    worksheet[0, 1] = "没有查询到符合条件的记录";
        //                }
        //                for (int i = 0; i < componentViews.Length; i++)
        //                {

        //                    ComponentView curComponentView = componentViews[i];
        //                    object[] columns = Utilities.GetValuesByPropertieNames(curComponentView, (from kn in ComponenViewMetaData.componenkeyNames select kn.Key).ToArray());
        //                    for (int j = 0; j < worksheet.Columns; j++)
        //                    {
        //                        worksheet[i, j] = columns[j];
        //                    }
        //                }
        //                if (selectedID != -1)
        //                {
        //                    Utilities.SelectLineByID(this.reoGridControlComponen, selectedID);
        //                }
        //            }));

        //        })).Start();
        //    }


        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormComponenModify(this.projectID, this.warehouseID, this.supplierID, this.userID);
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback((addedID) =>
            {
                this.pagerWidget.Search(false,addedID);
            });
            form.Show();

        }//添加
  

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int componenID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formComponenModify = new FormComponenModify(this.projectID, this.warehouseID, this.supplierID, this.userID, componenID);
                formComponenModify.SetModifyFinishedCallback((addedID) =>
                {
                    this.pagerWidget.Search(false,addedID);
                });
                formComponenModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }//修改

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
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

                            var componen_historyid = (from kn in wmsEntities.Component
                                                      where kn.NewestComponentID == id
                                                      select kn.ID).ToArray();
                            if (componen_historyid.Length > 0)
                            {
                                try
                                {
                                    foreach (int NewestComponentid in componen_historyid)
                                    {
                                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Component WHERE ID = @componentID", new SqlParameter("componentID", NewestComponentid));


                                    }
                                    wmsEntities.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                        }

                    }
                    catch
                    {
                        MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        foreach (int id in deleteIDs)
                        {
                            this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Component WHERE ID = @componenID", new SqlParameter("componenID", id));
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


        }//删除

        private void textBoxSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.pagerWidget.Search();
            }
        }

        private void toolStripComboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.textBoxSearchValue.Text = "";
                this.textBoxSearchValue.Enabled = false;
                this.textBoxSearchValue.BackColor = Color.LightGray;
            }
            else
            {
                this.textBoxSearchValue.Enabled = true;
                this.textBoxSearchValue.BackColor = Color.White;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButtonComponentSingleBoxTranPackingInfo_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int componenID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var form = new ComponentSingleBoxTranPackingInfoModify(this.userID,componenID);
                if (check_history == 1)
                {
                    form.SetMode(FormMode.CHECK);
                }
                else
                {
                    form.SetMode(FormMode.ALTER);
                }
                form.SetModifyFinishedCallback((addedID) =>
                {
                    this.pagerWidget.Search(false, addedID);
                });
                form.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonComponentOuterPackingSize_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int componenID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var form = new ComponentOuterPackingSizeModify(this.userID,componenID);
                if (check_history == 1)
                {
                    form.SetMode(FormMode.CHECK);
                }
                else
                {
                    form.SetMode(FormMode.ALTER);
                }
                form.SetModifyFinishedCallback((addedID) =>
                {
                    this.pagerWidget.Search(false, addedID);
                });
                form.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonComponentShipmentInfo_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int componenID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var form = new ComponentShipmentInfoModify(this.userID,componenID);

                if (check_history == 1)
                {
                    form.SetMode(FormMode.CHECK);
                }
                else
                {
                    form.SetMode(FormMode.ALTER);
                }
                form.SetModifyFinishedCallback((addedID) =>
                {
                    this.pagerWidget.Search(false, addedID);
                });
                form.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            //创建导入窗口
            StandardImportForm<DataAccess.Component> formImport =
                new StandardImportForm<DataAccess.Component>
                (
                    //参数1：KeyName
                    ComponenViewMetaData.pluscomponenkeyNames,
                    (results, unimportedColumns) => //参数2：导入数据二次处理回调函数
                    {
                        return true;
                    },
                    () => //参数3：导入完成回调函数
                    {
                        this.pagerWidget.Search();
                    }
                );

            //显示导入窗口
            formImport.Show();
        }
    }
    }
