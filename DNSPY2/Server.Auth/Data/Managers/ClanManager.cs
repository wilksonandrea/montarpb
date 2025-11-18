using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Managers
{
	// Token: 0x02000061 RID: 97
	public class ClanManager
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000D14C File Offset: 0x0000B34C
		public static ClanModel GetClanDB(object Value, int Type)
		{
			ClanModel clanModel = new ClanModel();
			if ((Type == 1 && (int)Value <= 0) || (Type == 0 && string.IsNullOrEmpty(Value.ToString())))
			{
				return clanModel;
			}
			ClanModel clanModel2;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					string text = ((Type == 0) ? "name" : "id");
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@valor", Value);
					npgsqlCommand.CommandText = "SELECT * FROM system_clan WHERE " + text + "=@valor";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						clanModel.Id = int.Parse(npgsqlDataReader["id"].ToString());
						clanModel.Rank = (int)byte.Parse(npgsqlDataReader["rank"].ToString());
						clanModel.Name = npgsqlDataReader["name"].ToString();
						clanModel.OwnerId = long.Parse(npgsqlDataReader["owner_id"].ToString());
						clanModel.Logo = uint.Parse(npgsqlDataReader["logo"].ToString());
						clanModel.NameColor = (int)byte.Parse(npgsqlDataReader["name_color"].ToString());
						clanModel.Effect = (int)byte.Parse(npgsqlDataReader["effects"].ToString());
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				clanModel2 = ((clanModel.Id == 0) ? new ClanModel() : clanModel);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				clanModel2 = new ClanModel();
			}
			return clanModel2;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000D338 File Offset: 0x0000B538
		public static List<Account> GetClanPlayers(int ClanId, long Exception)
		{
			List<Account> list = new List<Account>();
			if (ClanId <= 0)
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
					npgsqlCommand.CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan";
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
								Rank = (int)byte.Parse(npgsqlDataReader["rank"].ToString()),
								IsOnline = bool.Parse(npgsqlDataReader["online"].ToString())
							};
							account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
							account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
							account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
							account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), num);
							if (account.IsOnline && !AccountManager.Accounts.ContainsKey(num))
							{
								account.SetOnlineStatus(false);
								account.Status.ResetData(account.PlayerId);
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

		// Token: 0x0600017B RID: 379 RVA: 0x0000D530 File Offset: 0x0000B730
		public static List<Account> GetClanPlayers(int ClanId, long Exception, bool IsOnline)
		{
			List<Account> list = new List<Account>();
			if (ClanId <= 0)
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
					npgsqlCommand.CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan AND online=@on";
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
								Rank = (int)byte.Parse(npgsqlDataReader["rank"].ToString()),
								IsOnline = bool.Parse(npgsqlDataReader["online"].ToString())
							};
							account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
							account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
							account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
							account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), num);
							if (account.IsOnline && !AccountManager.Accounts.ContainsKey(num))
							{
								account.SetOnlineStatus(false);
								account.Status.ResetData(account.PlayerId);
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

		// Token: 0x0600017C RID: 380 RVA: 0x00002409 File Offset: 0x00000609
		public ClanManager()
		{
		}
	}
}
