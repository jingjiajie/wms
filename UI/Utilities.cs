using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using unvell.ReoGrid;
using System.Drawing;

namespace WMS.UI
{
    class Utilities
    {
        public const int WM_SETREDRAW = 0xB;

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public static object[] GetValuesByPropertieNames<T>(T obj, string[] keys)
        {
            Type objType = typeof(T);
            object[] values = new object[keys.Length];
            for (int i = 0; i < values.Length; i++)
            {
                string key = keys[i];
                PropertyInfo propertyInfo = objType.GetProperty(key);
                if (propertyInfo == null)
                {
                    throw new Exception("你给的类型"+objType.Name+"里没有" + key + "这个属性！检查检查你的代码吧。");
                }
                values[i] = propertyInfo.GetValue(obj, null);
            }
            return values;
        }

        public static void CopyPropertiesToTextBoxes<T>(T sourceObject, Form form, string textBoxNamePrefix = "textBox")
        {
            PropertyInfo[] stockInfoProperties = sourceObject.GetType().GetProperties();
            foreach (PropertyInfo p in stockInfoProperties)
            {
                Control[] foundControls = form.Controls.Find(textBoxNamePrefix + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                TextBox curTextBox = (TextBox)foundControls[0];
                object value = p.GetValue(sourceObject, null);
                curTextBox.Text = value == null ? "" : value.ToString();
            }
        }

        public static void CopyPropertiesToComboBoxes<T>(T sourceObject, Form form, string comboBoxNamePrefix = "comboBox")
        {
            PropertyInfo[] stockInfoProperties = sourceObject.GetType().GetProperties();
            foreach (PropertyInfo p in stockInfoProperties)
            {
                Control[] foundControls = form.Controls.Find(comboBoxNamePrefix + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                ComboBox curComboBox = (ComboBox)foundControls[0];
                object value = p.GetValue(sourceObject, null);
                if (value == null) continue;
                foreach (object item in curComboBox.Items)
                {
                    if (item.ToString() == value.ToString())
                    {
                        curComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        public static bool CopyComboBoxsToProperties<T>(Form form, T targetObject, KeyName[] keyNames,string textBoxNamePrefix = "comboBox")
        {
            Type objType = typeof(T);
            KeyName[] comboBoxProperties = (from kn in keyNames where kn.ComboBoxItems != null select kn).ToArray();
            foreach(KeyName curKeyName in comboBoxProperties)
            {
                if (curKeyName.Save == false)
                {
                    continue;
                }
                PropertyInfo p = objType.GetProperty(curKeyName.Key);
                if (p == null)
                {
                    throw new Exception("你的对象里没有" + curKeyName.Key + "这个属性！检查一下你的代码吧！");
                }
                Control[] foundControls = form.Controls.Find(textBoxNamePrefix + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                ComboBox comboBox = (ComboBox)foundControls[0];
                object comboBoxValue = null;
                try
                {
                    comboBoxValue = ((ComboBoxItem)comboBox.SelectedItem).Value;
                }
                catch
                {
                    throw new Exception(comboBox.Name + "中Item的类型必须是ComboBoxItem类型，才可以调用Utilities.CopyComboBoxsToProperties！");
                }
                try
                {
                    p.SetValue(targetObject, comboBoxValue, null);
                }
                catch
                {
                    throw new Exception(curKeyName.Key + "的类型与" + comboBox.Name + "选中项的类型不兼容！");
                }
            }
            return true;
        }

        public static bool CopyTextBoxTextsToProperties<T>(Form form, T targetObject, KeyName[] keyNames, out string errorMessage, string textBoxNamePrefix = "textBox")
        {
            Type objType = typeof(T);
            foreach (KeyName curKeyName in keyNames)
            {
                if (curKeyName.Save == false)
                {
                    continue;
                }
                PropertyInfo p = objType.GetProperty(curKeyName.Key);
                if (p == null)
                {
                    throw new Exception("你的类型"+objType.Name+"里没有" + curKeyName.Key + "这个属性！检查一下你的代码吧！");
                }
                Control[] foundControls = form.Controls.Find(textBoxNamePrefix + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                TextBox curTextBox = (TextBox)foundControls[0];
                if(CopyTextToProperty(curTextBox.Text,p.Name, targetObject, keyNames, out errorMessage) == false)
                {
                    return false;
                }
            }
            errorMessage = null;
            return true;
        }


        public static bool CopyTextToProperty<T>(string text, string propertyName, T targetObject, KeyName[] keyNames, out string errorMessage)
        {
            Type objType = typeof(T);
            PropertyInfo p = objType.GetProperty(propertyName);
            if (p == null)
            {
                throw new Exception("你的对象" + objType.Name + "里没有" + propertyName + "这个属性！检查一下你的代码吧！");
            }

            KeyName keyName = (from kn in keyNames where kn.Key == p.Name select kn).FirstOrDefault();
            if(keyName == null)
            {
                throw new Exception(objType.Name + "的KeyNames中不存在" + p.Name + "，请检查你的代码！");
            }
            string chineseName = keyName.Name;

            Type originType = p.PropertyType;
            if(string.IsNullOrWhiteSpace(text) && keyName.NotNull == true)
            {
                errorMessage = chineseName + " 不允许为空！";
                return false;
            }
            //如果文本框的文字为空，并且数据库字段类型不是字符串型，则赋值为NULL
            if (string.IsNullOrWhiteSpace(text) && originType != typeof(string))
            {
                if (IsNullableType(originType))
                {
                    p.SetValue(targetObject, null, null);
                    errorMessage = null;
                    return true;
                }
                else
                {
                    errorMessage = chineseName + " 不允许为空！";
                    return false;
                }
            }
            //根据源类型不同，将编辑框中的文本转换成合适的类型
            if (originType == typeof(string))
            {
                if (text.Length > 64)
                {
                    errorMessage = chineseName + " 长度不允许超过64个字";
                    return false;
                }
                p.SetValue(targetObject, text, null);
            }
            else if (originType == typeof(int?) || originType == typeof(int))
            {
                try
                {
                    p.SetValue(targetObject, int.Parse(text), null);
                }
                catch
                {
                    errorMessage = chineseName + " 只接受整数值";
                    return false;
                }
            }
            else if (originType == typeof(decimal) || originType == typeof(decimal?))
            {
                try
                {
                    decimal value = decimal.Parse(text);
                    if (value > 1e17M || value < -1e17M)
                    {
                        errorMessage = chineseName + " 数值过大，请重新输入";
                        return false;
                    }
                    p.SetValue(targetObject, value, null);
                }
                catch
                {
                    errorMessage = chineseName + " 只接受数值类型";
                    return false;
                }
            }
            else if (originType == typeof(DateTime?) || originType == typeof(DateTime))
            {
                try
                {
                    DateTime dt = DateTime.Parse(text);
                    if (dt < new DateTime(1753, 1, 1) || dt > new DateTime(9999, 12, 31, 23, 59, 59))
                    {
                        errorMessage = chineseName + " 请填写合适的日期";
                        return false;
                    }
                    p.SetValue(targetObject, dt, null);
                }
                catch
                {
                    errorMessage = chineseName + " 只接受日期字符串 年-月-日 (时:分:秒 可选)";
                    return false;
                }
            }
            else
            {
                errorMessage = "内部错误：未知源类型 " + originType;
                return false;
            }

            errorMessage = null;
            return true;
        }

        public static bool IsNullableType(Type type)
        {
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        public static bool IsQuotateType(Type type)
        {
            Type[] quotateTypes = new Type[] //所有需要加引号的数据类型
            {
                typeof(string),typeof(string),typeof(DateTime),typeof(DateTime?)
            };
            foreach (Type t in quotateTypes)
            {
                if (type == t) return true;
            }
            return false;
        }

        public static void CreateEditPanel(TableLayoutPanel tableLayoutPanel,KeyName[] keyNames)
        {
            //初始化属性编辑框
            tableLayoutPanel.Controls.Clear();
            for (int i = 0; i < keyNames.Length; i++)
            {
                KeyName curKeyName = keyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false)
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                label.Dock = DockStyle.Fill;
                label.Font = new Font("微软雅黑", 10);
                label.AutoSize = true;

                tableLayoutPanel.Controls.Add(label);

                //如果是编辑框形式
                if (curKeyName.ComboBoxItems == null)
                {
                    TextBox textBox = new TextBox();
                    textBox.Font = new Font("微软雅黑", 10);

                    //设置了默认值，就添加事件，更改文字后文字变成黑色。默认值在后面统一填入
                    if(curKeyName.DefaultValueFunc != null)
                    {
                        textBox.TextChanged += (obj, e) =>
                        {
                            textBox.ForeColor = Color.Black;
                        };
                    }

                    //如果设置了占位符，则想办法给它模拟出一个占位符来。windows居然不支持，呵呵
                    if (curKeyName.EditPlaceHolder != null)
                    {
                        //加一个label覆盖在上面，看着跟真的placeholder似的
                        Label labelLayer = new Label();
                        labelLayer.Text = curKeyName.EditPlaceHolder;
                        labelLayer.ForeColor = Color.Gray;
                        labelLayer.Font = textBox.Font;
                        labelLayer.AutoSize = true;
                        labelLayer.Click += (obj, e) =>
                        {
                            textBox.Focus();
                            //调用编辑框的点击事件
                            MethodInfo onClickMethod = typeof(TextBox).GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
                            onClickMethod.Invoke(textBox, new object[] { EventArgs.Empty });
                            return;
                        };

                        textBox.Controls.Add(labelLayer);

                        //防止第一次显示的时候有字也显示占位符的囧境
                        textBox.TextChanged += (obj,e)=>{
                            if(textBox.Text.Length != 0)
                            {
                                labelLayer.Hide();
                            }
                        };
                        
                        textBox.Click += (obj, e) =>
                        {
                            labelLayer.Hide();
                            textBox.SelectAll();
                        };
                        textBox.Leave += (obj, e) =>
                        {
                            if (string.IsNullOrWhiteSpace(textBox.Text))
                            {
                                textBox.Text = "";
                                labelLayer.Show();
                            }
                        };
                    }
                    textBox.Name = "textBox" + curKeyName.Key;
                    if (curKeyName.Editable == false)
                    {
                        textBox.ReadOnly = true;
                    }
                    textBox.Dock = DockStyle.Fill;
                    //textBox.MouseEnter += (obj, e) =>
                    //{
                    //    textBox.Focus();
                    //    textBox.SelectAll();
                    //};
                    tableLayoutPanel.Controls.Add(textBox);
                }
                else //否则是下拉列表形式
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.Name = "comboBox" + curKeyName.Key;
                    comboBox.Items.AddRange(curKeyName.ComboBoxItems);
                    comboBox.SelectedIndex = 0;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.Dock = DockStyle.Fill;
                    tableLayoutPanel.Controls.Add(comboBox);
                }
            }
            FillTextBoxDefaultValues(tableLayoutPanel, keyNames);
        }

        public static int[] GetSelectedIDs(ReoGridControl reoGridControl)
        {
            List<int> ids = new List<int>();
            var worksheet = reoGridControl.Worksheets[0];
            for (int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
            {
                if (worksheet[row, 0] == null) continue;
                if (int.TryParse(worksheet[row, 0].ToString(), out int shipmentTicketID))
                {
                    ids.Add(shipmentTicketID);
                }
            }
            return ids.ToArray();
        }

        public static void SelectLineByID(ReoGridControl reoGridControl,int id)
        {
            var worksheet = reoGridControl.Worksheets[0];
            for (int i = 0; i < worksheet.Rows; i++)
            {
                if(worksheet[i,0] == null)
                {
                    continue;
                }
                if (int.TryParse(worksheet[i,0].ToString(),out int value) == true)
                {
                    if(value != id)
                    {
                        continue;
                    }
                    worksheet.SelectionRange = new RangePosition(i,0,1,1);
                    return;
                }
            }
        }
        
        [Obsolete] //已废弃
        public static string GenerateNo(string prefix, int id)
        {
            return prefix + id.ToString().PadLeft(5, '0');
        }

        public static string GenerateTicketNumber(string supplierNumber, DateTime createTime, int rankOfMonth)
        {
            return string.Format("{0}-{1:00}-{2}",supplierNumber,createTime.Month,rankOfMonth);
        }

        public static string GenerateTicketNo(string prefix,DateTime createTime, int rankOfDay/*当天第几张*/)
        {
            return string.Format("{0}{1:0000}{2:00}{3:00}{4:00}{5:00}-{6}", prefix, createTime.Year, createTime.Month, createTime.Day, createTime.Hour, createTime.Minute, rankOfDay);
        }

        /// <summary>
        /// 计算当日单据第几张中的最大数
        /// </summary>
        /// <param name="AllNoOfDay">当天所有的单号（必须是“[前缀][日期时间]-[第几张]“格式）</param>
        /// <returns>返回单据第几张中的最大数</returns>
        public static int GetMaxTicketRankOfDay(string[] allNoOfDay)
        {
            if (allNoOfDay.Length == 0) return 0;
            int[] ranks = new int[allNoOfDay.Length];
            for(int i = 0; i < allNoOfDay.Length; i++)
            {
                if(allNoOfDay[i] == null)
                {
                    ranks[i] = 0;
                    continue;
                }
                string[] segments = allNoOfDay[i].Split('-');
                if(segments.Length != 2 || int.TryParse(segments[1],out ranks[i]) == false)
                {
                    Console.WriteLine("输入单号不合法：" + allNoOfDay[i]);
                    ranks[i] = 0;
                }
            }
            return ranks.Max();
        }

        /// <summary>
        /// 计算当月单据第几张中的最大数
        /// </summary>
        /// <param name="AllNoOfMonth">当月同一供应商所有单子的Number（必须是“[供应商]-[月份]-[当月第几张]“格式）</param>
        /// <returns>返回当月单据第几张中的最大数</returns>
        public static int GetMaxTicketRankOfSupplierAndMonth(string[] allNumberOfSupplierAndMonth)
        {
            if (allNumberOfSupplierAndMonth.Length == 0) return 0;
            int[] ranks = new int[allNumberOfSupplierAndMonth.Length];
            for (int i = 0; i < allNumberOfSupplierAndMonth.Length; i++)
            {
                if (allNumberOfSupplierAndMonth[i] == null)
                {
                    ranks[i] = 0;
                    continue;
                }
                string[] segments = allNumberOfSupplierAndMonth[i].Split('-');
                if (segments.Length != 3 || int.TryParse(segments[2], out ranks[i]) == false)
                {
                    Console.WriteLine("输入Number不合法：" + allNumberOfSupplierAndMonth[i]);
                    ranks[i] = 0;
                }
            }
            return ranks.Max();
        }

        public static void FillTextBoxDefaultValues(TableLayoutPanel editPanel,KeyName[] keyNames,string prefix = "textBox")
        {
            KeyName[] keyNameHasDefaultValueFunc = (from kn in keyNames
                                                    where kn.DefaultValueFunc != null
                                                    select kn).ToArray();
            foreach (KeyName curKeyName in keyNameHasDefaultValueFunc)
            {
                Control[] foundControls = editPanel.Controls.Find(prefix + curKeyName.Key, true);
                if (foundControls.Length == 0) continue;
                TextBox textBox = (TextBox)foundControls[0];
                textBox.Text = curKeyName.DefaultValueFunc();
                textBox.ForeColor = Color.DarkGray;
            }
        }

        public static void InitReoGrid(ReoGridControl reoGrid,KeyName[] keyNames,WorksheetSelectionMode selectionMode = WorksheetSelectionMode.Row)
        {
            //初始化表格
            var worksheet = reoGrid.Worksheets[0];
            worksheet.SelectionMode = selectionMode;
            for (int i = 0; i < keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = keyNames[i].Visible;
            }
            worksheet.Columns = keyNames.Length; //限制表的长度
        }
    }
}