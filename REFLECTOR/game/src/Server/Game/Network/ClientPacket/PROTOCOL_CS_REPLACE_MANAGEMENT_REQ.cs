namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_REPLACE_MANAGEMENT_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private JoinClanType joinClanType_0;
        private uint uint_0;

        public override void Read()
        {
            this.int_3 = base.ReadC();
            this.int_0 = base.ReadC();
            this.int_2 = base.ReadC();
            this.int_1 = base.ReadC();
            this.joinClanType_0 = (JoinClanType) base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                    if ((clan.Id <= 0) || (clan.OwnerId != player.PlayerId))
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        if (clan.Authority != this.int_3)
                        {
                            clan.Authority = this.int_3;
                        }
                        if (clan.RankLimit != this.int_0)
                        {
                            clan.RankLimit = this.int_0;
                        }
                        if (clan.MinAgeLimit != this.int_1)
                        {
                            clan.MinAgeLimit = this.int_1;
                        }
                        if (clan.MaxAgeLimit != this.int_2)
                        {
                            clan.MaxAgeLimit = this.int_2;
                        }
                        if (clan.JoinType != this.joinClanType_0)
                        {
                            clan.JoinType = this.joinClanType_0;
                        }
                        DaoManagerSQL.UpdateClanInfo(clan.Id, clan.Authority, clan.RankLimit, clan.MinAgeLimit, clan.MaxAgeLimit, (int) clan.JoinType);
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

