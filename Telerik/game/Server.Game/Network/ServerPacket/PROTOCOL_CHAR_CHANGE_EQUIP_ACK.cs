using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CHAR_CHANGE_EQUIP_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_CHAR_CHANGE_EQUIP_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(6150);
			base.WriteD(this.uint_0);
			base.WriteH(0);
		}
	}
}