using System;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F1 RID: 497
	public static class ClanSync
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x0002F854 File Offset: 0x0002DA54
		public static void Load(SyncClientPacket C)
		{
			long num = C.ReadQ();
			int num2 = (int)C.ReadC();
			Account account = AccountManager.GetAccount(num, true);
			if (account == null)
			{
				return;
			}
			if (num2 == 3)
			{
				int num3 = C.ReadD();
				int num4 = (int)C.ReadC();
				account.ClanId = num3;
				account.ClanAccess = num4;
			}
		}
	}
}
