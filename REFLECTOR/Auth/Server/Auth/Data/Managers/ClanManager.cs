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

    public class ClanManager
    {
        public static ClanModel GetClanDB(object Value, int Type)
        {
            ClanModel model = new ClanModel();
            if (((Type == 1) && (((int) Value) <= 0)) || ((Type == 0) && string.IsNullOrEmpty(Value.ToString())))
            {
                return model;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    string str = (Type == 0) ? "name" : "id";
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@valor", Value);
                    command.CommandText = "SELECT * FROM system_clan WHERE " + str + "=@valor";
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
                        model.Id = int.Parse(reader["id"].ToString());
                        model.Rank = byte.Parse(reader["rank"].ToString());
                        model.Name = reader["name"].ToString();
                        model.OwnerId = long.Parse(reader["owner_id"].ToString());
                        model.Logo = uint.Parse(reader["logo"].ToString());
                        model.NameColor = byte.Parse(reader["name_color"].ToString());
                        model.Effect = byte.Parse(reader["effects"].ToString());
                    }
                }
                return ((model.Id == 0) ? new ClanModel() : model);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return new ClanModel();
            }
        }

        public static List<Account> GetClanPlayers(int ClanId, long Exception)
        {
            List<Account> list = new List<Account>();
            if (ClanId > 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@clan", ClanId);
                        command.CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan";
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
                            long playerId = long.Parse(reader["player_id"].ToString());
                            if (playerId != Exception)
                            {
                                Account account1 = new Account();
                                account1.PlayerId = playerId;
                                account1.Nickname = reader["nickname"].ToString();
                                account1.Rank = byte.Parse(reader["rank"].ToString());
                                account1.IsOnline = bool.Parse(reader["online"].ToString());
                                Account item = account1;
                                item.Bonus = DaoManagerSQL.GetPlayerBonusDB(item.PlayerId);
                                item.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(item.PlayerId);
                                item.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(item.PlayerId);
                                item.Status.SetData(uint.Parse(reader["status"].ToString()), playerId);
                                if (item.IsOnline && !AccountManager.Accounts.ContainsKey(playerId))
                                {
                                    item.SetOnlineStatus(false);
                                    item.Status.ResetData(item.PlayerId);
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

        public static List<Account> GetClanPlayers(int ClanId, long Exception, bool IsOnline)
        {
            List<Account> list = new List<Account>();
            if (ClanId > 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@clan", ClanId);
                        command.Parameters.AddWithValue("@on", IsOnline);
                        command.CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan AND online=@on";
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
                            long playerId = long.Parse(reader["player_id"].ToString());
                            if (playerId != Exception)
                            {
                                Account account1 = new Account();
                                account1.PlayerId = playerId;
                                account1.Nickname = reader["nickname"].ToString();
                                account1.Rank = byte.Parse(reader["rank"].ToString());
                                account1.IsOnline = bool.Parse(reader["online"].ToString());
                                Account item = account1;
                                item.Bonus = DaoManagerSQL.GetPlayerBonusDB(item.PlayerId);
                                item.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(item.PlayerId);
                                item.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(item.PlayerId);
                                item.Status.SetData(uint.Parse(reader["status"].ToString()), playerId);
                                if (item.IsOnline && !AccountManager.Accounts.ContainsKey(playerId))
                                {
                                    item.SetOnlineStatus(false);
                                    item.Status.ResetData(item.PlayerId);
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
    }
}

