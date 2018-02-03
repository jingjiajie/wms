using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class SearchWidget<T> : UserControl
    {
        private PagerWidget<T> pagerWidget = null;
        KeyName[] keyNames = null;


        public SearchWidget(KeyName[] keyNames, PagerWidget<T> pagerWidget)
        {
            InitializeComponent();
            this.pagerWidget = pagerWidget;
            this.keyNames = keyNames;
            
            string[] visibleColumnNames = (from kn in keyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            comboBoxCondition.Items.Add("无");
            comboBoxCondition.Items.AddRange(visibleColumnNames);
            comboBoxCondition.SelectedIndex = 0;
            comboBoxCondition.SelectedIndexChanged += (sender, e) =>
            {
                if (comboBoxCondition.SelectedIndex == 0)
                {
                    textBoxCondition.Text = "";
                    textBoxCondition.Enabled = false;
                }
                else
                {
                    textBoxCondition.Enabled = true;
                }
            };
            
            this.Dock = DockStyle.Fill;
        }

        public void Search(bool savePage = false, int selectID = -1)
        {
            pagerWidget.ClearCondition();
            if (comboBoxCondition.SelectedIndex != 0)
            {
                string name = comboBoxCondition.SelectedItem.ToString();
                string key = (from kn in this.keyNames where kn.Name == name select kn.Key).FirstOrDefault();
                if (key == null)
                {
                    throw new Exception("SearchWidget找不到字段 " + name + " 对应的Key，请检查传入的KeyNames");
                }
                //判断搜索字段类型
                PropertyInfo property = typeof(T).GetProperty(key);
                if (property == null)
                {
                    throw new Exception(typeof(T).Name + " 中不存在字段 " + key + " 请检查程序！");
                }
                //如果是日期类型，按模糊搜索
                if (property.PropertyType == typeof(DateTime) || (property.PropertyType == typeof(DateTime?)))
                {
                    pagerWidget.AddCondition(string.Format("DATEDIFF(day,@value,{0})=0", key), new SqlParameter("value", textBoxCondition.Text));
                }
                else //否则按普通搜索
                {
                    pagerWidget.AddCondition(key, textBoxCondition.Text);
                }
            }
            pagerWidget.Search(savePage, selectID);
        }

        public void SetSearchCondition(string key, string value)
        {
            string name = (from kn in JobTicketViewMetaData.KeyNames
                           where kn.Key == key
                           select kn.Name).FirstOrDefault();
            if (name == null)
            {
                return;
            }
            for (int i = 0; i < this.comboBoxCondition.Items.Count; i++)
            {
                var item = comboBoxCondition.Items[i];
                if (item.ToString() == name)
                {
                    this.comboBoxCondition.SelectedIndex = i;
                }
            }
            this.textBoxCondition.Text = value;
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                this.buttonSearch.PerformClick();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }
    }
}
