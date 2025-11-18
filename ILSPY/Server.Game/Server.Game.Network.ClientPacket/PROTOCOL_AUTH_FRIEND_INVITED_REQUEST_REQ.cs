using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ : GameClientPacket
{
	private int int_0;

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
			Account account = method_0(player);
			if (account != null)
			{
				if (account.Status.ServerId != byte.MaxValue && account.Status.ServerId != 0)
				{
					if (account.MatchSlot >= 0)
					{
						Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147495939u));
						return;
					}
					int friendIdx = account.Friend.GetFriendIdx(player.PlayerId);
					if (friendIdx == -1)
					{
						Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487806u));
					}
					else if (account.IsOnline)
					{
						account.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(friendIdx), OnlyInServer: false);
					}
					else
					{
						Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487807u));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147495938u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487805u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private Account method_0(Account account_0)
	{
		FriendModel friend = account_0.Friend.GetFriend(int_0);
		if (friend == null)
		{
			return null;
		}
		return AccountManager.GetAccount(friend.PlayerId, 287);
	}
}
