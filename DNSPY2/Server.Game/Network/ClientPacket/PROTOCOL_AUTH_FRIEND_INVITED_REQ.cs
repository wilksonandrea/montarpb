using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000126 RID: 294
	public class PROTOCOL_AUTH_FRIEND_INVITED_REQ : GameClientPacket
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00004D06 File Offset: 0x00002F06
		public override void Read()
		{
			this.string_0 = base.ReadU(66);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00014E94 File Offset: 0x00013094
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
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487800U));
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
										this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487800U));
									}
									else
									{
										int num = AllUtils.AddFriend(account, player, 2);
										if (AllUtils.AddFriend(player, account, (num != 1) ? 1 : 0) != -1)
										{
											if (num != -1)
											{
												int num2;
												FriendModel friend = account.Friend.GetFriend(player.PlayerId, out num2);
												if (friend != null)
												{
													account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((num == 0) ? FriendChangeState.Insert : FriendChangeState.Update, friend, num2), false);
												}
												int num3;
												FriendModel friend2 = player.Friend.GetFriend(account.PlayerId, out num3);
												if (friend2 != null)
												{
													this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, friend2, num3));
													goto IL_19C;
												}
												goto IL_19C;
											}
										}
										this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487801U));
									}
								}
								else
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487809U));
								}
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487810U));
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487799U));
					}
					IL_19C:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_FRIEND_INVITED_REQ()
		{
		}

		// Token: 0x04000211 RID: 529
		private string string_0;
	}
}
