using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using System;
using System.Net;

namespace Server.Auth.Data.Sync.Server
{
	public class AuthLogin
	{
		public AuthLogin()
		{
		}

		public static void SendLoginKickInfo(Account Player)
		{
			try
			{
				int serverId = Player.Status.ServerId;
				if (serverId == 255 || serverId == 0)
				{
					Player.SetOnlineStatus(false);
				}
				else
				{
					SChannelModel server = SChannelXML.GetServer(serverId);
					if (server != null)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(10);
							syncServerPacket.WriteQ(Player.PlayerId);
							AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
						}
					}
					else
					{
						return;
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