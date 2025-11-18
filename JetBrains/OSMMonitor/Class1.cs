// Decompiled with JetBrains decompiler
// Type: Class1
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
internal static class Class1
{
  public static bool Boolean_0 => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

  public static bool Boolean_1 => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

  public static bool Boolean_2 => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

  public static string String_0 => Path.GetPathRoot(Environment.SystemDirectory);

  public static bool Boolean_3 => Environment.Is64BitOperatingSystem;

  public static string String_1 => !Class1.Boolean_3 ? "32" : "64";

  public static bool Boolean_4 => LicenseManager.UsageMode == LicenseUsageMode.Designtime;
}
