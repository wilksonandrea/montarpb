using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000055 RID: 85
	public class FriendInfo
	{
		// Token: 0x06000138 RID: 312 RVA: 0x0000B76C File Offset: 0x0000996C
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			int num2 = (int)C.ReadC();
			long num3 = C.ReadQ();
			long num4 = C.ReadQ();
			Account account = AccountManager.GetAccount(num3, true);
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
						if (num5 != -1 && friend != null)
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

		// Token: 0x06000139 RID: 313 RVA: 0x00002409 File Offset: 0x00000609
		public FriendInfo()
		{
		}
	}
}
