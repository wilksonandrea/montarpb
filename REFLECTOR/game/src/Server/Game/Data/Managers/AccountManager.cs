namespace Server.Game.Data.Managers
{
    using Npgsql;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class AccountManager
    {
        public static SortedList<long, Account> Accounts = new SortedList<long, Account>();

        public static void AddAccount(Account acc)
        {
            SortedList<long, Account> accounts = Accounts;
            lock (accounts)
            {
                if (!Accounts.ContainsKey(acc.PlayerId))
                {
                    Accounts.Add(acc.PlayerId, acc);
                }
            }
        }

        public static Account GetAccount(long id, bool noUseDB)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                Account account;
                return (Accounts.TryGetValue(id, out account) ? account : (noUseDB ? null : GetAccountDB(id, 2, 0x1d1f)));
            }
            catch
            {
                return null;
            }
        }

        public static Account GetAccount(long id, int searchFlag)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                Account account;
                return (Accounts.TryGetValue(id, out account) ? account : GetAccountDB(id, 2, searchFlag));
            }
            catch
            {
                return null;
            }
        }

        public static Account GetAccount(string text, int type, int searchFlag)
        {
            Account account2;
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            using (IEnumerator<Account> enumerator = Accounts.Values.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Account current = enumerator.Current;
                        if (current == null)
                        {
                            continue;
                        }
                        if (((type != 1) || ((current.Nickname != text) || (current.Nickname.Length <= 0))) && ((type != 0) || (string.Compare(current.Username, text) != 0)))
                        {
                            continue;
                        }
                        account2 = current;
                    }
                    else
                    {
                        return GetAccountDB(text, type, searchFlag);
                    }
                    break;
                }
            }
            return account2;
        }

        public static Account GetAccountDB(object valor, int type, int searchDBFlag)
        {
            Account account;
            if (((type != 2) || (((long) valor) != 0)) && (((type != 0) && (type != 1)) || (((string) valor) != "")))
            {
                account = null;
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@value", valor);
                        command.CommandText = "SELECT * FROM accounts WHERE " + ((type == 0) ? "username" : ((type == 1) ? "nickname" : "player_id")) + "=@value";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            Account account1 = new Account();
                            account1.Username = reader["username"].ToString();
                            account1.Password = reader["password"].ToString();
                            account = account1;
                            account.SetPlayerId(long.Parse(reader["player_id"].ToString()), searchDBFlag);
                            account.Email = reader["email"].ToString();
                            account.Age = int.Parse(reader["age"].ToString());
                            account.SetPublicIP(reader.GetString(5));
                            account.Nickname = reader["nickname"].ToString();
                            account.NickColor = int.Parse(reader["nick_color"].ToString());
                            account.Rank = int.Parse(reader["rank"].ToString());
                            account.Exp = int.Parse(reader["experience"].ToString());
                            account.Gold = int.Parse(reader["gold"].ToString());
                            account.Cash = int.Parse(reader["cash"].ToString());
                            account.CafePC = (CafeEnum) int.Parse(reader["pc_cafe"].ToString());
                            account.Access = (AccessLevel) int.Parse(reader["access_level"].ToString());
                            account.IsOnline = bool.Parse(reader["online"].ToString());
                            account.ClanId = int.Parse(reader["clan_id"].ToString());
                            account.ClanAccess = int.Parse(reader["clan_access"].ToString());
                            account.Effects = (CouponEffects) long.Parse(reader["coupon_effect"].ToString());
                            account.Status.SetData(uint.Parse(reader["status"].ToString()), account.PlayerId);
                            account.LastRankUpDate = uint.Parse(reader["last_rank_update"].ToString());
                            account.BanObjectId = long.Parse(reader["ban_object_id"].ToString());
                            account.Ribbon = int.Parse(reader["ribbon"].ToString());
                            account.Ensign = int.Parse(reader["ensign"].ToString());
                            account.Medal = int.Parse(reader["medal"].ToString());
                            account.MasterMedal = int.Parse(reader["master_medal"].ToString());
                            account.Mission.Mission1 = int.Parse(reader["mission_id1"].ToString());
                            account.Mission.Mission2 = int.Parse(reader["mission_id2"].ToString());
                            account.Mission.Mission3 = int.Parse(reader["mission_id3"].ToString());
                            account.Tags = int.Parse(reader["tags"].ToString());
                            account.InventoryPlus = int.Parse(reader["inventory_plus"].ToString());
                            account.Country = (CountryFlags) int.Parse(reader["country_flag"].ToString());
                            AddAccount(account);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print("was a problem loading accounts! " + exception.Message, LoggerType.Error, exception);
                }
            }
            else
            {
                return null;
            }
            return account;
        }

        public static void GetFriendlyAccounts(PlayerFriends System)
        {
            if ((System != null) && (System.Friends.Count != 0))
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        string str = "";
                        List<string> list = new List<string>();
                        int num = 0;
                        while (true)
                        {
                            if (num >= System.Friends.Count)
                            {
                                str = string.Join(",", list.ToArray());
                                command.CommandText = "SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (" + str + ") ORDER BY player_id";
                                NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                                while (true)
                                {
                                    if (!reader.Read())
                                    {
                                        command.Dispose();
                                        reader.Dispose();
                                        reader.Close();
                                        connection.Dispose();
                                        connection.Close();
                                        break;
                                    }
                                    FriendModel friend = System.GetFriend(long.Parse(reader["player_id"].ToString()));
                                    if (friend != null)
                                    {
                                        friend.Info.Nickname = reader["nickname"].ToString();
                                        friend.Info.Rank = int.Parse(reader["rank"].ToString());
                                        friend.Info.IsOnline = bool.Parse(reader["online"].ToString());
                                        friend.Info.Status.SetData(uint.Parse(reader["status"].ToString()), friend.PlayerId);
                                    }
                                }
                                break;
                            }
                            FriendModel model = System.Friends[num];
                            string parameterName = "@valor" + num.ToString();
                            command.Parameters.AddWithValue(parameterName, model.PlayerId);
                            list.Add(parameterName);
                            num++;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print("was a problem loading (FriendlyAccounts); " + exception.Message, LoggerType.Error, exception);
                }
            }
        }

        public static bool UpdatePlayerName(string name, long playerId) => 
            ComDiv.UpdateDB("accounts", "nickname", name, "player_id", playerId);
    }
}

