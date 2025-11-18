// Decompiled with JetBrains decompiler
// Type: MainForm
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
public class MainForm : Form, IMessageFilter
{
  public const int int_0 = 161;
  public const int int_1 = 2;
  public const int int_2 = 513;
  private readonly HashSet<Control> hashSet_0 = new HashSet<Control>();
  private readonly int int_3;
  private readonly DirectoryInfo directoryInfo_0;
  private readonly Color color_0 = Color.FromArgb(20, 30, 54);
  private readonly Color color_1 = Color.FromArgb(46, 51, 73);
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

  [DllImport("user32.dll")]
  public static extern int SendMessage(IntPtr intptr_0, int int_4, int int_5, int int_6);

  [DllImport("user32.dll")]
  public static extern bool ReleaseCapture();

  [DllImport("Gdi32.dll")]
  private static extern IntPtr CreateRoundRectRgn(
    int int_4,
    int int_5,
    int int_6,
    int int_7,
    int int_8,
    int int_9);

  public MainForm(int int_4, DirectoryInfo directoryInfo_1)
  {
    this.InitializeComponent();
    this.int_3 = int_4;
    this.directoryInfo_0 = directoryInfo_1;
    Application.AddMessageFilter((IMessageFilter) this);
    this.hashSet_0.Add((Control) this);
    this.hashSet_0.Add((Control) this.MonitorLogo);
    this.hashSet_0.Add((Control) this.MonitorName);
  }

  private void method_0()
  {
    this.AppTitle.Text = "Version ****";
    this.AppStatus.Text = "PLEASE WAIT...";
    this.verticalProgressBar_0.Int32_0 = GClass6.smethod_1();
    this.RamPercent.Text = "--";
    this.LogFileSize.Text = "--";
  }

  public bool PreFilterMessage(ref Message Msg)
  {
    if (Msg.Msg != 513 || !this.hashSet_0.Contains(Control.FromHandle(Msg.HWnd)))
      return false;
    MainForm.ReleaseCapture();
    MainForm.SendMessage(this.Handle, 161, 2, 0);
    return true;
  }

  private IEnumerable<Control> method_1(Control control_0)
  {
    Stack<Control> source = new Stack<Control>();
    source.Push(control_0);
    while (source.Any<Control>())
    {
      Control control1 = source.Pop();
      foreach (Control control2 in (ArrangedElementCollection) control1.Controls)
        source.Push(control2);
      yield return control1;
    }
  }

  private void method_2(string[] string_0, PrivateFontCollection privateFontCollection_0)
  {
    foreach (string filename in string_0)
      privateFontCollection_0.AddFontFile(filename);
  }

  private string method_3(FontFamily[] fontFamily_0)
  {
    foreach (FontFamily fontFamily in fontFamily_0)
    {
      if (fontFamily.Name == this.method_4())
        return fontFamily.Name;
    }
    return "Consolas";
  }

