using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000DF RID: 223
	public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK : GameServerPacket
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00004444 File Offset: 0x00002644
		public PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(uint uint_1, List<QuickstartModel> list_1, RoomModel roomModel_1, QuickstartModel quickstartModel_1)
		{
			this.uint_0 = uint_1;
			this.list_0 = list_1;
			this.quickstartModel_0 = quickstartModel_1;
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public override void Write()
		{
			base.WriteH(5378);
			base.WriteD(this.uint_0);
			foreach (QuickstartModel quickstartModel in this.list_0)
			{
				base.WriteC((byte)quickstartModel.MapId);
				base.WriteC((byte)quickstartModel.Rule);
				base.WriteC((byte)quickstartModel.StageOptions);
				base.WriteC((byte)quickstartModel.Type);
			}
			if (this.uint_0 == 0U)
			{
				base.WriteC((byte)this.roomModel_0.ChannelId);
				base.WriteD(this.roomModel_0.RoomId);
				base.WriteH(1);
				if (this.uint_0 != 0U)
				{
					base.WriteC((byte)this.quickstartModel_0.MapId);
					base.WriteC((byte)this.quickstartModel_0.Rule);
					base.WriteC((byte)this.quickstartModel_0.StageOptions);
					base.WriteC((byte)this.quickstartModel_0.Type);
				}
				else
				{
					base.WriteC(0);
					base.WriteC(0);
					base.WriteC(0);
					base.WriteC(0);
				}
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
			}
		}

		// Token: 0x04000197 RID: 407
		private readonly uint uint_0;

		// Token: 0x04000198 RID: 408
		private readonly List<QuickstartModel> list_0;

		// Token: 0x04000199 RID: 409
		private readonly QuickstartModel quickstartModel_0;

		// Token: 0x0400019A RID: 410
		private readonly RoomModel roomModel_0;
	}
}
