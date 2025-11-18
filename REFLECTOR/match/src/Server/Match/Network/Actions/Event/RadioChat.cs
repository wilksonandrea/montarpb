namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class RadioChat
    {
        public static RadioChatInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            RadioChatInfo info1 = new RadioChatInfo();
            info1.RadioId = C.ReadC();
            info1.AreaId = C.ReadC();
            RadioChatInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot} Radio: {info.RadioId} Area: {info.AreaId}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, RadioChatInfo Info)
        {
            S.WriteC(Info.RadioId);
            S.WriteC(Info.AreaId);
        }
    }
}

