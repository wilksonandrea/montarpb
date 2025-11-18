namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_SENDPING_ACK : GameServerPacket
{
	private readonly byte[] byte_0;

	public PROTOCOL_BATTLE_SENDPING_ACK(byte[] byte_1)
	{
		byte_0 = byte_1;
	}

	public override void Write()
	{
		WriteH(5147);
		WriteB(byte_0);
	}
}
