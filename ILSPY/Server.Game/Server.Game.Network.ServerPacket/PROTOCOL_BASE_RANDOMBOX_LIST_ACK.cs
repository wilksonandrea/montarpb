using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_RANDOMBOX_LIST_ACK : GameServerPacket
{
	private readonly bool bool_0;

	public PROTOCOL_BASE_RANDOMBOX_LIST_ACK(bool bool_1)
	{
		bool_0 = bool_1;
	}

	public override void Write()
	{
		WriteH(2499);
		WriteC(bool_0 ? ((byte)1) : ((byte)0));
		WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
	}
}
