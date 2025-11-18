using System;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000051 RID: 81
	public class AccountInfo
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000B590 File Offset: 0x00009790
		public static void Load(SyncClientPacket C)
		{
			long num = C.ReadQ();
			int num2 = (int)C.ReadC();
			string text = C.ReadS((int)C.ReadC());
			byte[] array = C.ReadB((int)C.ReadUH());
			Account account = AccountManager.GetAccount(num, true);
			if (account != null)
			{
				if (num2 == 0)
				{
					account.SendPacket(array, text);
					return;
				}
				account.SendCompletePacket(array, text);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00002409 File Offset: 0x00000609
		public AccountInfo()
		{
		}
	}
}
