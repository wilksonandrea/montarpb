using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200005F RID: 95
	public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK : GameServerPacket
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000326D File Offset: 0x0000146D
		public PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000C098 File Offset: 0x0000A298
		public override void Write()
		{
			base.WriteH(5173);
			base.WriteC(this.roomModel_0.IngameAiLevel);
			for (int i = 0; i < 18; i++)
			{
				base.WriteD(this.roomModel_0.Slots[i].AiLevel);
			}
		}

		// Token: 0x040000BA RID: 186
		private readonly RoomModel roomModel_0;
	}
}
