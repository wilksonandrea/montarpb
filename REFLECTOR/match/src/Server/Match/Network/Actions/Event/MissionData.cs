namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;
    using System.Runtime.InteropServices;

    public class MissionData
    {
        public static MissionDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog, bool OnlyBytes = false)
        {
            MissionDataInfo info1 = new MissionDataInfo();
            info1.PlantTime = C.ReadT();
            info1.Bomb = C.ReadC();
            MissionDataInfo info = info1;
            if (!OnlyBytes)
            {
                info.BombEnum = ((BombFlag) info.Bomb) & (BombFlag.Unknown | BombFlag.Defuse | BombFlag.Stop | BombFlag.Start);
                info.BombId = info.Bomb >> 4;
            }
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Bomb: {info.BombEnum}; Id: {info.BombId}; PlantTime: {info.PlantTime}; Time: {Time}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, MissionDataInfo Info)
        {
            S.WriteT(Info.PlantTime);
            S.WriteC((byte) Info.Bomb);
        }
    }
}

