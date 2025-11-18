namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using System;

    public class ClanServersSync
    {
        public static void Load(SyncClientPacket C)
        {
            int clanId = C.ReadD();
            ClanModel clan = ClanManager.GetClan(clanId);
            if (C.ReadC() != 0)
            {
                if (clan != null)
                {
                    ClanManager.RemoveClan(clan);
                }
            }
            else if (clan == null)
            {
                long num2 = C.ReadQ();
                uint num3 = C.ReadUD();
                string str = C.ReadS(C.ReadC());
                string str2 = C.ReadS(C.ReadC());
                ClanModel model1 = new ClanModel();
                model1.Id = clanId;
                model1.Name = str;
                model1.OwnerId = num2;
                model1.Logo = 0;
                model1.Info = str2;
                model1.CreationDate = num3;
                ClanManager.AddClan(model1);
            }
        }
    }
}

