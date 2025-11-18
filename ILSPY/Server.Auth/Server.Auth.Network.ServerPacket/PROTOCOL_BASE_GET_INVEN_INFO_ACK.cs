using System.Collections.Generic;
using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_INVEN_INFO_ACK : AuthServerPacket
{
	private readonly uint uint_0;

	private readonly int int_0;

	private readonly List<ItemsModel> list_0;

	public PROTOCOL_BASE_GET_INVEN_INFO_ACK(uint uint_1, List<ItemsModel> list_1, int int_1)
	{
		uint_0 = uint_1;
		list_0 = list_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(2319);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 != 0)
		{
			return;
		}
		WriteH((ushort)list_0.Count);
		foreach (ItemsModel item in list_0)
		{
			WriteD((uint)item.ObjectId);
			WriteD(item.Id);
			WriteC((byte)item.Equip);
			WriteD(item.Count);
		}
		WriteH((ushort)int_0);
		WriteH((ushort)list_0.Count);
		WriteH((ushort)list_0.Count);
		WriteH((ushort)list_0.Count);
		WriteH(0);
	}
}
