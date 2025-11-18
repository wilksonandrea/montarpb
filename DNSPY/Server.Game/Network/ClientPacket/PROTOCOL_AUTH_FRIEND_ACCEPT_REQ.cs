using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000124 RID: 292
	public class PROTOCOL_AUTH_FRIEND_ACCEPT_REQ : GameClientPacket
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00004CEA File Offset: 0x00002EEA
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00014B20 File Offset: 0x00012D20
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					FriendModel friend = player.Friend.GetFriend(this.int_0);
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
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Accept, null, 0, this.int_0));
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, this.int_0));
							int num = -1;
							FriendModel friend2 = account.Friend.GetFriend(player.PlayerId, out num);
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
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend2, num), false);
							}
						}
						else
						{
							this.uint_0 = 2147483648U;
						}
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
					if (this.uint_0 > 0U)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(this.uint_0));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_FRIEND_ACCEPT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_FRIEND_ACCEPT_REQ()
		{
		}

		// Token: 0x0400020D RID: 525
		private int int_0;

		// Token: 0x0400020E RID: 526
		private uint uint_0;
	}
}
