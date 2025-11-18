using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Sync;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace Server.Auth.Data.Sync.Server
{
	public class UpdateServer
	{
		private static DateTime dateTime_0;

		public UpdateServer()
		{
		}

		public static void RefreshSChannel(int ServerId)
		{
			try
			{
				if (ComDiv.GetDuration(UpdateServer.dateTime_0) >= (double)ConfigLoader.UpdateIntervalPlayersServer)
				{
					UpdateServer.dateTime_0 = DateTimeUtil.Now();
					int count = AuthXender.SocketSessions.Count;
					foreach (SChannelModel server in SChannelXML.Servers)
					{
						if (server.Id != ServerId)
						{
							IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
							using (SyncServerPacket syncServerPacket = new SyncServerPacket())
							{
								syncServerPacket.WriteH(15);
								syncServerPacket.WriteD(ServerId);
								syncServerPacket.WriteD(count);
								AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
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