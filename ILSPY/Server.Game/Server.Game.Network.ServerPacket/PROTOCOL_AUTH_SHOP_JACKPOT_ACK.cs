namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_JACKPOT_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_AUTH_SHOP_JACKPOT_ACK(string string_1, int int_2, int int_3)
	{
		string_0 = string_1;
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(1068);
		WriteH(0);
		WriteC((byte)int_1);
		WriteD(int_0);
		WriteC((byte)string_0.Length);
		WriteN(string_0, string_0.Length, "UTF-16LE");
	}
}
