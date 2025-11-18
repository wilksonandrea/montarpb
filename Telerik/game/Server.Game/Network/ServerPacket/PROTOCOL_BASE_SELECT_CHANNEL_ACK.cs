using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_SELECT_CHANNEL_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly ushort ushort_0;

		private readonly uint uint_0;

		public PROTOCOL_BASE_SELECT_CHANNEL_ACK(uint uint_1, int int_1, int int_2)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
			this.ushort_0 = (ushort)int_2;
		}

		public override void Write()
		{
			base.WriteH(2335);
			base.WriteD(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteD(this.int_0);
				base.WriteH(this.ushort_0);
			}
		}
	}
}