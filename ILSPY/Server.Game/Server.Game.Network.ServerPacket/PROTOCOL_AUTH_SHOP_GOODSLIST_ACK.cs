using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GOODSLIST_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly ShopData shopData_0;

	public PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(ShopData shopData_1, int int_1)
	{
		shopData_0 = shopData_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1036);
		WriteD(int_0);
		WriteD(shopData_0.ItemsCount);
		WriteD(shopData_0.Offset);
		WriteB(shopData_0.Buffer);
		WriteD(50);
	}
}
