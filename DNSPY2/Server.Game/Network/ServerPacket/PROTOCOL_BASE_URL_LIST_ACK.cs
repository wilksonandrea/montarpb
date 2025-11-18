using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000021 RID: 33
	public class PROTOCOL_BASE_URL_LIST_ACK : GameServerPacket
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002994 File Offset: 0x00000B94
		public PROTOCOL_BASE_URL_LIST_ACK(ServerConfig serverConfig_1)
		{
			this.serverConfig_0 = serverConfig_1;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public override void Write()
		{
			base.WriteH(2466);
			base.WriteH(514);
			base.WriteH((ushort)this.serverConfig_0.OfficialBanner.Length);
			base.WriteD(0);
			base.WriteC(2);
			base.WriteN(this.serverConfig_0.OfficialBanner, this.serverConfig_0.OfficialBanner.Length, "UTF-16LE");
			base.WriteQ(0L);
		}

		// Token: 0x04000045 RID: 69
		private readonly ServerConfig serverConfig_0;
	}
}
