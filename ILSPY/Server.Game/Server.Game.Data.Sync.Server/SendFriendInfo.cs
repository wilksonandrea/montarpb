using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server;

public class SendFriendInfo
{
	public static void Load(Account Player, FriendModel Friend, int Type)
	{
		try
		{
			if (Player == null)
			{
				return;
			}
			SChannelModel server = GameXender.Sync.GetServer(Player.Status);
			if (server == null)
			{
				return;
			}
			IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(17);
			syncServerPacket.WriteQ(Player.PlayerId);
			syncServerPacket.WriteC((byte)Type);
			syncServerPacket.WriteQ(Friend.PlayerId);
			if (Type != 2)
			{
				syncServerPacket.WriteC((byte)Friend.State);
				syncServerPacket.WriteC(Friend.Removed ? ((byte)1) : ((byte)0));
			}
			GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
