// Decompiled with JetBrains decompiler
// Type: GClass1
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.Runtime.InteropServices;

#nullable disable
public static class GClass1
{
  private const int int_0 = 64 /*0x40*/;

  [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
  private static extern IntPtr CallWindowProcW(
    [In] byte[] byte_0,
    IntPtr intptr_0,
    int int_1,
    [In, Out] byte[] byte_1,
    IntPtr intptr_1);

  [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool VirtualProtect(
    [In] byte[] byte_0,
    IntPtr intptr_0,
    int int_1,
    out int int_2);

  public static string smethod_0()
  {
    byte[] byte_0 = new byte[8];
    return GClass1.smethod_1(ref byte_0) ? $"{BitConverter.ToUInt32(byte_0, 4):X8}{BitConverter.ToUInt32(byte_0, 0):X8}" : "ND";
  }

  private static bool smethod_1(ref byte[] byte_0)
  {
    byte[] numArray1 = new byte[26]
    {
      (byte) 85,
      (byte) 137,
      (byte) 229,
      (byte) 87,
      (byte) 139,
      (byte) 125,
      (byte) 16 /*0x10*/,
      (byte) 106,
      (byte) 1,
      (byte) 88,
      (byte) 83,
      (byte) 15,
      (byte) 162,
      (byte) 137,
      (byte) 7,
      (byte) 137,
      (byte) 87,
      (byte) 4,
      (byte) 91,
      (byte) 95,
      (byte) 137,
      (byte) 236,
      (byte) 93,
      (byte) 194,
      (byte) 16 /*0x10*/,
      (byte) 0
    };
    byte[] numArray2 = new byte[19]
    {
      (byte) 83,
      (byte) 72,
      (byte) 199,
      (byte) 192 /*0xC0*/,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 15,
      (byte) 162,
      (byte) 65,
      (byte) 137,
      (byte) 0,
      (byte) 65,
      (byte) 137,
      (byte) 80 /*0x50*/,
      (byte) 4,
      (byte) 91,
      (byte) 195
    };
    byte[] byte_0_1 = GClass1.smethod_2() ? numArray2 : numArray1;
    IntPtr num = new IntPtr(byte_0_1.Length);
    if (!GClass1.VirtualProtect(byte_0_1, num, 64 /*0x40*/, out int _))
      Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
    num = new IntPtr(byte_0.Length);
    return GClass1.CallWindowProcW(byte_0_1, IntPtr.Zero, 0, byte_0, num) != IntPtr.Zero;
  }

  private static bool smethod_2() => IntPtr.Size == 8;
}
