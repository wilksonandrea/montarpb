namespace Server.Match.Data.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.SharpDX;
    using Server.Match.Data.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class ObjectHitInfo
    {
        public ObjectHitInfo(int int_9)
        {
            this.Type = int_9;
            this.DeathType = CharaDeath.DEFAULT;
        }

        public int Type { get; set; }

        public int ObjSyncId { get; set; }

        public int ObjId { get; set; }

        public int ObjLife { get; set; }

        public int KillerSlot { get; set; }

        public int AnimId1 { get; set; }

        public int AnimId2 { get; set; }

        public int DestroyState { get; set; }

        public int WeaponId { get; set; }

        public byte Accessory { get; set; }

        public byte Extensions { get; set; }

        public float SpecialUse { get; set; }

        public Half3 Position { get; set; }

        public ClassType WeaponClass { get; set; }

        public CharaHitPart HitPart { get; set; }

        public CharaDeath DeathType { get; set; }
    }
}

