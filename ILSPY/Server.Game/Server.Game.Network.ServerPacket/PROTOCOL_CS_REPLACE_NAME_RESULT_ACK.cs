namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_NAME_RESULT_ACK : GameServerPacket
{
	private readonly string string_0;

	public PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(string string_1)
	{
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(864);
		WriteU(string_0, 34);
	}
}
