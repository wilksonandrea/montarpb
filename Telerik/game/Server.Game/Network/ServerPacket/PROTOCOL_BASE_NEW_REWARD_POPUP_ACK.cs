using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_NEW_REWARD_POPUP_ACK : GameServerPacket
	{
		private readonly ItemsModel itemsModel_0;

		private readonly PlayerInventory playerInventory_0;

		private readonly List<ItemsModel> list_0;

		public PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Account account_0, ItemsModel itemsModel_1)
		{
			this.itemsModel_0 = itemsModel_1;
			if (account_0 != null)
			{
				this.playerInventory_0 = account_0.Inventory;
				this.list_0 = new List<ItemsModel>();
				if (itemsModel_1 != null)
				{
					this.list_0.Add(itemsModel_1);
				}
			}
		}

		public override void Write()
		{
			base.WriteH(2430);
			base.WriteH(0);
			base.WriteH((ushort)this.playerInventory_0.Items.Count);
			base.WriteC(1);
			base.WriteD(this.itemsModel_0.Id);
		}
	}
}