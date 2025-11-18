using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class ClanServersSync
	{
		public ClanServersSync()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			byte num = C.ReadC();
			int ınt32 = C.ReadD();
			ClanModel clan = ClanManager.GetClan(ınt32);
			if (num != 0)
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
			long ınt64 = C.ReadQ();
			uint uInt32 = C.ReadUD();
			string str = C.ReadS((int)C.ReadC());
			string str1 = C.ReadS((int)C.ReadC());
			ClanManager.AddClan(new ClanModel()
			{
				Id = ınt32,
				Name = str,
				OwnerId = ınt64,
				Logo = 0,
				Info = str1,
				CreationDate = uInt32
			});
		}
	}
}