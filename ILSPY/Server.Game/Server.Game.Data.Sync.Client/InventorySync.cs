using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client;

public class InventorySync
{
	public static void Load(SyncClientPacket C)
	{
		long id = C.ReadQ();
		long objectId = C.ReadQ();
		int ıd = C.ReadD();
		ItemEquipType equip = (ItemEquipType)C.ReadC();
		ItemCategory category = (ItemCategory)C.ReadC();
		uint count = C.ReadUD();
		byte length = C.ReadC();
		string name = C.ReadS(length);
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account != null)
		{
			ItemsModel ıtem = account.Inventory.GetItem(objectId);
			if (ıtem == null)
			{
				ItemsModel ıtem2 = new ItemsModel
				{
					ObjectId = objectId,
					Id = ıd,
					Equip = equip,
					Count = count,
					Category = category,
					Name = name
				};
				account.Inventory.AddItem(ıtem2);
			}
			else
			{
				ıtem.Count = count;
			}
		}
	}
}
