using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_ROUND_START_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5153);
		WriteC((byte)roomModel_0.Rounds);
		WriteD(roomModel_0.GetInBattleTimeLeft());
		WriteD(AllUtils.GetSlotsFlag(roomModel_0, OnlyNoSpectators: true, MissionSuccess: false));
		WriteC((byte)(roomModel_0.SwapRound ? 3u : 0u));
		WriteH((ushort)roomModel_0.FRRounds);
		WriteH((ushort)roomModel_0.CTRounds);
		if (roomModel_0.SwapRound)
		{
			WriteB(method_0(roomModel_0));
		}
	}

	private byte[] method_0(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slotModel in slots)
		{
			PlayerEquipment equipment = slotModel.Equipment;
			if (equipment != null)
			{
				if (slotModel.Team == TeamEnum.FR_TEAM)
				{
					syncServerPacket.WriteD((!roomModel_1.SwapRound) ? equipment.CharaRedId : equipment.CharaBlueId);
				}
				else if (slotModel.Team == TeamEnum.CT_TEAM)
				{
					syncServerPacket.WriteD((!roomModel_1.SwapRound) ? equipment.CharaBlueId : equipment.CharaRedId);
				}
			}
			else if (slotModel.Team == TeamEnum.FR_TEAM)
			{
				syncServerPacket.WriteD((!roomModel_1.SwapRound) ? 601001 : 602002);
			}
			else if (slotModel.Team == TeamEnum.CT_TEAM)
			{
				syncServerPacket.WriteD((!roomModel_1.SwapRound) ? 602002 : 601001);
			}
		}
		return syncServerPacket.ToArray();
	}
}
