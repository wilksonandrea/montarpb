namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CREATE_CLAN_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly ClanModel clanModel_0;
        private readonly uint uint_0;

        public PROTOCOL_CS_CREATE_CLAN_ACK(uint uint_1, ClanModel clanModel_1, Account account_1)
        {
            this.uint_0 = uint_1;
            this.clanModel_0 = clanModel_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x327);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD(this.clanModel_0.Id);
                base.WriteU(this.clanModel_0.Name, 0x22);
                base.WriteC((byte) this.clanModel_0.Rank);
                base.WriteC((byte) DaoManagerSQL.GetClanPlayers(this.clanModel_0.Id));
                base.WriteC((byte) this.clanModel_0.MaxPlayers);
                base.WriteD(this.clanModel_0.CreationDate);
                base.WriteD(this.clanModel_0.Logo);
                base.WriteB(new byte[11]);
                base.WriteQ(this.clanModel_0.OwnerId);
                base.WriteS(this.account_0.Nickname, 0x42);
                base.WriteC((byte) this.account_0.NickColor);
                base.WriteC((byte) this.account_0.Rank);
                base.WriteU(this.clanModel_0.Info, 510);
                base.WriteU("Temp", 0x2a);
                base.WriteC((byte) this.clanModel_0.RankLimit);
                base.WriteC((byte) this.clanModel_0.MinAgeLimit);
                base.WriteC((byte) this.clanModel_0.MaxAgeLimit);
                base.WriteC((byte) this.clanModel_0.Authority);
                base.WriteU("", 510);
                base.WriteB(new byte[0x2c]);
                base.WriteF((double) this.clanModel_0.Points);
                base.WriteF(60.0);
                base.WriteB(new byte[0x10]);
                base.WriteF((double) this.clanModel_0.Points);
                base.WriteF(60.0);
                base.WriteB(new byte[80]);
                base.WriteB(new byte[0x42]);
                base.WriteD(this.account_0.Gold);
            }
        }
    }
}

