using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class GClass15 : ProgressBar
{
	public enum GEnum0
	{
		NoText,
		Percentage,
		CurrProgress,
		CustomText,
		TextAndPercentage,
		TextAndCurrProgress
	}

	[CompilerGenerated]
	private Font font_0 = new Font(FontFamily.GenericSerif, 11f, FontStyle.Bold | FontStyle.Italic);

	private SolidBrush solidBrush_0 = (SolidBrush)Brushes.Black;

	private SolidBrush solidBrush_1 = (SolidBrush)Brushes.LightGreen;

	private GEnum0 genum0_0 = GEnum0.CurrProgress;

	private string string_0 = string.Empty;

	[Description("Font of the text on ProgressBar")]
	[Category("Additional Options")]
	public Font Font_0
	{
		[CompilerGenerated]
		get
		{
			return font_0;
		}
		[CompilerGenerated]
		set
		{
			font_0 = value;
		}
	}

	[Category("Additional Options")]
	public Color Color_0
	{
		get
		{
			return solidBrush_0.Color;
		}
		set
		{
			solidBrush_0.Dispose();
			solidBrush_0 = new SolidBrush(value);
		}
	}

	[Category("Additional Options")]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public Color Color_1
	{
		get
		{
			return solidBrush_1.Color;
		}
		set
		{
			solidBrush_1.Dispose();
			solidBrush_1 = new SolidBrush(value);
		}
	}

	[Category("Additional Options")]
	[Browsable(true)]
	public GEnum0 GEnum0_0
	{
		get
		{
			return genum0_0;
		}
		set
		{
			genum0_0 = value;
			Invalidate();
		}
	}

	[Description("If it's empty, % will be shown")]
	[Category("Additional Options")]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public string String_0
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
			Invalidate();
		}
	}

	private string String_1
	{
		get
		{
			string result = String_0;
			switch (GEnum0_0)
			{
			case GEnum0.Percentage:
				result = String_2;
				break;
			case GEnum0.CurrProgress:
				result = String_3;
				break;
			case GEnum0.TextAndCurrProgress:
				result = String_0 + ": " + String_3;
				break;
			case GEnum0.TextAndPercentage:
				result = String_0 + ": " + String_2;
				break;
			}
			return result;
		}
		set
		{
		}
	}

	private string String_2 => $"{(float)(int)((float)base.Value - (float)base.Minimum) / ((float)base.Maximum - (float)base.Minimum) * 100f} %";

	private string String_3 => $"{base.Value}/{base.Maximum}";

	public GClass15()
	{
		base.Value = base.Minimum;
		method_0();
	}

	private void method_0()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
	}

	void Control.OnPaint(PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		method_1(graphics);
		method_2(graphics);
	}

	private void method_1(Graphics graphics_0)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		ProgressBarRenderer.DrawHorizontalBar(graphics_0, clientRectangle);
		clientRectangle.Inflate(-3, -3);
		if (base.Value > 0)
		{
			graphics_0.FillRectangle(rect: new Rectangle(clientRectangle.X, clientRectangle.Y, (int)Math.Round((float)base.Value / (float)base.Maximum * (float)clientRectangle.Width), clientRectangle.Height), brush: solidBrush_1);
		}
	}

	private void method_2(Graphics graphics_0)
	{
		if (GEnum0_0 != 0)
		{
			string string_ = String_1;
			SizeF sizeF = graphics_0.MeasureString(string_, Font_0);
			graphics_0.DrawString(point: new Point(base.Width / 2 - (int)sizeF.Width / 2, base.Height / 2 - (int)sizeF.Height / 2), s: string_, font: Font_0, brush: solidBrush_0);
		}
	}

	public void method_3()
	{
		solidBrush_0.Dispose();
		solidBrush_1.Dispose();
		Dispose();
	}
}
