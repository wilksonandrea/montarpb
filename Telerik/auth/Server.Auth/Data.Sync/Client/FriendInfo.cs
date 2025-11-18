using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Data.Sync.Client
{
	public class FriendInfo
	{
		public FriendInfo()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadC();
			int ınt321 = C.ReadC();
			long ınt64 = C.ReadQ();
			long ınt641 = C.ReadQ();
			Account account = AccountManager.GetAccount(ınt64, true);
			if (account != null)
			{
				Account account1 = AccountManager.GetAccount(ınt641, true);
				if (account1 != null)
				{
					FriendState friendState = (ınt321 == 1 ? FriendState.Online : FriendState.Offline);
					if (ınt32 != 0)
					{
						account1.SendPacket(new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account, friendState));
					}
					else
					{
						int ınt322 = -1;
						FriendModel friend = account1.Friend.GetFriend(account.PlayerId, out ınt322);
						if (ınt322 != -1 && friend != null)
						{
							account1.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, friendState, ınt322));
							return;
						}
					}
				}
			}
		}
	}
}