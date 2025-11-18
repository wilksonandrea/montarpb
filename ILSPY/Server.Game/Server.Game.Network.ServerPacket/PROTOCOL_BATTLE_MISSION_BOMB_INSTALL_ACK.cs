namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly float float_0;

	private readonly float float_1;

	private readonly float float_2;

	private readonly byte byte_0;

	private readonly ushort ushort_0;

	public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(int int_1, byte byte_1, ushort ushort_1, float float_3, float float_4, float float_5)
	{
		byte_0 = byte_1;
		int_0 = int_1;
		ushort_0 = ushort_1;
		float_0 = float_3;
		float_1 = float_4;
		float_2 = float_5;
	}

	public override void Write()
	{
		WriteH(5157);
		WriteD(int_0);
		WriteC(byte_0);
		WriteH(ushort_0);
		WriteT(float_0);
		WriteT(float_1);
		WriteT(float_2);
	}
}
