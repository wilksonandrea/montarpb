// Decompiled with JetBrains decompiler
// Type: GClass5
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System.Drawing;
using System.Drawing.Imaging;

#nullable disable
public class GClass5
{
  protected static GClass5 gclass5_0 = new GClass5();

  public static GClass5 smethod_0() => GClass5.gclass5_0;

  public Bitmap method_0(Image image_0, float float_0)
  {
    Bitmap bitmap = new Bitmap(image_0.Width, image_0.Height);
    ImageAttributes imageAttr = new ImageAttributes();
    using (Graphics graphics = Graphics.FromImage((Image) bitmap))
    {
      imageAttr.SetColorMatrix(new ColorMatrix()
      {
        Matrix33 = float_0
      }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      graphics.DrawImage(image_0, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image_0.Width, image_0.Height, GraphicsUnit.Pixel, imageAttr);
      graphics.Dispose();
    }
    return bitmap;
  }
}
