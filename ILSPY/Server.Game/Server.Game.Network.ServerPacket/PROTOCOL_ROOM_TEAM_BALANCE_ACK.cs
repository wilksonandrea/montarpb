using System.Collections.Generic;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_TEAM_BALANCE_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly List<SlotChange> list_0;

	public PROTOCOL_ROOM_TEAM_BALANCE_ACK(List<SlotChange> list_1, int int_2, int int_3)
	{
		list_0 = list_1;
		int_1 = int_2;
		int_0 = int_3;
	}

	public override void Write()
	{
		WriteH(3622);
		WriteC((byte)int_0);
		WriteC((byte)int_1);
		WriteC((byte)list_0.Count);
		foreach (SlotChange item in list_0)
		{
			WriteC((byte)item.OldSlot.Id);
			WriteC((byte)item.NewSlot.Id);
			WriteC((byte)item.OldSlot.State);
			WriteC((byte)item.NewSlot.State);
		}
	}
}
