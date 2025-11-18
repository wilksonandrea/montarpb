// Decompiled with JetBrains decompiler
// Type: GClass7
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
public class GClass7
{
  [DllImport("user32.dll")]
  public static extern bool EnumDisplaySettings(
    string string_0,
    int int_0,
    ref GClass7.GStruct0 gstruct0_0);

  public static int smethod_0()
  {
    GClass7.GStruct0 gstruct0_0 = new GClass7.GStruct0();
    gstruct0_0.short_2 = (short) Marshal.SizeOf<GClass7.GStruct0>(gstruct0_0);
    gstruct0_0.short_3 = (short) 0;
    return GClass7.EnumDisplaySettings((string) null, -1, ref gstruct0_0) ? gstruct0_0.int_7 : -1;
  }

  public struct GStruct0
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string string_0;
    public short short_0;
    public short short_1;
    public short short_2;
    public short short_3;
    public int int_0;
    public Point point_0;
    public int int_1;
    public int int_2;
    public short short_4;
    public short short_5;
    public short short_6;
    public short short_7;
    public short short_8;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string string_1;
    public short short_9;
    public int int_3;
    public int int_4;
    public int int_5;
    public int int_6;
    public int int_7;
    public int int_8;
    public int int_9;
    public int int_10;
    public int int_11;
    public int int_12;
    public int int_13;
    public int int_14;
    public int int_15;
  }
}
