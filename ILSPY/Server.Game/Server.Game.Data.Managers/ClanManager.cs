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
using Server.Game.Data.Models;
using Server.Game.Network;

namespace Server.Game.Data.Managers;

public static class ClanManager
{
	public static List<ClanModel> Clans = new List<ClanModel>();

	public static void Load()
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_clan";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					long num = long.Parse(((DbDataReader)(object)val3)["owner_id"].ToString());
					if (num != 0L)
					{
						ClanModel clanModel = new ClanModel
						{
							Id = int.Parse(((DbDataReader)(object)val3)["id"].ToString()),
							Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString()),
							Name = ((DbDataReader)(object)val3)["name"].ToString(),
							OwnerId = num,
							Logo = uint.Parse(((DbDataReader)(object)val3)["logo"].ToString()),
							NameColor = int.Parse(((DbDataReader)(object)val3)["name_color"].ToString()),
							Info = ((DbDataReader)(object)val3)["info"].ToString(),
							News = ((DbDataReader)(object)val3)["news"].ToString(),
							CreationDate = uint.Parse(((DbDataReader)(object)val3)["create_date"].ToString()),
							Authority = int.Parse(((DbDataReader)(object)val3)["authority"].ToString()),
							RankLimit = int.Parse(((DbDataReader)(object)val3)["rank_limit"].ToString()),
							MinAgeLimit = int.Parse(((DbDataReader)(object)val3)["min_age_limit"].ToString()),
							MaxAgeLimit = int.Parse(((DbDataReader)(object)val3)["max_age_limit"].ToString()),
							JoinType = (JoinClanType)int.Parse(((DbDataReader)(object)val3)["join_permission"].ToString()),
							Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
							MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
							MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
							Points = int.Parse(((DbDataReader)(object)val3)["point"].ToString()),
							MaxPlayers = int.Parse(((DbDataReader)(object)val3)["max_players"].ToString()),
							Exp = int.Parse(((DbDataReader)(object)val3)["exp"].ToString()),
							Effect = int.Parse(((DbDataReader)(object)val3)["effects"].ToString())
						};
						string exp = ((DbDataReader)(object)val3)["best_exp"].ToString();
						string participation = ((DbDataReader)(object)val3)["best_participants"].ToString();
						string wins = ((DbDataReader)(object)val3)["best_wins"].ToString();
						string kills = ((DbDataReader)(object)val3)["best_kills"].ToString();
						string headshots = ((DbDataReader)(object)val3)["best_headshots"].ToString();
						clanModel.BestPlayers.SetPlayers(exp, participation, wins, kills, headshots);
						Clans.Add(clanModel);
					}
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
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static List<ClanModel> GetClanListPerPage(int Page)
	{
		List<ClanModel> list = new List<ClanModel>();
		if (Page == 0)
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
				val2.Parameters.AddWithValue("@page", (object)(170 * Page));
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_clan ORDER BY id DESC OFFSET @page LIMIT 170";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					long num = long.Parse(((DbDataReader)(object)val3)["owner_id"].ToString());
					if (num != 0L)
					{
						ClanModel clanModel = new ClanModel
						{
							Id = int.Parse(((DbDataReader)(object)val3)["id"].ToString()),
							Rank = byte.Parse(((DbDataReader)(object)val3)["rank"].ToString()),
							Name = ((DbDataReader)(object)val3)["name"].ToString(),
							OwnerId = num,
							Logo = uint.Parse(((DbDataReader)(object)val3)["logo"].ToString()),
							NameColor = byte.Parse(((DbDataReader)(object)val3)["name_color"].ToString()),
							Info = ((DbDataReader)(object)val3)["info"].ToString(),
							News = ((DbDataReader)(object)val3)["news"].ToString(),
							CreationDate = uint.Parse(((DbDataReader)(object)val3)["create_date"].ToString()),
							Authority = int.Parse(((DbDataReader)(object)val3)["authority"].ToString()),
							RankLimit = int.Parse(((DbDataReader)(object)val3)["rank_limit"].ToString()),
							MinAgeLimit = int.Parse(((DbDataReader)(object)val3)["age_limit_start"].ToString()),
							MaxAgeLimit = int.Parse(((DbDataReader)(object)val3)["age_limit_end"].ToString()),
							JoinType = (JoinClanType)int.Parse(((DbDataReader)(object)val3)["join_permission"].ToString()),
							Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
							MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
							MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
							Points = int.Parse(((DbDataReader)(object)val3)["point"].ToString()),
							MaxPlayers = int.Parse(((DbDataReader)(object)val3)["max_players"].ToString()),
							Exp = int.Parse(((DbDataReader)(object)val3)["exp"].ToString()),
							Effect = byte.Parse(((DbDataReader)(object)val3)["effects"].ToString())
						};
						string exp = ((DbDataReader)(object)val3)["best_exp"].ToString();
						string participation = ((DbDataReader)(object)val3)["best_participants"].ToString();
						string wins = ((DbDataReader)(object)val3)["best_wins"].ToString();
						string kills = ((DbDataReader)(object)val3)["best_kills"].ToString();
						string headshots = ((DbDataReader)(object)val3)["best_headshots"].ToString();
						clanModel.BestPlayers.SetPlayers(exp, participation, wins, kills, headshots);
						list.Add(clanModel);
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

	public static ClanModel GetClan(int ClanId)
	{
		if (ClanId == 0)
		{
			return new ClanModel();
		}
		lock (Clans)
		{
			foreach (ClanModel clan in Clans)
			{
				if (clan.Id == ClanId)
				{
					return clan;
				}
			}
		}
		return new ClanModel();
	}

	public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache)
	{
		List<Account> list = new List<Account>();
		if (ClanId == 0)
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
				((DbCommand)(object)val2).CommandText = "SELECT player_id, nickname, nick_color, rank, online, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					long num = long.Parse(((DbDataReader)(object)val3)["player_id"].ToString());
					if (num == Exception)
					{
						continue;
					}
					Account account = new Account
					{
						PlayerId = num,
						Nickname = ((DbDataReader)(object)val3)["nickname"].ToString(),
						NickColor = int.Parse(((DbDataReader)(object)val3)["nick_color"].ToString()),
						Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString()),
						IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString()),
						ClanId = ClanId,
						ClanAccess = int.Parse(((DbDataReader)(object)val3)["clan_access"].ToString()),
						ClanDate = uint.Parse(((DbDataReader)(object)val3)["clan_date"].ToString())
					};
					account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
					account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
					account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
					account.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), account.PlayerId);
					if (UseCache)
					{
						Account account2 = AccountManager.GetAccount(account.PlayerId, noUseDB: true);
						if (account2 != null)
						{
							account.Connection = account2.Connection;
						}
					}
					list.Add(account);
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

	public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache, bool IsOnline)
	{
		List<Account> list = new List<Account>();
		if (ClanId == 0)
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
				((DbCommand)(object)val2).CommandText = "SELECT player_id, nickname, nick_color, rank, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan AND online=@on";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					long ınt = ((DbDataReader)(object)val3).GetInt64(0);
					if (ınt == Exception)
					{
						continue;
					}
					Account account = new Account
					{
						PlayerId = ınt,
						Nickname = ((DbDataReader)(object)val3)["nickname"].ToString(),
						NickColor = int.Parse(((DbDataReader)(object)val3)["nick_color"].ToString()),
						Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString()),
						IsOnline = IsOnline,
						ClanId = ClanId,
						ClanAccess = int.Parse(((DbDataReader)(object)val3)["clan_access"].ToString()),
						ClanDate = uint.Parse(((DbDataReader)(object)val3)["clan_date"].ToString())
					};
					account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
					account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
					account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
					account.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), account.PlayerId);
					if (UseCache)
					{
						Account account2 = AccountManager.GetAccount(account.PlayerId, noUseDB: true);
						if (account2 != null)
						{
							account.Connection = account2.Connection;
						}
					}
					list.Add(account);
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

	public static void SendPacket(GameServerPacket Packet, List<Account> Players)
	{
		if (Players.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
		foreach (Account Player in Players)
		{
			Player.SendCompletePacket(completeBytes, Packet.GetType().Name, OnlyInServer: false);
		}
	}

	public static void SendPacket(GameServerPacket Packet, List<Account> Players, long Exception)
	{
		if (Players.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
		foreach (Account Player in Players)
		{
			if (Player.PlayerId != Exception)
			{
				Player.SendCompletePacket(completeBytes, Packet.GetType().Name, OnlyInServer: false);
			}
		}
	}

	public static void SendPacket(GameServerPacket Packet, int ClanId, long Exception, bool UseCache, bool IsOnline)
	{
		SendPacket(Packet, GetClanPlayers(ClanId, Exception, UseCache, IsOnline));
	}

	public static bool RemoveClan(ClanModel clan)
	{
		lock (Clans)
		{
			return Clans.Remove(clan);
		}
	}

	public static void AddClan(ClanModel clan)
	{
		lock (Clans)
		{
			Clans.Add(clan);
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
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@name", (object)name);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM system_clan WHERE name=@name";
				num = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return num > 0;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return true;
		}
	}

	public static bool IsClanLogoExist(uint logo)
	{
		try
		{
			int num = 0;
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@logo", (object)(long)logo);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM system_clan WHERE logo=@logo";
				num = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return num > 0;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return true;
		}
	}
}
