using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;

public class GClass14
{
	public static void smethod_0(SyncClientPacket syncClientPacket_0)
	{
		byte b = syncClientPacket_0.ReadC();
		string text = syncClientPacket_0.ReadS(b);
		if (!string.IsNullOrEmpty(text) && b < 60)
		{
			CLogger.Print("From Server Message: " + text, LoggerType.Info);
		}
	}
}
