using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace JHRS.PrintClient
{
    [RunInstaller(true)]
    public partial class PrintInstall : Installer
    {
        public PrintInstall()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            string path = this.Context.Parameters["targetdir"];
            //获取用户设定的安装目标路径, 注意，需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\"            
            LogWrite(path);

            const string UriScheme = "jhrsprint";
            const string FriendlyName = "jhrsprint自定义协议";
            using (var key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Classes\\" + UriScheme))
            {
                string applicationLocation = path.Substring(0, path.Length - 1) + @"JHRS.PrintClient.exe";
                LogWrite($"打印客户端安装路径：{applicationLocation}");
                key.SetValue("", "URL:" + FriendlyName);
                LogWrite($"自定义协议名称：URL:{FriendlyName}");
                key.SetValue("URL Protocol", "");

                using (var defaultIcon = key.CreateSubKey("DefaultIcon"))
                {
                    defaultIcon.SetValue("", applicationLocation + ",1");
                }

                using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
                }
                LogWrite($"设置结束！key.Name是：{key.Name}，{key}");
            }
            base.OnAfterInstall(savedState);
        }
        public override void Install(IDictionary stateSaver)
        {
            LogWrite("Install！");
            base.Install(stateSaver);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            LogWrite("OnBeforeInstall!");
            base.OnBeforeInstall(savedState);
        }
        public override void Uninstall(IDictionary savedState)
        {
            LogWrite("Uninstall!"); base.Uninstall(savedState);
        }
        public override void Rollback(IDictionary savedState)
        {
            LogWrite("Rollback");
            base.Rollback(savedState);
        }

        public void LogWrite(string str)
        {
#if DEBUG
            string LogPath = @"c:\log\";
            if (!System.IO.Directory.Exists(LogPath)) System.IO.Directory.CreateDirectory(LogPath);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogPath + @"SetUpLog.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str + "\n");
            }
#endif
        }
    }
}
