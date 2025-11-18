using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_URL_LIST_ACK : GameServerPacket
	{
		private readonly ServerConfig serverConfig_0;

		public PROTOCOL_BASE_URL_LIST_ACK(ServerConfig serverConfig_1)
		{
			this.serverConfig_0 = serverConfig_1;
		}

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
	}
}