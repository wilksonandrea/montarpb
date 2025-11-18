namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class StageInfoObjStatic
    {
        public static StageStaticInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            StageStaticInfo info1 = new StageStaticInfo();
            info1.IsDestroyed = C.ReadC();
            StageStaticInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"Sub Head: StageInfoObjStatic; Destroyed: {info.IsDestroyed}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, StageStaticInfo Info)
        {
            S.WriteC(Info.IsDestroyed);
        }
    }
}

