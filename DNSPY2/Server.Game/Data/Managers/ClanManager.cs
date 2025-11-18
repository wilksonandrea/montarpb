using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network;

namespace Server.Game.Data.Managers
{
	// Token: 0x02000209 RID: 521
	public static class ClanManager
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0003713C File Offset: 0x0003533C
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
						long num = long.Parse(npgsqlDataReader["owner_id"].ToString());
						if (num != 0L)
						{
							ClanModel clanModel = new ClanModel
							{
								Id = int.Parse(npgsqlDataReader["id"].ToString()),
								Rank = int.Parse(npgsqlDataReader["rank"].ToString()),
								Name = npgsqlDataReader["name"].ToString(),
								OwnerId = num,
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
							string text = npgsqlDataReader["best_exp"].ToString();
							string text2 = npgsqlDataReader["best_participants"].ToString();
							string text3 = npgsqlDataReader["best_wins"].ToString();
							string text4 = npgsqlDataReader["best_kills"].ToString();
							string text5 = npgsqlDataReader["best_headshots"].ToString();
							clanModel.BestPlayers.SetPlayers(text, text2, text3, text4, text5);
							ClanManager.Clans.Add(clanModel);
						}
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000374AC File Offset: 0x000356AC
		public static List<ClanModel> GetClanListPerPage(int Page)
		{
			List<ClanModel> list = new List<ClanModel>();
			if (Page == 0)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@page", 170 * Page);
					npgsqlCommand.CommandText = "SELECT * FROM system_clan ORDER BY id DESC OFFSET @page LIMIT 170";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long num = long.Parse(npgsqlDataReader["owner_id"].ToString());
						if (num != 0L)
						{
							ClanModel clanModel = new ClanModel
							{
								Id = int.Parse(npgsqlDataReader["id"].ToString()),
								Rank = (int)byte.Parse(npgsqlDataReader["rank"].ToString()),
								Name = npgsqlDataReader["name"].ToString(),
								OwnerId = num,
								Logo = uint.Parse(npgsqlDataReader["logo"].ToString()),
								NameColor = (int)byte.Parse(npgsqlDataReader["name_color"].ToString()),
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
								Effect = (int)byte.Parse(npgsqlDataReader["effects"].ToString())
							};
							string text = npgsqlDataReader["best_exp"].ToString();
							string text2 = npgsqlDataReader["best_participants"].ToString();
							string text3 = npgsqlDataReader["best_wins"].ToString();
							string text4 = npgsqlDataReader["best_kills"].ToString();
							string text5 = npgsqlDataReader["best_headshots"].ToString();
							clanModel.BestPlayers.SetPlayers(text, text2, text3, text4, text5);
							list.Add(clanModel);
						}
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return list;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00037848 File Offset: 0x00035A48
		public static ClanModel GetClan(int ClanId)
		{
			if (ClanId == 0)
			{
				return new ClanModel();
			}
			List<ClanModel> clans = ClanManager.Clans;
			lock (clans)
			{
				foreach (ClanModel clanModel in ClanManager.Clans)
				{
					if (clanModel.Id == ClanId)
					{
						return clanModel;
					}
				}
			}
			return new ClanModel();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000378DC File Offset: 0x00035ADC
		public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache)
		{
			List<Account> list = new List<Account>();
			if (ClanId == 0)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT player_id, nickname, nick_color, rank, online, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long num = long.Parse(npgsqlDataReader["player_id"].ToString());
						if (num != Exception)
						{
							Account account = new Account
							{
								PlayerId = num,
								Nickname = npgsqlDataReader["nickname"].ToString(),
								NickColor = int.Parse(npgsqlDataReader["nick_color"].ToString()),
								Rank = int.Parse(npgsqlDataReader["rank"].ToString()),
								IsOnline = bool.Parse(npgsqlDataReader["online"].ToString()),
								ClanId = ClanId,
								ClanAccess = int.Parse(npgsqlDataReader["clan_access"].ToString()),
								ClanDate = uint.Parse(npgsqlDataReader["clan_date"].ToString())
							};
							account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
							account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
							account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
							account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), account.PlayerId);
							if (UseCache)
							{
								Account account2 = AccountManager.GetAccount(account.PlayerId, true);
								if (account2 != null)
								{
									account.Connection = account2.Connection;
								}
							}
							list.Add(account);
						}
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return list;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00037B20 File Offset: 0x00035D20
		public static List<Account> GetClanPlayers(int ClanId, long Exception, bool UseCache, bool IsOnline)
		{
			List<Account> list = new List<Account>();
			if (ClanId == 0)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@clan", ClanId);
					npgsqlCommand.Parameters.AddWithValue("@on", IsOnline);
					npgsqlCommand.CommandText = "SELECT player_id, nickname, nick_color, rank, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan AND online=@on";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						long @int = npgsqlDataReader.GetInt64(0);
						if (@int != Exception)
						{
							Account account = new Account
							{
								PlayerId = @int,
								Nickname = npgsqlDataReader["nickname"].ToString(),
								NickColor = int.Parse(npgsqlDataReader["nick_color"].ToString()),
								Rank = int.Parse(npgsqlDataReader["rank"].ToString()),
								IsOnline = IsOnline,
								ClanId = ClanId,
								ClanAccess = int.Parse(npgsqlDataReader["clan_access"].ToString()),
								ClanDate = uint.Parse(npgsqlDataReader["clan_date"].ToString())
							};
							account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
							account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
							account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
							account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), account.PlayerId);
							if (UseCache)
							{
								Account account2 = AccountManager.GetAccount(account.PlayerId, true);
								if (account2 != null)
								{
									account.Connection = account2.Connection;
								}
							}
							list.Add(account);
						}
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return list;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00037D5C File Offset: 0x00035F5C
		public static void SendPacket(GameServerPacket Packet, List<Account> Players)
		{
			if (Players.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
			foreach (Account account in Players)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name, false);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00037DCC File Offset: 0x00035FCC
		public static void SendPacket(GameServerPacket Packet, List<Account> Players, long Exception)
		{
			if (Players.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("ClanManager.SendPacket");
			foreach (Account account in Players)
			{
				if (account.PlayerId != Exception)
				{
					account.SendCompletePacket(completeBytes, Packet.GetType().Name, false);
				}
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00006265 File Offset: 0x00004465
		public static void SendPacket(GameServerPacket Packet, int ClanId, long Exception, bool UseCache, bool IsOnline)
		{
			ClanManager.SendPacket(Packet, ClanManager.GetClanPlayers(ClanId, Exception, UseCache, IsOnline));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00037E48 File Offset: 0x00036048
		public static bool RemoveClan(ClanModel clan)
		{
			List<ClanModel> clans = ClanManager.Clans;
			bool flag2;
			lock (clans)
			{
				flag2 = ClanManager.Clans.Remove(clan);
			}
			return flag2;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00037E90 File Offset: 0x00036090
		public static void AddClan(ClanModel clan)
		{
			List<ClanModel> clans = ClanManager.Clans;
			lock (clans)
			{
				ClanManager.Clans.Add(clan);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00037ED4 File Offset: 0x000360D4
		public static bool IsClanNameExist(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return true;
			}
			bool flag;
			try
			{
				int num = 0;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@name", name);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan WHERE name=@name";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = num > 0;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00037F88 File Offset: 0x00036188
		public static bool IsClanLogoExist(uint logo)
		{
			bool flag;
			try
			{
				int num = 0;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@logo", (long)((ulong)logo));
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan WHERE logo=@logo";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = num > 0;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00006277 File Offset: 0x00004477
		// Note: this type is marked as 'beforefieldinit'.
		static ClanManager()
		{
		}

		// Token: 0x0400044F RID: 1103
		public static List<ClanModel> Clans = new List<ClanModel>();
	}
}
