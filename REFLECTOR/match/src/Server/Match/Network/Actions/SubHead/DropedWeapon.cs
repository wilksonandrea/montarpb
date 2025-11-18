namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models.SubHead;
    using System;

    public class DropedWeapon
    {
        public static DropedWeaponInfo ReadInfo(SyncClientPacket C, bool GenLog)
        {
            DropedWeaponInfo info1 = new DropedWeaponInfo();
            info1.WeaponPos = C.ReadUHV();
            info1.Unk1 = C.ReadUH();
            info1.Unk2 = C.ReadUH();
            info1.Unk3 = C.ReadUH();
            info1.Unk4 = C.ReadUH();
            info1.WeaponFlag = C.ReadC();
            info1.Unks = C.ReadB(0x10);
            DropedWeaponInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"Sub Head: DroppedWeapon; Weapon Flag: {info.WeaponFlag}; X: {info.WeaponPos.X}; Y: {info.WeaponPos.Y}; Z: {info.WeaponPos.Z}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, DropedWeaponInfo Info)
        {
            S.WriteHV(Info.WeaponPos);
            S.WriteH(Info.Unk1);
            S.WriteH(Info.Unk2);
            S.WriteH(Info.Unk3);
            S.WriteH(Info.Unk4);
            S.WriteC(Info.WeaponFlag);
            S.WriteB(Info.Unks);
        }
    }
}

