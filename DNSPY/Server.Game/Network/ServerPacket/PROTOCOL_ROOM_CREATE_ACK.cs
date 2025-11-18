using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000EE RID: 238
	public class PROTOCOL_ROOM_CREATE_ACK : GameServerPacket
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00004617 File Offset: 0x00002817
		public PROTOCOL_ROOM_CREATE_ACK(uint uint_1, RoomModel roomModel_1)
		{
			this.uint_0 = uint_1;
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00012150 File Offset: 0x00010350
		public override void Write()
		{
			base.WriteH(3593);
			base.WriteD((uint)((this.uint_0 == 0U) ? this.roomModel_0.RoomId : ((int)this.uint_0)));
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.roomModel_0.RoomId);
				base.WriteU(this.roomModel_0.Name, 46);
				base.WriteC((byte)this.roomModel_0.MapId);
				base.WriteC((byte)this.roomModel_0.Rule);
				base.WriteC((byte)this.roomModel_0.Stage);
				base.WriteC((byte)this.roomModel_0.RoomType);
				base.WriteC((byte)this.roomModel_0.State);
				base.WriteC((byte)this.roomModel_0.GetCountPlayers());
				base.WriteC((byte)this.roomModel_0.GetSlotCount());
				base.WriteC((byte)this.roomModel_0.Ping);
				base.WriteH((ushort)this.roomModel_0.WeaponsFlag);
				base.WriteD(this.roomModel_0.GetFlag());
				base.WriteH(0);
				base.WriteD(this.roomModel_0.NewInt);
				base.WriteH(0);
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
		}

		// Token: 0x040001B6 RID: 438
		private readonly RoomModel roomModel_0;

		// Token: 0x040001B7 RID: 439
		private readonly uint uint_0;
	}
}
