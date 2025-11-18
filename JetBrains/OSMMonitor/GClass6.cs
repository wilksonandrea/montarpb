// Decompiled with JetBrains decompiler
// Type: GClass6
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Microsoft.VisualBasic.Devices;
using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;

#nullable disable
public class GClass6
{
  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  private static extern int ShellExecute(
    IntPtr intptr_0,
    string string_0,
    string string_1,
    string string_2,
    string string_3,
    int int_0);

  public static bool smethod_0()
  {
    using (WindowsIdentity current = WindowsIdentity.GetCurrent())
      return new WindowsPrincipal(current).IsInRole(WindowsBuiltInRole.Administrator);
  }

  public static int smethod_1()
  {
    int num = 0;
    try
    {
      num = Convert.ToInt32((double) ulong.Parse(new ComputerInfo().TotalPhysicalMemory.ToString()) / 1048576.0);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return num;
  }

  public static double smethod_2()
  {
    double num = 0.0;
    try
    {
      using (Process currentProcess = Process.GetCurrentProcess())
      {
        currentProcess.Refresh();
        num = (double) currentProcess.PrivateMemorySize64 / 1048576.0;
        currentProcess.Dispose();
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return num;
  }

  public static double smethod_3()
  {
    double num = 0.0;
    try
    {
      num = GClass6.smethod_2() * 100.0 / (double) GClass6.smethod_1();
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return num;
  }

  public static long smethod_4(DirectoryInfo directoryInfo_0_1, bool bool_0)
  {
    long num = directoryInfo_0_1.EnumerateFiles().Sum<FileInfo>((Func<FileInfo, long>) (fileInfo_0 => fileInfo_0.Length));
    if (bool_0)
      num += directoryInfo_0_1.EnumerateDirectories().Sum<DirectoryInfo>((Func<DirectoryInfo, long>) (directoryInfo_0_2 => GClass6.smethod_4(directoryInfo_0_2, true)));
    return num;
  }

  public static bool smethod_5(DirectoryInfo directoryInfo_0)
  {
    try
    {
      foreach (DirectoryInfo directory in directoryInfo_0.GetDirectories())
      {
        foreach (FileInfo file in directoryInfo_0.GetFiles())
        {
          file.IsReadOnly = false;
          file.Delete();
        }
        directory.Delete(true);
      }
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static void smethod_6(string string_0, string string_1, string string_2)
  {
    try
    {
      GClass6.ShellExecute(IntPtr.Zero, string_2, string_1, string_0, (string) null, 1);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static string smethod_7()
  {
    try
    {
      return Guid.NewGuid().ToString().ToUpper();
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return "";
    }
  }

  public static string smethod_8()
  {
    try
    {
      ManagementObjectCollection objectCollection = new ManagementObjectSearcher("Select ProcessorId From Win32_processor").Get();
      string str = "";
      using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = objectCollection.GetEnumerator())
      {
        if (enumerator.MoveNext())
          str = enumerator.Current["ProcessorId"].ToString();
      }
      return str.ToUpper();
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return "";
    }
  }
}
