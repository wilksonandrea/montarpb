using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class GClass15 : ProgressBar
{
    private SolidBrush solidBrush_0 = ((SolidBrush) Brushes.Black);
    private SolidBrush solidBrush_1 = ((SolidBrush) Brushes.LightGreen);
    private GEnum0 genum0_0 = GEnum0.CurrProgress;
    private string string_0 = string.Empty;

    public GClass15()
    {
        base.Value = base.Minimum;
        this.method_0();
    }

    private void method_0()
    {
        base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
    }

    private void method_1(Graphics graphics_0)
    {
        Rectangle clientRectangle = base.ClientRectangle;
        ProgressBarRenderer.DrawHorizontalBar(graphics_0, clientRectangle);
        clientRectangle.Inflate(-3, -3);
        if (base.Value > 0)
        {
            Rectangle rect = new Rectangle(clientRectangle.X, clientRectangle.Y, (int) Math.Round((double) ((((float) base.Value) / ((float) base.Maximum)) * clientRectangle.Width)), clientRectangle.Height);
            graphics_0.FillRectangle(this.solidBrush_1, rect);
        }
    }

    private void method_2(Graphics graphics_0)
    {
        if (this.GEnum0_0 != GEnum0.NoText)
        {
            string text = this.String_1;
            SizeF ef = graphics_0.MeasureString(text, this.Font_0);
            Point point = new Point((base.Width / 2) - (((int) ef.Width) / 2), (base.Height / 2) - (((int) ef.Height) / 2));
            graphics_0.DrawString(text, this.Font_0, this.solidBrush_0, (PointF) point);
        }
    }

    public void method_3()
    {
        this.solidBrush_0.Dispose();
        this.solidBrush_1.Dispose();
        base.Dispose();
    }

    override void Control.OnPaint(PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        this.method_1(graphics);
        this.method_2(graphics);
    }

    [Description("Font of the text on ProgressBar"), Category("Additional Options")]
    public Font Font_0 { get; set; }

    [Category("Additional Options")]
    public Color Color_0
    {
        get => 
            this.solidBrush_0.Color;
        set
        {
            this.solidBrush_0.Dispose();
            this.solidBrush_0 = new SolidBrush(value);
        }
    }

    [Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
    public Color Color_1
    {
        get => 
            this.solidBrush_1.Color;
        set
        {
            this.solidBrush_1.Dispose();
            this.solidBrush_1 = new SolidBrush(value);
        }
    }

    [Category("Additional Options"), Browsable(true)]
    public GEnum0 GEnum0_0
    {
        get => 
            this.genum0_0;
        set
        {
            this.genum0_0 = value;
            base.Invalidate();
        }
    }

    [Description("If it's empty, % will be shown"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
    public string String_0
    {
        get => 
            this.string_0;
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
            string str = this.String_0;
            switch (this.GEnum0_0)
            {
                case GEnum0.Percentage:
                    str = this.String_2;
                    break;

                case GEnum0.CurrProgress:
                    str = this.String_3;
                    break;

                case GEnum0.TextAndPercentage:
                    str = this.String_0 + ": " + this.String_2;
                    break;

                case GEnum0.TextAndCurrProgress:
                    str = this.String_0 + ": " + this.String_3;
                    break;

                default:
                    break;
            }
            return str;
        }
        set
        {
        }
    }

    private string String_2 =>
        $"{(((float) (base.Value - base.Minimum)) / (base.Maximum - base.Minimum)) * 100f} %";

    private string String_3 =>
        $"{base.Value}/{base.Maximum}";

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

