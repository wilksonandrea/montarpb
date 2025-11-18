namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ : GameClientPacket
    {
        private uint uint_0;
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
                    Account account2 = AccountManager.GetAccountDB(this.long_0, 2, 0x1f);
                    if ((account2 == null) || ((player.Nickname.Length <= 0) || (player.PlayerId == this.long_0)))
                    {
                        this.uint_0 = 0x80001803;
                    }
                    else
                    {
                        if (player.Nickname != account2.Nickname)
                        {
                            player.FindPlayer = account2.Nickname;
                        }
                        base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(this.uint_0, account2, 0x7fffffff));
                    }
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

