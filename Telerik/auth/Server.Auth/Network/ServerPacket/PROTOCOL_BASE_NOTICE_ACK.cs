using Plugin.Core;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_NOTICE_ACK : AuthServerPacket
	{
		private readonly ServerConfig serverConfig_0;

		private readonly string string_0;

		private readonly string string_1;

		public PROTOCOL_BASE_NOTICE_ACK(ServerConfig serverConfig_1)
		{
			this.serverConfig_0 = serverConfig_1;
			if (serverConfig_1 != null)
			{
				this.string_0 = Translation.GetLabel(serverConfig_1.ChannelAnnounce);
				this.string_1 = Translation.GetLabel(serverConfig_1.ChatAnnounce);
			}
		}

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
	}
}