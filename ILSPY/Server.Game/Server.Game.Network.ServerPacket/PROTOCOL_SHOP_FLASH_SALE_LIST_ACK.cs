namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_FLASH_SALE_LIST_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(1111);
		WriteC(1);
		WriteD(1);
		WriteC(1);
	}
}
