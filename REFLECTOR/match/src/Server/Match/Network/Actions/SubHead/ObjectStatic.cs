namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class ObjectStatic
    {
        public static ObjectStaticInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            ObjectStaticInfo info1 = new ObjectStaticInfo();
            info1.Type = C.ReadUH();
            info1.Life = C.ReadUH();
            info1.DestroyedBySlot = C.ReadC();
            ObjectStaticInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"Sub Head: ObjectStatic; Type: {info.Type}; Life: {info.Life}; Destroyed: {info.DestroyedBySlot}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, ObjectStaticInfo Info)
        {
            S.WriteH(Info.Type);
            S.WriteH(Info.Life);
            S.WriteC(Info.DestroyedBySlot);
        }
    }
}

