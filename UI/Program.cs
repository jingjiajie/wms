using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WMS.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SMC2000M());
            //Application.Run(new FormMain());
            //DataAccess.User us = new DataAccess.User();


        }
    }
}
