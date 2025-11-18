namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private uint uint_0;

        public override void Read()
        {
            this.int_0 = base.ReadH();
            this.int_1 = base.ReadH();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if ((player.Match == null) || ((player.MatchSlot != player.Match.Leader) || (player.Match.State != MatchState.Ready)))
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        int num = this.int_1 - ((this.int_1 / 10) * 10);
                        MatchModel match = ChannelsXML.GetChannel(this.int_1, num).GetMatch(this.int_0);
                        if (match == null)
                        {
                            this.uint_0 = 0x80000000;
                        }
                        else
                        {
                            Account leader = match.GetLeader();
                            if ((leader != null) && ((leader.Connection != null) && leader.IsOnline))
                            {
                                leader.SendPacket(new PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(player.Match, player));
                            }
                            else
                            {
                                this.uint_0 = 0x80000000;
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_PROPOSE_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

