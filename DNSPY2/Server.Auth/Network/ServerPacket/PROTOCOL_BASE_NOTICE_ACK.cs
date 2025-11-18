using System;
using Plugin.Core;
using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000027 RID: 39
	public class PROTOCOL_BASE_NOTICE_ACK : AuthServerPacket
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000282A File Offset: 0x00000A2A
		public PROTOCOL_BASE_NOTICE_ACK(ServerConfig serverConfig_1)
		{
			this.serverConfig_0 = serverConfig_1;
			if (serverConfig_1 != null)
			{
				this.string_0 = Translation.GetLabel(serverConfig_1.ChannelAnnounce);
				this.string_1 = Translation.GetLabel(serverConfig_1.ChatAnnounce);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006B0C File Offset: 0x00004D0C
		public override void Write()
		{
			base.WriteH(2455);
			base.WriteH(0);
			base.WriteD(this.serverConfig_0.ChatAnnounceColor);
			base.WriteD(this.serverConfig_0.ChannelAnnounceColor);
			base.WriteH(0);
			base.WriteH((ushort)this.string_1.Length);
			base.WriteN(this.string_1, this.string_1.Length, "UTF-16LE");
			base.WriteH((ushort)this.string_0.Length);
			base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
		}

		// Token: 0x04000056 RID: 86
		private readonly ServerConfig serverConfig_0;

		// Token: 0x04000057 RID: 87
		private readonly string string_0;

		// Token: 0x04000058 RID: 88
		private readonly string string_1;
	}
}
