// Decompiled with JetBrains decompiler
// Type: GClass10
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
public class GClass10
{
  public const int int_0 = 512 /*0x0200*/;
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
  public const int int_11 = 176 /*0xB0*/;
  public const int int_12 = 187;
  public const int int_13 = 201;
  public const int int_14 = 214;
  public const int int_15 = 792;
  public const long long_0 = 1;
  public const long long_1 = 2;
  public const long long_2 = 4;
  public const long long_3 = 8;
  public const long long_4 = 16 /*0x10*/;
  public const long long_5 = 32 /*0x20*/;

  [DllImport("USER32.DLL")]
  public static extern bool PostMessage(
    IntPtr intptr_0,
    uint uint_0,
    IntPtr intptr_1,
    IntPtr intptr_2);

  [DllImport("USER32.DLL")]
  public static extern int SendMessage(
    IntPtr intptr_0,
    int int_16,
    IntPtr intptr_1,
    IntPtr intptr_2);

  [DllImport("USER32.DLL")]
  public static extern uint GetCaretBlinkTime();

  public static bool smethod_0(Control control_0, ref Bitmap bitmap_0)
  {
    Graphics graphics = Graphics.FromImage((Image) bitmap_0);
    IntPtr intptr_2 = new IntPtr(12);
    IntPtr hdc = graphics.GetHdc();
    GClass10.SendMessage(control_0.Handle, 791, hdc, intptr_2);
    graphics.ReleaseHdc(hdc);
    graphics.Dispose();
    return true;
  }

  private enum Enum3 : long
  {
    PRF_CHECKVISIBLE = 1,
    PRF_NONCLIENT = 2,
    PRF_CLIENT = 4,
    PRF_ERASEBKGND = 8,
    PRF_CHILDREN = 16, // 0x0000000000000010
    PRF_OWNED = 32, // 0x0000000000000020
  }
}
