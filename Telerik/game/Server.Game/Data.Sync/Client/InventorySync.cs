using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class InventorySync
	{
		public InventorySync()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			long ınt64 = C.ReadQ();
			long ınt641 = C.ReadQ();
			int ınt32 = C.ReadD();
			ItemEquipType ıtemEquipType = (ItemEquipType)C.ReadC();
			ItemCategory ıtemCategory = (ItemCategory)C.ReadC();
			uint uInt32 = C.ReadUD();
			string str = C.ReadS((int)C.ReadC());
			Account account = AccountManager.GetAccount(ınt64, true);
			if (account == null)
			{
				return;
			}
			ItemsModel ıtem = account.Inventory.GetItem(ınt641);
			if (ıtem != null)
			{
				ıtem.Count = uInt32;
				return;
			}
			ItemsModel ıtemsModel = new ItemsModel()
			{
				ObjectId = ınt641,
				Id = ınt32,
				Equip = ıtemEquipType,
				Count = uInt32,
				Category = ıtemCategory,
				Name = str
			};
			account.Inventory.AddItem(ıtemsModel);
		}
	}
}