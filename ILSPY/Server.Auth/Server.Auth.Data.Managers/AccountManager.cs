using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Net.NetworkInformation;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Managers;

public class AccountManager
{
	public static SortedList<long, Account> Accounts = new SortedList<long, Account>();

	public static bool AddAccount(Account acc)
	{
		lock (Accounts)
		{
			if (!Accounts.ContainsKey(acc.PlayerId))
			{
				Accounts.Add(acc.PlayerId, acc);
				return true;
			}
		}
		return false;
	}

	public static Account GetAccountDB(object valor, object valor2, int type, int searchFlag)
	{
		if ((type == 0 && (string)valor == "") || (type == 1 && (long)valor == 0L) || (type == 2 && (string.IsNullOrEmpty((string)valor) || string.IsNullOrEmpty((string)valor2))))
		{
			return null;
		}
		Account account = null;
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@valor", valor);
				switch (type)
				{
				case 0:
					((DbCommand)(object)val2).CommandText = "SELECT * FROM accounts WHERE username=@valor LIMIT 1";
					break;
				case 1:
					((DbCommand)(object)val2).CommandText = "SELECT * FROM accounts WHERE player_id=@valor LIMIT 1";
					break;
				case 2:
					val2.Parameters.AddWithValue("@valor2", valor2);
					((DbCommand)(object)val2).CommandText = "SELECT * FROM accounts WHERE username=@valor AND password=@valor2 LIMIT 1";
					break;
				}
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					account = new Account
					{
						Username = ((DbDataReader)(object)val3)["username"].ToString(),
						Password = ((DbDataReader)(object)val3)["password"].ToString()
					};
					account.SetPlayerId(long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()), searchFlag);
					account.Email = ((DbDataReader)(object)val3)["email"].ToString();
					account.Age = int.Parse(((DbDataReader)(object)val3)["age"].ToString());
					account.MacAddress = (PhysicalAddress)((DbDataReader)(object)val3).GetValue(6);
					account.Nickname = ((DbDataReader)(object)val3)["nickname"].ToString();
					account.NickColor = int.Parse(((DbDataReader)(object)val3)["nick_color"].ToString());
					account.Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString());
					account.Exp = int.Parse(((DbDataReader)(object)val3)["experience"].ToString());
					account.Gold = int.Parse(((DbDataReader)(object)val3)["gold"].ToString());
					account.Cash = int.Parse(((DbDataReader)(object)val3)["cash"].ToString());
					account.CafePC = (CafeEnum)int.Parse(((DbDataReader)(object)val3)["pc_cafe"].ToString());
					account.Access = (AccessLevel)int.Parse(((DbDataReader)(object)val3)["access_level"].ToString());
					account.IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString());
					account.ClanId = int.Parse(((DbDataReader)(object)val3)["clan_id"].ToString());
					account.ClanAccess = int.Parse(((DbDataReader)(object)val3)["clan_access"].ToString());
					account.Effects = (CouponEffects)long.Parse(((DbDataReader)(object)val3)["coupon_effect"].ToString());
					account.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), account.PlayerId);
					account.LastRankUpDate = uint.Parse(((DbDataReader)(object)val3)["last_rank_update"].ToString());
					account.BanObjectId = long.Parse(((DbDataReader)(object)val3)["ban_object_id"].ToString());
					account.Ribbon = int.Parse(((DbDataReader)(object)val3)["ribbon"].ToString());
					account.Ensign = int.Parse(((DbDataReader)(object)val3)["ensign"].ToString());
					account.Medal = int.Parse(((DbDataReader)(object)val3)["medal"].ToString());
					account.MasterMedal = int.Parse(((DbDataReader)(object)val3)["master_medal"].ToString());
					account.Mission.Mission1 = int.Parse(((DbDataReader)(object)val3)["mission_id1"].ToString());
					account.Mission.Mission2 = int.Parse(((DbDataReader)(object)val3)["mission_id2"].ToString());
					account.Mission.Mission3 = int.Parse(((DbDataReader)(object)val3)["mission_id3"].ToString());
					account.Tags = int.Parse(((DbDataReader)(object)val3)["tags"].ToString());
					account.InventoryPlus = int.Parse(((DbDataReader)(object)val3)["inventory_plus"].ToString());
					account.Country = (CountryFlags)int.Parse(((DbDataReader)(object)val3)["country_flag"].ToString());
					if (AddAccount(account) && account.IsOnline)
					{
						account.SetOnlineStatus(Online: false);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return account;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("was a problem loading accounts! " + ex.Message, LoggerType.Error, ex);
			return account;
		}
	}

	public static void GetFriendlyAccounts(PlayerFriends system)
	{
		if (system == null || system.Friends.Count == 0)
		{
			return;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				string text = "";
				List<string> list = new List<string>();
				for (int i = 0; i < system.Friends.Count; i++)
				{
					FriendModel friendModel = system.Friends[i];
					string text2 = "@valor" + i;
					val2.Parameters.AddWithValue(text2, (object)friendModel.PlayerId);
					list.Add(text2);
				}
				text = string.Join(",", list.ToArray());
				((DbCommand)(object)val2).CommandText = "SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (" + text + ") ORDER BY player_id";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					FriendModel friend = system.GetFriend(long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()));
					if (friend != null)
					{
						friend.Info.Nickname = ((DbDataReader)(object)val3)["nickname"].ToString();
						friend.Info.Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString());
						friend.Info.IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString());
						friend.Info.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), friend.PlayerId);
						if (friend.Info.IsOnline && !Accounts.ContainsKey(friend.PlayerId))
						{
							friend.Info.SetOnlineStatus(state: false);
							friend.Info.Status.ResetData(friend.PlayerId);
						}
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Dispose();
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
			CLogger.Print("was a problem loading (FriendAccounts)! " + ex.Message, LoggerType.Error, ex);
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
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				string text = "";
				List<string> list = new List<string>();
				int num = 0;
				while (true)
				{
					if (num < System.Friends.Count)
					{
						FriendModel friendModel = System.Friends[num];
						if (friendModel.State <= 0)
						{
							string text2 = "@valor" + num;
							val2.Parameters.AddWithValue(text2, (object)friendModel.PlayerId);
							list.Add(text2);
							num++;
							continue;
						}
						break;
					}
					text = string.Join(",", list.ToArray());
					if (text == "")
					{
						break;
					}
					((DbConnection)(object)val).Open();
					val2.Parameters.AddWithValue("@on", (object)isOnline);
					((DbCommand)(object)val2).CommandText = "SELECT nickname, player_id, rank, status FROM accounts WHERE player_id in (" + text + ") AND online=@on ORDER BY player_id";
					NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
					while (((DbDataReader)(object)val3).Read())
					{
						FriendModel friend = System.GetFriend(long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()));
						if (friend != null)
						{
							friend.Info.Nickname = ((DbDataReader)(object)val3)["nickname"].ToString();
							friend.Info.Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString());
							friend.Info.IsOnline = isOnline;
							friend.Info.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), friend.PlayerId);
							if (isOnline && !Accounts.ContainsKey(friend.PlayerId))
							{
								friend.Info.SetOnlineStatus(state: false);
								friend.Info.Status.ResetData(friend.PlayerId);
							}
						}
					}
					((Component)(object)val2).Dispose();
					((DbDataReader)(object)val3).Dispose();
					((DbDataReader)(object)val3).Close();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
					break;
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("was a problem loading (FriendAccounts2)! " + ex.Message, LoggerType.Error, ex);
		}
	}

	public static Account GetAccount(long id, int searchFlag)
	{
		if (id == 0L)
		{
			return null;
		}
		try
		{
			Account value;
			return Accounts.TryGetValue(id, out value) ? value : GetAccountDB(id, null, 1, searchFlag);
		}
		catch
		{
			return null;
		}
	}

	public static Account GetAccount(long id, bool noUseDB)
	{
		if (id == 0L)
		{
			return null;
		}
		try
		{
			Account value;
			return Accounts.TryGetValue(id, out value) ? value : (noUseDB ? null : GetAccountDB(id, null, 1, 6175));
		}
		catch
		{
			return null;
		}
	}

	public static bool CreateAccount(out Account Player, string Username, string Password)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@login", (object)Username);
				val2.Parameters.AddWithValue("@pass", (object)Password);
				((DbCommand)(object)val2).CommandText = "INSERT INTO accounts (username, password, country_flag) VALUES (@login, @pass, 0)";
				((DbCommand)(object)val2).ExecuteNonQuery();
				((DbCommand)(object)val2).CommandText = "SELECT * FROM accounts WHERE username=@login";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				Account account = new Account();
				while (((DbDataReader)(object)val3).Read())
				{
					account.Username = ((DbDataReader)(object)val3)["username"].ToString();
					account.Password = ((DbDataReader)(object)val3)["password"].ToString();
					account.SetPlayerId(long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()), 95);
					account.Email = ((DbDataReader)(object)val3)["email"].ToString();
					account.Age = int.Parse(((DbDataReader)(object)val3)["age"].ToString());
					account.MacAddress = (PhysicalAddress)((DbDataReader)(object)val3).GetValue(6);
					account.Nickname = ((DbDataReader)(object)val3)["nickname"].ToString();
					account.NickColor = int.Parse(((DbDataReader)(object)val3)["nick_color"].ToString());
					account.Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString());
					account.Exp = int.Parse(((DbDataReader)(object)val3)["experience"].ToString());
					account.Gold = int.Parse(((DbDataReader)(object)val3)["gold"].ToString());
					account.Cash = int.Parse(((DbDataReader)(object)val3)["cash"].ToString());
					account.CafePC = (CafeEnum)int.Parse(((DbDataReader)(object)val3)["pc_cafe"].ToString());
					account.Access = (AccessLevel)int.Parse(((DbDataReader)(object)val3)["access_level"].ToString());
					account.IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString());
					account.ClanId = int.Parse(((DbDataReader)(object)val3)["clan_id"].ToString());
					account.ClanAccess = int.Parse(((DbDataReader)(object)val3)["clan_access"].ToString());
					account.Effects = (CouponEffects)long.Parse(((DbDataReader)(object)val3)["coupon_effect"].ToString());
					account.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), account.PlayerId);
					account.LastRankUpDate = uint.Parse(((DbDataReader)(object)val3)["last_rank_update"].ToString());
					account.BanObjectId = long.Parse(((DbDataReader)(object)val3)["ban_object_id"].ToString());
					account.Ribbon = int.Parse(((DbDataReader)(object)val3)["ribbon"].ToString());
					account.Ensign = int.Parse(((DbDataReader)(object)val3)["ensign"].ToString());
					account.Medal = int.Parse(((DbDataReader)(object)val3)["medal"].ToString());
					account.MasterMedal = int.Parse(((DbDataReader)(object)val3)["master_medal"].ToString());
					account.Mission.Mission1 = int.Parse(((DbDataReader)(object)val3)["mission_id1"].ToString());
					account.Mission.Mission2 = int.Parse(((DbDataReader)(object)val3)["mission_id2"].ToString());
					account.Mission.Mission3 = int.Parse(((DbDataReader)(object)val3)["mission_id3"].ToString());
					account.Tags = int.Parse(((DbDataReader)(object)val3)["tags"].ToString());
					account.InventoryPlus = int.Parse(((DbDataReader)(object)val3)["inventory_plus"].ToString());
					account.Country = (CountryFlags)int.Parse(((DbDataReader)(object)val3)["country_flag"].ToString());
				}
				Player = account;
				AddAccount(account);
				((Component)(object)val2).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return true;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("[AccountManager.CreateAccount] " + ex.Message, LoggerType.Error, ex);
			Player = null;
			return false;
		}
	}
}
