namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SQL;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(0x42);
        }

        public override void Run()
        {
            try
            {
                base.Client.SendPacket(new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK(!DaoManagerSQL.IsPlayerNameExist(this.string_0) ? 0 : 0x80000113));
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

