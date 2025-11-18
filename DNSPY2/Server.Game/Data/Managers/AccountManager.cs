using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Data.Managers
{
	// Token: 0x02000208 RID: 520
	public static class AccountManager
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x00036918 File Offset: 0x00034B18
		public static void AddAccount(Account acc)
		{
			SortedList<long, Account> accounts = AccountManager.Accounts;
			lock (accounts)
			{
				if (!AccountManager.Accounts.ContainsKey(acc.PlayerId))
				{
					AccountManager.Accounts.Add(acc.PlayerId, acc);
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00036974 File Offset: 0x00034B74
		public static Account GetAccountDB(object valor, int type, int searchDBFlag)
		{
			if ((type == 2 && (long)valor == 0L) || ((type == 0 || type == 1) && (string)valor == ""))
			{
				return null;
			}
			Account account = null;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@value", valor);
					npgsqlCommand.CommandText = "SELECT * FROM accounts WHERE " + ((type == 0) ? "username" : ((type == 1) ? "nickname" : "player_id")) + "=@value";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						account = new Account
						{
							Username = npgsqlDataReader["username"].ToString(),
							Password = npgsqlDataReader["password"].ToString()
						};
						account.SetPlayerId(long.Parse(npgsqlDataReader["player_id"].ToString()), searchDBFlag);
						account.Email = npgsqlDataReader["email"].ToString();
						account.Age = int.Parse(npgsqlDataReader["age"].ToString());
						account.SetPublicIP(npgsqlDataReader.GetString(5));
						account.Nickname = npgsqlDataReader["nickname"].ToString();
						account.NickColor = int.Parse(npgsqlDataReader["nick_color"].ToString());
						account.Rank = int.Parse(npgsqlDataReader["rank"].ToString());
						account.Exp = int.Parse(npgsqlDataReader["experience"].ToString());
						account.Gold = int.Parse(npgsqlDataReader["gold"].ToString());
						account.Cash = int.Parse(npgsqlDataReader["cash"].ToString());
						account.CafePC = (CafeEnum)int.Parse(npgsqlDataReader["pc_cafe"].ToString());
						account.Access = (AccessLevel)int.Parse(npgsqlDataReader["access_level"].ToString());
						account.IsOnline = bool.Parse(npgsqlDataReader["online"].ToString());
						account.ClanId = int.Parse(npgsqlDataReader["clan_id"].ToString());
						account.ClanAccess = int.Parse(npgsqlDataReader["clan_access"].ToString());
						account.Effects = (CouponEffects)long.Parse(npgsqlDataReader["coupon_effect"].ToString());
						account.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), account.PlayerId);
						account.LastRankUpDate = uint.Parse(npgsqlDataReader["last_rank_update"].ToString());
						account.BanObjectId = long.Parse(npgsqlDataReader["ban_object_id"].ToString());
						account.Ribbon = int.Parse(npgsqlDataReader["ribbon"].ToString());
						account.Ensign = int.Parse(npgsqlDataReader["ensign"].ToString());
						account.Medal = int.Parse(npgsqlDataReader["medal"].ToString());
						account.MasterMedal = int.Parse(npgsqlDataReader["master_medal"].ToString());
						account.Mission.Mission1 = int.Parse(npgsqlDataReader["mission_id1"].ToString());
						account.Mission.Mission2 = int.Parse(npgsqlDataReader["mission_id2"].ToString());
						account.Mission.Mission3 = int.Parse(npgsqlDataReader["mission_id3"].ToString());
						account.Tags = int.Parse(npgsqlDataReader["tags"].ToString());
						account.InventoryPlus = int.Parse(npgsqlDataReader["inventory_plus"].ToString());
						account.Country = (CountryFlags)int.Parse(npgsqlDataReader["country_flag"].ToString());
						AccountManager.AddAccount(account);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("was a problem loading accounts! " + ex.Message, LoggerType.Error, ex);
			}
			return account;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public static void GetFriendlyAccounts(PlayerFriends System)
		{
			if (System != null && System.Friends.Count != 0)
			{
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						List<string> list = new List<string>();
						for (int i = 0; i < System.Friends.Count; i++)
						{
							FriendModel friendModel = System.Friends[i];
							string text = "@valor" + i.ToString();
							npgsqlCommand.Parameters.AddWithValue(text, friendModel.PlayerId);
							list.Add(text);
						}
						string text2 = string.Join(",", list.ToArray());
						npgsqlCommand.CommandText = "SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (" + text2 + ") ORDER BY player_id";
						NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
						while (npgsqlDataReader.Read())
						{
							FriendModel friend = System.GetFriend(long.Parse(npgsqlDataReader["player_id"].ToString()));
							if (friend != null)
							{
								friend.Info.Nickname = npgsqlDataReader["nickname"].ToString();
								friend.Info.Rank = int.Parse(npgsqlDataReader["rank"].ToString());
								friend.Info.IsOnline = bool.Parse(npgsqlDataReader["online"].ToString());
								friend.Info.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), friend.PlayerId);
							}
						}
						npgsqlCommand.Dispose();
						npgsqlDataReader.Dispose();
						npgsqlDataReader.Close();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
				catch (Exception ex)
				{
					CLogger.Print("was a problem loading (FriendlyAccounts); " + ex.Message, LoggerType.Error, ex);
				}
				return;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00037000 File Offset: 0x00035200
		public static Account GetAccount(long id, int searchFlag)
		{
			if (id == 0L)
			{
				return null;
			}
			Account account;
			try
			{
				Account account2;
				account = (AccountManager.Accounts.TryGetValue(id, out account2) ? account2 : AccountManager.GetAccountDB(id, 2, searchFlag));
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0003704C File Offset: 0x0003524C
		public static Account GetAccount(long id, bool noUseDB)
		{
			if (id == 0L)
			{
				return null;
			}
			Account account;
			try
			{
				Account account2;
				account = (AccountManager.Accounts.TryGetValue(id, out account2) ? account2 : (noUseDB ? null : AccountManager.GetAccountDB(id, 2, 7455)));
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000370A4 File Offset: 0x000352A4
		public static Account GetAccount(string text, int type, int searchFlag)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			foreach (Account account in AccountManager.Accounts.Values)
			{
				if (account != null && ((type == 1 && account.Nickname == text && account.Nickname.Length > 0) || (type == 0 && string.Compare(account.Username, text) == 0)))
				{
					return account;
				}
			}
			return AccountManager.GetAccountDB(text, type, searchFlag);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000623C File Offset: 0x0000443C
		public static bool UpdatePlayerName(string name, long playerId)
		{
			return ComDiv.UpdateDB("accounts", "nickname", name, "player_id", playerId);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00006259 File Offset: 0x00004459
		// Note: this type is marked as 'beforefieldinit'.
		static AccountManager()
		{
		}

		// Token: 0x0400044E RID: 1102
		public static SortedList<long, Account> Accounts = new SortedList<long, Account>();
	}
}
