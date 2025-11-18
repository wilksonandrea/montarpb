using Plugin.Core.Network;
using System;

namespace Server.Match.Network.Packets
{
	public class PROTOCOL_CONNECT
	{
		public PROTOCOL_CONNECT()
		{
		}

		public static byte[] GET_CODE()
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC(66);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteT(0f);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteH(14);
				syncServerPacket.WriteD(0);
				syncServerPacket.WriteC(8);
				array = syncServerPacket.ToArray();
			}
			return array;
		}
	}
}