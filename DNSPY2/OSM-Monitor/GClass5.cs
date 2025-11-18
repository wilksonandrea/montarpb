using System;
using System.Drawing;
using System.Drawing.Imaging;

// Token: 0x02000019 RID: 25
public class GClass5
{
	// Token: 0x0600007D RID: 125 RVA: 0x00002133 File Offset: 0x00000333
	public GClass5()
	{
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000023F5 File Offset: 0x000005F5
	public static GClass5 smethod_0()
	{
		return GClass5.gclass5_0;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004AF8 File Offset: 0x00002CF8
	public Bitmap method_0(Image image_0, float float_0)
	{
		Bitmap bitmap = new Bitmap(image_0.Width, image_0.Height);
		ImageAttributes imageAttributes = new ImageAttributes();
		using (Graphics graphics = Graphics.FromImage(bitmap))
		{
			imageAttributes.SetColorMatrix(new ColorMatrix
			{
				Matrix33 = float_0
			}, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			graphics.DrawImage(image_0, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image_0.Width, image_0.Height, GraphicsUnit.Pixel, imageAttributes);
			graphics.Dispose();
		}
		return bitmap;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000023FC File Offset: 0x000005FC
	// Note: this type is marked as 'beforefieldinit'.
	static GClass5()
	{
	}

	// Token: 0x0400004A RID: 74
	protected static GClass5 gclass5_0 = new GClass5();
}
