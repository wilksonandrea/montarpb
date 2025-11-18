using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly string string_1;

	public PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(string string_2, string string_3)
	{
		string_0 = string_2;
		string_1 = string_3;
	}

	public override void Write()
	{
		WriteH(1803);
		WriteD(94767);
		WriteD(100950);
		WriteD(0);
		WriteD(983299);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(3);
		WriteC(8);
		WriteB(Bitwise.HexStringToByteArray("47 00 4D 00 00 00 45 00 56 00 45 00 4E 00 54 00 5F 00 38 00 00 00"));
		WriteD(56);
		WriteC(1);
		WriteD(180214952);
		WriteB(Bitwise.HexStringToByteArray("81 E0 D0 03 09 04 15 00 80 22"));
	}
}
