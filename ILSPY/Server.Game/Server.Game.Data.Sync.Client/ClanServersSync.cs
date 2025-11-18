using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;

namespace Server.Game.Data.Sync.Client;

public class ClanServersSync
{
	public static void Load(SyncClientPacket C)
	{
		byte num = C.ReadC();
		int num2 = C.ReadD();
		ClanModel clan = ClanManager.GetClan(num2);
		if (num == 0)
		{
			if (clan == null)
			{
				long ownerId = C.ReadQ();
				uint creationDate = C.ReadUD();
				string name = C.ReadS(C.ReadC());
				string ınfo = C.ReadS(C.ReadC());
				ClanManager.AddClan(new ClanModel
				{
					Id = num2,
					Name = name,
					OwnerId = ownerId,
					Logo = 0u,
					Info = ınfo,
					CreationDate = creationDate
				});
			}
		}
		else if (clan != null)
		{
			ClanManager.RemoveClan(clan);
		}
	}
}
