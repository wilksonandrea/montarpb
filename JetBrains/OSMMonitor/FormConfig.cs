// Decompiled with JetBrains decompiler
// Type: FormConfig
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.Settings;
using Plugin.Core.XML;
using Server.Game;
using Server.Match.Data.XML;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
public class FormConfig : Form
{
  private readonly DirectoryInfo directoryInfo_0;
  private IContainer icontainer_0;
  private Button OpenConfigBTN;
  private RadioButton HighRB;
  private RadioButton DefaultRB;
  private Button button_0;
  private Button ChangeLogBTN;
  private Button button_1;
  private Button button_2;
  private Button button_3;
  private Button button_4;
  private Button button_5;

  public FormConfig(DirectoryInfo directoryInfo_1)
  {
    this.InitializeComponent();
    this.directoryInfo_0 = directoryInfo_1;
  }

  private void method_0(bool bool_0, bool bool_1)
  {
    this.HighRB.Checked = bool_0;
    this.DefaultRB.Checked = bool_1;
  }

  private static void smethod_0(string string_0, string string_1)
  {
    StringBuilder stringBuilder = new StringBuilder(80 /*0x50*/);
    if (string_0 != null)
      stringBuilder.Append("---[").Append(string_0).Append(']');
    string str = string_1 == null ? "" : $"[{string_1}]---";
    int num = 79 - str.Length;
    while (stringBuilder.Length != num)
      stringBuilder.Append('-');
    stringBuilder.Append(str);
    Console.WriteLine((string_1.Equals("Ended") ? $"{stringBuilder}\n" : $"\n{stringBuilder}") ?? "");
  }

  private void FormConfig_Load(object sender, EventArgs e)
  {
    float float_0 = float.Parse("10") / 100f;
    this.BackgroundImage = (Image) GClass5.smethod_0().method_0((Image) Class11.Bitmap_0, float_0);
    this.BackgroundImageLayout = ImageLayout.Center;
    if (ConfigLoader.ShowMoreInfo)
      this.method_0(true, false);
    else
      this.method_0(false, true);
  }

