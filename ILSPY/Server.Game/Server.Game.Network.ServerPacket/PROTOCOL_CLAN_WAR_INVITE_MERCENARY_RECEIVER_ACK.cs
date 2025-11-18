namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly uint uint_0;

	public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(uint uint_1, int int_1 = 0)
	{
		uint_0 = uint_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1572);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC((byte)int_0);
		}
	}
}
