namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class FireNHitDataOnObject
    {
        public static FireNHitDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            FireNHitDataObjectInfo info1 = new FireNHitDataObjectInfo();
            info1.Portal = C.ReadUH();
            FireNHitDataObjectInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Passed on the portal [{info.Portal}]", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, FireNHitDataObjectInfo Info)
        {
            S.WriteH(Info.Portal);
        }
    }
}

