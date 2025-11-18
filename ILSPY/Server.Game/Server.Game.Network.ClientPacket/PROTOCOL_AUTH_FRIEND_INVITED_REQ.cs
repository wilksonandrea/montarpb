using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadU(66);
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
			if (player.Nickname.Length != 0 && !(player.Nickname == string_0))
			{
				if (player.Friend.Friends.Count >= 50)
				{
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487800u));
					return;
				}
				Account account = AccountManager.GetAccount(string_0, 1, 287);
				if (account != null)
				{
					if (player.Friend.GetFriendIdx(account.PlayerId) == -1)
					{
						if (account.Friend.Friends.Count >= 50)
						{
							Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487800u));
							return;
						}
						int num = AllUtils.AddFriend(account, player, 2);
						if (AllUtils.AddFriend(player, account, (num != 1) ? 1 : 0) != -1 && num != -1)
						{
							int index;
							FriendModel friend = account.Friend.GetFriend(player.PlayerId, out index);
							if (friend != null)
							{
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((num == 0) ? FriendChangeState.Insert : FriendChangeState.Update, friend, index), OnlyInServer: false);
							}
							int index2;
							FriendModel friend2 = player.Friend.GetFriend(account.PlayerId, out index2);
							if (friend2 != null)
							{
								Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, friend2, index2));
							}
						}
						else
						{
							Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487801u));
						}
					}
					else
					{
						Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487809u));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487810u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487799u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
