namespace Server.Auth.Data.Managers
{
    using Npgsql;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Auth.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;

    public class AccountManager
    {
        public static SortedList<long, Account> Accounts = new SortedList<long, Account>();

        public static bool AddAccount(Account acc)
        {
            SortedList<long, Account> accounts = Accounts;
            lock (accounts)
            {
                if (!Accounts.ContainsKey(acc.PlayerId))
                {
                    Accounts.Add(acc.PlayerId, acc);
                    return true;
                }
            }
            return false;
        }

        public static bool CreateAccount(out Account Player, string Username, string Password)
        {
            bool flag;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@login", Username);
                    command.Parameters.AddWithValue("@pass", Password);
                    command.CommandText = "INSERT INTO accounts (username, password, country_flag) VALUES (@login, @pass, 0)";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT * FROM accounts WHERE username=@login";
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    Account acc = new Account();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            Player = acc;
                            AddAccount(acc);
                            command.Dispose();
                            connection.Dispose();
                            connection.Close();
                            flag = true;
                            break;
                        }
                        acc.Username = reader["username"].ToString();
                        acc.Password = reader["password"].ToString();
                        acc.SetPlayerId(long.Parse(reader["player_id"].ToString()), 0x5f);
                        acc.Email = reader["email"].ToString();
                        acc.Age = int.Parse(reader["age"].ToString());
                        acc.MacAddress = (PhysicalAddress) reader.GetValue(6);
                        acc.Nickname = reader["nickname"].ToString();
                        acc.NickColor = int.Parse(reader["nick_color"].ToString());
                        acc.Rank = int.Parse(reader["rank"].ToString());
                        acc.Exp = int.Parse(reader["experience"].ToString());
                        acc.Gold = int.Parse(reader["gold"].ToString());
                        acc.Cash = int.Parse(reader["cash"].ToString());
                        acc.CafePC = (CafeEnum) int.Parse(reader["pc_cafe"].ToString());
                        acc.Access = (AccessLevel) int.Parse(reader["access_level"].ToString());
                        acc.IsOnline = bool.Parse(reader["online"].ToString());
                        acc.ClanId = int.Parse(reader["clan_id"].ToString());
                        acc.ClanAccess = int.Parse(reader["clan_access"].ToString());
                        acc.Effects = (CouponEffects) long.Parse(reader["coupon_effect"].ToString());
                        acc.Status.SetData(uint.Parse(reader["status"].ToString()), acc.PlayerId);
                        acc.LastRankUpDate = uint.Parse(reader["last_rank_update"].ToString());
                        acc.BanObjectId = long.Parse(reader["ban_object_id"].ToString());
                        acc.Ribbon = int.Parse(reader["ribbon"].ToString());
                        acc.Ensign = int.Parse(reader["ensign"].ToString());
                        acc.Medal = int.Parse(reader["medal"].ToString());
                        acc.MasterMedal = int.Parse(reader["master_medal"].ToString());
                        acc.Mission.Mission1 = int.Parse(reader["mission_id1"].ToString());
                        acc.Mission.Mission2 = int.Parse(reader["mission_id2"].ToString());
                        acc.Mission.Mission3 = int.Parse(reader["mission_id3"].ToString());
                        acc.Tags = int.Parse(reader["tags"].ToString());
                        acc.InventoryPlus = int.Parse(reader["inventory_plus"].ToString());
                        acc.Country = (CountryFlags) int.Parse(reader["country_flag"].ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("[AccountManager.CreateAccount] " + exception.Message, LoggerType.Error, exception);
                Player = null;
                flag = false;
            }
            return flag;
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
                return (Accounts.TryGetValue(id, out account) ? account : (noUseDB ? null : GetAccountDB(id, null, 1, 0x181f)));
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
                return (Accounts.TryGetValue(id, out account) ? account : GetAccountDB(id, null, 1, searchFlag));
            }
            catch
            {
                return null;
            }
        }

        public static Account GetAccountDB(object valor, object valor2, int type, int searchFlag)
        {
            Account account;
            if ((((type != 0) || (((string) valor) != "")) && ((type != 1) || (((long) valor) != 0))) && ((type != 2) || (!string.IsNullOrEmpty((string) valor) && !string.IsNullOrEmpty((string) valor2))))
            {
                account = null;
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@valor", valor);
                        if (type == 0)
                        {
                            command.CommandText = "SELECT * FROM accounts WHERE username=@valor LIMIT 1";
                        }
                        else if (type == 1)
                        {
                            command.CommandText = "SELECT * FROM accounts WHERE player_id=@valor LIMIT 1";
                        }
                        else if (type == 2)
                        {
                            command.Parameters.AddWithValue("@valor2", valor2);
                            command.CommandText = "SELECT * FROM accounts WHERE username=@valor AND password=@valor2 LIMIT 1";
                        }
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
                            Account account1 = new Account();
                            account1.Username = reader["username"].ToString();
                            account1.Password = reader["password"].ToString();
                            account = account1;
                            account.SetPlayerId(long.Parse(reader["player_id"].ToString()), searchFlag);
                            account.Email = reader["email"].ToString();
                            account.Age = int.Parse(reader["age"].ToString());
                            account.MacAddress = (PhysicalAddress) reader.GetValue(6);
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
                            if (AddAccount(account) && account.IsOnline)
                            {
                                account.SetOnlineStatus(false);
                            }
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

        public static void GetFriendlyAccounts(PlayerFriends system)
        {
            if ((system != null) && (system.Friends.Count != 0))
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
                            if (num >= system.Friends.Count)
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
                                    FriendModel friend = system.GetFriend(long.Parse(reader["player_id"].ToString()));
                                    if (friend != null)
                                    {
                                        friend.Info.Nickname = reader["nickname"].ToString();
                                        friend.Info.Rank = int.Parse(reader["rank"].ToString());
                                        friend.Info.IsOnline = bool.Parse(reader["online"].ToString());
                                        friend.Info.Status.SetData(uint.Parse(reader["status"].ToString()), friend.PlayerId);
                                        if (friend.Info.IsOnline && !Accounts.ContainsKey(friend.PlayerId))
                                        {
                                            friend.Info.SetOnlineStatus(false);
                                            friend.Info.Status.ResetData(friend.PlayerId);
                                        }
                                    }
                                }
                                break;
                            }
                            FriendModel model = system.Friends[num];
                            string parameterName = "@valor" + num.ToString();
                            command.Parameters.AddWithValue(parameterName, model.PlayerId);
                            list.Add(parameterName);
                            num++;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print("was a problem loading (FriendAccounts)! " + exception.Message, LoggerType.Error, exception);
                }
            }
        }

        public static void GetFriendlyAccounts(PlayerFriends System, bool isOnline)
        {
            if ((System != null) && (System.Friends.Count != 0))
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        string str = "";
                        List<string> list = new List<string>();
                        int num = 0;
                        while (true)
                        {
                            if (num < System.Friends.Count)
                            {
                                FriendModel model = System.Friends[num];
                                if (model.State <= 0)
                                {
                                    string parameterName = "@valor" + num.ToString();
                                    command.Parameters.AddWithValue(parameterName, model.PlayerId);
                                    list.Add(parameterName);
                                    num++;
                                    continue;
                                }
                            }
                            else
                            {
                                str = string.Join(",", list.ToArray());
                                if (str != "")
                                {
                                    connection.Open();
                                    command.Parameters.AddWithValue("@on", isOnline);
                                    command.CommandText = "SELECT nickname, player_id, rank, status FROM accounts WHERE player_id in (" + str + ") AND online=@on ORDER BY player_id";
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
                                            friend.Info.IsOnline = isOnline;
                                            friend.Info.Status.SetData(uint.Parse(reader["status"].ToString()), friend.PlayerId);
                                            if (isOnline && !Accounts.ContainsKey(friend.PlayerId))
                                            {
                                                friend.Info.SetOnlineStatus(false);
                                                friend.Info.Status.ResetData(friend.PlayerId);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print("was a problem loading (FriendAccounts2)! " + exception.Message, LoggerType.Error, exception);
                }
            }
        }
    }
}

