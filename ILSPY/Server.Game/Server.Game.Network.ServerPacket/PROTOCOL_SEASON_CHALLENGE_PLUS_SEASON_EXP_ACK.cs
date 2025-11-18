namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(8456);
		WriteH(0);
		WriteD(uint_0);
		WriteC(1);
		WriteC(6);
		WriteD(2580);
		WriteC(5);
		WriteC(5);
		WriteC(1);
	}
}
