using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001FD RID: 509
	public class RoomSadeSync
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x00030D04 File Offset: 0x0002EF04
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadH();
			int num2 = (int)C.ReadH();
			int num3 = (int)C.ReadH();
			byte b = C.ReadC();
			ushort num4 = C.ReadUH();
			ushort num5 = C.ReadUH();
			int num6 = (int)C.ReadC();
			ushort num7 = C.ReadUH();
			if (C.ToArray().Length > 16)
			{
				CLogger.Print(string.Format("Invalid Sabotage (Length > 16): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(num3, num2);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(num);
			SlotModel slotModel;
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.GetSlot((int)b, out slotModel))
			{
				room.Bar1 = (int)num4;
				room.Bar2 = (int)num5;
				RoomCondition roomType = room.RoomType;
				int num8 = 0;
				if (num6 == 1)
				{
					SlotModel slotModel2 = slotModel;
					slotModel2.DamageBar1 += num7;
					num8 += (int)(slotModel.DamageBar1 / 600);
				}
				else if (num6 == 2)
				{
					SlotModel slotModel3 = slotModel;
					slotModel3.DamageBar2 += num7;
					num8 += (int)(slotModel.DamageBar2 / 600);
				}
				slotModel.EarnedEXP = num8;
				if (roomType == RoomCondition.Destroy)
				{
					using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK protocol_BATTLE_MISSION_GENERATOR_INFO_ACK = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
					{
						room.SendPacketToPlayers(protocol_BATTLE_MISSION_GENERATOR_INFO_ACK, SlotState.BATTLE, 0);
					}
					if (room.Bar1 == 0)
					{
						RoomSadeSync.EndRound(room, (!room.SwapRound) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
						return;
					}
					if (room.Bar2 == 0)
					{
						RoomSadeSync.EndRound(room, (!room.SwapRound) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
						return;
					}
				}
				else if (roomType == RoomCondition.Defense)
				{
					using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK protocol_BATTLE_MISSION_DEFENCE_INFO_ACK = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
					{
						room.SendPacketToPlayers(protocol_BATTLE_MISSION_DEFENCE_INFO_ACK, SlotState.BATTLE, 0);
					}
					if (room.Bar1 == 0 && room.Bar2 == 0)
					{
						RoomSadeSync.EndRound(room, (!room.SwapRound) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
					}
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00005B8C File Offset: 0x00003D8C
		public static void EndRound(RoomModel Room, TeamEnum Winner)
		{
			if (Winner == TeamEnum.FR_TEAM)
			{
				Room.FRRounds++;
			}
			else if (Winner == TeamEnum.CT_TEAM)
			{
				Room.CTRounds++;
			}
			AllUtils.BattleEndRound(Room, Winner, RoundEndType.Normal);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000025DF File Offset: 0x000007DF
		public RoomSadeSync()
		{
		}
	}
}
