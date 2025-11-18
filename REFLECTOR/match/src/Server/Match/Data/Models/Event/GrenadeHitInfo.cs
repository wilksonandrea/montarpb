namespace Server.Match.Data.Models.Event
{
    using Plugin.Core.Enums;
    using Plugin.Core.SharpDX;
    using Server.Match.Data.Enums;
    using System;
    using System.Collections.Generic;

    public class GrenadeHitInfo
    {
        public byte Extensions;
        public byte Accessory;
        public ushort BoomInfo;
        public ushort GrenadesCount;
        public ushort ObjectId;
        public uint HitInfo;
        public int WeaponId;
        public List<int> BoomPlayers;
        public CharaDeath DeathType;
        public Half3 FirePos;
        public Half3 HitPos;
        public Half3 PlayerPos;
        public HitType HitEnum;
        public ClassType WeaponClass;
    }
}

