namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_FIND_USER_REQ : GameClientPacket
    {
        private string string_0;
        private uint uint_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(0x22);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    Account account2 = AccountManager.GetAccount(this.string_0, 1, 0x11e);
                    if ((account2 == null) || ((player.Nickname.Length <= 0) || (player.Nickname == this.string_0)))
                    {
                        this.uint_0 = 0x80001803;
                    }
                    else if (player.Nickname != account2.Nickname)
                    {
                        player.FindPlayer = account2.Nickname;
                    }
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(this.uint_0, account2, 0x7fffffff));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

