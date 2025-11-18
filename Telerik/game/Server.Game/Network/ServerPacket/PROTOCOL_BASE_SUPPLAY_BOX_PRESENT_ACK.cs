using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly List<ItemsModel> list_0 = new List<ItemsModel>();

		private readonly List<ItemsModel> list_1 = new List<ItemsModel>();

		private readonly List<ItemsModel> list_2 = new List<ItemsModel>();

		public PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK(uint uint_1, ItemsModel itemsModel_0, Account account_0)
		{
			this.uint_0 = uint_1;
			ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
			if (ıtemsModel != null)
			{
				ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
				SendItemInfo.LoadItem(account_0, ıtemsModel);
				if (ıtemsModel.Category == ItemCategory.Weapon)
				{
					this.list_1.Add(ıtemsModel);
					return;
				}
				if (ıtemsModel.Category == ItemCategory.Character)
				{
					this.list_0.Add(ıtemsModel);
					return;
				}
				if (ıtemsModel.Category == ItemCategory.Coupon)
				{
					this.list_2.Add(ıtemsModel);
				}
			}
		}

		public override void Write()
		{
			base.WriteH(2411);
			base.WriteD(this.list_0.Count);
			base.WriteD(this.list_1.Count);
			base.WriteD(this.list_2.Count);
			base.WriteD(0);
			foreach (ItemsModel list0 in this.list_0)
			{
				base.WriteQ(list0.ObjectId);
				base.WriteD(list0.Id);
				base.WriteC((byte)list0.Equip);
				base.WriteD(list0.Count);
			}
			foreach (ItemsModel list1 in this.list_1)
			{
				base.WriteQ(list1.ObjectId);
				base.WriteD(list1.Id);
				base.WriteC((byte)list1.Equip);
				base.WriteD(list1.Count);
			}
			foreach (ItemsModel list2 in this.list_2)
			{
				base.WriteQ(list2.ObjectId);
				base.WriteD(list2.Id);
				base.WriteC((byte)list2.Equip);
				base.WriteD(list2.Count);
			}
		}
	}
}