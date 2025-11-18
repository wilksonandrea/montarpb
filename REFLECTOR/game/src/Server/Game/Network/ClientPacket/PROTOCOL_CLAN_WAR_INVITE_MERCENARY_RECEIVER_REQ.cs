namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    MatchModel match = player.Match;
                    if ((match == null) || (player.MatchSlot != match.Leader))
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(0x80000000, 0));
                    }
                    else
                    {
                        match.Training = this.int_0;
                        using (PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK protocol_clan_war_invite_mercenary_receiver_ack = new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(0, this.int_0))
                        {
                            match.SendPacketToPlayers(protocol_clan_war_invite_mercenary_receiver_ack);
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

