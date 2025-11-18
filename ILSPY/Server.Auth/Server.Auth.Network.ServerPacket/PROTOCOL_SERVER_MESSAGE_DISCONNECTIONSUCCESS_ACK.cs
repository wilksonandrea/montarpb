using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK : AuthServerPacket
{
	private readonly uint uint_0;

	private readonly bool bool_0;

	public PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(uint uint_1, bool bool_1)
	{
		uint_0 = uint_1;
		bool_0 = bool_1;
	}

	public override void Write()
	{
		WriteH(3074);
		WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		WriteD(uint_0);
		WriteD(bool_0 ? 1 : 0);
		if (bool_0)
		{
			WriteD(0);
		}
	}
}
