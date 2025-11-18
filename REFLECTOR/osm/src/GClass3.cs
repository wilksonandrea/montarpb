using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.IO;

public static class GClass3
{
    private static void smethod_0(string string_0, GEnum4 genum4_0, GEnum3 genum3_0, int int_0)
    {
        try
        {
            GInterface1 interface2 = (GInterface1) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            interface2.GEnum3_0 = genum3_0;
            interface2.Boolean_0 = true;
            interface2.String_2 = "All";
            interface2.String_1 = string_0;
            interface2.String_0 = "Server: " + Path.GetFileName(string_0);
            GInterface0 interface3 = (GInterface0) Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            interface2.GEnum4_0 = genum4_0;
            if (int_0 == 1)
            {
                interface3.GInterface2_0.imethod_0(interface2);
            }
            else
            {
                interface3.GInterface2_0.imethod_1(interface2.String_0);
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
    }

    public static void smethod_1(string string_0)
    {
        smethod_0(string_0, GEnum4.NET_FW_RULE_DIR_OUT, GEnum3.NET_FW_ACTION_ALLOW, 1);
    }

    public static void smethod_2(string string_0)
    {
        smethod_0(string_0, GEnum4.NET_FW_RULE_DIR_OUT, GEnum3.NET_FW_ACTION_BLOCK, 0);
    }
}

