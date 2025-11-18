// Decompiled with JetBrains decompiler
// Type: FormAdditional
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core.Utility;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
public class FormAdditional : Form
{
  private IContainer icontainer_0;
  private GroupBox groupBox1;
  private Timer timer_0;
  private Label ServerRegion;
  private Label label118;
  private Label label28;
  private Label label112;
  private Label Label116;
  private Label label117;
  private Label ConfigIndex;
  private Label RunTimeline;
  private Label AutoBan;
  private Label label22;
  private Label RuleIndex;
  private Label label114;
  private Label label119;
  private Label label20;
  private Label InternetCafe;
  private Label label26;
  private Label AutoAccount;
  private Label label111;
  private Label label44;
  private Label label115;
  private Label label23;
  private Label label113;
  private Label label17;
  private Label label43;
  private Label LocalAddress;
  private Label ServerVersion;
  private Label label25;

  public FormAdditional() => this.InitializeComponent();

  private void FormAdditional_Load(object sender, EventArgs e)
  {
    float float_0 = float.Parse("10") / 100f;
    this.BackgroundImage = (Image) GClass5.smethod_0().method_0((Image) Class11.Bitmap_0, float_0);
    this.BackgroundImageLayout = ImageLayout.Center;
    this.ServerVersion.Text = "Loading...";
    this.ServerRegion.Text = "Loading...";
    this.LocalAddress.Text = "Loading...";
    this.RunTimeline.Text = "Loading...";
    this.ConfigIndex.Text = "Loading...";
    this.RuleIndex.Text = "Loading...";
    this.InternetCafe.Text = "Loading...";
    this.AutoAccount.Text = "Loading...";
    this.AutoBan.Text = "Loading...";
    this.timer_0.Start();
  }

