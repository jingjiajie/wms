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

namespace WMS.UI
{
    public partial class FormBaseComponent : Form
    {
        class KeyName
        {
            public string Key;
            public string Name;
            public bool Visible = true;
        }

        private KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="WarehouseID",Name="仓库ID"},
            new KeyName(){Key="SupplierID",Name="供货商ID"},
            new KeyName(){Key="ContainerNo",Name="容器号"},
            new KeyName(){Key="Factroy",Name="工厂"},
            new KeyName(){Key="WorkPosition",Name="工位"},
            new KeyName(){Key="No",Name="零件代号"},
            new KeyName(){Key="Name",Name="零件名称"},
            new KeyName(){Key="SupplierType",Name="A系列/B系列供应商"},
            new KeyName(){Key="Type",Name="机型区分"},
            new KeyName(){Key="Size",Name="尺寸（大件/小件）"},
            new KeyName(){Key="Category",Name="分类"},
            new KeyName(){Key="GroupPrincipal",Name="分组负责人"},
            new KeyName(){Key="SingleCarUsageAmount",Name="单台用量"},
            new KeyName(){Key="ChargeBelow50000",Name="物流服务费1-50000台套"},
            new KeyName(){Key="ChargeAbove50000",Name="物流服务费50000台套以上"},
            new KeyName(){Key="InventoryRequirement1Day",Name="1天库存要求"},
            new KeyName(){Key="InventoryRequirement3Day",Name="3天库存要求"},
            new KeyName(){Key="InventoryRequirement5Day",Name="5天库存要求"},
            new KeyName(){Key="InventoryRequirement10Day",Name="10天库存要求"},

        };
        public FormBaseComponent()
        {
            InitializeComponent();
        }
        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in this.keyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < this.keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = this.keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = this.keyNames[i].Visible;
            }
            worksheet.Columns = this.keyNames.Length;//限制表的长度
            Console.WriteLine("表格行数：" + this.keyNames.Length);
        }

        private void FormBaseComponent_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search(null, null);
            //showreoGridControl();//显示所有数据
            //toolStripComboBoxSelect.Items.Add("零件ID");
            //toolStripComboBoxSelect.Items.Add("仓库ID");
            //toolStripComboBoxSelect.Items.Add("供货商ID");
            //toolStripComboBoxSelect.Items.Add("容器号");
            //toolStripComboBoxSelect.Items.Add("工厂");
            //toolStripComboBoxSelect.Items.Add("工位");
            //toolStripComboBoxSelect.Items.Add("零件代号");
            //toolStripComboBoxSelect.Items.Add("零件名称");
            //toolStripComboBoxSelect.Items.Add("A系列/B系列供应商");
            //toolStripComboBoxSelect.Items.Add("机型区分");
            //toolStripComboBoxSelect.Items.Add("尺寸（大件/小件）");
            //toolStripComboBoxSelect.Items.Add("分类");
            //toolStripComboBoxSelect.Items.Add("分组负责人");
            //toolStripComboBoxSelect.Items.Add("单台用量");
            //toolStripComboBoxSelect.Items.Add("物流服务费1-50000台套");
            //toolStripComboBoxSelect.Items.Add("物流服务费50000台套以上");
            //toolStripComboBoxSelect.Items.Add("1天库存要求");
            //toolStripComboBoxSelect.Items.Add("3天库存要求");
            //toolStripComboBoxSelect.Items.Add("5天库存要求");
            //toolStripComboBoxSelect.Items.Add("10天库存要求");

        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.Search(null, null);
                return;
            }
            else
            {
                string key = (from kn in this.keyNames
                              where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                              select kn.Key).First();
                string value = this.toolStripTextBoxSelect.Text;
                this.Search(key, value);
                return;
            }
        }

    private void Search(string key, string value)
        {
            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlComponen.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();

                DataAccess.Component[] Components = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    Components = wmsEntities.Database.SqlQuery<DataAccess.Component>("SELECT * FROM Component").ToArray();
                }
                else
                {
                    if (Double.TryParse(value, out double tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        Components = wmsEntities.Database.SqlQuery<DataAccess.Component>(String.Format("SELECT * FROM Component WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlComponen.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (Components.Length == 0)
                    {
                        worksheet[1, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < Components.Length; i++)
                    {
                        DataAccess.Component curComponent = Components[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curComponent, (from kn in this.keyNames select kn.Key).ToArray());
                        for (int j = 0; j < keyNames.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }


        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormBase.FormBaseComponenAdd ad = new FormBase.FormBaseComponenAdd();
            ad.ShowDialog();
            this.Search(null, null); 

        }//添加
  

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型



            String name = worksheet1[i - 1, 7].ToString();

            WMSEntities wms = new WMSEntities();
            DataAccess.Component allComponen = (from s in wms.Component
                                                where s.Name == name
                                                select s).First();
            int a = allComponen.ID;
            int b = allComponen.WarehouseID;
            int c = allComponen.SupplierID;
            string d = allComponen.ContainerNo;
            string e1 = allComponen.Factroy;
            string f = allComponen.WorkPosition;
            string g = allComponen.No;
            string h = allComponen.Name;
            string i1 = allComponen.SupplierType;
            string j = allComponen.Type;
            string k = allComponen.Size;
            string l = allComponen.Category;
            string m = allComponen.GroupPrincipal;
            var n = Convert.ToString(allComponen.SingleCarUsageAmount);
            var o = Convert.ToString(allComponen.ChargeBelow50000);
            var p = Convert.ToString(allComponen.ChargeAbove50000);
            var q = Convert.ToString(allComponen.InventoryRequirement1Day);
            var r = Convert.ToString(allComponen.InventoryRequirement3Day);
            var s1 = Convert.ToString(allComponen.InventoryRequirement5Day);
            var t = Convert.ToString(allComponen.InventoryRequirement10Day);
               
            FormBase.FormBaseComponenAlter adc = new FormBase.FormBaseComponenAlter(a,b,c,d,e1,f,g,h,i1,j,k,l,m,n,o,p,q,r,s1,t);
            adc.ShowDialog();
            this.Search(null, null); 
        }//修改

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            String name = worksheet1[i - 1, 7].ToString();

            WMSEntities wms = new WMSEntities();
            DataAccess.Component allComponen = (from s in wms.Component
                                              where s.Name == name
                                              select s).First();
            wms.Component.Remove(allComponen);//删除
            wms.SaveChanges();
            worksheet1.Reset();
            this.Search(null, null);//显示所有数据

        }//删除
    }
}
