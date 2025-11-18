using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_FRIEND_INSERT_REQ : GameClientPacket
	{
		private string string_0;

		private int int_0;

		private int int_1;

		public PROTOCOL_AUTH_FRIEND_INSERT_REQ()
		{
		}

		private MessageModel method_0(string string_1, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(7)
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

		public override void Read()
		{
			this.string_0 = base.ReadU(66);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Nickname.Length == 0 || player.Nickname == this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479497));
					}
					else if (player.Friend.Friends.Count < 50)
					{
						Account account = AccountManager.GetAccount(this.string_0, 1, 287);
						if (account == null)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479486));
						}
						else if (player.Friend.GetFriendIdx(account.PlayerId) != -1)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479487));
						}
						else if (account.Friend.Friends.Count < 50)
						{
							int ınt32 = AllUtils.AddFriend(account, player, 2);
							if (AllUtils.AddFriend(player, account, ınt32 != 1) != -1)
							{
								if (ınt32 == -1)
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479495));
									return;
								}
								FriendModel friend = account.Friend.GetFriend(player.PlayerId, out this.int_1);
								if (friend != null)
								{
									MessageModel messageModel = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
									if (messageModel != null)
									{
										account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), true);
									}
									account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((ınt32 == 0 ? FriendChangeState.Insert : FriendChangeState.Update), friend, this.int_1), false);
								}
								FriendModel friendModel = player.Friend.GetFriend(account.PlayerId, out this.int_0);
								if (friendModel != null)
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, friendModel, this.int_0));
									goto Label1;
								}
								else
								{
									goto Label1;
								}
							}
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479495));
							return;
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479496));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(-2147479496));
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_FRIEND_INVITED_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}