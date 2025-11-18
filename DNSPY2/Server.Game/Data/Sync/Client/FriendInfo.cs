using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F3 RID: 499
	public class FriendInfo
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0002F92C File Offset: 0x0002DB2C
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			int num2 = (int)C.ReadC();
			long num3 = C.ReadQ();
			long num4 = C.ReadQ();
			Account account = AccountManager.GetAccount(num3, 31);
			if (account != null)
			{
				Account account2 = AccountManager.GetAccount(num4, true);
				if (account2 != null)
				{
					FriendState friendState = ((num2 == 1) ? FriendState.Online : FriendState.Offline);
					if (num == 0)
					{
						int num5 = -1;
						FriendModel friend = account2.Friend.GetFriend(account.PlayerId, out num5);
						if (num5 != -1 && friend != null && friend.State == 0)
						{
							account2.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, friendState, num5));
							return;
						}
					}
					else
					{
						account2.SendPacket(new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account, friendState));
					}
				}
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000025DF File Offset: 0x000007DF
		public FriendInfo()
		{
		}
	}
}
