using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly List<int> list_0;

	public PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(RoomModel roomModel_1, List<int> list_1)
	{
		roomModel_0 = roomModel_1;
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(5151);
		WriteD(AllUtils.GetSlotsFlag(roomModel_0, OnlyNoSpectators: false, MissionSuccess: false));
		WriteB(method_0(roomModel_0, list_0));
		WriteC((byte)(roomModel_0.SwapRound ? 3u : 0u));
		if (roomModel_0.SwapRound)
		{
			WriteB(method_1(roomModel_0));
		}
	}

	private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (roomModel_1.IsBotMode())
		{
			syncServerPacket.WriteB(Bitwise.HexStringToByteArray("FF FF FF FF FF FF FF FF FF FF"));
		}
		else if (roomModel_1.IsDinoMode())
		{
			int num = ((list_1.Count == 1 || roomModel_1.IsDinoMode("CC")) ? 255 : roomModel_1.TRex);
			syncServerPacket.WriteC((byte)num);
			syncServerPacket.WriteC(10);
			for (int i = 0; i < list_1.Count; i++)
			{
				int num2 = list_1[i];
				if ((num2 != roomModel_1.TRex && roomModel_1.IsDinoMode("DE")) || roomModel_1.IsDinoMode("CC"))
				{
					syncServerPacket.WriteC((byte)num2);
				}
			}
			int num3 = 8 - list_1.Count - ((num == 255) ? 1 : 0);
			for (int j = 0; j < num3; j++)
			{
				syncServerPacket.WriteC(byte.MaxValue);
			}
			syncServerPacket.WriteC(byte.MaxValue);
		}
		else
		{
			syncServerPacket.WriteB(new byte[10]);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(RoomModel roomModel_1)
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
