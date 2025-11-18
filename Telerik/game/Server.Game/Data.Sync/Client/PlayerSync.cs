using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System;

namespace Server.Game.Data.Sync.Client
{
	public static class PlayerSync
	{
		public static void Load(SyncClientPacket C)
		{
			long ınt64 = C.ReadQ();
			int ınt32 = C.ReadC();
			int ınt321 = C.ReadC();
			int ınt322 = C.ReadD();
			int ınt323 = C.ReadD();
			int ınt324 = C.ReadD();
			Account account = AccountManager.GetAccount(ınt64, true);
			if (account == null)
			{
				return;
			}
			if (ınt32 == 0)
			{
				account.Rank = ınt321;
				account.Gold = ınt322;
				account.Cash = ınt323;
				account.Tags = ınt324;
			}
		}
	}
}