using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200004A RID: 74
	public class PROTOCOL_BASE_ATTENDANCE_ACK : GameServerPacket
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000B1FC File Offset: 0x000093FC
		public PROTOCOL_BASE_ATTENDANCE_ACK(EventErrorEnum eventErrorEnum_1, EventVisitModel eventVisitModel_1, PlayerEvent playerEvent_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
			this.eventVisitModel_0 = eventVisitModel_1;
			this.playerEvent_0 = playerEvent_1;
			if (playerEvent_1 != null)
			{
				this.uint_0 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
				this.uint_1 = uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", playerEvent_1.LastVisitDate))));
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000B270 File Offset: 0x00009470
		public override void Write()
		{
			base.WriteH(2337);
			base.WriteD((uint)this.eventErrorEnum_0);
			if (this.eventErrorEnum_0 == (EventErrorEnum)2147489028U)
			{
				base.WriteD(this.eventVisitModel_0.Id);
				base.WriteC((byte)this.playerEvent_0.LastVisitCheckDay);
				base.WriteC((byte)(this.playerEvent_0.LastVisitCheckDay - 1));
				base.WriteC((this.uint_1 < this.uint_0) ? 1 : 2);
				base.WriteC((byte)this.playerEvent_0.LastVisitSeqType);
				base.WriteC(1);
			}
		}

		// Token: 0x04000092 RID: 146
		private readonly EventVisitModel eventVisitModel_0;

		// Token: 0x04000093 RID: 147
		private readonly PlayerEvent playerEvent_0;

		// Token: 0x04000094 RID: 148
		private readonly EventErrorEnum eventErrorEnum_0;

		// Token: 0x04000095 RID: 149
		private readonly uint uint_0;

		// Token: 0x04000096 RID: 150
		private readonly uint uint_1;
	}
}
