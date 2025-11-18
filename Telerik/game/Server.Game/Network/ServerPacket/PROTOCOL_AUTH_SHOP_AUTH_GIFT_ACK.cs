using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly List<ItemsModel> list_0 = new List<ItemsModel>();

		private readonly List<ItemsModel> list_1 = new List<ItemsModel>();

		private readonly List<ItemsModel> list_2 = new List<ItemsModel>();

		private readonly List<ItemsModel> list_3 = new List<ItemsModel>();

		public PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(uint uint_1, ItemsModel itemsModel_0 = null, Account account_0 = null)
		{
			this.uint_0 = uint_1;
			ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
			if (ıtemsModel != null)
			{
				ComDiv.TryCreateItem(ıtemsModel, account_0.Inventory, account_0.PlayerId);
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
					return;
				}
				if (ıtemsModel.Category == ItemCategory.NewItem)
				{
					this.list_3.Add(ıtemsModel);
				}
			}
		}

		public override void Write()
		{
			base.WriteH(1054);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 1)
			{
				base.WriteH(0);
				base.WriteH((ushort)(this.list_0.Count + this.list_1.Count + this.list_2.Count + this.list_3.Count));
				foreach (ItemsModel list0 in this.list_0)
				{
					base.WriteD((uint)list0.ObjectId);
					base.WriteD(list0.Id);
					base.WriteC((byte)list0.Equip);
					base.WriteD(list0.Count);
				}
				foreach (ItemsModel list1 in this.list_1)
				{
					base.WriteD((uint)list1.ObjectId);
					base.WriteD(list1.Id);
					base.WriteC((byte)list1.Equip);
					base.WriteD(list1.Count);
				}
				foreach (ItemsModel list2 in this.list_2)
				{
					base.WriteD((uint)list2.ObjectId);
					base.WriteD(list2.Id);
					base.WriteC((byte)list2.Equip);
					base.WriteD(list2.Count);
				}
				foreach (ItemsModel list3 in this.list_3)
				{
					base.WriteD((uint)list3.ObjectId);
					base.WriteD(list3.Id);
					base.WriteC((byte)list3.Equip);
					base.WriteD(list3.Count);
				}
			}
		}
	}
}