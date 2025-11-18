using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Sync.Client
{
	public static class RoomPassPortal
	{
		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadH();
			int ınt321 = C.ReadH();
			short ınt16 = C.ReadH();
			int ınt322 = C.ReadC();
			C.ReadC();
			if ((int)C.ToArray().Length > 10)
			{
				CLogger.Print(string.Format("Invalid Portal (Length > 10): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(ınt16, ınt321);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(ınt32);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.IsDinoMode("DE"))
			{
				SlotModel slot = room.GetSlot(ınt322);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					slot.PassSequence++;
					if (slot.Team != TeamEnum.FR_TEAM)
					{
						room.CTDino += 5;
					}
					else
					{
						room.FRDino += 5;
					}
					RoomPassPortal.smethod_0(room, slot);
					using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK pROTOCOLBATTLEMISSIONTOUCHDOWNACK = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(room, slot))
					{
						using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK pROTOCOLBATTLEMISSIONTOUCHDOWNCOUNTACK = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
						{
							room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONTOUCHDOWNACK, pROTOCOLBATTLEMISSIONTOUCHDOWNCOUNTACK, SlotState.BATTLE, 0);
						}
					}
				}
			}
		}

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