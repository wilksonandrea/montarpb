using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Sync.Update
{
	public class KillFragInfo
	{
		public KillFragInfo()
		{
		}

		public static void GenDeath(RoomModel Room, SlotModel Killer, FragInfos Kill, bool IsSuicide)
		{
			int ınt32;
			bool flag = Room.IsBotMode();
			RoomDeath.RegistryFragInfos(Room, Killer, out ınt32, flag, IsSuicide, Kill);
			if (!flag)
			{
				Killer.Score += ınt32;
				AllUtils.CompleteMission(Room, Killer, Kill, MissionType.NA, 0);
				Kill.Score = ınt32;
			}
			else
			{
				SlotModel killer = Killer;
				killer.Score = killer.Score + Killer.KillsOnLife + Room.IngameAiLevel + ınt32;
				if (Killer.Score > 65535)
				{
					Killer.Score = 65535;
					CLogger.Print(string.Concat("[PlayerId: ", Killer.Id.ToString(), "] reached the maximum score of the BOT."), LoggerType.Warning, null);
				}
				Kill.Score = Killer.Score;
			}
			using (PROTOCOL_BATTLE_DEATH_ACK pROTOCOLBATTLEDEATHACK = new PROTOCOL_BATTLE_DEATH_ACK(Room, Kill, Killer))
			{
				Room.SendPacketToPlayers(pROTOCOLBATTLEDEATHACK, SlotState.BATTLE, 0);
			}
			RoomDeath.EndBattleByDeath(Room, Killer, flag, IsSuicide, Kill);
		}
	}
}