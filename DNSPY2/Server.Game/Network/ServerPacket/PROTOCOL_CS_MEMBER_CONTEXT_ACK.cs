using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B7 RID: 183
	public class PROTOCOL_CS_MEMBER_CONTEXT_ACK : GameServerPacket
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00003DFA File Offset: 0x00001FFA
		public PROTOCOL_CS_MEMBER_CONTEXT_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00003E10 File Offset: 0x00002010
		public PROTOCOL_CS_MEMBER_CONTEXT_ACK(int int_2)
		{
			this.int_0 = int_2;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0001027C File Offset: 0x0000E47C
		public override void Write()
		{
			base.WriteH(803);
			base.WriteD(this.int_0);
			if (this.int_0 == 0)
			{
				base.WriteC((byte)this.int_1);
				base.WriteC(14);
				base.WriteC((byte)Math.Ceiling((double)this.int_1 / 14.0));
				base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
			}
		}

		// Token: 0x04000150 RID: 336
		private readonly int int_0;

		// Token: 0x04000151 RID: 337
		private readonly int int_1;
	}
}
