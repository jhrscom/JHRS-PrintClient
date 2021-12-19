using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JHRS.PrintClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Debugger.Launch();

            if (args.Length == 0)
                Application.Run(new MainForm());
            else
                Application.Run(new MainForm(args));
        }
    }
}