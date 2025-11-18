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

public class PROTOCOL_AUTH_FRIEND_DELETE_REQ : GameClientPacket
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
			if (friend != null)
			{
				DaoManagerSQL.DeletePlayerFriend(friend.PlayerId, player.PlayerId);
				Account account = AccountManager.GetAccount(friend.PlayerId, 287);
				if (account != null)
				{
					int index = -1;
					FriendModel friend2 = account.Friend.GetFriend(player.PlayerId, out index);
					if (friend2 != null)
					{
						friend2.Removed = true;
						DaoManagerSQL.UpdatePlayerFriendBlock(account.PlayerId, friend2);
						SendFriendInfo.Load(account, friend2, 2);
						account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend2, index), OnlyInServer: false);
					}
				}
				player.Friend.RemoveFriend(friend);
				Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Delete, null, 0, int_0));
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_AUTH_FRIEND_DELETE_ACK(uint_0));
			Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_FRIEND_DELETE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
