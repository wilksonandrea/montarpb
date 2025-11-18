using System;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000059 RID: 89
	public static class PlayerSync
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000B968 File Offset: 0x00009B68
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
