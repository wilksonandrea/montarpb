using System;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D3 RID: 211
	public class PROTOCOL_GM_LOG_LOBBY_ACK : GameServerPacket
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00004275 File Offset: 0x00002475
		public PROTOCOL_GM_LOG_LOBBY_ACK(uint uint_1, long long_0)
		{
			this.uint_0 = uint_1;
			this.account_0 = AccountManager.GetAccount(long_0, true);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00004291 File Offset: 0x00002491
		public override void Write()
		{
			base.WriteH(2685);
			base.WriteD(this.uint_0);
			base.WriteQ(this.account_0.PlayerId);
		}

		// Token: 0x0400017D RID: 381
		private readonly Account account_0;

		// Token: 0x0400017E RID: 382
		private readonly uint uint_0;
	}
}
