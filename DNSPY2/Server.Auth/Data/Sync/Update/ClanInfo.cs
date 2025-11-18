using System;
using System.Collections.Generic;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Update
{
	// Token: 0x0200004D RID: 77
	public class ClanInfo
	{
		// Token: 0x06000126 RID: 294 RVA: 0x0000B114 File Offset: 0x00009314
		public static void AddMember(Account Player, Account Member)
		{
			List<Account> clanPlayers = Player.ClanPlayers;
			lock (clanPlayers)
			{
				Player.ClanPlayers.Add(Member);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000B15C File Offset: 0x0000935C
		public static void RemoveMember(Account Player, long PlayerId)
		{
			List<Account> clanPlayers = Player.ClanPlayers;
			lock (clanPlayers)
			{
				for (int i = 0; i < Player.ClanPlayers.Count; i++)
				{
					if (Player.ClanPlayers[i].PlayerId == PlayerId)
					{
						Player.ClanPlayers.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public static void ClearList(Account Player)
		{
			List<Account> clanPlayers = Player.ClanPlayers;
			lock (clanPlayers)
			{
				Player.ClanId = 0;
				Player.ClanAccess = 0;
				Player.ClanPlayers.Clear();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00002409 File Offset: 0x00000609
		public ClanInfo()
		{
		}
	}
}
