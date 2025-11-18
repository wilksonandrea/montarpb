namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SQL;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CANCEL_REQUEST_REQ : GameClientPacket
    {
        private uint uint_0;

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                if ((base.Client.Player == null) || !DaoManagerSQL.DeleteClanInviteDB(base.Client.PlayerId))
                {
                    this.uint_0 = 0x8000105b;
                }
                base.Client.SendPacket(new PROTOCOL_CS_CANCEL_REQUEST_ACK(this.uint_0));
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

