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
	public class RoomSadeSync
	{
		public RoomSadeSync()
		{
		}

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

		public static void Load(SyncClientPacket C)
		{
			SlotModel slotModel;
			int ınt32 = C.ReadH();
			int ınt321 = C.ReadH();
			short ınt16 = C.ReadH();
			byte num = C.ReadC();
			ushort uInt16 = C.ReadUH();
			ushort uInt161 = C.ReadUH();
			int ınt322 = C.ReadC();
			ushort uInt162 = C.ReadUH();
			if ((int)C.ToArray().Length > 16)
			{
				CLogger.Print(string.Format("Invalid Sabotage (Length > 16): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(ınt16, ınt321);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(ınt32);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.GetSlot((int)num, out slotModel))
			{
				room.Bar1 = uInt16;
				room.Bar2 = uInt161;
				RoomCondition roomType = room.RoomType;
				int damageBar1 = 0;
				if (ınt322 == 1)
				{
					SlotModel damageBar11 = slotModel;
					damageBar11.DamageBar1 = (ushort)(damageBar11.DamageBar1 + uInt162);
					damageBar1 = damageBar1 + slotModel.DamageBar1 / 600;
				}
				else if (ınt322 == 2)
				{
					SlotModel damageBar2 = slotModel;
					damageBar2.DamageBar2 = (ushort)(damageBar2.DamageBar2 + uInt162);
					damageBar1 = damageBar1 + slotModel.DamageBar2 / 600;
				}
				slotModel.EarnedEXP = damageBar1;
				if (roomType == RoomCondition.Destroy)
				{
					using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK pROTOCOLBATTLEMISSIONGENERATORINFOACK = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
					{
						room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONGENERATORINFOACK, SlotState.BATTLE, 0);
					}
					if (room.Bar1 == 0)
					{
						RoomSadeSync.EndRound(room, (!room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM));
						return;
					}
					if (room.Bar2 == 0)
					{
						RoomSadeSync.EndRound(room, (!room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM));
						return;
					}
				}
				else if (roomType == RoomCondition.Defense)
				{
					using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK pROTOCOLBATTLEMISSIONDEFENCEINFOACK = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
					{
						room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONDEFENCEINFOACK, SlotState.BATTLE, 0);
					}
					if (room.Bar1 == 0 && room.Bar2 == 0)
					{
						RoomSadeSync.EndRound(room, (!room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM));
					}
				}
			}
		}
	}
}