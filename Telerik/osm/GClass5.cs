using System;
using System.Drawing;
using System.Drawing.Imaging;

public class GClass5
{
	protected static GClass5 gclass5_0;

	static GClass5()
	{
		GClass5.gclass5_0 = new GClass5();
	}

	public GClass5()
	{
	}

	public Bitmap method_0(Image image_0, float float_0)
	{
		Bitmap bitmap = new Bitmap(image_0.Width, image_0.Height);
		ImageAttributes ımageAttribute = new ImageAttributes();
		using (Graphics graphic = Graphics.FromImage(bitmap))
		{
			ımageAttribute.SetColorMatrix(new ColorMatrix()
			{
				Matrix33 = float_0
			}, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			graphic.DrawImage(image_0, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image_0.Width, image_0.Height, GraphicsUnit.Pixel, ımageAttribute);
			graphic.Dispose();
		}
		return bitmap;
	}

	public static GClass5 smethod_0()
	{
		return GClass5.gclass5_0;
	}
}