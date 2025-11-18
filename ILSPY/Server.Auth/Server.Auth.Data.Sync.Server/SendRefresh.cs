using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Server;

public static class SendRefresh
{
	public static void RefreshAccount(Account Player, bool IsConnect)
	{
		try
		{
			UpdateServer.RefreshSChannel(0);
			AccountManager.GetFriendlyAccounts(Player.Friend);
			foreach (FriendModel friend in Player.Friend.Friends)
			{
				PlayerInfo ınfo = friend.Info;
				if (ınfo != null)
				{
					SChannelModel server = SChannelXML.GetServer(ınfo.Status.ServerId);
					if (server != null)
					{
						SendRefreshPacket(0, Player.PlayerId, friend.PlayerId, IsConnect, server);
					}
				}
			}
			if (Player.ClanId <= 0)
			{
				return;
			}
			foreach (Account clanPlayer in Player.ClanPlayers)
			{
				if (clanPlayer != null && clanPlayer.IsOnline)
				{
					SChannelModel server2 = SChannelXML.GetServer(clanPlayer.Status.ServerId);
					if (server2 != null)
					{
						SendRefreshPacket(1, Player.PlayerId, clanPlayer.PlayerId, IsConnect, server2);
					}
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void SendRefreshPacket(int Type, long PlayerId, long MemberId, bool IsConnect, SChannelModel Server)
	{
		IPEndPoint connection = SynchronizeXML.GetServer(Server.Port).Connection;
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteH(11);
		syncServerPacket.WriteC((byte)Type);
		syncServerPacket.WriteC(IsConnect ? ((byte)1) : ((byte)0));
		syncServerPacket.WriteQ(PlayerId);
		syncServerPacket.WriteQ(MemberId);
		AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
	}
}
