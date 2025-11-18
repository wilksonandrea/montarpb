using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Token: 0x02000020 RID: 32
public class GClass10
{
	// Token: 0x0600009B RID: 155
	[DllImport("USER32.DLL")]
	public static extern bool PostMessage(IntPtr intptr_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2);

	// Token: 0x0600009C RID: 156
	[DllImport("USER32.DLL")]
	public static extern int SendMessage(IntPtr intptr_0, int int_16, IntPtr intptr_1, IntPtr intptr_2);

	// Token: 0x0600009D RID: 157
	[DllImport("USER32.DLL")]
	public static extern uint GetCaretBlinkTime();

	// Token: 0x0600009E RID: 158 RVA: 0x00005228 File Offset: 0x00003428
	public static bool smethod_0(Control control_0, ref Bitmap bitmap_0)
	{
		Graphics graphics = Graphics.FromImage(bitmap_0);
		int num = 12;
		IntPtr intPtr = new IntPtr(num);
		IntPtr hdc = graphics.GetHdc();
		GClass10.SendMessage(control_0.Handle, 791, hdc, intPtr);
		graphics.ReleaseHdc(hdc);
		graphics.Dispose();
		return true;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00002133 File Offset: 0x00000333
	public GClass10()
	{
	}

	// Token: 0x04000082 RID: 130
	public const int int_0 = 512;

	// Token: 0x04000083 RID: 131
	public const int int_1 = 513;

	// Token: 0x04000084 RID: 132
	public const int int_2 = 514;

	// Token: 0x04000085 RID: 133
	public const int int_3 = 516;

	// Token: 0x04000086 RID: 134
	public const int int_4 = 515;

	// Token: 0x04000087 RID: 135
	public const int int_5 = 675;

	// Token: 0x04000088 RID: 136
	public const int int_6 = 15;

	// Token: 0x04000089 RID: 137
	public const int int_7 = 20;

	// Token: 0x0400008A RID: 138
	public const int int_8 = 791;

	// Token: 0x0400008B RID: 139
	public const int int_9 = 276;

	// Token: 0x0400008C RID: 140
	public const int int_10 = 277;

	// Token: 0x0400008D RID: 141
	public const int int_11 = 176;

	// Token: 0x0400008E RID: 142
	public const int int_12 = 187;

	// Token: 0x0400008F RID: 143
	public const int int_13 = 201;

	// Token: 0x04000090 RID: 144
	public const int int_14 = 214;

	// Token: 0x04000091 RID: 145
	public const int int_15 = 792;

	// Token: 0x04000092 RID: 146
	public const long long_0 = 1L;

	// Token: 0x04000093 RID: 147
	public const long long_1 = 2L;

	// Token: 0x04000094 RID: 148
	public const long long_2 = 4L;

	// Token: 0x04000095 RID: 149
	public const long long_3 = 8L;

	// Token: 0x04000096 RID: 150
	public const long long_4 = 16L;

	// Token: 0x04000097 RID: 151
	public const long long_5 = 32L;

	// Token: 0x02000021 RID: 33
	private enum Enum3 : long
	{
		// Token: 0x04000099 RID: 153
		PRF_CHECKVISIBLE = 1L,
		// Token: 0x0400009A RID: 154
		PRF_NONCLIENT,
		// Token: 0x0400009B RID: 155
		PRF_CLIENT = 4L,
		// Token: 0x0400009C RID: 156
		PRF_ERASEBKGND = 8L,
		// Token: 0x0400009D RID: 157
		PRF_CHILDREN = 16L,
		// Token: 0x0400009E RID: 158
		PRF_OWNED = 32L
	}
}
