namespace Server.Game.Data.Managers
{
    using Npgsql;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class ClanManager
    {
        public static List<ClanModel> Clans = new List<ClanModel>();

        public static void AddClan(ClanModel clan)
        {
            List<ClanModel> clans = Clans;
            lock (clans)
            {
                Clans.Add(clan);
            }
        }

        public static ClanModel GetClan(int ClanId)
        {
            ClanModel model2;
            if (ClanId == 0)
            {
                return new ClanModel();
            }
            List<ClanModel> clans = Clans;
            lock (clans)
            {
                using (List<ClanModel>.Enumerator enumerator = Clans.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            ClanModel current = enumerator.Current;
                            if (current.Id != ClanId)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return new ClanModel();
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public static List<ClanModel> GetClanListPerPage(int Page)
        {
            List<ClanModel> list = new List<ClanModel>();
            if (Page != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@page", 170 * Page);
                        command.CommandText = "SELECT * FROM system_clan ORDER BY id DESC OFFSET @page LIMIT 170";
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
                            long num = long.Parse(reader["owner_id"].ToString());
                            if (num != 0)
                            {
                                ClanModel model1 = new ClanModel();
                                model1.Id = int.Parse(reader["id"].ToString());
                                model1.Rank = byte.Parse(reader["rank"].ToString());
                                model1.Name = reader["name"].ToString();
                                model1.OwnerId = num;
                                model1.Logo = uint.Parse(reader["logo"].ToString());
                                model1.NameColor = byte.Parse(reader["name_color"].ToString());
                                model1.Info = reader["info"].ToString();
                                model1.News = reader["news"].ToString();
                                model1.CreationDate = uint.Parse(reader["create_date"].ToString());
                                model1.Authority = int.Parse(reader["authority"].ToString());
                                model1.RankLimit = int.Parse(reader["rank_limit"].ToString());
                                model1.MinAgeLimit = int.Parse(reader["age_limit_start"].ToString());
                                model1.MaxAgeLimit = int.Parse(reader["age_limit_end"].ToString());
                                model1.JoinType = (JoinClanType) int.Parse(reader["join_permission"].ToString());
                                model1.Matches = int.Parse(reader["matches"].ToString());
                                model1.MatchWins = int.Parse(reader["match_wins"].ToString());
                                model1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                                model1.Points = int.Parse(reader["point"].ToString());
                                model1.MaxPlayers = int.Parse(reader["max_players"].ToString());
                                model1.Exp = int.Parse(reader["exp"].ToString());
                                model1.Effect = byte.Parse(reader["effects"].ToString());
                                ClanModel item = model1;
                                string exp = reader["best_exp"].ToString();
                                item.BestPlayers.SetPlayers(exp, reader["best_participants"].ToString(), reader["best_wins"].ToString(), reader["best_kills"].ToString(), reader["best_headshots"].ToString());
                                list.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache)
        {
            List<Account> list = new List<Account>();
            if (ClanId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@clan", ClanId);
                        command.CommandText = "SELECT player_id, nickname, nick_color, rank, online, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan";
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
                            long num = long.Parse(reader["player_id"].ToString());
                            if (num != Exception)
                            {
                                Account account1 = new Account();
                                account1.PlayerId = num;
                                account1.Nickname = reader["nickname"].ToString();
                                account1.NickColor = int.Parse(reader["nick_color"].ToString());
                                account1.Rank = int.Parse(reader["rank"].ToString());
                                account1.IsOnline = bool.Parse(reader["online"].ToString());
                                account1.ClanId = ClanId;
                                account1.ClanAccess = int.Parse(reader["clan_access"].ToString());
                                account1.ClanDate = uint.Parse(reader["clan_date"].ToString());
                                Account item = account1;
                                item.Bonus = DaoManagerSQL.GetPlayerBonusDB(item.PlayerId);
                                item.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(item.PlayerId);
                                item.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(item.PlayerId);
                                item.Status.SetData(uint.Parse(reader["status"].ToString()), item.PlayerId);
                                if (UseCache)
                                {
                                    Account account = AccountManager.GetAccount(item.PlayerId, true);
                                    if (account != null)
                                    {
                                        item.Connection = account.Connection;
                                    }
                                }
                                list.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache, bool IsOnline)
        {
            List<Account> list = new List<Account>();
            if (ClanId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@clan", ClanId);
                        command.Parameters.AddWithValue("@on", IsOnline);
                        command.CommandText = "SELECT player_id, nickname, nick_color, rank, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan AND online=@on";
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
                            long num = reader.GetInt64(0);
                            if (num != Exception)
                            {
                                Account account1 = new Account();
                                account1.PlayerId = num;
                                account1.Nickname = reader["nickname"].ToString();
                                account1.NickColor = int.Parse(reader["nick_color"].ToString());
                                account1.Rank = int.Parse(reader["rank"].ToString());
                                account1.IsOnline = IsOnline;
                                account1.ClanId = ClanId;
                                account1.ClanAccess = int.Parse(reader["clan_access"].ToString());
                                account1.ClanDate = uint.Parse(reader["clan_date"].ToString());
                                Account item = account1;
                                item.Bonus = DaoManagerSQL.GetPlayerBonusDB(item.PlayerId);
                                item.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(item.PlayerId);
                                item.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(item.PlayerId);
                                item.Status.SetData(uint.Parse(reader["status"].ToString()), item.PlayerId);
                                if (UseCache)
                                {
                                    Account account = AccountManager.GetAccount(item.PlayerId, true);
                                    if (account != null)
                                    {
                                        item.Connection = account.Connection;
                                    }
                                }
                                list.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static bool IsClanLogoExist(uint logo)
        {
            try
            {
                int num = 0;
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@logo", (long) logo);
                    command1.CommandText = "SELECT COUNT(*) FROM system_clan WHERE logo=@logo";
                    num = Convert.ToInt32(command1.ExecuteScalar());
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return (num > 0);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return true;
            }
        }

        public static bool IsClanNameExist(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            try
            {
                int num = 0;
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@name", name);
                    command1.CommandText = "SELECT COUNT(*) FROM system_clan WHERE name=@name";
                    num = Convert.ToInt32(command1.ExecuteScalar());
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return (num > 0);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return true;
            }
        }

        public static void Load()
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "SELECT * FROM system_clan";
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
                        long num = long.Parse(reader["owner_id"].ToString());
                        if (num != 0)
                        {
                            ClanModel model1 = new ClanModel();
                            model1.Id = int.Parse(reader["id"].ToString());
                            model1.Rank = int.Parse(reader["rank"].ToString());
                            model1.Name = reader["name"].ToString();
                            model1.OwnerId = num;
                            model1.Logo = uint.Parse(reader["logo"].ToString());
                            model1.NameColor = int.Parse(reader["name_color"].ToString());
                            model1.Info = reader["info"].ToString();
                            model1.News = reader["news"].ToString();
                            model1.CreationDate = uint.Parse(reader["create_date"].ToString());
                            model1.Authority = int.Parse(reader["authority"].ToString());
                            model1.RankLimit = int.Parse(reader["rank_limit"].ToString());
                            model1.MinAgeLimit = int.Parse(reader["min_age_limit"].ToString());
                            model1.MaxAgeLimit = int.Parse(reader["max_age_limit"].ToString());
                            model1.JoinType = (JoinClanType) int.Parse(reader["join_permission"].ToString());
                            model1.Matches = int.Parse(reader["matches"].ToString());
                            model1.MatchWins = int.Parse(reader["match_wins"].ToString());
                            model1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                            model1.Points = int.Parse(reader["point"].ToString());
                            model1.MaxPlayers = int.Parse(reader["max_players"].ToString());
                            model1.Exp = int.Parse(reader["exp"].ToString());
                            model1.Effect = int.Parse(reader["effects"].ToString());
                            ClanModel item = model1;
                            string exp = reader["best_exp"].ToString();
                            item.BestPlayers.SetPlayers(exp, reader["best_participants"].ToString(), reader["best_wins"].ToString(), reader["best_kills"].ToString(), reader["best_headshots"].ToString());
                            Clans.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static bool RemoveClan(ClanModel clan)
        {
            List<ClanModel> clans = Clans;
            lock (clans)
            {
                return Clans.Remove(clan);
            }
        }

        public static void SendPacket(GameServerPacket Packet, List<Account> Players)
        {
            if (Players.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
                using (List<Account>.Enumerator enumerator = Players.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name, false);
                    }
                }
            }
        }

        public static void SendPacket(GameServerPacket Packet, List<Account> Players, long Exception)
        {
            if (Players.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
                foreach (Account account in Players)
                {
                    if (account.PlayerId != Exception)
                    {
                        account.SendCompletePacket(completeBytes, Packet.GetType().Name, false);
                    }
                }
            }
        }

        public static void SendPacket(GameServerPacket Packet, int ClanId, long Exception, bool UseCache, bool IsOnline)
        {
            SendPacket(Packet, GetClanPlayers(ClanId, Exception, UseCache, IsOnline));
        }
    }
}

