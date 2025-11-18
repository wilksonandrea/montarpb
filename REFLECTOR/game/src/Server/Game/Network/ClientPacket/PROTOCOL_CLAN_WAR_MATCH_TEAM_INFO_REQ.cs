namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;

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
                if ((player != null) && (player.Match != null))
                {
                    int num = this.int_1 - ((this.int_1 / 10) * 10);
                    ChannelModel channel = ChannelsXML.GetChannel(this.int_1, num);
                    if (channel == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(0x80000000));
                    }
                    else
                    {
                        MatchModel match = channel.GetMatch(this.int_0);
                        if (match != null)
                        {
                            base.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(0, match.Clan));
                        }
                        else
                        {
                            base.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(0x80000000));
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

