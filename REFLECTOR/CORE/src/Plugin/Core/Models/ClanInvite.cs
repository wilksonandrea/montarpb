namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ClanInvite
    {
        public int Id { get; set; }

        public uint InviteDate { get; set; }

        public long PlayerId { get; set; }

        public string Text { get; set; }
    }
}

