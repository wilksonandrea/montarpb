namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_GM_LOG_LOBBY_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && player.IsGM())
                {
                    long playerId = player.GetChannel().GetPlayer(this.int_0).PlayerId;
                    if (playerId > 0L)
                    {
                        base.Client.SendPacket(new PROTOCOL_GM_LOG_LOBBY_ACK(0, playerId));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_GM_LOG_LOBBY_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

