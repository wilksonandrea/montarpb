// Decompiled with JetBrains decompiler
// Type: GClass4
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

#nullable disable
public static class GClass4
{
  public static string smethod_0()
  {
    return GClass4.smethod_1(string.Join("\n", GClass4.smethod_5(GClass4.Enum2.Cpuid), GClass4.smethod_5(GClass4.Enum2.Motherboard)));
  }

  private static string smethod_1(string string_0)
  {
    using (SHA1Managed shA1Managed = new SHA1Managed())
    {
      byte[] hash = shA1Managed.ComputeHash(Encoding.UTF8.GetBytes(string_0));
      StringBuilder stringBuilder = new StringBuilder(hash.Length * 2);
      foreach (byte num in hash)
        stringBuilder.Append(num.ToString("X2"));
      return stringBuilder.ToString();
    }
  }

  private static string smethod_2(string string_0, string string_1)
  {
    string str = "";
    foreach (ManagementBaseObject instance in new ManagementClass(string_0).GetInstances())
    {
      ManagementObject managementObject = instance as ManagementObject;
      if (!(str != ""))
      {
        try
        {
          str = managementObject[string_1].ToString();
          break;
        }
        catch
        {
        }
      }
    }
    return str;
  }

  private static string smethod_3(string string_0_1, string string_1)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GClass4.Class8 class8 = new GClass4.Class8();
    // ISSUE: reference to a compiler-generated field
    class8.string_0 = string_1;
    Class4 class4 = new Class2().method_2("/usr/bin/sudo", " " + string_0_1, new Class3()
    {
      ProcessWindowStyle_0 = ProcessWindowStyle.Hidden,
      Boolean_1 = true,
      Boolean_2 = true,
      Boolean_0 = false
    }, true);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    class8.string_0 = class8.string_0.EndsWith(":") ? class8.string_0 : class8.string_0 + ":";
    // ISSUE: reference to a compiler-generated method
    string str = ((IEnumerable<string>) class4.String_3.Split(new string[1]
    {
      Environment.NewLine
    }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (string_0_2 => string_0_2.Trim(' ', '\t'))).First<string>(new Func<string, bool>(class8.method_0));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return str.Substring(str.IndexOf(class8.string_0, StringComparison.Ordinal) + class8.string_0.Length).Trim(' ', '\t');
  }

  private static string smethod_4(string string_0)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GClass4.Class9 class9 = new GClass4.Class9();
    Process process = new Process();
    ProcessStartInfo processStartInfo = new ProcessStartInfo()
    {
      FileName = "/bin/sh"
    };
    string str = $"/usr/sbin/ioreg -rd1 -c IOPlatformExpertDevice | awk -F'\\\"' '/{string_0}/{{ print $(NF-1) }}'";
    processStartInfo.Arguments = $"-c \"{str}\"";
    processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
    processStartInfo.RedirectStandardOutput = true;
    processStartInfo.UseShellExecute = false;
    // ISSUE: reference to a compiler-generated field
    class9.string_0 = (string) null;
    process.StartInfo = processStartInfo;
    // ISSUE: reference to a compiler-generated method
    process.OutputDataReceived += new DataReceivedEventHandler(class9.method_0);
    process.Start();
    process.BeginOutputReadLine();
    process.WaitForExit();
    // ISSUE: reference to a compiler-generated field
    return class9.string_0;
  }

  private static string smethod_5(GClass4.Enum2 enum2_0)
  {
    switch (enum2_0)
    {
      case GClass4.Enum2.Motherboard:
        if (Class1.Boolean_2)
          return GClass4.smethod_3("dmidecode -t 2", "Manufacturer");
        if (Class1.Boolean_0)
          return GClass4.smethod_2("Win32_BaseBoard", "Manufacturer");
        if (Class1.Boolean_1)
          return GClass4.smethod_4("IOPlatformSerialNumber");
        break;
      case GClass4.Enum2.Cpuid:
        if (Class1.Boolean_2)
          return string.Join("", ((IEnumerable<string>) GClass4.smethod_3("dmidecode -t 4", "ID").Split(' ')).Reverse<string>());
        if (Class1.Boolean_0)
        {
          string str = GClass1.smethod_0();
          return str == null || str.Length <= 2 ? GClass4.smethod_2("Win32_Processor", "ProcessorId") : str;
        }
        if (Class1.Boolean_1)
          return GClass4.smethod_4("IOPlatformUUID");
        break;
    }
    throw new InvalidEnumArgumentException();
  }

  private enum Enum2
  {
    Motherboard,
    Cpuid,
  }
}
