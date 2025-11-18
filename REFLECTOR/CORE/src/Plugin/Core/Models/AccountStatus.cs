namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Runtime.CompilerServices;

    public class AccountStatus
    {
        public AccountStatus()
        {
            this.Buffer = new byte[4];
        }

        private void method_0()
        {
            uint num = BitConverter.ToUInt32(this.Buffer, 0);
            ComDiv.UpdateDB("accounts", "status", (long) num, "player_id", this.PlayerId);
        }

        public void ResetData(long PlayerId)
        {
            if (PlayerId != 0)
            {
                int roomId = this.RoomId;
                int clanMatchId = this.ClanMatchId;
                int serverId = this.ServerId;
                this.SetData(uint.MaxValue, PlayerId);
                if ((this.ChannelId != this.ChannelId) || ((roomId != this.RoomId) || ((clanMatchId != this.ClanMatchId) || (serverId != this.ServerId))))
                {
                    ComDiv.UpdateDB("accounts", "status", 0xffffffffL, "player_id", PlayerId);
                }
            }
        }

        public void SetData(uint Data, long PlayerId)
        {
            this.SetData(BitConverter.GetBytes(Data), PlayerId);
        }

        public void SetData(byte[] Buffer, long PlayerId)
        {
            this.PlayerId = PlayerId;
            this.Buffer = Buffer;
            this.ChannelId = Buffer[0];
            this.RoomId = Buffer[1];
            this.ServerId = Buffer[2];
            this.ClanMatchId = Buffer[3];
        }

        public void UpdateChannel(byte ChannelId)
        {
            this.ChannelId = ChannelId;
            this.Buffer[0] = ChannelId;
            this.method_0();
        }

        public void UpdateClanMatch(byte ClanMatchId)
        {
            this.ClanMatchId = ClanMatchId;
            this.Buffer[3] = ClanMatchId;
            this.method_0();
        }

        public void UpdateRoom(byte RoomId)
        {
            this.RoomId = RoomId;
            this.Buffer[1] = RoomId;
            this.method_0();
        }

        public void UpdateServer(byte ServerId)
        {
            this.ServerId = ServerId;
            this.Buffer[2] = ServerId;
            this.method_0();
        }

        public long PlayerId { get; set; }

        public byte ChannelId { get; set; }

        public byte RoomId { get; set; }

        public byte ClanMatchId { get; set; }

        public byte ServerId { get; set; }

        public byte[] Buffer { get; set; }
    }
}

