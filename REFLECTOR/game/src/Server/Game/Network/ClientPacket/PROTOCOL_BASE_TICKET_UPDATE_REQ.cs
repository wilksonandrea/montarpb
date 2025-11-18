namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_TICKET_UPDATE_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_BASE_TICKET_UPDATE_ACK());
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

