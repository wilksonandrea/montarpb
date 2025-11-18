using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_SHOP_CAPSULE_ACK : GameServerPacket
	{
		private readonly List<ItemsModel> list_0;

		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(ItemsModel itemsModel_0, int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.list_0 = new List<ItemsModel>();
			ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
			if (ıtemsModel != null)
			{
				this.list_0.Add(ıtemsModel);
			}
		}

		public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(List<ItemsModel> list_1, int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.list_0 = new List<ItemsModel>();
			foreach (ItemsModel list1 in list_1)
			{
				ItemsModel ıtemsModel = new ItemsModel(list1);
				if (ıtemsModel == null)
				{
					continue;
				}
				this.list_0.Add(ıtemsModel);
			}
		}

		public override void Write()
		{
			base.WriteH(1064);
			base.WriteH(0);
			base.WriteC(1);
			base.WriteC((byte)this.int_1);
			base.WriteC((byte)this.list_0.Count);
			foreach (ItemsModel list0 in this.list_0)
			{
				base.WriteD(list0.Id);
			}
		}
	}
}