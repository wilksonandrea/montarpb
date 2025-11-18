using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_INVENTORY_ENTER_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3330);
		WriteD(0);
		WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
	}
}
