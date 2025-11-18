using System;
using System.Collections.Generic;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000105 RID: 261
	public class PROTOCOL_ROOM_TEAM_BALANCE_ACK : GameServerPacket
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00004993 File Offset: 0x00002B93
		public PROTOCOL_ROOM_TEAM_BALANCE_ACK(List<SlotChange> list_1, int int_2, int int_3)
		{
			this.list_0 = list_1;
			this.int_1 = int_2;
			this.int_0 = int_3;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00013A3C File Offset: 0x00011C3C
		public override void Write()
		{
			base.WriteH(3622);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
			base.WriteC((byte)this.list_0.Count);
			foreach (SlotChange slotChange in this.list_0)
			{
				base.WriteC((byte)slotChange.OldSlot.Id);
				base.WriteC((byte)slotChange.NewSlot.Id);
				base.WriteC((byte)slotChange.OldSlot.State);
				base.WriteC((byte)slotChange.NewSlot.State);
			}
		}

		// Token: 0x040001DF RID: 479
		private readonly int int_0;

		// Token: 0x040001E0 RID: 480
		private readonly int int_1;

		// Token: 0x040001E1 RID: 481
		private readonly List<SlotChange> list_0;
	}
}
