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
using System.Reflection;
using System.Data.Entity;

namespace WMS.UI
{
    public partial class StandardImportForm<TargetClass> : Form where TargetClass:class,new()
    {
        private KeyName[] keyNames = null;
        private Func<TargetClass[],Dictionary<string,string[]>,bool> importListener = null;

        public StandardImportForm(KeyName[] keyNames, Func<TargetClass[], Dictionary<string, string[]>, bool> importListener)
        {
            InitializeComponent();
            this.keyNames = keyNames;
            this.importListener = importListener;
        }

        private void StandardImportForm_Load(object sender, EventArgs e)
        {
            Utilities.InitReoGridImport(this.reoGridControlMain, this.keyNames);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            TargetClass[] newObjs = Utilities.MakeObjectByReoGridImport<TargetClass>(this.reoGridControlMain, this.keyNames, out string errorMessage);
            if (newObjs == null)
            {
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newObjs.Length == 0)
            {
                MessageBox.Show("未导入任何数据！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dictionary<string, string[]> unImportedColumns = new Dictionary<string, string[]>();
            for(int i = 0; i < this.keyNames.Length; i++)
            {
                //如果在导入窗口中可见的列设置为不导入，则加入未导入列表中
                if(this.keyNames[i].ImportVisible == true && this.keyNames[i].Import == false)
                {
                    unImportedColumns.Add(this.keyNames[i].Key, this.GetColumn(i, newObjs.Length));
                }
            }
            if (this.importListener(newObjs, unImportedColumns) == false)
            {
                return;
            }
            WMSEntities wmsEntities = new WMSEntities();
            //获取wmsEntities.XXX对象
            PropertyInfo propertyOfTargetClass = wmsEntities.GetType().GetProperty(typeof(TargetClass).Name);
            if(propertyOfTargetClass == null)
            {
                throw new Exception("WMSEntities中不存在" + typeof(TargetClass).Name + "！");
            }
            DbSet<TargetClass> dbSetOfTargetClass = propertyOfTargetClass.GetValue(wmsEntities, null) as DbSet<TargetClass>;
            //获取wmsEntities.XXX.Add()方法
            MethodInfo methodAdd = typeof(DbSet<TargetClass>).GetMethod("Add");
            
            foreach(var obj in newObjs)
            {
                methodAdd.Invoke(dbSetOfTargetClass, new object[] { obj });
            }
            wmsEntities.SaveChanges();
        }

        private string[] GetColumn(int column,int length)
        {
            string[] results = new string[length];
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for(int line = 0; line < length; line++)
            {
                Cell curCell = worksheet.GetCell(line,column);
                if (curCell == null || curCell.Data == null)
                {
                    results[line] = "";
                    continue;
                }
                results[line] = curCell.Data.ToString();
            }
            return results;
        }
    }
}
