using System;
using System.IO;
using Plugin.Core;
using Plugin.Core.Enums;

public static class GClass3
{
	private static void smethod_0(string string_0, GEnum4 genum4_0, GEnum3 genum3_0, int int_0)
	{
		try
		{
			GInterface1 gInterface = (GInterface1)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
			gInterface.GEnum3_0 = genum3_0;
			gInterface.Boolean_0 = true;
			gInterface.String_2 = "All";
			gInterface.String_1 = string_0;
			gInterface.String_0 = "Server: " + Path.GetFileName(string_0);
			GInterface0 gInterface2 = (GInterface0)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
			gInterface.GEnum4_0 = genum4_0;
			if (int_0 == 1)
			{
				gInterface2.GInterface2_0.imethod_0(gInterface);
			}
			else
			{
				gInterface2.GInterface2_0.imethod_1(gInterface.String_0);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
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
