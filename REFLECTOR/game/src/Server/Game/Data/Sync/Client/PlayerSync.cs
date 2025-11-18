namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using System;

    public static class PlayerSync
    {
        public static void Load(SyncClientPacket C)
        {
            int num = C.ReadC();
            int num2 = C.ReadC();
            int num3 = C.ReadD();
            int num4 = C.ReadD();
            int num5 = C.ReadD();
            Account account = AccountManager.GetAccount(C.ReadQ(), true);
            if ((account != null) && (num == 0))
            {
                account.Rank = num2;
                account.Gold = num3;
                account.Cash = num4;
                account.Tags = num5;
            }
        }
    }
}

