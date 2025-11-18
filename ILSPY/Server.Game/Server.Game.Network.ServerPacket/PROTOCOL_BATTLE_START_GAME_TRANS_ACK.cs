using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_GAME_TRANS_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly SlotModel slotModel_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly PlayerTitles playerTitles_0;

	private readonly int int_0;

	public PROTOCOL_BATTLE_START_GAME_TRANS_ACK(RoomModel roomModel_1, SlotModel slotModel_1, PlayerTitles playerTitles_1)
	{
		roomModel_0 = roomModel_1;
		slotModel_0 = slotModel_1;
		playerTitles_0 = playerTitles_1;
		if (roomModel_1 == null || slotModel_1 == null || playerTitles_1 == null)
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
	}

	public override void Write()
	{
		WriteH(5128);
		WriteH(0);
		WriteD((uint)slotModel_0.PlayerId);
		WriteC(2);
		WriteC((byte)slotModel_0.Id);
		WriteD(int_0);
		WriteD(playerEquipment_0.WeaponPrimary);
		WriteD(playerEquipment_0.WeaponSecondary);
		WriteD(playerEquipment_0.WeaponMelee);
		WriteD(playerEquipment_0.WeaponExplosive);
		WriteD(playerEquipment_0.WeaponSpecial);
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
		WriteB(Bitwise.HexStringToByteArray("64 64 64 64 64"));
		WriteC((byte)playerTitles_0.Equiped1);
		WriteC((byte)playerTitles_0.Equiped2);
		WriteC((byte)playerTitles_0.Equiped3);
		WriteD(playerEquipment_0.AccessoryId);
		WriteD(playerEquipment_0.SprayId);
		WriteD(playerEquipment_0.NameCardId);
	}
}
