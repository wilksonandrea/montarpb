using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_MATCH_SERVER_IDX_ACK : GameServerPacket
	{
		private readonly short short_0;

		public PROTOCOL_MATCH_SERVER_IDX_ACK(short short_1)
		{
			this.short_0 = short_1;
		}

		public override void Write()
		{
			base.WriteH(7682);
			base.WriteH(0);
		}
	}
}