using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class GClass15 : ProgressBar
{
	private SolidBrush solidBrush_0 = (SolidBrush)Brushes.Black;

	private SolidBrush solidBrush_1 = (SolidBrush)Brushes.LightGreen;

	private GClass15.GEnum0 genum0_0 = GClass15.GEnum0.CurrProgress;

	private string string_0 = string.Empty;

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

	[Browsable(true)]
	[Category("Additional Options")]
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

	[Category("Additional Options")]
	[Description("Font of the text on ProgressBar")]
	public System.Drawing.Font Font_0 { get; set; } = new System.Drawing.Font(FontFamily.GenericSerif, 11f, FontStyle.Bold | FontStyle.Italic);

	[Browsable(true)]
	[Category("Additional Options")]
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

	[Browsable(true)]
	[Category("Additional Options")]
	[Description("If it's empty, % will be shown")]
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

	private string String_1
	{
		get
		{
			string string0 = this.String_0;
			switch (this.GEnum0_0)
			{
				case GClass15.GEnum0.Percentage:
				{
					string0 = this.String_2;
					return string0;
				}
				case GClass15.GEnum0.CurrProgress:
				{
					string0 = this.String_3;
					return string0;
				}
				case GClass15.GEnum0.CustomText:
				{
					return string0;
				}
				case GClass15.GEnum0.TextAndPercentage:
				{
					string0 = string.Concat(this.String_0, ": ", this.String_2);
					return string0;
				}
				case GClass15.GEnum0.TextAndCurrProgress:
				{
					string0 = string.Concat(this.String_0, ": ", this.String_3);
					return string0;
				}
				default:
				{
					return string0;
				}
			}
		}
		set
		{
		}
	}

	private string String_2
	{
		get
		{
			return string.Format("{0} %", (float)((int)((float)base.Value - (float)base.Minimum)) / ((float)base.Maximum - (float)base.Minimum) * 100f);
		}
	}

	private string String_3
	{
		get
		{
			return string.Format("{0}/{1}", base.Value, base.Maximum);
		}
	}

	public GClass15()
	{
		base.Value = base.Minimum;
		this.method_0();
	}

	private void method_0()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
	}

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

	private void method_2(Graphics graphics_0)
	{
		if (this.GEnum0_0 != GClass15.GEnum0.NoText)
		{
			string string1 = this.String_1;
			SizeF sizeF = graphics_0.MeasureString(string1, this.Font_0);
			Point point = new Point(base.Width / 2 - (int)sizeF.Width / 2, base.Height / 2 - (int)sizeF.Height / 2);
			graphics_0.DrawString(string1, this.Font_0, this.solidBrush_0, point);
		}
	}

	public void method_3()
	{
		this.solidBrush_0.Dispose();
		this.solidBrush_1.Dispose();
		base.Dispose();
	}

	protected override void System.Windows.Forms.Control.OnPaint(PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		this.method_1(graphics);
		this.method_2(graphics);
	}

	public enum GEnum0
	{
		NoText,
		Percentage,
		CurrProgress,
		CustomText,
		TextAndPercentage,
		TextAndCurrProgress
	}
}