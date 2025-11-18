using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E9 RID: 233
	public class PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK : GameServerPacket
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000455F File Offset: 0x0000275F
		public PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
			if (roomModel_1 != null)
			{
				this.bool_0 = roomModel_1.IsBotMode();
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000457D File Offset: 0x0000277D
		public PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(RoomModel roomModel_1, bool bool_1)
		{
			this.roomModel_0 = roomModel_1;
			this.bool_0 = bool_1;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00011E3C File Offset: 0x0001003C
		public override void Write()
		{
			base.WriteH(3601);
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
			if (this.bool_0)
			{
				base.WriteC(this.roomModel_0.AiCount);
				base.WriteC(this.roomModel_0.AiLevel);
			}
		}

		// Token: 0x040001AD RID: 429
		private readonly RoomModel roomModel_0;

		// Token: 0x040001AE RID: 430
		private readonly bool bool_0;
	}
}
