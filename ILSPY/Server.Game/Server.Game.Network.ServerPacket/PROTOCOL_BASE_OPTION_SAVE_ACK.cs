namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_OPTION_SAVE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2323);
		WriteD(0);
	}
}
