using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_SENDPING_ACK : GameServerPacket
	{
		private readonly byte[] byte_0;

		public PROTOCOL_BATTLE_SENDPING_ACK(byte[] byte_1)
		{
			this.byte_0 = byte_1;
		}

		public override void Write()
		{
			base.WriteH(5147);
			base.WriteB(this.byte_0);
		}
	}
}