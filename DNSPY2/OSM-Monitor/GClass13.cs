using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

// Token: 0x02000026 RID: 38
public class GClass13
{
	// Token: 0x060000B3 RID: 179 RVA: 0x000055C8 File Offset: 0x000037C8
	public static void smethod_0(string string_0)
	{
		try
		{
			foreach (SChannelModel schannelModel in SChannelXML.Servers)
			{
				if (schannelModel == null)
				{
					break;
				}
				IPEndPoint connection = SynchronizeXML.GetServer((int)schannelModel.Port).Connection;
				using (SyncServerPacket syncServerPacket = new SyncServerPacket())
				{
					syncServerPacket.WriteH(7171);
					syncServerPacket.WriteC((byte)string_0.Length);
					syncServerPacket.WriteS(string_0, string_0.Length);
					GClass12.smethod_4(syncServerPacket.ToArray(), connection);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00002133 File Offset: 0x00000333
	public GClass13()
	{
	}
}
