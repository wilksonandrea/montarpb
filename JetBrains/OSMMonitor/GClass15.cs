// Decompiled with JetBrains decompiler
// Type: GClass15
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
public class GClass15 : ProgressBar
{
  private SolidBrush solidBrush_0 = (SolidBrush) Brushes.Black;
  private SolidBrush solidBrush_1 = (SolidBrush) Brushes.LightGreen;
  private GClass15.GEnum0 genum0_0 = GClass15.GEnum0.CurrProgress;
  private string string_0 = string.Empty;

  [Description("Font of the text on ProgressBar")]
  [Category("Additional Options")]
  public Font Font_0 { get; set; } = new Font(FontFamily.GenericSerif, 11f, FontStyle.Bold | FontStyle.Italic);

  [Category("Additional Options")]
  public Color Color_0
  {
    get => this.solidBrush_0.Color;
    set
    {
      this.solidBrush_0.Dispose();
      this.solidBrush_0 = new SolidBrush(value);
    }
  }

  [Category("Additional Options")]
  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  public Color Color_1
  {
    get => this.solidBrush_1.Color;
    set
    {
      this.solidBrush_1.Dispose();
      this.solidBrush_1 = new SolidBrush(value);
    }
  }

  [Category("Additional Options")]
  [Browsable(true)]
  public GClass15.GEnum0 GEnum0_0
  {
    get => this.genum0_0;
    set
    {
      this.genum0_0 = value;
      this.Invalidate();
    }
  }

  [Description("If it's empty, % will be shown")]
  [Category("Additional Options")]
  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  public string String_0
  {
    get => this.string_0;
    set
    {
      this.string_0 = value;
      this.Invalidate();
    }
  }

  private string String_1
  {
    get
    {
      string string1 = this.String_0;
      switch (this.GEnum0_0)
      {
        case GClass15.GEnum0.Percentage:
          string1 = this.String_2;
          break;
        case GClass15.GEnum0.CurrProgress:
          string1 = this.String_3;
          break;
        case GClass15.GEnum0.TextAndPercentage:
          string1 = $"{this.String_0}: {this.String_2}";
          break;
        case GClass15.GEnum0.TextAndCurrProgress:
          string1 = $"{this.String_0}: {this.String_3}";
          break;
      }
      return string1;
    }
    set
    {
    }
  }

  private string String_2
  {
    get
    {
      return $"{(ValueType) (float) ((double) (int) ((double) this.Value - (double) this.Minimum) / ((double) this.Maximum - (double) this.Minimum) * 100.0)} %";
    }
  }

  private string String_3 => $"{this.Value}/{this.Maximum}";

  public GClass15()
  {
    this.Value = this.Minimum;
    this.method_0();
  }

  private void method_0()
  {
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
  }

  virtual void Control.OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;
    this.method_1(graphics);
    this.method_2(graphics);
  }

  private void method_1(Graphics graphics_0)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    ProgressBarRenderer.DrawHorizontalBar(graphics_0, clientRectangle);
    clientRectangle.Inflate(-3, -3);
    if (this.Value <= 0)
      return;
    Rectangle rect = new Rectangle(clientRectangle.X, clientRectangle.Y, (int) Math.Round((double) this.Value / (double) this.Maximum * (double) clientRectangle.Width), clientRectangle.Height);
    graphics_0.FillRectangle((Brush) this.solidBrush_1, rect);
  }

  private void method_2(Graphics graphics_0)
  {
    if (this.GEnum0_0 == GClass15.GEnum0.NoText)
      return;
    string string1 = this.String_1;
    SizeF sizeF = graphics_0.MeasureString(string1, this.Font_0);
    Point point = new Point(this.Width / 2 - (int) sizeF.Width / 2, this.Height / 2 - (int) sizeF.Height / 2);
    graphics_0.DrawString(string1, this.Font_0, (Brush) this.solidBrush_0, (PointF) point);
  }

  public void method_3()
  {
    this.solidBrush_0.Dispose();
    this.solidBrush_1.Dispose();
    this.Dispose();
  }

  public enum GEnum0
  {
    NoText,
    Percentage,
    CurrProgress,
    CustomText,
    TextAndPercentage,
    TextAndCurrProgress,
  }
}
