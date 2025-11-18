using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.SQL
{
	// Token: 0x02000007 RID: 7
	public static class DaoManagerSQL
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00008C6C File Offset: 0x00006E6C
		public static List<ItemsModel> GetPlayerInventoryItems(long OwnerId)
		{
			List<ItemsModel> list = new List<ItemsModel>();
			if (OwnerId == 0L)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_items WHERE owner_id=@owner ORDER BY object_id ASC;";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						ItemsModel itemsModel = new ItemsModel(int.Parse(npgsqlDataReader["id"].ToString()))
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							Name = npgsqlDataReader["name"].ToString(),
							Count = uint.Parse(npgsqlDataReader["count"].ToString()),
							Equip = (ItemEquipType)int.Parse(npgsqlDataReader["equip"].ToString())
						};
						list.Add(itemsModel);
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

		// Token: 0x0600001D RID: 29 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public static bool CreatePlayerInventoryItem(ItemsModel Item, long OwnerId)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@itmId", Item.Id);
					npgsqlCommand.Parameters.AddWithValue("@ItmNm", Item.Name);
					npgsqlCommand.Parameters.AddWithValue("@count", (long)((ulong)Item.Count));
					npgsqlCommand.Parameters.AddWithValue("@equip", (int)Item.Equip);
					npgsqlCommand.CommandText = "INSERT INTO player_items(owner_id, id, name, count, equip) VALUES(@owner, @itmId, @ItmNm, @count, @equip) RETURNING object_id";
					object obj = npgsqlCommand.ExecuteScalar();
					Item.ObjectId = ((Item.Equip != ItemEquipType.Permanent) ? ((long)obj) : Item.ObjectId);
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002148 File Offset: 0x00000348
		public static bool DeletePlayerInventoryItem(long ObjectId, long OwnerId)
		{
			return ObjectId != 0L && OwnerId != 0L && ComDiv.DeleteDB("player_items", "object_id", ObjectId, "owner_id", OwnerId);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00008F08 File Offset: 0x00007108
		public static BanHistory GetAccountBan(long ObjectId)
		{
			BanHistory banHistory = new BanHistory();
			if (ObjectId == 0L)
			{
				return banHistory;
			}
			BanHistory banHistory2;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@obj", ObjectId);
					npgsqlCommand.CommandText = "SELECT * FROM base_ban_history WHERE object_id=@obj";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						banHistory.ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString());
						banHistory.PlayerId = long.Parse(npgsqlDataReader["owner_id"].ToString());
						banHistory.Type = npgsqlDataReader["type"].ToString();
						banHistory.Value = npgsqlDataReader["value"].ToString();
						banHistory.Reason = npgsqlDataReader["reason"].ToString();
						banHistory.StartDate = DateTime.Parse(npgsqlDataReader["start_date"].ToString());
						banHistory.EndDate = DateTime.Parse(npgsqlDataReader["expire_date"].ToString());
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				return banHistory;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				banHistory2 = null;
			}
			return banHistory2;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00009098 File Offset: 0x00007298
		public static List<string> GetHwIdList()
		{
			List<string> list = new List<string>();
			List<string> list2;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandText = "SELECT * FROM base_ban_hwid";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						string text = npgsqlDataReader["hardware_id"].ToString();
						if (text != null || text.Length != 0)
						{
							list.Add(text);
						}
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				return list;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				list2 = null;
			}
			return list2;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00009164 File Offset: 0x00007364
		public static void GetBanStatus(string MAC, string IP4, out bool ValidMac, out bool ValidIp4)
		{
			ValidMac = false;
			ValidIp4 = false;
			try
			{
				DateTime dateTime = DateTimeUtil.Now();
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@mac", MAC);
					npgsqlCommand.Parameters.AddWithValue("@ip", IP4);
					npgsqlCommand.CommandText = "SELECT * FROM base_ban_history WHERE value in (@mac, @ip)";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						string text = npgsqlDataReader["type"].ToString();
						string text2 = npgsqlDataReader["value"].ToString();
						if (!(DateTime.Parse(npgsqlDataReader["expire_date"].ToString()) < dateTime))
						{
							if (text == "MAC" && text2 == MAC)
							{
								ValidMac = true;
							}
							else if (text == "IP4" && text2 == IP4)
							{
								ValidIp4 = true;
							}
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

		// Token: 0x06000022 RID: 34 RVA: 0x000092C0 File Offset: 0x000074C0
		public static BanHistory SaveBanHistory(long PlayerId, string Type, string Value, DateTime EndDate)
		{
			BanHistory banHistory = new BanHistory
			{
				PlayerId = PlayerId,
				Type = Type,
				Value = Value,
				EndDate = EndDate
			};
			BanHistory banHistory2;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@provider", banHistory.PlayerId);
					npgsqlCommand.Parameters.AddWithValue("@type", banHistory.Type);
					npgsqlCommand.Parameters.AddWithValue("@value", banHistory.Value);
					npgsqlCommand.Parameters.AddWithValue("@reason", banHistory.Reason);
					npgsqlCommand.Parameters.AddWithValue("@start", banHistory.StartDate);
					npgsqlCommand.Parameters.AddWithValue("@end", banHistory.EndDate);
					npgsqlCommand.CommandText = "INSERT INTO base_ban_history(owner_id, type, value, reason, start_date, expire_date) VALUES(@provider, @type, @value, @reason, @start, @end) RETURNING object_id";
					object obj = npgsqlCommand.ExecuteScalar();
					banHistory.ObjectId = (long)obj;
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					banHistory2 = banHistory;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				banHistory2 = null;
			}
			return banHistory2;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00009410 File Offset: 0x00007610
		public static bool SaveAutoBan(long PlayerId, string Username, string Nickname, string Type, string Time, string Address, string HackType)
		{
			if (PlayerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@player_id", PlayerId);
					npgsqlCommand.Parameters.AddWithValue("@login", Username);
					npgsqlCommand.Parameters.AddWithValue("@player_name", Nickname);
					npgsqlCommand.Parameters.AddWithValue("@type", Type);
					npgsqlCommand.Parameters.AddWithValue("@time", Time);
					npgsqlCommand.Parameters.AddWithValue("@ip", Address);
					npgsqlCommand.Parameters.AddWithValue("@hack_type", HackType);
					npgsqlCommand.CommandText = "INSERT INTO base_auto_ban(owner_id, username, nickname, type, time, ip4_address, hack_type) VALUES(@player_id, @login, @player_name, @type, @time, @ip, @hack_type)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002172 File Offset: 0x00000372
		public static bool SaveBanReason(long ObjectId, string Reason)
		{
			return ObjectId != 0L && ComDiv.UpdateDB("base_ban_history", "reason", Reason, "object_id", ObjectId);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00009524 File Offset: 0x00007724
		public static bool CreateClan(out int ClanId, string Name, long OwnerId, string ClanInfo, uint CreateDate)
		{
			bool flag;
			try
			{
				ClanId = -1;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@name", Name);
					npgsqlCommand.Parameters.AddWithValue("@date", (long)((ulong)CreateDate));
					npgsqlCommand.Parameters.AddWithValue("@info", ClanInfo);
					npgsqlCommand.Parameters.AddWithValue("@best", "0-0");
					npgsqlCommand.CommandText = "INSERT INTO system_clan (name, owner_id, create_date, info, best_exp, best_participants, best_wins, best_kills, best_headshots) VALUES (@name, @owner, @date, @info, @best, @best, @best, @best, @best) RETURNING id";
					object obj = npgsqlCommand.ExecuteScalar();
					ClanId = (int)obj;
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				ClanId = -1;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000962C File Offset: 0x0000782C
		public static bool UpdateClanInfo(int ClanId, int Authority, int RankLimit, int MinAge, int MaxAge, int JoinType)
		{
			if (ClanId == 0)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@ClanId", ClanId);
					npgsqlCommand.Parameters.AddWithValue("@Authority", Authority);
					npgsqlCommand.Parameters.AddWithValue("@RankLimit", RankLimit);
					npgsqlCommand.Parameters.AddWithValue("@MinAge", MinAge);
					npgsqlCommand.Parameters.AddWithValue("@MaxAge", MaxAge);
					npgsqlCommand.Parameters.AddWithValue("@JoinType", JoinType);
					npgsqlCommand.CommandText = "UPDATE system_clan SET authority=@Authority, rank_limit=@RankLimit, min_age_limit=@MinAge, max_age_limit=@MaxAge, join_permission=@JoinType WHERE id=@ClanId";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000974C File Offset: 0x0000794C
		public static void UpdateClanBestPlayers(ClanModel Clan)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", Clan.Id);
					npgsqlCommand.Parameters.AddWithValue("@bp1", Clan.BestPlayers.Exp.GetSplit());
					npgsqlCommand.Parameters.AddWithValue("@bp2", Clan.BestPlayers.Participation.GetSplit());
					npgsqlCommand.Parameters.AddWithValue("@bp3", Clan.BestPlayers.Wins.GetSplit());
					npgsqlCommand.Parameters.AddWithValue("@bp4", Clan.BestPlayers.Kills.GetSplit());
					npgsqlCommand.Parameters.AddWithValue("@bp5", Clan.BestPlayers.Headshots.GetSplit());
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "UPDATE system_clan SET best_exp=@bp1, best_participants=@bp2, best_wins=@bp3, best_kills=@bp4, best_headshots=@bp5 WHERE id=@id";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002194 File Offset: 0x00000394
		public static bool UpdateClanLogo(int ClanId, uint logo)
		{
			return ClanId != 0 && ComDiv.UpdateDB("system_clan", "logo", (long)((ulong)logo), "id", ClanId);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000021BC File Offset: 0x000003BC
		public static bool UpdateClanPoints(int ClanId, float Gold)
		{
			return ClanId != 0 && ComDiv.UpdateDB("system_clan", "gold", Gold, "id", ClanId);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000021E3 File Offset: 0x000003E3
		public static bool UpdateClanExp(int ClanId, int Exp)
		{
			return ClanId != 0 && ComDiv.UpdateDB("system_clan", "exp", Exp, "id", ClanId);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000220A File Offset: 0x0000040A
		public static bool UpdateClanRank(int ClanId, int Rank)
		{
			return ClanId != 0 && ComDiv.UpdateDB("system_clan", "rank", Rank, "id", ClanId);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000098B0 File Offset: 0x00007AB0
		public static bool UpdateClanBattles(int ClanId, int Matches, int Wins, int Loses)
		{
			if (ClanId == 0)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@clan", ClanId);
					npgsqlCommand.Parameters.AddWithValue("@partidas", Matches);
					npgsqlCommand.Parameters.AddWithValue("@vitorias", Wins);
					npgsqlCommand.Parameters.AddWithValue("@derrotas", Loses);
					npgsqlCommand.CommandText = "UPDATE system_clan SET matches=@partidas, match_wins=@vitorias, match_loses=@derrotas WHERE id=@clan";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000999C File Offset: 0x00007B9C
		public static int GetClanPlayers(int ClanId)
		{
			int num = 0;
			if (ClanId == 0)
			{
				return num;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM accounts WHERE clan_id=@clan";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00009A44 File Offset: 0x00007C44
		public static MessageModel GetMessage(long ObjectId, long PlayerId)
		{
			MessageModel messageModel = null;
			if (ObjectId != 0L && PlayerId != 0L)
			{
				MessageModel messageModel2;
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@obj", ObjectId);
						npgsqlCommand.Parameters.AddWithValue("@owner", PlayerId);
						npgsqlCommand.CommandText = "SELECT * FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
						npgsqlCommand.CommandType = CommandType.Text;
						NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
						while (npgsqlDataReader.Read())
						{
							messageModel = new MessageModel((long)((ulong)uint.Parse(npgsqlDataReader["expire_date"].ToString())), DateTimeUtil.Now())
							{
								ObjectId = ObjectId,
								SenderId = long.Parse(npgsqlDataReader["sender_id"].ToString()),
								SenderName = npgsqlDataReader["sender_name"].ToString(),
								ClanId = int.Parse(npgsqlDataReader["clan_id"].ToString()),
								ClanNote = (NoteMessageClan)int.Parse(npgsqlDataReader["clan_note"].ToString()),
								Text = npgsqlDataReader["text"].ToString(),
								Type = (NoteMessageType)int.Parse(npgsqlDataReader["type"].ToString()),
								State = (NoteMessageState)int.Parse(npgsqlDataReader["state"].ToString())
							};
						}
						npgsqlCommand.Dispose();
						npgsqlDataReader.Close();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
					return messageModel;
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
					messageModel2 = null;
				}
				return messageModel2;
			}
			return messageModel;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00009C24 File Offset: 0x00007E24
		public static List<MessageModel> GetGiftMessages(long OwnerId)
		{
			List<MessageModel> list = new List<MessageModel>();
			if (OwnerId == 0L)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						NoteMessageType noteMessageType = (NoteMessageType)int.Parse(npgsqlDataReader["type"].ToString());
						if (noteMessageType == NoteMessageType.Gift)
						{
							MessageModel messageModel = new MessageModel((long)((ulong)uint.Parse(npgsqlDataReader["expire_date"].ToString())), DateTimeUtil.Now())
							{
								ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
								SenderId = long.Parse(npgsqlDataReader["sender_id"].ToString()),
								SenderName = npgsqlDataReader["sender_name"].ToString(),
								ClanId = int.Parse(npgsqlDataReader["clan_id"].ToString()),
								ClanNote = (NoteMessageClan)int.Parse(npgsqlDataReader["clan_note"].ToString()),
								Text = npgsqlDataReader["text"].ToString(),
								Type = noteMessageType,
								State = (NoteMessageState)int.Parse(npgsqlDataReader["state"].ToString())
							};
							list.Add(messageModel);
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

		// Token: 0x06000030 RID: 48 RVA: 0x00009E08 File Offset: 0x00008008
		public static List<MessageModel> GetMessages(long OwnerId)
		{
			List<MessageModel> list = new List<MessageModel>();
			if (OwnerId == 0L)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						NoteMessageType noteMessageType = (NoteMessageType)int.Parse(npgsqlDataReader["type"].ToString());
						if (noteMessageType != NoteMessageType.Gift)
						{
							MessageModel messageModel = new MessageModel((long)((ulong)uint.Parse(npgsqlDataReader["expire_date"].ToString())), DateTimeUtil.Now())
							{
								ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
								SenderId = long.Parse(npgsqlDataReader["sender_id"].ToString()),
								SenderName = npgsqlDataReader["sender_name"].ToString(),
								ClanId = int.Parse(npgsqlDataReader["clan_id"].ToString()),
								ClanNote = (NoteMessageClan)int.Parse(npgsqlDataReader["clan_note"].ToString()),
								Text = npgsqlDataReader["text"].ToString(),
								Type = noteMessageType,
								State = (NoteMessageState)int.Parse(npgsqlDataReader["state"].ToString())
							};
							list.Add(messageModel);
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

		// Token: 0x06000031 RID: 49 RVA: 0x00009FEC File Offset: 0x000081EC
		public static bool MessageExists(long ObjectId, long OwnerId)
		{
			if (ObjectId != 0L && OwnerId != 0L)
			{
				try
				{
					int num = 0;
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@obj", ObjectId);
						npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
						npgsqlCommand.CommandText = "SELECT COUNT(*) FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
						num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
					return num > 0;
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000A0BC File Offset: 0x000082BC
		public static int GetMessagesCount(long OwnerId)
		{
			int num = 0;
			if (OwnerId == 0L)
			{
				return num;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000A164 File Offset: 0x00008364
		public static bool CreateMessage(long OwnerId, MessageModel Message)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@sendid", Message.SenderId);
					npgsqlCommand.Parameters.AddWithValue("@clan", Message.ClanId);
					npgsqlCommand.Parameters.AddWithValue("@sendname", Message.SenderName);
					npgsqlCommand.Parameters.AddWithValue("@text", Message.Text);
					npgsqlCommand.Parameters.AddWithValue("@type", (int)Message.Type);
					npgsqlCommand.Parameters.AddWithValue("@state", (int)Message.State);
					npgsqlCommand.Parameters.AddWithValue("@expire", Message.ExpireDate);
					npgsqlCommand.Parameters.AddWithValue("@cb", (int)Message.ClanNote);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire_date) VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
					object obj = npgsqlCommand.ExecuteScalar();
					Message.ObjectId = (long)obj;
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002231 File Offset: 0x00000431
		public static void UpdateState(long ObjectId, long OwnerId, int Value)
		{
			ComDiv.UpdateDB("player_messages", "state", Value, "object_id", ObjectId, "owner_id", OwnerId);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000225F File Offset: 0x0000045F
		public static void UpdateExpireDate(long ObjectId, long OwnerId, uint Date)
		{
			ComDiv.UpdateDB("player_messages", "expire_date", (long)((ulong)Date), "object_id", ObjectId, "owner_id", OwnerId);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000228E File Offset: 0x0000048E
		public static bool DeleteMessage(long ObjectId, long OwnerId)
		{
			return ObjectId != 0L && OwnerId != 0L && ComDiv.DeleteDB("player_messages", "object_id", ObjectId, "owner_id", OwnerId);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000022B8 File Offset: 0x000004B8
		public static bool DeleteMessages(List<object> ObjectIds, long OwnerId)
		{
			return ObjectIds.Count != 0 && OwnerId != 0L && ComDiv.DeleteDB("player_messages", "object_id", ObjectIds.ToArray(), "owner_id", OwnerId);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000A300 File Offset: 0x00008500
		public static void RecycleMessages(long OwnerId, List<MessageModel> Messages)
		{
			List<object> list = new List<object>();
			for (int i = 0; i < Messages.Count; i++)
			{
				MessageModel messageModel = Messages[i];
				if (messageModel.DaysRemaining == 0)
				{
					list.Add(messageModel.ObjectId);
					Messages.RemoveAt(i--);
				}
			}
			DaoManagerSQL.DeleteMessages(list, OwnerId);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000A358 File Offset: 0x00008558
		public static PlayerEquipment GetPlayerEquipmentsDB(long OwnerId)
		{
			PlayerEquipment playerEquipment = null;
			if (OwnerId == 0L)
			{
				return playerEquipment;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_equipments WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerEquipment = new PlayerEquipment
						{
							OwnerId = OwnerId,
							WeaponPrimary = int.Parse(npgsqlDataReader["weapon_primary"].ToString()),
							WeaponSecondary = int.Parse(npgsqlDataReader["weapon_secondary"].ToString()),
							WeaponMelee = int.Parse(npgsqlDataReader["weapon_melee"].ToString()),
							WeaponExplosive = int.Parse(npgsqlDataReader["weapon_explosive"].ToString()),
							WeaponSpecial = int.Parse(npgsqlDataReader["weapon_special"].ToString()),
							CharaRedId = int.Parse(npgsqlDataReader["chara_red_side"].ToString()),
							CharaBlueId = int.Parse(npgsqlDataReader["chara_blue_side"].ToString()),
							DinoItem = int.Parse(npgsqlDataReader["dino_item_chara"].ToString()),
							PartHead = int.Parse(npgsqlDataReader["part_head"].ToString()),
							PartFace = int.Parse(npgsqlDataReader["part_face"].ToString()),
							PartJacket = int.Parse(npgsqlDataReader["part_jacket"].ToString()),
							PartPocket = int.Parse(npgsqlDataReader["part_pocket"].ToString()),
							PartGlove = int.Parse(npgsqlDataReader["part_glove"].ToString()),
							PartBelt = int.Parse(npgsqlDataReader["part_belt"].ToString()),
							PartHolster = int.Parse(npgsqlDataReader["part_holster"].ToString()),
							PartSkin = int.Parse(npgsqlDataReader["part_skin"].ToString()),
							BeretItem = int.Parse(npgsqlDataReader["beret_item_part"].ToString()),
							AccessoryId = int.Parse(npgsqlDataReader["accesory_id"].ToString()),
							SprayId = int.Parse(npgsqlDataReader["spray_id"].ToString()),
							NameCardId = int.Parse(npgsqlDataReader["namecard_id"].ToString())
						};
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
			return playerEquipment;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000A660 File Offset: 0x00008860
		public static bool CreatePlayerEquipmentsDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_equipments(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000A70C File Offset: 0x0000890C
		public static List<CharacterModel> GetPlayerCharactersDB(long OwnerId)
		{
			List<CharacterModel> list = new List<CharacterModel>();
			if (OwnerId == 0L)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@OwnerId", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_characters WHERE owner_id=@OwnerId ORDER BY slot ASC;";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						CharacterModel characterModel = new CharacterModel
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							Id = int.Parse(npgsqlDataReader["id"].ToString()),
							Slot = int.Parse(npgsqlDataReader["slot"].ToString()),
							Name = npgsqlDataReader["name"].ToString(),
							CreateDate = uint.Parse(npgsqlDataReader["create_date"].ToString()),
							PlayTime = uint.Parse(npgsqlDataReader["playtime"].ToString())
						};
						list.Add(characterModel);
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

		// Token: 0x0600003C RID: 60 RVA: 0x0000A894 File Offset: 0x00008A94
		public static bool CreatePlayerCharacter(CharacterModel Chara, long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner_id", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@id", Chara.Id);
					npgsqlCommand.Parameters.AddWithValue("@slot", Chara.Slot);
					npgsqlCommand.Parameters.AddWithValue("@name", Chara.Name);
					npgsqlCommand.Parameters.AddWithValue("@createdate", (long)((ulong)Chara.CreateDate));
					npgsqlCommand.Parameters.AddWithValue("@playtime", (long)((ulong)Chara.PlayTime));
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO player_characters(owner_id, id, slot, name, create_date, playtime) VALUES(@owner_id, @id, @slot, @name, @createdate, @playtime) RETURNING object_id";
					object obj = npgsqlCommand.ExecuteScalar();
					Chara.ObjectId = (long)obj;
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		public static StatisticTotal GetPlayerStatBasicDB(long OwnerId)
		{
			StatisticTotal statisticTotal = null;
			if (OwnerId == 0L)
			{
				return statisticTotal;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_basics WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticTotal = new StatisticTotal
						{
							OwnerId = OwnerId,
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							MatchDraws = int.Parse(npgsqlDataReader["match_draws"].ToString()),
							KillsCount = int.Parse(npgsqlDataReader["kills_count"].ToString()),
							DeathsCount = int.Parse(npgsqlDataReader["deaths_count"].ToString()),
							HeadshotsCount = int.Parse(npgsqlDataReader["headshots_count"].ToString()),
							AssistsCount = int.Parse(npgsqlDataReader["assists_count"].ToString()),
							EscapesCount = int.Parse(npgsqlDataReader["escapes_count"].ToString()),
							MvpCount = int.Parse(npgsqlDataReader["mvp_count"].ToString()),
							TotalMatchesCount = int.Parse(npgsqlDataReader["total_matches"].ToString()),
							TotalKillsCount = int.Parse(npgsqlDataReader["total_kills"].ToString())
						};
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
			return statisticTotal;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000AC04 File Offset: 0x00008E04
		public static bool CreatePlayerStatBasicDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_basics(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public static StatisticSeason GetPlayerStatSeasonDB(long OwnerId)
		{
			StatisticSeason statisticSeason = null;
			if (OwnerId == 0L)
			{
				return statisticSeason;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_seasons WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticSeason = new StatisticSeason
						{
							OwnerId = OwnerId,
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							MatchDraws = int.Parse(npgsqlDataReader["match_draws"].ToString()),
							KillsCount = int.Parse(npgsqlDataReader["kills_count"].ToString()),
							DeathsCount = int.Parse(npgsqlDataReader["deaths_count"].ToString()),
							HeadshotsCount = int.Parse(npgsqlDataReader["headshots_count"].ToString()),
							AssistsCount = int.Parse(npgsqlDataReader["assists_count"].ToString()),
							EscapesCount = int.Parse(npgsqlDataReader["escapes_count"].ToString()),
							MvpCount = int.Parse(npgsqlDataReader["mvp_count"].ToString()),
							TotalMatchesCount = int.Parse(npgsqlDataReader["total_matches"].ToString()),
							TotalKillsCount = int.Parse(npgsqlDataReader["total_kills"].ToString())
						};
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
			return statisticSeason;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public static bool CreatePlayerStatSeasonDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_seasons(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000AF8C File Offset: 0x0000918C
		public static StatisticClan GetPlayerStatClanDB(long OwnerId)
		{
			StatisticClan statisticClan = null;
			if (OwnerId == 0L)
			{
				return statisticClan;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_clans WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticClan = new StatisticClan
						{
							OwnerId = OwnerId,
							Matches = int.Parse(npgsqlDataReader["clan_matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["clan_match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["clan_match_loses"].ToString())
						};
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
			return statisticClan;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public static bool CreatePlayerStatClanDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_clans(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000B154 File Offset: 0x00009354
		public static StatisticDaily GetPlayerStatDailiesDB(long OwnerId)
		{
			StatisticDaily statisticDaily = null;
			if (OwnerId == 0L)
			{
				return statisticDaily;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_dailies WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticDaily = new StatisticDaily
						{
							OwnerId = OwnerId,
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							MatchDraws = int.Parse(npgsqlDataReader["match_draws"].ToString()),
							KillsCount = int.Parse(npgsqlDataReader["kills_count"].ToString()),
							DeathsCount = int.Parse(npgsqlDataReader["deaths_count"].ToString()),
							HeadshotsCount = int.Parse(npgsqlDataReader["headshots_count"].ToString()),
							ExpGained = int.Parse(npgsqlDataReader["exp_gained"].ToString()),
							PointGained = int.Parse(npgsqlDataReader["point_gained"].ToString()),
							Playtime = uint.Parse(string.Format("{0}", npgsqlDataReader["playtime"]))
						};
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
			return statisticDaily;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000B350 File Offset: 0x00009550
		public static bool CreatePlayerStatDailiesDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_dailies(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000B3FC File Offset: 0x000095FC
		public static StatisticWeapon GetPlayerStatWeaponsDB(long OwnerId)
		{
			StatisticWeapon statisticWeapon = null;
			if (OwnerId == 0L)
			{
				return statisticWeapon;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_weapons WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticWeapon = new StatisticWeapon
						{
							OwnerId = OwnerId,
							AssaultKills = int.Parse(npgsqlDataReader["assault_rifle_kills"].ToString()),
							AssaultDeaths = int.Parse(npgsqlDataReader["assault_rifle_deaths"].ToString()),
							SmgKills = int.Parse(npgsqlDataReader["sub_machine_gun_kills"].ToString()),
							SmgDeaths = int.Parse(npgsqlDataReader["sub_machine_gun_deaths"].ToString()),
							SniperKills = int.Parse(npgsqlDataReader["sniper_rifle_kills"].ToString()),
							SniperDeaths = int.Parse(npgsqlDataReader["sniper_rifle_deaths"].ToString()),
							MachinegunKills = int.Parse(npgsqlDataReader["machine_gun_kills"].ToString()),
							MachinegunDeaths = int.Parse(npgsqlDataReader["machine_gun_deaths"].ToString()),
							ShotgunKills = int.Parse(npgsqlDataReader["shot_gun_kills"].ToString()),
							ShotgunDeaths = int.Parse(npgsqlDataReader["shot_gun_deaths"].ToString())
						};
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
			return statisticWeapon;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public static bool CreatePlayerStatWeaponsDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_weapons(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public static StatisticAcemode GetPlayerStatAcemodesDB(long OwnerId)
		{
			StatisticAcemode statisticAcemode = null;
			if (OwnerId == 0L)
			{
				return statisticAcemode;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_acemodes WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticAcemode = new StatisticAcemode
						{
							OwnerId = OwnerId,
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							Kills = int.Parse(npgsqlDataReader["kills_count"].ToString()),
							Deaths = int.Parse(npgsqlDataReader["deaths_count"].ToString()),
							Headshots = int.Parse(npgsqlDataReader["headshots_count"].ToString()),
							Assists = int.Parse(npgsqlDataReader["assists_count"].ToString()),
							Escapes = int.Parse(npgsqlDataReader["escapes_count"].ToString()),
							Winstreaks = int.Parse(npgsqlDataReader["winstreaks_count"].ToString())
						};
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
			return statisticAcemode;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000B87C File Offset: 0x00009A7C
		public static bool CreatePlayerStatAcemodesDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_acemodes(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000B928 File Offset: 0x00009B28
		public static StatisticBattlecup GetPlayerStatBattlecupDB(long OwnerId)
		{
			StatisticBattlecup statisticBattlecup = null;
			if (OwnerId == 0L)
			{
				return statisticBattlecup;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_battlecups WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticBattlecup = new StatisticBattlecup
						{
							OwnerId = OwnerId,
							Matches = int.Parse(npgsqlDataReader["matches"].ToString()),
							MatchWins = int.Parse(npgsqlDataReader["match_wins"].ToString()),
							MatchLoses = int.Parse(npgsqlDataReader["match_loses"].ToString()),
							KillsCount = int.Parse(npgsqlDataReader["kills_count"].ToString()),
							DeathsCount = int.Parse(npgsqlDataReader["deaths_count"].ToString()),
							HeadshotsCount = int.Parse(npgsqlDataReader["headshots_count"].ToString()),
							AssistsCount = int.Parse(npgsqlDataReader["assists_count"].ToString()),
							EscapesCount = int.Parse(npgsqlDataReader["escapes_count"].ToString())
						};
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
			return statisticBattlecup;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public static bool CreatePlayerStatBattlecupsDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_battlecups(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000BB98 File Offset: 0x00009D98
		public static PlayerTitles GetPlayerTitlesDB(long OwnerId)
		{
			PlayerTitles playerTitles = null;
			if (OwnerId == 0L)
			{
				return playerTitles;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_titles WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerTitles = new PlayerTitles
						{
							OwnerId = OwnerId,
							Equiped1 = int.Parse(npgsqlDataReader["equip_slot1"].ToString()),
							Equiped2 = int.Parse(npgsqlDataReader["equip_slot2"].ToString()),
							Equiped3 = int.Parse(npgsqlDataReader["equip_slot3"].ToString()),
							Flags = long.Parse(npgsqlDataReader["flags"].ToString()),
							Slots = int.Parse(npgsqlDataReader["slots"].ToString())
						};
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
			return playerTitles;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000BD08 File Offset: 0x00009F08
		public static bool CreatePlayerTitlesDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_titles(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000BDB4 File Offset: 0x00009FB4
		public static PlayerBonus GetPlayerBonusDB(long OwnerId)
		{
			PlayerBonus playerBonus = null;
			if (OwnerId == 0L)
			{
				return playerBonus;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_bonus WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerBonus = new PlayerBonus
						{
							OwnerId = OwnerId,
							Bonuses = int.Parse(npgsqlDataReader["bonuses"].ToString()),
							CrosshairColor = int.Parse(npgsqlDataReader["crosshair_color"].ToString()),
							FreePass = int.Parse(npgsqlDataReader["free_pass"].ToString()),
							FakeRank = int.Parse(npgsqlDataReader["fake_rank"].ToString()),
							FakeNick = npgsqlDataReader["fake_nick"].ToString(),
							MuzzleColor = int.Parse(npgsqlDataReader["muzzle_color"].ToString()),
							NickBorderColor = int.Parse(npgsqlDataReader["nick_border_color"].ToString())
						};
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
			return playerBonus;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000BF58 File Offset: 0x0000A158
		public static bool CreatePlayerBonusDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_bonus(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000C004 File Offset: 0x0000A204
		public static PlayerConfig GetPlayerConfigDB(long OwnerId)
		{
			PlayerConfig playerConfig = null;
			if (OwnerId == 0L)
			{
				return playerConfig;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_configs WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerConfig = new PlayerConfig
						{
							OwnerId = OwnerId,
							Config = int.Parse(npgsqlDataReader["configs"].ToString()),
							ShowBlood = int.Parse(npgsqlDataReader["show_blood"].ToString()),
							Crosshair = int.Parse(npgsqlDataReader["crosshair"].ToString()),
							HandPosition = int.Parse(npgsqlDataReader["hand_pos"].ToString()),
							AudioSFX = int.Parse(npgsqlDataReader["audio_sfx"].ToString()),
							AudioBGM = int.Parse(npgsqlDataReader["audio_bgm"].ToString()),
							AudioEnable = int.Parse(npgsqlDataReader["audio_enable"].ToString()),
							Sensitivity = int.Parse(npgsqlDataReader["sensitivity"].ToString()),
							PointOfView = int.Parse(npgsqlDataReader["pov_size"].ToString()),
							InvertMouse = int.Parse(npgsqlDataReader["invert_mouse"].ToString()),
							EnableInviteMsg = int.Parse(npgsqlDataReader["enable_invite"].ToString()),
							EnableWhisperMsg = int.Parse(npgsqlDataReader["enable_whisper"].ToString()),
							Macro = int.Parse(npgsqlDataReader["macro_enable"].ToString()),
							Macro1 = npgsqlDataReader["macro1"].ToString(),
							Macro2 = npgsqlDataReader["macro2"].ToString(),
							Macro3 = npgsqlDataReader["macro3"].ToString(),
							Macro4 = npgsqlDataReader["macro4"].ToString(),
							Macro5 = npgsqlDataReader["macro5"].ToString(),
							Nations = int.Parse(npgsqlDataReader["nations"].ToString())
						};
						npgsqlDataReader.GetBytes(19, 0L, playerConfig.KeyboardKeys, 0, 235);
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
			return playerConfig;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public static bool CreatePlayerConfigDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_configs(owner_id) VALUES(@owner)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public static PlayerEvent GetPlayerEventDB(long OwnerId)
		{
			PlayerEvent playerEvent = null;
			if (OwnerId == 0L)
			{
				return playerEvent;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_events WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerEvent = new PlayerEvent
						{
							OwnerId = OwnerId,
							LastVisitCheckDay = int.Parse(npgsqlDataReader["last_visit_check_day"].ToString()),
							LastVisitSeqType = int.Parse(npgsqlDataReader["last_visit_seq_type"].ToString()),
							LastVisitDate = uint.Parse(npgsqlDataReader["last_visit_date"].ToString()),
							LastXmasDate = uint.Parse(npgsqlDataReader["last_xmas_date"].ToString()),
							LastPlaytimeDate = uint.Parse(npgsqlDataReader["last_playtime_date"].ToString()),
							LastPlaytimeValue = (long)int.Parse(npgsqlDataReader["last_playtime_value"].ToString()),
							LastPlaytimeFinish = int.Parse(npgsqlDataReader["last_playtime_finish"].ToString()),
							LastLoginDate = uint.Parse(npgsqlDataReader["last_login_date"].ToString()),
							LastQuestDate = uint.Parse(npgsqlDataReader["last_quest_date"].ToString()),
							LastQuestFinish = int.Parse(npgsqlDataReader["last_quest_finish"].ToString())
						};
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
			return playerEvent;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000C598 File Offset: 0x0000A798
		public static bool CreatePlayerEventDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_events (owner_id) VALUES (@id)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000C63C File Offset: 0x0000A83C
		public static List<FriendModel> GetPlayerFriendsDB(long OwnerId)
		{
			List<FriendModel> list = new List<FriendModel>();
			if (OwnerId == 0L)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_friends WHERE owner_id=@owner ORDER BY id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						FriendModel friendModel = new FriendModel(long.Parse(npgsqlDataReader["id"].ToString()))
						{
							OwnerId = OwnerId,
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							State = int.Parse(npgsqlDataReader["state"].ToString()),
							Removed = bool.Parse(npgsqlDataReader["removed"].ToString())
						};
						list.Add(friendModel);
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

		// Token: 0x06000054 RID: 84 RVA: 0x0000C780 File Offset: 0x0000A980
		public static void UpdatePlayerBonus(long PlayerId, int Bonuses, int FreePass)
		{
			if (PlayerId == 0L)
			{
				return;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@id", PlayerId);
					npgsqlCommand.Parameters.AddWithValue("@bonuses", Bonuses);
					npgsqlCommand.Parameters.AddWithValue("@freepass", FreePass);
					npgsqlCommand.CommandText = "UPDATE player_bonus SET bonuses=@bonuses, free_pass=@freepass WHERE owner_id=@id";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000C854 File Offset: 0x0000AA54
		public static List<QuickstartModel> GetPlayerQuickstartsDB(long OwnerId)
		{
			List<QuickstartModel> list = new List<QuickstartModel>();
			if (OwnerId == 0L)
			{
				return list;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_quickstarts WHERE owner_id=@owner;";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						QuickstartModel quickstartModel = new QuickstartModel
						{
							MapId = (int)byte.Parse(npgsqlDataReader["list0_map_id"].ToString()),
							Rule = (int)byte.Parse(npgsqlDataReader["list0_map_rule"].ToString()),
							StageOptions = (int)byte.Parse(npgsqlDataReader["list0_map_stage"].ToString()),
							Type = (int)byte.Parse(npgsqlDataReader["list0_map_type"].ToString())
						};
						list.Add(quickstartModel);
						QuickstartModel quickstartModel2 = new QuickstartModel
						{
							MapId = (int)byte.Parse(npgsqlDataReader["list1_map_id"].ToString()),
							Rule = (int)byte.Parse(npgsqlDataReader["list1_map_rule"].ToString()),
							StageOptions = (int)byte.Parse(npgsqlDataReader["list1_map_stage"].ToString()),
							Type = (int)byte.Parse(npgsqlDataReader["list1_map_type"].ToString())
						};
						list.Add(quickstartModel2);
						QuickstartModel quickstartModel3 = new QuickstartModel
						{
							MapId = (int)byte.Parse(npgsqlDataReader["list2_map_id"].ToString()),
							Rule = (int)byte.Parse(npgsqlDataReader["list2_map_rule"].ToString()),
							StageOptions = (int)byte.Parse(npgsqlDataReader["list2_map_stage"].ToString()),
							Type = (int)byte.Parse(npgsqlDataReader["list2_map_type"].ToString())
						};
						list.Add(quickstartModel3);
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

		// Token: 0x06000056 RID: 86 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public static bool CreatePlayerQuickstartsDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_quickstarts(owner_id) VALUES(@owner);";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public static bool IsPlayerNameExist(string Nickname)
		{
			if (string.IsNullOrEmpty(Nickname))
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
					npgsqlCommand.Parameters.AddWithValue("@name", Nickname);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM accounts WHERE nickname=@name";
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
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000CC08 File Offset: 0x0000AE08
		public static List<NHistoryModel> GetPlayerNickHistory(object Value, int Type)
		{
			List<NHistoryModel> list = new List<NHistoryModel>();
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					string text = ((Type == 0) ? "WHERE new_nick=@valor" : "WHERE owner_id=@valor");
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@valor", Value);
					npgsqlCommand.CommandText = "SELECT * FROM base_nick_history " + text + " ORDER BY change_date LIMIT 30";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						NHistoryModel nhistoryModel = new NHistoryModel
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							OwnerId = long.Parse(npgsqlDataReader["owner_id"].ToString()),
							OldNick = npgsqlDataReader["old_nick"].ToString(),
							NewNick = npgsqlDataReader["new_nick"].ToString(),
							ChangeDate = uint.Parse(npgsqlDataReader["change_date"].ToString()),
							Motive = npgsqlDataReader["motive"].ToString()
						};
						list.Add(nhistoryModel);
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

		// Token: 0x06000059 RID: 89 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public static bool CreatePlayerNickHistory(long OwnerId, string OldNick, string NewNick, string Motive)
		{
			NHistoryModel nhistoryModel = new NHistoryModel
			{
				OwnerId = OwnerId,
				OldNick = OldNick,
				NewNick = NewNick,
				ChangeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
				Motive = Motive
			};
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", nhistoryModel.OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@oldnick", nhistoryModel.OldNick);
					npgsqlCommand.Parameters.AddWithValue("@newnick", nhistoryModel.NewNick);
					npgsqlCommand.Parameters.AddWithValue("@date", (long)((ulong)nhistoryModel.ChangeDate));
					npgsqlCommand.Parameters.AddWithValue("@motive", nhistoryModel.Motive);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO base_nick_history(owner_id, old_nick, new_nick, change_date, motive) VALUES(@owner, @oldnick, @newnick, @date, @motive)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		public static bool UpdateAccountValuable(long PlayerId, int Gold, int Cash, int Tags)
		{
			if (PlayerId != 0L)
			{
				if (Gold != -1 || Cash != -1 || Tags != -1)
				{
					bool flag;
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.Parameters.AddWithValue("@owner", PlayerId);
							string text = "";
							if (Gold > -1)
							{
								npgsqlCommand.Parameters.AddWithValue("@gold", Gold);
								text += "gold=@gold";
							}
							if (Cash > -1)
							{
								npgsqlCommand.Parameters.AddWithValue("@cash", Cash);
								text = text + ((text != "") ? ", " : "") + "cash=@cash";
							}
							if (Tags > -1)
							{
								npgsqlCommand.Parameters.AddWithValue("@tags", Tags);
								text = text + ((text != "") ? ", " : "") + "tags=@tags";
							}
							npgsqlCommand.CommandText = "UPDATE accounts SET " + text + " WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000D078 File Offset: 0x0000B278
		public static bool UpdatePlayerKD(long OwnerId, int Kills, int Deaths, int Headshots, int Totals)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@deaths", Deaths);
					npgsqlCommand.Parameters.AddWithValue("@kills", Kills);
					npgsqlCommand.Parameters.AddWithValue("@hs", Headshots);
					npgsqlCommand.Parameters.AddWithValue("@total", Totals);
					npgsqlCommand.CommandText = "UPDATE player_stat_seasons SET kills_count=@kills, deaths_count=@deaths, headshots_count=@hs, total_kills=@total WHERE owner_id=@owner";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000D17C File Offset: 0x0000B37C
		public static bool UpdatePlayerMatches(int Matches, int MatchWins, int MatchLoses, int MatchDraws, int Totals, long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@partidas", Matches);
					npgsqlCommand.Parameters.AddWithValue("@ganhas", MatchWins);
					npgsqlCommand.Parameters.AddWithValue("@perdidas", MatchLoses);
					npgsqlCommand.Parameters.AddWithValue("@empates", MatchDraws);
					npgsqlCommand.Parameters.AddWithValue("@todaspartidas", Totals);
					npgsqlCommand.CommandText = "UPDATE player_stat_seasons SET matches=@partidas, match_wins=@ganhas, match_loses=@perdidas, match_draws=@empates, total_matches=@todaspartidas WHERE owner_id=@owner";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000D29C File Offset: 0x0000B49C
		public static bool UpdateAccountCash(long OwnerId, int Cash)
		{
			if (OwnerId != 0L)
			{
				if (Cash != -1)
				{
					bool flag;
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
							npgsqlCommand.Parameters.AddWithValue("@cash", Cash);
							npgsqlCommand.CommandText = "UPDATE accounts SET cash=@cash WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000D36C File Offset: 0x0000B56C
		public static bool UpdateAccountGold(long OwnerId, int Gold)
		{
			if (OwnerId != 0L)
			{
				if (Gold != -1)
				{
					bool flag;
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
							npgsqlCommand.Parameters.AddWithValue("@gold", Gold);
							npgsqlCommand.CommandText = "UPDATE accounts SET gold=@gold WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000D43C File Offset: 0x0000B63C
		public static bool UpdateAccountTags(long OwnerId, int Tags)
		{
			if (OwnerId != 0L)
			{
				if (Tags != -1)
				{
					bool flag;
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
							npgsqlCommand.Parameters.AddWithValue("@tag", Tags);
							npgsqlCommand.CommandText = "UPDATE accounts SET tags=@tag WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000022E7 File Offset: 0x000004E7
		public static void UpdateCouponEffect(long PlayerId, CouponEffects Effects)
		{
			if (PlayerId == 0L)
			{
				return;
			}
			ComDiv.UpdateDB("accounts", "coupon_effect", (long)Effects, "player_id", PlayerId);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000D50C File Offset: 0x0000B70C
		public static int GetRequestClanId(long OwnerId)
		{
			int num = 0;
			if (OwnerId == 0L)
			{
				return num;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT clan_id FROM system_clan_invites WHERE player_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						num = int.Parse(npgsqlDataReader["clan_id"].ToString());
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public static int GetRequestClanCount(int ClanId)
		{
			int num = 0;
			if (ClanId == 0)
			{
				return num;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000D67C File Offset: 0x0000B87C
		public static List<ClanInvite> GetClanRequestList(int ClanId)
		{
			List<ClanInvite> list = new List<ClanInvite>();
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
					npgsqlCommand.CommandText = "SELECT * FROM system_clan_invites WHERE clan_id=@clan";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						ClanInvite clanInvite = new ClanInvite
						{
							Id = ClanId,
							PlayerId = long.Parse(npgsqlDataReader["player_id"].ToString()),
							InviteDate = uint.Parse(npgsqlDataReader["invite_date"].ToString()),
							Text = npgsqlDataReader["text"].ToString()
						};
						list.Add(clanInvite);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return list;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000A0BC File Offset: 0x000082BC
		public static int GetPlayerMessagesCount(long OwnerId)
		{
			int num = 0;
			if (OwnerId == 0L)
			{
				return num;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000D79C File Offset: 0x0000B99C
		public static bool CreatePlayerMessage(long OwnerId, MessageModel Message)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@sendid", Message.SenderId);
					npgsqlCommand.Parameters.AddWithValue("@clan", Message.ClanId);
					npgsqlCommand.Parameters.AddWithValue("@sendname", Message.SenderName);
					npgsqlCommand.Parameters.AddWithValue("@text", Message.Text);
					npgsqlCommand.Parameters.AddWithValue("@type", Message.Type);
					npgsqlCommand.Parameters.AddWithValue("@state", Message.State);
					npgsqlCommand.Parameters.AddWithValue("@expire", Message.ExpireDate);
					npgsqlCommand.Parameters.AddWithValue("@cb", (int)Message.ClanNote);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire)VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
					object obj = npgsqlCommand.ExecuteScalar();
					Message.ObjectId = (long)obj;
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000230E File Offset: 0x0000050E
		public static bool DeletePlayerFriend(long friendId, long pId)
		{
			return ComDiv.DeleteDB("player_friends", "id", friendId, "owner_id", pId);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002330 File Offset: 0x00000530
		public static void UpdatePlayerFriendState(long ownerId, FriendModel friend)
		{
			ComDiv.UpdateDB("player_friends", "state", friend.State, "owner_id", ownerId, "id", friend.PlayerId);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002368 File Offset: 0x00000568
		public static void UpdatePlayerFriendBlock(long OwnerId, FriendModel Friend)
		{
			ComDiv.UpdateDB("player_friends", "removed", Friend.Removed, "owner_id", OwnerId, "id", Friend.PlayerId);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000023A0 File Offset: 0x000005A0
		public static bool DeleteClanInviteDB(int ClanId, long PlayerId)
		{
			return PlayerId != 0L && ClanId != 0 && ComDiv.DeleteDB("system_clan_invites", "clan_id", ClanId, "player_id", PlayerId);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000023CA File Offset: 0x000005CA
		public static bool DeleteClanInviteDB(long PlayerId)
		{
			return PlayerId != 0L && ComDiv.DeleteDB("system_clan_invites", "player_id", PlayerId);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000D938 File Offset: 0x0000BB38
		public static bool CreateClanInviteInDB(ClanInvite invite)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@clan", invite.Id);
					npgsqlCommand.Parameters.AddWithValue("@player", invite.PlayerId);
					npgsqlCommand.Parameters.AddWithValue("@date", (long)((ulong)invite.InviteDate));
					npgsqlCommand.Parameters.AddWithValue("@text", invite.Text);
					npgsqlCommand.CommandText = "INSERT INTO system_clan_invites(clan_id, player_id, invite_date, text)VALUES(@clan,@player,@date,@text)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public static int GetRequestClanInviteCount(int clanId)
		{
			int num = 0;
			if (clanId == 0)
			{
				return num;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@clan", clanId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		public static string GetRequestClanInviteText(int ClanId, long PlayerId)
		{
			string text = null;
			if (ClanId != 0 && PlayerId != 0L)
			{
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@clan", ClanId);
						npgsqlCommand.Parameters.AddWithValue("@player", PlayerId);
						npgsqlCommand.CommandText = "SELECT text FROM system_clan_invites WHERE clan_id=@clan AND player_id=@player";
						npgsqlCommand.CommandType = CommandType.Text;
						NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
						if (npgsqlDataReader.Read())
						{
							text = npgsqlDataReader["text"].ToString();
						}
						npgsqlCommand.Dispose();
						npgsqlDataReader.Close();
						npgsqlConnection.Close();
					}
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
				return text;
			}
			return text;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000DB10 File Offset: 0x0000BD10
		public static string GetPlayerIP4Address(long PlayerId)
		{
			string text = "";
			if (PlayerId == 0L)
			{
				return text;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@player", PlayerId);
					npgsqlCommand.CommandText = "SELECT ip4_address FROM accounts WHERE player_id=@player";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						text = npgsqlDataReader["ip4_address"].ToString();
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public static PlayerMissions GetPlayerMissionsDB(long OwnerId, int Mission1, int Mission2, int Mission3, int Mission4)
		{
			PlayerMissions playerMissions = null;
			if (OwnerId == 0L)
			{
				return playerMissions;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_missions WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerMissions = new PlayerMissions
						{
							OwnerId = OwnerId,
							ActualMission = int.Parse(npgsqlDataReader["current_mission"].ToString()),
							Card1 = int.Parse(npgsqlDataReader["card1"].ToString()),
							Card2 = int.Parse(npgsqlDataReader["card2"].ToString()),
							Card3 = int.Parse(npgsqlDataReader["card3"].ToString()),
							Card4 = int.Parse(npgsqlDataReader["card4"].ToString()),
							Mission1 = Mission1,
							Mission2 = Mission2,
							Mission3 = Mission3,
							Mission4 = Mission4
						};
						npgsqlDataReader.GetBytes(6, 0L, playerMissions.List1, 0, 40);
						npgsqlDataReader.GetBytes(7, 0L, playerMissions.List2, 0, 40);
						npgsqlDataReader.GetBytes(8, 0L, playerMissions.List3, 0, 40);
						npgsqlDataReader.GetBytes(9, 0L, playerMissions.List4, 0, 40);
						playerMissions.UpdateSelectedCard();
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
			return playerMissions;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		public static bool CreatePlayerMissionsDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_missions(owner_id) VALUES(@owner)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000DE80 File Offset: 0x0000C080
		public static void UpdateCurrentPlayerMissionList(long player_id, PlayerMissions mission)
		{
			byte[] currentMissionList = mission.GetCurrentMissionList();
			ComDiv.UpdateDB("player_missions", string.Format("mission{0}_raw", mission.ActualMission + 1), currentMissionList, "owner_id", player_id);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000023E6 File Offset: 0x000005E6
		public static bool DeletePlayerCharacter(long ObjectId, long OwnerId)
		{
			return ObjectId != 0L && OwnerId != 0L && ComDiv.DeleteDB("player_characters", "object_id", ObjectId, "owner_id", OwnerId);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002410 File Offset: 0x00000610
		public static bool UpdatePlayerCharacter(int Slot, long ObjectId, long OwnerId)
		{
			return ComDiv.UpdateDB("player_characters", "slot", Slot, "object_id", ObjectId, "owner_id", OwnerId);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000243D File Offset: 0x0000063D
		public static bool UpdateEquipedPlayerTitle(long player_id, int index, int titleId)
		{
			return ComDiv.UpdateDB("player_titles", string.Format("equip_slot{0}", index + 1), titleId, "owner_id", player_id);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000246C File Offset: 0x0000066C
		public static void UpdatePlayerTitlesFlags(long player_id, long flags)
		{
			ComDiv.UpdateDB("player_titles", "flags", flags, "owner_id", player_id);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public static void UpdatePlayerTitleRequi(long player_id, int medalhas, int insignias, int ordens_azuis, int broche)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@pid", player_id);
					npgsqlCommand.Parameters.AddWithValue("@broche", broche);
					npgsqlCommand.Parameters.AddWithValue("@insignias", insignias);
					npgsqlCommand.Parameters.AddWithValue("@medalhas", medalhas);
					npgsqlCommand.Parameters.AddWithValue("@ordensazuis", ordens_azuis);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "UPDATE accounts SET ribbon=@broche, ensign=@insignias, medal=@medalhas, master_medal=@ordensazuis WHERE player_id=@pid";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000248F File Offset: 0x0000068F
		public static bool UpdatePlayerMissionId(long player_id, int value, int index)
		{
			return ComDiv.UpdateDB("accounts", string.Format("mission_id{0}", index + 1), value, "player_id", player_id);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public static int GetUsedTicket(long OwnerId, string Token)
		{
			int num = 0;
			if (OwnerId != 0L && !string.IsNullOrEmpty(Token))
			{
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@player", OwnerId);
						npgsqlCommand.Parameters.AddWithValue("@token", Token);
						npgsqlCommand.CommandText = "SELECT used_count FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
						npgsqlCommand.CommandType = CommandType.Text;
						NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
						if (npgsqlDataReader.Read())
						{
							num = int.Parse(npgsqlDataReader["used_count"].ToString());
						}
						npgsqlCommand.Dispose();
						npgsqlDataReader.Close();
						npgsqlConnection.Close();
					}
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
				return num;
			}
			return num;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public static bool IsTicketUsedByPlayer(long OwnerId, string Token)
		{
			bool flag = false;
			if (OwnerId == 0L)
			{
				return flag;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@player", OwnerId);
					npgsqlCommand.Parameters.AddWithValue("@token", Token);
					npgsqlCommand.CommandText = "SELECT * FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
					npgsqlCommand.CommandType = CommandType.Text;
					flag = Convert.ToBoolean(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return flag;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000E164 File Offset: 0x0000C364
		public static bool CreatePlayerRedeemHistory(long OwnerId, string Token, int Used)
		{
			if (OwnerId != 0L && !string.IsNullOrEmpty(Token) && Used != 0)
			{
				bool flag;
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
						npgsqlCommand.Parameters.AddWithValue("@token", Token);
						npgsqlCommand.Parameters.AddWithValue("@used", Used);
						npgsqlCommand.CommandText = "INSERT INTO base_redeem_history(owner_id, used_token, used_count) VALUES(@owner, @token, @used)";
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
					flag = true;
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
					flag = false;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000E24C File Offset: 0x0000C44C
		public static PlayerVip GetPlayerVIP(long OwnerId)
		{
			PlayerVip playerVip = null;
			if (OwnerId == 0L)
			{
				return playerVip;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@ownerId", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_vip WHERE owner_id=@ownerId";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						playerVip = new PlayerVip
						{
							OwnerId = OwnerId,
							Address = npgsqlDataReader["registered_ip"].ToString(),
							Benefit = npgsqlDataReader["last_benefit"].ToString(),
							Expirate = uint.Parse(npgsqlDataReader["expirate"].ToString())
						};
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return playerVip;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000E354 File Offset: 0x0000C554
		public static PlayerReport GetPlayerReportDB(long OwnerId)
		{
			PlayerReport playerReport = null;
			if (OwnerId == 0L)
			{
				return playerReport;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
						npgsqlCommand.CommandText = "SELECT * FROM player_reports WHERE owner_id=@owner";
						npgsqlCommand.CommandType = CommandType.Text;
						using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default))
						{
							while (npgsqlDataReader.Read())
							{
								playerReport = new PlayerReport
								{
									OwnerId = OwnerId,
									TicketCount = int.Parse(npgsqlDataReader["ticket_count"].ToString()),
									ReportedCount = int.Parse(npgsqlDataReader["reported_count"].ToString())
								};
							}
							npgsqlDataReader.Close();
							npgsqlConnection.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return playerReport;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000E47C File Offset: 0x0000C67C
		public static bool CreatePlayerReportDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
						npgsqlCommand.CommandText = "INSERT INTO player_reports(owner_id) VALUES(@owner)";
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000E540 File Offset: 0x0000C740
		public static bool CreatePlayerReportHistory(long OwnerId, long SenderId, string OwnerNick, string SenderNick, ReportType Type, string Message)
		{
			RHistoryModel rhistoryModel = new RHistoryModel
			{
				OwnerId = OwnerId,
				OwnerNick = OwnerNick,
				SenderId = SenderId,
				SenderNick = SenderNick,
				Date = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
				Type = Type,
				Message = Message
			};
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.Parameters.AddWithValue("@OwnerId", rhistoryModel.OwnerId);
						npgsqlCommand.Parameters.AddWithValue("@OwnerNick", rhistoryModel.OwnerNick);
						npgsqlCommand.Parameters.AddWithValue("@SenderId", rhistoryModel.SenderId);
						npgsqlCommand.Parameters.AddWithValue("@SenderNick", rhistoryModel.SenderNick);
						npgsqlCommand.Parameters.AddWithValue("@Date", (long)((ulong)rhistoryModel.Date));
						npgsqlCommand.Parameters.AddWithValue("@Type", (int)rhistoryModel.Type);
						npgsqlCommand.Parameters.AddWithValue("@Message", rhistoryModel.Message);
						npgsqlCommand.CommandText = "INSERT INTO base_report_history(date, owner_id, owner_nick, sender_id, sender_nick, type, message) VALUES(@Date, @OwnerId, @OwnerNick, @SenderId, @SenderNick, @Type, @Message)";
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
						flag = true;
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000E708 File Offset: 0x0000C908
		public static PlayerBattlepass GetPlayerBattlepassDB(long OwnerId)
		{
			PlayerBattlepass playerBattlepass = null;
			if (OwnerId == 0L)
			{
				return playerBattlepass;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_battlepass WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerBattlepass = new PlayerBattlepass
						{
							Id = int.Parse(npgsqlDataReader["id"].ToString()),
							Level = int.Parse(npgsqlDataReader["level"].ToString()),
							IsPremium = bool.Parse(npgsqlDataReader["premium"].ToString()),
							TotalPoints = int.Parse(npgsqlDataReader["total_points"].ToString()),
							DailyPoints = int.Parse(npgsqlDataReader["daily_points"].ToString()),
							LastRecord = uint.Parse(npgsqlDataReader["last_record"].ToString())
						};
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
			return playerBattlepass;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000E88C File Offset: 0x0000CA8C
		public static PlayerCompetitive GetPlayerCompetitiveDB(long OwnerId)
		{
			PlayerCompetitive playerCompetitive = null;
			if (OwnerId == 0L)
			{
				return playerCompetitive;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_competitive WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerCompetitive = new PlayerCompetitive
						{
							OwnerId = OwnerId,
							Level = int.Parse(npgsqlDataReader["level"].ToString()),
							Points = int.Parse(npgsqlDataReader["points"].ToString())
						};
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
			return playerCompetitive;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000E990 File Offset: 0x0000CB90
		public static bool CreatePlayerBattlepassDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_battlepass VALUES(@owner);";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public static bool CreatePlayerCompetitiveDB(long OwnerId)
		{
			if (OwnerId == 0L)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.Parameters.AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_competitive VALUES(@owner);";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}
	}
}
