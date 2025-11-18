using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System;

namespace Server.Game.Data.Sync.Client
{
	public static class ClanSync
	{
		public static void Load(SyncClientPacket C)
		{
			long ınt64 = C.ReadQ();
			int ınt32 = C.ReadC();
			Account account = AccountManager.GetAccount(ınt64, true);
			if (account == null)
			{
				return;
			}
			if (ınt32 == 3)
			{
				int ınt321 = C.ReadD();
				int ınt322 = C.ReadC();
				account.ClanId = ınt321;
				account.ClanAccess = ınt322;
			}
		}
	}
}