using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F5 RID: 501
	public class InventorySync
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0002FABC File Offset: 0x0002DCBC
		public static void Load(SyncClientPacket C)
		{
			long num = C.ReadQ();
			long num2 = C.ReadQ();
			int num3 = C.ReadD();
			ItemEquipType itemEquipType = (ItemEquipType)C.ReadC();
			ItemCategory itemCategory = (ItemCategory)C.ReadC();
			uint num4 = C.ReadUD();
			byte b = C.ReadC();
			string text = C.ReadS((int)b);
			Account account = AccountManager.GetAccount(num, true);
			if (account == null)
			{
				return;
			}
			ItemsModel item = account.Inventory.GetItem(num2);
			if (item == null)
			{
				ItemsModel itemsModel = new ItemsModel
				{
					ObjectId = num2,
					Id = num3,
					Equip = itemEquipType,
					Count = num4,
					Category = itemCategory,
					Name = text
				};
				account.Inventory.AddItem(itemsModel);
				return;
			}
			item.Count = num4;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000025DF File Offset: 0x000007DF
		public InventorySync()
		{
		}
	}
}
