namespace Server.Match.Data.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PacketModel
    {
        public int Opcode { get; set; }

        public int Slot { get; set; }

        public int Round { get; set; }

        public int Length { get; set; }

        public int Respawn { get; set; }

        public int RoundNumber { get; set; }

        public int AccountId { get; set; }

        public int Unk1 { get; set; }

        public int Unk2 { get; set; }

        public float Time { get; set; }

        public byte[] Data { get; set; }

        public byte[] WithEndData { get; set; }

        public byte[] WithoutEndData { get; set; }

        public DateTime ReceiveDate { get; set; }
    }
}

