namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CharaFireNHitData
    {
        public static List<CharaFireNHitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
        {
            List<CharaFireNHitDataInfo> list = new List<CharaFireNHitDataInfo>();
            int num = C.ReadC();
            for (int i = 0; i < num; i++)
            {
                CharaFireNHitDataInfo info1 = new CharaFireNHitDataInfo();
                info1.WeaponId = C.ReadD();
                info1.Accessory = C.ReadC();
                info1.Extensions = C.ReadC();
                info1.HitInfo = C.ReadUD();
                info1.Unk = C.ReadH();
                info1.X = C.ReadUH();
                info1.Y = C.ReadUH();
                info1.Z = C.ReadUH();
                CharaFireNHitDataInfo item = info1;
                if (!OnlyBytes)
                {
                    item.WeaponClass = (ClassType) ComDiv.GetIdStatics(item.WeaponId, 2);
                }
                if (GenLog)
                {
                    CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {item.WeaponId}; X: {item.X} Y: {item.Y} Z: {item.Z}", LoggerType.Warning, null);
                }
                list.Add(item);
            }
            return list;
        }

        public static void WriteInfo(SyncServerPacket S, List<CharaFireNHitDataInfo> Hits)
        {
            S.WriteC((byte) Hits.Count);
            foreach (CharaFireNHitDataInfo info in Hits)
            {
                S.WriteD(info.WeaponId);
                S.WriteC(info.Accessory);
                S.WriteC(info.Extensions);
                S.WriteD(info.HitInfo);
                S.WriteH(info.Unk);
                S.WriteH(info.X);
                S.WriteH(info.Y);
                S.WriteH(info.Z);
            }
        }
    }
}

