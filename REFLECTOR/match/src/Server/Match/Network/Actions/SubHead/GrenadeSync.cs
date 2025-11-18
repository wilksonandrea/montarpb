namespace Server.Match.Network.Actions.SubHead
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Models.SubHead;
    using System;
    using System.Runtime.InteropServices;

    public class GrenadeSync
    {
        public static GrenadeInfo ReadInfo(SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
        {
            GrenadeInfo info1 = new GrenadeInfo();
            info1.Unk1 = C.ReadC();
            info1.Unk2 = C.ReadC();
            info1.Unk3 = C.ReadC();
            info1.Unk4 = C.ReadC();
            info1.BoomInfo = C.ReadUH();
            info1.WeaponId = C.ReadD();
            info1.Accessory = C.ReadC();
            info1.Extensions = C.ReadC();
            info1.ObjPosX = C.ReadUH();
            info1.ObjPosY = C.ReadUH();
            info1.ObjPosZ = C.ReadUH();
            info1.Unk5 = C.ReadD();
            info1.Unk6 = C.ReadD();
            info1.Unk7 = C.ReadD();
            GrenadeInfo info = info1;
            if (!OnlyBytes)
            {
                info.WeaponClass = (ClassType) ComDiv.GetIdStatics(info.WeaponId, 2);
            }
            if (GenLog)
            {
                CLogger.Print($"UDP_SUB_HEAD_GRENADE; Weapon Id: {info.WeaponId}; Acc Flag: {info.Accessory}; Ext: {info.Extensions}; Boom Info: {info.BoomInfo}; X: {info.ObjPosX}; Y: {info.ObjPosY}; Z: {info.ObjPosZ}", LoggerType.Warning, null);
            }
            return info;
        }

        public static void WriteInfo(SyncServerPacket S, GrenadeInfo Info)
        {
            S.WriteC(Info.Unk1);
            S.WriteC(Info.Unk2);
            S.WriteC(Info.Unk3);
            S.WriteC(Info.Unk4);
            S.WriteH(Info.BoomInfo);
            S.WriteD(Info.WeaponId);
            S.WriteC(Info.Accessory);
            S.WriteC(Info.Extensions);
            S.WriteH(Info.ObjPosX);
            S.WriteH(Info.ObjPosY);
            S.WriteH(Info.ObjPosZ);
            S.WriteD(Info.Unk5);
            S.WriteD(Info.Unk6);
            S.WriteD(Info.Unk7);
        }
    }
}

