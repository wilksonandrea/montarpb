using Plugin.Core;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.Settings;
using Plugin.Core.XML;
using Server.Auth.Data.XML;
using Server.Game;
using Server.Game.Data.XML;
using Server.Match.Data.XML;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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

	private void button_0_Click(object sender, EventArgs e)
	{
		(new Thread(() => GClass6.smethod_5(this.directoryInfo_0))).Start();
		MessageBox.Show("Logs has been cleared!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_1_Click(object sender, EventArgs e)
	{
		(new Thread(() => {
			FormConfig.smethod_0("Config", "Begin");
			ServerConfigJSON.Reload();
			CommandHelperJSON.Reload();
			ResolutionJSON.Reload();
			FormConfig.smethod_0("Config", "Ended");
		})).Start();
		MessageBox.Show("Config successfully Reloaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_2_Click(object sender, EventArgs e)
	{
		(new Thread(() => {
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
		MessageBox.Show("Events Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_3_Click(object sender, EventArgs e)
	{
		(new Thread(() => {
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
		MessageBox.Show("Server Data Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_4_Click(object sender, EventArgs e)
	{
		(new Thread(() => {
			FormConfig.smethod_0("Classic Mode", "Begin");
			GameRuleXML.Reload();
			FormConfig.smethod_0("Classic Mode", "Ended");
		})).Start();
		MessageBox.Show("Tournament Rule Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_5_Click(object sender, EventArgs e)
	{
		(new Thread(() => {
			FormConfig.smethod_0("Shop Data", "Begin");
			ShopManager.Reset();
			ShopManager.Load(1);
			ShopManager.Load(2);
			FormConfig.smethod_0("Shop Data", "Ended");
			GameXender.UpdateShop();
		})).Start();
		MessageBox.Show("Shop Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void ChangeLogBTN_Click(object sender, EventArgs e)
	{
		ConfigEngine configEngine = new ConfigEngine("Config/Settings.ini", FileAccess.ReadWrite);
		if (ConfigLoader.ShowMoreInfo)
		{
			ConfigLoader.ShowMoreInfo = false;
			this.method_0(false, true);
		}
		else
		{
			ConfigLoader.ShowMoreInfo = true;
			this.method_0(true, false);
		}
		(new Thread(() => {
			string str = "MoreInfo";
			string str1 = "Server";
			if (configEngine.KeyExists(str, str1))
			{
				configEngine.WriteX(str, ConfigLoader.ShowMoreInfo, str1);
				return;
			}
			MessageBox.Show(string.Concat(new string[] { "Key: '", str, "' on Section '", str1, "' doesn't exist!" }), "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		})).Start();
		MessageBox.Show(string.Concat("Logs mode changed to ", (ConfigLoader.ShowMoreInfo ? "High." : "Default.")), "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void FormConfig_Load(object sender, EventArgs e)
	{
		float single = float.Parse("10") / 100f;
		this.BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, single);
		this.BackgroundImageLayout = ImageLayout.Center;
		if (ConfigLoader.ShowMoreInfo)
		{
			this.method_0(true, false);
			return;
		}
		this.method_0(false, true);
	}

	private void InitializeComponent()
	{
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormConfig));
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
		base.SuspendLayout();
		this.HighRB.AutoCheck = false;
		this.HighRB.AutoSize = true;
		this.HighRB.ForeColor = Color.Silver;
		this.HighRB.Location = new Point(105, 41);
		this.HighRB.Name = "HighRB";
		this.HighRB.Size = new System.Drawing.Size(71, 19);
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
		this.DefaultRB.Size = new System.Drawing.Size(84, 19);
		this.DefaultRB.TabIndex = 135;
		this.DefaultRB.TabStop = true;
		this.DefaultRB.Text = "Default Log";
		this.DefaultRB.UseVisualStyleBackColor = true;
		this.button_0.Cursor = Cursors.Hand;
		this.button_0.FlatAppearance.BorderColor = Color.Gold;
		this.button_0.FlatStyle = FlatStyle.Flat;
		this.button_0.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_0.ForeColor = Color.Gold;
		this.button_0.Location = new Point(15, 160);
		this.button_0.Name = "ClearLogBTN";
		this.button_0.Size = new System.Drawing.Size(161, 69);
		this.button_0.TabIndex = 134;
		this.button_0.Text = "Clear Log Files";
		this.button_0.UseVisualStyleBackColor = true;
		this.button_0.Click += new EventHandler(this.button_0_Click);
		this.ChangeLogBTN.Cursor = Cursors.Hand;
		this.ChangeLogBTN.FlatAppearance.BorderColor = Color.Gold;
		this.ChangeLogBTN.FlatStyle = FlatStyle.Flat;
		this.ChangeLogBTN.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.ChangeLogBTN.ForeColor = Color.Gold;
		this.ChangeLogBTN.Location = new Point(15, 74);
		this.ChangeLogBTN.Name = "ChangeLogBTN";
		this.ChangeLogBTN.Size = new System.Drawing.Size(161, 70);
		this.ChangeLogBTN.TabIndex = 133;
		this.ChangeLogBTN.Text = "Change Log Mode";
		this.ChangeLogBTN.UseVisualStyleBackColor = true;
		this.ChangeLogBTN.Click += new EventHandler(this.ChangeLogBTN_Click);
		this.button_1.Cursor = Cursors.Hand;
		this.button_1.FlatStyle = FlatStyle.Flat;
		this.button_1.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_1.ForeColor = Color.Silver;
		this.button_1.Location = new Point(182, 74);
		this.button_1.Name = "Reload1BTN";
		this.button_1.Size = new System.Drawing.Size(223, 26);
		this.button_1.TabIndex = 128;
		this.button_1.Text = "Reload Config";
		this.button_1.UseVisualStyleBackColor = true;
		this.button_1.Click += new EventHandler(this.button_1_Click);
		this.button_2.Cursor = Cursors.Hand;
		this.button_2.FlatStyle = FlatStyle.Flat;
		this.button_2.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_2.ForeColor = Color.Silver;
		this.button_2.Location = new Point(182, 117);
		this.button_2.Name = "Reload3BTN";
		this.button_2.Size = new System.Drawing.Size(223, 26);
		this.button_2.TabIndex = 130;
		this.button_2.Text = "Reload Events";
		this.button_2.UseVisualStyleBackColor = true;
		this.button_2.Click += new EventHandler(this.button_2_Click);
		this.button_3.Cursor = Cursors.Hand;
		this.button_3.FlatStyle = FlatStyle.Flat;
		this.button_3.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_3.ForeColor = Color.Silver;
		this.button_3.Location = new Point(15, 246);
		this.button_3.Name = "Reload5BTN";
		this.button_3.Size = new System.Drawing.Size(358, 26);
		this.button_3.TabIndex = 132;
		this.button_3.Text = "Reload Attachments";
		this.button_3.UseVisualStyleBackColor = true;
		this.button_3.Click += new EventHandler(this.button_3_Click);
		this.button_4.Cursor = Cursors.Hand;
		this.button_4.FlatStyle = FlatStyle.Flat;
		this.button_4.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_4.ForeColor = Color.Silver;
		this.button_4.Location = new Point(182, 203);
		this.button_4.Name = "Reload4BTN";
		this.button_4.Size = new System.Drawing.Size(223, 26);
		this.button_4.TabIndex = 131;
		this.button_4.Text = "Reload Item Rules";
		this.button_4.UseVisualStyleBackColor = true;
		this.button_4.Click += new EventHandler(this.button_4_Click);
		this.button_5.Cursor = Cursors.Hand;
		this.button_5.FlatStyle = FlatStyle.Flat;
		this.button_5.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_5.ForeColor = Color.Silver;
		this.button_5.Location = new Point(182, 160);
		this.button_5.Name = "Reload2BTN";
		this.button_5.Size = new System.Drawing.Size(223, 26);
		this.button_5.TabIndex = 129;
		this.button_5.Text = "Reload Shop";
		this.button_5.UseVisualStyleBackColor = true;
		this.button_5.Click += new EventHandler(this.button_5_Click);
		this.OpenConfigBTN.BackgroundImage = (Image)componentResourceManager.GetObject("OpenConfigBTN.BackgroundImage");
		this.OpenConfigBTN.BackgroundImageLayout = ImageLayout.Stretch;
		this.OpenConfigBTN.Cursor = Cursors.Hand;
		this.OpenConfigBTN.FlatStyle = FlatStyle.Flat;
		this.OpenConfigBTN.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.OpenConfigBTN.ForeColor = Color.Silver;
		this.OpenConfigBTN.Location = new Point(379, 246);
		this.OpenConfigBTN.Name = "OpenConfigBTN";
		this.OpenConfigBTN.Size = new System.Drawing.Size(26, 26);
		this.OpenConfigBTN.TabIndex = 137;
		this.OpenConfigBTN.UseVisualStyleBackColor = true;
		this.OpenConfigBTN.Click += new EventHandler(this.OpenConfigBTN_Click);
		base.AutoScaleDimensions = new SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = Color.FromArgb(46, 51, 73);
		base.ClientSize = new System.Drawing.Size(420, 313);
		base.Controls.Add(this.OpenConfigBTN);
		base.Controls.Add(this.HighRB);
		base.Controls.Add(this.DefaultRB);
		base.Controls.Add(this.button_0);
		base.Controls.Add(this.ChangeLogBTN);
		base.Controls.Add(this.button_1);
		base.Controls.Add(this.button_2);
		base.Controls.Add(this.button_3);
		base.Controls.Add(this.button_4);
		base.Controls.Add(this.button_5);
		this.Font = new System.Drawing.Font("Roboto Slab", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
		base.Name = "FormConfig";
		this.Text = "FormMonitor";
		base.Load += new EventHandler(this.FormConfig_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void method_0(bool bool_0, bool bool_1)
	{
		this.HighRB.Checked = bool_0;
		this.DefaultRB.Checked = bool_1;
	}

	private void OpenConfigBTN_Click(object sender, EventArgs e)
	{
		(new Thread(() => GClass6.smethod_6("Config/Settings.ini", "notepad.exe", "open"))).Start();
	}

	private static void smethod_0(string string_0, string string_1)
	{
		StringBuilder stringBuilder = new StringBuilder(80);
		if (string_0 != null)
		{
			stringBuilder.Append("---[").Append(string_0).Append(']');
		}
		string str = (string_1 == null ? "" : string.Concat("[", string_1, "]---"));
		int length = 79 - str.Length;
		while (stringBuilder.Length != length)
		{
			stringBuilder.Append('-');
		}
		stringBuilder.Append(str);
		Console.WriteLine((string_1.Equals("Ended") ? string.Format("{0}\n", stringBuilder) : string.Format("\n{0}", stringBuilder)) ?? "");
	}

	protected override void System.Windows.Forms.Form.Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			this.icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}
}