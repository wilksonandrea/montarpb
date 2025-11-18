using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_ACCEPT_REQ : GameClientPacket
{
	private int int_0;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			FriendModel friend = player.Friend.GetFriend(int_0);
			if (friend != null && friend.State > 0)
			{
				Account account = AccountManager.GetAccount(friend.PlayerId, 287);
				if (account != null)
				{
					if (friend.Info == null)
					{
						friend.SetModel(account.PlayerId, account.Rank, account.NickColor, account.Nickname, account.IsOnline, account.Status);
					}
					else
					{
						friend.Info.SetInfo(account.Rank, account.NickColor, account.Nickname, account.IsOnline, account.Status);
					}
					friend.State = 0;
					DaoManagerSQL.UpdatePlayerFriendState(player.PlayerId, friend);
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Accept, null, 0, int_0));
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, int_0));
					int index = -1;
					FriendModel friend2 = account.Friend.GetFriend(player.PlayerId, out index);
					if (friend2 != null && friend2.State > 0)
					{
						if (friend2.Info == null)
						{
							friend2.SetModel(player.PlayerId, player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
						}
						else
						{
							friend2.Info.SetInfo(player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
						}
						friend2.State = 0;
						DaoManagerSQL.UpdatePlayerFriendState(account.PlayerId, friend2);
						SendFriendInfo.Load(account, friend2, 1);
						account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend2, index), OnlyInServer: false);
					}
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			if (uint_0 != 0)
			{
				Client.SendPacket(new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(uint_0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_FRIEND_ACCEPT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
