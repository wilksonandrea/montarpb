using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Server
{
	// Token: 0x0200004F RID: 79
	public static class SendRefresh
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000B2D4 File Offset: 0x000094D4
		public static void RefreshAccount(Account Player, bool IsConnect)
		{
			try
			{
				UpdateServer.RefreshSChannel(0);
				AccountManager.GetFriendlyAccounts(Player.Friend);
				foreach (FriendModel friendModel in Player.Friend.Friends)
				{
					PlayerInfo info = friendModel.Info;
					if (info != null)
					{
						SChannelModel server = SChannelXML.GetServer((int)info.Status.ServerId);
						if (server != null)
						{
							SendRefresh.SendRefreshPacket(0, Player.PlayerId, friendModel.PlayerId, IsConnect, server);
						}
					}
				}
				if (Player.ClanId > 0)
				{
					foreach (Account account in Player.ClanPlayers)
					{
						if (account != null && account.IsOnline)
						{
							SChannelModel server2 = SChannelXML.GetServer((int)account.Status.ServerId);
							if (server2 != null)
							{
								SendRefresh.SendRefreshPacket(1, Player.PlayerId, account.PlayerId, IsConnect, server2);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000B40C File Offset: 0x0000960C
		public static void SendRefreshPacket(int Type, long PlayerId, long MemberId, bool IsConnect, SChannelModel Server)
		{
			IPEndPoint connection = SynchronizeXML.GetServer((int)Server.Port).Connection;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteH(11);
				syncServerPacket.WriteC((byte)Type);
				syncServerPacket.WriteC((IsConnect > false) ? 1 : 0);
				syncServerPacket.WriteQ(PlayerId);
				syncServerPacket.WriteQ(MemberId);
				AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
			}
		}
	}
}
