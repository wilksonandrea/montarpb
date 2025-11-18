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
	// Token: 0x020001FB RID: 507
	public static class RoomPassPortal
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0003097C File Offset: 0x0002EB7C
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadH();
			int num2 = (int)C.ReadH();
			int num3 = (int)C.ReadH();
			int num4 = (int)C.ReadC();
			C.ReadC();
			if (C.ToArray().Length > 10)
			{
				CLogger.Print(string.Format("Invalid Portal (Length > 10): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(num3, num2);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(num);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.IsDinoMode("DE"))
			{
				SlotModel slot = room.GetSlot(num4);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					slot.PassSequence++;
					if (slot.Team == TeamEnum.FR_TEAM)
					{
						room.FRDino += 5;
					}
					else
					{
						room.CTDino += 5;
					}
					RoomPassPortal.smethod_0(room, slot);
					using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK protocol_BATTLE_MISSION_TOUCHDOWN_ACK = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(room, slot))
					{
						using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK protocol_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
						{
							room.SendPacketToPlayers(protocol_BATTLE_MISSION_TOUCHDOWN_ACK, protocol_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK, SlotState.BATTLE, 0);
						}
					}
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00030AD4 File Offset: 0x0002ECD4
		private static void smethod_0(RoomModel roomModel_0, SlotModel slotModel_0)
		{
			MissionType missionType = MissionType.NA;
			if (slotModel_0.PassSequence == 1)
			{
				missionType = MissionType.TOUCHDOWN;
			}
			else if (slotModel_0.PassSequence == 2)
			{
				missionType = MissionType.TOUCHDOWN_ACE_ATTACKER;
			}
			else if (slotModel_0.PassSequence == 3)
			{
				missionType = MissionType.TOUCHDOWN_HATTRICK;
			}
			else if (slotModel_0.PassSequence >= 4)
			{
				missionType = MissionType.TOUCHDOWN_GAME_MAKER;
			}
			if (missionType != MissionType.NA)
			{
				AllUtils.CompleteMission(roomModel_0, slotModel_0, missionType, 0);
			}
		}
	}
}
