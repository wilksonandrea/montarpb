using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Update;

public class KillFragInfo
{
	public static void GenDeath(RoomModel Room, SlotModel Killer, FragInfos Kill, bool IsSuicide)
	{
		bool flag = Room.IsBotMode();
		RoomDeath.RegistryFragInfos(Room, Killer, out var Score, flag, IsSuicide, Kill);
		if (flag)
		{
			Killer.Score += Killer.KillsOnLife + Room.IngameAiLevel + Score;
			if (Killer.Score > 65535)
			{
				Killer.Score = 65535;
				CLogger.Print("[PlayerId: " + Killer.Id + "] reached the maximum score of the BOT.", LoggerType.Warning);
			}
			Kill.Score = Killer.Score;
		}
		else
		{
			Killer.Score += Score;
			AllUtils.CompleteMission(Room, Killer, Kill, MissionType.NA, 0);
			Kill.Score = Score;
		}
		using (PROTOCOL_BATTLE_DEATH_ACK packet = new PROTOCOL_BATTLE_DEATH_ACK(Room, Kill, Killer))
		{
			Room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		RoomDeath.EndBattleByDeath(Room, Killer, flag, IsSuicide, Kill);
	}
}
