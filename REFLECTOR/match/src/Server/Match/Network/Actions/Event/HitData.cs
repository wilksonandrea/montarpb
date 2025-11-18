namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using Server.Match.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class HitData
    {
        public static List<HitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool genLog, bool OnlyBytes = false)
        {
            List<HitDataInfo> list = new List<HitDataInfo>();
            int num = C.ReadC();
            for (int i = 0; i < num; i++)
            {
                HitDataInfo info1 = new HitDataInfo();
                info1.StartBullet = C.ReadTV();
                info1.EndBullet = C.ReadTV();
                info1.BulletPos = C.ReadTV();
                info1.BoomInfo = C.ReadUH();
                info1.ObjectId = C.ReadUH();
                info1.HitIndex = C.ReadUD();
                info1.WeaponId = C.ReadD();
                info1.Accessory = C.ReadC();
                info1.Extensions = C.ReadC();
                HitDataInfo item = info1;
                if (!OnlyBytes)
                {
                    item.HitEnum = (HitType) AllUtils.GetHitHelmet(item.HitIndex);
                    if (item.BoomInfo > 0)
                    {
                        item.BoomPlayers = new List<int>();
                        for (int j = 0; j < 0x12; j++)
                        {
                            int num4 = 1 << (j & 0x1f);
                            if ((item.BoomInfo & num4) == num4)
                            {
                                item.BoomPlayers.Add(j);
                            }
                        }
                    }
                    item.WeaponClass = (ClassType) ComDiv.GetIdStatics(item.WeaponId, 2);
                }
                if (genLog)
                {
                    CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {item.WeaponId}; Ext: {item.Extensions}; Acc: {item.Accessory}", LoggerType.Warning, null);
                    CLogger.Print($"PVP Slot: {Action.Slot}; Hit Data: {item.HitIndex} [Start]: X: {item.StartBullet.X}; Y: {item.StartBullet.Y}; Z: {item.StartBullet.Z}", LoggerType.Warning, null);
                    CLogger.Print($"PVP Slot: {Action.Slot}; Hit Data: {item.HitIndex} [Ended]: X: {item.EndBullet.X}; Y: {item.EndBullet.Y}; Z: {item.EndBullet.Z}", LoggerType.Warning, null);
                }
                list.Add(item);
            }
            return list;
        }

        public static void WriteInfo(SyncServerPacket S, List<HitDataInfo> Hits)
        {
            S.WriteC((byte) Hits.Count);
            foreach (HitDataInfo info in Hits)
            {
                S.WriteTV(info.StartBullet);
                S.WriteTV(info.EndBullet);
                S.WriteTV(info.BulletPos);
                S.WriteH(info.BoomInfo);
                S.WriteH(info.ObjectId);
                S.WriteD(info.HitIndex);
                S.WriteD(info.WeaponId);
                S.WriteC(info.Accessory);
                S.WriteC(info.Extensions);
            }
        }
    }
}

