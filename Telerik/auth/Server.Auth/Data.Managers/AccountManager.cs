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
using System.Net.NetworkInformation;

namespace Server.Auth.Data.Managers
{
	public class AccountManager
	{
		public static SortedList<long, Account> Accounts;

		static AccountManager()
		{
			AccountManager.Accounts = new SortedList<long, Account>();
		}

		public AccountManager()
		{
		}

		public static bool AddAccount(Account acc)
		{
			bool flag;
			lock (AccountManager.Accounts)
			{
				if (AccountManager.Accounts.ContainsKey(acc.PlayerId))
				{
					return false;
				}
				else
				{
					AccountManager.Accounts.Add(acc.PlayerId, acc);
					flag = true;
				}
			}
			return flag;
		}

		public static bool CreateAccount(out Account Player, string Username, string Password)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@login", Username);
					npgsqlCommand.get_Parameters().AddWithValue("@pass", Password);
					npgsqlCommand.CommandText = "INSERT INTO accounts (username, password, country_flag) VALUES (@login, @pass, 0)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.CommandText = "SELECT * FROM accounts WHERE username=@login";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					Account account = new Account();
					while (npgsqlDataReader.Read())
					{
						account.Username = npgsqlDataReader["username"].ToString();
						account.Password = npgsqlDataReader["password"].ToString();
						account.SetPlayerId(long.Parse(npgsqlDataReader["player_id"].ToString()), 95);
						account.Email = npgsqlDataReader["email"].ToString();
						account.Age = int.Parse(npgsqlDataReader["age"].ToString());
						account.MacAddress = (PhysicalAddress)npgsqlDataReader.GetValue(6);
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
					}
					Player = account;
					AccountManager.AddAccount(account);
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AccountManager.CreateAccount] ", exception.Message), LoggerType.Error, exception);
				Player = null;
				flag = false;
			}
			return flag;
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
				account1 = (AccountManager.Accounts.TryGetValue(id, out account) ? account : AccountManager.GetAccountDB(id, null, 1, searchFlag));
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
					accountDB = AccountManager.GetAccountDB(id, null, 1, 6175);
				}
				account1 = accountDB;
			}
			catch
			{
				account1 = null;
			}
			return account1;
		}

		public static Account GetAccountDB(object valor, object valor2, int type, int searchFlag)
		{
			if (type == 0 && (string)valor == "" || type == 1 && (long)valor == 0 || type == 2 && (string.IsNullOrEmpty((string)valor) || string.IsNullOrEmpty((string)valor2)))
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
					npgsqlCommand.get_Parameters().AddWithValue("@valor", valor);
					if (type == 0)
					{
						npgsqlCommand.CommandText = "SELECT * FROM accounts WHERE username=@valor LIMIT 1";
					}
					else if (type == 1)
					{
						npgsqlCommand.CommandText = "SELECT * FROM accounts WHERE player_id=@valor LIMIT 1";
					}
					else if (type == 2)
					{
						npgsqlCommand.get_Parameters().AddWithValue("@valor2", valor2);
						npgsqlCommand.CommandText = "SELECT * FROM accounts WHERE username=@valor AND password=@valor2 LIMIT 1";
					}
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						account = new Account()
						{
							Username = npgsqlDataReader["username"].ToString(),
							Password = npgsqlDataReader["password"].ToString()
						};
						account.SetPlayerId(long.Parse(npgsqlDataReader["player_id"].ToString()), searchFlag);
						account.Email = npgsqlDataReader["email"].ToString();
						account.Age = int.Parse(npgsqlDataReader["age"].ToString());
						account.MacAddress = (PhysicalAddress)npgsqlDataReader.GetValue(6);
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
						if (!AccountManager.AddAccount(account) || !account.IsOnline)
						{
							continue;
						}
						account.SetOnlineStatus(false);
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
				CLogger.Print(string.Concat("was a problem loading accounts! ", exception.Message), LoggerType.Error, exception);
			}
			return account;
		}

		public static void GetFriendlyAccounts(PlayerFriends system)
		{
			if (system == null || system.Friends.Count == 0)
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
					for (int i = 0; i < system.Friends.Count; i++)
					{
						FriendModel ıtem = system.Friends[i];
						string str1 = string.Concat("@valor", i.ToString());
						npgsqlCommand.get_Parameters().AddWithValue(str1, ıtem.PlayerId);
						strs.Add(str1);
					}
					str = string.Join(",", strs.ToArray());
					npgsqlCommand.CommandText = string.Concat("SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (", str, ") ORDER BY player_id");
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						FriendModel friend = system.GetFriend(long.Parse(npgsqlDataReader["player_id"].ToString()));
						if (friend == null)
						{
							continue;
						}
						friend.Info.Nickname = npgsqlDataReader["nickname"].ToString();
						friend.Info.Rank = int.Parse(npgsqlDataReader["rank"].ToString());
						friend.Info.IsOnline = bool.Parse(npgsqlDataReader["online"].ToString());
						friend.Info.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), friend.PlayerId);
						if (!friend.Info.IsOnline || AccountManager.Accounts.ContainsKey(friend.PlayerId))
						{
							continue;
						}
						friend.Info.SetOnlineStatus(false);
						friend.Info.Status.ResetData(friend.PlayerId);
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
				CLogger.Print(string.Concat("was a problem loading (FriendAccounts)! ", exception.Message), LoggerType.Error, exception);
			}
		}

		public static void GetFriendlyAccounts(PlayerFriends System, bool isOnline)
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
					string str = "";
					List<string> strs = new List<string>();
					int ınt32 = 0;
					while (ınt32 < System.Friends.Count)
					{
						FriendModel ıtem = System.Friends[ınt32];
						if (ıtem.State > 0)
						{
							return;
						}
						else
						{
							string str1 = string.Concat("@valor", ınt32.ToString());
							npgsqlCommand.get_Parameters().AddWithValue(str1, ıtem.PlayerId);
							strs.Add(str1);
							ınt32++;
						}
					}
					str = string.Join(",", strs.ToArray());
					if (str != "")
					{
						npgsqlConnection.Open();
						npgsqlCommand.get_Parameters().AddWithValue("@on", isOnline);
						npgsqlCommand.CommandText = string.Concat("SELECT nickname, player_id, rank, status FROM accounts WHERE player_id in (", str, ") AND online=@on ORDER BY player_id");
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
							friend.Info.IsOnline = isOnline;
							friend.Info.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), friend.PlayerId);
							if (!isOnline || AccountManager.Accounts.ContainsKey(friend.PlayerId))
							{
								continue;
							}
							friend.Info.SetOnlineStatus(false);
							friend.Info.Status.ResetData(friend.PlayerId);
						}
						npgsqlCommand.Dispose();
						npgsqlDataReader.Dispose();
						npgsqlDataReader.Close();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
					else
					{
						return;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("was a problem loading (FriendAccounts2)! ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}