using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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

public class FormConfig : Form
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class12
	{
		public static readonly Class12 _003C_003E9 = new Class12();

		public static ThreadStart _003C_003E9__5_0;

		public static ThreadStart _003C_003E9__8_0;

		public static ThreadStart _003C_003E9__9_0;

		public static ThreadStart _003C_003E9__10_0;

		public static ThreadStart _003C_003E9__11_0;

		public static ThreadStart _003C_003E9__12_0;

		internal void method_0()
		{
			GClass6.smethod_6("Config/Settings.ini", "notepad.exe", "open");
		}

		internal void method_1()
		{
			smethod_0("Config", "Begin");
			ServerConfigJSON.Reload();
			CommandHelperJSON.Reload();
			ResolutionJSON.Reload();
			smethod_0("Config", "Ended");
		}

		internal void method_2()
		{
			smethod_0("Shop Data", "Begin");
			ShopManager.Reset();
			ShopManager.Load(1);
			ShopManager.Load(2);
			smethod_0("Shop Data", "Ended");
			GameXender.UpdateShop();
		}

		internal void method_3()
		{
			smethod_0("Events Data", "Begin");
			EventLoginXML.Reload();
			EventBoostXML.Reload();
			EventPlaytimeXML.Reload();
			EventQuestXML.Reload();
			EventRankUpXML.Reload();
			EventVisitXML.Reload();
			EventXmasXML.Reload();
			smethod_0("Events Data", "Ended");
			GameXender.UpdateEvents();
		}

		internal void method_4()
		{
			smethod_0("Classic Mode", "Begin");
			GameRuleXML.Reload();
			smethod_0("Classic Mode", "Ended");
		}

		internal void method_5()
		{
			smethod_0("Server Data", "Begin");
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
			smethod_0("Server Data", "Ended");
		}
	}

	[CompilerGenerated]
	private sealed class Class13
	{
		public ConfigEngine configEngine_0;

		internal void method_0()
		{
			string text = "MoreInfo";
			string text2 = "Server";
			if (!configEngine_0.KeyExists(text, text2))
			{
				MessageBox.Show("Key: '" + text + "' on Section '" + text2 + "' doesn't exist!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				configEngine_0.WriteX(text, ConfigLoader.ShowMoreInfo, text2);
			}
		}
	}

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
		InitializeComponent();
		directoryInfo_0 = directoryInfo_1;
	}

	private void method_0(bool bool_0, bool bool_1)
	{
		HighRB.Checked = bool_0;
		DefaultRB.Checked = bool_1;
	}

	private static void smethod_0(string string_0, string string_1)
	{
		StringBuilder stringBuilder = new StringBuilder(80);
		if (string_0 != null)
		{
			stringBuilder.Append("---[").Append(string_0).Append(']');
		}
		string text = ((string_1 == null) ? "" : ("[" + string_1 + "]---"));
		int num = 79 - text.Length;
		while (stringBuilder.Length != num)
		{
			stringBuilder.Append('-');
		}
		stringBuilder.Append(text);
		Console.WriteLine((string_1.Equals("Ended") ? $"{stringBuilder}\n" : $"\n{stringBuilder}") ?? "");
	}

	private void FormConfig_Load(object sender, EventArgs e)
	{
		float float_ = float.Parse("10") / 100f;
		BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, float_);
		BackgroundImageLayout = ImageLayout.Center;
		if (ConfigLoader.ShowMoreInfo)
		{
			method_0(bool_0: true, bool_1: false);
		}
		else
		{
			method_0(bool_0: false, bool_1: true);
		}
	}

	private void OpenConfigBTN_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			GClass6.smethod_6("Config/Settings.ini", "notepad.exe", "open");
		}).Start();
	}

	private void ChangeLogBTN_Click(object sender, EventArgs e)
	{
		ConfigEngine configEngine_0 = new ConfigEngine("Config/Settings.ini");
		if (!ConfigLoader.ShowMoreInfo)
		{
			ConfigLoader.ShowMoreInfo = true;
			method_0(bool_0: true, bool_1: false);
		}
		else
		{
			ConfigLoader.ShowMoreInfo = false;
			method_0(bool_0: false, bool_1: true);
		}
		new Thread((ThreadStart)delegate
		{
			string text = "MoreInfo";
			string text2 = "Server";
			if (!configEngine_0.KeyExists(text, text2))
			{
				MessageBox.Show("Key: '" + text + "' on Section '" + text2 + "' doesn't exist!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				configEngine_0.WriteX(text, ConfigLoader.ShowMoreInfo, text2);
			}
		}).Start();
		MessageBox.Show("Logs mode changed to " + (ConfigLoader.ShowMoreInfo ? "High." : "Default."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_0_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			GClass6.smethod_5(directoryInfo_0);
		}).Start();
		MessageBox.Show("Logs has been cleared!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_1_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			smethod_0("Config", "Begin");
			ServerConfigJSON.Reload();
			CommandHelperJSON.Reload();
			ResolutionJSON.Reload();
			smethod_0("Config", "Ended");
		}).Start();
		MessageBox.Show("Config successfully Reloaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_5_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			smethod_0("Shop Data", "Begin");
			ShopManager.Reset();
			ShopManager.Load(1);
			ShopManager.Load(2);
			smethod_0("Shop Data", "Ended");
			GameXender.UpdateShop();
		}).Start();
		MessageBox.Show("Shop Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_2_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			smethod_0("Events Data", "Begin");
			EventLoginXML.Reload();
			EventBoostXML.Reload();
			EventPlaytimeXML.Reload();
			EventQuestXML.Reload();
			EventRankUpXML.Reload();
			EventVisitXML.Reload();
			EventXmasXML.Reload();
			smethod_0("Events Data", "Ended");
			GameXender.UpdateEvents();
		}).Start();
		MessageBox.Show("Events Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_4_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			smethod_0("Classic Mode", "Begin");
			GameRuleXML.Reload();
			smethod_0("Classic Mode", "Ended");
		}).Start();
		MessageBox.Show("Tournament Rule Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button_3_Click(object sender, EventArgs e)
	{
		new Thread((ThreadStart)delegate
		{
			smethod_0("Server Data", "Begin");
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
			smethod_0("Server Data", "Ended");
		}).Start();
		MessageBox.Show("Server Data Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	void Form.Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
		this.HighRB = new System.Windows.Forms.RadioButton();
		this.DefaultRB = new System.Windows.Forms.RadioButton();
		this.button_0 = new System.Windows.Forms.Button();
		this.ChangeLogBTN = new System.Windows.Forms.Button();
		this.button_1 = new System.Windows.Forms.Button();
		this.button_2 = new System.Windows.Forms.Button();
		this.button_3 = new System.Windows.Forms.Button();
		this.button_4 = new System.Windows.Forms.Button();
		this.button_5 = new System.Windows.Forms.Button();
		this.OpenConfigBTN = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.HighRB.AutoCheck = false;
		this.HighRB.AutoSize = true;
		this.HighRB.ForeColor = System.Drawing.Color.Silver;
		this.HighRB.Location = new System.Drawing.Point(105, 41);
		this.HighRB.Name = "HighRB";
		this.HighRB.Size = new System.Drawing.Size(71, 19);
		this.HighRB.TabIndex = 136;
		this.HighRB.Text = "High Log";
		this.HighRB.UseVisualStyleBackColor = true;
		this.DefaultRB.AutoCheck = false;
		this.DefaultRB.AutoSize = true;
		this.DefaultRB.Checked = true;
		this.DefaultRB.Cursor = System.Windows.Forms.Cursors.Default;
		this.DefaultRB.ForeColor = System.Drawing.Color.Silver;
		this.DefaultRB.Location = new System.Drawing.Point(15, 41);
		this.DefaultRB.Name = "DefaultRB";
		this.DefaultRB.Size = new System.Drawing.Size(84, 19);
		this.DefaultRB.TabIndex = 135;
		this.DefaultRB.TabStop = true;
		this.DefaultRB.Text = "Default Log";
		this.DefaultRB.UseVisualStyleBackColor = true;
		this.button_0.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_0.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
		this.button_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_0.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_0.ForeColor = System.Drawing.Color.Gold;
		this.button_0.Location = new System.Drawing.Point(15, 160);
		this.button_0.Name = "ClearLogBTN";
		this.button_0.Size = new System.Drawing.Size(161, 69);
		this.button_0.TabIndex = 134;
		this.button_0.Text = "Clear Log Files";
		this.button_0.UseVisualStyleBackColor = true;
		this.button_0.Click += new System.EventHandler(button_0_Click);
		this.ChangeLogBTN.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ChangeLogBTN.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
		this.ChangeLogBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ChangeLogBTN.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.ChangeLogBTN.ForeColor = System.Drawing.Color.Gold;
		this.ChangeLogBTN.Location = new System.Drawing.Point(15, 74);
		this.ChangeLogBTN.Name = "ChangeLogBTN";
		this.ChangeLogBTN.Size = new System.Drawing.Size(161, 70);
		this.ChangeLogBTN.TabIndex = 133;
		this.ChangeLogBTN.Text = "Change Log Mode";
		this.ChangeLogBTN.UseVisualStyleBackColor = true;
		this.ChangeLogBTN.Click += new System.EventHandler(ChangeLogBTN_Click);
		this.button_1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_1.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_1.ForeColor = System.Drawing.Color.Silver;
		this.button_1.Location = new System.Drawing.Point(182, 74);
		this.button_1.Name = "Reload1BTN";
		this.button_1.Size = new System.Drawing.Size(223, 26);
		this.button_1.TabIndex = 128;
		this.button_1.Text = "Reload Config";
		this.button_1.UseVisualStyleBackColor = true;
		this.button_1.Click += new System.EventHandler(button_1_Click);
		this.button_2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_2.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_2.ForeColor = System.Drawing.Color.Silver;
		this.button_2.Location = new System.Drawing.Point(182, 117);
		this.button_2.Name = "Reload3BTN";
		this.button_2.Size = new System.Drawing.Size(223, 26);
		this.button_2.TabIndex = 130;
		this.button_2.Text = "Reload Events";
		this.button_2.UseVisualStyleBackColor = true;
		this.button_2.Click += new System.EventHandler(button_2_Click);
		this.button_3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_3.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_3.ForeColor = System.Drawing.Color.Silver;
		this.button_3.Location = new System.Drawing.Point(15, 246);
		this.button_3.Name = "Reload5BTN";
		this.button_3.Size = new System.Drawing.Size(358, 26);
		this.button_3.TabIndex = 132;
		this.button_3.Text = "Reload Attachments";
		this.button_3.UseVisualStyleBackColor = true;
		this.button_3.Click += new System.EventHandler(button_3_Click);
		this.button_4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_4.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_4.ForeColor = System.Drawing.Color.Silver;
		this.button_4.Location = new System.Drawing.Point(182, 203);
		this.button_4.Name = "Reload4BTN";
		this.button_4.Size = new System.Drawing.Size(223, 26);
		this.button_4.TabIndex = 131;
		this.button_4.Text = "Reload Item Rules";
		this.button_4.UseVisualStyleBackColor = true;
		this.button_4.Click += new System.EventHandler(button_4_Click);
		this.button_5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button_5.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.button_5.ForeColor = System.Drawing.Color.Silver;
		this.button_5.Location = new System.Drawing.Point(182, 160);
		this.button_5.Name = "Reload2BTN";
		this.button_5.Size = new System.Drawing.Size(223, 26);
		this.button_5.TabIndex = 129;
		this.button_5.Text = "Reload Shop";
		this.button_5.UseVisualStyleBackColor = true;
		this.button_5.Click += new System.EventHandler(button_5_Click);
		this.OpenConfigBTN.BackgroundImage = (System.Drawing.Image)resources.GetObject("OpenConfigBTN.BackgroundImage");
		this.OpenConfigBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.OpenConfigBTN.Cursor = System.Windows.Forms.Cursors.Hand;
		this.OpenConfigBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.OpenConfigBTN.Font = new System.Drawing.Font("Roboto Slab", 8f);
		this.OpenConfigBTN.ForeColor = System.Drawing.Color.Silver;
		this.OpenConfigBTN.Location = new System.Drawing.Point(379, 246);
		this.OpenConfigBTN.Name = "OpenConfigBTN";
		this.OpenConfigBTN.Size = new System.Drawing.Size(26, 26);
		this.OpenConfigBTN.TabIndex = 137;
		this.OpenConfigBTN.UseVisualStyleBackColor = true;
		this.OpenConfigBTN.Click += new System.EventHandler(OpenConfigBTN_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
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
		this.Font = new System.Drawing.Font("Roboto Slab", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormConfig";
		this.Text = "FormMonitor";
		base.Load += new System.EventHandler(FormConfig_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	[CompilerGenerated]
	private void method_1()
	{
		GClass6.smethod_5(directoryInfo_0);
	}
}
