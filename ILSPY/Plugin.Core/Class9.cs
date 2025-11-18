using System.Runtime.InteropServices;

internal class Class9
{
	[StructLayout(LayoutKind.Explicit)]
	private struct Struct0
	{
		[FieldOffset(0)]
		public uint uint_0;

		[FieldOffset(0)]
		public float float_0;
	}

	private static readonly uint[] uint_0;

	private static readonly uint[] uint_1;

	private static readonly uint[] uint_2;

	private static readonly ushort[] ushort_0;

	private static readonly byte[] byte_0;

	static Class9()
	{
		uint_0 = new uint[2048];
		uint_1 = new uint[64];
		uint_2 = new uint[64];
		ushort_0 = new ushort[512];
		byte_0 = new byte[512];
		uint_0[0] = 0u;
		for (int i = 1; i < 1024; i++)
		{
			uint num = (uint)(i << 13);
			uint num2 = 0u;
			while ((num & 0x800000) == 0)
			{
				num2 -= 8388608;
				num <<= 1;
			}
			num &= 0x800001u;
			num2 += 947912704;
			uint_0[i] = num | num2;
		}
		for (int j = 1024; j < 2048; j++)
		{
			uint_0[j] = (uint)(939524096 + (j - 1024 << 13));
		}
		uint_1[0] = 0u;
		for (int k = 1; k < 63; k++)
		{
			if (k >= 31)
			{
				uint_1[k] = (uint)(int.MinValue + (k - 32 << 23));
			}
			else
			{
				uint_1[k] = (uint)(k << 23);
			}
		}
		uint_1[31] = 1199570944u;
		uint_1[32] = 2147483648u;
		uint_1[63] = 947912704u;
		uint_2[0] = 0u;
		for (int l = 1; l < 64; l++)
		{
			uint_2[l] = 1024u;
		}
		uint_2[32] = 0u;
		for (int m = 0; m < 256; m++)
		{
			int num3 = m - 127;
			if (num3 < -24)
			{
				ushort_0[m | 0] = 0;
				ushort_0[m | 0x100] = 32768;
				byte_0[m | 0] = 24;
				byte_0[m | 0x100] = 24;
			}
			else if (num3 < -14)
			{
				ushort_0[m | 0] = (ushort)(1024 >> ((-num3 - 14) & 0x1F));
				ushort_0[m | 0x100] = (ushort)((uint)(1024 >> ((-num3 - 14) & 0x1F)) | 0x8000u);
				byte_0[m | 0] = (byte)(-num3 - 1);
				byte_0[m | 0x100] = (byte)(-num3 - 1);
			}
			else if (num3 <= 15)
			{
				ushort_0[m | 0] = (ushort)(num3 + 15 << 10);
				ushort_0[m | 0x100] = (ushort)((uint)(num3 + 15 << 10) | 0x8000u);
				byte_0[m | 0] = 13;
				byte_0[m | 0x100] = 13;
			}
			else if (num3 >= 128)
			{
				ushort_0[m | 0] = 31744;
				ushort_0[m | 0x100] = 64512;
				byte_0[m | 0] = 13;
				byte_0[m | 0x100] = 13;
			}
			else
			{
				ushort_0[m | 0] = 31744;
				ushort_0[m | 0x100] = 64512;
				byte_0[m | 0] = 24;
				byte_0[m | 0x100] = 24;
			}
		}
	}

	public static ushort smethod_0(float float_0)
	{
		Struct0 @struct = default(Struct0);
		@struct.float_0 = float_0;
		Struct0 struct2 = @struct;
		return (ushort)(ushort_0[(struct2.uint_0 >> 23) & 0x1FF] + ((struct2.uint_0 & 0x7FFFFF) >> (byte_0[(struct2.uint_0 >> 23) & 0x1FF] & 0x1F)));
	}

	public static float smethod_1(ushort ushort_1)
	{
		Struct0 @struct = default(Struct0);
		@struct.uint_0 = uint_0[uint_2[ushort_1 >> 10] + (ushort_1 & 0x3FF)] + uint_1[ushort_1 >> 10];
		return @struct.float_0;
	}
}
