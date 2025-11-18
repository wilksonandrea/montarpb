using System;
using System.IO;
using Plugin.Core;
using Plugin.Core.Enums;

// Token: 0x02000013 RID: 19
public static class GClass3
{
	// Token: 0x0600006D RID: 109 RVA: 0x00004644 File Offset: 0x00002844
	private static void smethod_0(string string_0, GEnum4 genum4_0, GEnum3 genum3_0, int int_0)
	{
		try
		{
			GInterface1 ginterface = (GInterface1)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
			ginterface.GEnum3_0 = genum3_0;
			ginterface.Boolean_0 = true;
			ginterface.String_2 = "All";
			ginterface.String_1 = string_0;
			ginterface.String_0 = "Server: " + Path.GetFileName(string_0);
			GInterface0 ginterface2 = (GInterface0)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
			ginterface.GEnum4_0 = genum4_0;
			if (int_0 == 1)
			{
				ginterface2.GInterface2_0.imethod_0(ginterface);
			}
			else
			{
				ginterface2.GInterface2_0.imethod_1(ginterface.String_0);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00002392 File Offset: 0x00000592
	public static void smethod_1(string string_0)
	{
		GClass3.smethod_0(string_0, GEnum4.NET_FW_RULE_DIR_OUT, GEnum3.NET_FW_ACTION_ALLOW, 1);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000239D File Offset: 0x0000059D
	public static void smethod_2(string string_0)
	{
		GClass3.smethod_0(string_0, GEnum4.NET_FW_RULE_DIR_OUT, GEnum3.NET_FW_ACTION_BLOCK, 0);
	}
}
