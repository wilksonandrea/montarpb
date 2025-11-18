using System;
using Plugin.Core;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200000F RID: 15
	public class PROTOCOL_BASE_NOTICE_ACK : GameServerPacket
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00009388 File Offset: 0x00007588
		public PROTOCOL_BASE_NOTICE_ACK(string string_2)
		{
			this.serverConfig_0 = GameXender.Client.Config;
			if (this.serverConfig_0 != null)
			{
				this.string_0 = Translation.GetLabel(this.serverConfig_0.ChannelAnnounce);
				this.string_1 = Translation.GetLabel(this.serverConfig_0.ChatAnnounce) + " \n\r" + string_2;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000093EC File Offset: 0x000075EC
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

		// Token: 0x04000020 RID: 32
		private readonly ServerConfig serverConfig_0;

		// Token: 0x04000021 RID: 33
		private readonly string string_0;

		// Token: 0x04000022 RID: 34
		private readonly string string_1;
	}
}