  private void OpenConfigBTN_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() => GClass6.smethod_6("Config/Settings.ini", "notepad.exe", "open"))).Start();
  }

  private void ChangeLogBTN_Click(object sender, EventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    FormConfig.Class13 class13 = new FormConfig.Class13();
    // ISSUE: reference to a compiler-generated field
    class13.configEngine_0 = new ConfigEngine("Config/Settings.ini", FileAccess.ReadWrite);
    if (!ConfigLoader.ShowMoreInfo)
    {
      ConfigLoader.ShowMoreInfo = true;
      this.method_0(true, false);
    }
    else
    {
      ConfigLoader.ShowMoreInfo = false;
      this.method_0(false, true);
    }
    // ISSUE: reference to a compiler-generated method
    new Thread(new ThreadStart(class13.method_0)).Start();
    int num = (int) MessageBox.Show("Logs mode changed to " + (ConfigLoader.ShowMoreInfo ? "High." : "Default."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void button_0_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() => GClass6.smethod_5(this.directoryInfo_0))).Start();
    int num = (int) MessageBox.Show("Logs has been cleared!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void button_1_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() =>
    {
      FormConfig.smethod_0("Config", "Begin");
      ServerConfigJSON.Reload();
      CommandHelperJSON.Reload();
      ResolutionJSON.Reload();
      FormConfig.smethod_0("Config", "Ended");
    })).Start();
    int num = (int) MessageBox.Show("Config successfully Reloaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void button_5_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() =>
    {
      FormConfig.smethod_0("Shop Data", "Begin");
      ShopManager.Reset();
      ShopManager.Load(1);
      ShopManager.Load(2);
      FormConfig.smethod_0("Shop Data", "Ended");
      GameXender.UpdateShop();
    })).Start();
    int num = (int) MessageBox.Show("Shop Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void button_2_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() =>
    {
      FormConfig.smethod_0("Events Data", "Begin");
      EventLoginXML.Reload();
      EventBoostXML.Reload();
      EventPlaytimeXML.Reload();
      EventQuestXML.Reload();
      EventRankUpXML.Reload();
      EventVisitXML.Reload();
      EventXmasXML.Reload();
      FormConfig.smethod_0("Events Data", "Ended");
      GameXender.UpdateEvents();
    })).Start();
    int num = (int) MessageBox.Show("Events Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void button_4_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() =>
    {
      FormConfig.smethod_0("Classic Mode", "Begin");
      GameRuleXML.Reload();
      FormConfig.smethod_0("Classic Mode", "Ended");
    })).Start();
    int num = (int) MessageBox.Show("Tournament Rule Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void button_3_Click(object sender, EventArgs e)
  {
    new Thread((ThreadStart) (() =>
    {
      FormConfig.smethod_0("Server Data", "Begin");
      TemplatePackXML.Reload();
      TitleSystemXML.Reload();
      TitleAwardXML.Reload();
      MissionAwardXML.Reload();
      MissionConfigXML.Reload();
      SChannelXML.Reload();
      SynchronizeXML.Reload();
      SystemMapXML.Reload();
      ClanRankXML.Reload();
      PlayerRankXML.Reload();
      CouponEffectXML.Reload();
      PermissionXML.Reload();
      RandomBoxXML.Reload();
      DirectLibraryXML.Reload();
      InternetCafeXML.Reload();
      RedeemCodeXML.Reload();
      NickFilter.Reload();
      Server.Auth.Data.XML.ChannelsXML.Reload();
      Server.Game.Data.XML.ChannelsXML.Reload();
      MapStructureXML.Reload();
      CharaStructureXML.Reload();
      ItemStatisticXML.Reload();
      FormConfig.smethod_0("Server Data", "Ended");
    })).Start();
    int num = (int) MessageBox.Show("Server Data Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormConfig));
    this.HighRB = new RadioButton();
    this.DefaultRB = new RadioButton();
    this.button_0 = new Button();
    this.ChangeLogBTN = new Button();
    this.button_1 = new Button();
    this.button_2 = new Button();
    this.button_3 = new Button();
    this.button_4 = new Button();
    this.button_5 = new Button();
    this.OpenConfigBTN = new Button();
    this.SuspendLayout();
    this.HighRB.AutoCheck = false;
    this.HighRB.AutoSize = true;
    this.HighRB.ForeColor = Color.Silver;
    this.HighRB.Location = new Point(105, 41);
    this.HighRB.Name = "HighRB";
    this.HighRB.Size = new Size(71, 19);
    this.HighRB.TabIndex = 136;
    this.HighRB.Text = "High Log";
    this.HighRB.UseVisualStyleBackColor = true;
    this.DefaultRB.AutoCheck = false;
    this.DefaultRB.AutoSize = true;
    this.DefaultRB.Checked = true;
    this.DefaultRB.Cursor = Cursors.Default;
    this.DefaultRB.ForeColor = Color.Silver;
    this.DefaultRB.Location = new Point(15, 41);
    this.DefaultRB.Name = "DefaultRB";
    this.DefaultRB.Size = new Size(84, 19);
    this.DefaultRB.TabIndex = 135;
    this.DefaultRB.TabStop = true;
    this.DefaultRB.Text = "Default Log";
    this.DefaultRB.UseVisualStyleBackColor = true;
    this.button_0.Cursor = Cursors.Hand;
    this.button_0.FlatAppearance.BorderColor = Color.Gold;
    this.button_0.FlatStyle = FlatStyle.Flat;
    this.button_0.Font = new Font("Roboto Slab", 8f);
    this.button_0.ForeColor = Color.Gold;
    this.button_0.Location = new Point(15, 160 /*0xA0*/);
    this.button_0.Name = "ClearLogBTN";
    this.button_0.Size = new Size(161, 69);
    this.button_0.TabIndex = 134;
    this.button_0.Text = "Clear Log Files";
    this.button_0.UseVisualStyleBackColor = true;
    this.button_0.Click += new EventHandler(this.button_0_Click);
    this.ChangeLogBTN.Cursor = Cursors.Hand;
    this.ChangeLogBTN.FlatAppearance.BorderColor = Color.Gold;
    this.ChangeLogBTN.FlatStyle = FlatStyle.Flat;
    this.ChangeLogBTN.Font = new Font("Roboto Slab", 8f);
    this.ChangeLogBTN.ForeColor = Color.Gold;
    this.ChangeLogBTN.Location = new Point(15, 74);
    this.ChangeLogBTN.Name = "ChangeLogBTN";
    this.ChangeLogBTN.Size = new Size(161, 70);
    this.ChangeLogBTN.TabIndex = 133;
    this.ChangeLogBTN.Text = "Change Log Mode";
    this.ChangeLogBTN.UseVisualStyleBackColor = true;
    this.ChangeLogBTN.Click += new EventHandler(this.ChangeLogBTN_Click);
    this.button_1.Cursor = Cursors.Hand;
    this.button_1.FlatStyle = FlatStyle.Flat;
    this.button_1.Font = new Font("Roboto Slab", 8f);
    this.button_1.ForeColor = Color.Silver;
    this.button_1.Location = new Point(182, 74);
    this.button_1.Name = "Reload1BTN";
    this.button_1.Size = new Size(223, 26);
    this.button_1.TabIndex = 128 /*0x80*/;
    this.button_1.Text = "Reload Config";
    this.button_1.UseVisualStyleBackColor = true;
    this.button_1.Click += new EventHandler(this.button_1_Click);
    this.button_2.Cursor = Cursors.Hand;
    this.button_2.FlatStyle = FlatStyle.Flat;
    this.button_2.Font = new Font("Roboto Slab", 8f);
    this.button_2.ForeColor = Color.Silver;
    this.button_2.Location = new Point(182, 117);
    this.button_2.Name = "Reload3BTN";
    this.button_2.Size = new Size(223, 26);
    this.button_2.TabIndex = 130;
    this.button_2.Text = "Reload Events";
    this.button_2.UseVisualStyleBackColor = true;
    this.button_2.Click += new EventHandler(this.button_2_Click);
    this.button_3.Cursor = Cursors.Hand;
    this.button_3.FlatStyle = FlatStyle.Flat;
    this.button_3.Font = new Font("Roboto Slab", 8f);
    this.button_3.ForeColor = Color.Silver;
    this.button_3.Location = new Point(15, 246);
    this.button_3.Name = "Reload5BTN";
    this.button_3.Size = new Size(358, 26);
    this.button_3.TabIndex = 132;
    this.button_3.Text = "Reload Attachments";
    this.button_3.UseVisualStyleBackColor = true;
    this.button_3.Click += new EventHandler(this.button_3_Click);
    this.button_4.Cursor = Cursors.Hand;
    this.button_4.FlatStyle = FlatStyle.Flat;
    this.button_4.Font = new Font("Roboto Slab", 8f);
    this.button_4.ForeColor = Color.Silver;
    this.button_4.Location = new Point(182, 203);
    this.button_4.Name = "Reload4BTN";
    this.button_4.Size = new Size(223, 26);
    this.button_4.TabIndex = 131;
    this.button_4.Text = "Reload Item Rules";
    this.button_4.UseVisualStyleBackColor = true;
    this.button_4.Click += new EventHandler(this.button_4_Click);
    this.button_5.Cursor = Cursors.Hand;
    this.button_5.FlatStyle = FlatStyle.Flat;
    this.button_5.Font = new Font("Roboto Slab", 8f);
    this.button_5.ForeColor = Color.Silver;
    this.button_5.Location = new Point(182, 160 /*0xA0*/);
    this.button_5.Name = "Reload2BTN";
    this.button_5.Size = new Size(223, 26);
    this.button_5.TabIndex = 129;
    this.button_5.Text = "Reload Shop";
    this.button_5.UseVisualStyleBackColor = true;
    this.button_5.Click += new EventHandler(this.button_5_Click);
    this.OpenConfigBTN.BackgroundImage = (Image) componentResourceManager.GetObject("OpenConfigBTN.BackgroundImage");
    this.OpenConfigBTN.BackgroundImageLayout = ImageLayout.Stretch;
    this.OpenConfigBTN.Cursor = Cursors.Hand;
    this.OpenConfigBTN.FlatStyle = FlatStyle.Flat;
    this.OpenConfigBTN.Font = new Font("Roboto Slab", 8f);
    this.OpenConfigBTN.ForeColor = Color.Silver;
    this.OpenConfigBTN.Location = new Point(379, 246);
    this.OpenConfigBTN.Name = "OpenConfigBTN";
    this.OpenConfigBTN.Size = new Size(26, 26);
    this.OpenConfigBTN.TabIndex = 137;
    this.OpenConfigBTN.UseVisualStyleBackColor = true;
    this.OpenConfigBTN.Click += new EventHandler(this.OpenConfigBTN_Click);
    this.AutoScaleDimensions = new SizeF(7f, 15f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.FromArgb(46, 51, 73);
    this.ClientSize = new Size(420, 313);
    this.Controls.Add((Control) this.OpenConfigBTN);
    this.Controls.Add((Control) this.HighRB);
    this.Controls.Add((Control) this.DefaultRB);
    this.Controls.Add((Control) this.button_0);
    this.Controls.Add((Control) this.ChangeLogBTN);
    this.Controls.Add((Control) this.button_1);
    this.Controls.Add((Control) this.button_2);
    this.Controls.Add((Control) this.button_3);
    this.Controls.Add((Control) this.button_4);
    this.Controls.Add((Control) this.button_5);
    this.Font = new Font("Roboto Slab", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = nameof (FormConfig);
    this.Text = "FormMonitor";
    this.Load += new EventHandler(this.FormConfig_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
