namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLIENT_LEAVE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(772);
		WriteD(0);
	}
}
