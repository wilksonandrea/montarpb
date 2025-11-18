using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000026 RID: 38
	public class PROTOCOL_BASE_USER_TITLE_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00002A6A File Offset: 0x00000C6A
		public PROTOCOL_BASE_USER_TITLE_INFO_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000A324 File Offset: 0x00008524
		public override void Write()
		{
			base.WriteH(2383);
			base.WriteB(BitConverter.GetBytes(this.account_0.PlayerId), 0, 4);
			base.WriteQ(this.account_0.Title.Flags);
			base.WriteC((byte)this.account_0.Title.Equiped1);
			base.WriteC((byte)this.account_0.Title.Equiped2);
			base.WriteC((byte)this.account_0.Title.Equiped3);
			base.WriteD(this.account_0.Title.Slots);
		}

		// Token: 0x0400004C RID: 76
		private readonly Account account_0;
	}
}
