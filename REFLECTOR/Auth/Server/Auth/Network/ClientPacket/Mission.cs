namespace Server.Auth.Network.ClientPacket
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mission
    {
        public int Id { get; set; }

        public int RewardId { get; set; }

        public int RewardCount { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ObjectivesData { get; set; }
    }
}

