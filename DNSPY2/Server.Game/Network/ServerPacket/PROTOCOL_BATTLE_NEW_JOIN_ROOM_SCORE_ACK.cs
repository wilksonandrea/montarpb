using System;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200006F RID: 111
	public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK : GameServerPacket
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000345C File Offset: 0x0000165C
		public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000D588 File Offset: 0x0000B788
		public override void Write()
		{
			base.WriteH(5263);
			base.WriteD(this.method_1());
			base.WriteD(this.roomModel_0.GetTimeByMask() * 60 - this.roomModel_0.GetInBattleTime());
			base.WriteB(this.method_0(this.roomModel_0));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		private byte[] method_0(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (roomModel_1.IsDinoMode("DE"))
				{
					syncServerPacket.WriteD(roomModel_1.FRDino);
					syncServerPacket.WriteD(roomModel_1.CTDino);
				}
				else if (roomModel_1.RoomType == RoomCondition.DeathMatch && !roomModel_1.IsBotMode())
				{
					syncServerPacket.WriteD(roomModel_1.FRKills);
					syncServerPacket.WriteD(roomModel_1.CTKills);
				}
				else if (roomModel_1.RoomType == RoomCondition.FreeForAll)
				{
					syncServerPacket.WriteD(this.GetSlotKill());
					syncServerPacket.WriteD(0);
				}
				else if (roomModel_1.IsBotMode())
				{
					syncServerPacket.WriteD((int)roomModel_1.IngameAiLevel);
					syncServerPacket.WriteD(0);
				}
				else
				{
					syncServerPacket.WriteD(roomModel_1.FRRounds);
					syncServerPacket.WriteD(roomModel_1.CTRounds);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		private int method_1()
		{
			if (this.roomModel_0.IsBotMode())
			{
				return 3;
			}
			if (this.roomModel_0.RoomType == RoomCondition.DeathMatch && !this.roomModel_0.IsBotMode())
			{
				return 1;
			}
			if (this.roomModel_0.IsDinoMode(""))
			{
				return 4;
			}
			if (this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				return 5;
			}
			return 2;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000D720 File Offset: 0x0000B920
		public int GetSlotKill()
		{
			int[] array = new int[18];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.roomModel_0.Slots[i].AllKills;
			}
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] > array[num])
				{
					num = j;
				}
			}
			return num;
		}

		// Token: 0x040000DD RID: 221
		private readonly RoomModel roomModel_0;
	}
}
