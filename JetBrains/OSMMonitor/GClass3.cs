// Decompiled with JetBrains decompiler
// Type: GClass3
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.IO;

#nullable disable
public static class GClass3
{
  private static void smethod_0(string string_0, GEnum4 genum4_0, GEnum3 genum3_0, int int_0)
  {
    try
    {
      // ISSUE: variable of a compiler-generated type
      GInterface1 instance1 = (GInterface1) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
      instance1.GEnum3_0 = genum3_0;
      instance1.Boolean_0 = true;
      instance1.String_2 = "All";
      instance1.String_1 = string_0;
      instance1.String_0 = "Server: " + Path.GetFileName(string_0);
      // ISSUE: variable of a compiler-generated type
      GInterface0 instance2 = (GInterface0) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
      instance1.GEnum4_0 = genum4_0;
      if (int_0 == 1)
      {
        // ISSUE: reference to a compiler-generated method
        instance2.GInterface2_0.imethod_0(instance1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        instance2.GInterface2_0.imethod_1(instance1.String_0);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static void smethod_1(string string_0)
  {
    GClass3.smethod_0(string_0, GEnum4.NET_FW_RULE_DIR_OUT, GEnum3.NET_FW_ACTION_ALLOW, 1);
  }

  public static void smethod_2(string string_0)
  {
    GClass3.smethod_0(string_0, GEnum4.NET_FW_RULE_DIR_OUT, GEnum3.NET_FW_ACTION_BLOCK, 0);
  }
}
