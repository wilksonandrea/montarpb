using System;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Update;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000053 RID: 83
	public static class ClanSync
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000B618 File Offset: 0x00009818
		public static void Load(SyncClientPacket C)
		{
			long num = C.ReadQ();
			int num2 = (int)C.ReadC();
			Account account = AccountManager.GetAccount(num, true);
			if (account == null)
			{
				return;
			}
			if (num2 == 0)
			{
				ClanInfo.ClearList(account);
				return;
			}
			if (num2 == 1)
			{
				long num3 = C.ReadQ();
				string text = C.ReadS((int)C.ReadC());
				byte[] array = C.ReadB(4);
				byte b = C.ReadC();
				Account account2 = new Account
				{
					PlayerId = num3,
					Nickname = text,
					Rank = (int)b
				};
				account2.Status.SetData(array, num3);
				ClanInfo.AddMember(account, account2);
				return;
			}
			if (num2 == 2)
			{
				long num3 = C.ReadQ();
				ClanInfo.RemoveMember(account, num3);
				return;
			}
			if (num2 == 3)
			{
				int num4 = C.ReadD();
				int num5 = (int)C.ReadC();
				account.ClanId = num4;
				account.ClanAccess = num5;
			}
		}
	}
}
