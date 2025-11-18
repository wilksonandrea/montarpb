using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Server.Game.Data.Managers
{
	public static class ClanManager
	{
		public static List<ClanModel> Clans;

		static ClanManager()
		{
			ClanManager.Clans = new List<ClanModel>();
		}

		public static void AddClan(ClanModel clan)
		{
			lock (ClanManager.Clans)
			{
				ClanManager.Clans.Add(clan);
			}
		}

		public static ClanModel GetClan(int ClanId)
		{
			ClanModel clanModel;
			if (ClanId == 0)
			{
				return new ClanModel();
			}
			lock (ClanManager.Clans)
			{
				List<ClanModel>.Enumerator enumerator = ClanManager.Clans.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ClanModel current = enumerator.Current;
						if (current.Id != ClanId)
						{
							continue;
						}
						clanModel = current;
						return clanModel;
					}
					return new ClanModel();
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return clanModel;
		}

		public static List<ClanModel> GetClanListPerPage(int Page)
		{
			List<ClanModel> clanModels = new List<ClanModel>();
			if (Page == 0)
			{
				return clanModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@page", 170 * Page);
					npgsqlCommand.CommandText = "SELECT * FROM system_clan ORDER BY id DESC OFFSET @page LIMIT 170";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long ınt64 = long.Parse(npgsqlDataReader["owner_id"].ToString());
						if (ınt64 == 0)
						{
							continue;
						}
						ClanModel clanModel = new ClanModel()
						{
							Id = int.Parse(npgsqlDataReader["id"].ToString()),
							Rank = byte.Parse(npgsqlDataReader["rank"].ToString()),
							Name = npgsqlDataReader["name"].ToString(),
							OwnerId = ınt64,
							Logo = uint.Parse(npgsqlDataReader["logo"].ToString()),
							NameColor = byte.Parse(npgsqlDataReader["name_color"].ToString()),
							Info = npgsqlDataReader["info"].ToString(),
							News = npgsqlDataReader["news"].ToString(),
							CreationDate = uint.Parse(npgsqlDataReader["create_date"].ToString()),
							Authority = int.Parse(npgsqlDataReader["authority"].ToString()),
							RankLimit = int.Parse(npgsqlDataReader["rank_limit"].ToString()),
							MinAgeLimit = int.Parse(npgsqlDataReader["age_limit_start"].ToString()),
							MaxAgeLimit = int.Parse(npgsqlDataReader["age_limit_end"].ToString()),
							JoinType = (JoinClanType)int.Parse(npgsqlDataReader["join_permission"].ToString()),
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							Points = (float)int.Parse(npgsqlDataReader["point"].ToString()),
							MaxPlayers = int.Parse(npgsqlDataReader["max_players"].ToString()),
							Exp = int.Parse(npgsqlDataReader["exp"].ToString()),
							Effect = byte.Parse(npgsqlDataReader["effects"].ToString())
						};
						string str = npgsqlDataReader["best_exp"].ToString();
						string str1 = npgsqlDataReader["best_participants"].ToString();
						string str2 = npgsqlDataReader["best_wins"].ToString();
						string str3 = npgsqlDataReader["best_kills"].ToString();
						string str4 = npgsqlDataReader["best_headshots"].ToString();
						clanModel.BestPlayers.SetPlayers(str, str1, str2, str3, str4);
						clanModels.Add(clanModel);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return clanModels;
		}

		public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache)
		{
			List<Account> accounts = new List<Account>();
			if (ClanId == 0)
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
					npgsqlCommand.CommandText = "SELECT player_id, nickname, nick_color, rank, online, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan";
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
							NickColor = int.Parse(npgsqlDataReader["nick_color"].ToString()),
							Rank = int.Parse(npgsqlDataReader["rank"].ToString()),
							IsOnline = bool.Parse(npgsqlDataReader["online"].ToString()),
							ClanId = ClanId,
							ClanAccess = int.Parse(npgsqlDataReader["clan_access"].ToString()),
							ClanDate = uint.Parse(npgsqlDataReader["clan_date"].ToString()),
							Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId),
							Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId)
						};
						account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
						account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), account.PlayerId);
						if (UseCache)
						{
							Account account1 = AccountManager.GetAccount(account.PlayerId, true);
							if (account1 != null)
							{
								account.Connection = account1.Connection;
							}
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

		public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache, bool IsOnline)
		{
			List<Account> accounts = new List<Account>();
			if (ClanId == 0)
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
					npgsqlCommand.CommandText = "SELECT player_id, nickname, nick_color, rank, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan AND online=@on";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long ınt64 = npgsqlDataReader.GetInt64(0);
						if (ınt64 == Exception)
						{
							continue;
						}
						Account account = new Account()
						{
							PlayerId = ınt64,
							Nickname = npgsqlDataReader["nickname"].ToString(),
							NickColor = int.Parse(npgsqlDataReader["nick_color"].ToString()),
							Rank = int.Parse(npgsqlDataReader["rank"].ToString()),
							IsOnline = IsOnline,
							ClanId = ClanId,
							ClanAccess = int.Parse(npgsqlDataReader["clan_access"].ToString()),
							ClanDate = uint.Parse(npgsqlDataReader["clan_date"].ToString()),
							Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId),
							Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId)
						};
						account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
						account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), account.PlayerId);
						if (UseCache)
						{
							Account account1 = AccountManager.GetAccount(account.PlayerId, true);
							if (account1 != null)
							{
								account.Connection = account1.Connection;
							}
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

		public static bool IsClanLogoExist(uint logo)
		{
			bool flag;
			try
			{
				int ınt32 = 0;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@logo", (long)((ulong)logo));
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan WHERE logo=@logo";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = ınt32 > 0;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = true;
			}
			return flag;
		}

		public static bool IsClanNameExist(string name)
		{
			bool flag;
			if (string.IsNullOrEmpty(name))
			{
				return true;
			}
			try
			{
				int ınt32 = 0;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@name", name);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan WHERE name=@name";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = ınt32 > 0;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = true;
			}
			return flag;
		}

		public static void Load()
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandText = "SELECT * FROM system_clan";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long ınt64 = long.Parse(npgsqlDataReader["owner_id"].ToString());
						if (ınt64 == 0)
						{
							continue;
						}
						ClanModel clanModel = new ClanModel()
						{
							Id = int.Parse(npgsqlDataReader["id"].ToString()),
							Rank = int.Parse(npgsqlDataReader["rank"].ToString()),
							Name = npgsqlDataReader["name"].ToString(),
							OwnerId = ınt64,
							Logo = uint.Parse(npgsqlDataReader["logo"].ToString()),
							NameColor = int.Parse(npgsqlDataReader["name_color"].ToString()),
							Info = npgsqlDataReader["info"].ToString(),
							News = npgsqlDataReader["news"].ToString(),
							CreationDate = uint.Parse(npgsqlDataReader["create_date"].ToString()),
							Authority = int.Parse(npgsqlDataReader["authority"].ToString()),
							RankLimit = int.Parse(npgsqlDataReader["rank_limit"].ToString()),
							MinAgeLimit = int.Parse(npgsqlDataReader["min_age_limit"].ToString()),
							MaxAgeLimit = int.Parse(npgsqlDataReader["max_age_limit"].ToString()),
							JoinType = (JoinClanType)int.Parse(npgsqlDataReader["join_permission"].ToString()),
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							Points = (float)int.Parse(npgsqlDataReader["point"].ToString()),
							MaxPlayers = int.Parse(npgsqlDataReader["max_players"].ToString()),
							Exp = int.Parse(npgsqlDataReader["exp"].ToString()),
							Effect = int.Parse(npgsqlDataReader["effects"].ToString())
						};
						string str = npgsqlDataReader["best_exp"].ToString();
						string str1 = npgsqlDataReader["best_participants"].ToString();
						string str2 = npgsqlDataReader["best_wins"].ToString();
						string str3 = npgsqlDataReader["best_kills"].ToString();
						string str4 = npgsqlDataReader["best_headshots"].ToString();
						clanModel.BestPlayers.SetPlayers(str, str1, str2, str3, str4);
						ClanManager.Clans.Add(clanModel);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static bool RemoveClan(ClanModel clan)
		{
			bool flag;
			lock (ClanManager.Clans)
			{
				flag = ClanManager.Clans.Remove(clan);
			}
			return flag;
		}

		public static void SendPacket(GameServerPacket Packet, List<Account> Players)
		{
			if (Players.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
			foreach (Account player in Players)
			{
				player.SendCompletePacket(completeBytes, Packet.GetType().Name, false);
			}
		}

		public static void SendPacket(GameServerPacket Packet, List<Account> Players, long Exception)
		{
			if (Players.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
			foreach (Account player in Players)
			{
				if (player.PlayerId == Exception)
				{
					continue;
				}
				player.SendCompletePacket(completeBytes, Packet.GetType().Name, false);
			}
		}

		public static void SendPacket(GameServerPacket Packet, int ClanId, long Exception, bool UseCache, bool IsOnline)
		{
			ClanManager.SendPacket(Packet, ClanManager.GetClanPlayers(ClanId, Exception, UseCache, IsOnline));
		}
	}
}