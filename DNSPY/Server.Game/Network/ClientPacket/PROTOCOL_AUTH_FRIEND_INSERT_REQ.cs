using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000122 RID: 290
	public class PROTOCOL_AUTH_FRIEND_INSERT_REQ : GameClientPacket
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00004CC2 File Offset: 0x00002EC2
		public override void Read()
		{
			this.string_0 = base.ReadU(66);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000147EC File Offset: 0x000129EC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Nickname.Length != 0 && !(player.Nickname == this.string_0))
					{
						if (player.Friend.Friends.Count >= 50)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487800U));
						}
						else
						{
							Account account = AccountManager.GetAccount(this.string_0, 1, 287);
							if (account != null)
							{
								if (player.Friend.GetFriendIdx(account.PlayerId) == -1)
								{
									if (account.Friend.Friends.Count >= 50)
									{
										this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487800U));
									}
									else
									{
										int num = AllUtils.AddFriend(account, player, 2);
										if (AllUtils.AddFriend(player, account, (num != 1) ? 1 : 0) != -1)
										{
											if (num != -1)
											{
												FriendModel friend = account.Friend.GetFriend(player.PlayerId, out this.int_1);
												if (friend != null)
												{
													MessageModel messageModel = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
													if (messageModel != null)
													{
														account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), true);
													}
													account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((num == 0) ? FriendChangeState.Insert : FriendChangeState.Update, friend, this.int_1), false);
												}
												FriendModel friend2 = player.Friend.GetFriend(account.PlayerId, out this.int_0);
												if (friend2 != null)
												{
													this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, friend2, this.int_0));
													goto IL_1E3;
												}
												goto IL_1E3;
											}
										}
										this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487801U));
									}
								}
								else
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487809U));
								}
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487810U));
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INSERT_ACK(2147487799U));
					}
					IL_1E3:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00014A18 File Offset: 0x00012C18
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

		// Token: 0x060002BC RID: 700 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_FRIEND_INSERT_REQ()
		{
		}

		// Token: 0x04000208 RID: 520
		private string string_0;

		// Token: 0x04000209 RID: 521
		private int int_0;

		// Token: 0x0400020A RID: 522
		private int int_1;
	}
}
