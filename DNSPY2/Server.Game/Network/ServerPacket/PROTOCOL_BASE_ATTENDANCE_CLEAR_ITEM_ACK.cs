using System;
using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200004B RID: 75
	public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK : GameServerPacket
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00002F72 File Offset: 0x00001172
		public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum eventErrorEnum_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002F81 File Offset: 0x00001181
		public override void Write()
		{
			base.WriteH(2339);
			base.WriteD((uint)this.eventErrorEnum_0);
		}

		// Token: 0x04000097 RID: 151
		private readonly EventErrorEnum eventErrorEnum_0;
	}
}
