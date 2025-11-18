namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_SEIZE_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly byte byte_0;

	public PROTOCOL_BATTLE_MISSION_SEIZE_ACK(int int_1, byte byte_1)
	{
		int_0 = int_1;
		byte_0 = byte_1;
	}

	public override void Write()
	{
		WriteH(5292);
		WriteD(int_0);
		WriteC(byte_0);
	}
}
