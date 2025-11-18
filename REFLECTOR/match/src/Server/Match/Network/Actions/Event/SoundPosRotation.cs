namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class SoundPosRotation
    {
        public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            SoundPosRotationInfo info1 = new SoundPosRotationInfo();
            info1.Time = C.ReadT();
            SoundPosRotationInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Time: {info.Time}", LoggerType.Warning, null);
            }
            return info;
        }

        public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog)
        {
            SoundPosRotationInfo info1 = new SoundPosRotationInfo();
            info1.Time = Time;
            SoundPosRotationInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Time: {info.Time}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, SoundPosRotationInfo Info)
        {
            S.WriteT(Info.Time);
        }
    }
}

