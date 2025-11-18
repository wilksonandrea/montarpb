namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;
    using System.Collections.Generic;

    public class UseObject
    {
        public static List<UseObjectInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            List<UseObjectInfo> list = new List<UseObjectInfo>();
            int num = C.ReadC();
            for (int i = 0; i < num; i++)
            {
                UseObjectInfo info1 = new UseObjectInfo();
                info1.ObjectId = C.ReadUH();
                info1.Use = C.ReadC();
                info1.SpaceFlags = (CharaMoves) C.ReadC();
                UseObjectInfo item = info1;
                if (GenLog)
                {
                    CLogger.Print($"PVP Slot: {Action.Slot}; Use Object: {item.Use}; Flag: {item.SpaceFlags}; ObjectId: {item.ObjectId}", LoggerType.Warning, null);
                }
                list.Add(item);
            }
            return list;
        }

        public static void WriteInfo(SyncServerPacket S, List<UseObjectInfo> Infos)
        {
            S.WriteC((byte) Infos.Count);
            foreach (UseObjectInfo info in Infos)
            {
                S.WriteH(info.ObjectId);
                S.WriteC(info.Use);
                S.WriteC((byte) info.SpaceFlags);
            }
        }
    }
}

