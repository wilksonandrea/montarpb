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
			GInterface1 genum30 = (GInterface1)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
			genum30.GEnum3_0 = genum3_0;
			genum30.Boolean_0 = true;
			genum30.String_2 = "All";
			genum30.String_1 = string_0;
			genum30.String_0 = string.Concat("Server: ", Path.GetFileName(string_0));
			GInterface0 variable = (GInterface0)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
			genum30.GEnum4_0 = genum4_0;
			if (int_0 != 1)
			{
				variable.GInterface2_0.imethod_1(genum30.String_0);
			}
			else
			{
				variable.GInterface2_0.imethod_0(genum30);
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
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