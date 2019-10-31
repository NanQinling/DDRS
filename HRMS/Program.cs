using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Models;

namespace DDRS
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

            FrmUserLogin objLoginForm = new FrmUserLogin();
            DialogResult result = objLoginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }
        }

        //定义一个全局变量
        public static Admin currentAdmin = null;
        //定义一个全局变量
        public static LoginLog currentLoginLog = null;
        //定义一个日期全局变量
        public static MyDate salaryDate = null;
        //定义一个版本全局变量
        public static string version; 

    }
}
