using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Server;

public class AuthLogin
{
	public static void SendLoginKickInfo(Account Player)
	{
		try
		{
			int serverId = Player.Status.ServerId;
			if (serverId != 255 && serverId != 0)
			{
				SChannelModel server = SChannelXML.GetServer(serverId);
				if (server != null)
				{
					IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
					using SyncServerPacket syncServerPacket = new SyncServerPacket();
					syncServerPacket.WriteH(10);
					syncServerPacket.WriteQ(Player.PlayerId);
					AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
					return;
				}
			}
			else
			{
				Player.SetOnlineStatus(Online: false);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
