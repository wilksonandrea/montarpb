using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Update
{
	// Token: 0x020001E5 RID: 485
	public class KillFragInfo
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0002E99C File Offset: 0x0002CB9C
		public static void GenDeath(RoomModel Room, SlotModel Killer, FragInfos Kill, bool IsSuicide)
		{
			bool flag = Room.IsBotMode();
			int num;
			RoomDeath.RegistryFragInfos(Room, Killer, out num, flag, IsSuicide, Kill);
			if (flag)
			{
				Killer.Score += Killer.KillsOnLife + (int)Room.IngameAiLevel + num;
				if (Killer.Score > 65535)
				{
					Killer.Score = 65535;
					CLogger.Print("[PlayerId: " + Killer.Id.ToString() + "] reached the maximum score of the BOT.", LoggerType.Warning, null);
				}
				Kill.Score = Killer.Score;
			}
			else
			{
				Killer.Score += num;
				AllUtils.CompleteMission(Room, Killer, Kill, MissionType.NA, 0);
				Kill.Score = num;
			}
			using (PROTOCOL_BATTLE_DEATH_ACK protocol_BATTLE_DEATH_ACK = new PROTOCOL_BATTLE_DEATH_ACK(Room, Kill, Killer))
			{
				Room.SendPacketToPlayers(protocol_BATTLE_DEATH_ACK, SlotState.BATTLE, 0);
			}
			RoomDeath.EndBattleByDeath(Room, Killer, flag, IsSuicide, Kill);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000025DF File Offset: 0x000007DF
		public KillFragInfo()
		{
		}
	}
}
