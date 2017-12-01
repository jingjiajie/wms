using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WMS.UI
{
    class KeyName
    {
        public string Key;
        public string Name;
        public bool Visible = true;
    }

    class Utilities
    {
        public const int WM_SETREDRAW = 0xB;

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        
        public static object[] GetValuesByPropertieNames<T>(T obj, string[] keys)
        {
            Type objType = obj.GetType();
            object[] values = new object[keys.Length];
            for (int i = 0; i < values.Length; i++)
            {
                string key = keys[i];
                PropertyInfo propertyInfo = objType.GetProperty(key);
                values[i] = propertyInfo.GetValue(obj, null);
            }
            return values;
        }

        public static void CopyPropertiesToTextBoxes<T>(T sourceObject,Form form,string textBoxNamePrefix = "textBox")
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

        public static bool CopyTextBoxTextsToProperties<T>(Form form,T targetObject,KeyName[] keyNames,out string errorMessage,string textBoxNamePrefix="textBox")
        {
            PropertyInfo[] stockInfoProperties = typeof(T).GetProperties();
            foreach (PropertyInfo p in stockInfoProperties)
            {
                Control[] foundControls = form.Controls.Find(textBoxNamePrefix + p.Name, true);
                if (foundControls.Length == 0)
                {
                    continue;
                }
                TextBox curTextBox = (TextBox)foundControls[0];
                string chineseName = p.Name;
                var searchedName = (from kn in keyNames where kn.Key == p.Name select kn.Name).ToArray();
                if(searchedName.Length > 0)
                {
                    chineseName = searchedName.First();
                }

                Type originType = p.PropertyType;
                //如果文本框的文字为空，并且数据库字段类型不是字符串型，则赋值为NULL
                if (curTextBox.Text.Length == 0 && originType != typeof(string))
                {
                    try
                    {
                        p.SetValue(targetObject, null, null);
                        continue;
                    }
                    catch
                    {
                        errorMessage = chineseName + " 不允许为空！";
                        return false;
                    }
                }
                //根据源类型不同，将编辑框中的文本转换成合适的类型
                if (originType == typeof(String))
                {
                    p.SetValue(targetObject, curTextBox.Text, null);
                }
                else if (originType == typeof(int?) || originType == typeof(int))
                {
                    try
                    {
                        p.SetValue(targetObject, int.Parse(curTextBox.Text), null);
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
                        p.SetValue(targetObject, decimal.Parse(curTextBox.Text), null);
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
                        p.SetValue(targetObject, DateTime.Parse(curTextBox.Text), null);
                    }
                    catch
                    {
                        errorMessage = chineseName + " 只接受日期字符串(年-月-日)";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "内部错误：未知源类型 " + originType;
                    return false;
                }
            }
            errorMessage = null;
            return true;
        }
    }
}
