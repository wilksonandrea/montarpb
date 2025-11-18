using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Game.Data.Sync.Server;

public class UpdateChannel
{
	public static void RefreshChannel(int ServerId, int ChannelId, int Count)
	{
		try
		{
			SChannelModel server = GameXender.Sync.GetServer(0);
			if (server == null)
			{
				return;
			}
			IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(33);
			syncServerPacket.WriteD(ServerId);
			syncServerPacket.WriteD(ChannelId);
			syncServerPacket.WriteD(Count);
			GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
