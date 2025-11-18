using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_REQUEST_CONTEXT_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly int int_0;

		public PROTOCOL_CS_REQUEST_CONTEXT_ACK(int int_1)
		{
			if (int_1 <= 0)
			{
				this.uint_0 = -1;
				return;
			}
			this.int_0 = DaoManagerSQL.GetRequestClanInviteCount(int_1);
		}

		public override void Write()
		{
			base.WriteH(817);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteC((byte)this.int_0);
				base.WriteC(13);
				base.WriteC((byte)Math.Ceiling((double)this.int_0 / 13));
				base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
			}
		}
	}
}