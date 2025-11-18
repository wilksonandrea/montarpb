// Decompiled with JetBrains decompiler
// Type: GClass11
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

#nullable disable
public class GClass11
{
  private const uint uint_0 = 1;
  private const uint uint_1 = 4;

  [DllImport("user32.dll", SetLastError = true)]
  private static extern IntPtr FindWindow(string string_0, string string_1);

  [DllImport("user32.dll", SetLastError = true)]
  private static extern bool SetWindowPos(
    IntPtr intptr_0,
    IntPtr intptr_1,
    int int_0,
    int int_1,
    int int_2,
    int int_3,
    uint uint_2);

  private static GClass11.Struct4 smethod_0()
  {
    return new GClass11.Struct4(GClass11.GetSystemMetrics(0), GClass11.GetSystemMetrics(1));
  }

  [DllImport("User32.dll", CharSet = CharSet.Auto)]
  private static extern int GetSystemMetrics(int int_0);

  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool GetWindowRect(HandleRef handleRef_0, out GClass11.Struct5 struct5_0);

  private static GClass11.Struct4 smethod_1(IntPtr intptr_0)
  {
    GClass11.Struct5 struct5_0;
    if (!GClass11.GetWindowRect(new HandleRef((object) null, intptr_0), out struct5_0))
      CLogger.Print("Unable to get window rect!", LoggerType.Warning, (Exception) null);
    return new GClass11.Struct4(struct5_0.int_2 - struct5_0.int_0, struct5_0.int_3 - struct5_0.int_1);
  }

  public static void smethod_2()
  {
    IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
    if (mainWindowHandle == IntPtr.Zero)
      CLogger.Print("Couldn't find a window to center!", LoggerType.Warning, (Exception) null);
    GClass11.Struct4 struct4_1 = GClass11.smethod_0();
    GClass11.Struct4 struct4_2 = GClass11.smethod_1(mainWindowHandle);
    int int_0 = (struct4_1.Int32_0 - struct4_2.Int32_0) / 2;
    int int_1 = (struct4_1.Int32_1 - struct4_2.Int32_1) / 2;
    GClass11.SetWindowPos(mainWindowHandle, IntPtr.Zero, int_0, int_1, 0, 0, 5U);
  }

  public static void smethod_3(int int_0)
  {
    if (int_0 == 0)
      return;
    using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessID={int_0}"))
    {
      foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
        GClass11.smethod_3(Convert.ToInt32(managementBaseObject["ProcessID"]));
      try
      {
        Process.GetProcessById(int_0).Kill();
      }
      catch (ArgumentException ex)
      {
      }
    }
  }

  private struct Struct4(int int_2, int int_3)
  {
    public int Int32_0 { get; set; } = int_2;

    public int Int32_1 { get; set; } = int_3;
  }

  private struct Struct5
  {
    public int int_0;
    public int int_1;
    public int int_2;
    public int int_3;
  }
}
