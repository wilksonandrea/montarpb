using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

[Description("Vertical Progress Bar")]
[ToolboxBitmap(typeof(ProgressBar))]
[Browsable(false)]
public sealed class VerticalProgressBar : UserControl
{
	private Container container_0;

	private int int_0 = 50;

	private int int_1;

	private int int_2 = 100;

	private int int_3 = 10;

	private GEnum1 genum1_0;

	private GEnum2 genum2_0;

	private Color color_0 = Color.Blue;

	[Description("VerticalProgressBar Maximum Value")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public int Int32_0
	{
		get
		{
			return int_2;
		}
		set
		{
			int_2 = value;
			if (int_2 < int_1)
			{
				int_1 = int_2;
			}
			if (int_2 < int_0)
			{
				int_0 = int_2;
			}
			Invalidate();
		}
	}

	[Description("VerticalProgressBar Minimum Value")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public int Int32_1
	{
		get
		{
			return int_1;
		}
		set
		{
			int_1 = value;
			if (int_1 > int_2)
			{
				int_2 = int_1;
			}
			if (int_1 > int_0)
			{
				int_0 = int_1;
			}
			Invalidate();
		}
	}

	[Description("VerticalProgressBar Step")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public int Int32_2
	{
		get
		{
			return int_3;
		}
		set
		{
			int_3 = value;
		}
	}

	[Description("VerticalProgressBar Current Value")]
	[Category("VerticalProgressBar")]
	public int Int32_3
	{
		get
		{
			return int_0;
		}
		set
		{
			int_0 = value;
			if (int_0 > int_2)
			{
				int_0 = int_2;
			}
			if (int_0 < int_1)
			{
				int_0 = int_1;
			}
			Invalidate();
		}
	}

	[Description("VerticalProgressBar Color")]
	[Category("VerticalProgressBar")]
	[RefreshProperties(RefreshProperties.All)]
	public Color Color_0
	{
		get
		{
			return color_0;
		}
		set
		{
			color_0 = value;
			Invalidate();
		}
	}

	[Description("VerticalProgressBar Border Style")]
	[Category("VerticalProgressBar")]
	public GEnum2 GEnum2_0
	{
		get
		{
			return genum2_0;
		}
		set
		{
			genum2_0 = value;
			Invalidate();
		}
	}

	[Description("VerticalProgressBar Style")]
	[Category("VerticalProgressBar")]
	public GEnum1 GEnum1_0
	{
		get
		{
			return genum1_0;
		}
		set
		{
			genum1_0 = value;
			Invalidate();
		}
	}

	public VerticalProgressBar()
	{
		method_6();
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.UserPaint, value: true);
		SetStyle(ControlStyles.DoubleBuffer, value: true);
		base.Name = "VerticalProgressBar";
		base.Size = new Size(10, 120);
	}

	public void method_0()
	{
		int_0 += int_3;
		if (int_0 > int_2)
		{
			int_0 = int_2;
		}
		if (int_0 < int_1)
		{
			int_0 = int_1;
		}
		Invalidate();
	}

	public void method_1(int int_4)
	{
		int_0 += int_4;
		if (int_0 > int_2)
		{
			int_0 = int_2;
		}
		if (int_0 < int_1)
		{
			int_0 = int_1;
		}
		Invalidate();
	}

	private void method_2(Graphics graphics_0)
	{
		if (genum2_0 == GEnum2.Classic)
		{
			Color color = ControlPaint.Dark(BackColor);
			Color color2 = ControlPaint.Dark(BackColor);
			Pen pen = new Pen(color, 1f);
			graphics_0.DrawLine(pen, base.Width, 0, 0, 0);
			graphics_0.DrawLine(pen, 0, 0, 0, base.Height);
			pen = new Pen(color2, 1f);
			graphics_0.DrawLine(pen, 0, base.Height, base.Width, base.Height);
			graphics_0.DrawLine(pen, base.Width, base.Height, base.Width, 0);
		}
	}

	private void method_3(Graphics graphics_0)
	{
		if (int_1 == this.int_2 || int_0 - int_1 == 0)
		{
			return;
		}
		int int_;
		int int_2;
		int int_3;
		if (genum2_0 == GEnum2.None)
		{
			int_ = base.Width;
			int_2 = 0;
			int_3 = base.Height;
		}
		else
		{
			if (base.Width <= 4 && base.Height <= 2)
			{
				return;
			}
			int_ = base.Width - 4;
			int_2 = 2;
			int_3 = base.Height - 1;
		}
		int int_4 = (int_0 - int_1) * base.Height / (this.int_2 - int_1);
		if (genum1_0 == GEnum1.Solid)
		{
			method_4(graphics_0, int_2, int_3, int_, int_4);
		}
		if (genum1_0 == GEnum1.Classic)
		{
			method_5(graphics_0, int_2, int_3, int_, int_4);
		}
	}

	private void method_4(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
	{
		graphics_0.FillRectangle(new SolidBrush(color_0), int_4, int_5 - int_7, int_6, int_7);
	}

	private void method_5(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
	{
		int num = int_5 - int_7;
		int num2 = int_6 * 3 / 4;
		if (num2 > -1)
		{
			for (int num3 = int_5; num3 > num; num3 -= num2 + 1)
			{
				graphics_0.FillRectangle(new SolidBrush(color_0), int_4, num3 - num2, int_6, num2);
			}
		}
	}

	void Control.OnPaint(PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		method_3(graphics);
		method_2(graphics);
		base.OnPaint(e);
	}

	void Control.OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		Invalidate();
	}

	void ContainerControl.Dispose(bool disposing)
	{
		if (disposing && container_0 != null)
		{
			container_0.Dispose();
		}
		Dispose(disposing);
	}

	private void method_6()
	{
		container_0 = new Container();
	}
}
