namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_INVENTORY_LEAVE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3332);
		WriteH(0);
		WriteD(0);
	}
}
