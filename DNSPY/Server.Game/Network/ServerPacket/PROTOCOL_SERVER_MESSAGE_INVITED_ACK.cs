using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000110 RID: 272
	public class PROTOCOL_SERVER_MESSAGE_INVITED_ACK : GameServerPacket
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00004A51 File Offset: 0x00002C51
		public PROTOCOL_SERVER_MESSAGE_INVITED_ACK(Account account_1, RoomModel roomModel_1)
		{
			this.account_0 = account_1;
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001453C File Offset: 0x0001273C
		public override void Write()
		{
			base.WriteH(3077);
			base.WriteU(this.account_0.Nickname, 66);
			base.WriteD(this.roomModel_0.RoomId);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteS(this.roomModel_0.Password, 4);
		}

		// Token: 0x040001FA RID: 506
		private readonly Account account_0;

		// Token: 0x040001FB RID: 507
		private readonly RoomModel roomModel_0;
	}
}
