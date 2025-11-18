// Decompiled with JetBrains decompiler
// Type: VerticalProgressBar
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
[Description("Vertical Progress Bar")]
[ToolboxBitmap(typeof (ProgressBar))]
[Browsable(false)]
public sealed class VerticalProgressBar : UserControl
{
  private System.ComponentModel.Container container_0;
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
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.Name = nameof (VerticalProgressBar);
    this.Size = new Size(10, 120);
  }

  [Description("VerticalProgressBar Maximum Value")]
  [Category("VerticalProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  public int Int32_0
  {
    get => this.int_2;
    set
    {
      this.int_2 = value;
      if (this.int_2 < this.int_1)
        this.int_1 = this.int_2;
      if (this.int_2 < this.int_0)
        this.int_0 = this.int_2;
      this.Invalidate();
    }
  }

  [Description("VerticalProgressBar Minimum Value")]
  [Category("VerticalProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  public int Int32_1
  {
    get => this.int_1;
    set
    {
      this.int_1 = value;
      if (this.int_1 > this.int_2)
        this.int_2 = this.int_1;
      if (this.int_1 > this.int_0)
        this.int_0 = this.int_1;
      this.Invalidate();
    }
  }

  [Description("VerticalProgressBar Step")]
  [Category("VerticalProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  public int Int32_2
  {
    get => this.int_3;
    set => this.int_3 = value;
  }

  [Description("VerticalProgressBar Current Value")]
  [Category("VerticalProgressBar")]
  public int Int32_3
  {
    get => this.int_0;
    set
    {
      this.int_0 = value;
      if (this.int_0 > this.int_2)
        this.int_0 = this.int_2;
      if (this.int_0 < this.int_1)
        this.int_0 = this.int_1;
      this.Invalidate();
    }
  }

  [Description("VerticalProgressBar Color")]
  [Category("VerticalProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  public Color Color_0
  {
    get => this.color_0;
    set
    {
      this.color_0 = value;
      this.Invalidate();
    }
  }

  [Description("VerticalProgressBar Border Style")]
  [Category("VerticalProgressBar")]
  public GEnum2 GEnum2_0
  {
    get => this.genum2_0;
    set
    {
      this.genum2_0 = value;
      this.Invalidate();
    }
  }

  [Description("VerticalProgressBar Style")]
  [Category("VerticalProgressBar")]
  public GEnum1 GEnum1_0
  {
    get => this.genum1_0;
    set
    {
      this.genum1_0 = value;
      this.Invalidate();
    }
  }

  public void method_0()
  {
    this.int_0 += this.int_3;
    if (this.int_0 > this.int_2)
      this.int_0 = this.int_2;
    if (this.int_0 < this.int_1)
      this.int_0 = this.int_1;
    this.Invalidate();
  }

  public void method_1(int int_4)
  {
    this.int_0 += int_4;
    if (this.int_0 > this.int_2)
      this.int_0 = this.int_2;
    if (this.int_0 < this.int_1)
      this.int_0 = this.int_1;
    this.Invalidate();
  }

  private void method_2(Graphics graphics_0)
  {
    if (this.genum2_0 != GEnum2.Classic)
      return;
    Color color1 = ControlPaint.Dark(this.BackColor);
    Color color2 = ControlPaint.Dark(this.BackColor);
    Pen pen1 = new Pen(color1, 1f);
    graphics_0.DrawLine(pen1, this.Width, 0, 0, 0);
    graphics_0.DrawLine(pen1, 0, 0, 0, this.Height);
    Pen pen2 = new Pen(color2, 1f);
    graphics_0.DrawLine(pen2, 0, this.Height, this.Width, this.Height);
    graphics_0.DrawLine(pen2, this.Width, this.Height, this.Width, 0);
  }

  private void method_3(Graphics graphics_0)
  {
    if (this.int_1 == this.int_2 || this.int_0 - this.int_1 == 0)
      return;
    int int_6;
    int int_4;
    int int_5;
    if (this.genum2_0 == GEnum2.None)
    {
      int_6 = this.Width;
      int_4 = 0;
      int_5 = this.Height;
    }
    else
    {
      if (this.Width <= 4 && this.Height <= 2)
        return;
      int_6 = this.Width - 4;
      int_4 = 2;
      int_5 = this.Height - 1;
    }
    int int_7 = (this.int_0 - this.int_1) * this.Height / (this.int_2 - this.int_1);
    if (this.genum1_0 == GEnum1.Solid)
      this.method_4(graphics_0, int_4, int_5, int_6, int_7);
    if (this.genum1_0 != GEnum1.Classic)
      return;
    this.method_5(graphics_0, int_4, int_5, int_6, int_7);
  }

  private void method_4(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
  {
    graphics_0.FillRectangle((Brush) new SolidBrush(this.color_0), int_4, int_5 - int_7, int_6, int_7);
  }

  private void method_5(Graphics graphics_0, int int_4, int int_5, int int_6, int int_7)
  {
    int num = int_5 - int_7;
    int height = int_6 * 3 / 4;
    if (height <= -1)
      return;
    for (int index = int_5; index > num; index -= height + 1)
      graphics_0.FillRectangle((Brush) new SolidBrush(this.color_0), int_4, index - height, int_6, height);
  }

  virtual void Control.OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;
    this.method_3(graphics);
    this.method_2(graphics);
    // ISSUE: explicit non-virtual call
    __nonvirtual (((Control) this).OnPaint(e));
  }

  virtual void Control.OnSizeChanged(EventArgs e)
  {
    // ISSUE: explicit non-virtual call
    __nonvirtual (((Control) this).OnSizeChanged(e));
    this.Invalidate();
  }

  virtual void ContainerControl.Dispose(bool disposing)
  {
    if (disposing && this.container_0 != null)
      this.container_0.Dispose();
    // ISSUE: explicit non-virtual call
    __nonvirtual (((ContainerControl) this).Dispose(disposing));
  }

  private void method_6() => this.container_0 = new System.ComponentModel.Container();
}
