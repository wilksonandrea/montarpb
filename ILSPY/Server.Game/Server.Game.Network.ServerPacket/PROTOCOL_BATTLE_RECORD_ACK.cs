using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_RECORD_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_RECORD_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5163);
		WriteH((ushort)roomModel_0.FRKills);
		WriteH((ushort)roomModel_0.FRDeaths);
		WriteH((ushort)roomModel_0.FRAssists);
		WriteH((ushort)roomModel_0.CTKills);
		WriteH((ushort)roomModel_0.CTDeaths);
		WriteH((ushort)roomModel_0.CTAssists);
		SlotModel[] slots = roomModel_0.Slots;
		foreach (SlotModel slotModel in slots)
		{
			WriteH((ushort)slotModel.AllKills);
			WriteH((ushort)slotModel.AllDeaths);
			WriteH((ushort)slotModel.AllAssists);
		}
	}
}
