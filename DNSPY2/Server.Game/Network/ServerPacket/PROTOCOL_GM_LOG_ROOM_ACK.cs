using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D4 RID: 212
	public class PROTOCOL_GM_LOG_ROOM_ACK : GameServerPacket
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000042BB File Offset: 0x000024BB
		public PROTOCOL_GM_LOG_ROOM_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000042D1 File Offset: 0x000024D1
		public override void Write()
		{
			base.WriteH(2687);
			base.WriteD(this.uint_0);
			base.WriteQ(this.account_0.PlayerId);
		}

		// Token: 0x0400017F RID: 383
		private readonly Account account_0;

		// Token: 0x04000180 RID: 384
		private readonly uint uint_0;
	}
}
