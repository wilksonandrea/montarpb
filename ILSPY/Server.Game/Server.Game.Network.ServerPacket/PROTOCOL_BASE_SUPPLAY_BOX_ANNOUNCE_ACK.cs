namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK : GameServerPacket
{
	private readonly string string_0;

	public PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK(string string_1)
	{
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(2409);
		WriteD(0);
	}
}
