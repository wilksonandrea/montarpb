using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Plugin.Core.Utility;

// Token: 0x02000029 RID: 41
public partial class FormAdditional : Form
{
	// Token: 0x060000BD RID: 189 RVA: 0x0000248B File Offset: 0x0000068B
	public FormAdditional()
	{
		this.InitializeComponent();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00005764 File Offset: 0x00003964
	private void FormAdditional_Load(object sender, EventArgs e)
	{
		float num = float.Parse("10") / 100f;
		this.BackgroundImage = GClass5.smethod_0().method_0(Class11.Bitmap_0, num);
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

	// Token: 0x060000BF RID: 191 RVA: 0x0000583C File Offset: 0x00003A3C
	private void timer_0_Tick(object sender, EventArgs e)
	{
		this.ServerVersion.Text = GClass9.string_14;
		this.ServerRegion.Text = GClass9.string_15;
		this.LocalAddress.Text = GClass9.string_16;
		this.RunTimeline.Text = GClass9.string_17;
		this.ConfigIndex.Text = GClass9.string_18;
		this.RuleIndex.Text = GClass9.string_19;
		this.RuleIndex.ForeColor = (GClass9.string_19.Equals("Enabled") ? ColorUtil.Green : (GClass9.string_19.Equals("Disabled") ? ColorUtil.Yellow : ColorUtil.Silver));
		this.InternetCafe.Text = GClass9.string_20;
		this.InternetCafe.ForeColor = (GClass9.string_20.Equals("Enabled") ? ColorUtil.Green : (GClass9.string_20.Equals("Disabled") ? ColorUtil.Yellow : ColorUtil.Silver));
		this.AutoAccount.Text = GClass9.string_21;
		this.AutoAccount.ForeColor = (GClass9.string_21.Equals("Enabled") ? ColorUtil.Green : (GClass9.string_21.Equals("Disabled") ? ColorUtil.Yellow : ColorUtil.Silver));
		this.AutoBan.Text = GClass9.string_22;
		this.AutoBan.ForeColor = (GClass9.string_22.Equals("Enabled") ? ColorUtil.Green : (GClass9.string_22.Equals("Disabled") ? ColorUtil.Yellow : ColorUtil.Silver));
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00002499 File Offset: 0x00000699
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			this.icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}
}
