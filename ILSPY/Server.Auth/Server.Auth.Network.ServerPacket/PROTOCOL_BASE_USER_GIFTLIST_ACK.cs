using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_USER_GIFTLIST_ACK : AuthServerPacket
{
	private readonly int int_0;

	private readonly List<MessageModel> list_0;

	public PROTOCOL_BASE_USER_GIFTLIST_ACK(int int_1, List<MessageModel> list_1)
	{
		int_0 = int_1;
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(1042);
		WriteC(0);
		WriteC((byte)list_0.Count);
		for (int i = 0; i < list_0.Count; i++)
		{
			_ = list_0[i];
		}
		WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
	}
}
