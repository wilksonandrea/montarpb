namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK());
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

