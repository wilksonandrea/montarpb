using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_ATTENDANCE_ACK : GameServerPacket
	{
		private readonly EventVisitModel eventVisitModel_0;

		private readonly PlayerEvent playerEvent_0;

		private readonly EventErrorEnum eventErrorEnum_0;

		private readonly uint uint_0;

		private readonly uint uint_1;

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

		public override void Write()
		{
			base.WriteH(2337);
			base.WriteD((uint)this.eventErrorEnum_0);
			if (this.eventErrorEnum_0 == EventErrorEnum.VISIT_EVENT_SUCCESS)
			{
				base.WriteD(this.eventVisitModel_0.Id);
				base.WriteC((byte)this.playerEvent_0.LastVisitCheckDay);
				base.WriteC((byte)(this.playerEvent_0.LastVisitCheckDay - 1));
				base.WriteC((byte)((this.uint_1 < this.uint_0 ? 1 : 2)));
				base.WriteC((byte)this.playerEvent_0.LastVisitSeqType);
				base.WriteC(1);
			}
		}
	}
}