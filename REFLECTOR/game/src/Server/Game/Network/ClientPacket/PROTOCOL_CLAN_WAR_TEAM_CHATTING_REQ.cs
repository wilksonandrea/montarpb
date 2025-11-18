namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ : GameClientPacket
    {
        private ChattingType chattingType_0;
        private string string_0;

        public override void Read()
        {
            this.chattingType_0 = (ChattingType) base.ReadH();
            this.string_0 = base.ReadS(base.ReadH());
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (((player != null) && (player.Match != null)) && (this.chattingType_0 == ChattingType.Match))
                {
                    MatchModel match = player.Match;
                    using (PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK protocol_clan_war_team_chatting_ack = new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(player.Nickname, this.string_0))
                    {
                        match.SendPacketToPlayers(protocol_clan_war_team_chatting_ack);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

