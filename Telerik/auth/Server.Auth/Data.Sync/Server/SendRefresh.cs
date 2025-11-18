using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using System;
using System.Collections.Generic;
using System.Net;

namespace Server.Auth.Data.Sync.Server
{
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
					if (ınfo == null)
					{
						continue;
					}
					SChannelModel server = SChannelXML.GetServer((int)ınfo.Status.ServerId);
					if (server == null)
					{
						continue;
					}
					SendRefresh.SendRefreshPacket(0, Player.PlayerId, friend.PlayerId, IsConnect, server);
				}
				if (Player.ClanId > 0)
				{
					foreach (Account clanPlayer in Player.ClanPlayers)
					{
						if (clanPlayer == null || !clanPlayer.IsOnline)
						{
							continue;
						}
						SChannelModel sChannelModel = SChannelXML.GetServer((int)clanPlayer.Status.ServerId);
						if (sChannelModel == null)
						{
							continue;
						}
						SendRefresh.SendRefreshPacket(1, Player.PlayerId, clanPlayer.PlayerId, IsConnect, sChannelModel);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static void SendRefreshPacket(int Type, long PlayerId, long MemberId, bool IsConnect, SChannelModel Server)
		{
			IPEndPoint connection = SynchronizeXML.GetServer((int)Server.Port).Connection;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteH(11);
				syncServerPacket.WriteC((byte)Type);
				syncServerPacket.WriteC((byte)IsConnect);
				syncServerPacket.WriteQ(PlayerId);
				syncServerPacket.WriteQ(MemberId);
				AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
			}
		}
	}
}