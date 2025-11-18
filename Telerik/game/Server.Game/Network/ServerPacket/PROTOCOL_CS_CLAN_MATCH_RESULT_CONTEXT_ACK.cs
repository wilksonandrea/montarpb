using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(1955);
			base.WriteC((byte)this.int_0);
			base.WriteC(13);
			base.WriteC((byte)Math.Ceiling((double)this.int_0 / 13));
		}
	}
}