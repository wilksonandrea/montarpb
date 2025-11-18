namespace Server.Game.Data.Sync.Update
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Client;
    using Server.Game.Data.Utils;
    using Server.Game.Network.ServerPacket;
    using System;

    public class KillFragInfo
    {
        public static void GenDeath(RoomModel Room, SlotModel Killer, FragInfos Kill, bool IsSuicide)
        {
            int num;
            bool flag = Room.IsBotMode();
            RoomDeath.RegistryFragInfos(Room, Killer, out num, flag, IsSuicide, Kill);
            if (!flag)
            {
                Killer.Score += num;
                AllUtils.CompleteMission(Room, Killer, Kill, MissionType.NA, 0);
                Kill.Score = num;
            }
            else
            {
                Killer.Score += (Killer.KillsOnLife + Room.IngameAiLevel) + num;
                if (Killer.Score > 0xffff)
                {
                    Killer.Score = 0xffff;
                    CLogger.Print("[PlayerId: " + Killer.Id.ToString() + "] reached the maximum score of the BOT.", LoggerType.Warning, null);
                }
                Kill.Score = Killer.Score;
            }
            using (PROTOCOL_BATTLE_DEATH_ACK protocol_battle_death_ack = new PROTOCOL_BATTLE_DEATH_ACK(Room, Kill, Killer))
            {
                Room.SendPacketToPlayers(protocol_battle_death_ack, SlotState.BATTLE, 0);
            }
            RoomDeath.EndBattleByDeath(Room, Killer, flag, IsSuicide, Kill);
        }
    }
}

