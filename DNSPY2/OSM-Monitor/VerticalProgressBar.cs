using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// Token: 0x02000034 RID: 52
[Description("Vertical Progress Bar")]
[ToolboxBitmap(typeof(ProgressBar))]
[Browsable(false)]
public sealed class VerticalProgressBar : UserControl
{
	// Token: 0x06000112 RID: 274 RVA: 0x0000B260 File Offset: 0x00009460
	public VerticalProgressBar()
	{
		this.method_6();
		base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		base.SetStyle(ControlStyles.UserPaint, true);
		base.SetStyle(ControlStyles.DoubleBuffer, true);
		base.Name = "VerticalProgressBar";
		base.Size = new Size(10, 120);
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000113 RID: 275 RVA: 0x00002916 File Offset: 0x00000B16
	// (set) Token: 0x06000114 RID: 276 RVA: 0x0000B2D8 File Offset: 0x000094D8
	[Description("VerticalProgressBar Maximum Value")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public int Int32_0
	{
		get
		{
			return this.int_2;
		}
		set
		{
			this.int_2 = value;
			if (this.int_2 < this.int_1)
			{
				this.int_1 = this.int_2;
			}
			if (this.int_2 < this.int_0)
			{
				this.int_0 = this.int_2;
			}
			base.Invalidate();
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000115 RID: 277 RVA: 0x0000291E File Offset: 0x00000B1E
	// (set) Token: 0x06000116 RID: 278 RVA: 0x0000B328 File Offset: 0x00009528
	[Description("VerticalProgressBar Minimum Value")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public int Int32_1
	{
		get
		{
			return this.int_1;
		}
		set
		{
			this.int_1 = value;
			if (this.int_1 > this.int_2)
			{
				this.int_2 = this.int_1;
			}
			if (this.int_1 > this.int_0)
			{
				this.int_0 = this.int_1;
			}
			base.Invalidate();
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000117 RID: 279 RVA: 0x00002926 File Offset: 0x00000B26
	// (set) Token: 0x06000118 RID: 280 RVA: 0x0000292E File Offset: 0x00000B2E
	[Description("VerticalProgressBar Step")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public int Int32_2
	{
		get
		{
			return this.int_3;
		}
		set
		{
			this.int_3 = value;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000119 RID: 281 RVA: 0x00002937 File Offset: 0x00000B37
	// (set) Token: 0x0600011A RID: 282 RVA: 0x0000B378 File Offset: 0x00009578
	[Description("VerticalProgressBar Current Value")]
	[Category("VerticalProgressBar")]
	public int Int32_3
	{
		get
		{
			return this.int_0;
		}
		set
		{
			this.int_0 = value;
			if (this.int_0 > this.int_2)
			{
				this.int_0 = this.int_2;
			}
			if (this.int_0 < this.int_1)
			{
				this.int_0 = this.int_1;
			}
			base.Invalidate();
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600011B RID: 283 RVA: 0x0000293F File Offset: 0x00000B3F
	// (set) Token: 0x0600011C RID: 284 RVA: 0x00002947 File Offset: 0x00000B47
	[Description("VerticalProgressBar Color")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public Color Color_0
	{
		get
		{
			return this.color_0;
		}
		set
		{
			this.color_0 = value;
			base.Invalidate();
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x0600011D RID: 285 RVA: 0x00002956 File Offset: 0x00000B56
	// (set) Token: 0x0600011E RID: 286 RVA: 0x0000295E File Offset: 0x00000B5E
	[Description("VerticalProgressBar Border Style")]
	[Category("VerticalProgressBar")]
	public GEnum2 GEnum2_0
	{
		get
		{
			return this.genum2_0;
		}
		set
		{
			this.genum2_0 = value;
			base.Invalidate();
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x0600011F RID: 287 RVA: 0x0000296D File Offset: 0x00000B6D
	// (set) Token: 0x06000120 RID: 288 RVA: 0x00002975 File Offset: 0x00000B75
	[Description("VerticalProgressBar Style")]
	[Category("VerticalProgressBar")]
	public GEnum1 GEnum1_0
	{
		get
		{
			return this.genum1_0;
		}
		set
		{
			this.genum1_0 = value;
			base.Invalidate();
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000B3C8 File Offset: 0x000095C8
	public void method_0()
	{
		this.int_0 += this.int_3;
		if (this.int_0 > this.int_2)
		{
			this.int_0 = this.int_2;
		}
		if (this.int_0 < this.int_1)
		{
			this.int_0 = this.int_1;
		}
		base.Invalidate();
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000B424 File Offset: 0x00009624
	public void method_1(int int_4)
	{
		this.int_0 += int_4;
		if (this.int_0 > this.int_2)
		{
			this.int_0 = this.int_2;
		}
		if (this.int_0 < this.int_1)
		{
			this.int_0 = this.int_1;
		}
		base.Invalidate();
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000B47C File Offset: 0x0000967C
	private void method_2(Graphics graphics_0)
	{
		if (this.genum2_0 == GEnum2.Classic)
		{
			Color color = ControlPaint.Dark(this.BackColor);
			Color color2 = ControlPaint.Dark(this.BackColor);
			Pen pen = new Pen(color, 1f);
			graphics_0.DrawLine(pen, base.Width, 0, 0, 0);
			graphics_0.DrawLine(pen, 0, 0, 0, base.Height);
			pen = new Pen(color2, 1f);
			graphics_0.DrawLine(pen, 0, base.Height, base.Width, base.Height);
			graphics_0.DrawLine(pen, base.Width, base.Height, base.Width, 0);
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000B518 File Offset: 0x00009718
	private void method_3(Graphics graphics_0)
	{
		if (this.int_1 == this.int_2 || this.int_0 - this.int_1 == 0)
		{
			return;
		}
		int num;
		int num2;
		int num3;
		if (this.genum2_0 == GEnum2.None)
		{
			num = base.Width;
			num2 = 0;
			num3 = base.Height;
		}
		else
		{
			if (base.Width <= 4 && base.Height <= 2)
			{
				return;
			}
			num = base.Width - 4;
			num2 = 2;
			num3 = base.Height - 1;
		}
		int num4 = (this.int_0 - this.int_1) * base.Height / (this.int_2 - this.int_1);
		if (this.genum1_0 == GEnum1.Solid)
		{
			this.method_4(graphics_0, num2, num3, num, num4);
		}
		if (this.genum1_0 == GEnum1.Classic)
		{
			this.method_5(graphics_0, num2, num3, num, num4);
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00002984 File Offset: 0x00000B84
	private void method_4(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
	{
		graphics_0.FillRectangle(new SolidBrush(this.color_0), int_4, int_5 - int_7, int_6, int_7);
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000B5D4 File Offset: 0x000097D4
	private void method_5(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
	{
		int num = int_5 - int_7;
		int num2 = int_6 * 3 / 4;
		if (num2 <= -1)
		{
			return;
		}
		for (int i = int_5; i > num; i -= num2 + 1)
		{
			graphics_0.FillRectangle(new SolidBrush(this.color_0), int_4, i - num2, int_6, num2);
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000B618 File Offset: 0x00009818
	protected void OnPaint(PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		this.method_3(graphics);
		this.method_2(graphics);
		base.OnPaint(e);
	}

	// Token: 0x06000128 RID: 296 RVA: 0x000029A0 File Offset: 0x00000BA0
	protected void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		base.Invalidate();
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000029AF File Offset: 0x00000BAF
	protected void Dispose(bool disposing)
	{
		if (disposing && this.container_0 != null)
		{
			this.container_0.Dispose();
		}
		base.Dispose(disposing);
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000029CE File Offset: 0x00000BCE
	private void method_6()
	{
		this.container_0 = new Container();
	}

	// Token: 0x04000138 RID: 312
	private Container container_0;

	// Token: 0x04000139 RID: 313
	private int int_0 = 50;

	// Token: 0x0400013A RID: 314
	private int int_1;

	// Token: 0x0400013B RID: 315
	private int int_2 = 100;

	// Token: 0x0400013C RID: 316
	private int int_3 = 10;

	// Token: 0x0400013D RID: 317
	private GEnum1 genum1_0;

	// Token: 0x0400013E RID: 318
	private GEnum2 genum2_0;

	// Token: 0x0400013F RID: 319
	private Color color_0 = Color.Blue;
}
