using System;
using Plugin.Core.SQL;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REQUEST_CONTEXT_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly int int_0;

	public PROTOCOL_CS_REQUEST_CONTEXT_ACK(int int_1)
	{
		if (int_1 > 0)
		{
			int_0 = DaoManagerSQL.GetRequestClanInviteCount(int_1);
		}
		else
		{
			uint_0 = uint.MaxValue;
		}
	}

	public override void Write()
	{
		WriteH(817);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC((byte)int_0);
			WriteC(13);
			WriteC((byte)Math.Ceiling((double)int_0 / 13.0));
			WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
		}
	}
}
