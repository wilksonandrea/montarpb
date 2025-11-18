using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

[Description("Vertical Progress Bar"), ToolboxBitmap(typeof(ProgressBar)), Browsable(false)]
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

    public VerticalProgressBar()
    {
        this.method_6();
        base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        base.SetStyle(ControlStyles.UserPaint, true);
        base.SetStyle(ControlStyles.DoubleBuffer, true);
        base.Name = "VerticalProgressBar";
        base.Size = new Size(10, 120);
    }

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

    private void method_2(Graphics graphics_0)
    {
        if (this.genum2_0 == GEnum2.Classic)
        {
            Color color = ControlPaint.Dark(this.BackColor);
            Pen pen = new Pen(ControlPaint.Dark(this.BackColor), 1f);
            graphics_0.DrawLine(pen, base.Width, 0, 0, 0);
            graphics_0.DrawLine(pen, 0, 0, 0, base.Height);
            pen = new Pen(color, 1f);
            graphics_0.DrawLine(pen, 0, base.Height, base.Width, base.Height);
            graphics_0.DrawLine(pen, base.Width, base.Height, base.Width, 0);
        }
    }

    private void method_3(Graphics graphics_0)
    {
        if ((this.int_1 != this.int_2) && ((this.int_0 - this.int_1) != 0))
        {
            int width;
            int num3;
            int height;
            if (this.genum2_0 == GEnum2.None)
            {
                width = base.Width;
                num3 = 0;
                height = base.Height;
            }
            else
            {
                if ((base.Width <= 4) && (base.Height <= 2))
                {
                    return;
                }
                width = base.Width - 4;
                num3 = 2;
                height = base.Height - 1;
            }
            int num2 = ((this.int_0 - this.int_1) * base.Height) / (this.int_2 - this.int_1);
            if (this.genum1_0 == GEnum1.Solid)
            {
                this.method_4(graphics_0, num3, height, width, num2);
            }
            if (this.genum1_0 == GEnum1.Classic)
            {
                this.method_5(graphics_0, num3, height, width, num2);
            }
        }
    }

    private void method_4(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
    {
        graphics_0.FillRectangle(new SolidBrush(this.color_0), int_4, int_5 - int_7, int_6, int_7);
    }

    private void method_5(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
    {
        int num = int_5 - int_7;
        int height = (int_6 * 3) / 4;
        if (height > -1)
        {
            for (int i = int_5; i > num; i -= height + 1)
            {
                graphics_0.FillRectangle(new SolidBrush(this.color_0), int_4, i - height, int_6, height);
            }
        }
    }

    private void method_6()
    {
        this.container_0 = new Container();
    }

    override void ContainerControl.Dispose(bool disposing)
    {
        if (disposing && (this.container_0 != null))
        {
            this.container_0.Dispose();
        }
        base.Dispose(disposing);
    }

    override void Control.OnPaint(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        this.method_3(graphics);
        this.method_2(graphics);
        base.OnPaint(e);
    }

    override void Control.OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        base.Invalidate();
    }

    [Description("VerticalProgressBar Maximum Value"), Category("VerticalProgressBar"), RefreshProperties(RefreshProperties.All)]
    public int Int32_0
    {
        get => 
            this.int_2;
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

    [Description("VerticalProgressBar Minimum Value"), Category("VerticalProgressBar"), RefreshProperties(RefreshProperties.All)]
    public int Int32_1
    {
        get => 
            this.int_1;
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

    [Description("VerticalProgressBar Step"), Category("VerticalProgressBar"), RefreshProperties(RefreshProperties.All)]
    public int Int32_2
    {
        get => 
            this.int_3;
        set => 
            this.int_3 = value;
    }

    [Description("VerticalProgressBar Current Value"), Category("VerticalProgressBar")]
    public int Int32_3
    {
        get => 
            this.int_0;
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

    [Description("VerticalProgressBar Color"), Category("VerticalProgressBar"), RefreshProperties(RefreshProperties.All)]
    public Color Color_0
    {
        get => 
            this.color_0;
        set
        {
            this.color_0 = value;
            base.Invalidate();
        }
    }

    [Description("VerticalProgressBar Border Style"), Category("VerticalProgressBar")]
    public GEnum2 GEnum2_0
    {
        get => 
            this.genum2_0;
        set
        {
            this.genum2_0 = value;
            base.Invalidate();
        }
    }

    [Description("VerticalProgressBar Style"), Category("VerticalProgressBar")]
    public GEnum1 GEnum1_0
    {
        get => 
            this.genum1_0;
        set
        {
            this.genum1_0 = value;
            base.Invalidate();
        }
    }
}

