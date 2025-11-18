using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_CONTEXT_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_CS_MEMBER_CONTEXT_ACK(int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
	}

	public PROTOCOL_CS_MEMBER_CONTEXT_ACK(int int_2)
	{
		int_0 = int_2;
	}

	public override void Write()
	{
		WriteH(803);
		WriteD(int_0);
		if (int_0 == 0)
		{
			WriteC((byte)int_1);
			WriteC(14);
			WriteC((byte)Math.Ceiling((double)int_1 / 14.0));
			WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
		}
	}
}
