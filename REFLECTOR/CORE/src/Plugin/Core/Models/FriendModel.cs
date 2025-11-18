namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class FriendModel
    {
        public FriendModel(long long_3)
        {
            this.PlayerId = long_3;
            this.Info = new PlayerInfo(long_3);
        }

        public FriendModel(long long_3, int int_1, int int_2, string string_0, bool bool_1, AccountStatus accountStatus_0)
        {
            this.PlayerId = long_3;
            this.SetModel(long_3, int_1, int_2, string_0, bool_1, accountStatus_0);
        }

        public void SetModel(long PlayerId, int Rank, int NickColor, string Nickname, bool IsOnline, AccountStatus Status)
        {
            this.Info = new PlayerInfo(PlayerId, Rank, NickColor, Nickname, IsOnline, Status);
        }

        public long ObjectId { get; set; }

        public long OwnerId { get; set; }

        public long PlayerId { get; set; }

        public int State { get; set; }

        public bool Removed { get; set; }

        public PlayerInfo Info { get; set; }
    }
}

