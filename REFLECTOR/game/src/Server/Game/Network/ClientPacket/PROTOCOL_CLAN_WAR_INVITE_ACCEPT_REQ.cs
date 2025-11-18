namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_INVITE_ACCEPT_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private uint uint_0;

        public override void Read()
        {
            base.ReadD();
            this.int_0 = base.ReadH();
            this.int_1 = base.ReadH();
            this.int_2 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    MatchModel match = player.Match;
                    int num = this.int_1 - ((this.int_1 / 10) * 10);
                    MatchModel model2 = ChannelsXML.GetChannel(this.int_1, num).GetMatch(this.int_0);
                    if ((match == null) || ((model2 == null) || (player.MatchSlot != match.Leader)))
                    {
                        this.uint_0 = 0x80001094;
                    }
                    else if (this.int_2 != 1)
                    {
                        Account leader = model2.GetLeader();
                        if ((leader != null) && (leader.Match != null))
                        {
                            leader.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_ACCEPT_ACK(0x80001093));
                        }
                    }
                    else if (match.Training != model2.Training)
                    {
                        this.uint_0 = 0x80001092;
                    }
                    else if ((model2.GetCountPlayers() != match.Training) || (match.GetCountPlayers() != match.Training))
                    {
                        this.uint_0 = 0x80001091;
                    }
                    else if ((model2.State == MatchState.Play) || (match.State == MatchState.Play))
                    {
                        this.uint_0 = 0x80001090;
                    }
                    else
                    {
                        match.State = MatchState.Play;
                        Account leader = model2.GetLeader();
                        if ((leader != null) && (leader.Match != null))
                        {
                            leader.SendPacket(new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match));
                            leader.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(match));
                            model2.Slots[leader.MatchSlot].State = SlotMatchState.Ready;
                        }
                        model2.State = MatchState.Play;
                    }
                    base.Client.SendPacket(new PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("CLAN_WAR_ACCEPT_BATTLE_REC: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

