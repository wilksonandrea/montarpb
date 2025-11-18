namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event.Event;
    using System;

    public class ActionForObjectSync
    {
        public static ActionObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            ActionObjectInfo info1 = new ActionObjectInfo();
            info1.Unk1 = C.ReadC();
            info1.Unk2 = C.ReadC();
            ActionObjectInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot} Unk1: {info.Unk1}; Unk2: {info.Unk2}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, ActionObjectInfo Info)
        {
            S.WriteC(Info.Unk1);
            S.WriteC(Info.Unk2);
        }
    }
}

