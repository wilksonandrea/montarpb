namespace Server.Match.Network.Actions.Event
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using System;

    public class GetWeaponForClient
    {
        public static WeaponClient ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
        {
            WeaponClient client1 = new WeaponClient();
            client1.AmmoPrin = C.ReadUH();
            client1.AmmoDual = C.ReadUH();
            client1.AmmoTotal = C.ReadUH();
            client1.Unk1 = C.ReadUH();
            client1.Unk2 = C.ReadUH();
            client1.Unk3 = C.ReadUH();
            client1.Flags = C.ReadC();
            client1.WeaponId = C.ReadD();
            client1.Accessory = C.ReadC();
            client1.Extensions = C.ReadC();
            WeaponClient client = client1;
            if (GenLog)
            {
                CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {client.WeaponId}; Ext: {client.Extensions}; Acc: {client.Accessory}; Ammo Prin: {client.AmmoPrin}; Ammo Dual: {client.AmmoDual}; Ammo Total: {client.AmmoTotal}", LoggerType.Warning, null);
            }
            return client;
        }

        public static void WriteInfo(SyncServerPacket S, WeaponClient Info)
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
            S.WriteC(Info.Flags);
            S.WriteD(Info.WeaponId);
            S.WriteC(Info.Accessory);
            S.WriteC(Info.Extensions);
        }
    }
}

