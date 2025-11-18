namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                CLogger.Print(base.GetType().Name + " CALLLED!", LoggerType.Warning, null);
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