  private string method_4()
  {
    string str = "";
    try
    {
      string path = "Config/FontSet.ini";
      if (!File.Exists(path))
      {
        int num = (int) MessageBox.Show("File Not Found! " + path, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return str;
      }
      foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
      {
        if (!readAllLine.StartsWith(";") && !readAllLine.StartsWith("["))
        {
          str = readAllLine;
          break;
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    return str;
  }

  private void MainForm_Load(object sender, EventArgs e)
  {
    Rectangle workingArea = Screen.GetWorkingArea((Control) this);
    this.Location = new Point(workingArea.Right - this.Size.Width, workingArea.Bottom - this.Size.Height);
    this.FormLoaderPNL.BackColor = this.color_1;
    this.MonitorPanelN.BackColor = this.color_1;
    this.AdditionalPanelN.BackColor = this.color_1;
    this.ConfigPanelN.BackColor = this.color_1;
    this.method_0();
    using (PrivateFontCollection privateFontCollection_0 = new PrivateFontCollection())
    {
      string[] files = Directory.GetFiles("Font/");
      if (files.Length != 0)
      {
        this.method_2(files, privateFontCollection_0);
        string familyName = this.method_3(privateFontCollection_0.Families);
        foreach (Control control in this.method_1((Control) this))
          control.Font = new Font(familyName, control.Font.Size, control.Font.Style);
      }
      else
      {
        int num = (int) MessageBox.Show("The Font was not found. try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        GClass11.smethod_3(this.int_3);
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
    FormMonitor formMonitor1 = new FormMonitor();
    formMonitor1.Dock = DockStyle.Fill;
    formMonitor1.TopLevel = false;
    formMonitor1.TopMost = true;
    FormMonitor formMonitor2 = formMonitor1;
    this.FormLoaderPNL.Controls.Add((Control) formMonitor2);
    formMonitor2.Show();
    this.timer_0.Start();
  }

  private void button_0_Click(object sender, EventArgs e) => this.Refresh();

  private void CloseBTN_Click(object sender, EventArgs e) => this.Close();

  private void button_1_Click(object sender, EventArgs e)
  {
    this.WindowState = FormWindowState.Minimized;
  }

  private void MainForm_Paint(object sender, PaintEventArgs e)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    clientRectangle.Inflate(0, 0);
    ControlPaint.DrawBorder(e.Graphics, clientRectangle, Color.FromArgb((int) byte.MaxValue, 54, 54, 164), ButtonBorderStyle.Solid);
  }

  private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    DialogResult dialogResult = MessageBox.Show("Are you sure want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    if (dialogResult == DialogResult.Yes)
      GClass11.smethod_3(this.int_3);
    e.Cancel = dialogResult == DialogResult.No;
  }

  private void timer_0_Tick(object sender, EventArgs e)
  {
    this.AppTitle.Text = "Version " + GClass9.string_0;
    Label appStatus = this.AppStatus;
    Color color;
    switch (GClass9.string_1)
    {
      case "SERVER ONLINE":
        color = ColorUtil.Green;
        break;
      case "SERVER OFFLINE":
        color = ColorUtil.Red;
        break;
      default:
        color = ColorUtil.White;
        break;
    }
    appStatus.ForeColor = color;
    this.AppStatus.Text = GClass9.string_1;
    this.verticalProgressBar_0.Int32_3 = int.Parse(GClass9.string_2);
    this.RamPercent.Text = GClass9.string_3;
    this.LogFileSize.Text = GClass9.string_4;
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
    FormMonitor formMonitor1 = new FormMonitor();
    formMonitor1.Dock = DockStyle.Fill;
    formMonitor1.TopLevel = false;
    formMonitor1.TopMost = true;
    FormMonitor formMonitor2 = formMonitor1;
    this.FormLoaderPNL.Controls.Add((Control) formMonitor2);
    formMonitor2.Show();
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
    FormAdditional formAdditional1 = new FormAdditional();
    formAdditional1.Dock = DockStyle.Fill;
    formAdditional1.TopLevel = false;
    formAdditional1.TopMost = true;
    FormAdditional formAdditional2 = formAdditional1;
    this.FormLoaderPNL.Controls.Add((Control) formAdditional2);
    formAdditional2.Show();
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
    FormConfig formConfig1 = new FormConfig(this.directoryInfo_0);
    formConfig1.Dock = DockStyle.Fill;
    formConfig1.TopLevel = false;
    formConfig1.TopMost = true;
    FormConfig formConfig2 = formConfig1;
    this.FormLoaderPNL.Controls.Add((Control) formConfig2);
    formConfig2.Show();
  }

  virtual void Form.Dispose(bool disposing)
  {
    if (disposing && this.icontainer_0 != null)
      this.icontainer_0.Dispose();
    // ISSUE: explicit non-virtual call
    __nonvirtual (((Form) this).Dispose(disposing));
  }

  private void InitializeComponent()
  {
    this.icontainer_0 = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
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
    this.SuspendLayout();
    this.RamPercent.BackColor = Color.Transparent;
    this.RamPercent.FlatStyle = FlatStyle.Flat;
    this.RamPercent.Font = new Font("Roboto Slab", 18f);
    this.RamPercent.ForeColor = Color.White;
    this.RamPercent.Location = new Point(33, 205);
    this.RamPercent.Name = "RamPercent";
    this.RamPercent.Size = new Size(125, 31 /*0x1F*/);
    this.RamPercent.TabIndex = 91;
    this.RamPercent.Text = "99.9%";
    this.RamPercent.TextAlign = ContentAlignment.BottomLeft;
    this.label47.BackColor = Color.Transparent;
    this.label47.FlatStyle = FlatStyle.Flat;
    this.label47.Font = new Font("Roboto Slab", 10f);
    this.label47.ForeColor = Color.White;
    this.label47.Location = new Point(33, 182);
    this.label47.Name = "label47";
    this.label47.Size = new Size(125, 23);
    this.label47.TabIndex = 92;
    this.label47.Text = "Memory Usage";
    this.label48.BackColor = Color.Transparent;
    this.label48.FlatStyle = FlatStyle.Flat;
    this.label48.Font = new Font("Roboto Slab SemiBold", 14f, FontStyle.Bold | FontStyle.Italic);
    this.label48.ForeColor = Color.White;
    this.label48.Location = new Point(12, 84);
    this.label48.Name = "label48";
    this.label48.Size = new Size(394, 29);
    this.label48.TabIndex = 93;
    this.label48.Text = "Please Monitor Your Server";
    this.label48.TextAlign = ContentAlignment.MiddleCenter;
    this.MonitorName.BackColor = Color.Transparent;
    this.MonitorName.FlatStyle = FlatStyle.Flat;
    this.MonitorName.Font = new Font("Roboto Slab", 10f, FontStyle.Bold);
    this.MonitorName.ForeColor = Color.White;
    this.MonitorName.Location = new Point(56, 12);
    this.MonitorName.Name = "MonitorName";
    this.MonitorName.Size = new Size(149, 38);
    this.MonitorName.TabIndex = 95;
    this.MonitorName.Text = "OSM - Monitor";
    this.MonitorName.TextAlign = ContentAlignment.MiddleLeft;
    this.groupBox3.Controls.Add((Control) this.verticalProgressBar_0);
    this.groupBox3.FlatStyle = FlatStyle.Flat;
    this.groupBox3.Font = new Font("Roboto Slab", 6f);
    this.groupBox3.ForeColor = Color.White;
    this.groupBox3.Location = new Point(16 /*0x10*/, 178);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(11, 58);
    this.groupBox3.TabIndex = 96 /*0x60*/;
    this.groupBox3.TabStop = false;
    this.button_0.BackColor = Color.FromArgb(0, 126, 249);
    this.button_0.Cursor = Cursors.Hand;
    this.button_0.FlatAppearance.BorderColor = Color.Silver;
    this.button_0.FlatAppearance.BorderSize = 0;
    this.button_0.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
    this.button_0.FlatAppearance.MouseOverBackColor = Color.Blue;
    this.button_0.FlatStyle = FlatStyle.Flat;
    this.button_0.Font = new Font("Roboto Slab", 10f);
    this.button_0.ForeColor = Color.White;
    this.button_0.Location = new Point(313, 182);
    this.button_0.Name = "RefreshBTN";
    this.button_0.Size = new Size(93, 54);
    this.button_0.TabIndex = 1;
    this.button_0.Text = "Refresh";
    this.button_0.UseVisualStyleBackColor = false;
    this.button_0.Click += new EventHandler(this.button_0_Click);
    this.groupBox2.FlatStyle = FlatStyle.Flat;
    this.groupBox2.Font = new Font("Roboto Slab", 6f);
    this.groupBox2.ForeColor = Color.White;
    this.groupBox2.Location = new Point(16 /*0x10*/, 163);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(390, 1);
    this.groupBox2.TabIndex = 97;
    this.groupBox2.TabStop = false;
    this.button_1.Cursor = Cursors.Hand;
    this.button_1.FlatAppearance.BorderColor = Color.FromArgb(20, 30, 54);
    this.button_1.FlatAppearance.BorderSize = 0;
    this.button_1.FlatStyle = FlatStyle.Flat;
    this.button_1.Font = new Font("Roboto Slab", 14f);
    this.button_1.ForeColor = Color.White;
    this.button_1.Location = new Point(328, 1);
    this.button_1.Name = "MinimizeBTN";
    this.button_1.Size = new Size(38, 48 /*0x30*/);
    this.button_1.TabIndex = 99;
    this.button_1.Text = "_";
    this.button_1.UseVisualStyleBackColor = true;
    this.button_1.Click += new EventHandler(this.button_1_Click);
    this.CloseBTN.Cursor = Cursors.Hand;
    this.CloseBTN.FlatAppearance.BorderColor = Color.FromArgb(20, 30, 54);
    this.CloseBTN.FlatAppearance.BorderSize = 0;
    this.CloseBTN.FlatAppearance.MouseDownBackColor = Color.Maroon;
    this.CloseBTN.FlatAppearance.MouseOverBackColor = Color.Red;
    this.CloseBTN.FlatStyle = FlatStyle.Flat;
    this.CloseBTN.Font = new Font("Roboto Slab", 15f);
    this.CloseBTN.ForeColor = Color.White;
    this.CloseBTN.Location = new Point(369, 1);
    this.CloseBTN.Name = "CloseBTN";
    this.CloseBTN.Size = new Size(52, 48 /*0x30*/);
    this.CloseBTN.TabIndex = 98;
    this.CloseBTN.Text = "X";
    this.CloseBTN.UseVisualStyleBackColor = true;
    this.CloseBTN.Click += new EventHandler(this.CloseBTN_Click);
    this.AppTitle.FlatStyle = FlatStyle.Flat;
    this.AppTitle.Font = new Font("Roboto Slab", 8f);
    this.AppTitle.ForeColor = Color.Silver;
    this.AppTitle.Location = new Point(14, 113);
    this.AppTitle.Name = "AppTitle";
    this.AppTitle.Size = new Size(392, 17);
    this.AppTitle.TabIndex = 85;
    this.AppTitle.Text = "Version 1.2.34567.890";
    this.AppTitle.TextAlign = ContentAlignment.MiddleCenter;
    this.button_2.BackColor = Color.FromArgb(20, 30, 54);
    this.button_2.Cursor = Cursors.Hand;
    this.button_2.FlatAppearance.BorderColor = Color.Silver;
    this.button_2.FlatAppearance.BorderSize = 0;
    this.button_2.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
    this.button_2.FlatAppearance.MouseOverBackColor = Color.Blue;
    this.button_2.FlatStyle = FlatStyle.Flat;
    this.button_2.Font = new Font("Roboto Slab", 9f);
    this.button_2.ForeColor = Color.White;
    this.button_2.Location = new Point(1, 561);
    this.button_2.Name = "MonitorBTN";
    this.button_2.Size = new Size(146, 54);
    this.button_2.TabIndex = 106;
    this.button_2.Text = "Monitor";
    this.button_2.UseVisualStyleBackColor = false;
    this.button_2.Click += new EventHandler(this.button_2_Click);
    this.AdditionalBTN.BackColor = Color.FromArgb(20, 30, 54);
    this.AdditionalBTN.Cursor = Cursors.Hand;
    this.AdditionalBTN.FlatAppearance.BorderColor = Color.Silver;
    this.AdditionalBTN.FlatAppearance.BorderSize = 0;
    this.AdditionalBTN.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
    this.AdditionalBTN.FlatAppearance.MouseOverBackColor = Color.Blue;
    this.AdditionalBTN.FlatStyle = FlatStyle.Flat;
    this.AdditionalBTN.Font = new Font("Roboto Slab", 9f);
    this.AdditionalBTN.ForeColor = Color.White;
    this.AdditionalBTN.Location = new Point(146, 561);
    this.AdditionalBTN.Name = "AdditionalBTN";
    this.AdditionalBTN.Size = new Size(130, 54);
    this.AdditionalBTN.TabIndex = 107;
    this.AdditionalBTN.Text = "Additional";
    this.AdditionalBTN.UseVisualStyleBackColor = false;
    this.AdditionalBTN.Click += new EventHandler(this.AdditionalBTN_Click);
    this.button_3.BackColor = Color.FromArgb(20, 30, 54);
    this.button_3.Cursor = Cursors.Hand;
    this.button_3.FlatAppearance.BorderColor = Color.Silver;
    this.button_3.FlatAppearance.BorderSize = 0;
    this.button_3.FlatAppearance.MouseDownBackColor = Color.DarkBlue;
    this.button_3.FlatAppearance.MouseOverBackColor = Color.Blue;
    this.button_3.FlatStyle = FlatStyle.Flat;
    this.button_3.Font = new Font("Roboto Slab", 9f);
    this.button_3.ForeColor = Color.White;
    this.button_3.Location = new Point(275, 561);
    this.button_3.Name = "ConfigBTN";
    this.button_3.Size = new Size(146, 54);
    this.button_3.TabIndex = 108;
    this.button_3.Text = "Config";
    this.button_3.UseVisualStyleBackColor = false;
    this.button_3.Click += new EventHandler(this.button_3_Click);
    this.groupBox4.FlatStyle = FlatStyle.Flat;
    this.groupBox4.Font = new Font("Roboto Slab", 6f);
    this.groupBox4.ForeColor = Color.White;
    this.groupBox4.Location = new Point(167, 178);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.Size = new Size(1, 58);
    this.groupBox4.TabIndex = 111;
    this.groupBox4.TabStop = false;
    this.label51.BackColor = Color.Transparent;
    this.label51.FlatStyle = FlatStyle.Flat;
    this.label51.Font = new Font("Roboto Slab", 10f);
    this.label51.ForeColor = Color.White;
    this.label51.Location = new Point(179, 182);
    this.label51.Name = "label51";
    this.label51.Size = new Size(125, 23);
    this.label51.TabIndex = 110;
    this.label51.Text = "Log Files";
    this.LogFileSize.BackColor = Color.Transparent;
    this.LogFileSize.FlatStyle = FlatStyle.Flat;
    this.LogFileSize.Font = new Font("Roboto Slab", 18f);
    this.LogFileSize.ForeColor = Color.White;
    this.LogFileSize.Location = new Point(179, 205);
    this.LogFileSize.Name = "LogFileSize";
    this.LogFileSize.Size = new Size(125, 31 /*0x1F*/);
    this.LogFileSize.TabIndex = 109;
    this.LogFileSize.Text = "100MB";
    this.LogFileSize.TextAlign = ContentAlignment.BottomLeft;
    this.AppStatus.FlatStyle = FlatStyle.Flat;
    this.AppStatus.Font = new Font("Roboto Slab", 8f);
    this.AppStatus.ForeColor = Color.Silver;
    this.AppStatus.Location = new Point(14, 143);
    this.AppStatus.Name = "AppStatus";
    this.AppStatus.Size = new Size(392, 17);
    this.AppStatus.TabIndex = 112 /*0x70*/;
    this.AppStatus.Text = "Server Status";
    this.AppStatus.TextAlign = ContentAlignment.MiddleCenter;
    this.timer_0.Interval = 1000;
    this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
    this.FormLoaderPNL.BackColor = Color.FromArgb(46, 51, 73);
    this.FormLoaderPNL.Location = new Point(1, 242);
    this.FormLoaderPNL.Name = "FormLoaderPNL";
    this.FormLoaderPNL.Size = new Size(420, 313);
    this.FormLoaderPNL.TabIndex = 120;
    this.MonitorLogo.BackgroundImageLayout = ImageLayout.Stretch;
    this.MonitorLogo.Image = (Image) Class11.Bitmap_0;
    this.MonitorLogo.Location = new Point(12, 12);
    this.MonitorLogo.Name = "MonitorLogo";
    this.MonitorLogo.Size = new Size(38, 38);
    this.MonitorLogo.SizeMode = PictureBoxSizeMode.Zoom;
    this.MonitorLogo.TabIndex = 94;
    this.MonitorLogo.TabStop = false;
    this.toolTip_0.ToolTipTitle = "Monitor Tips";
    this.AdditionalPanelN.BackColor = Color.FromArgb(46, 51, 73);
    this.AdditionalPanelN.Location = new Point(146, 555);
    this.AdditionalPanelN.Name = "AdditionalPanelN";
    this.AdditionalPanelN.Size = new Size(130, 6);
    this.AdditionalPanelN.TabIndex = 128 /*0x80*/;
    this.NavPNL.BackColor = Color.FromArgb(0, 126, 249);
    this.NavPNL.Location = new Point(146, 612);
    this.NavPNL.Name = "NavPNL";
    this.NavPNL.Size = new Size(130, 2);
    this.NavPNL.TabIndex = 129;
    this.MonitorPanelN.BackColor = Color.FromArgb(46, 51, 73);
    this.MonitorPanelN.Location = new Point(1, 555);
    this.MonitorPanelN.Name = "MonitorPanelN";
    this.MonitorPanelN.Size = new Size(146, 6);
    this.MonitorPanelN.TabIndex = 129;
    this.ConfigPanelN.BackColor = Color.FromArgb(46, 51, 73);
    this.ConfigPanelN.Location = new Point(275, 555);
    this.ConfigPanelN.Name = "ConfigPanelN";
    this.ConfigPanelN.Size = new Size(146, 6);
    this.ConfigPanelN.TabIndex = 129;
    this.FMonitorPanel.BackColor = Color.FromArgb(20, 30, 54);
    this.FMonitorPanel.Location = new Point(1, 616);
    this.FMonitorPanel.Name = "FMonitorPanel";
    this.FMonitorPanel.Size = new Size(146, 3);
    this.FMonitorPanel.TabIndex = 130;
    this.FAdditionalPanel.BackColor = Color.FromArgb(20, 30, 54);
    this.FAdditionalPanel.Location = new Point(146, 616);
    this.FAdditionalPanel.Name = "FAdditionalPanel";
    this.FAdditionalPanel.Size = new Size(130, 3);
    this.FAdditionalPanel.TabIndex = 131;
    this.FConfigPanel.BackColor = Color.FromArgb(20, 30, 54);
    this.FConfigPanel.Location = new Point(275, 616);
    this.FConfigPanel.Name = "FConfigPanel";
    this.FConfigPanel.Size = new Size(146, 3);
    this.FConfigPanel.TabIndex = 131;
    this.verticalProgressBar_0.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.verticalProgressBar_0.GEnum2_0 = GEnum2.None;
    this.verticalProgressBar_0.Color_0 = Color.DarkGray;
    this.verticalProgressBar_0.ForeColor = Color.Gray;
    this.verticalProgressBar_0.Location = new Point(3, 8);
    this.verticalProgressBar_0.Int32_0 = 2000;
    this.verticalProgressBar_0.Int32_1 = 0;
    this.verticalProgressBar_0.Name = "MemoryVPB";
    this.verticalProgressBar_0.Size = new Size(5, 46);
    this.verticalProgressBar_0.Int32_2 = 100;
    this.verticalProgressBar_0.GEnum1_0 = GEnum1.Solid;
    this.verticalProgressBar_0.TabIndex = 90;
    this.verticalProgressBar_0.Int32_3 = 1000;
    this.AutoScaleDimensions = new SizeF(7f, 15f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.FromArgb(20, 30, 54);
    this.BackgroundImageLayout = ImageLayout.Center;
    this.ClientSize = new Size(422, 620);
    this.Controls.Add((Control) this.NavPNL);
    this.Controls.Add((Control) this.ConfigPanelN);
    this.Controls.Add((Control) this.AdditionalPanelN);
    this.Controls.Add((Control) this.MonitorPanelN);
    this.Controls.Add((Control) this.FConfigPanel);
    this.Controls.Add((Control) this.FAdditionalPanel);
    this.Controls.Add((Control) this.FMonitorPanel);
    this.Controls.Add((Control) this.FormLoaderPNL);
    this.Controls.Add((Control) this.AdditionalBTN);
    this.Controls.Add((Control) this.AppStatus);
    this.Controls.Add((Control) this.groupBox4);
    this.Controls.Add((Control) this.label51);
    this.Controls.Add((Control) this.LogFileSize);
    this.Controls.Add((Control) this.button_3);
    this.Controls.Add((Control) this.button_2);
    this.Controls.Add((Control) this.AppTitle);
    this.Controls.Add((Control) this.button_1);
    this.Controls.Add((Control) this.CloseBTN);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.button_0);
    this.Controls.Add((Control) this.groupBox3);
    this.Controls.Add((Control) this.MonitorName);
    this.Controls.Add((Control) this.MonitorLogo);
    this.Controls.Add((Control) this.label48);
    this.Controls.Add((Control) this.label47);
    this.Controls.Add((Control) this.RamPercent);
    this.Font = new Font("Roboto Slab", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = nameof (MainForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "OSM-Monitor";
    this.TopMost = true;
    this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
    this.Load += new EventHandler(this.MainForm_Load);
    this.Paint += new PaintEventHandler(this.MainForm_Paint);
    this.groupBox3.ResumeLayout(false);
    ((ISupportInitialize) this.MonitorLogo).EndInit();
    this.ResumeLayout(false);
  }
}
