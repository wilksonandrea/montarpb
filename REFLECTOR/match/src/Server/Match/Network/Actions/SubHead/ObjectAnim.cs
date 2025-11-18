namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class ObjectAnim
    {
        public static ObjectAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            ObjectAnimInfo info1 = new ObjectAnimInfo();
            info1.Life = C.ReadUH();
            info1.Anim1 = C.ReadC();
            info1.Anim2 = C.ReadC();
            info1.SyncDate = C.ReadT();
            ObjectAnimInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"Sub Head: ObjectAnim; Life: {info.Life}; Animation[1]: {info.Anim1}; Animation[2]: {info.Anim2}; Sync: {info.SyncDate}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, ObjectAnimInfo Info)
        {
            S.WriteH(Info.Life);
            S.WriteC(Info.Anim1);
            S.WriteC(Info.Anim2);
            S.WriteT(Info.SyncDate);
        }
    }
}

