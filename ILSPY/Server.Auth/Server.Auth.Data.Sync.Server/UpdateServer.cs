using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Server.Auth.Data.Sync.Server;

public class UpdateServer
{
	private static DateTime dateTime_0;

	public static void RefreshSChannel(int ServerId)
	{
		try
		{
			if (ComDiv.GetDuration(dateTime_0) < (double)ConfigLoader.UpdateIntervalPlayersServer)
			{
				return;
			}
			dateTime_0 = DateTimeUtil.Now();
			int count = AuthXender.SocketSessions.Count;
			foreach (SChannelModel server in SChannelXML.Servers)
			{
				if (server.Id == ServerId)
				{
					server.LastPlayers = count;
					continue;
				}
				IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
				using SyncServerPacket syncServerPacket = new SyncServerPacket();
				syncServerPacket.WriteH(15);
				syncServerPacket.WriteD(ServerId);
				syncServerPacket.WriteD(count);
				AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
