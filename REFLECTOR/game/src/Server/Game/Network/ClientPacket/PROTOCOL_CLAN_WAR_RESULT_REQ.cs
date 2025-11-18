namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_RESULT_REQ : GameClientPacket
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
                base.Client.SendPacket(new PROTOCOL_CLAN_WAR_RESULT_ACK());
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CLAN_WAR_RESULT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

