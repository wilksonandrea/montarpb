namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_ROOM_INVITED_REQ : GameClientPacket
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
                if ((player != null) && (player.ClanId != 0))
                {
                    Account account2 = AccountManager.GetAccount(this.long_0, 0x1f);
                    if ((account2 != null) && (account2.ClanId == player.ClanId))
                    {
                        account2.SendPacket(new PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(base.Client.PlayerId), false);
                    }
                    player.SendPacket(new PROTOCOL_CS_ROOM_INVITED_ACK(0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

