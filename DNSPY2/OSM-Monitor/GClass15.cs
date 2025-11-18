using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

// Token: 0x02000030 RID: 48
public class GClass15 : ProgressBar
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000FE RID: 254 RVA: 0x00002803 File Offset: 0x00000A03
	// (set) Token: 0x060000FF RID: 255 RVA: 0x0000280B File Offset: 0x00000A0B
	[Description("Font of the text on ProgressBar")]
	[Category("Additional Options")]
	public Font Font_0
	{
		[CompilerGenerated]
		get
		{
			return this.font_0;
		}
		[CompilerGenerated]
		set
		{
			this.font_0 = value;
		}
	} = new Font(FontFamily.GenericSerif, 11f, FontStyle.Bold | FontStyle.Italic);

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000100 RID: 256 RVA: 0x00002814 File Offset: 0x00000A14
	// (set) Token: 0x06000101 RID: 257 RVA: 0x00002821 File Offset: 0x00000A21
	[Category("Additional Options")]
	public Color Color_0
	{
		get
		{
			return this.solidBrush_0.Color;
		}
		set
		{
			this.solidBrush_0.Dispose();
			this.solidBrush_0 = new SolidBrush(value);
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000102 RID: 258 RVA: 0x0000283A File Offset: 0x00000A3A
	// (set) Token: 0x06000103 RID: 259 RVA: 0x00002847 File Offset: 0x00000A47
	[Category("Additional Options")]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public Color Color_1
	{
		get
		{
			return this.solidBrush_1.Color;
		}
		set
		{
			this.solidBrush_1.Dispose();
			this.solidBrush_1 = new SolidBrush(value);
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000104 RID: 260 RVA: 0x00002860 File Offset: 0x00000A60
	// (set) Token: 0x06000105 RID: 261 RVA: 0x00002868 File Offset: 0x00000A68
	[Category("Additional Options")]
	[Browsable(true)]
	public GClass15.GEnum0 GEnum0_0
	{
		get
		{
			return this.genum0_0;
		}
		set
		{
			this.genum0_0 = value;
			base.Invalidate();
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000106 RID: 262 RVA: 0x00002877 File Offset: 0x00000A77
	// (set) Token: 0x06000107 RID: 263 RVA: 0x0000287F File Offset: 0x00000A7F
	[Description("If it's empty, % will be shown")]
	[Category("Additional Options")]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public string String_0
	{
		get
		{
			return this.string_0;
		}
		set
		{
			this.string_0 = value;
			base.Invalidate();
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000108 RID: 264 RVA: 0x0000B068 File Offset: 0x00009268
	// (set) Token: 0x06000109 RID: 265 RVA: 0x0000288E File Offset: 0x00000A8E
	private string String_1
	{
		get
		{
			string text = this.String_0;
			switch (this.GEnum0_0)
			{
			case GClass15.GEnum0.Percentage:
				text = this.String_2;
				break;
			case GClass15.GEnum0.CurrProgress:
				text = this.String_3;
				break;
			case GClass15.GEnum0.TextAndPercentage:
				text = this.String_0 + ": " + this.String_2;
				break;
			case GClass15.GEnum0.TextAndCurrProgress:
				text = this.String_0 + ": " + this.String_3;
				break;
			}
			return text;
		}
		set
		{
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600010A RID: 266 RVA: 0x00002890 File Offset: 0x00000A90
	private string String_2
	{
		get
		{
			return string.Format("{0} %", (float)((int)((float)base.Value - (float)base.Minimum)) / ((float)base.Maximum - (float)base.Minimum) * 100f);
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600010B RID: 267 RVA: 0x000028C8 File Offset: 0x00000AC8
	private string String_3
	{
		get
		{
			return string.Format("{0}/{1}", base.Value, base.Maximum);
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000B0E4 File Offset: 0x000092E4
	public GClass15()
	{
		base.Value = base.Minimum;
		this.method_0();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000028EA File Offset: 0x00000AEA
	private void method_0()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000B154 File Offset: 0x00009354
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		this.method_1(graphics);
		this.method_2(graphics);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000B178 File Offset: 0x00009378
	private void method_1(Graphics graphics_0)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		ProgressBarRenderer.DrawHorizontalBar(graphics_0, clientRectangle);
		clientRectangle.Inflate(-3, -3);
		if (base.Value > 0)
		{
			Rectangle rectangle = new Rectangle(clientRectangle.X, clientRectangle.Y, (int)Math.Round((double)((float)base.Value / (float)base.Maximum * (float)clientRectangle.Width)), clientRectangle.Height);
			graphics_0.FillRectangle(this.solidBrush_1, rectangle);
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000B1F0 File Offset: 0x000093F0
	private void method_2(Graphics graphics_0)
	{
		if (this.GEnum0_0 != GClass15.GEnum0.NoText)
		{
			string string_ = this.String_1;
			SizeF sizeF = graphics_0.MeasureString(string_, this.Font_0);
			Point point = new Point(base.Width / 2 - (int)sizeF.Width / 2, base.Height / 2 - (int)sizeF.Height / 2);
			graphics_0.DrawString(string_, this.Font_0, this.solidBrush_0, point);
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x000028F8 File Offset: 0x00000AF8
	public void method_3()
	{
		this.solidBrush_0.Dispose();
		this.solidBrush_1.Dispose();
		base.Dispose();
	}

	// Token: 0x04000126 RID: 294
	[CompilerGenerated]
	private Font font_0;

	// Token: 0x04000127 RID: 295
	private SolidBrush solidBrush_0 = (SolidBrush)Brushes.Black;

	// Token: 0x04000128 RID: 296
	private SolidBrush solidBrush_1 = (SolidBrush)Brushes.LightGreen;

	// Token: 0x04000129 RID: 297
	private GClass15.GEnum0 genum0_0 = GClass15.GEnum0.CurrProgress;

	// Token: 0x0400012A RID: 298
	private string string_0 = string.Empty;

	// Token: 0x02000031 RID: 49
	public enum GEnum0
	{
		// Token: 0x0400012C RID: 300
		NoText,
		// Token: 0x0400012D RID: 301
		Percentage,
		// Token: 0x0400012E RID: 302
		CurrProgress,
		// Token: 0x0400012F RID: 303
		CustomText,
		// Token: 0x04000130 RID: 304
		TextAndPercentage,
		// Token: 0x04000131 RID: 305
		TextAndCurrProgress
	}
}
