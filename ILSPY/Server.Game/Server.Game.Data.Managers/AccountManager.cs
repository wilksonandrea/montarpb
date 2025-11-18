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
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Data.Managers;

public static class AccountManager
{
	public static SortedList<long, Account> Accounts = new SortedList<long, Account>();

	public static void AddAccount(Account acc)
	{
		lock (Accounts)
		{
			if (!Accounts.ContainsKey(acc.PlayerId))
			{
				Accounts.Add(acc.PlayerId, acc);
			}
		}
	}

	public static Account GetAccountDB(object valor, int type, int searchDBFlag)
	{
		if ((type == 2 && (long)valor == 0L) || ((type == 0 || type == 1) && (string)valor == ""))
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
				val2.Parameters.AddWithValue("@value", valor);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM accounts WHERE " + type switch
				{
					1 => "nickname", 
					0 => "username", 
					_ => "player_id", 
				} + "=@value";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					account = new Account
					{
						Username = ((DbDataReader)(object)val3)["username"].ToString(),
						Password = ((DbDataReader)(object)val3)["password"].ToString()
					};
					account.SetPlayerId(long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()), searchDBFlag);
					account.Email = ((DbDataReader)(object)val3)["email"].ToString();
					account.Age = int.Parse(((DbDataReader)(object)val3)["age"].ToString());
					account.SetPublicIP(((DbDataReader)(object)val3).GetString(5));
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
					AddAccount(account);
				}
				((Component)(object)val2).Dispose();
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

	public static void GetFriendlyAccounts(PlayerFriends System)
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
				((DbConnection)(object)val).Open();
				string text = "";
				List<string> list = new List<string>();
				for (int i = 0; i < System.Friends.Count; i++)
				{
					FriendModel friendModel = System.Friends[i];
					string text2 = "@valor" + i;
					val2.Parameters.AddWithValue(text2, (object)friendModel.PlayerId);
					list.Add(text2);
				}
				text = string.Join(",", list.ToArray());
				((DbCommand)(object)val2).CommandText = "SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (" + text + ") ORDER BY player_id";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					FriendModel friend = System.GetFriend(long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()));
					if (friend != null)
					{
						friend.Info.Nickname = ((DbDataReader)(object)val3)["nickname"].ToString();
						friend.Info.Rank = int.Parse(((DbDataReader)(object)val3)["rank"].ToString());
						friend.Info.IsOnline = bool.Parse(((DbDataReader)(object)val3)["online"].ToString());
						friend.Info.Status.SetData(uint.Parse(((DbDataReader)(object)val3)["status"].ToString()), friend.PlayerId);
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
			CLogger.Print("was a problem loading (FriendlyAccounts); " + ex.Message, LoggerType.Error, ex);
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
			return Accounts.TryGetValue(id, out value) ? value : GetAccountDB(id, 2, searchFlag);
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
			return Accounts.TryGetValue(id, out value) ? value : (noUseDB ? null : GetAccountDB(id, 2, 7455));
		}
		catch
		{
			return null;
		}
	}

	public static Account GetAccount(string text, int type, int searchFlag)
	{
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		foreach (Account value in Accounts.Values)
		{
			if (value != null && ((type == 1 && value.Nickname == text && value.Nickname.Length > 0) || (type == 0 && string.Compare(value.Username, text) == 0)))
			{
				return value;
			}
		}
		return GetAccountDB(text, type, searchFlag);
	}

	public static bool UpdatePlayerName(string name, long playerId)
	{
		return ComDiv.UpdateDB("accounts", "nickname", name, "player_id", playerId);
	}
}
