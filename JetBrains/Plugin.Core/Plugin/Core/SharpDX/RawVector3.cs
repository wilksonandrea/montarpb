// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SharpDX.RawVector3
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

#nullable disable
namespace Plugin.Core.SharpDX;

public struct RawVector3
{
  public float X;
  public float Y;
  public float Z;

  public static float smethod_1(ushort value1)
  {
    return new Class9.Struct0()
    {
      uint_0 = (Class9.uint_0[(long) Class9.uint_2[(int) value1 >> 10] + (long) ((int) value1 & 1023 /*0x03FF*/)] + Class9.uint_1[(int) value1 >> 10])
    }.float_0;
  }
}
