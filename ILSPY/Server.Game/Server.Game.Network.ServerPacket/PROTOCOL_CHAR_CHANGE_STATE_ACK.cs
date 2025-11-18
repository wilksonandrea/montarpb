using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_CHANGE_STATE_ACK : GameServerPacket
{
	private readonly CharacterModel characterModel_0;

	public PROTOCOL_CHAR_CHANGE_STATE_ACK(CharacterModel characterModel_1)
	{
		characterModel_0 = characterModel_1;
	}

	public override void Write()
	{
		WriteH(6153);
		WriteH(0);
		WriteD(0);
		WriteC(20);
		WriteC((byte)characterModel_0.Slot);
	}
}
