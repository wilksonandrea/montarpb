namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_LEAVE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(1028);
		WriteH(0);
		WriteD(0);
	}
}
