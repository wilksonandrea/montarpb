using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200006C RID: 108
	public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK : GameServerPacket
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000033C2 File Offset: 0x000015C2
		public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(RoomModel roomModel_1, SlotModel slotModel_1)
		{
			this.roomModel_0 = roomModel_1;
			this.slotModel_0 = slotModel_1;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000D528 File Offset: 0x0000B728
		public override void Write()
		{
			base.WriteH(5179);
			base.WriteH((ushort)this.roomModel_0.FRDino);
			base.WriteH((ushort)this.roomModel_0.CTDino);
			base.WriteD(this.slotModel_0.Id);
			base.WriteH((short)this.slotModel_0.PassSequence);
		}

		// Token: 0x040000D9 RID: 217
		private readonly RoomModel roomModel_0;

		// Token: 0x040000DA RID: 218
		private readonly SlotModel slotModel_0;
	}
}
