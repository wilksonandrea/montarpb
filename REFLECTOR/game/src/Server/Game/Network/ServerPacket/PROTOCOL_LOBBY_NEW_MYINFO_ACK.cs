namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_LOBBY_NEW_MYINFO_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly ClanModel clanModel_0;
        private readonly StatisticClan statisticClan_0;

        public PROTOCOL_LOBBY_NEW_MYINFO_ACK(Account account_1)
        {
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
                this.statisticClan_0 = account_1.Statistic.Clan;
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x3d1);
            base.WriteD(0);
            base.WriteQ(this.account_0.PlayerId);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0x2775);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0x4eea);
            base.WriteD(0);
            base.WriteD(0x765f);
            base.WriteD(0);
            base.WriteD(0x9dd4);
            base.WriteD(0xc549);
            base.WriteD(0xecbe);
            base.WriteD(0);
            base.WriteD(0x11433);
            base.WriteD(0x13ba8);
            base.WriteD(0x1631d);
            base.WriteD(0x18a92);
            base.WriteD(0x1b207);
            base.WriteD(0x1d97c);
            base.WriteD(0x200f1);
            base.WriteD(0x22866);
            base.WriteD(0x24fdb);
            base.WriteD(0x27750);
            base.WriteD(0x29ec5);
            base.WriteD(0x2c63a);
            base.WriteD(0x2edaf);
            base.WriteD(0);
        }
    }
}

