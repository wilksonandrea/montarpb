using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Data.Sync.Client;

public class FriendInfo
{
	public static void Load(SyncClientPacket C)
	{
		int num = C.ReadC();
		int num2 = C.ReadC();
		long id = C.ReadQ();
		long id2 = C.ReadQ();
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account == null)
		{
			return;
		}
		Account account2 = AccountManager.GetAccount(id2, noUseDB: true);
		if (account2 == null)
		{
			return;
		}
		FriendState friendState = ((num2 == 1) ? FriendState.Online : FriendState.Offline);
		if (num == 0)
		{
			int index = -1;
			FriendModel friend = account2.Friend.GetFriend(account.PlayerId, out index);
			if (index != -1 && friend != null)
			{
				account2.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, friendState, index));
			}
		}
		else
		{
			account2.SendPacket(new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account, friendState));
		}
	}
}
