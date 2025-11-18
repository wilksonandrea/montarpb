namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class SeizeDataForClient
    {
        public static SeizeDataForClientInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            SeizeDataForClientInfo info1 = new SeizeDataForClientInfo();
            info1.UseTime = C.ReadT();
            info1.Flags = C.ReadC();
            SeizeDataForClientInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Use Time: {info.UseTime}; Flags: {info.Flags}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, SeizeDataForClientInfo Info)
        {
            S.WriteT(Info.UseTime);
            S.WriteC(Info.Flags);
        }
    }
}

