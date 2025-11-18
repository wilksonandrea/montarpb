using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F0 RID: 496
	public class ClanServersSync
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0002F7C0 File Offset: 0x0002D9C0
		public static void Load(SyncClientPacket C)
		{
			bool flag = C.ReadC() != 0;
			int num = C.ReadD();
			ClanModel clan = ClanManager.GetClan(num);
			if (flag)
			{
				if (clan != null)
				{
					ClanManager.RemoveClan(clan);
				}
				return;
			}
			if (clan != null)
			{
				return;
			}
			long num2 = C.ReadQ();
			uint num3 = C.ReadUD();
			string text = C.ReadS((int)C.ReadC());
			string text2 = C.ReadS((int)C.ReadC());
			ClanManager.AddClan(new ClanModel
			{
				Id = num,
				Name = text,
				OwnerId = num2,
				Logo = 0U,
				Info = text2,
				CreationDate = num3
			});
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000025DF File Offset: 0x000007DF
		public ClanServersSync()
		{
		}
	}
}
