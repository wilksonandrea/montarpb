using System.Collections.Generic;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_CAPSULE_ACK : GameServerPacket
{
	private readonly List<ItemsModel> list_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(ItemsModel itemsModel_0, int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
		list_0 = new List<ItemsModel>();
		ItemsModel ıtemsModel = new ItemsModel(itemsModel_0);
		if (ıtemsModel != null)
		{
			list_0.Add(ıtemsModel);
		}
	}

	public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(List<ItemsModel> list_1, int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
		list_0 = new List<ItemsModel>();
		foreach (ItemsModel item in list_1)
		{
			ItemsModel ıtemsModel = new ItemsModel(item);
			if (ıtemsModel != null)
			{
				list_0.Add(ıtemsModel);
			}
		}
	}

	public override void Write()
	{
		WriteH(1064);
		WriteH(0);
		WriteC(1);
		WriteC((byte)int_1);
		WriteC((byte)list_0.Count);
		foreach (ItemsModel item in list_0)
		{
			WriteD(item.Id);
		}
	}
}
