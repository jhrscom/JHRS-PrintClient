
namespace JHRS.PrintClient
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.previewControl1 = new FastReport.Preview.PreviewControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打印设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.打印日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出打印客户端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.report1 = new FastReport.Report();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).BeginInit();
            this.SuspendLayout();
            // 
            // previewControl1
            // 
            this.previewControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl1.Font = new System.Drawing.Font("宋体", 9F);
            this.previewControl1.Location = new System.Drawing.Point(0, 0);
            this.previewControl1.Name = "previewControl1";
            this.previewControl1.PageOffset = new System.Drawing.Point(10, 10);
            this.previewControl1.Size = new System.Drawing.Size(968, 542);
            this.previewControl1.TabIndex = 1;
            this.previewControl1.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打印设置ToolStripMenuItem,
            this.默认设置ToolStripMenuItem,
            this.打印日志ToolStripMenuItem,
            this.退出打印客户端ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 92);
            // 
            // 打印设置ToolStripMenuItem
            // 
            this.打印设置ToolStripMenuItem.Name = "打印设置ToolStripMenuItem";
            this.打印设置ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打印设置ToolStripMenuItem.Text = "打印设置";
            this.打印设置ToolStripMenuItem.Click += new System.EventHandler(this.打印设置ToolStripMenuItem_Click);
            // 
            // 默认设置ToolStripMenuItem
            // 
            this.默认设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.默认设置ToolStripMenuItem.Name = "默认设置ToolStripMenuItem";
            this.默认设置ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.默认设置ToolStripMenuItem.Text = "默认设置";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // 打印日志ToolStripMenuItem
            // 
            this.打印日志ToolStripMenuItem.Name = "打印日志ToolStripMenuItem";
            this.打印日志ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打印日志ToolStripMenuItem.Text = "打印日志";
            this.打印日志ToolStripMenuItem.Click += new System.EventHandler(this.打印日志ToolStripMenuItem_Click);
            // 
            // 退出打印客户端ToolStripMenuItem
            // 
            this.退出打印客户端ToolStripMenuItem.Name = "退出打印客户端ToolStripMenuItem";
            this.退出打印客户端ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.退出打印客户端ToolStripMenuItem.Text = "退出打印客户端";
            this.退出打印客户端ToolStripMenuItem.Click += new System.EventHandler(this.退出打印客户端ToolStripMenuItem_Click);
            // 
            // report1
            // 
            this.report1.ReportResourceString = resources.GetString("report1.ReportResourceString");
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 542);
            this.Controls.Add(this.previewControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web打印客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.report1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl previewControl1;
        private FastReport.Report report1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打印设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出打印客户端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 默认设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem 打印日志ToolStripMenuItem;
    }
}

