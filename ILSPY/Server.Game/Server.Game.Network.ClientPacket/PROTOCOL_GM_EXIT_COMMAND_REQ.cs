namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GM_EXIT_COMMAND_REQ : GameClientPacket
{
	private byte byte_0;

	public override void Read()
	{
		byte_0 = ReadC();
	}

	public override void Run()
	{
	}
}
