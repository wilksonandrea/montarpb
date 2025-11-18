using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000DB RID: 219
	public class PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK : GameServerPacket
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000043D9 File Offset: 0x000025D9
		public PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		public override void Write()
		{
			base.WriteH(2568);
			base.WriteC(0);
			base.WriteU(this.roomModel_0.LeaderName, 66);
			base.WriteC((byte)this.roomModel_0.KillTime);
			base.WriteC((byte)(this.roomModel_0.Rounds - 1));
			base.WriteH((ushort)this.roomModel_0.GetInBattleTime());
			base.WriteC(this.roomModel_0.Limit);
			base.WriteC(this.roomModel_0.WatchRuleFlag);
			base.WriteH((ushort)this.roomModel_0.BalanceType);
			base.WriteB(this.roomModel_0.RandomMaps);
			base.WriteC(this.roomModel_0.CountdownIG);
			base.WriteB(this.roomModel_0.LeaderAddr);
			base.WriteC(this.roomModel_0.KillCam);
			base.WriteH(0);
		}

		// Token: 0x0400018A RID: 394
		private readonly RoomModel roomModel_0;
	}
}
