namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (player.Match != null))
                {
                    ChannelModel channel = player.GetChannel();
                    if ((channel != null) && (channel.Type == ChannelType.Clan))
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(channel.Matches, player.Match.MatchId));
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

