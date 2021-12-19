using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using JHRS.PrintClient.Extensions;

namespace JHRS.PrintClient
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(LogHelper.logFile)) File.Delete(LogHelper.logFile);
            richTextBox1.Text = string.Empty;
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(LogHelper.logFile))
                richTextBox1.Text = File.ReadAllText(LogHelper.logFile);
            bool.TryParse(ConfigurationManager.AppSettings["enablelog"], out bool enablelog);
            checkBox1.Checked = enablelog;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (checkBox1.Checked)
            {
                config.AppSettings.Settings["enablelog"].Value = "true";
            }
            else
            {
                config.AppSettings.Settings["enablelog"].Value = "false";
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}