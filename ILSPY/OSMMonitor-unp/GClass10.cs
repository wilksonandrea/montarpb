using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class GClass10
{
	private enum Enum3 : long
	{
		PRF_CHECKVISIBLE = 1L,
		PRF_NONCLIENT = 2L,
		PRF_CLIENT = 4L,
		PRF_ERASEBKGND = 8L,
		PRF_CHILDREN = 0x10L,
		PRF_OWNED = 0x20L
	}

	public const int int_0 = 512;

	public const int int_1 = 513;

	public const int int_2 = 514;

	public const int int_3 = 516;

	public const int int_4 = 515;

	public const int int_5 = 675;

	public const int int_6 = 15;

	public const int int_7 = 20;

	public const int int_8 = 791;

	public const int int_9 = 276;

	public const int int_10 = 277;

	public const int int_11 = 176;

	public const int int_12 = 187;

	public const int int_13 = 201;

	public const int int_14 = 214;

	public const int int_15 = 792;

	public const long long_0 = 1L;

	public const long long_1 = 2L;

	public const long long_2 = 4L;

	public const long long_3 = 8L;

	public const long long_4 = 16L;

	public const long long_5 = 32L;

	[DllImport("USER32.DLL")]
	public static extern bool PostMessage(IntPtr intptr_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2);

	[DllImport("USER32.DLL")]
	public static extern int SendMessage(IntPtr intptr_0, int int_16, IntPtr intptr_1, IntPtr intptr_2);

	[DllImport("USER32.DLL")]
	public static extern uint GetCaretBlinkTime();

	public static bool smethod_0(Control control_0, ref Bitmap bitmap_0)
	{
		Graphics graphics = Graphics.FromImage(bitmap_0);
		int value = 12;
		IntPtr intptr_ = new IntPtr(value);
		IntPtr hdc = graphics.GetHdc();
		SendMessage(control_0.Handle, 791, hdc, intptr_);
		graphics.ReleaseHdc(hdc);
		graphics.Dispose();
		return true;
	}
}
