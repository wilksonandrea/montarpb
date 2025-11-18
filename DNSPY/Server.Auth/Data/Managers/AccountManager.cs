using System;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Managers
{
	// Token: 0x02000060 RID: 96
	public class AccountManager
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000C264 File Offset: 0x0000A464
		public static bool AddAccount(Account acc)
		{
			SortedList<long, Account> accounts = AccountManager.Accounts;
			lock (accounts)
			{
				if (!AccountManager.Accounts.ContainsKey(acc.PlayerId))
				{
					AccountManager.Accounts.Add(acc.PlayerId, acc);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		public static Account GetAccountDB(object valor, object valor2, int type, int searchFlag)
		{
			if ((type == 0 && (string)valor == "") || (type == 1 && (long)valor == 0L) || (type == 2 && (string.IsNullOrEmpty((string)valor) || string.IsNullOrEmpty((string)valor2))))
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
					npgsqlCommand.Parameters.AddWithValue("@valor", valor);
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
						npgsqlCommand.Parameters.AddWithValue("@valor2", valor2);
						npgsqlCommand.CommandText = "SELECT * FROM accounts WHERE username=@valor AND password=@valor2 LIMIT 1";
					}
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						account = new Account
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
						if (AccountManager.AddAccount(account) && account.IsOnline)
						{
							account.SetOnlineStatus(false);
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
				CLogger.Print("was a problem loading accounts! " + ex.Message, LoggerType.Error, ex);
			}
			return account;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C77C File Offset: 0x0000A97C
		public static void GetFriendlyAccounts(PlayerFriends system)
		{
			if (system != null && system.Friends.Count != 0)
			{
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						List<string> list = new List<string>();
						for (int i = 0; i < system.Friends.Count; i++)
						{
							FriendModel friendModel = system.Friends[i];
							string text = "@valor" + i.ToString();
							npgsqlCommand.Parameters.AddWithValue(text, friendModel.PlayerId);
							list.Add(text);
						}
						string text2 = string.Join(",", list.ToArray());
						npgsqlCommand.CommandText = "SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in (" + text2 + ") ORDER BY player_id";
						NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
						while (npgsqlDataReader.Read())
						{
							FriendModel friend = system.GetFriend(long.Parse(npgsqlDataReader["player_id"].ToString()));
							if (friend != null)
							{
								friend.Info.Nickname = npgsqlDataReader["nickname"].ToString();
								friend.Info.Rank = int.Parse(npgsqlDataReader["rank"].ToString());
								friend.Info.IsOnline = bool.Parse(npgsqlDataReader["online"].ToString());
								friend.Info.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), friend.PlayerId);
								if (friend.Info.IsOnline && !AccountManager.Accounts.ContainsKey(friend.PlayerId))
								{
									friend.Info.SetOnlineStatus(false);
									friend.Info.Status.ResetData(friend.PlayerId);
								}
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
					CLogger.Print("was a problem loading (FriendAccounts)! " + ex.Message, LoggerType.Error, ex);
				}
				return;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public static void GetFriendlyAccounts(PlayerFriends System, bool isOnline)
		{
			if (System != null && System.Friends.Count != 0)
			{
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						List<string> list = new List<string>();
						for (int i = 0; i < System.Friends.Count; i++)
						{
							FriendModel friendModel = System.Friends[i];
							if (friendModel.State > 0)
							{
								return;
							}
							string text = "@valor" + i.ToString();
							npgsqlCommand.Parameters.AddWithValue(text, friendModel.PlayerId);
							list.Add(text);
						}
						string text2 = string.Join(",", list.ToArray());
						if (!(text2 == ""))
						{
							npgsqlConnection.Open();
							npgsqlCommand.Parameters.AddWithValue("@on", isOnline);
							npgsqlCommand.CommandText = "SELECT nickname, player_id, rank, status FROM accounts WHERE player_id in (" + text2 + ") AND online=@on ORDER BY player_id";
							NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
							while (npgsqlDataReader.Read())
							{
								FriendModel friend = System.GetFriend(long.Parse(npgsqlDataReader["player_id"].ToString()));
								if (friend != null)
								{
									friend.Info.Nickname = npgsqlDataReader["nickname"].ToString();
									friend.Info.Rank = int.Parse(npgsqlDataReader["rank"].ToString());
									friend.Info.IsOnline = isOnline;
									friend.Info.Status.SetData(uint.Parse(npgsqlDataReader["status"].ToString()), friend.PlayerId);
									if (isOnline && !AccountManager.Accounts.ContainsKey(friend.PlayerId))
									{
										friend.Info.SetOnlineStatus(false);
										friend.Info.Status.ResetData(friend.PlayerId);
									}
								}
							}
							npgsqlCommand.Dispose();
							npgsqlDataReader.Dispose();
							npgsqlDataReader.Close();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
					}
				}
				catch (Exception ex)
				{
					CLogger.Print("was a problem loading (FriendAccounts2)! " + ex.Message, LoggerType.Error, ex);
				}
				return;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000CC5C File Offset: 0x0000AE5C
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
				account = (AccountManager.Accounts.TryGetValue(id, out account2) ? account2 : AccountManager.GetAccountDB(id, null, 1, searchFlag));
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
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
				account = (AccountManager.Accounts.TryGetValue(id, out account2) ? account2 : (noUseDB ? null : AccountManager.GetAccountDB(id, null, 1, 6175)));
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000CD00 File Offset: 0x0000AF00
		public static bool CreateAccount(out Account Player, string Username, string Password)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@login", Username);
					npgsqlCommand.Parameters.AddWithValue("@pass", Password);
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
			catch (Exception ex)
			{
				CLogger.Print("[AccountManager.CreateAccount] " + ex.Message, LoggerType.Error, ex);
				Player = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00002409 File Offset: 0x00000609
		public AccountManager()
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00002C98 File Offset: 0x00000E98
		// Note: this type is marked as 'beforefieldinit'.
		static AccountManager()
		{
		}

		// Token: 0x040000D5 RID: 213
		public static SortedList<long, Account> Accounts = new SortedList<long, Account>();
	}
}
