using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using System;

public class GClass14
{
	public GClass14()
	{
	}

	public static void smethod_0(SyncClientPacket syncClientPacket_0)
	{
		byte num = syncClientPacket_0.ReadC();
		string str = syncClientPacket_0.ReadS((int)num);
		if (!string.IsNullOrEmpty(str) && num < 60)
		{
			CLogger.Print(string.Concat("From Server Message: ", str), LoggerType.Info, null);
		}
	}
}