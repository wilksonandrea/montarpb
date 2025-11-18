namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ : GameClientPacket
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
                if (player != null)
                {
                    PlayerSession session = player.GetChannel().GetPlayer(this.int_0);
                    if (session != null)
                    {
                        Account account2 = AccountManager.GetAccount(session.PlayerId, true);
                        if (account2 != null)
                        {
                            if (player.Nickname != account2.Nickname)
                            {
                                player.FindPlayer = account2.Nickname;
                            }
                            base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0, account2, 0x7fffffff));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_USER_STATISTICS_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

