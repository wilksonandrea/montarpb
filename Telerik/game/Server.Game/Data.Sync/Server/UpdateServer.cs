using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using Server.Game.Data.XML;
using System;
using System.Collections.Generic;
using System.Net;

namespace Server.Game.Data.Sync.Server
{
	public class UpdateServer
	{
		private static DateTime dateTime_0;

		public UpdateServer()
		{
		}

		public static void RefreshSChannel(int serverId)
		{
			try
			{
				if (ComDiv.GetDuration(UpdateServer.dateTime_0) >= (double)ConfigLoader.UpdateIntervalPlayersServer)
				{
					UpdateServer.dateTime_0 = DateTimeUtil.Now();
					int count = 0;
					foreach (ChannelModel channel in ChannelsXML.Channels)
					{
						count += channel.Players.Count;
					}
					foreach (SChannelModel server in SChannelXML.Servers)
					{
						if (server.Id != serverId)
						{
							IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
							using (SyncServerPacket syncServerPacket = new SyncServerPacket())
							{
								syncServerPacket.WriteH(15);
								syncServerPacket.WriteD(serverId);
								syncServerPacket.WriteD(count);
								GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
							}
						}
						else
						{
							server.LastPlayers = count;
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}