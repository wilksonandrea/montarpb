using Plugin.Core.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public class MainForm : Form, IMessageFilter
{
    public const int int_0 = 0xa1;
    public const int int_1 = 2;
    public const int int_2 = 0x201;
    private readonly HashSet<Control> hashSet_0 = new HashSet<Control>();
    private readonly int int_3;
    private readonly DirectoryInfo directoryInfo_0;
    private readonly Color color_0 = Color.FromArgb(20, 30, 0x36);
    private readonly Color color_1 = Color.FromArgb(0x2e, 0x33, 0x49);
    private IContainer icontainer_0;
    private VerticalProgressBar verticalProgressBar_0;
    private Label RamPercent;
    private Label label47;
    private Label label48;
    private PictureBox MonitorLogo;
    private Label MonitorName;
    private GroupBox groupBox3;
    private Button button_0;
    private GroupBox groupBox2;
    private Button button_1;
    private Button CloseBTN;
    private Label AppTitle;
    private Button button_2;
    private Button AdditionalBTN;
    private Button button_3;
    private GroupBox groupBox4;
    private Label label51;
    private Label LogFileSize;
    private Label AppStatus;
    private Timer timer_0;
    private Panel FormLoaderPNL;
    private ToolTip toolTip_0;
    private Panel AdditionalPanelN;
    private Panel NavPNL;
    private Panel MonitorPanelN;
    private Panel ConfigPanelN;
    private Panel FMonitorPanel;
    private Panel FAdditionalPanel;
    private Panel FConfigPanel;

    public MainForm(int int_4, DirectoryInfo directoryInfo_1)
    {
        this.InitializeComponent();
        this.int_3 = int_4;
        this.directoryInfo_0 = directoryInfo_1;
        Application.AddMessageFilter(this);
        this.hashSet_0.Add(this);
        this.hashSet_0.Add(this.MonitorLogo);
        this.hashSet_0.Add(this.MonitorName);
    }

    private void AdditionalBTN_Click(object sender, EventArgs e)
    {
        this.NavPNL.Width = this.FAdditionalPanel.Width;
        this.NavPNL.Top = this.FAdditionalPanel.Top;
        this.NavPNL.Left = this.FAdditionalPanel.Left;
        this.AdditionalBTN.BackColor = this.color_1;
        this.button_2.BackColor = this.color_0;
        this.button_3.BackColor = this.color_0;
        this.FormLoaderPNL.Controls.Clear();
        FormAdditional additional1 = new FormAdditional();
        additional1.Dock = DockStyle.Fill;
        additional1.TopLevel = false;
        additional1.TopMost = true;
        FormAdditional additional = additional1;
        this.FormLoaderPNL.Controls.Add(additional);
        additional.Show();
    }

    private void button_0_Click(object sender, EventArgs e)
    {
        this.Refresh();
    }

    private void button_1_Click(object sender, EventArgs e)
    {
        base.WindowState = FormWindowState.Minimized;
    }

    private void button_2_Click(object sender, EventArgs e)
    {
        this.NavPNL.Width = this.FMonitorPanel.Width;
        this.NavPNL.Top = this.FMonitorPanel.Top;
        this.NavPNL.Left = this.FMonitorPanel.Left;
        this.button_2.BackColor = this.color_1;
        this.AdditionalBTN.BackColor = this.color_0;
        this.button_3.BackColor = this.color_0;
        this.FormLoaderPNL.Controls.Clear();
        FormMonitor monitor1 = new FormMonitor();
        monitor1.Dock = DockStyle.Fill;
        monitor1.TopLevel = false;
        monitor1.TopMost = true;
        FormMonitor monitor = monitor1;
        this.FormLoaderPNL.Controls.Add(monitor);
        monitor.Show();
    }

    private void button_3_Click(object sender, EventArgs e)
    {
        this.NavPNL.Width = this.FConfigPanel.Width;
        this.NavPNL.Top = this.FConfigPanel.Top;
        this.NavPNL.Left = this.FConfigPanel.Left;
        this.button_3.BackColor = this.color_1;
        this.button_2.BackColor = this.color_0;
        this.AdditionalBTN.BackColor = this.color_0;
        this.FormLoaderPNL.Controls.Clear();
        FormConfig config1 = new FormConfig(this.directoryInfo_0);
        config1.Dock = DockStyle.Fill;
        config1.TopLevel = false;
        config1.TopMost = true;
        FormConfig config = config1;
        this.FormLoaderPNL.Controls.Add(config);
        config.Show();
    }

    private void CloseBTN_Click(object sender, EventArgs e)
    {
        base.Close();
    }

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(int int_4, int int_5, int int_6, int int_7, int int_8, int int_9);
    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        ComponentResourceManager manager = new ComponentResourceManager(typeof(MainForm));
        this.RamPercent = new Label();
        this.label47 = new Label();
        this.label48 = new Label();
        this.MonitorName = new Label();
        this.groupBox3 = new GroupBox();
        this.button_0 = new Button();
        this.groupBox2 = new GroupBox();
        this.button_1 = new Button();
        this.CloseBTN = new Button();
        this.AppTitle = new Label();
        this.button_2 = new Button();
        this.AdditionalBTN = new Button();
        this.button_3 = new Button();
        this.groupBox4 = new GroupBox();
        this.label51 = new Label();
        this.LogFileSize = new Label();
        this.AppStatus = new Label();
        this.timer_0 = new Timer(this.icontainer_0);
        this.FormLoaderPNL = new Panel();
        this.MonitorLogo = new PictureBox();
        this.toolTip_0 = new ToolTip(this.icontainer_0);
        this.AdditionalPanelN = new Panel();
        this.NavPNL = new Panel();
        this.MonitorPanelN = new Panel();
        this.ConfigPanelN = new Panel();
        this.FMonitorPanel = new Panel();
        this.FAdditionalPanel = new Panel();
        this.FConfigPanel = new Panel();
        this.verticalProgressBar_0 = new VerticalProgressBar();
        this.groupBox3.SuspendLayout();
        ((ISupportInitialize) this.MonitorLogo).BeginInit();
        base.SuspendLayout();
        this.RamPercent.BackColor = Color.Transparent;
        this.RamPercent.FlatStyle = FlatStyle.Flat;
        this.RamPercent.Font = new Font("Roboto Slab", 18f);
        this.RamPercent.ForeColor = Color.White;
        this.RamPercent.Location = new Point(0x21, 0xcd);
        this.RamPercent.Name = "RamPercent";
        this.RamPercent.Size = new Size(0x7d, 0x1f);
        this.RamPercent.TabIndex = 0x5b;
        this.RamPercent.Text = "99.9%";
        this.RamPercent.TextAlign = ContentAlignment.BottomLeft;
        this.label47.BackColor = Color.Transparent;
        this.label47.FlatStyle = FlatStyle.Flat;
        this.label47.Font = new Font("Roboto Slab", 10f);
        this.label47.ForeColor = Color.White;
        this.label47.Location = new Point(0x21, 0xb6);
        this.label47.Name = "label47";
        this.label47.Size = new Size(0x7d, 0x17);
        this.label47.TabIndex = 0x5c;
        this.label47.Text = "Memory Usage";
        this.label48.BackColor = Color.Transparent;
        this.label48.FlatStyle = FlatStyle.Flat;
        this.label48.Font = new Font("Roboto Slab SemiBold", 14f, FontStyle.Italic | FontStyle.Bold);
        this.label48.ForeColor = Color.White;
        this.label48.Location = new Point(12, 0x54);
        this.label48.Name = "label48";
        this.label48.Size = new Size(0x18a, 0x1d);
        this.label48.TabIndex = 0x5d;
        this.label48.Text = "Please Monitor Your Server";
        this.label48.TextAlign = ContentAlignment.MiddleCenter;
        this.MonitorName.BackColor = Color.Transparent;
        this.MonitorName.FlatStyle = FlatStyle.Flat;
        this.MonitorName.Font = new Font("Roboto Slab", 10f, FontStyle.Bold);
        this.MonitorName.ForeColor = Color.White;
        this.MonitorName.Location = new Point(0x38, 12);
        this.MonitorName.Name = "MonitorName";
        this.MonitorName.Size = new Size(0x95, 0x26);
        this.MonitorName.TabIndex = 0x5f;
        this.MonitorName.Text = "OSM - Monitor";
        this.MonitorName.TextAlign = ContentAlignment.MiddleLeft;
        this.groupBox3.Controls.Add(this.verticalProgressBar_0);
        this.groupBox3.FlatStyle = FlatStyle.Flat;
        this.groupBox3.Font = new Font("Roboto Slab", 6f);
        this.groupBox3.ForeColor = Color.White;
        this.groupBox3.Location = new Point(0x10, 0xb2);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new Size(11, 0x3a);
        this.groupBox3.TabIndex = 0x60;
        this.groupBox3.TabStop = false;
        this.button_0.BackColor = Color.FromArgb(0, 0x7e, 0xf9);
        this.button_0.Cursor = Cursors.Hand;
        this.button_0.FlatAppearance.BorderColor = Color.Silver;
        this.button_0.FlatAppearance.BorderSize = 0;
        this.button_0.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
        this.button_0.FlatAppearance.MouseOverBackColor = Color.Blue;
        this.button_0.FlatStyle = FlatStyle.Flat;
        this.button_0.Font = new Font("Roboto Slab", 10f);
        this.button_0.ForeColor = Color.White;
        this.button_0.Location = new Point(0x139, 0xb6);
        this.button_0.Name = "RefreshBTN";
        this.button_0.Size = new Size(0x5d, 0x36);
        this.button_0.TabIndex = 1;
        this.button_0.Text = "Refresh";
        this.button_0.UseVisualStyleBackColor = false;
        this.button_0.Click += new EventHandler(this.button_0_Click);
        this.groupBox2.FlatStyle = FlatStyle.Flat;
        this.groupBox2.Font = new Font("Roboto Slab", 6f);
        this.groupBox2.ForeColor = Color.White;
        this.groupBox2.Location = new Point(0x10, 0xa3);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new Size(390, 1);
        this.groupBox2.TabIndex = 0x61;
        this.groupBox2.TabStop = false;
        this.button_1.Cursor = Cursors.Hand;
        this.button_1.FlatAppearance.BorderColor = Color.FromArgb(20, 30, 0x36);
        this.button_1.FlatAppearance.BorderSize = 0;
        this.button_1.FlatStyle = FlatStyle.Flat;
        this.button_1.Font = new Font("Roboto Slab", 14f);
        this.button_1.ForeColor = Color.White;
        this.button_1.Location = new Point(0x148, 1);
        this.button_1.Name = "MinimizeBTN";
        this.button_1.Size = new Size(0x26, 0x30);
        this.button_1.TabIndex = 0x63;
        this.button_1.Text = "_";
        this.button_1.UseVisualStyleBackColor = true;
        this.button_1.Click += new EventHandler(this.button_1_Click);
        this.CloseBTN.Cursor = Cursors.Hand;
        this.CloseBTN.FlatAppearance.BorderColor = Color.FromArgb(20, 30, 0x36);
        this.CloseBTN.FlatAppearance.BorderSize = 0;
        this.CloseBTN.FlatAppearance.MouseDownBackColor = Color.Maroon;
        this.CloseBTN.FlatAppearance.MouseOverBackColor = Color.Red;
        this.CloseBTN.FlatStyle = FlatStyle.Flat;
        this.CloseBTN.Font = new Font("Roboto Slab", 15f);
        this.CloseBTN.ForeColor = Color.White;
        this.CloseBTN.Location = new Point(0x171, 1);
        this.CloseBTN.Name = "CloseBTN";
        this.CloseBTN.Size = new Size(0x34, 0x30);
        this.CloseBTN.TabIndex = 0x62;
        this.CloseBTN.Text = "X";
        this.CloseBTN.UseVisualStyleBackColor = true;
        this.CloseBTN.Click += new EventHandler(this.CloseBTN_Click);
        this.AppTitle.FlatStyle = FlatStyle.Flat;
        this.AppTitle.Font = new Font("Roboto Slab", 8f);
        this.AppTitle.ForeColor = Color.Silver;
        this.AppTitle.Location = new Point(14, 0x71);
        this.AppTitle.Name = "AppTitle";
        this.AppTitle.Size = new Size(0x188, 0x11);
        this.AppTitle.TabIndex = 0x55;
        this.AppTitle.Text = "Version 1.2.34567.890";
        this.AppTitle.TextAlign = ContentAlignment.MiddleCenter;
        this.button_2.BackColor = Color.FromArgb(20, 30, 0x36);
        this.button_2.Cursor = Cursors.Hand;
        this.button_2.FlatAppearance.BorderColor = Color.Silver;
        this.button_2.FlatAppearance.BorderSize = 0;
        this.button_2.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
        this.button_2.FlatAppearance.MouseOverBackColor = Color.Blue;
        this.button_2.FlatStyle = FlatStyle.Flat;
        this.button_2.Font = new Font("Roboto Slab", 9f);
        this.button_2.ForeColor = Color.White;
        this.button_2.Location = new Point(1, 0x231);
        this.button_2.Name = "MonitorBTN";
        this.button_2.Size = new Size(0x92, 0x36);
        this.button_2.TabIndex = 0x6a;
        this.button_2.Text = "Monitor";
        this.button_2.UseVisualStyleBackColor = false;
        this.button_2.Click += new EventHandler(this.button_2_Click);
        this.AdditionalBTN.BackColor = Color.FromArgb(20, 30, 0x36);
        this.AdditionalBTN.Cursor = Cursors.Hand;
        this.AdditionalBTN.FlatAppearance.BorderColor = Color.Silver;
        this.AdditionalBTN.FlatAppearance.BorderSize = 0;
        this.AdditionalBTN.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
        this.AdditionalBTN.FlatAppearance.MouseOverBackColor = Color.Blue;
        this.AdditionalBTN.FlatStyle = FlatStyle.Flat;
        this.AdditionalBTN.Font = new Font("Roboto Slab", 9f);
        this.AdditionalBTN.ForeColor = Color.White;
        this.AdditionalBTN.Location = new Point(0x92, 0x231);
        this.AdditionalBTN.Name = "AdditionalBTN";
        this.AdditionalBTN.Size = new Size(130, 0x36);
        this.AdditionalBTN.TabIndex = 0x6b;
        this.AdditionalBTN.Text = "Additional";
        this.AdditionalBTN.UseVisualStyleBackColor = false;
        this.AdditionalBTN.Click += new EventHandler(this.AdditionalBTN_Click);
        this.button_3.BackColor = Color.FromArgb(20, 30, 0x36);
        this.button_3.Cursor = Cursors.Hand;
        this.button_3.FlatAppearance.BorderColor = Color.Silver;
        this.button_3.FlatAppearance.BorderSize = 0;
        this.button_3.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
        this.button_3.FlatAppearance.MouseOverBackColor = Color.Blue;
        this.button_3.FlatStyle = FlatStyle.Flat;
        this.button_3.Font = new Font("Roboto Slab", 9f);
        this.button_3.ForeColor = Color.White;
        this.button_3.Location = new Point(0x113, 0x231);
        this.button_3.Name = "ConfigBTN";
        this.button_3.Size = new Size(0x92, 0x36);
        this.button_3.TabIndex = 0x6c;
        this.button_3.Text = "Config";
        this.button_3.UseVisualStyleBackColor = false;
        this.button_3.Click += new EventHandler(this.button_3_Click);
        this.groupBox4.FlatStyle = FlatStyle.Flat;
        this.groupBox4.Font = new Font("Roboto Slab", 6f);
        this.groupBox4.ForeColor = Color.White;
        this.groupBox4.Location = new Point(0xa7, 0xb2);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new Size(1, 0x3a);
        this.groupBox4.TabIndex = 0x6f;
        this.groupBox4.TabStop = false;
        this.label51.BackColor = Color.Transparent;
        this.label51.FlatStyle = FlatStyle.Flat;
        this.label51.Font = new Font("Roboto Slab", 10f);
        this.label51.ForeColor = Color.White;
        this.label51.Location = new Point(0xb3, 0xb6);
        this.label51.Name = "label51";
        this.label51.Size = new Size(0x7d, 0x17);
        this.label51.TabIndex = 110;
        this.label51.Text = "Log Files";
        this.LogFileSize.BackColor = Color.Transparent;
        this.LogFileSize.FlatStyle = FlatStyle.Flat;
        this.LogFileSize.Font = new Font("Roboto Slab", 18f);
        this.LogFileSize.ForeColor = Color.White;
        this.LogFileSize.Location = new Point(0xb3, 0xcd);
        this.LogFileSize.Name = "LogFileSize";
        this.LogFileSize.Size = new Size(0x7d, 0x1f);
        this.LogFileSize.TabIndex = 0x6d;
        this.LogFileSize.Text = "100MB";
        this.LogFileSize.TextAlign = ContentAlignment.BottomLeft;
        this.AppStatus.FlatStyle = FlatStyle.Flat;
        this.AppStatus.Font = new Font("Roboto Slab", 8f);
        this.AppStatus.ForeColor = Color.Silver;
        this.AppStatus.Location = new Point(14, 0x8f);
        this.AppStatus.Name = "AppStatus";
        this.AppStatus.Size = new Size(0x188, 0x11);
        this.AppStatus.TabIndex = 0x70;
        this.AppStatus.Text = "Server Status";
        this.AppStatus.TextAlign = ContentAlignment.MiddleCenter;
        this.timer_0.Interval = 0x3e8;
        this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
        this.FormLoaderPNL.BackColor = Color.FromArgb(0x2e, 0x33, 0x49);
        this.FormLoaderPNL.Location = new Point(1, 0xf2);
        this.FormLoaderPNL.Name = "FormLoaderPNL";
        this.FormLoaderPNL.Size = new Size(420, 0x139);
        this.FormLoaderPNL.TabIndex = 120;
        this.MonitorLogo.BackgroundImageLayout = ImageLayout.Stretch;
        this.MonitorLogo.Image = Class11.Bitmap_0;
        this.MonitorLogo.Location = new Point(12, 12);
        this.MonitorLogo.Name = "MonitorLogo";
        this.MonitorLogo.Size = new Size(0x26, 0x26);
        this.MonitorLogo.SizeMode = PictureBoxSizeMode.Zoom;
        this.MonitorLogo.TabIndex = 0x5e;
        this.MonitorLogo.TabStop = false;
        this.toolTip_0.ToolTipTitle = "Monitor Tips";
        this.AdditionalPanelN.BackColor = Color.FromArgb(0x2e, 0x33, 0x49);
        this.AdditionalPanelN.Location = new Point(0x92, 0x22b);
        this.AdditionalPanelN.Name = "AdditionalPanelN";
        this.AdditionalPanelN.Size = new Size(130, 6);
        this.AdditionalPanelN.TabIndex = 0x80;
        this.NavPNL.BackColor = Color.FromArgb(0, 0x7e, 0xf9);
        this.NavPNL.Location = new Point(0x92, 0x264);
        this.NavPNL.Name = "NavPNL";
        this.NavPNL.Size = new Size(130, 2);
        this.NavPNL.TabIndex = 0x81;
        this.MonitorPanelN.BackColor = Color.FromArgb(0x2e, 0x33, 0x49);
        this.MonitorPanelN.Location = new Point(1, 0x22b);
        this.MonitorPanelN.Name = "MonitorPanelN";
        this.MonitorPanelN.Size = new Size(0x92, 6);
        this.MonitorPanelN.TabIndex = 0x81;
        this.ConfigPanelN.BackColor = Color.FromArgb(0x2e, 0x33, 0x49);
        this.ConfigPanelN.Location = new Point(0x113, 0x22b);
        this.ConfigPanelN.Name = "ConfigPanelN";
        this.ConfigPanelN.Size = new Size(0x92, 6);
        this.ConfigPanelN.TabIndex = 0x81;
        this.FMonitorPanel.BackColor = Color.FromArgb(20, 30, 0x36);
        this.FMonitorPanel.Location = new Point(1, 0x268);
        this.FMonitorPanel.Name = "FMonitorPanel";
        this.FMonitorPanel.Size = new Size(0x92, 3);
        this.FMonitorPanel.TabIndex = 130;
        this.FAdditionalPanel.BackColor = Color.FromArgb(20, 30, 0x36);
        this.FAdditionalPanel.Location = new Point(0x92, 0x268);
        this.FAdditionalPanel.Name = "FAdditionalPanel";
        this.FAdditionalPanel.Size = new Size(130, 3);
        this.FAdditionalPanel.TabIndex = 0x83;
        this.FConfigPanel.BackColor = Color.FromArgb(20, 30, 0x36);
        this.FConfigPanel.Location = new Point(0x113, 0x268);
        this.FConfigPanel.Name = "FConfigPanel";
        this.FConfigPanel.Size = new Size(0x92, 3);
        this.FConfigPanel.TabIndex = 0x83;
        this.verticalProgressBar_0.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        this.verticalProgressBar_0.GEnum2_0 = GEnum2.None;
        this.verticalProgressBar_0.Color_0 = Color.DarkGray;
        this.verticalProgressBar_0.ForeColor = Color.Gray;
        this.verticalProgressBar_0.Location = new Point(3, 8);
        this.verticalProgressBar_0.Int32_0 = 0x7d0;
        this.verticalProgressBar_0.Int32_1 = 0;
        this.verticalProgressBar_0.Name = "MemoryVPB";
        this.verticalProgressBar_0.Size = new Size(5, 0x2e);
        this.verticalProgressBar_0.Int32_2 = 100;
        this.verticalProgressBar_0.GEnum1_0 = GEnum1.Solid;
        this.verticalProgressBar_0.TabIndex = 90;
        this.verticalProgressBar_0.Int32_3 = 0x3e8;
        base.AutoScaleDimensions = new SizeF(7f, 15f);
        base.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(20, 30, 0x36);
        this.BackgroundImageLayout = ImageLayout.Center;
        base.ClientSize = new Size(0x1a6, 620);
        base.Controls.Add(this.NavPNL);
        base.Controls.Add(this.ConfigPanelN);
        base.Controls.Add(this.AdditionalPanelN);
        base.Controls.Add(this.MonitorPanelN);
        base.Controls.Add(this.FConfigPanel);
        base.Controls.Add(this.FAdditionalPanel);
        base.Controls.Add(this.FMonitorPanel);
        base.Controls.Add(this.FormLoaderPNL);
        base.Controls.Add(this.AdditionalBTN);
        base.Controls.Add(this.AppStatus);
        base.Controls.Add(this.groupBox4);
        base.Controls.Add(this.label51);
        base.Controls.Add(this.LogFileSize);
        base.Controls.Add(this.button_3);
        base.Controls.Add(this.button_2);
        base.Controls.Add(this.AppTitle);
        base.Controls.Add(this.button_1);
        base.Controls.Add(this.CloseBTN);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.button_0);
        base.Controls.Add(this.groupBox3);
        base.Controls.Add(this.MonitorName);
        base.Controls.Add(this.MonitorLogo);
        base.Controls.Add(this.label48);
        base.Controls.Add(this.label47);
        base.Controls.Add(this.RamPercent);
        this.Font = new Font("Roboto Slab", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        base.FormBorderStyle = FormBorderStyle.None;
        base.Icon = (Icon) manager.GetObject("$this.Icon");
        base.Name = "MainForm";
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "OSM-Monitor";
        base.TopMost = true;
        base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
        base.Load += new EventHandler(this.MainForm_Load);
        base.Paint += new PaintEventHandler(this.MainForm_Paint);
        this.groupBox3.ResumeLayout(false);
        ((ISupportInitialize) this.MonitorLogo).EndInit();
        base.ResumeLayout(false);
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult result = MessageBox.Show("Are you sure want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            GClass11.smethod_3(this.int_3);
        }
        e.Cancel = result == DialogResult.No;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        Rectangle workingArea = Screen.GetWorkingArea(this);
        base.Location = new Point(workingArea.Right - base.Size.Width, workingArea.Bottom - base.Size.Height);
        this.FormLoaderPNL.BackColor = this.color_1;
        this.MonitorPanelN.BackColor = this.color_1;
        this.AdditionalPanelN.BackColor = this.color_1;
        this.ConfigPanelN.BackColor = this.color_1;
        this.method_0();
        using (PrivateFontCollection fonts = new PrivateFontCollection())
        {
            string[] files = Directory.GetFiles("Font/");
            if (files.Length == 0)
            {
                MessageBox.Show("The Font was not found. try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                GClass11.smethod_3(this.int_3);
            }
            else
            {
                this.method_2(files, fonts);
                string familyName = this.method_3(fonts.Families);
                foreach (Control control in this.method_1(this))
                {
                    control.Font = new Font(familyName, control.Font.Size, control.Font.Style);
                }
            }
        }
        this.FormLoaderPNL.BringToFront();
        this.NavPNL.Width = this.FMonitorPanel.Width;
        this.NavPNL.Top = this.FMonitorPanel.Top;
        this.NavPNL.Left = this.FMonitorPanel.Left;
        this.button_2.BackColor = this.color_1;
        this.AdditionalBTN.BackColor = this.color_0;
        this.button_3.BackColor = this.color_0;
        this.FormLoaderPNL.Controls.Clear();
        FormMonitor monitor1 = new FormMonitor();
        monitor1.Dock = DockStyle.Fill;
        monitor1.TopLevel = false;
        monitor1.TopMost = true;
        FormMonitor monitor = monitor1;
        this.FormLoaderPNL.Controls.Add(monitor);
        monitor.Show();
        this.timer_0.Start();
    }

    private void MainForm_Paint(object sender, PaintEventArgs e)
    {
        Rectangle clientRectangle = base.ClientRectangle;
        clientRectangle.Inflate(0, 0);
        ControlPaint.DrawBorder(e.Graphics, clientRectangle, Color.FromArgb(0xff, 0x36, 0x36, 0xa4), ButtonBorderStyle.Solid);
    }

    private void method_0()
    {
        this.AppTitle.Text = "Version ****";
        this.AppStatus.Text = "PLEASE WAIT...";
        this.verticalProgressBar_0.Int32_0 = GClass6.smethod_1();
        this.RamPercent.Text = "--";
        this.LogFileSize.Text = "--";
    }

    [IteratorStateMachine(typeof(Class14))]
    private IEnumerable<Control> method_1(Control control_0)
    {
        Class14 class1 = new Class14(-2);
        class1.control_2 = control_0;
        return class1;
    }

    private void method_2(string[] string_0, PrivateFontCollection privateFontCollection_0)
    {
        foreach (string str in string_0)
        {
            privateFontCollection_0.AddFontFile(str);
        }
    }

    private string method_3(FontFamily[] fontFamily_0)
    {
        foreach (FontFamily family in fontFamily_0)
        {
            if (family.Name == this.method_4())
            {
                return family.Name;
            }
        }
        return "Consolas";
    }

    private string method_4()
    {
        string str = "";
        try
        {
            string path = "Config/FontSet.ini";
            if (File.Exists(path))
            {
                foreach (string str4 in File.ReadAllLines(path, Encoding.UTF8))
                {
                    if (!str4.StartsWith(";") && !str4.StartsWith("["))
                    {
                        str = str4;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("File Not Found! " + path, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return str;
            }
        }
        catch (Exception exception1)
        {
            MessageBox.Show(exception1.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        return str;
    }

    public bool PreFilterMessage(ref Message Msg)
    {
        if ((Msg.Msg != 0x201) || !this.hashSet_0.Contains(FromHandle(Msg.HWnd)))
        {
            return false;
        }
        ReleaseCapture();
        SendMessage(base.Handle, 0xa1, 2, 0);
        return true;
    }

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr intptr_0, int int_4, int int_5, int int_6);
    override void Form.Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }

    private void timer_0_Tick(object sender, EventArgs e)
    {
        this.AppTitle.Text = "Version " + GClass9.string_0;
        this.AppStatus.ForeColor = GClass9.string_1.Equals("SERVER ONLINE") ? ColorUtil.Green : (GClass9.string_1.Equals("SERVER OFFLINE") ? ColorUtil.Red : ColorUtil.White);
        this.AppStatus.Text = GClass9.string_1;
        this.verticalProgressBar_0.Int32_3 = int.Parse(GClass9.string_2);
        this.RamPercent.Text = GClass9.string_3;
        this.LogFileSize.Text = GClass9.string_4;
    }

    [CompilerGenerated]
    private sealed class Class14 : IEnumerable<Control>, IEnumerable, IEnumerator<Control>, IDisposable, IEnumerator
    {
        private int int_0;
        private Control control_0;
        private int int_1;
        private Control control_1;
        public Control control_2;
        private Stack<Control> stack_0;

        [DebuggerHidden]
        public Class14(int int_2)
        {
            this.int_0 = int_2;
            this.int_1 = Environment.CurrentManagedThreadId;
        }

        private bool MoveNext()
        {
            int num = this.int_0;
            if (num == 0)
            {
                this.int_0 = -1;
                this.stack_0 = new Stack<Control>();
                this.stack_0.Push(this.control_1);
            }
            else
            {
                if (num != 1)
                {
                    return false;
                }
                this.int_0 = -1;
            }
            if (!this.stack_0.Any<Control>())
            {
                return false;
            }
            Control control = this.stack_0.Pop();
            foreach (Control control2 in control.Controls)
            {
                this.stack_0.Push(control2);
            }
            this.control_0 = control;
            this.int_0 = 1;
            return true;
        }

        [DebuggerHidden]
        IEnumerator<Control> IEnumerable<Control>.GetEnumerator()
        {
            MainForm.Class14 class2;
            if ((this.int_0 != -2) || (this.int_1 != Environment.CurrentManagedThreadId))
            {
                class2 = new MainForm.Class14(0);
            }
            else
            {
                this.int_0 = 0;
                class2 = this;
            }
            class2.control_1 = this.control_2;
            return class2;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<System.Windows.Forms.Control>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
            this.stack_0 = null;
            this.int_0 = -2;
        }

        Control IEnumerator<Control>.Current =>
            this.control_0;

        object IEnumerator.Current =>
            this.control_0;
    }
}

