using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// Token: 0x0200002D RID: 45
public partial class FormMonitor : Form
{
	// Token: 0x060000DB RID: 219 RVA: 0x0000274C File Offset: 0x0000094C
	public FormMonitor()
	{
		this.InitializeComponent();
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00007AEC File Offset: 0x00005CEC
	private void FormMonitor_Load(object sender, EventArgs e)
	{
		float num = float.Parse("10") / 100f;
		this.BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, num);
		this.BackgroundImageLayout = ImageLayout.Center;
		this.RegisteredUsers.Text = "Loading...";
		this.OnlinePlayers.Text = "Loading...";
		this.TotalClans.Text = "Loading...";
		this.VipUsers.Text = "Loading...";
		this.UnknownUsers.Text = "Loading...";
		this.TotalBannedPlayers.Text = "Loading...";
		this.RegisteredShopItems.Text = "Loading...";
		this.ShopCafeItems.Text = "Loading...";
		this.RepairableItems.Text = "Loading...";
		this.timer_0.Start();
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00007BC4 File Offset: 0x00005DC4
	private void timer_0_Tick(object sender, EventArgs e)
	{
		this.RegisteredUsers.Text = GClass9.string_5;
		this.OnlinePlayers.Text = GClass9.string_6;
		this.TotalClans.Text = GClass9.string_7;
		this.VipUsers.Text = GClass9.string_8;
		this.UnknownUsers.Text = GClass9.string_9;
		this.TotalBannedPlayers.Text = GClass9.string_10;
		this.RegisteredShopItems.Text = GClass9.string_11;
		this.ShopCafeItems.Text = GClass9.string_12;
		this.RepairableItems.Text = GClass9.string_13;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000275A File Offset: 0x0000095A
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			this.icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}
}
