using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_CHANGE_NICKNAME_ACK : GameServerPacket
	{
		private readonly string string_0;

		public PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		public override void Write()
		{
			base.WriteH(1836);
			base.WriteC((byte)this.string_0.Length);
			base.WriteU(this.string_0, this.string_0.Length * 2);
		}
	}
}