using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_REPAIR_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly List<ItemsModel> list_0;

	private readonly Account account_0;

	public PROTOCOL_SHOP_REPAIR_ACK(uint uint_1, List<ItemsModel> list_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(1077);
		WriteH(0);
		if (uint_0 == 1)
		{
			WriteC((byte)list_0.Count);
			foreach (ItemsModel item in list_0)
			{
				WriteD((uint)item.ObjectId);
				WriteD(item.Id);
			}
			WriteD(account_0.Cash);
			WriteD(account_0.Gold);
		}
		else
		{
			WriteD(uint_0);
		}
	}
}
