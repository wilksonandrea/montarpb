using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_INSERT_REQ : GameClientPacket
{
	private string string_0;

	private int int_0;

	private int int_1;

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
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487800u));
					return;
				}
				Account account = AccountManager.GetAccount(string_0, 1, 287);
				if (account != null)
				{
					if (player.Friend.GetFriendIdx(account.PlayerId) == -1)
					{
						if (account.Friend.Friends.Count >= 50)
						{
							Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487800u));
							return;
						}
						int num = AllUtils.AddFriend(account, player, 2);
						if (AllUtils.AddFriend(player, account, (num != 1) ? 1 : 0) != -1 && num != -1)
						{
							FriendModel friend = account.Friend.GetFriend(player.PlayerId, out int_1);
							if (friend != null)
							{
								MessageModel messageModel = method_0(player.Nickname, account.PlayerId, Client.PlayerId);
								if (messageModel != null)
								{
									account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: true);
								}
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((num == 0) ? FriendChangeState.Insert : FriendChangeState.Update, friend, int_1), OnlyInServer: false);
							}
							FriendModel friend2 = player.Friend.GetFriend(account.PlayerId, out int_0);
							if (friend2 != null)
							{
								Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, friend2, int_0));
							}
						}
						else
						{
							Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487801u));
						}
					}
					else
					{
						Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487809u));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487810u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487799u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private MessageModel method_0(string string_1, long long_0, long long_1)
	{
		MessageModel messageModel = new MessageModel(7.0)
		{
			SenderId = long_1,
			SenderName = string_1,
			Type = NoteMessageType.Insert,
			State = NoteMessageState.Unreaded
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			return null;
		}
		return messageModel;
	}
}
