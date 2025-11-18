namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class Animation
    {
        public static AnimationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            AnimationInfo info1 = new AnimationInfo();
            info1.Animation = C.ReadUH();
            AnimationInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; POV: {info.Animation}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, AnimationInfo Info)
        {
            S.WriteH(Info.Animation);
        }
    }
}

