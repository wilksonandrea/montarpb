using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E1 RID: 225
	public class PROTOCOL_MATCH_CLAN_SEASON_ACK : GameServerPacket
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00004469 File Offset: 0x00002669
		public PROTOCOL_MATCH_CLAN_SEASON_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00011A3C File Offset: 0x0000FC3C
		public override void Write()
		{
			base.WriteH(7700);
			base.WriteH(0);
			base.WriteD(2);
			base.WriteD((this.bool_0 > false) ? 1 : 0);
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
			base.WriteB(new byte[109]);
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
			base.WriteB(new byte[103]);
		}

		// Token: 0x040001A4 RID: 420
		private readonly bool bool_0;
	}
}
