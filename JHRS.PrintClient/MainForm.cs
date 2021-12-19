using JHRS.PrintClient.Entity;
using JHRS.PrintClient.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace JHRS.PrintClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public string[] args { get; set; }
        /// <summary>
        /// 带参
        /// </summary>
        /// <param name="args"></param>
        public MainForm(string[] args)
        {
            this.args = args;
            InitializeComponent();
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="args"></param>
        private void Print(string[] args)
        {
            var printConfig = ConfigList.FirstOrDefault(x => x.Selected);
            LogHelper.WriteLog($"----------------------------------------------开始打印----------------------------------------------");
            LogHelper.WriteLog($"进入打印方法，打印设置名称：{printConfig?.ConfigName}，打印机名称：【{printConfig?.DefaultPrinter}】，是否启用直接打印功能：{printConfig?.DirectPrint}");

            if (printConfig == null)
            {
                LogHelper.WriteLog("未指定默认打印机，请先做打印设置！");
                LogHelper.WriteLog($"----------------------------------------------结束打印----------------------------------------------\r\n\r\n");
                DialogResult result = MessageBox.Show("未指定默认打印机，请先做打印设置！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    DialogResult dialog = new PrintSet().ShowDialog(this);
                    if (dialog == DialogResult.Cancel)
                    {
                        Print(args);
                    }
                }
                return;
            }

            LogHelper.WriteLog($"打印数据请求接口来源：{args[0].Replace("jhrsprint:", "")}");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(args[0].Replace("jhrsprint:", "")).Result;
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            var data = response.Content.ReadAsStringAsync().Result;
            LogHelper.WriteLog($"打印数据：{data}");
            PrintData printData = JsonConvert.DeserializeObject<PrintData>(data);

            byte[] arrary = printData.FrxFile.Split('^').Select(x => (byte)int.Parse(x)).ToArray();

            report1.Preview = previewControl1;

            report1.Load(new MemoryStream(arrary));
            report1.RegisterData(printData.Data, "打印数据源");
            
            report1.PrintSettings.Printer = printConfig.DefaultPrinter;
            LogHelper.WriteLog($"----------------------------------------------结束打印----------------------------------------------\r\n\r\n");
            if (printConfig.DirectPrint)
            {
                report1.PrintSettings.ShowDialog = false;
                report1.Print();
                ExitSystem();
            }
            else
            {
                report1.Prepare();
                report1.ShowPrepared();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (args != null)
            {
                Print(args);
            }
            SetMenuItem();
        }

        /// <summary>
        /// 设置默认打印机
        /// </summary>
        public void SetMenuItem(bool reload = false)
        {
            reloadConfig = reload;
            var arrary = ConfigList.Select(x => x.ConfigName).ToArray();
            if (arrary.Length == 0)
            {
                toolStripComboBox1.Items.Add("未设置");
                toolStripComboBox1.SelectedIndex = 0;
            }
            else
            {
                toolStripComboBox1.Items.Clear();
                toolStripComboBox1.Items.AddRange(ConfigList.Select(x => x.ConfigName).ToArray());
                var selected = ConfigList.FirstOrDefault(x => x.Selected);
                if (selected != null) toolStripComboBox1.SelectedItem = selected.ConfigName;
            }
        }

        private bool reloadConfig = false;
        private List<Configs> list;
        private List<Configs> ConfigList
        {
            get
            {
                if (list == null || reloadConfig == true)
                {
                    list = XmlHelper.Deserialize<List<Configs>>(Constants.ConfigSavePath);
                    reloadConfig = false;
                }
                if (list != null) return list;
                return list ?? new List<Configs>();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
            var printConfig = ConfigList.FirstOrDefault(x => x.Selected);
            if (printConfig != null && printConfig.DirectPrint)
            {
                ExitSystem();
            }
            else
            {
                DialogResult dr = MessageBox.Show("您确定要退出打印客户端吗？", "系统提示", mess, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                    ExitSystem();
                else
                    e.Cancel = true;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Maximized;
                    this.ShowInTaskbar = true;
                }
                this.Activate();
            }
        }

        private void 退出打印客户端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("您确定要退出打印客户端吗？", "系统提示", mess, MessageBoxIcon.Question);
            if (dr == DialogResult.OK) ExitSystem();
        }

        private void 打印设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var set = new PrintSet();
            if (set.ParentForm == null)
            {
                set.StartPosition = FormStartPosition.CenterScreen;
            }
            set.ShowDialog(this);
        }

        /// <summary>
        /// 更改打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = ConfigList;
            foreach (var item in list)
            {
                if (item.ConfigName == toolStripComboBox1.SelectedItem.ToString())
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }

            XmlHelper.SerializeToXmlFile(list);
        }

        private void 打印日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LogForm().ShowDialog(this);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        private void ExitSystem()
        {
            notifyIcon.Dispose();
            Environment.Exit(0);
        }
    }

    /// <summary>
    /// 打印数据实体
    /// </summary>
    public class PrintData
    {
        /// <summary>
        /// 待打印数据使用的报表，由服务器端返回告诉客户端控件
        /// </summary>
        public string FrxFile { get; set; }

        /// <summary>
        /// 报表数据源，由服务器端返回
        /// </summary>
        public DataTable Data { get; set; }
    }
}