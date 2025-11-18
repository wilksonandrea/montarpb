using System;
using System.Runtime.InteropServices;

// Token: 0x0200003C RID: 60
internal class Class9
{
	// Token: 0x0600020C RID: 524 RVA: 0x00018F84 File Offset: 0x00017184
	static Class9()
	{
		Class9.uint_0[0] = 0U;
		for (int i = 1; i < 1024; i++)
		{
			uint num = (uint)((uint)i << 13);
			uint num2 = 0U;
			while ((num & 8388608U) == 0U)
			{
				num2 -= 8388608U;
				num <<= 1;
			}
			num &= 8388609U;
			num2 += 947912704U;
			Class9.uint_0[i] = num | num2;
		}
		for (int j = 1024; j < 2048; j++)
		{
			Class9.uint_0[j] = (uint)(939524096 + (j - 1024 << 13));
		}
		Class9.uint_1[0] = 0U;
		for (int k = 1; k < 63; k++)
		{
			if (k >= 31)
			{
				Class9.uint_1[k] = (uint)(int.MinValue + (k - 32 << 23));
			}
			else
			{
				Class9.uint_1[k] = (uint)((uint)k << 23);
			}
		}
		Class9.uint_1[31] = 1199570944U;
		Class9.uint_1[32] = 2147483648U;
		Class9.uint_1[63] = 947912704U;
		Class9.uint_2[0] = 0U;
		for (int l = 1; l < 64; l++)
		{
			Class9.uint_2[l] = 1024U;
		}
		Class9.uint_2[32] = 0U;
		for (int m = 0; m < 256; m++)
		{
			int num3 = m - 127;
			if (num3 < -24)
			{
				Class9.ushort_0[m | 0] = 0;
				Class9.ushort_0[m | 256] = 32768;
				Class9.byte_0[m | 0] = 24;
				Class9.byte_0[m | 256] = 24;
			}
			else if (num3 < -14)
			{
				Class9.ushort_0[m | 0] = (ushort)(1024 >> -num3 - 14);
				Class9.ushort_0[m | 256] = (ushort)((1024 >> -num3 - 14) | 32768);
				Class9.byte_0[m | 0] = (byte)(-num3 - 1);
				Class9.byte_0[m | 256] = (byte)(-num3 - 1);
			}
			else if (num3 <= 15)
			{
				Class9.ushort_0[m | 0] = (ushort)(num3 + 15 << 10);
				Class9.ushort_0[m | 256] = (ushort)((num3 + 15 << 10) | 32768);
				Class9.byte_0[m | 0] = 13;
				Class9.byte_0[m | 256] = 13;
			}
			else if (num3 >= 128)
			{
				Class9.ushort_0[m | 0] = 31744;
				Class9.ushort_0[m | 256] = 64512;
				Class9.byte_0[m | 0] = 13;
				Class9.byte_0[m | 256] = 13;
			}
			else
			{
				Class9.ushort_0[m | 0] = 31744;
				Class9.ushort_0[m | 256] = 64512;
				Class9.byte_0[m | 0] = 24;
				Class9.byte_0[m | 256] = 24;
			}
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x000192A4 File Offset: 0x000174A4
	public static ushort smethod_0(float float_0)
	{
		Class9.Struct0 @struct = new Class9.Struct0
		{
			float_0 = float_0
		};
		return (ushort)((uint)Class9.ushort_0[(int)((@struct.uint_0 >> 23) & 511U)] + ((@struct.uint_0 & 8388607U) >> (int)Class9.byte_0[(int)((@struct.uint_0 >> 23) & 511U)]));
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00019304 File Offset: 0x00017504
	public static float smethod_1(ushort ushort_1)
	{
		return new Class9.Struct0
		{
			uint_0 = Class9.uint_0[(int)(checked((IntPtr)(unchecked((ulong)Class9.uint_2[ushort_1 >> 10] + (ulong)((long)(ushort_1 & 1023))))))] + Class9.uint_1[ushort_1 >> 10]
		}.float_0;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00002116 File Offset: 0x00000316
	public Class9()
	{
	}

	// Token: 0x040000B4 RID: 180
	private static readonly uint[] uint_0 = new uint[2048];

	// Token: 0x040000B5 RID: 181
	private static readonly uint[] uint_1 = new uint[64];

	// Token: 0x040000B6 RID: 182
	private static readonly uint[] uint_2 = new uint[64];

	// Token: 0x040000B7 RID: 183
	private static readonly ushort[] ushort_0 = new ushort[512];

	// Token: 0x040000B8 RID: 184
	private static readonly byte[] byte_0 = new byte[512];

	// Token: 0x0200003D RID: 61
	[StructLayout(LayoutKind.Explicit)]
	private struct Struct0
	{
		// Token: 0x040000B9 RID: 185
		[FieldOffset(0)]
		public uint uint_0;

		// Token: 0x040000BA RID: 186
		[FieldOffset(0)]
		public float float_0;
	}
}
