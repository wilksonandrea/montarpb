using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

public class GClass13
{
	public static void smethod_0(string string_0)
	{
		try
		{
			foreach (SChannelModel server in SChannelXML.Servers)
			{
				if (server == null)
				{
					break;
				}
				IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
				using SyncServerPacket syncServerPacket = new SyncServerPacket();
				syncServerPacket.WriteH(7171);
				syncServerPacket.WriteC((byte)string_0.Length);
				syncServerPacket.WriteS(string_0, string_0.Length);
				GClass12.smethod_4(syncServerPacket.ToArray(), connection);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
