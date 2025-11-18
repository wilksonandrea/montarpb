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
	// Token: 0x02000125 RID: 293
	public class PROTOCOL_AUTH_FRIEND_DELETE_REQ : GameClientPacket
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00014D4C File Offset: 0x00012F4C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					FriendModel friend = player.Friend.GetFriend(this.int_0);
					if (friend != null)
					{
						DaoManagerSQL.DeletePlayerFriend(friend.PlayerId, player.PlayerId);
						Account account = AccountManager.GetAccount(friend.PlayerId, 287);
						if (account != null)
						{
							int num = -1;
							FriendModel friend2 = account.Friend.GetFriend(player.PlayerId, out num);
							if (friend2 != null)
							{
								friend2.Removed = true;
								DaoManagerSQL.UpdatePlayerFriendBlock(account.PlayerId, friend2);
								SendFriendInfo.Load(account, friend2, 2);
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend2, num), false);
							}
						}
						player.Friend.RemoveFriend(friend);
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Delete, null, 0, this.int_0));
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_DELETE_ACK(this.uint_0));
					this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_FRIEND_DELETE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_FRIEND_DELETE_REQ()
		{
		}

		// Token: 0x0400020F RID: 527
		private int int_0;

		// Token: 0x04000210 RID: 528
		private uint uint_0;
	}
}
