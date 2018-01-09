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
    public partial class FormOtherInfo : Form
    {
        private int setitem=-1;

        private PagerWidget<PackageUnitView> PackageUnitpagerWidget = null;
        private PagerWidget<WarehouseView> warehousepagerWidget = null;
        private PagerWidget<ProjectView> projectpagerWidget = null;
        private WMSEntities wmsEntities = new WMSEntities();
        public FormOtherInfo(int setitem)
        {
            InitializeComponent();
            this.setitem = setitem;
        }

        private void InitComponents()
        {
            this.reoGridControlWarehouse.SetSettings(WorkbookSettings.View_ShowHorScroll, false);
            this.reoGridControlWarehouse.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);
            this.reoGridControlProject.SetSettings(WorkbookSettings.View_ShowHorScroll,false);
            this.reoGridControlProject.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);
            this.reoGridControlPackageUnit.SetSettings(WorkbookSettings.View_ShowHorScroll, false);
            this.reoGridControlPackageUnit.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);

            string[] visibleColumnNames = (from kn in FormBase.BaseWarehouseMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            this.warehousepagerWidget = new PagerWidget<WarehouseView>(this.reoGridControlWarehouse, FormBase.BaseWarehouseMetaData.KeyNames);
            this.warehousepagerWidget.SetPageSize(-1);
            //this.panelPager1.Controls.Add(warehousepagerWidget);
            //warehousepagerWidget.Show();

            string[] visibleColumnNames1= (from kn in FormBase.BaseProjectMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            this.projectpagerWidget = new PagerWidget<ProjectView>(this.reoGridControlProject, FormBase.BaseProjectMetaData.KeyNames);
            this.projectpagerWidget.SetPageSize(-1);
            //this.panelPager2.Controls.Add(projectpagerWidget);
            //projectpagerWidget.Show();

            string[] visibleColumnNames2 = (from kn in FormBase.BasePackageUnitMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            this.PackageUnitpagerWidget = new PagerWidget<PackageUnitView>(this.reoGridControlPackageUnit, FormBase.BasePackageUnitMetaData.KeyNames);
            this.PackageUnitpagerWidget.SetPageSize(-1);
            //this.panelPager3.Controls.Add(PackageUnitpagerWidget);
            //PackageUnitpagerWidget.Show();

        }


        private void base_Warehouse_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.warehousepagerWidget.Search();
            this.projectpagerWidget.Search();
            this.PackageUnitpagerWidget.Search();

        }



        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            this.setitem = 0;
            var form = new FormBase.FormBaseWarehouseModify(this.setitem);
            form.SetMode(FormMode.ADD);

                form.SetAddFinishedCallback((addedID) =>
                {
                    this.warehousepagerWidget.Search(false, addedID);
                });


            form.Show();

        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
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
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Warehouse WHERE ID = @warehouseID", new SqlParameter("warehouseID", id));
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
                    this.warehousepagerWidget.Search();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));


                
            })).Start();
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            this.setitem = 0;
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int ID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1 = new FormBase.FormBaseWarehouseModify(this.setitem,ID);

                    a1.SetModifyFinishedCallback((addedID) =>
                    {
                        this.warehousepagerWidget.Search(false, addedID);
                    });

                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonAddProject_Click(object sender, EventArgs e)
        {
            var formBaseProjectModify = new FormBase.FormBaseProjectModify();
            formBaseProjectModify.SetMode(FormMode.ADD);
            formBaseProjectModify.SetAddFinishedCallback((addedID) =>
            {
                this.projectpagerWidget.Search(false, addedID);
                var worksheet = this.reoGridControlProject.Worksheets[0];
            });
            formBaseProjectModify.Show();
        }

        private void toolStripButtonAlterProject_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlProject.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int projectID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formBaseProjectModify = new FormBase.FormBaseProjectModify(projectID);
                formBaseProjectModify.SetModifyFinishedCallback((addedID) =>
                {
                    this.projectpagerWidget.Search(false, addedID);
                });
                formBaseProjectModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonDelectProject_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlProject.Worksheets[0];
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
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Project WHERE ID = @projectID", new SqlParameter("projectID", id));
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
                    this.projectpagerWidget.Search();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            })).Start();
        }

        private void toolStripButtonAddPackageUnit_Click(object sender, EventArgs e)
        {
            this.setitem = 1;
            var form = new FormBase.FormBaseWarehouseModify(this.setitem);
            form.SetMode(FormMode.ADD);

            form.SetAddFinishedCallback((addedID) =>
            {
                this.PackageUnitpagerWidget.Search(false, addedID);
            });

            form.Show();
        }

        private void toolStripButtonAlterPackageUnit_Click(object sender, EventArgs e)
        {
            this.setitem = 1;
            var worksheet = this.reoGridControlPackageUnit.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int ID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1 = new FormBase.FormBaseWarehouseModify(this.setitem, ID);

                a1.SetModifyFinishedCallback((addedID) =>
                {
                    this.PackageUnitpagerWidget.Search(false, addedID);
                });

                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void toolStripButtonDelectPackageUnit_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPackageUnit.Worksheets[0];
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
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM PackageUnit WHERE ID = @packageUnitID", new SqlParameter("packageUnitID", id));
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
                    this.PackageUnitpagerWidget.Search();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            })).Start();
        }


        private void reoGridControlProject_Click(object sender, EventArgs e)
        {

        }

        //启用双缓冲技术
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
