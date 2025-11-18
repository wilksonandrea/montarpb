namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK : GameServerPacket
    {
        public readonly MatchModel match;
        public readonly Account Player;

        public PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(MatchModel matchModel_0, Account account_0)
        {
            this.match = matchModel_0;
            this.Player = account_0;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1b0f);
            base.WriteH((short) this.match.MatchId);
            base.WriteH((ushort) this.match.GetServerInfo());
            base.WriteH((ushort) this.match.GetServerInfo());
            base.WriteC((byte) this.match.State);
            base.WriteC((byte) this.match.FriendId);
            base.WriteC((byte) this.match.Training);
            base.WriteC((byte) this.match.GetCountPlayers());
            base.WriteD(this.match.Leader);
            base.WriteC(0);
            base.WriteD(this.match.Clan.Id);
            base.WriteC((byte) this.match.Clan.Rank);
            base.WriteD(this.match.Clan.Logo);
            base.WriteS(this.match.Clan.Name, 0x11);
            base.WriteT(this.match.Clan.Points);
            base.WriteC((byte) this.match.Clan.NameColor);
            if (this.Player == null)
            {
                base.WriteB(new byte[0x2b]);
            }
            else
            {
                base.WriteC((byte) this.Player.Rank);
                base.WriteS(this.Player.Nickname, 0x21);
                base.WriteQ(this.Player.PlayerId);
                base.WriteC((byte) this.match.Slots[this.match.Leader].State);
            }
        }
    }
}

