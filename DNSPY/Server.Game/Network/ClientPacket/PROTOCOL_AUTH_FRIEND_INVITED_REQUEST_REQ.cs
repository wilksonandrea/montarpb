using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000127 RID: 295
	public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ : GameClientPacket
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00004D16 File Offset: 0x00002F16
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00015078 File Offset: 0x00013278
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = this.method_0(player);
					if (account != null)
					{
						if (account.Status.ServerId != 255 && account.Status.ServerId != 0)
						{
							if (account.MatchSlot >= 0)
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147495939U));
							}
							else
							{
								int friendIdx = account.Friend.GetFriendIdx(player.PlayerId);
								if (friendIdx == -1)
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487806U));
								}
								else if (account.IsOnline)
								{
									account.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(friendIdx), false);
								}
								else
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487807U));
								}
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147495938U));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(2147487805U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000151A0 File Offset: 0x000133A0
		private Account method_0(Account account_0)
		{
			FriendModel friend = account_0.Friend.GetFriend(this.int_0);
			if (friend == null)
			{
				return null;
			}
			return AccountManager.GetAccount(friend.PlayerId, 287);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ()
		{
		}

		// Token: 0x04000212 RID: 530
		private int int_0;
	}
}
