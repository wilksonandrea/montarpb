using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200006B RID: 107
	public class PROTOCOL_BATTLE_MISSION_ROUND_START_ACK : GameServerPacket
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000033B3 File Offset: 0x000015B3
		public PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000D47C File Offset: 0x0000B67C
		public override void Write()
		{
			base.WriteH(5153);
			base.WriteC((byte)this.roomModel_0.Rounds);
			base.WriteD(this.roomModel_0.GetInBattleTimeLeft());
			base.WriteD(AllUtils.GetSlotsFlag(this.roomModel_0, true, false));
			base.WriteC(this.roomModel_0.SwapRound ? 3 : 0);
			base.WriteH((ushort)this.roomModel_0.FRRounds);
			base.WriteH((ushort)this.roomModel_0.CTRounds);
			if (this.roomModel_0.SwapRound)
			{
				base.WriteB(this.method_0(this.roomModel_0));
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000D380 File Offset: 0x0000B580
		private byte[] method_0(RoomModel roomModel_1)
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

		// Token: 0x040000D8 RID: 216
		private readonly RoomModel roomModel_0;
	}
}
