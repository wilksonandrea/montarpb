namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_REQUEST_INFO_REQ : GameClientPacket
    {
        private long long_0;

        public override void Read()
        {
            this.long_0 = base.ReadQ();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    base.Client.SendPacket(new PROTOCOL_CS_REQUEST_INFO_ACK(this.long_0, DaoManagerSQL.GetRequestClanInviteText(player.ClanId, this.long_0)));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

