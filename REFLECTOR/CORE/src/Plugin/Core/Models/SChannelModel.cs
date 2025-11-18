namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class SChannelModel
    {
        public SChannelModel(string string_1, ushort ushort_1)
        {
            this.Host = string_1;
            this.Port = ushort_1;
        }

        public int Id { get; set; }

        public int LastPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int ChannelPlayers { get; set; }

        public bool State { get; set; }

        public SChannelType Type { get; set; }

        public string Host { get; set; }

        public ushort Port { get; set; }

        public bool IsMobile { get; set; }
    }
}

