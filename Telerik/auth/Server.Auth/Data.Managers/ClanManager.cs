using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Server.Auth.Data.Managers
{
	public class ClanManager
	{
		public ClanManager()
		{
		}

		public static ClanModel GetClanDB(object Value, int Type)
		{
			ClanModel clanModel;
			ClanModel str = new ClanModel();
			if (Type == 1 && (int)Value <= 0 || Type == 0 && string.IsNullOrEmpty(Value.ToString()))
			{
				return str;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					string str1 = (Type == 0 ? "name" : "id");
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@valor", Value);
					npgsqlCommand.CommandText = string.Concat("SELECT * FROM system_clan WHERE ", str1, "=@valor");
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						str.Id = int.Parse(npgsqlDataReader["id"].ToString());
						str.Rank = byte.Parse(npgsqlDataReader["rank"].ToString());
						str.Name = npgsqlDataReader["name"].ToString();
						str.OwnerId = long.Parse(npgsqlDataReader["owner_id"].ToString());
						str.Logo = uint.Parse(npgsqlDataReader["logo"].ToString());
						str.NameColor = byte.Parse(npgsqlDataReader["name_color"].ToString());
						str.Effect = byte.Parse(npgsqlDataReader["effects"].ToString());
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				clanModel = (str.Id == 0 ? new ClanModel() : str);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				clanModel = new ClanModel();
			}
			return clanModel;
		}

		public static List<Account> GetClanPlayers(int ClanId, long Exception)
		{
			List<Account> accounts = new List<Account>();
			if (ClanId <= 0)
			{
				return accounts;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long ınt64 = long.Parse(npgsqlDataReader["player_id"].ToString());
						if (ınt64 == Exception)
						{
							continue;
						}
						Account account = new Account()
						{
							PlayerId = ınt64,
							Nickname = npgsqlDataReader["nickname"].ToString(),
							Rank = byte.Parse(npgsqlDataReader["rank"].ToString()),
							IsOnline = bool.Parse(npgsqlDataReader["online"].ToString()),
							Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId),
							Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId)
						};
						account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
						account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), ınt64);
						if (account.IsOnline && !AccountManager.Accounts.ContainsKey(ınt64))
						{
							account.SetOnlineStatus(false);
							account.Status.ResetData(account.PlayerId);
						}
						accounts.Add(account);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (System.Exception exception1)
			{
				System.Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return accounts;
		}

		public static List<Account> GetClanPlayers(int ClanId, long Exception, bool IsOnline)
		{
			List<Account> accounts = new List<Account>();
			if (ClanId <= 0)
			{
				return accounts;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.get_Parameters().AddWithValue("@on", IsOnline);
					npgsqlCommand.CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan AND online=@on";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long ınt64 = long.Parse(npgsqlDataReader["player_id"].ToString());
						if (ınt64 == Exception)
						{
							continue;
						}
						Account account = new Account()
						{
							PlayerId = ınt64,
							Nickname = npgsqlDataReader["nickname"].ToString(),
							Rank = byte.Parse(npgsqlDataReader["rank"].ToString()),
							IsOnline = bool.Parse(npgsqlDataReader["online"].ToString()),
							Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId),
							Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId)
						};
						account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
						account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), ınt64);
						if (account.IsOnline && !AccountManager.Accounts.ContainsKey(ınt64))
						{
							account.SetOnlineStatus(false);
							account.Status.ResetData(account.PlayerId);
						}
						accounts.Add(account);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (System.Exception exception1)
			{
				System.Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return accounts;
		}
	}
}