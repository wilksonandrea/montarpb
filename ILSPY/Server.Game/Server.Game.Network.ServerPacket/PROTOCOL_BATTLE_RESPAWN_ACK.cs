using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_RESPAWN_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly SlotModel slotModel_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly List<int> list_0;

	private readonly int int_0;

	public PROTOCOL_BATTLE_RESPAWN_ACK(RoomModel roomModel_1, SlotModel slotModel_1)
	{
		roomModel_0 = roomModel_1;
		slotModel_0 = slotModel_1;
		if (roomModel_1 == null || slotModel_1 == null)
		{
			return;
		}
		playerEquipment_0 = slotModel_1.Equipment;
		if (playerEquipment_0 != null)
		{
			switch (roomModel_1.ValidateTeam(slotModel_1.Team, slotModel_1.CostumeTeam))
			{
			case TeamEnum.FR_TEAM:
				int_0 = playerEquipment_0.CharaRedId;
				break;
			case TeamEnum.CT_TEAM:
				int_0 = playerEquipment_0.CharaBlueId;
				break;
			}
		}
		list_0 = AllUtils.GetDinossaurs(roomModel_1, ForceNewTRex: false, slotModel_1.Id);
	}

	public override void Write()
	{
		WriteH(5138);
		WriteD(slotModel_0.Id);
		WriteD(roomModel_0.SpawnsCount++);
		WriteD(++slotModel_0.SpawnsCount);
		WriteD(playerEquipment_0.WeaponPrimary);
		WriteD(playerEquipment_0.WeaponSecondary);
		WriteD(playerEquipment_0.WeaponMelee);
		WriteD(playerEquipment_0.WeaponExplosive);
		WriteD(playerEquipment_0.WeaponSpecial);
		WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
		WriteD(int_0);
		WriteD(playerEquipment_0.PartHead);
		WriteD(playerEquipment_0.PartFace);
		WriteD(playerEquipment_0.PartJacket);
		WriteD(playerEquipment_0.PartPocket);
		WriteD(playerEquipment_0.PartGlove);
		WriteD(playerEquipment_0.PartBelt);
		WriteD(playerEquipment_0.PartHolster);
		WriteD(playerEquipment_0.PartSkin);
		WriteD(playerEquipment_0.BeretItem);
		WriteD(playerEquipment_0.AccessoryId);
		WriteB(method_0(roomModel_0, list_0));
	}

	private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (roomModel_1.IsDinoMode())
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
}
