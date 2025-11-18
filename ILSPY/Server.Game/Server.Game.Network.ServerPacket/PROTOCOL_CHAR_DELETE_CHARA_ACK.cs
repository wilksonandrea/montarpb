using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_DELETE_CHARA_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly int int_0;

	private readonly ItemsModel itemsModel_0;

	private readonly CharacterModel characterModel_0;

	public PROTOCOL_CHAR_DELETE_CHARA_ACK(uint uint_1, int int_1, Account account_0, ItemsModel itemsModel_1)
	{
		uint_0 = uint_1;
		int_0 = int_1;
		itemsModel_0 = itemsModel_1;
		if (account_0 != null && itemsModel_1 != null)
		{
			characterModel_0 = account_0.Character.GetCharacter(itemsModel_1.Id);
		}
	}

	public override void Write()
	{
		WriteH(6152);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC((byte)int_0);
			WriteD((uint)itemsModel_0.ObjectId);
			WriteD(characterModel_0.Slot);
		}
	}
}
