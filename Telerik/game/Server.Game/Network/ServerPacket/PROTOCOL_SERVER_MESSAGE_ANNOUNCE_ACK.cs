using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : GameServerPacket
	{
		private readonly string string_0;

		public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		public override void Write()
		{
			base.WriteH(3079);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteH((ushort)this.string_0.Length);
			base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
			base.WriteD(2);
		}
	}
}