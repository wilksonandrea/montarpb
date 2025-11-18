using System;
using System.Runtime.InteropServices;

internal class Class9
{
    private static readonly uint[] uint_0 = new uint[0x800];
    private static readonly uint[] uint_1 = new uint[0x40];
    private static readonly uint[] uint_2 = new uint[0x40];
    private static readonly ushort[] ushort_0 = new ushort[0x200];
    private static readonly byte[] byte_0 = new byte[0x200];

    static Class9()
    {
        uint_0[0] = 0;
        int index = 1;
        while (index < 0x400)
        {
            uint num2 = (uint) (index << 13);
            uint num3 = 0;
            while (true)
            {
                if ((num2 & 0x800000) != 0)
                {
                    uint_0[index] = (num2 & 0x800001) | (num3 + 0x38800000);
                    index++;
                    break;
                }
                num3 -= 0x800000;
                num2 = num2 << 1;
            }
        }
        for (int i = 0x400; i < 0x800; i++)
        {
            uint_0[i] = (uint) (0x38000000 + ((i - 0x400) << 13));
        }
        uint_1[0] = 0;
        for (int j = 1; j < 0x3f; j++)
        {
            uint_1[j] = (j < 0x1f) ? ((uint) (j << 0x17)) : ((uint) (-2147483648 + ((j - 0x20) << 0x17)));
        }
        uint_1[0x1f] = 0x47800000;
        uint_1[0x20] = 0x80000000;
        uint_1[0x3f] = 0x38800000;
        uint_2[0] = 0;
        for (int k = 1; k < 0x40; k++)
        {
            uint_2[k] = 0x400;
        }
        uint_2[0x20] = 0;
        for (int m = 0; m < 0x100; m++)
        {
            int num8 = m - 0x7f;
            if (num8 < -24)
            {
                ushort_0[m | 0] = 0;
                ushort_0[m | 0x100] = 0x8000;
                byte_0[m | 0] = 0x18;
                byte_0[m | 0x100] = 0x18;
            }
            else if (num8 < -14)
            {
                ushort_0[m | 0] = (ushort) (0x400 >> ((-num8 - 14) & 0x1f));
                ushort_0[m | 0x100] = (ushort) ((0x400 >> ((-num8 - 14) & 0x1f)) | 0x8000);
                byte_0[m | 0] = (byte) (-num8 - 1);
                byte_0[m | 0x100] = (byte) (-num8 - 1);
            }
            else if (num8 <= 15)
            {
                ushort_0[m | 0] = (ushort) ((num8 + 15) << 10);
                ushort_0[m | 0x100] = (ushort) (((num8 + 15) << 10) | 0x8000);
                byte_0[m | 0] = 13;
                byte_0[m | 0x100] = 13;
            }
            else if (num8 >= 0x80)
            {
                ushort_0[m | 0] = 0x7c00;
                ushort_0[m | 0x100] = 0xfc00;
                byte_0[m | 0] = 13;
                byte_0[m | 0x100] = 13;
            }
            else
            {
                ushort_0[m | 0] = 0x7c00;
                ushort_0[m | 0x100] = 0xfc00;
                byte_0[m | 0] = 0x18;
                byte_0[m | 0x100] = 0x18;
            }
        }
    }

    public static ushort smethod_0(float float_0)
    {
        Struct0 struct2 = new Struct0 {
            float_0 = float_0
        };
        return (ushort) (ushort_0[((int) (struct2.uint_0 >> 0x17)) & 0x1ff] + ((struct2.uint_0 & 0x7fffff) >> (byte_0[((int) (struct2.uint_0 >> 0x17)) & 0x1ff] & 0x1f)));
    }

    public static float smethod_1(ushort ushort_1)
    {
        Struct0 struct2 = new Struct0 {
            uint_0 = uint_0[(int) ((IntPtr) (uint_2[ushort_1 >> 10] + (ushort_1 & 0x3ff)))] + uint_1[ushort_1 >> 10]
        };
        return struct2.float_0;
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

