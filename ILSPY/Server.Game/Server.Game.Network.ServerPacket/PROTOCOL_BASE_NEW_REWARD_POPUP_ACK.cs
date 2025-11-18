using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_NEW_REWARD_POPUP_ACK : GameServerPacket
{
	private readonly ItemsModel itemsModel_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly List<ItemsModel> list_0;

	public PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Account account_0, ItemsModel itemsModel_1)
	{
		itemsModel_0 = itemsModel_1;
		if (account_0 != null)
		{
			playerInventory_0 = account_0.Inventory;
			list_0 = new List<ItemsModel>();
			if (itemsModel_1 != null)
			{
				list_0.Add(itemsModel_1);
			}
		}
	}

	public override void Write()
	{
		WriteH(2430);
		WriteH(0);
		WriteH((ushort)playerInventory_0.Items.Count);
		WriteC(1);
		WriteD(itemsModel_0.Id);
	}
}
