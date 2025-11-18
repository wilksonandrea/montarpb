using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly SlotModel slotModel_0;

	public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(RoomModel roomModel_1, SlotModel slotModel_1)
	{
		roomModel_0 = roomModel_1;
		slotModel_0 = slotModel_1;
	}

	public override void Write()
	{
		WriteH(5179);
		WriteH((ushort)roomModel_0.FRDino);
		WriteH((ushort)roomModel_0.CTDino);
		WriteD(slotModel_0.Id);
		WriteH((short)slotModel_0.PassSequence);
	}
}
