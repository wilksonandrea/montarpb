using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Update;
using System;

namespace Server.Auth.Data.Sync.Client
{
	public static class ClanSync
	{
		public static void Load(SyncClientPacket C)
		{
			long ınt64;
			long ınt641 = C.ReadQ();
			int ınt32 = C.ReadC();
			Account account = AccountManager.GetAccount(ınt641, true);
			if (account == null)
			{
				return;
			}
			if (ınt32 == 0)
			{
				ClanInfo.ClearList(account);
				return;
			}
			if (ınt32 != 1)
			{
				if (ınt32 == 2)
				{
					ınt64 = C.ReadQ();
					ClanInfo.RemoveMember(account, ınt64);
					return;
				}
				if (ınt32 == 3)
				{
					int ınt321 = C.ReadD();
					int ınt322 = C.ReadC();
					account.ClanId = ınt321;
					account.ClanAccess = ınt322;
				}
				return;
			}
			ınt64 = C.ReadQ();
			string str = C.ReadS((int)C.ReadC());
			byte[] numArray = C.ReadB(4);
			byte num = C.ReadC();
			Account account1 = new Account()
			{
				PlayerId = ınt64,
				Nickname = str,
				Rank = num
			};
			account1.Status.SetData(numArray, ınt64);
			ClanInfo.AddMember(account, account1);
		}
	}
}