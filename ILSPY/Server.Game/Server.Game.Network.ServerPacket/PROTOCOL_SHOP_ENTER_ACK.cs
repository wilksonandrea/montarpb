using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_ENTER_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(1026);
		WriteD(0);
		WriteC(0);
		WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
	}
}
