namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private uint uint_0;

        private void method_0(Account account_0, MatchModel matchModel_0)
        {
            if (!matchModel_0.AddPlayer(account_0))
            {
                this.uint_0 = 0x80000000;
            }
            base.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(this.uint_0, matchModel_0));
            if (this.uint_0 == 0)
            {
                using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK protocol_clan_war_regist_mercenary_ack = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel_0))
                {
                    matchModel_0.SendPacketToPlayers(protocol_clan_war_regist_mercenary_ack);
                }
            }
        }

        public override void Read()
        {
            this.int_0 = base.ReadH();
            this.int_1 = base.ReadH();
            this.int_2 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((this.int_2 >= 2) || ((player == null) || ((player.Match != null) || (player.Room != null))))
                {
                    base.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(0x80000000, null));
                }
                else
                {
                    int num = this.int_1 - ((this.int_1 / 10) * 10);
                    ChannelModel channel = ChannelsXML.GetChannel(this.int_1, (this.int_2 == 0) ? num : player.ChannelId);
                    if (channel == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(0x80000000, null));
                    }
                    else if (player.ClanId == 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(0x8000105b, null));
                    }
                    else
                    {
                        MatchModel model2 = (this.int_2 == 1) ? channel.GetMatch(this.int_0, player.ClanId) : channel.GetMatch(this.int_0);
                        if (model2 != null)
                        {
                            this.method_0(player, model2);
                        }
                        else
                        {
                            base.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(0x80000000, null));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

