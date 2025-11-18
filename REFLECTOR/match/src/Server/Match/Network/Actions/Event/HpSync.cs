namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class HpSync
    {
        public static HPSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            HPSyncInfo info1 = new HPSyncInfo();
            info1.CharaLife = C.ReadUH();
            HPSyncInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; is using Chara with HP ({info.CharaLife})", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, HPSyncInfo Info)
        {
            S.WriteH(Info.CharaLife);
        }
    }
}

