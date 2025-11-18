using System;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F6 RID: 502
	public static class PlayerSync
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0002FB70 File Offset: 0x0002DD70
		public static void Load(SyncClientPacket C)
		{
			long num = C.ReadQ();
			int num2 = (int)C.ReadC();
			int num3 = (int)C.ReadC();
			int num4 = C.ReadD();
			int num5 = C.ReadD();
			int num6 = C.ReadD();
			Account account = AccountManager.GetAccount(num, true);
			if (account == null)
			{
				return;
			}
			if (num2 == 0)
			{
				account.Rank = num3;
				account.Gold = num4;
				account.Cash = num5;
				account.Tags = num6;
			}
		}
	}
}
