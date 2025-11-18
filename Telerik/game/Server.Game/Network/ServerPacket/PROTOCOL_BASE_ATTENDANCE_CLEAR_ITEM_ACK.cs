using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK : GameServerPacket
	{
		private readonly EventErrorEnum eventErrorEnum_0;

		public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum eventErrorEnum_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
		}

		public override void Write()
		{
			base.WriteH(2339);
			base.WriteD((uint)this.eventErrorEnum_0);
		}
	}
}