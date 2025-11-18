using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_DELETE_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly List<object> list_0;

	public PROTOCOL_MESSENGER_NOTE_DELETE_ACK(uint uint_1, List<object> list_1)
	{
		uint_0 = uint_1;
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(1929);
		WriteD(uint_0);
		WriteC((byte)list_0.Count);
		foreach (long item in list_0)
		{
			WriteD((uint)item);
		}
	}
}
