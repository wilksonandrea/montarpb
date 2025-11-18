namespace Server.Match.Data.Models.SubHead
{
    using Plugin.Core.Enums;
    using System;

    public class GrenadeInfo
    {
        public byte Accessory;
        public byte Extensions;
        public byte Unk1;
        public byte Unk2;
        public byte Unk3;
        public byte Unk4;
        public int WeaponId;
        public int Unk5;
        public int Unk6;
        public int Unk7;
        public ClassType WeaponClass;
        public ushort ObjPosX;
        public ushort ObjPosY;
        public ushort ObjPosZ;
        public ushort BoomInfo;
        public ushort GrenadesCount;
    }
}

