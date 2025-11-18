using System;
using System.Runtime.InteropServices;

internal class Class9
{
	private readonly static uint[] uint_0;

	private readonly static uint[] uint_1;

	private readonly static uint[] uint_2;

	private readonly static ushort[] ushort_0;

	private readonly static byte[] byte_0;

	static Class9()
	{
		Class9.uint_0 = new uint[2048];
		Class9.uint_1 = new uint[64];
		Class9.uint_2 = new uint[64];
		Class9.ushort_0 = new ushort[512];
		Class9.byte_0 = new byte[512];
		Class9.uint_0[0] = 0;
		for (int i = 1; i < 1024; i++)
		{
			uint uInt32 = (uint)(i << 13);
			uint uInt321 = 0;
			while ((uInt32 & 8388608) == 0)
			{
				uInt321 -= 8388608;
				uInt32 <<= 1;
			}
			uInt32 &= 8388609;
			uInt321 += 947912704;
			Class9.uint_0[i] = uInt32 | uInt321;
		}
		for (int j = 1024; j < 2048; j++)
		{
			Class9.uint_0[j] = (uint)(939524096 + (j - 1024 << 13));
		}
		Class9.uint_1[0] = 0;
		for (int k = 1; k < 63; k++)
		{
			if (k < 31)
			{
				Class9.uint_1[k] = (uint)(k << 23);
			}
			else
			{
				Class9.uint_1[k] = (uint)(-2147483648 + (k - 32 << 23));
			}
		}
		Class9.uint_1[31] = 1199570944;
		Class9.uint_1[32] = -2147483648;
		Class9.uint_1[63] = 947912704;
		Class9.uint_2[0] = 0;
		for (int l = 1; l < 64; l++)
		{
			Class9.uint_2[l] = 1024;
		}
		Class9.uint_2[32] = 0;
		for (int m = 0; m < 256; m++)
		{
			int ınt32 = m - 127;
			if (ınt32 < -24)
			{
				Class9.ushort_0[m | 0] = 0;
				Class9.ushort_0[m | 256] = 32768;
				Class9.byte_0[m | 0] = 24;
				Class9.byte_0[m | 256] = 24;
			}
			else if (ınt32 < -14)
			{
				Class9.ushort_0[m | 0] = (ushort)(1024 >> (-ınt32 - 14 & 31 & 31));
				Class9.ushort_0[m | 256] = (ushort)(1024 >> (-ınt32 - 14 & 31 & 31) | 32768);
				Class9.byte_0[m | 0] = (byte)(-ınt32 - 1);
				Class9.byte_0[m | 256] = (byte)(-ınt32 - 1);
			}
			else if (ınt32 <= 15)
			{
				Class9.ushort_0[m | 0] = (ushort)(ınt32 + 15 << 10);
				Class9.ushort_0[m | 256] = (ushort)(ınt32 + 15 << 10 | 32768);
				Class9.byte_0[m | 0] = 13;
				Class9.byte_0[m | 256] = 13;
			}
			else if (ınt32 < 128)
			{
				Class9.ushort_0[m | 0] = 31744;
				Class9.ushort_0[m | 256] = 64512;
				Class9.byte_0[m | 0] = 24;
				Class9.byte_0[m | 256] = 24;
			}
			else
			{
				Class9.ushort_0[m | 0] = 31744;
				Class9.ushort_0[m | 256] = 64512;
				Class9.byte_0[m | 0] = 13;
				Class9.byte_0[m | 256] = 13;
			}
		}
	}

	public Class9()
	{
	}

	public static ushort smethod_0(float float_0)
	{
		Class9.Struct0 struct0 = new Class9.Struct0()
		{
			float_0 = float_0
		};
		return (ushort)(Class9.ushort_0[struct0.uint_0 >> 23 & 511] + ((struct0.uint_0 & 8388607) >> (Class9.byte_0[struct0.uint_0 >> 23 & 511] & 31 & 31)));
	}

	public static float smethod_1(ushort ushort_1)
	{
		Class9.Struct0 struct0 = new Class9.Struct0()
		{
			uint_0 = Class9.uint_0[checked((IntPtr)((ulong)Class9.uint_2[ushort_1 >> 10] + (long)(ushort_1 & 1023)))] + Class9.uint_1[ushort_1 >> 10]
		};
		return struct0.float_0;
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct Struct0
	{
		[FieldOffset(0)]
		public uint uint_0;

		[FieldOffset(0)]
		public float float_0;
	}
}