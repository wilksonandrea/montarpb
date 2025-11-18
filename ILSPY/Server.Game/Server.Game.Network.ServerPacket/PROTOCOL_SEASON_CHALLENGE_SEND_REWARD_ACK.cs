namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly int[] int_0;

	public PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(uint uint_1, int[] int_1)
	{
		uint_0 = uint_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(8453);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC(2);
			WriteD(int_0[1]);
			WriteD(int_0[2]);
			WriteD(int_0[0]);
			WriteC(1);
			WriteC(1);
			WriteC(1);
		}
	}
}
