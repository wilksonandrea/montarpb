using System;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200000C RID: 12
	public class PROTOCOL_MATCH_CLAN_SEASON_ACK : AuthServerPacket
	{
		// Token: 0x0600004A RID: 74 RVA: 0x0000254E File Offset: 0x0000074E
		public PROTOCOL_MATCH_CLAN_SEASON_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004184 File Offset: 0x00002384
		public override void Write()
		{
			base.WriteH(7700);
			base.WriteH(0);
			base.WriteD(2);
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
			base.WriteB(new byte[109]);
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
			base.WriteB(new byte[103]);
		}

		// Token: 0x04000020 RID: 32
		private readonly int int_0;
	}
}
