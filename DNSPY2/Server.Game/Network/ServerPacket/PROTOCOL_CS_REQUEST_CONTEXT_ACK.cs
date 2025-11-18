using System;
using Plugin.Core.SQL;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000CA RID: 202
	public class PROTOCOL_CS_REQUEST_CONTEXT_ACK : GameServerPacket
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000410E File Offset: 0x0000230E
		public PROTOCOL_CS_REQUEST_CONTEXT_ACK(int int_1)
		{
			if (int_1 > 0)
			{
				this.int_0 = DaoManagerSQL.GetRequestClanInviteCount(int_1);
				return;
			}
			this.uint_0 = uint.MaxValue;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00010710 File Offset: 0x0000E910
		public override void Write()
		{
			base.WriteH(817);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC((byte)this.int_0);
				base.WriteC(13);
				base.WriteC((byte)Math.Ceiling((double)this.int_0 / 13.0));
				base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
			}
		}

		// Token: 0x0400016C RID: 364
		private readonly uint uint_0;

		// Token: 0x0400016D RID: 365
		private readonly int int_0;
	}
}
