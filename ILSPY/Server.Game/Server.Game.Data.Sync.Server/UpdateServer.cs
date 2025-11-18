using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.XML;

namespace Server.Game.Data.Sync.Server;

public class UpdateServer
{
	private static DateTime dateTime_0;

	public static void RefreshSChannel(int serverId)
	{
		try
		{
			if (ComDiv.GetDuration(dateTime_0) < (double)ConfigLoader.UpdateIntervalPlayersServer)
			{
				return;
			}
			dateTime_0 = DateTimeUtil.Now();
			int num = 0;
			foreach (ChannelModel channel in ChannelsXML.Channels)
			{
				num += channel.Players.Count;
			}
			foreach (SChannelModel server in SChannelXML.Servers)
			{
				if (server.Id == serverId)
				{
					server.LastPlayers = num;
					continue;
				}
				IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
				using SyncServerPacket syncServerPacket = new SyncServerPacket();
				syncServerPacket.WriteH(15);
				syncServerPacket.WriteD(serverId);
				syncServerPacket.WriteD(num);
				GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