  private void timer_0_Tick(object sender, EventArgs e)
  {
    this.ServerVersion.Text = GClass9.string_14;
    this.ServerRegion.Text = GClass9.string_15;
    this.LocalAddress.Text = GClass9.string_16;
    this.RunTimeline.Text = GClass9.string_17;
    this.ConfigIndex.Text = GClass9.string_18;
    this.RuleIndex.Text = GClass9.string_19;
    Label ruleIndex = this.RuleIndex;
    Color color1;
    switch (GClass9.string_19)
    {
      case "Enabled":
        color1 = ColorUtil.Green;
        break;
      case "Disabled":
        color1 = ColorUtil.Yellow;
        break;
      default:
        color1 = ColorUtil.Silver;
        break;
    }
    ruleIndex.ForeColor = color1;
    this.InternetCafe.Text = GClass9.string_20;
    Label internetCafe = this.InternetCafe;
    Color color2;
    switch (GClass9.string_20)
    {
      case "Enabled":
        color2 = ColorUtil.Green;
        break;
      case "Disabled":
        color2 = ColorUtil.Yellow;
        break;
      default:
        color2 = ColorUtil.Silver;
        break;
    }
    internetCafe.ForeColor = color2;
    this.AutoAccount.Text = GClass9.string_21;
    Label autoAccount = this.AutoAccount;
    Color color3;
    switch (GClass9.string_21)
    {
      case "Enabled":
        color3 = ColorUtil.Green;
        break;
      case "Disabled":
        color3 = ColorUtil.Yellow;
        break;
      default:
        color3 = ColorUtil.Silver;
        break;
    }
    autoAccount.ForeColor = color3;
    this.AutoBan.Text = GClass9.string_22;
    Label autoBan = this.AutoBan;
    Color color4;
    switch (GClass9.string_22)
    {
      case "Enabled":
        color4 = ColorUtil.Green;
        break;
      case "Disabled":
        color4 = ColorUtil.Yellow;
        break;
      default:
        color4 = ColorUtil.Silver;
        break;
    }
    autoBan.ForeColor = color4;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormAdditional));
    this.groupBox1 = new GroupBox();
    this.ServerRegion = new Label();
    this.label118 = new Label();
    this.label28 = new Label();
    this.label112 = new Label();
    this.Label116 = new Label();
    this.label117 = new Label();
    this.ConfigIndex = new Label();
    this.RunTimeline = new Label();
    this.AutoBan = new Label();
    this.label22 = new Label();
    this.RuleIndex = new Label();
    this.label114 = new Label();
    this.label119 = new Label();
    this.label20 = new Label();
    this.InternetCafe = new Label();
    this.label26 = new Label();
    this.AutoAccount = new Label();
    this.label111 = new Label();
    this.label44 = new Label();
    this.label115 = new Label();
    this.label23 = new Label();
    this.label113 = new Label();
    this.label17 = new Label();
    this.label43 = new Label();
    this.LocalAddress = new Label();
    this.ServerVersion = new Label();
    this.label25 = new Label();
    this.timer_0 = new Timer(this.icontainer_0);
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.BackColor = Color.Transparent;
    this.groupBox1.Controls.Add((Control) this.ServerRegion);
    this.groupBox1.Controls.Add((Control) this.label118);
    this.groupBox1.Controls.Add((Control) this.label28);
    this.groupBox1.Controls.Add((Control) this.label112);
    this.groupBox1.Controls.Add((Control) this.Label116);
    this.groupBox1.Controls.Add((Control) this.label117);
    this.groupBox1.Controls.Add((Control) this.ConfigIndex);
    this.groupBox1.Controls.Add((Control) this.RunTimeline);
    this.groupBox1.Controls.Add((Control) this.AutoBan);
    this.groupBox1.Controls.Add((Control) this.label22);
    this.groupBox1.Controls.Add((Control) this.RuleIndex);
    this.groupBox1.Controls.Add((Control) this.label114);
    this.groupBox1.Controls.Add((Control) this.label119);
    this.groupBox1.Controls.Add((Control) this.label20);
    this.groupBox1.Controls.Add((Control) this.InternetCafe);
    this.groupBox1.Controls.Add((Control) this.label26);
    this.groupBox1.Controls.Add((Control) this.AutoAccount);
    this.groupBox1.Controls.Add((Control) this.label111);
    this.groupBox1.Controls.Add((Control) this.label44);
    this.groupBox1.Controls.Add((Control) this.label115);
    this.groupBox1.Controls.Add((Control) this.label23);
    this.groupBox1.Controls.Add((Control) this.label113);
    this.groupBox1.Controls.Add((Control) this.label17);
    this.groupBox1.Controls.Add((Control) this.label43);
    this.groupBox1.Controls.Add((Control) this.LocalAddress);
    this.groupBox1.Controls.Add((Control) this.ServerVersion);
    this.groupBox1.Controls.Add((Control) this.label25);
    this.groupBox1.FlatStyle = FlatStyle.Flat;
    this.groupBox1.Font = new Font("Roboto Slab", 8f);
    this.groupBox1.ForeColor = Color.White;
    this.groupBox1.Location = new Point(15, 9);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(390, 294);
    this.groupBox1.TabIndex = 90;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Additional Information";
    this.ServerRegion.AutoSize = true;
    this.ServerRegion.Font = new Font("Roboto Slab", 10f);
    this.ServerRegion.ForeColor = Color.Silver;
    this.ServerRegion.Location = new Point(218, 57);
    this.ServerRegion.Name = "ServerRegion";
    this.ServerRegion.Size = new Size(62, 19);
    this.ServerRegion.TabIndex = 87;
    this.ServerRegion.Text = "Nations";
    this.label118.AutoSize = true;
    this.label118.Font = new Font("Roboto Slab", 10f);
    this.label118.ForeColor = Color.Silver;
    this.label118.Location = new Point(17, 57);
    this.label118.Name = "label118";
    this.label118.Size = new Size(104, 19);
    this.label118.TabIndex = 85;
    this.label118.Text = "Server Region";
    this.label28.AutoSize = true;
    this.label28.Font = new Font("Roboto Slab", 10f);
    this.label28.ForeColor = Color.Silver;
    this.label28.Location = new Point(202, 57);
    this.label28.Name = "label28";
    this.label28.Size = new Size(12, 19);
    this.label28.TabIndex = 86;
    this.label28.Text = ":";
    this.label112.AutoSize = true;
    this.label112.Font = new Font("Roboto Slab", 10f);
    this.label112.ForeColor = Color.Silver;
    this.label112.Location = new Point(17, 85);
    this.label112.Name = "label112";
    this.label112.Size = new Size(102, 19);
    this.label112.TabIndex = 43;
    this.label112.Text = "Local Address";
    this.Label116.AutoSize = true;
    this.Label116.Font = new Font("Roboto Slab", 10f);
    this.Label116.ForeColor = Color.Silver;
    this.Label116.Location = new Point(17, 197);
    this.Label116.Name = "Label116";
    this.Label116.Size = new Size(118, 19);
    this.Label116.TabIndex = 79;
    this.Label116.Text = "Internet PC Cafe";
    this.label117.AutoSize = true;
    this.label117.Font = new Font("Roboto Slab", 10f);
    this.label117.ForeColor = Color.Silver;
    this.label117.Location = new Point(17, 225);
    this.label117.Name = "label117";
    this.label117.Size = new Size(101, 19);
    this.label117.TabIndex = 52;
    this.label117.Text = "Auto Account";
    this.ConfigIndex.AutoSize = true;
    this.ConfigIndex.Font = new Font("Roboto Slab", 10f);
    this.ConfigIndex.ForeColor = Color.Silver;
    this.ConfigIndex.Location = new Point(218, 141);
    this.ConfigIndex.Name = "ConfigIndex";
    this.ConfigIndex.Size = new Size(15, 19);
    this.ConfigIndex.TabIndex = 72;
    this.ConfigIndex.Text = "1";
    this.RunTimeline.AutoSize = true;
    this.RunTimeline.Font = new Font("Roboto Slab", 10f);
    this.RunTimeline.ForeColor = Color.Silver;
    this.RunTimeline.Location = new Point(218, 113);
    this.RunTimeline.Name = "RunTimeline";
    this.RunTimeline.Size = new Size(41, 19);
    this.RunTimeline.TabIndex = 70;
    this.RunTimeline.Text = "2000";
    this.AutoBan.AutoSize = true;
    this.AutoBan.Font = new Font("Roboto Slab", 10f);
    this.AutoBan.ForeColor = Color.Silver;
    this.AutoBan.Location = new Point(218, 253);
    this.AutoBan.Name = "AutoBan";
    this.AutoBan.Size = new Size(63 /*0x3F*/, 19);
    this.AutoBan.TabIndex = 84;
    this.AutoBan.Text = "Enabled";
    this.label22.AutoSize = true;
    this.label22.Font = new Font("Roboto Slab", 10f);
    this.label22.ForeColor = Color.Silver;
    this.label22.Location = new Point(202, 141);
    this.label22.Name = "label22";
    this.label22.Size = new Size(12, 19);
    this.label22.TabIndex = 60;
    this.label22.Text = ":";
    this.RuleIndex.AutoSize = true;
    this.RuleIndex.Font = new Font("Roboto Slab", 10f);
    this.RuleIndex.ForeColor = Color.Silver;
    this.RuleIndex.Location = new Point(218, 169);
    this.RuleIndex.Name = "RuleIndex";
    this.RuleIndex.Size = new Size(63 /*0x3F*/, 19);
    this.RuleIndex.TabIndex = 73;
    this.RuleIndex.Text = "Enabled";
    this.label114.AutoSize = true;
    this.label114.Font = new Font("Roboto Slab", 10f);
    this.label114.ForeColor = Color.Silver;
    this.label114.Location = new Point(17, 141);
    this.label114.Name = "label114";
    this.label114.Size = new Size(94, 19);
    this.label114.TabIndex = 48 /*0x30*/;
    this.label114.Text = "Config Index";
    this.label119.AutoSize = true;
    this.label119.Font = new Font("Roboto Slab", 10f);
    this.label119.ForeColor = Color.Silver;
    this.label119.Location = new Point(17, 253);
    this.label119.Name = "label119";
    this.label119.Size = new Size(123, 19);
    this.label119.TabIndex = 80 /*0x50*/;
    this.label119.Text = "Auto Ban System";
    this.label20.AutoSize = true;
    this.label20.Font = new Font("Roboto Slab", 10f);
    this.label20.ForeColor = Color.Silver;
    this.label20.Location = new Point(202, 113);
    this.label20.Name = "label20";
    this.label20.Size = new Size(12, 19);
    this.label20.TabIndex = 58;
    this.label20.Text = ":";
    this.InternetCafe.AutoSize = true;
    this.InternetCafe.Font = new Font("Roboto Slab", 10f);
    this.InternetCafe.ForeColor = Color.Silver;
    this.InternetCafe.Location = new Point(218, 197);
    this.InternetCafe.Name = "InternetCafe";
    this.InternetCafe.Size = new Size(63 /*0x3F*/, 19);
    this.InternetCafe.TabIndex = 83;
    this.InternetCafe.Text = "Enabled";
    this.label26.AutoSize = true;
    this.label26.Font = new Font("Roboto Slab", 10f);
    this.label26.ForeColor = Color.Silver;
    this.label26.Location = new Point(202, 169);
    this.label26.Name = "label26";
    this.label26.Size = new Size(12, 19);
    this.label26.TabIndex = 64 /*0x40*/;
    this.label26.Text = ":";
    this.AutoAccount.AutoSize = true;
    this.AutoAccount.Font = new Font("Roboto Slab", 10f);
    this.AutoAccount.ForeColor = Color.Silver;
    this.AutoAccount.Location = new Point(218, 225);
    this.AutoAccount.Name = "AutoAccount";
    this.AutoAccount.Size = new Size(67, 19);
    this.AutoAccount.TabIndex = 76;
    this.AutoAccount.Text = "Disabled";
    this.label111.AutoSize = true;
    this.label111.Font = new Font("Roboto Slab", 10f);
    this.label111.ForeColor = Color.Silver;
    this.label111.Location = new Point(17, 29);
    this.label111.Name = "label111";
    this.label111.Size = new Size(110, 19);
    this.label111.TabIndex = 50;
    this.label111.Text = "Server Version";
    this.label44.AutoSize = true;
    this.label44.Font = new Font("Roboto Slab", 10f);
    this.label44.ForeColor = Color.Silver;
    this.label44.Location = new Point(202, 197);
    this.label44.Name = "label44";
    this.label44.Size = new Size(12, 19);
    this.label44.TabIndex = 81;
    this.label44.Text = ":";
    this.label115.AutoSize = true;
    this.label115.Font = new Font("Roboto Slab", 10f);
    this.label115.ForeColor = Color.Silver;
    this.label115.Location = new Point(17, 169);
    this.label115.Name = "label115";
    this.label115.Size = new Size(126, 19);
    this.label115.TabIndex = 49;
    this.label115.Text = "Tournament Rule";
    this.label23.AutoSize = true;
    this.label23.Font = new Font("Roboto Slab", 10f);
    this.label23.ForeColor = Color.Silver;
    this.label23.Location = new Point(202, 225);
    this.label23.Name = "label23";
    this.label23.Size = new Size(12, 19);
    this.label23.TabIndex = 61;
    this.label23.Text = ":";
    this.label113.AutoSize = true;
    this.label113.Font = new Font("Roboto Slab", 10f);
    this.label113.ForeColor = Color.Silver;
    this.label113.Location = new Point(17, 113);
    this.label113.Name = "label113";
    this.label113.Size = new Size(100, 19);
    this.label113.TabIndex = 46;
    this.label113.Text = "Run Timeline";
    this.label17.AutoSize = true;
    this.label17.Font = new Font("Roboto Slab", 10f);
    this.label17.ForeColor = Color.Silver;
    this.label17.Location = new Point(202, 85);
    this.label17.Name = "label17";
    this.label17.Size = new Size(12, 19);
    this.label17.TabIndex = 55;
    this.label17.Text = ":";
    this.label43.AutoSize = true;
    this.label43.Font = new Font("Roboto Slab", 10f);
    this.label43.ForeColor = Color.Silver;
    this.label43.Location = new Point(202, 253);
    this.label43.Name = "label43";
    this.label43.Size = new Size(12, 19);
    this.label43.TabIndex = 82;
    this.label43.Text = ":";
    this.LocalAddress.AutoSize = true;
    this.LocalAddress.Font = new Font("Roboto Slab", 10f);
    this.LocalAddress.ForeColor = Color.Silver;
    this.LocalAddress.Location = new Point(218, 85);
    this.LocalAddress.Name = "LocalAddress";
    this.LocalAddress.Size = new Size(50, 19);
    this.LocalAddress.TabIndex = 67;
    this.LocalAddress.Text = "0.0.0.0";
    this.ServerVersion.AutoSize = true;
    this.ServerVersion.Font = new Font("Roboto Slab", 10f);
    this.ServerVersion.ForeColor = Color.Silver;
    this.ServerVersion.Location = new Point(218, 29);
    this.ServerVersion.Name = "ServerVersion";
    this.ServerVersion.Size = new Size(38, 19);
    this.ServerVersion.TabIndex = 74;
    this.ServerVersion.Text = "V3.0";
    this.label25.AutoSize = true;
    this.label25.Font = new Font("Roboto Slab", 10f);
    this.label25.ForeColor = Color.Silver;
    this.label25.Location = new Point(202, 29);
    this.label25.Name = "label25";
    this.label25.Size = new Size(12, 19);
    this.label25.TabIndex = 63 /*0x3F*/;
    this.label25.Text = ":";
    this.timer_0.Interval = 1000;
    this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
    this.AutoScaleDimensions = new SizeF(7f, 15f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.FromArgb(46, 51, 73);
    this.ClientSize = new Size(420, 313);
    this.Controls.Add((Control) this.groupBox1);
    this.Font = new Font("Roboto Slab", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = nameof (FormAdditional);
    this.Text = nameof (FormAdditional);
    this.Load += new EventHandler(this.FormAdditional_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
