using System;
using System.Drawing;
using System.Drawing.Imaging;

public class GClass5
{
    protected static GClass5 gclass5_0 = new GClass5();

    public Bitmap method_0(Image image_0, float float_0)
    {
        Bitmap image = new Bitmap(image_0.Width, image_0.Height);
        ImageAttributes imageAttr = new ImageAttributes();
        using (Graphics graphics = Graphics.FromImage(image))
        {
            ColorMatrix newColorMatrix = new ColorMatrix();
            newColorMatrix.Matrix33 = float_0;
            imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(image_0, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image_0.Width, image_0.Height, GraphicsUnit.Pixel, imageAttr);
            graphics.Dispose();
        }
        return image;
    }

    public static GClass5 smethod_0() => 
        gclass5_0;
}

