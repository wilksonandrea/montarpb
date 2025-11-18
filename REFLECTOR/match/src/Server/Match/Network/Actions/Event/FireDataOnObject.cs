namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class FireDataOnObject
    {
        public static FireDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            FireDataObjectInfo info1 = new FireDataObjectInfo();
            info1.DeathType = (CharaDeath) C.ReadC();
            info1.HitPart = C.ReadC();
            info1.Unk = C.ReadC();
            FireDataObjectInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Death Type: {info.DeathType}; Hit Part: {info.HitPart}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, FireDataObjectInfo Info)
        {
            S.WriteC((byte) Info.DeathType);
            S.WriteC(Info.HitPart);
            S.WriteC(Info.Unk);
        }
    }
}

