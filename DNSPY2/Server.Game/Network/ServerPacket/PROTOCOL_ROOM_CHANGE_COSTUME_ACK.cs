using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E7 RID: 231
	public class PROTOCOL_ROOM_CHANGE_COSTUME_ACK : GameServerPacket
	{
		// Token: 0x06000236 RID: 566 RVA: 0x000044F7 File Offset: 0x000026F7
		public PROTOCOL_ROOM_CHANGE_COSTUME_ACK(SlotModel slotModel_1)
		{
			this.slotModel_0 = slotModel_1;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00004506 File Offset: 0x00002706
		public override void Write()
		{
			base.WriteH(3678);
			base.WriteD(this.slotModel_0.Id);
			base.WriteC((byte)this.slotModel_0.CostumeTeam);
		}

		// Token: 0x040001AB RID: 427
		private readonly SlotModel slotModel_0;
	}
}
