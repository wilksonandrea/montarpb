// Decompiled with JetBrains decompiler
// Type: Class2
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using System;
using System.Diagnostics;

#nullable disable
internal class Class2
{
  public Process method_0(string string_0, string string_1, Class3 class3_0)
  {
    Class3 class3 = class3_0 ?? Class3.Class3_0;
    return new Process()
    {
      StartInfo = new ProcessStartInfo()
      {
        FileName = string_0,
        Arguments = string_1,
        CreateNoWindow = class3.Boolean_1,
        WindowStyle = class3.ProcessWindowStyle_0,
        RedirectStandardOutput = class3.Boolean_2,
        UseShellExecute = class3.Boolean_0,
        WorkingDirectory = class3.String_0
      }
    };
  }

  public Process method_1(string string_0, string string_1)
  {
    return this.method_0(string_0, string_1, Class3.Class3_0);
  }

  public Class4 method_2(string string_0, string string_1, Class3 class3_0, bool bool_0)
  {
    Process process = this.method_0(string_0, string_1, class3_0);
    Class4 class4 = new Class4()
    {
      String_0 = string_0,
      String_1 = string_1
    };
    try
    {
      if (bool_0)
      {
        process.Start();
        class4.String_3 = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        class4.Enum1_0 = Enum1.Ok;
        class4.Int32_0 = process.ExitCode;
        if (!process.HasExited)
          process.Kill();
      }
      else
      {
        process.Start();
        class4.String_3 = process.StandardOutput.ReadToEnd();
      }
    }
    catch (Exception ex)
    {
      class4.Int32_0 = int.MaxValue;
      class4.Enum1_0 = Enum1.ExceptionBeforeRun;
      class4.String_2 = ex.ToString();
    }
    return class4;
  }

  public Class4 method_3(string string_0, string string_1, bool bool_0)
  {
    return this.method_2(string_0, string_1, Class3.Class3_0, bool_0);
  }
}
