using JHRS.PrintClient.Entity;
using JHRS.PrintClient.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JHRS.PrintClient
{
    public partial class PrintSet : Form
    {
        public PrintSet()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (this.Owner as MainForm).SetMenuItem(true);
            this.Close();
        }

        private List<Configs> cc;
        public List<Configs> ConfigList
        {
            get
            {
                if (cc != null) return cc;
                cc = XmlHelper.Deserialize<List<Configs>>(Constants.ConfigSavePath);
                if (cc == null) cc = new List<Configs>();
                return cc;
            }
            set { cc = value; }
        }

        /// <summary>
        /// 保存打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请输入设置名称，如：请假单打印。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            if (ConfigList.Any(x => x.ConfigName == txtName.Text.Trim()) && "ㅡ请选择ㅡ".Equals(cboConfigs.SelectedItem.ToString()))
            {
                MessageBox.Show($"当前设置名称【{txtName.Text.Trim()}】已被占用，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ("ㅡ请选择ㅡ".Equals(cboPrinters.SelectedItem.ToString()))
            {
                MessageBox.Show("请选择打印机！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Configs update = ConfigList.FirstOrDefault(x => x.ConfigName == cboConfigs.SelectedItem.ToString());

            Configs config = update ?? new Configs();

            config.ConfigName = txtName.Text.Trim();
            if (chkDefault.Checked)
            {
                ConfigList.ForEach(x => { x.Selected = false; });
                config.Selected = true;
            }
            config.DefaultPrinter = cboPrinters.SelectedItem.ToString();
            config.DirectPrint = chkDirectPrint.Checked ? true : false;
            if (update == null) //新增操作
            {
                config.Index = ConfigList.Count + 1;
                if (!ConfigList.Any(x => x.Selected))
                {
                    config.Selected = true;
                }
                ConfigList.Add(config);
            }
            XmlHelper.SerializeToXmlFile(ConfigList);
            MessageBox.Show($"保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PrintSet_Load(sender, e);
        }

        /// <summary>
        /// 窗体打开时加载默认配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintSet_Load(object sender, EventArgs e)
        {
            ResetForm();
            BindData();
            InintData(ConfigList.FirstOrDefault(x => x.Selected));
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindData()
        {
            cboPrinters.Items.Add("ㅡ请选择ㅡ");
            cboConfigs.Items.Add("ㅡ请选择ㅡ");
            cboConfigs.SelectedIndex = 0;
            cboConfigs.Items.AddRange(ConfigList.Select(x => x.ConfigName).ToArray());

            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                cboPrinters.Items.Add("没有找到可用打印机！");
                label7.Text = "没有找到可用打印机！";
            }
            else
            {
                foreach (string item in PrinterSettings.InstalledPrinters)//获取所有打印机名称
                {
                    cboPrinters.Items.Add(item);
                }
                cboPrinters.SelectedIndex = 1;
            }
        }

        /// <summary>
        ///初始化数据
        /// </summary>
        private void InintData(Configs current)
        {
            if (current != null)
            {
                cboConfigs.SelectedItem = current.ConfigName;
                txtName.Text = current.ConfigName;
                label7.Text = current.DefaultPrinter;
                cboPrinters.SelectedItem = current.DefaultPrinter;
                chkDirectPrint.Checked = current.DirectPrint;
                chkDefault.Checked = current.Selected;
            }
        }

        /// <summary>
        /// 重置界面控件数据源
        /// </summary>
        private void ResetForm()
        {
            txtName.Text = string.Empty;
            chkDirectPrint.Checked = chkDefault.Checked = false;
            cboPrinters.Items.Clear();
            cboConfigs.Items.Clear();
            label7.Text = string.Empty;
        }

        /// <summary>
        /// 删除当前项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Configs selected = ConfigList.FirstOrDefault(x => x.ConfigName == cboConfigs.SelectedItem.ToString());
            if (selected != null)
            {
                ConfigList.Remove(selected);
            }
            XmlHelper.SerializeToXmlFile(ConfigList);
            cc = ConfigList;
            PrintSet_Load(sender, e);
            MessageBox.Show($"删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            chkDirectPrint.Checked = chkDefault.Checked = false;
            label7.Text = string.Empty;
            cboPrinters.SelectedIndex = 0;
            InintData(ConfigList.FirstOrDefault(x => x.ConfigName == cboConfigs.SelectedItem.ToString()));
        }

        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.Owner as MainForm).SetMenuItem(true);
        }
    }
}