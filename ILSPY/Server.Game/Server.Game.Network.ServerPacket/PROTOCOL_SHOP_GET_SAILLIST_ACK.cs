using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_GET_SAILLIST_ACK : GameServerPacket
{
	private readonly bool bool_0;

	public PROTOCOL_SHOP_GET_SAILLIST_ACK(bool bool_1)
	{
		bool_0 = bool_1;
	}

	public override void Write()
	{
		WriteH(1030);
		WriteC(bool_0 ? ((byte)1) : ((byte)0));
		WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
	}
}
