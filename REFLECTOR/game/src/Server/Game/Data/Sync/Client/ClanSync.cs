namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using System;

    public static class ClanSync
    {
        public static void Load(SyncClientPacket C)
        {
            int num = C.ReadC();
            Account account = AccountManager.GetAccount(C.ReadQ(), true);
            if ((account != null) && (num == 3))
            {
                account.ClanId = C.ReadD();
                account.ClanAccess = C.ReadC();
            }
        }
    }
}

