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

            //初始化搜索条件框
            comboBoxSearchCondition.Items.Add("无");
            comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            comboBoxSearchCondition.SelectedIndex = 0;
            comboBoxSearchCondition.SelectedIndexChanged += (sender, e) =>
            {
                if (comboBoxSearchCondition.SelectedIndex == 0)
                {
                    textBoxSearchCondition.Text = "";
                    textBoxSearchCondition.Enabled = false;
                }
                else
                {
                    textBoxSearchCondition.Enabled = true;
                }
            };

            //初始化排序条件框
            comboBoxOrderByCondition.Items.Add("无");
            comboBoxOrderByCondition.Items.AddRange(visibleColumnNames);
            comboBoxOrderByCondition.SelectedIndex = 0;
            comboBoxOrderByOrder.SelectedIndex = 0;

            this.Dock = DockStyle.Fill;
        }

        public void Search(bool savePage = false, int selectID = -1)
        {
            pagerWidget.ClearCondition();
            pagerWidget.ClearOrderBy();
            if (comboBoxSearchCondition.SelectedIndex != 0)
            {
                string name = comboBoxSearchCondition.SelectedItem.ToString();
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
                    pagerWidget.AddCondition(string.Format("DATEDIFF(day,@value,{0})=0", key), new SqlParameter("value", textBoxSearchCondition.Text));
                }
                else //否则按普通搜索
                {
                    pagerWidget.AddCondition(key, textBoxSearchCondition.Text);
                }
            }
            if(comboBoxOrderByCondition.SelectedIndex != 0)
            {
                string name = comboBoxOrderByCondition.SelectedItem.ToString();
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
                string orderByCond = key;
                if(this.comboBoxOrderByOrder.SelectedIndex == 1) //倒序
                {
                    orderByCond += " DESC";
                }
                pagerWidget.AddOrderBy(orderByCond);
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
            for (int i = 0; i < this.comboBoxSearchCondition.Items.Count; i++)
            {
                var item = comboBoxSearchCondition.Items[i];
                if (item.ToString() == name)
                {
                    this.comboBoxSearchCondition.SelectedIndex = i;
                }
            }
            this.textBoxSearchCondition.Text = value;
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

        private void comboBoxOrderByCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxOrderByCondition.SelectedIndex == 0)
            {
                this.comboBoxOrderByOrder.Enabled = false;
            }
            else
            {
                this.comboBoxOrderByOrder.Enabled = true;
            }
        }
    }
}
