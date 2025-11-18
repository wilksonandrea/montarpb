namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Auth.Data.Managers;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.Sync.Update;
    using System;

    public static class ClanSync
    {
        public static void Load(SyncClientPacket C)
        {
            int num2 = C.ReadC();
            Account player = AccountManager.GetAccount(C.ReadQ(), true);
            if (player != null)
            {
                if (num2 == 0)
                {
                    ClanInfo.ClearList(player);
                }
                else if (num2 != 1)
                {
                    if (num2 == 2)
                    {
                        ClanInfo.RemoveMember(player, C.ReadQ());
                    }
                    else if (num2 == 3)
                    {
                        player.ClanId = C.ReadD();
                        player.ClanAccess = C.ReadC();
                    }
                }
                else
                {
                    long playerId = C.ReadQ();
                    string str = C.ReadS(C.ReadC());
                    byte[] buffer = C.ReadB(4);
                    byte num3 = C.ReadC();
                    Account account1 = new Account();
                    account1.PlayerId = playerId;
                    account1.Nickname = str;
                    account1.Rank = num3;
                    Account member = account1;
                    member.Status.SetData(buffer, playerId);
                    ClanInfo.AddMember(player, member);
                }
            }
        }
    }
}

