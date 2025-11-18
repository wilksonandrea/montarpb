namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class ObjectMove
    {
        public static ObjectMoveInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            ObjectMoveInfo info1 = new ObjectMoveInfo();
            info1.Unk = C.ReadB(0x10);
            ObjectMoveInfo info = info1;
            if (GenLog)
            {
                CLogger.Print(Bitwise.ToHexData("UDP_SUB_HEAD: OBJECT_MOVE", info.Unk), LoggerType.Opcode, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, ObjectMoveInfo Info)
        {
            S.WriteB(Info.Unk);
        }
    }
}

