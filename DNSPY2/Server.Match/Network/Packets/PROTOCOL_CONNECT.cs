using System;
using Plugin.Core.Network;

namespace Server.Match.Network.Packets
{
	// Token: 0x02000006 RID: 6
	public class PROTOCOL_CONNECT
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000651C File Offset: 0x0000471C
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

		// Token: 0x06000026 RID: 38 RVA: 0x000020A2 File Offset: 0x000002A2
		public PROTOCOL_CONNECT()
		{
		}
	}
}
