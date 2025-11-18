using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_REPLACE_NAME_RESULT_ACK : GameServerPacket
	{
		private readonly string string_0;

		public PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		public override void Write()
		{
			base.WriteH(864);
			base.WriteU(this.string_0, 34);
		}
	}
}