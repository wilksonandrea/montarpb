namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;
    using System.Runtime.InteropServices;

    public class WeaponSync
    {
        public static WeaponSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
        {
            WeaponSyncInfo info1 = new WeaponSyncInfo();
            info1.WeaponId = C.ReadD();
            info1.Accessory = C.ReadC();
            info1.Extensions = C.ReadC();
            WeaponSyncInfo info = info1;
            if (!OnlyBytes)
            {
                info.WeaponClass = (ClassType) ComDiv.GetIdStatics(info.WeaponId, 2);
            }
            if (GenLog)
            {
                CLogger.Print($"PVP Slot {Action.Slot}; Weapon Id: {info.WeaponId}; Extensions: {info.Extensions}; Unknowns: {info.Accessory}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, WeaponSyncInfo Info)
        {
            S.WriteD(Info.WeaponId);
            S.WriteC(Info.Accessory);
            S.WriteC(Info.Extensions);
        }
    }
}

