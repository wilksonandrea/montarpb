using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Server.Game.Data.Managers
{
	public static class AccountManager
	{
		public static SortedList<long, Account> Accounts;

		static AccountManager()
		{
			AccountManager.Accounts = new SortedList<long, Account>();
		}

		public static void AddAccount(Account acc)
		{
			lock (AccountManager.Accounts)
			{
				if (!AccountManager.Accounts.ContainsKey(acc.PlayerId))
				{
					AccountManager.Accounts.Add(acc.PlayerId, acc);
				}
			}
		}

		public static Account GetAccount(long id, int searchFlag)
		{
			Account account;
			Account account1;
			if (id == 0)
			{
				return null;
			}
			try
			{
				account1 = (AccountManager.Accounts.TryGetValue(id, out account) ? account : AccountManager.GetAccountDB(id, 2, searchFlag));
			}
			catch
			{
				account1 = null;
			}
			return account1;
		}

		public static Account GetAccount(long id, bool noUseDB)
		{
			Account account;
			Account account1;
			Account accountDB;
			if (id == 0)
			{
				return null;
			}
			try
			{
				if (AccountManager.Accounts.TryGetValue(id, out account))
				{
					accountDB = account;
				}
				else if (noUseDB)
				{
					accountDB = null;
				}
				else
				{
					accountDB = AccountManager.GetAccountDB(id, 2, 7455);
				}
				account1 = accountDB;
			}
			catch
			{
				account1 = null;
			}
			return account1;
		}

		public static Account GetAccount(string text, int type, int searchFlag)
		{
			Account account;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			using (IEnumerator<Account> enumerator = AccountManager.Accounts.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Account current = enumerator.Current;
					if (current == null || (type != 1 || !(current.Nickname == text) || current.Nickname.Length <= 0) && (type != 0 || string.Compare(current.Username, text) != 0))
					{
						continue;
					}
					account = current;
					return account;
				}
				return AccountManager.GetAccountDB(text, type, searchFlag);
			}
			return account;
		}

		public static Account GetAccountDB(object valor, int type, int searchDBFlag)
		{
			string str;
			if (type == 2 && (long)valor == 0 || (type == 0 || type == 1) && (string)valor == "")
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
					npgsqlCommand.get_Parameters().AddWithValue("@value", valor);
					NpgsqlCommand npgsqlCommand1 = npgsqlCommand;
					if (type == 0)
					{
						str = "username";
					}
					else
					{
						str = (type == 1 ? "nickname" : "player_id");
					}
					npgsqlCommand1.CommandText = string.Concat("SELECT * FROM accounts WHERE ", str, "=@value");
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						account = new Account()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("was a problem loading accounts! ", exception.Message), LoggerType.Error, exception);
			}
			return account;
		}

		public static void GetFriendlyAccounts(PlayerFriends System)
		{
			if (System == null || System.Friends.Count == 0)
			{
				return;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					string str = "";
					List<string> strs = new List<string>();
					for (int i = 0; i < System.Friends.Count; i++)
					{
						FriendModel ıtem = System.Friends[i];
						string str1 = string.Concat("@valor", i.ToString());
						npgsqlCommand.get_Parameters().AddWithValue(str1, ıtem.PlayerId);
						strs.Add(str1);
					}
					str = string.Join(",", strs.ToArray());
					npgsqlCommand.CommandText = string.Concat("SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (", str, ") ORDER BY player_id");
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						FriendModel friend = System.GetFriend(long.Parse(npgsqlDataReader["player_id"].ToString()));
						if (friend == null)
						{
							continue;
						}
						friend.Info.Nickname = npgsqlDataReader["nickname"].ToString();
						friend.Info.Rank = int.Parse(npgsqlDataReader["rank"].ToString());
						friend.Info.IsOnline = bool.Parse(npgsqlDataReader["online"].ToString());
						friend.Info.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), friend.PlayerId);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("was a problem loading (FriendlyAccounts); ", exception.Message), LoggerType.Error, exception);
			}
		}

		public static bool UpdatePlayerName(string name, long playerId)
		{
			return ComDiv.UpdateDB("accounts", "nickname", name, "player_id", playerId);
		}
	}
}