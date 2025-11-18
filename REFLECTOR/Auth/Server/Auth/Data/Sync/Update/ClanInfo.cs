namespace Server.Auth.Data.Sync.Update
{
    using Server.Auth.Data.Models;
    using System;
    using System.Collections.Generic;

    public class ClanInfo
    {
        public static void AddMember(Account Player, Account Member)
        {
            List<Account> clanPlayers = Player.ClanPlayers;
            lock (clanPlayers)
            {
                Player.ClanPlayers.Add(Member);
            }
        }

        public static void ClearList(Account Player)
        {
            List<Account> clanPlayers = Player.ClanPlayers;
            lock (clanPlayers)
            {
                Player.ClanId = 0;
                Player.ClanAccess = 0;
                Player.ClanPlayers.Clear();
            }
        }

        public static void RemoveMember(Account Player, long PlayerId)
        {
            List<Account> clanPlayers = Player.ClanPlayers;
            lock (clanPlayers)
            {
                for (int i = 0; i < Player.ClanPlayers.Count; i++)
                {
                    if (Player.ClanPlayers[i].PlayerId == PlayerId)
                    {
                        Player.ClanPlayers.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}

