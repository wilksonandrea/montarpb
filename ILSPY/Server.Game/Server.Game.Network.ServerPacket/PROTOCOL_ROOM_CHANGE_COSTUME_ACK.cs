using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_COSTUME_ACK : GameServerPacket
{
	private readonly SlotModel slotModel_0;

	public PROTOCOL_ROOM_CHANGE_COSTUME_ACK(SlotModel slotModel_1)
	{
		slotModel_0 = slotModel_1;
	}

	public override void Write()
	{
		WriteH(3678);
		WriteD(slotModel_0.Id);
		WriteC((byte)slotModel_0.CostumeTeam);
	}
}
