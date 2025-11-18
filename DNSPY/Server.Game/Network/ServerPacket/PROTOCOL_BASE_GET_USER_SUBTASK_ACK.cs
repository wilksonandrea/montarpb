using System;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200001C RID: 28
	public class PROTOCOL_BASE_GET_USER_SUBTASK_ACK : GameServerPacket
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000028BA File Offset: 0x00000ABA
		public PROTOCOL_BASE_GET_USER_SUBTASK_ACK(PlayerSession playerSession_0)
		{
			this.account_0 = AccountManager.GetAccount(playerSession_0.PlayerId, true);
			this.int_0 = playerSession_0.SessionId;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000028E0 File Offset: 0x00000AE0
		public override void Write()
		{
			base.WriteH(2448);
			base.WriteD(this.int_0);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteD(this.int_0);
			base.WriteC(0);
		}

		// Token: 0x0400003B RID: 59
		private readonly Account account_0;

		// Token: 0x0400003C RID: 60
		private readonly int int_0;
	}
}
