using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;

// Token: 0x02000027 RID: 39
public class GClass14
{
	// Token: 0x060000B5 RID: 181 RVA: 0x0000569C File Offset: 0x0000389C
	public static void smethod_0(SyncClientPacket syncClientPacket_0)
	{
		byte b = syncClientPacket_0.ReadC();
		string text = syncClientPacket_0.ReadS((int)b);
		if (!string.IsNullOrEmpty(text) && b < 60)
		{
			CLogger.Print("From Server Message: " + text, LoggerType.Info, null);
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00002133 File Offset: 0x00000333
	public GClass14()
	{
	}
}
