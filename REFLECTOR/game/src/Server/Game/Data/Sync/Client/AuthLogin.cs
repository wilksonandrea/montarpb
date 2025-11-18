namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;

    public class AuthLogin
    {
        public static void Load(SyncClientPacket C)
        {
            Account account = AccountManager.GetAccount(C.ReadQ(), true);
            if (account != null)
            {
                account.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
                account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(0x80001000));
                account.Close(0x3e8, false);
            }
        }
    }
}

