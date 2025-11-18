namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class DropWeapon
    {
        public static DropWeaponInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            DropWeaponInfo info1 = new DropWeaponInfo();
            info1.AmmoPrin = C.ReadUH();
            info1.AmmoDual = C.ReadUH();
            info1.AmmoTotal = C.ReadUH();
            info1.Unk1 = C.ReadUH();
            info1.Unk2 = C.ReadUH();
            info1.Unk3 = C.ReadUH();
            info1.Flags = C.ReadC();
            info1.WeaponId = C.ReadD();
            info1.Accessory = C.ReadC();
            info1.Extensions = C.ReadC();
            DropWeaponInfo info = info1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot : {Action.Slot}; Weapon Id: {info.WeaponId}; Ext: {info.Extensions}; Acc: {info.Accessory}; Ammo Prin: {info.AmmoPrin}; Ammo Dual: {info.AmmoDual}; Ammo Total: {info.AmmoTotal}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, DropWeaponInfo Info)
        {
            if (ConfigLoader.UseMaxAmmoInDrop)
            {
                S.WriteH((ushort) 0xffff);
                S.WriteH(Info.AmmoDual);
                S.WriteH((short) 0x2710);
            }
            else
            {
                S.WriteH(Info.AmmoPrin);
                S.WriteH(Info.AmmoDual);
                S.WriteH(Info.AmmoTotal);
            }
            S.WriteH(Info.Unk1);
            S.WriteH(Info.Unk2);
            S.WriteH(Info.Unk3);
            S.WriteC((byte) (Info.Flags + Info.Counter));
            S.WriteD(Info.WeaponId);
            S.WriteC(Info.Accessory);
            S.WriteC(Info.Extensions);
        }
    }
}

