using System;
using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000029 RID: 41
	public class PROTOCOL_BASE_URL_LIST_ACK : AuthServerPacket
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002872 File Offset: 0x00000A72
		public PROTOCOL_BASE_URL_LIST_ACK(ServerConfig serverConfig_1)
		{
			this.serverConfig_0 = serverConfig_1;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006BB0 File Offset: 0x00004DB0
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

		// Token: 0x04000059 RID: 89
		private readonly ServerConfig serverConfig_0;
	}
}
