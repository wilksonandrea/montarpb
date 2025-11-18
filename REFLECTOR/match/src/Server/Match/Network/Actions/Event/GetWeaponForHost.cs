namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class GetWeaponForHost
    {
        public static WeaponHost ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            WeaponHost host1 = new WeaponHost();
            host1.DeathType = C.ReadC();
            host1.X = C.ReadUH();
            host1.Y = C.ReadUH();
            host1.Z = C.ReadUH();
            host1.Unks = C.ReadC();
            host1.UNK = C.ReadD();
            host1.HitPart = C.ReadC();
            WeaponHost host = host1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; UNK: {host.UNK}; Death Type: {host.DeathType}; Hit: {host.HitPart}; X: {host.X}; Y: {host.Y}; Z: {host.Z}", LoggerType.Warning, null);
            }
            return host;
        }

        public static void WriteInfo(SyncServerPacket S, WeaponHost Info)
        {
            S.WriteC(Info.DeathType);
            S.WriteH(Info.X);
            S.WriteH(Info.Y);
            S.WriteH(Info.Z);
            S.WriteC(Info.Unks);
            S.WriteD(Info.UNK);
            S.WriteC(Info.HitPart);
        }
    }
}

