namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK());
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

