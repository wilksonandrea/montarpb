namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;

    public class PROTOCOL_MATCH_CLAN_SEASON_REQ : AuthClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_MATCH_CLAN_SEASON_ACK(0));
                base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK());
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

