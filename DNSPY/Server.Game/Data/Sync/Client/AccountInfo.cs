using System;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001EE RID: 494
	public class AccountInfo
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x0002F724 File Offset: 0x0002D924
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

		// Token: 0x060005CF RID: 1487 RVA: 0x000025DF File Offset: 0x000007DF
		public AccountInfo()
		{
		}
	}
}
