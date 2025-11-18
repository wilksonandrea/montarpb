namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class FireData
    {
        public static FireDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            FireDataInfo info1 = new FireDataInfo();
            info1.Effect = C.ReadC();
            info1.Part = C.ReadC();
            info1.Index = C.ReadH();
            info1.WeaponId = C.ReadD();
            info1.Accessory = C.ReadC();
            info1.Extensions = C.ReadC();
            info1.X = C.ReadUH();
            info1.Y = C.ReadUH();
            info1.Z = C.ReadUH();
            FireDataInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {info.WeaponId}; Extensions: {info.Extensions}; Fire: {info.Effect}; Part: {info.Part}; X: {info.X}; Y: {info.Y}; Z: {info.Z}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, FireDataInfo Info)
        {
            S.WriteC(Info.Effect);
            S.WriteC(Info.Part);
            S.WriteH(Info.Index);
            S.WriteD(Info.WeaponId);
            S.WriteC(Info.Accessory);
            S.WriteC(Info.Extensions);
            S.WriteH(Info.X);
            S.WriteH(Info.Y);
            S.WriteH(Info.Z);
        }
    }
}

