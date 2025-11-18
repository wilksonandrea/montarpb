namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class FragModel
    {
        public byte WeaponClass { get; set; }

        public byte HitspotInfo { get; set; }

        public byte Unk { get; set; }

        public KillingMessage KillFlag { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public byte VictimSlot { get; set; }

        public byte AssistSlot { get; set; }

        public byte[] Unks { get; set; }
    }
}

