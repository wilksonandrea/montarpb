using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000EA RID: 234
	public class PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK : GameServerPacket
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00004593 File Offset: 0x00002793
		public PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00012018 File Offset: 0x00010218
		public override void Write()
		{
			base.WriteH(3636);
			base.WriteC(0);
			base.WriteU(this.roomModel_0.LeaderName, 66);
			base.WriteD(this.roomModel_0.KillTime);
			base.WriteC(this.roomModel_0.Limit);
			base.WriteC(this.roomModel_0.WatchRuleFlag);
			base.WriteH((ushort)this.roomModel_0.BalanceType);
			base.WriteB(this.roomModel_0.RandomMaps);
			base.WriteC(this.roomModel_0.CountdownIG);
			base.WriteB(this.roomModel_0.LeaderAddr);
			base.WriteC(this.roomModel_0.KillCam);
			base.WriteH(0);
		}

		// Token: 0x040001AF RID: 431
		private readonly RoomModel roomModel_0;
	}
}
