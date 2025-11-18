namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class WeaponRecoil
    {
        public static WeaponRecoilInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            WeaponRecoilInfo info1 = new WeaponRecoilInfo();
            info1.RecoilHorzAngle = C.ReadT();
            info1.RecoilHorzMax = C.ReadT();
            info1.RecoilVertAngle = C.ReadT();
            info1.RecoilVertMax = C.ReadT();
            info1.Deviation = C.ReadT();
            info1.RecoilHorzCount = C.ReadC();
            info1.WeaponId = C.ReadD();
            info1.Accessory = C.ReadC();
            info1.Extensions = C.ReadC();
            WeaponRecoilInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; WeaponId: {info.WeaponId}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, WeaponRecoilInfo Info)
        {
            S.WriteT(Info.RecoilHorzAngle);
            S.WriteT(Info.RecoilHorzMax);
            S.WriteT(Info.RecoilVertAngle);
            S.WriteT(Info.RecoilVertMax);
            S.WriteT(Info.Deviation);
            S.WriteC(Info.RecoilHorzCount);
            S.WriteD(Info.WeaponId);
            S.WriteC(Info.Accessory);
            S.WriteC(Info.Extensions);
        }
    }
}

