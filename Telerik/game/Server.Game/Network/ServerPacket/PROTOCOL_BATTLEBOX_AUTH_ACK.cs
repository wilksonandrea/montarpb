using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLEBOX_AUTH_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly Account account_0;

		private readonly int int_0;

		public PROTOCOL_BATTLEBOX_AUTH_ACK(uint uint_1, Account account_1 = null, int int_1 = 0)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(7430);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteD(this.int_0);
				base.WriteB(new byte[5]);
				base.WriteD(this.account_0.Tags);
			}
		}
	}
}