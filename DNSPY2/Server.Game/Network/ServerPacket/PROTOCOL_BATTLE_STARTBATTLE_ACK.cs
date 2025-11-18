using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200007B RID: 123
	public class PROTOCOL_BATTLE_STARTBATTLE_ACK : GameServerPacket
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		public PROTOCOL_BATTLE_STARTBATTLE_ACK(SlotModel slotModel_1, Account account_0, List<int> list_1, bool bool_1)
		{
			this.slotModel_0 = slotModel_1;
			this.roomModel_0 = account_0.Room;
			if (this.roomModel_0 != null)
			{
				this.bool_0 = bool_1;
				this.list_0 = list_1;
				if (!this.roomModel_0.IsBotMode() && this.roomModel_0.RoomType != RoomCondition.Tutorial)
				{
					AllUtils.CompleteMission(this.roomModel_0, account_0, slotModel_1, bool_1 ? MissionType.STAGE_ENTER : MissionType.STAGE_INTERCEPT, 0);
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BATTLE_STARTBATTLE_ACK()
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public override void Write()
		{
			base.WriteH(5132);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteB(this.method_0(this.roomModel_0, this.list_0));
			base.WriteC((byte)this.roomModel_0.Rounds);
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, true, false));
			base.WriteC((this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll) ? 2 : 0);
			if (this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				base.WriteH((ushort)(this.roomModel_0.IsDinoMode("DE") ? this.roomModel_0.FRDino : (this.roomModel_0.IsDinoMode("CC") ? this.roomModel_0.FRKills : this.roomModel_0.FRRounds)));
				base.WriteH((ushort)(this.roomModel_0.IsDinoMode("DE") ? this.roomModel_0.CTDino : (this.roomModel_0.IsDinoMode("CC") ? this.roomModel_0.CTKills : this.roomModel_0.CTRounds)));
			}
			base.WriteC((this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll) ? 2 : 0);
			if (this.roomModel_0.ThisModeHaveRounds() || this.roomModel_0.IsDinoMode("") || this.roomModel_0.RoomType == RoomCondition.FreeForAll)
			{
				base.WriteH(0);
				base.WriteH(0);
			}
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, false, false));
			base.WriteC((!this.bool_0) ? 1 : 0);
			base.WriteC((byte)this.slotModel_0.Id);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000DC30 File Offset: 0x0000BE30
		private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (roomModel_1.IsDinoMode(""))
				{
					int num = ((list_1.Count == 1 || roomModel_1.IsDinoMode("CC")) ? 255 : roomModel_1.TRex);
					syncServerPacket.WriteC((byte)num);
					syncServerPacket.WriteC(10);
					for (int i = 0; i < list_1.Count; i++)
					{
						int num2 = list_1[i];
						if ((num2 != roomModel_1.TRex && roomModel_1.IsDinoMode("DE")) || roomModel_1.IsDinoMode("CC"))
						{
							syncServerPacket.WriteC((byte)num2);
						}
					}
					int num3 = 8 - list_1.Count - ((num == 255) ? 1 : 0);
					for (int j = 0; j < num3; j++)
					{
						syncServerPacket.WriteC(byte.MaxValue);
					}
					syncServerPacket.WriteC(byte.MaxValue);
				}
				else
				{
					syncServerPacket.WriteB(new byte[10]);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040000F1 RID: 241
		private readonly RoomModel roomModel_0;

		// Token: 0x040000F2 RID: 242
		private readonly SlotModel slotModel_0;

		// Token: 0x040000F3 RID: 243
		private readonly bool bool_0;

		// Token: 0x040000F4 RID: 244
		private readonly List<int> list_0;
	}
}
