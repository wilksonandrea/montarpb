using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_GAME_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_START_GAME_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5127);
		WriteH(0);
		WriteB(method_0(roomModel_0));
		WriteB(method_1(roomModel_0));
		WriteB(method_2(roomModel_0));
		WriteC((byte)roomModel_0.MapId);
		WriteC((byte)roomModel_0.Rule);
		WriteC((byte)roomModel_0.Stage);
		WriteC((byte)roomModel_0.RoomType);
	}

	private byte[] method_0(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)roomModel_1.GetReadyPlayers());
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State >= SlotState.READY && slotModel.Equipment != null)
			{
				Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
				if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
				{
					syncServerPacket.WriteD((uint)playerBySlot.PlayerId);
				}
			}
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)roomModel_1.Slots.Length);
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slotModel in slots)
		{
			syncServerPacket.WriteC((byte)slotModel.Team);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_2(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)roomModel_1.GetReadyPlayers());
		SlotModel[] slots = roomModel_1.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State < SlotState.READY || slotModel.Equipment == null)
			{
				continue;
			}
			Account playerBySlot = roomModel_1.GetPlayerBySlot(slotModel);
			if (playerBySlot == null || playerBySlot.SlotId != slotModel.Id)
			{
				continue;
			}
			syncServerPacket.WriteC((byte)slotModel.Id);
			PlayerEquipment equipment = playerBySlot.Equipment;
			PlayerTitles title = playerBySlot.Title;
			int value = 0;
			if (equipment != null && title != null)
			{
				switch (roomModel_1.ValidateTeam(slotModel.Team, slotModel.CostumeTeam))
				{
				case TeamEnum.FR_TEAM:
					value = equipment.CharaRedId;
					break;
				case TeamEnum.CT_TEAM:
					value = equipment.CharaBlueId;
					break;
				}
				syncServerPacket.WriteD(value);
				syncServerPacket.WriteD(equipment.WeaponPrimary);
				syncServerPacket.WriteD(equipment.WeaponSecondary);
				syncServerPacket.WriteD(equipment.WeaponMelee);
				syncServerPacket.WriteD(equipment.WeaponExplosive);
				syncServerPacket.WriteD(equipment.WeaponSpecial);
				syncServerPacket.WriteD(value);
				syncServerPacket.WriteD(equipment.PartHead);
				syncServerPacket.WriteD(equipment.PartFace);
				syncServerPacket.WriteD(equipment.PartJacket);
				syncServerPacket.WriteD(equipment.PartPocket);
				syncServerPacket.WriteD(equipment.PartGlove);
				syncServerPacket.WriteD(equipment.PartBelt);
				syncServerPacket.WriteD(equipment.PartHolster);
				syncServerPacket.WriteD(equipment.PartSkin);
				syncServerPacket.WriteD(equipment.BeretItem);
				syncServerPacket.WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
				syncServerPacket.WriteC((byte)title.Equiped1);
				syncServerPacket.WriteC((byte)title.Equiped2);
				syncServerPacket.WriteC((byte)title.Equiped3);
				syncServerPacket.WriteD(equipment.AccessoryId);
				syncServerPacket.WriteD(equipment.SprayId);
				syncServerPacket.WriteD(equipment.NameCardId);
			}
		}
		return syncServerPacket.ToArray();
	}
}
