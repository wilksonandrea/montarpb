using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly int[] int_0;

	private readonly byte byte_0;

	public PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(uint uint_1, PlayerEquipment playerEquipment_1, int[] int_1, byte byte_1)
	{
		uint_0 = uint_1;
		playerEquipment_0 = playerEquipment_1;
		int_0 = int_1;
		byte_0 = byte_1;
	}

	public override void Write()
	{
		WriteH(3666);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(int_0[1]);
			WriteC(10);
			WriteD(int_0[0]);
			WriteD(playerEquipment_0.PartHead);
			WriteD(playerEquipment_0.PartFace);
			WriteD(playerEquipment_0.PartJacket);
			WriteD(playerEquipment_0.PartPocket);
			WriteD(playerEquipment_0.PartGlove);
			WriteD(playerEquipment_0.PartBelt);
			WriteD(playerEquipment_0.PartHolster);
			WriteD(playerEquipment_0.PartSkin);
			WriteD(playerEquipment_0.BeretItem);
			WriteC(5);
			WriteD(playerEquipment_0.WeaponPrimary);
			WriteD(playerEquipment_0.WeaponSecondary);
			WriteD(playerEquipment_0.WeaponMelee);
			WriteD(playerEquipment_0.WeaponExplosive);
			WriteD(playerEquipment_0.WeaponSpecial);
			WriteC(2);
			WriteD(playerEquipment_0.CharaRedId);
			WriteD(playerEquipment_0.CharaBlueId);
			WriteC(byte_0);
		}
	}
}
