using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200006A RID: 106
	public class PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK : GameServerPacket
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000339D File Offset: 0x0000159D
		public PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(RoomModel roomModel_1, List<int> list_1)
		{
			this.roomModel_0 = roomModel_1;
			this.list_0 = list_1;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public override void Write()
		{
			base.WriteH(5151);
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, false, false));
			base.WriteB(this.method_0(this.roomModel_0, this.list_0));
			base.WriteC(this.roomModel_0.SwapRound ? 3 : 0);
			if (this.roomModel_0.SwapRound)
			{
				base.WriteB(this.method_1(this.roomModel_0));
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000D258 File Offset: 0x0000B458
		private byte[] method_0(RoomModel roomModel_1, List<int> list_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (roomModel_1.IsBotMode())
				{
					syncServerPacket.WriteB(Bitwise.HexStringToByteArray("FF FF FF FF FF FF FF FF FF FF"));
				}
				else if (roomModel_1.IsDinoMode(""))
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

		// Token: 0x06000121 RID: 289 RVA: 0x0000D380 File Offset: 0x0000B580
		private byte[] method_1(RoomModel roomModel_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (SlotModel slotModel in roomModel_1.Slots)
				{
					PlayerEquipment equipment = slotModel.Equipment;
					if (equipment != null)
					{
						if (slotModel.Team == TeamEnum.FR_TEAM)
						{
							syncServerPacket.WriteD((!roomModel_1.SwapRound) ? equipment.CharaRedId : equipment.CharaBlueId);
						}
						else if (slotModel.Team == TeamEnum.CT_TEAM)
						{
							syncServerPacket.WriteD((!roomModel_1.SwapRound) ? equipment.CharaBlueId : equipment.CharaRedId);
						}
					}
					else if (slotModel.Team == TeamEnum.FR_TEAM)
					{
						syncServerPacket.WriteD((!roomModel_1.SwapRound) ? 601001 : 602002);
					}
					else if (slotModel.Team == TeamEnum.CT_TEAM)
					{
						syncServerPacket.WriteD((!roomModel_1.SwapRound) ? 602002 : 601001);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040000D6 RID: 214
		private readonly RoomModel roomModel_0;

		// Token: 0x040000D7 RID: 215
		private readonly List<int> list_0;
	}
}
