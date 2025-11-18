using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Managers;

public class ClanManager
{
	public static ClanModel GetClanDB(object Value, int Type)
	{
		ClanModel clanModel = new ClanModel();
		if ((Type == 1 && (int)Value <= 0) || (Type == 0 && string.IsNullOrEmpty(Value.ToString())))
		{
			return clanModel;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				string text = ((Type == 0) ? "name" : "id");
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@valor", Value);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_clan WHERE " + text + "=@valor";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					clanModel.Id = int.Parse(((DbDataReader)(object)val3)["id"].ToString());
					clanModel.Rank = byte.Parse(((DbDataReader)(object)val3)["rank"].ToString());
					clanModel.Name = ((DbDataReader)(object)val3)["name"].ToString();
					clanModel.OwnerId = long.Parse(((DbDataReader)(object)val3)["owner_id"].ToString());
					clanModel.Logo = uint.Parse(((DbDataReader)(object)val3)["logo"].ToString());
					clanModel.NameColor = byte.Parse(((DbDataReader)(object)val3)["name_color"].ToString());
					clanModel.Effect = byte.Parse(((DbDataReader)(object)val3)["effects"].ToString());
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return (clanModel.Id == 0) ? new ClanModel() : clanModel;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return new ClanModel();
		}
	}

	public static List<Account> GetClanPlayers(int ClanId, long Exception)
	{
		List<Account> list = new List<Account>();
		if (ClanId <= 0)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@clan", (object)ClanId);
				((DbCommand)(object)val2).CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					long num = long.Parse(((DbDataReader)(object)val3)["player_id"].ToString());
					if (num != Exception)
					{
						Account account = new Account
						{
							PlayerId = num,
							Nickname = ((DbDataReader)(object)val3)["nickname"].ToString(),
							Rank = byte.Parse(((DbDataReader)(object)val3)["rank"].ToString()),
							IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString())
						};
						account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
						account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
						account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
						account.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), num);
						if (account.IsOnline && !AccountManager.Accounts.ContainsKey(num))
						{
							account.SetOnlineStatus(Online: false);
							account.Status.ResetData(account.PlayerId);
						}
						list.Add(account);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static List<Account> GetClanPlayers(int ClanId, long Exception, bool IsOnline)
	{
		List<Account> list = new List<Account>();
		if (ClanId <= 0)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@clan", (object)ClanId);
				val2.Parameters.AddWithValue("@on", (object)IsOnline);
				((DbCommand)(object)val2).CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan AND online=@on";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					long num = long.Parse(((DbDataReader)(object)val3)["player_id"].ToString());
					if (num != Exception)
					{
						Account account = new Account
						{
							PlayerId = num,
							Nickname = ((DbDataReader)(object)val3)["nickname"].ToString(),
							Rank = byte.Parse(((DbDataReader)(object)val3)["rank"].ToString()),
							IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString())
						};
						account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
						account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
						account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
						account.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), num);
						if (account.IsOnline && !AccountManager.Accounts.ContainsKey(num))
						{
							account.SetOnlineStatus(Online: false);
							account.Status.ResetData(account.PlayerId);
						}
						list.Add(account);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}
}
