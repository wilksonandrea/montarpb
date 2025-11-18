using Plugin.Core.Network;

namespace Server.Match.Network.Packets;

public class PROTOCOL_CONNECT
{
	public static byte[] GET_CODE()
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC(66);
		syncServerPacket.WriteC(0);
		syncServerPacket.WriteT(0f);
		syncServerPacket.WriteC(0);
		syncServerPacket.WriteH(14);
		syncServerPacket.WriteD(0);
		syncServerPacket.WriteC(8);
		return syncServerPacket.ToArray();
	}
}
