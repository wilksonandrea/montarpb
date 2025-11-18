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

    public class Suicide
    {
        public static List<SuicideInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
        {
            List<SuicideInfo> list = new List<SuicideInfo>();
            int num = C.ReadC();
            for (int i = 0; i < num; i++)
            {
                SuicideInfo info1 = new SuicideInfo();
                info1.PlayerPos = C.ReadUHV();
                info1.WeaponId = C.ReadD();
                info1.Accessory = C.ReadC();
                info1.Extensions = C.ReadC();
                info1.HitInfo = C.ReadUD();
                SuicideInfo item = info1;
                if (!OnlyBytes)
                {
                    item.WeaponClass = (ClassType) ComDiv.GetIdStatics(item.WeaponId, 2);
                }
                if (GenLog)
                {
                    CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {item.WeaponId}; Ext: {item.Extensions}; Acc: {item.Accessory}; Suicide Hit: {item.HitInfo}; X: {item.PlayerPos.X}; Y: {item.PlayerPos.Y}; Z: {item.PlayerPos.Z}", LoggerType.Warning, null);
                }
                list.Add(item);
            }
            return list;
        }

        public static void WriteInfo(SyncServerPacket S, List<SuicideInfo> Hits)
        {
            S.WriteC((byte) Hits.Count);
            foreach (SuicideInfo info in Hits)
            {
                S.WriteHV(info.PlayerPos);
                S.WriteD(info.WeaponId);
                S.WriteC(info.Accessory);
                S.WriteC(info.Extensions);
                S.WriteD(info.HitInfo);
            }
        }
    }
}

