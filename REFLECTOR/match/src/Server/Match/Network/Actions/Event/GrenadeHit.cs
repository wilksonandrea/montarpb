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

    public class GrenadeHit
    {
        public static List<GrenadeHitInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
        {
            List<GrenadeHitInfo> list = new List<GrenadeHitInfo>();
            int num = C.ReadC();
            for (int i = 0; i < num; i++)
            {
                GrenadeHitInfo info1 = new GrenadeHitInfo();
                info1.WeaponId = C.ReadD();
                info1.Accessory = C.ReadC();
                info1.Extensions = C.ReadC();
                info1.BoomInfo = C.ReadUH();
                info1.ObjectId = C.ReadUH();
                info1.HitInfo = C.ReadUD();
                info1.PlayerPos = C.ReadUHV();
                info1.FirePos = C.ReadUHV();
                info1.HitPos = C.ReadUHV();
                info1.GrenadesCount = C.ReadUH();
                info1.DeathType = (CharaDeath) C.ReadC();
                GrenadeHitInfo item = info1;
                if (!OnlyBytes)
                {
                    item.HitEnum = (HitType) AllUtils.GetHitHelmet(item.HitInfo);
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
                if (GenLog)
                {
                    CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {item.WeaponId}; Ext: {item.Extensions}; Acc: {item.Accessory}", LoggerType.Warning, null);
                    CLogger.Print($"PVP Slot: {Action.Slot}; Grenade Hit: {item.HitInfo}; [Object Postion] X: {item.HitPos.X}; Y: {item.HitPos.Y}; Z: {item.HitPos.Z}", LoggerType.Warning, null);
                    CLogger.Print($"PVP Slot: {Action.Slot}; Grenade Hit: {item.HitInfo}; [Player Postion] X: {item.FirePos.X}; Y: {item.FirePos.Y}; Z: {item.FirePos.Z}", LoggerType.Warning, null);
                }
                list.Add(item);
            }
            return list;
        }

        public static void WriteInfo(SyncServerPacket S, List<GrenadeHitInfo> Hits)
        {
            S.WriteC((byte) Hits.Count);
            foreach (GrenadeHitInfo info in Hits)
            {
                S.WriteD(info.WeaponId);
                S.WriteC(info.Accessory);
                S.WriteC(info.Extensions);
                S.WriteH(info.BoomInfo);
                S.WriteH(info.ObjectId);
                S.WriteD(info.HitInfo);
                S.WriteHV(info.PlayerPos);
                S.WriteHV(info.FirePos);
                S.WriteHV(info.HitPos);
                S.WriteH(info.GrenadesCount);
                S.WriteC((byte) info.DeathType);
            }
        }
    }
}

