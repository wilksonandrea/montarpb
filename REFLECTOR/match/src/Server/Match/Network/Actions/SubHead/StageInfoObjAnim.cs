namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class StageInfoObjAnim
    {
        public static StageAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            StageAnimInfo info1 = new StageAnimInfo();
            info1.Unk = C.ReadC();
            info1.Life = C.ReadUH();
            info1.SyncDate = C.ReadT();
            info1.Anim1 = C.ReadC();
            info1.Anim2 = C.ReadC();
            StageAnimInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"Sub Head: StageObjAnim; Unk: {info.Unk}; Life: {info.Life}; Sync: {info.SyncDate}; Animation[1]: {info.Anim1}; Animation[2]: {info.Anim2}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, StageAnimInfo Info)
        {
            S.WriteC(Info.Unk);
            S.WriteH(Info.Life);
            S.WriteT(Info.SyncDate);
            S.WriteC(Info.Anim1);
            S.WriteC(Info.Anim2);
        }
    }
}

