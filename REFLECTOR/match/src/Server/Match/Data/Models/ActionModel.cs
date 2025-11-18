namespace Server.Match.Data.Models
{
    using Server.Match.Data.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class ActionModel
    {
        public ushort Slot { get; set; }

        public ushort Length { get; set; }

        public UdpGameEvent Flag { get; set; }

        public UdpSubHead SubHead { get; set; }

        public byte[] Data { get; set; }
    }
}

