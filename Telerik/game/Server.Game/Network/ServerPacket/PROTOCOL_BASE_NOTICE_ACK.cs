using Plugin.Core;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_NOTICE_ACK : GameServerPacket
	{
		private readonly ServerConfig serverConfig_0;

		private readonly string string_0;

		private readonly string string_1;

		public PROTOCOL_BASE_NOTICE_ACK(string string_2)
		{
			this.serverConfig_0 = GameXender.Client.Config;
			if (this.serverConfig_0 != null)
			{
				this.string_0 = Translation.GetLabel(this.serverConfig_0.ChannelAnnounce);
				this.string_1 = string.Concat(Translation.GetLabel(this.serverConfig_0.ChatAnnounce), " \n\r", string_2);
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