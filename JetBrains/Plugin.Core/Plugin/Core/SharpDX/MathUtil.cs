// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SharpDX.MathUtil
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.SharpDX;

public static class MathUtil
{
  public const float ZeroTolerance = 1E-06f;
  public const float Pi = 3.14159274f;
  public const float TwoPi = 6.28318548f;
  public const float PiOverTwo = 1.57079637f;
  public const float PiOverFour = 0.7853982f;

  public override bool Equals(object left)
  {
    // ISSUE: explicit non-virtual call
    return left is Half3 half3 && __nonvirtual (((Class9) this).Equals(half3));
  }

  static MathUtil()
  {
    Class9.uint_0 = new uint[2048 /*0x0800*/];
    Class9.uint_1 = new uint[64 /*0x40*/];
    Class9.uint_2 = new uint[64 /*0x40*/];
    Class9.ushort_0 = new ushort[512 /*0x0200*/];
    Class9.byte_0 = new byte[512 /*0x0200*/];
    Class9.uint_0[0] = 0U;
    for (int index = 1; index < 1024 /*0x0400*/; ++index)
    {
      uint num1 = (uint) (index << 13);
      uint num2 = 0;
      for (; ((int) num1 & 8388608 /*0x800000*/) == 0; num1 <<= 1)
        num2 -= 8388608U /*0x800000*/;
      uint num3 = num1 & 8388609U /*0x800001*/;
      uint num4 = num2 + 947912704U /*0x38800000*/;
      Class9.uint_0[index] = num3 | num4;
    }
    for (int index = 1024 /*0x0400*/; index < 2048 /*0x0800*/; ++index)
      Class9.uint_0[index] = (uint) (939524096 /*0x38000000*/ + (index - 1024 /*0x0400*/ << 13));
    Class9.uint_1[0] = 0U;
    for (int index = 1; index < 63 /*0x3F*/; ++index)
      Class9.uint_1[index] = index < 31 /*0x1F*/ ? (uint) (index << 23) : (uint) (int.MinValue + (index - 32 /*0x20*/ << 23));
    Class9.uint_1[31 /*0x1F*/] = 1199570944U /*0x47800000*/;
    Class9.uint_1[32 /*0x20*/] = 2147483648U /*0x80000000*/;
    Class9.uint_1[63 /*0x3F*/] = 947912704U /*0x38800000*/;
    Class9.uint_2[0] = 0U;
    for (int index = 1; index < 64 /*0x40*/; ++index)
      Class9.uint_2[index] = 1024U /*0x0400*/;
    Class9.uint_2[32 /*0x20*/] = 0U;
    for (int index = 0; index < 256 /*0x0100*/; ++index)
    {
      int num = index - (int) sbyte.MaxValue;
      if (num < -24)
      {
        Class9.ushort_0[index | 0] = (ushort) 0;
        Class9.ushort_0[index | 256 /*0x0100*/] = (ushort) 32768 /*0x8000*/;
        Class9.byte_0[index | 0] = (byte) 24;
        Class9.byte_0[index | 256 /*0x0100*/] = (byte) 24;
      }
      else if (num < -14)
      {
        Class9.ushort_0[index | 0] = (ushort) (1024 /*0x0400*/ >> -num - 14);
        Class9.ushort_0[index | 256 /*0x0100*/] = (ushort) (1024 /*0x0400*/ >> -num - 14 | 32768 /*0x8000*/);
        Class9.byte_0[index | 0] = (byte) (-num - 1);
        Class9.byte_0[index | 256 /*0x0100*/] = (byte) (-num - 1);
      }
      else if (num <= 15)
      {
        Class9.ushort_0[index | 0] = (ushort) (num + 15 << 10);
        Class9.ushort_0[index | 256 /*0x0100*/] = (ushort) (num + 15 << 10 | 32768 /*0x8000*/);
        Class9.byte_0[index | 0] = (byte) 13;
        Class9.byte_0[index | 256 /*0x0100*/] = (byte) 13;
      }
      else if (num >= 128 /*0x80*/)
      {
        Class9.ushort_0[index | 0] = (ushort) 31744;
        Class9.ushort_0[index | 256 /*0x0100*/] = (ushort) 64512;
        Class9.byte_0[index | 0] = (byte) 13;
        Class9.byte_0[index | 256 /*0x0100*/] = (byte) 13;
      }
      else
      {
        Class9.ushort_0[index | 0] = (ushort) 31744;
        Class9.ushort_0[index | 256 /*0x0100*/] = (ushort) 64512;
        Class9.byte_0[index | 0] = (byte) 24;
        Class9.byte_0[index | 256 /*0x0100*/] = (byte) 24;
      }
    }
  }

  public static ushort smethod_0([In] float obj0)
  {
    Class9.Struct0 struct0 = new Class9.Struct0()
    {
      float_0 = obj0
    };
    return (ushort) ((uint) Class9.ushort_0[(int) (struct0.uint_0 >> 23) & 511 /*0x01FF*/] + ((struct0.uint_0 & 8388607U /*0x7FFFFF*/) >> (int) Class9.byte_0[(int) (struct0.uint_0 >> 23) & 511 /*0x01FF*/]));
  }
}
