using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_CREATE_NICK_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly Account account_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	public PROTOCOL_BASE_CREATE_NICK_ACK(uint uint_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		if (account_1 != null)
		{
			playerInventory_0 = account_1.Inventory;
			playerEquipment_0 = account_1.Equipment;
		}
	}

	public override void Write()
	{
		WriteH(2327);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC(1);
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.DinoItem));
			WriteC((byte)(account_0.Nickname.Length * 2));
			WriteU(account_0.Nickname, account_0.Nickname.Length * 2);
		}
	}
}
