using System;
using Plugin.Core.Enums;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000069 RID: 105
	public class PROTOCOL_BATTLE_MISSION_ROUND_END_ACK : GameServerPacket
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00003380 File Offset: 0x00001580
		public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(RoomModel roomModel_1, int int_1, RoundEndType roundEndType_1)
		{
			this.roomModel_0 = roomModel_1;
			this.int_0 = int_1;
			this.roundEndType_0 = roundEndType_1;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003380 File Offset: 0x00001580
		public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(RoomModel roomModel_1, TeamEnum teamEnum_0, RoundEndType roundEndType_1)
		{
			this.roomModel_0 = roomModel_1;
			this.int_0 = (int)teamEnum_0;
			this.roundEndType_0 = roundEndType_1;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000D108 File Offset: 0x0000B308
		public override void Write()
		{
			base.WriteH(5155);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.roundEndType_0);
			if (this.roomModel_0.IsDinoMode("DE"))
			{
				base.WriteH((ushort)this.roomModel_0.FRDino);
				base.WriteH((ushort)this.roomModel_0.CTDino);
				return;
			}
			if (this.roomModel_0.RoomType != RoomCondition.DeathMatch)
			{
				if (this.roomModel_0.RoomType != RoomCondition.FreeForAll)
				{
					base.WriteH((ushort)this.roomModel_0.FRRounds);
					base.WriteH((ushort)this.roomModel_0.CTRounds);
					return;
				}
			}
			base.WriteH((ushort)this.roomModel_0.FRKills);
			base.WriteH((ushort)this.roomModel_0.CTKills);
		}

		// Token: 0x040000D3 RID: 211
		private readonly RoomModel roomModel_0;

		// Token: 0x040000D4 RID: 212
		private readonly int int_0;

		// Token: 0x040000D5 RID: 213
		private readonly RoundEndType roundEndType_0;
	}
}
