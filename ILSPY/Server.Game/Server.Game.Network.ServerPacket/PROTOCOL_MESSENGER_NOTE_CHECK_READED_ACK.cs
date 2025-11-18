using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK : GameServerPacket
{
	private readonly List<int> list_0;

	public PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(List<int> list_1)
	{
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(1927);
		WriteC((byte)list_0.Count);
		for (int i = 0; i < list_0.Count; i++)
		{
			WriteD(list_0[i]);
		}
	}
}
