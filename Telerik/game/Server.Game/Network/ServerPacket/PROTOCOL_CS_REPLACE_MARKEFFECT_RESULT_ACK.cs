using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(980);
			base.WriteC((byte)this.int_0);
		}
	}
}