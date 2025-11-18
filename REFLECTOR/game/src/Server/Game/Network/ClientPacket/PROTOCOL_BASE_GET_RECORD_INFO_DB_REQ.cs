namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ : GameClientPacket
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
                    Account account2 = AccountManager.GetAccount(this.long_0, 0x1f);
                    if ((account2 != null) && (player.PlayerId != account2.PlayerId))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(account2));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

