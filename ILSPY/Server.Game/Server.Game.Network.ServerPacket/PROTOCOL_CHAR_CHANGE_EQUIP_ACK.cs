namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_CHANGE_EQUIP_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_CHAR_CHANGE_EQUIP_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(6150);
		WriteD(uint_0);
		WriteH(0);
	}
}
