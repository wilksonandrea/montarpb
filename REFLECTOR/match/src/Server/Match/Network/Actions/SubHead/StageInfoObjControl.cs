namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class StageInfoObjControl
    {
        public static StageControlInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            StageControlInfo info1 = new StageControlInfo();
            info1.Unk = C.ReadB(9);
            StageControlInfo info = info1;
            if (GenLog)
            {
                CLogger.Print("Sub Head: StageInfoObjControl; " + Bitwise.ToHexData("Controled Object Hex Data", info.Unk), LoggerType.Opcode, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, StageControlInfo Info)
        {
            S.WriteB(Info.Unk);
        }
    }
}

