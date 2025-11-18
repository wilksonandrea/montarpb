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

// Token: 0x0200002A RID: 42
public partial class FormConfig : Form
{
	// Token: 0x060000C2 RID: 194 RVA: 0x000024B8 File Offset: 0x000006B8
	public FormConfig(DirectoryInfo directoryInfo_1)
	{
		this.InitializeComponent();
		this.directoryInfo_0 = directoryInfo_1;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x000024CD File Offset: 0x000006CD
	private void method_0(bool bool_0, bool bool_1)
	{
		this.HighRB.Checked = bool_0;
		this.DefaultRB.Checked = bool_1;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00006E5C File Offset: 0x0000505C
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
		Console.WriteLine((string_1.Equals("Ended") ? string.Format("{0}\n", stringBuilder) : string.Format("\n{0}", stringBuilder)) ?? "");
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00006F04 File Offset: 0x00005104
	private void FormConfig_Load(object sender, EventArgs e)
	{
		float num = float.Parse("10") / 100f;
		this.BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, num);
		this.BackgroundImageLayout = ImageLayout.Center;
		if (ConfigLoader.ShowMoreInfo)
		{
			this.method_0(true, false);
			return;
		}
		this.method_0(false, true);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000024E7 File Offset: 0x000006E7
	private void OpenConfigBTN_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(FormConfig.Class12.<>9.method_0)).Start();
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00006F58 File Offset: 0x00005158
	private void ChangeLogBTN_Click(object sender, EventArgs e)
	{
		FormConfig.Class13 @class = new FormConfig.Class13();
		@class.configEngine_0 = new ConfigEngine("Config/Settings.ini", FileAccess.ReadWrite);
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
		new Thread(new ThreadStart(@class.method_0)).Start();
		MessageBox.Show("Logs mode changed to " + (ConfigLoader.ShowMoreInfo ? "High." : "Default."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00002512 File Offset: 0x00000712
	private void button_0_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(this.method_1)).Start();
		MessageBox.Show("Logs has been cleared!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0000253D File Offset: 0x0000073D
	private void button_1_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(FormConfig.Class12.<>9.method_1)).Start();
		MessageBox.Show("Config successfully Reloaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000257B File Offset: 0x0000077B
	private void button_5_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(FormConfig.Class12.<>9.method_2)).Start();
		MessageBox.Show("Shop Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000025B9 File Offset: 0x000007B9
	private void button_2_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(FormConfig.Class12.<>9.method_3)).Start();
		MessageBox.Show("Events Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x000025F7 File Offset: 0x000007F7
	private void button_4_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(FormConfig.Class12.<>9.method_4)).Start();
		MessageBox.Show("Tournament Rule Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00002635 File Offset: 0x00000835
	private void button_3_Click(object sender, EventArgs e)
	{
		new Thread(new ThreadStart(FormConfig.Class12.<>9.method_5)).Start();
		MessageBox.Show("Server Data Sucessfully reloaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00002673 File Offset: 0x00000873
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			this.icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00002692 File Offset: 0x00000892
	[CompilerGenerated]
	private void method_1()
	{
		GClass6.smethod_5(this.directoryInfo_0);
	}

	// Token: 0x040000C8 RID: 200
	private readonly DirectoryInfo directoryInfo_0;

	// Token: 0x040000C9 RID: 201
	private IContainer icontainer_0;

	// Token: 0x0200002B RID: 43
	[CompilerGenerated]
	[Serializable]
	private sealed class Class12
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000026AC File Offset: 0x000008AC
		internal void method_0()
		{
			GClass6.smethod_6("Config/Settings.ini", "notepad.exe", "open");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000026C2 File Offset: 0x000008C2
		internal void method_1()
		{
			FormConfig.smethod_0("Config", "Begin");
			ServerConfigJSON.Reload();
			CommandHelperJSON.Reload();
			ResolutionJSON.Reload();
			FormConfig.smethod_0("Config", "Ended");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000026F1 File Offset: 0x000008F1
		internal void method_2()
		{
			FormConfig.smethod_0("Shop Data", "Begin");
			ShopManager.Reset();
			ShopManager.Load(1);
			ShopManager.Load(2);
			FormConfig.smethod_0("Shop Data", "Ended");
			GameXender.UpdateShop();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007988 File Offset: 0x00005B88
		internal void method_3()
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
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002727 File Offset: 0x00000927
		internal void method_4()
		{
			FormConfig.smethod_0("Classic Mode", "Begin");
			GameRuleXML.Reload();
			FormConfig.smethod_0("Classic Mode", "Ended");
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000079DC File Offset: 0x00005BDC
		internal void method_5()
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
		}

		// Token: 0x040000D4 RID: 212
		public static readonly FormConfig.Class12 <>9 = new FormConfig.Class12();

		// Token: 0x040000D5 RID: 213
		public static ThreadStart <>9__5_0;

		// Token: 0x040000D6 RID: 214
		public static ThreadStart <>9__8_0;

		// Token: 0x040000D7 RID: 215
		public static ThreadStart <>9__9_0;

		// Token: 0x040000D8 RID: 216
		public static ThreadStart <>9__10_0;

		// Token: 0x040000D9 RID: 217
		public static ThreadStart <>9__11_0;

		// Token: 0x040000DA RID: 218
		public static ThreadStart <>9__12_0;
	}

	// Token: 0x0200002C RID: 44
	[CompilerGenerated]
	private sealed class Class13
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00007A78 File Offset: 0x00005C78
		internal void method_0()
		{
			string text = "MoreInfo";
			string text2 = "Server";
			if (!this.configEngine_0.KeyExists(text, text2))
			{
				MessageBox.Show(string.Concat(new string[] { "Key: '", text, "' on Section '", text2, "' doesn't exist!" }), "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.configEngine_0.WriteX(text, ConfigLoader.ShowMoreInfo, text2);
		}

		// Token: 0x040000DB RID: 219
		public ConfigEngine configEngine_0;
	}
}
