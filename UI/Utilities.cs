using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WMS.UI
{
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
    }
}
