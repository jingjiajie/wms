using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace WMS.UI
{
    class Utilities
    {
        public const int PAGE_SIZE = 50;

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
                    throw new Exception("你的对象里没有" + curKeyName.Key + "这个属性！检查一下你的代码吧！");
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


        private static bool CopyTextToProperty<T>(string text, string propertyName, T targetObject, KeyName[] keyNames, out string errorMessage)
        {
            Type objType = typeof(T);
            PropertyInfo p = objType.GetProperty(propertyName);
            if (p == null)
            {
                throw new Exception("你的对象" + objType.Name + "里没有" + propertyName + "这个属性！检查一下你的代码吧！");
            }

            string chineseName = p.Name;
            var searchedName = (from kn in keyNames where kn.Key == p.Name select kn.Name).ToArray();
            if (searchedName.Length > 0)
            {
                chineseName = searchedName.First();
            }

            Type originType = p.PropertyType;
            //如果文本框的文字为空，并且数据库字段类型不是字符串型，则赋值为NULL
            if (text.Length == 0 && originType != typeof(string))
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
            if (originType == typeof(String))
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
                tableLayoutPanel.Controls.Add(label);

                //如果是编辑框形式
                if (curKeyName.ComboBoxItems == null)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = "textBox" + curKeyName.Key;
                    if (curKeyName.Editable == false)
                    {
                        textBox.ReadOnly = true;
                    }
                    textBox.Dock = DockStyle.Fill;
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
        }

        public static string GenerateNo(string prefix, int id)
        {
            return prefix + id.ToString().PadLeft(5, '0');
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

        public static void InitReoGridImport(ReoGridControl reoGridControl,KeyName[] keyNames)
        {
            //初始化表格
            var worksheet = reoGridControl.Worksheets[0];
            for (int i = 0; i < keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = keyNames[i].ImportVisible;
            }
            worksheet.Columns = keyNames.Length; //限制表的长度
        }

        public static T[] MakeObjectByReoGridImport<T>(ReoGridControl reoGridControl,KeyName[] keyNames,out string errorMessage) where T:new()
        {
            var worksheet = reoGridControl.Worksheets[0];
            List<T> result = new List<T>();
            string[] propertyNames = (from kn in keyNames
                                    select kn.Key).ToArray();
            //遍历行
            for (int line = 0; line < worksheet.Rows; line++)
            {
                //如果是空行，则跳过
                if (IsEmptyLine(reoGridControl, line))
                {
                    continue;
                }

                T newObj = new T();
                result.Add(newObj);
                //遍历列
                for (int col = 0; col < keyNames.Length; col++)
                {
                    //如果字段对导入不可见，或者可见但不导入，则跳过
                    if (keyNames[col].ImportVisible == false || keyNames[col].Import == false)
                    {
                        continue;
                    }
                    //如果单元格为null，则跳过
                    if(worksheet.GetCell(line,col) == null)
                    {
                        continue;
                    }
                    if (CopyTextToProperty(worksheet[line, col].ToString(), propertyNames[col], newObj, keyNames, out errorMessage) == false)
                    {
                        errorMessage = string.Format("行{0}，列{1}：{2}", line, col, errorMessage);
                        return null;
                    }
                }
            }
            errorMessage = null;
            return result.ToArray();
        }

        private static bool IsEmptyLine(ReoGridControl reoGridControl,int line)
        {
            var worksheet = reoGridControl.Worksheets[0];
            for(int col = 0; col < worksheet.Columns; col++)
            {
                //只要有单元格有字，就判定不为空行。如果所有单元格都是null或者没有字，判定为空行。
                if(worksheet.GetCell(line,col) != null && worksheet.GetCell(line, col).Data.ToString().Length != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}