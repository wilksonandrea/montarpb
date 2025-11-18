using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.SQL;

public static class DaoManagerSQL
{
	public static List<ItemsModel> GetPlayerInventoryItems(long OwnerId)
	{
		List<ItemsModel> list = new List<ItemsModel>();
		if (OwnerId == 0L)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_items WHERE owner_id=@owner ORDER BY object_id ASC;";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					ItemsModel item = new ItemsModel(int.Parse(((DbDataReader)(object)val3)["id"].ToString()))
					{
						ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString()),
						Name = ((DbDataReader)(object)val3)["name"].ToString(),
						Count = uint.Parse(((DbDataReader)(object)val3)["count"].ToString()),
						Equip = (ItemEquipType)int.Parse(((DbDataReader)(object)val3)["equip"].ToString())
					};
					list.Add(item);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static bool CreatePlayerInventoryItem(ItemsModel Item, long OwnerId)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				obj.Parameters.AddWithValue("@itmId", (object)Item.Id);
				obj.Parameters.AddWithValue("@ItmNm", (object)Item.Name);
				obj.Parameters.AddWithValue("@count", (object)(long)Item.Count);
				obj.Parameters.AddWithValue("@equip", (object)(int)Item.Equip);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_items(owner_id, id, name, count, equip) VALUES(@owner, @itmId, @ItmNm, @count, @equip) RETURNING object_id";
				object obj2 = ((DbCommand)(object)obj).ExecuteScalar();
				Item.ObjectId = ((Item.Equip != ItemEquipType.Permanent) ? ((long)obj2) : Item.ObjectId);
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool DeletePlayerInventoryItem(long ObjectId, long OwnerId)
	{
		if (ObjectId != 0L && OwnerId != 0L)
		{
			return ComDiv.DeleteDB("player_items", "object_id", ObjectId, "owner_id", OwnerId);
		}
		return false;
	}

	public static BanHistory GetAccountBan(long ObjectId)
	{
		BanHistory banHistory = new BanHistory();
		if (ObjectId == 0L)
		{
			return banHistory;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@obj", (object)ObjectId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM base_ban_history WHERE object_id=@obj";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					banHistory.ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString());
					banHistory.PlayerId = long.Parse(((DbDataReader)(object)val3)["owner_id"].ToString());
					banHistory.Type = ((DbDataReader)(object)val3)["type"].ToString();
					banHistory.Value = ((DbDataReader)(object)val3)["value"].ToString();
					banHistory.Reason = ((DbDataReader)(object)val3)["reason"].ToString();
					banHistory.StartDate = DateTime.Parse(((DbDataReader)(object)val3)["start_date"].ToString());
					banHistory.EndDate = DateTime.Parse(((DbDataReader)(object)val3)["expire_date"].ToString());
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return banHistory;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return null;
		}
	}

	public static List<string> GetHwIdList()
	{
		List<string> list = new List<string>();
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)val2).CommandText = "SELECT * FROM base_ban_hwid";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					string text = ((DbDataReader)(object)val3)["hardware_id"].ToString();
					if (text != null || text.Length != 0)
					{
						list.Add(text);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return null;
		}
	}

	public static void GetBanStatus(string MAC, string IP4, out bool ValidMac, out bool ValidIp4)
	{
		ValidMac = false;
		ValidIp4 = false;
		try
		{
			DateTime dateTime = DateTimeUtil.Now();
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@mac", (object)MAC);
				val2.Parameters.AddWithValue("@ip", (object)IP4);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM base_ban_history WHERE value in (@mac, @ip)";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					string text = ((DbDataReader)(object)val3)["type"].ToString();
					string text2 = ((DbDataReader)(object)val3)["value"].ToString();
					if (!(DateTime.Parse(((DbDataReader)(object)val3)["expire_date"].ToString()) < dateTime))
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
				((Component)(object)val2).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static BanHistory SaveBanHistory(long PlayerId, string Type, string Value, DateTime EndDate)
	{
		BanHistory banHistory = new BanHistory
		{
			PlayerId = PlayerId,
			Type = Type,
			Value = Value,
			EndDate = EndDate
		};
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@provider", (object)banHistory.PlayerId);
				obj.Parameters.AddWithValue("@type", (object)banHistory.Type);
				obj.Parameters.AddWithValue("@value", (object)banHistory.Value);
				obj.Parameters.AddWithValue("@reason", (object)banHistory.Reason);
				obj.Parameters.AddWithValue("@start", (object)banHistory.StartDate);
				obj.Parameters.AddWithValue("@end", (object)banHistory.EndDate);
				((DbCommand)(object)obj).CommandText = "INSERT INTO base_ban_history(owner_id, type, value, reason, start_date, expire_date) VALUES(@provider, @type, @value, @reason, @start, @end) RETURNING object_id";
				object obj2 = ((DbCommand)(object)obj).ExecuteScalar();
				banHistory.ObjectId = (long)obj2;
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return banHistory;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return null;
		}
	}

	public static bool SaveAutoBan(long PlayerId, string Username, string Nickname, string Type, string Time, string Address, string HackType)
	{
		if (PlayerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@player_id", (object)PlayerId);
				obj.Parameters.AddWithValue("@login", (object)Username);
				obj.Parameters.AddWithValue("@player_name", (object)Nickname);
				obj.Parameters.AddWithValue("@type", (object)Type);
				obj.Parameters.AddWithValue("@time", (object)Time);
				obj.Parameters.AddWithValue("@ip", (object)Address);
				obj.Parameters.AddWithValue("@hack_type", (object)HackType);
				((DbCommand)(object)obj).CommandText = "INSERT INTO base_auto_ban(owner_id, username, nickname, type, time, ip4_address, hack_type) VALUES(@player_id, @login, @player_name, @type, @time, @ip, @hack_type)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool SaveBanReason(long ObjectId, string Reason)
	{
		if (ObjectId == 0L)
		{
			return false;
		}
		return ComDiv.UpdateDB("base_ban_history", "reason", Reason, "object_id", ObjectId);
	}

	public static bool CreateClan(out int ClanId, string Name, long OwnerId, string ClanInfo, uint CreateDate)
	{
		try
		{
			ClanId = -1;
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				obj.Parameters.AddWithValue("@name", (object)Name);
				obj.Parameters.AddWithValue("@date", (object)(long)CreateDate);
				obj.Parameters.AddWithValue("@info", (object)ClanInfo);
				obj.Parameters.AddWithValue("@best", (object)"0-0");
				((DbCommand)(object)obj).CommandText = "INSERT INTO system_clan (name, owner_id, create_date, info, best_exp, best_participants, best_wins, best_kills, best_headshots) VALUES (@name, @owner, @date, @info, @best, @best, @best, @best, @best) RETURNING id";
				object obj2 = ((DbCommand)(object)obj).ExecuteScalar();
				ClanId = (int)obj2;
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			ClanId = -1;
			return false;
		}
	}

	public static bool UpdateClanInfo(int ClanId, int Authority, int RankLimit, int MinAge, int MaxAge, int JoinType)
	{
		if (ClanId == 0)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@ClanId", (object)ClanId);
				obj.Parameters.AddWithValue("@Authority", (object)Authority);
				obj.Parameters.AddWithValue("@RankLimit", (object)RankLimit);
				obj.Parameters.AddWithValue("@MinAge", (object)MinAge);
				obj.Parameters.AddWithValue("@MaxAge", (object)MaxAge);
				obj.Parameters.AddWithValue("@JoinType", (object)JoinType);
				((DbCommand)(object)obj).CommandText = "UPDATE system_clan SET authority=@Authority, rank_limit=@RankLimit, min_age_limit=@MinAge, max_age_limit=@MaxAge, join_permission=@JoinType WHERE id=@ClanId";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static void UpdateClanBestPlayers(ClanModel Clan)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@id", (object)Clan.Id);
				obj.Parameters.AddWithValue("@bp1", (object)Clan.BestPlayers.Exp.GetSplit());
				obj.Parameters.AddWithValue("@bp2", (object)Clan.BestPlayers.Participation.GetSplit());
				obj.Parameters.AddWithValue("@bp3", (object)Clan.BestPlayers.Wins.GetSplit());
				obj.Parameters.AddWithValue("@bp4", (object)Clan.BestPlayers.Kills.GetSplit());
				obj.Parameters.AddWithValue("@bp5", (object)Clan.BestPlayers.Headshots.GetSplit());
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = "UPDATE system_clan SET best_exp=@bp1, best_participants=@bp2, best_wins=@bp3, best_kills=@bp4, best_headshots=@bp5 WHERE id=@id";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static bool UpdateClanLogo(int ClanId, uint logo)
	{
		if (ClanId == 0)
		{
			return false;
		}
		return ComDiv.UpdateDB("system_clan", "logo", (long)logo, "id", ClanId);
	}

	public static bool UpdateClanPoints(int ClanId, float Gold)
	{
		if (ClanId == 0)
		{
			return false;
		}
		return ComDiv.UpdateDB("system_clan", "gold", Gold, "id", ClanId);
	}

	public static bool UpdateClanExp(int ClanId, int Exp)
	{
		if (ClanId == 0)
		{
			return false;
		}
		return ComDiv.UpdateDB("system_clan", "exp", Exp, "id", ClanId);
	}

	public static bool UpdateClanRank(int ClanId, int Rank)
	{
		if (ClanId == 0)
		{
			return false;
		}
		return ComDiv.UpdateDB("system_clan", "rank", Rank, "id", ClanId);
	}

	public static bool UpdateClanBattles(int ClanId, int Matches, int Wins, int Loses)
	{
		if (ClanId == 0)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@clan", (object)ClanId);
				obj.Parameters.AddWithValue("@partidas", (object)Matches);
				obj.Parameters.AddWithValue("@vitorias", (object)Wins);
				obj.Parameters.AddWithValue("@derrotas", (object)Loses);
				((DbCommand)(object)obj).CommandText = "UPDATE system_clan SET matches=@partidas, match_wins=@vitorias, match_loses=@derrotas WHERE id=@clan";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static int GetClanPlayers(int ClanId)
	{
		int result = 0;
		if (ClanId == 0)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@clan", (object)ClanId);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM accounts WHERE clan_id=@clan";
				result = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static MessageModel GetMessage(long ObjectId, long PlayerId)
	{
		MessageModel result = null;
		if (ObjectId != 0L && PlayerId != 0L)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand val2 = val.CreateCommand();
					((DbConnection)(object)val).Open();
					val2.Parameters.AddWithValue("@obj", (object)ObjectId);
					val2.Parameters.AddWithValue("@owner", (object)PlayerId);
					((DbCommand)(object)val2).CommandText = "SELECT * FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
					while (((DbDataReader)(object)val3).Read())
					{
						result = new MessageModel(uint.Parse(((DbDataReader)(object)val3)["expire_date"].ToString()), DateTimeUtil.Now())
						{
							ObjectId = ObjectId,
							SenderId = long.Parse(((DbDataReader)(object)val3)["sender_id"].ToString()),
							SenderName = ((DbDataReader)(object)val3)["sender_name"].ToString(),
							ClanId = int.Parse(((DbDataReader)(object)val3)["clan_id"].ToString()),
							ClanNote = (NoteMessageClan)int.Parse(((DbDataReader)(object)val3)["clan_note"].ToString()),
							Text = ((DbDataReader)(object)val3)["text"].ToString(),
							Type = (NoteMessageType)int.Parse(((DbDataReader)(object)val3)["type"].ToString()),
							State = (NoteMessageState)int.Parse(((DbDataReader)(object)val3)["state"].ToString())
						};
					}
					((Component)(object)val2).Dispose();
					((DbDataReader)(object)val3).Close();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
					return result;
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return null;
			}
		}
		return result;
	}

	public static List<MessageModel> GetGiftMessages(long OwnerId)
	{
		List<MessageModel> list = new List<MessageModel>();
		if (OwnerId == 0L)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					NoteMessageType noteMessageType = (NoteMessageType)int.Parse(((DbDataReader)(object)val3)["type"].ToString());
					if (noteMessageType == NoteMessageType.Gift)
					{
						MessageModel item = new MessageModel(uint.Parse(((DbDataReader)(object)val3)["expire_date"].ToString()), DateTimeUtil.Now())
						{
							ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString()),
							SenderId = long.Parse(((DbDataReader)(object)val3)["sender_id"].ToString()),
							SenderName = ((DbDataReader)(object)val3)["sender_name"].ToString(),
							ClanId = int.Parse(((DbDataReader)(object)val3)["clan_id"].ToString()),
							ClanNote = (NoteMessageClan)int.Parse(((DbDataReader)(object)val3)["clan_note"].ToString()),
							Text = ((DbDataReader)(object)val3)["text"].ToString(),
							Type = noteMessageType,
							State = (NoteMessageState)int.Parse(((DbDataReader)(object)val3)["state"].ToString())
						};
						list.Add(item);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static List<MessageModel> GetMessages(long OwnerId)
	{
		List<MessageModel> list = new List<MessageModel>();
		if (OwnerId == 0L)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					NoteMessageType noteMessageType = (NoteMessageType)int.Parse(((DbDataReader)(object)val3)["type"].ToString());
					if (noteMessageType != NoteMessageType.Gift)
					{
						MessageModel item = new MessageModel(uint.Parse(((DbDataReader)(object)val3)["expire_date"].ToString()), DateTimeUtil.Now())
						{
							ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString()),
							SenderId = long.Parse(((DbDataReader)(object)val3)["sender_id"].ToString()),
							SenderName = ((DbDataReader)(object)val3)["sender_name"].ToString(),
							ClanId = int.Parse(((DbDataReader)(object)val3)["clan_id"].ToString()),
							ClanNote = (NoteMessageClan)int.Parse(((DbDataReader)(object)val3)["clan_note"].ToString()),
							Text = ((DbDataReader)(object)val3)["text"].ToString(),
							Type = noteMessageType,
							State = (NoteMessageState)int.Parse(((DbDataReader)(object)val3)["state"].ToString())
						};
						list.Add(item);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static bool MessageExists(long ObjectId, long OwnerId)
	{
		if (ObjectId != 0L && OwnerId != 0L)
		{
			try
			{
				int num = 0;
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					obj.Parameters.AddWithValue("@obj", (object)ObjectId);
					obj.Parameters.AddWithValue("@owner", (object)OwnerId);
					((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
					num = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
					((Component)(object)obj).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val)?.Dispose();
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

	public static int GetMessagesCount(long OwnerId)
	{
		int result = 0;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
				result = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreateMessage(long OwnerId, MessageModel Message)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				obj.Parameters.AddWithValue("@sendid", (object)Message.SenderId);
				obj.Parameters.AddWithValue("@clan", (object)Message.ClanId);
				obj.Parameters.AddWithValue("@sendname", (object)Message.SenderName);
				obj.Parameters.AddWithValue("@text", (object)Message.Text);
				obj.Parameters.AddWithValue("@type", (object)(int)Message.Type);
				obj.Parameters.AddWithValue("@state", (object)(int)Message.State);
				obj.Parameters.AddWithValue("@expire", (object)Message.ExpireDate);
				obj.Parameters.AddWithValue("@cb", (object)(int)Message.ClanNote);
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire_date) VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
				object obj2 = ((DbCommand)(object)obj).ExecuteScalar();
				Message.ObjectId = (long)obj2;
				((Component)(object)obj).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static void UpdateState(long ObjectId, long OwnerId, int Value)
	{
		ComDiv.UpdateDB("player_messages", "state", Value, "object_id", ObjectId, "owner_id", OwnerId);
	}

	public static void UpdateExpireDate(long ObjectId, long OwnerId, uint Date)
	{
		ComDiv.UpdateDB("player_messages", "expire_date", (long)Date, "object_id", ObjectId, "owner_id", OwnerId);
	}

	public static bool DeleteMessage(long ObjectId, long OwnerId)
	{
		if (ObjectId != 0L && OwnerId != 0L)
		{
			return ComDiv.DeleteDB("player_messages", "object_id", ObjectId, "owner_id", OwnerId);
		}
		return false;
	}

	public static bool DeleteMessages(List<object> ObjectIds, long OwnerId)
	{
		if (ObjectIds.Count != 0 && OwnerId != 0L)
		{
			return ComDiv.DeleteDB("player_messages", "object_id", ObjectIds.ToArray(), "owner_id", OwnerId);
		}
		return false;
	}

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
		DeleteMessages(list, OwnerId);
	}

	public static PlayerEquipment GetPlayerEquipmentsDB(long OwnerId)
	{
		PlayerEquipment result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_equipments WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new PlayerEquipment
					{
						OwnerId = OwnerId,
						WeaponPrimary = int.Parse(((DbDataReader)(object)val3)["weapon_primary"].ToString()),
						WeaponSecondary = int.Parse(((DbDataReader)(object)val3)["weapon_secondary"].ToString()),
						WeaponMelee = int.Parse(((DbDataReader)(object)val3)["weapon_melee"].ToString()),
						WeaponExplosive = int.Parse(((DbDataReader)(object)val3)["weapon_explosive"].ToString()),
						WeaponSpecial = int.Parse(((DbDataReader)(object)val3)["weapon_special"].ToString()),
						CharaRedId = int.Parse(((DbDataReader)(object)val3)["chara_red_side"].ToString()),
						CharaBlueId = int.Parse(((DbDataReader)(object)val3)["chara_blue_side"].ToString()),
						DinoItem = int.Parse(((DbDataReader)(object)val3)["dino_item_chara"].ToString()),
						PartHead = int.Parse(((DbDataReader)(object)val3)["part_head"].ToString()),
						PartFace = int.Parse(((DbDataReader)(object)val3)["part_face"].ToString()),
						PartJacket = int.Parse(((DbDataReader)(object)val3)["part_jacket"].ToString()),
						PartPocket = int.Parse(((DbDataReader)(object)val3)["part_pocket"].ToString()),
						PartGlove = int.Parse(((DbDataReader)(object)val3)["part_glove"].ToString()),
						PartBelt = int.Parse(((DbDataReader)(object)val3)["part_belt"].ToString()),
						PartHolster = int.Parse(((DbDataReader)(object)val3)["part_holster"].ToString()),
						PartSkin = int.Parse(((DbDataReader)(object)val3)["part_skin"].ToString()),
						BeretItem = int.Parse(((DbDataReader)(object)val3)["beret_item_part"].ToString()),
						AccessoryId = int.Parse(((DbDataReader)(object)val3)["accesory_id"].ToString()),
						SprayId = int.Parse(((DbDataReader)(object)val3)["spray_id"].ToString()),
						NameCardId = int.Parse(((DbDataReader)(object)val3)["namecard_id"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerEquipmentsDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_equipments(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static List<CharacterModel> GetPlayerCharactersDB(long OwnerId)
	{
		List<CharacterModel> list = new List<CharacterModel>();
		if (OwnerId == 0L)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@OwnerId", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_characters WHERE owner_id=@OwnerId ORDER BY slot ASC;";
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					CharacterModel item = new CharacterModel
					{
						ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString()),
						Id = int.Parse(((DbDataReader)(object)val3)["id"].ToString()),
						Slot = int.Parse(((DbDataReader)(object)val3)["slot"].ToString()),
						Name = ((DbDataReader)(object)val3)["name"].ToString(),
						CreateDate = uint.Parse(((DbDataReader)(object)val3)["create_date"].ToString()),
						PlayTime = uint.Parse(((DbDataReader)(object)val3)["playtime"].ToString())
					};
					list.Add(item);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static bool CreatePlayerCharacter(CharacterModel Chara, long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner_id", (object)OwnerId);
				obj.Parameters.AddWithValue("@id", (object)Chara.Id);
				obj.Parameters.AddWithValue("@slot", (object)Chara.Slot);
				obj.Parameters.AddWithValue("@name", (object)Chara.Name);
				obj.Parameters.AddWithValue("@createdate", (object)(long)Chara.CreateDate);
				obj.Parameters.AddWithValue("@playtime", (object)(long)Chara.PlayTime);
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_characters(owner_id, id, slot, name, create_date, playtime) VALUES(@owner_id, @id, @slot, @name, @createdate, @playtime) RETURNING object_id";
				object obj2 = ((DbCommand)(object)obj).ExecuteScalar();
				Chara.ObjectId = (long)obj2;
				((Component)(object)obj).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticTotal GetPlayerStatBasicDB(long OwnerId)
	{
		StatisticTotal result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_basics WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticTotal
					{
						OwnerId = OwnerId,
						Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
						MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
						MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
						MatchDraws = int.Parse(((DbDataReader)(object)val3)["match_draws"].ToString()),
						KillsCount = int.Parse(((DbDataReader)(object)val3)["kills_count"].ToString()),
						DeathsCount = int.Parse(((DbDataReader)(object)val3)["deaths_count"].ToString()),
						HeadshotsCount = int.Parse(((DbDataReader)(object)val3)["headshots_count"].ToString()),
						AssistsCount = int.Parse(((DbDataReader)(object)val3)["assists_count"].ToString()),
						EscapesCount = int.Parse(((DbDataReader)(object)val3)["escapes_count"].ToString()),
						MvpCount = int.Parse(((DbDataReader)(object)val3)["mvp_count"].ToString()),
						TotalMatchesCount = int.Parse(((DbDataReader)(object)val3)["total_matches"].ToString()),
						TotalKillsCount = int.Parse(((DbDataReader)(object)val3)["total_kills"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatBasicDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_basics(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticSeason GetPlayerStatSeasonDB(long OwnerId)
	{
		StatisticSeason result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_seasons WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticSeason
					{
						OwnerId = OwnerId,
						Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
						MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
						MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
						MatchDraws = int.Parse(((DbDataReader)(object)val3)["match_draws"].ToString()),
						KillsCount = int.Parse(((DbDataReader)(object)val3)["kills_count"].ToString()),
						DeathsCount = int.Parse(((DbDataReader)(object)val3)["deaths_count"].ToString()),
						HeadshotsCount = int.Parse(((DbDataReader)(object)val3)["headshots_count"].ToString()),
						AssistsCount = int.Parse(((DbDataReader)(object)val3)["assists_count"].ToString()),
						EscapesCount = int.Parse(((DbDataReader)(object)val3)["escapes_count"].ToString()),
						MvpCount = int.Parse(((DbDataReader)(object)val3)["mvp_count"].ToString()),
						TotalMatchesCount = int.Parse(((DbDataReader)(object)val3)["total_matches"].ToString()),
						TotalKillsCount = int.Parse(((DbDataReader)(object)val3)["total_kills"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatSeasonDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_seasons(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticClan GetPlayerStatClanDB(long OwnerId)
	{
		StatisticClan result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_clans WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticClan
					{
						OwnerId = OwnerId,
						Matches = int.Parse(((DbDataReader)(object)val3)["clan_matches"].ToString()),
						MatchWins = int.Parse(((DbDataReader)(object)val3)["clan_match_wins"].ToString()),
						MatchLoses = int.Parse(((DbDataReader)(object)val3)["clan_match_loses"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatClanDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_clans(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticDaily GetPlayerStatDailiesDB(long OwnerId)
	{
		StatisticDaily result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_dailies WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticDaily
					{
						OwnerId = OwnerId,
						Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
						MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
						MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
						MatchDraws = int.Parse(((DbDataReader)(object)val3)["match_draws"].ToString()),
						KillsCount = int.Parse(((DbDataReader)(object)val3)["kills_count"].ToString()),
						DeathsCount = int.Parse(((DbDataReader)(object)val3)["deaths_count"].ToString()),
						HeadshotsCount = int.Parse(((DbDataReader)(object)val3)["headshots_count"].ToString()),
						ExpGained = int.Parse(((DbDataReader)(object)val3)["exp_gained"].ToString()),
						PointGained = int.Parse(((DbDataReader)(object)val3)["point_gained"].ToString()),
						Playtime = uint.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["playtime"]))
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatDailiesDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_dailies(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticWeapon GetPlayerStatWeaponsDB(long OwnerId)
	{
		StatisticWeapon result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_weapons WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticWeapon
					{
						OwnerId = OwnerId,
						AssaultKills = int.Parse(((DbDataReader)(object)val3)["assault_rifle_kills"].ToString()),
						AssaultDeaths = int.Parse(((DbDataReader)(object)val3)["assault_rifle_deaths"].ToString()),
						SmgKills = int.Parse(((DbDataReader)(object)val3)["sub_machine_gun_kills"].ToString()),
						SmgDeaths = int.Parse(((DbDataReader)(object)val3)["sub_machine_gun_deaths"].ToString()),
						SniperKills = int.Parse(((DbDataReader)(object)val3)["sniper_rifle_kills"].ToString()),
						SniperDeaths = int.Parse(((DbDataReader)(object)val3)["sniper_rifle_deaths"].ToString()),
						MachinegunKills = int.Parse(((DbDataReader)(object)val3)["machine_gun_kills"].ToString()),
						MachinegunDeaths = int.Parse(((DbDataReader)(object)val3)["machine_gun_deaths"].ToString()),
						ShotgunKills = int.Parse(((DbDataReader)(object)val3)["shot_gun_kills"].ToString()),
						ShotgunDeaths = int.Parse(((DbDataReader)(object)val3)["shot_gun_deaths"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatWeaponsDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_weapons(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticAcemode GetPlayerStatAcemodesDB(long OwnerId)
	{
		StatisticAcemode result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_acemodes WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticAcemode
					{
						OwnerId = OwnerId,
						Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
						MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
						MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
						Kills = int.Parse(((DbDataReader)(object)val3)["kills_count"].ToString()),
						Deaths = int.Parse(((DbDataReader)(object)val3)["deaths_count"].ToString()),
						Headshots = int.Parse(((DbDataReader)(object)val3)["headshots_count"].ToString()),
						Assists = int.Parse(((DbDataReader)(object)val3)["assists_count"].ToString()),
						Escapes = int.Parse(((DbDataReader)(object)val3)["escapes_count"].ToString()),
						Winstreaks = int.Parse(((DbDataReader)(object)val3)["winstreaks_count"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatAcemodesDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_acemodes(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static StatisticBattlecup GetPlayerStatBattlecupDB(long OwnerId)
	{
		StatisticBattlecup result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_stat_battlecups WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new StatisticBattlecup
					{
						OwnerId = OwnerId,
						Matches = int.Parse(((DbDataReader)(object)val3)["matches"].ToString()),
						MatchWins = int.Parse(((DbDataReader)(object)val3)["match_wins"].ToString()),
						MatchLoses = int.Parse(((DbDataReader)(object)val3)["match_loses"].ToString()),
						KillsCount = int.Parse(((DbDataReader)(object)val3)["kills_count"].ToString()),
						DeathsCount = int.Parse(((DbDataReader)(object)val3)["deaths_count"].ToString()),
						HeadshotsCount = int.Parse(((DbDataReader)(object)val3)["headshots_count"].ToString()),
						AssistsCount = int.Parse(((DbDataReader)(object)val3)["assists_count"].ToString()),
						EscapesCount = int.Parse(((DbDataReader)(object)val3)["escapes_count"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerStatBattlecupsDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_stat_battlecups(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static PlayerTitles GetPlayerTitlesDB(long OwnerId)
	{
		PlayerTitles result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_titles WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new PlayerTitles
					{
						OwnerId = OwnerId,
						Equiped1 = int.Parse(((DbDataReader)(object)val3)["equip_slot1"].ToString()),
						Equiped2 = int.Parse(((DbDataReader)(object)val3)["equip_slot2"].ToString()),
						Equiped3 = int.Parse(((DbDataReader)(object)val3)["equip_slot3"].ToString()),
						Flags = long.Parse(((DbDataReader)(object)val3)["flags"].ToString()),
						Slots = int.Parse(((DbDataReader)(object)val3)["slots"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerTitlesDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_titles(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static PlayerBonus GetPlayerBonusDB(long OwnerId)
	{
		PlayerBonus result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_bonus WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new PlayerBonus
					{
						OwnerId = OwnerId,
						Bonuses = int.Parse(((DbDataReader)(object)val3)["bonuses"].ToString()),
						CrosshairColor = int.Parse(((DbDataReader)(object)val3)["crosshair_color"].ToString()),
						FreePass = int.Parse(((DbDataReader)(object)val3)["free_pass"].ToString()),
						FakeRank = int.Parse(((DbDataReader)(object)val3)["fake_rank"].ToString()),
						FakeNick = ((DbDataReader)(object)val3)["fake_nick"].ToString(),
						MuzzleColor = int.Parse(((DbDataReader)(object)val3)["muzzle_color"].ToString()),
						NickBorderColor = int.Parse(((DbDataReader)(object)val3)["nick_border_color"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerBonusDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_bonus(owner_id) VALUES(@id)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static PlayerConfig GetPlayerConfigDB(long OwnerId)
	{
		PlayerConfig playerConfig = null;
		if (OwnerId == 0L)
		{
			return playerConfig;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_configs WHERE owner_id=@owner";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					playerConfig = new PlayerConfig
					{
						OwnerId = OwnerId,
						Config = int.Parse(((DbDataReader)(object)val3)["configs"].ToString()),
						ShowBlood = int.Parse(((DbDataReader)(object)val3)["show_blood"].ToString()),
						Crosshair = int.Parse(((DbDataReader)(object)val3)["crosshair"].ToString()),
						HandPosition = int.Parse(((DbDataReader)(object)val3)["hand_pos"].ToString()),
						AudioSFX = int.Parse(((DbDataReader)(object)val3)["audio_sfx"].ToString()),
						AudioBGM = int.Parse(((DbDataReader)(object)val3)["audio_bgm"].ToString()),
						AudioEnable = int.Parse(((DbDataReader)(object)val3)["audio_enable"].ToString()),
						Sensitivity = int.Parse(((DbDataReader)(object)val3)["sensitivity"].ToString()),
						PointOfView = int.Parse(((DbDataReader)(object)val3)["pov_size"].ToString()),
						InvertMouse = int.Parse(((DbDataReader)(object)val3)["invert_mouse"].ToString()),
						EnableInviteMsg = int.Parse(((DbDataReader)(object)val3)["enable_invite"].ToString()),
						EnableWhisperMsg = int.Parse(((DbDataReader)(object)val3)["enable_whisper"].ToString()),
						Macro = int.Parse(((DbDataReader)(object)val3)["macro_enable"].ToString()),
						Macro1 = ((DbDataReader)(object)val3)["macro1"].ToString(),
						Macro2 = ((DbDataReader)(object)val3)["macro2"].ToString(),
						Macro3 = ((DbDataReader)(object)val3)["macro3"].ToString(),
						Macro4 = ((DbDataReader)(object)val3)["macro4"].ToString(),
						Macro5 = ((DbDataReader)(object)val3)["macro5"].ToString(),
						Nations = int.Parse(((DbDataReader)(object)val3)["nations"].ToString())
					};
					((DbDataReader)(object)val3).GetBytes(19, 0L, playerConfig.KeyboardKeys, 0, 235);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return playerConfig;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return playerConfig;
		}
	}

	public static bool CreatePlayerConfigDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_configs(owner_id) VALUES(@owner)";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static PlayerEvent GetPlayerEventDB(long OwnerId)
	{
		PlayerEvent result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_events WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new PlayerEvent
					{
						OwnerId = OwnerId,
						LastVisitCheckDay = int.Parse(((DbDataReader)(object)val3)["last_visit_check_day"].ToString()),
						LastVisitSeqType = int.Parse(((DbDataReader)(object)val3)["last_visit_seq_type"].ToString()),
						LastVisitDate = uint.Parse(((DbDataReader)(object)val3)["last_visit_date"].ToString()),
						LastXmasDate = uint.Parse(((DbDataReader)(object)val3)["last_xmas_date"].ToString()),
						LastPlaytimeDate = uint.Parse(((DbDataReader)(object)val3)["last_playtime_date"].ToString()),
						LastPlaytimeValue = int.Parse(((DbDataReader)(object)val3)["last_playtime_value"].ToString()),
						LastPlaytimeFinish = int.Parse(((DbDataReader)(object)val3)["last_playtime_finish"].ToString()),
						LastLoginDate = uint.Parse(((DbDataReader)(object)val3)["last_login_date"].ToString()),
						LastQuestDate = uint.Parse(((DbDataReader)(object)val3)["last_quest_date"].ToString()),
						LastQuestFinish = int.Parse(((DbDataReader)(object)val3)["last_quest_finish"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerEventDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_events (owner_id) VALUES (@id)";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static List<FriendModel> GetPlayerFriendsDB(long OwnerId)
	{
		List<FriendModel> list = new List<FriendModel>();
		if (OwnerId == 0L)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_friends WHERE owner_id=@owner ORDER BY id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					FriendModel item = new FriendModel(long.Parse(((DbDataReader)(object)val3)["id"].ToString()))
					{
						OwnerId = OwnerId,
						ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString()),
						State = int.Parse(((DbDataReader)(object)val3)["state"].ToString()),
						Removed = bool.Parse(((DbDataReader)(object)val3)["removed"].ToString())
					};
					list.Add(item);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static void UpdatePlayerBonus(long PlayerId, int Bonuses, int FreePass)
	{
		if (PlayerId == 0L)
		{
			return;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@id", (object)PlayerId);
				obj.Parameters.AddWithValue("@bonuses", (object)Bonuses);
				obj.Parameters.AddWithValue("@freepass", (object)FreePass);
				((DbCommand)(object)obj).CommandText = "UPDATE player_bonus SET bonuses=@bonuses, free_pass=@freepass WHERE owner_id=@id";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static List<QuickstartModel> GetPlayerQuickstartsDB(long OwnerId)
	{
		List<QuickstartModel> list = new List<QuickstartModel>();
		if (OwnerId == 0L)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_quickstarts WHERE owner_id=@owner;";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					QuickstartModel item = new QuickstartModel
					{
						MapId = byte.Parse(((DbDataReader)(object)val3)["list0_map_id"].ToString()),
						Rule = byte.Parse(((DbDataReader)(object)val3)["list0_map_rule"].ToString()),
						StageOptions = byte.Parse(((DbDataReader)(object)val3)["list0_map_stage"].ToString()),
						Type = byte.Parse(((DbDataReader)(object)val3)["list0_map_type"].ToString())
					};
					list.Add(item);
					QuickstartModel item2 = new QuickstartModel
					{
						MapId = byte.Parse(((DbDataReader)(object)val3)["list1_map_id"].ToString()),
						Rule = byte.Parse(((DbDataReader)(object)val3)["list1_map_rule"].ToString()),
						StageOptions = byte.Parse(((DbDataReader)(object)val3)["list1_map_stage"].ToString()),
						Type = byte.Parse(((DbDataReader)(object)val3)["list1_map_type"].ToString())
					};
					list.Add(item2);
					QuickstartModel item3 = new QuickstartModel
					{
						MapId = byte.Parse(((DbDataReader)(object)val3)["list2_map_id"].ToString()),
						Rule = byte.Parse(((DbDataReader)(object)val3)["list2_map_rule"].ToString()),
						StageOptions = byte.Parse(((DbDataReader)(object)val3)["list2_map_stage"].ToString()),
						Type = byte.Parse(((DbDataReader)(object)val3)["list2_map_type"].ToString())
					};
					list.Add(item3);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static bool CreatePlayerQuickstartsDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_quickstarts(owner_id) VALUES(@owner);";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool IsPlayerNameExist(string Nickname)
	{
		if (string.IsNullOrEmpty(Nickname))
		{
			return true;
		}
		try
		{
			int num = 0;
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@name", (object)Nickname);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM accounts WHERE nickname=@name";
				num = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return num > 0;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static List<NHistoryModel> GetPlayerNickHistory(object Value, int Type)
	{
		List<NHistoryModel> list = new List<NHistoryModel>();
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				string text = ((Type == 0) ? "WHERE new_nick=@valor" : "WHERE owner_id=@valor");
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@valor", Value);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM base_nick_history " + text + " ORDER BY change_date LIMIT 30";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					NHistoryModel item = new NHistoryModel
					{
						ObjectId = long.Parse(((DbDataReader)(object)val3)["object_id"].ToString()),
						OwnerId = long.Parse(((DbDataReader)(object)val3)["owner_id"].ToString()),
						OldNick = ((DbDataReader)(object)val3)["old_nick"].ToString(),
						NewNick = ((DbDataReader)(object)val3)["new_nick"].ToString(),
						ChangeDate = uint.Parse(((DbDataReader)(object)val3)["change_date"].ToString()),
						Motive = ((DbDataReader)(object)val3)["motive"].ToString()
					};
					list.Add(item);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static bool CreatePlayerNickHistory(long OwnerId, string OldNick, string NewNick, string Motive)
	{
		NHistoryModel nHistoryModel = new NHistoryModel
		{
			OwnerId = OwnerId,
			OldNick = OldNick,
			NewNick = NewNick,
			ChangeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
			Motive = Motive
		};
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)nHistoryModel.OwnerId);
				obj.Parameters.AddWithValue("@oldnick", (object)nHistoryModel.OldNick);
				obj.Parameters.AddWithValue("@newnick", (object)nHistoryModel.NewNick);
				obj.Parameters.AddWithValue("@date", (object)(long)nHistoryModel.ChangeDate);
				obj.Parameters.AddWithValue("@motive", (object)nHistoryModel.Motive);
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = "INSERT INTO base_nick_history(owner_id, old_nick, new_nick, change_date, motive) VALUES(@owner, @oldnick, @newnick, @date, @motive)";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool UpdateAccountValuable(long PlayerId, int Gold, int Cash, int Tags)
	{
		if (PlayerId != 0L && (Gold != -1 || Cash != -1 || Tags != -1))
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand val2 = val.CreateCommand();
					((DbConnection)(object)val).Open();
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					val2.Parameters.AddWithValue("@owner", (object)PlayerId);
					string text = "";
					if (Gold > -1)
					{
						val2.Parameters.AddWithValue("@gold", (object)Gold);
						text += "gold=@gold";
					}
					if (Cash > -1)
					{
						val2.Parameters.AddWithValue("@cash", (object)Cash);
						text = text + ((text != "") ? ", " : "") + "cash=@cash";
					}
					if (Tags > -1)
					{
						val2.Parameters.AddWithValue("@tags", (object)Tags);
						text = text + ((text != "") ? ", " : "") + "tags=@tags";
					}
					((DbCommand)(object)val2).CommandText = "UPDATE accounts SET " + text + " WHERE player_id=@owner";
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdatePlayerKD(long OwnerId, int Kills, int Deaths, int Headshots, int Totals)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				obj.Parameters.AddWithValue("@deaths", (object)Deaths);
				obj.Parameters.AddWithValue("@kills", (object)Kills);
				obj.Parameters.AddWithValue("@hs", (object)Headshots);
				obj.Parameters.AddWithValue("@total", (object)Totals);
				((DbCommand)(object)obj).CommandText = "UPDATE player_stat_seasons SET kills_count=@kills, deaths_count=@deaths, headshots_count=@hs, total_kills=@total WHERE owner_id=@owner";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool UpdatePlayerMatches(int Matches, int MatchWins, int MatchLoses, int MatchDraws, int Totals, long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				obj.Parameters.AddWithValue("@partidas", (object)Matches);
				obj.Parameters.AddWithValue("@ganhas", (object)MatchWins);
				obj.Parameters.AddWithValue("@perdidas", (object)MatchLoses);
				obj.Parameters.AddWithValue("@empates", (object)MatchDraws);
				obj.Parameters.AddWithValue("@todaspartidas", (object)Totals);
				((DbCommand)(object)obj).CommandText = "UPDATE player_stat_seasons SET matches=@partidas, match_wins=@ganhas, match_loses=@perdidas, match_draws=@empates, total_matches=@todaspartidas WHERE owner_id=@owner";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool UpdateAccountCash(long OwnerId, int Cash)
	{
		if (OwnerId != 0L && Cash != -1)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					((DbCommand)(object)obj).CommandType = CommandType.Text;
					obj.Parameters.AddWithValue("@owner", (object)OwnerId);
					obj.Parameters.AddWithValue("@cash", (object)Cash);
					((DbCommand)(object)obj).CommandText = "UPDATE accounts SET cash=@cash WHERE player_id=@owner";
					((DbCommand)(object)obj).ExecuteNonQuery();
					((Component)(object)obj).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdateAccountGold(long OwnerId, int Gold)
	{
		if (OwnerId != 0L && Gold != -1)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					((DbCommand)(object)obj).CommandType = CommandType.Text;
					obj.Parameters.AddWithValue("@owner", (object)OwnerId);
					obj.Parameters.AddWithValue("@gold", (object)Gold);
					((DbCommand)(object)obj).CommandText = "UPDATE accounts SET gold=@gold WHERE player_id=@owner";
					((DbCommand)(object)obj).ExecuteNonQuery();
					((Component)(object)obj).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdateAccountTags(long OwnerId, int Tags)
	{
		if (OwnerId != 0L && Tags != -1)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					((DbCommand)(object)obj).CommandType = CommandType.Text;
					obj.Parameters.AddWithValue("@owner", (object)OwnerId);
					obj.Parameters.AddWithValue("@tag", (object)Tags);
					((DbCommand)(object)obj).CommandText = "UPDATE accounts SET tags=@tag WHERE player_id=@owner";
					((DbCommand)(object)obj).ExecuteNonQuery();
					((Component)(object)obj).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static void UpdateCouponEffect(long PlayerId, CouponEffects Effects)
	{
		if (PlayerId != 0L)
		{
			ComDiv.UpdateDB("accounts", "coupon_effect", (long)Effects, "player_id", PlayerId);
		}
	}

	public static int GetRequestClanId(long OwnerId)
	{
		int result = 0;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "SELECT clan_id FROM system_clan_invites WHERE player_id=@owner";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				NpgsqlDataReader val2 = obj.ExecuteReader(CommandBehavior.Default);
				if (((DbDataReader)(object)val2).Read())
				{
					result = int.Parse(((DbDataReader)(object)val2)["clan_id"].ToString());
				}
				((Component)(object)obj).Dispose();
				((DbDataReader)(object)val2).Close();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static int GetRequestClanCount(int ClanId)
	{
		int result = 0;
		if (ClanId == 0)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@clan", (object)ClanId);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
				result = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static List<ClanInvite> GetClanRequestList(int ClanId)
	{
		List<ClanInvite> list = new List<ClanInvite>();
		if (ClanId == 0)
		{
			return list;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@clan", (object)ClanId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_clan_invites WHERE clan_id=@clan";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					ClanInvite item = new ClanInvite
					{
						Id = ClanId,
						PlayerId = long.Parse(((DbDataReader)(object)val3)["player_id"].ToString()),
						InviteDate = uint.Parse(((DbDataReader)(object)val3)["invite_date"].ToString()),
						Text = ((DbDataReader)(object)val3)["text"].ToString()
					};
					list.Add(item);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((DbConnection)(object)val).Close();
				return list;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return list;
		}
	}

	public static int GetPlayerMessagesCount(long OwnerId)
	{
		int result = 0;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
				result = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerMessage(long OwnerId, MessageModel Message)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				obj.Parameters.AddWithValue("@sendid", (object)Message.SenderId);
				obj.Parameters.AddWithValue("@clan", (object)Message.ClanId);
				obj.Parameters.AddWithValue("@sendname", (object)Message.SenderName);
				obj.Parameters.AddWithValue("@text", (object)Message.Text);
				obj.Parameters.AddWithValue("@type", (object)Message.Type);
				obj.Parameters.AddWithValue("@state", (object)Message.State);
				obj.Parameters.AddWithValue("@expire", (object)Message.ExpireDate);
				obj.Parameters.AddWithValue("@cb", (object)(int)Message.ClanNote);
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire)VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
				object obj2 = ((DbCommand)(object)obj).ExecuteScalar();
				Message.ObjectId = (long)obj2;
				((Component)(object)obj).Dispose();
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
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool DeletePlayerFriend(long friendId, long pId)
	{
		return ComDiv.DeleteDB("player_friends", "id", friendId, "owner_id", pId);
	}

	public static void UpdatePlayerFriendState(long ownerId, FriendModel friend)
	{
		ComDiv.UpdateDB("player_friends", "state", friend.State, "owner_id", ownerId, "id", friend.PlayerId);
	}

	public static void UpdatePlayerFriendBlock(long OwnerId, FriendModel Friend)
	{
		ComDiv.UpdateDB("player_friends", "removed", Friend.Removed, "owner_id", OwnerId, "id", Friend.PlayerId);
	}

	public static bool DeleteClanInviteDB(int ClanId, long PlayerId)
	{
		if (PlayerId != 0L && ClanId != 0)
		{
			return ComDiv.DeleteDB("system_clan_invites", "clan_id", ClanId, "player_id", PlayerId);
		}
		return false;
	}

	public static bool DeleteClanInviteDB(long PlayerId)
	{
		if (PlayerId == 0L)
		{
			return false;
		}
		return ComDiv.DeleteDB("system_clan_invites", "player_id", PlayerId);
	}

	public static bool CreateClanInviteInDB(ClanInvite invite)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@clan", (object)invite.Id);
				obj.Parameters.AddWithValue("@player", (object)invite.PlayerId);
				obj.Parameters.AddWithValue("@date", (object)(long)invite.InviteDate);
				obj.Parameters.AddWithValue("@text", (object)invite.Text);
				((DbCommand)(object)obj).CommandText = "INSERT INTO system_clan_invites(clan_id, player_id, invite_date, text)VALUES(@clan,@player,@date,@text)";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static int GetRequestClanInviteCount(int clanId)
	{
		int result = 0;
		if (clanId == 0)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@clan", (object)clanId);
				((DbCommand)(object)obj).CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
				result = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static string GetRequestClanInviteText(int ClanId, long PlayerId)
	{
		string result = null;
		if (ClanId != 0 && PlayerId != 0L)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					obj.Parameters.AddWithValue("@clan", (object)ClanId);
					obj.Parameters.AddWithValue("@player", (object)PlayerId);
					((DbCommand)(object)obj).CommandText = "SELECT text FROM system_clan_invites WHERE clan_id=@clan AND player_id=@player";
					((DbCommand)(object)obj).CommandType = CommandType.Text;
					NpgsqlDataReader val2 = obj.ExecuteReader(CommandBehavior.Default);
					if (((DbDataReader)(object)val2).Read())
					{
						result = ((DbDataReader)(object)val2)["text"].ToString();
					}
					((Component)(object)obj).Dispose();
					((DbDataReader)(object)val2).Close();
					((DbConnection)(object)val).Close();
					return result;
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return result;
			}
		}
		return result;
	}

	public static string GetPlayerIP4Address(long PlayerId)
	{
		string result = "";
		if (PlayerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@player", (object)PlayerId);
				((DbCommand)(object)obj).CommandText = "SELECT ip4_address FROM accounts WHERE player_id=@player";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				NpgsqlDataReader val2 = obj.ExecuteReader(CommandBehavior.Default);
				if (((DbDataReader)(object)val2).Read())
				{
					result = ((DbDataReader)(object)val2)["ip4_address"].ToString();
				}
				((Component)(object)obj).Dispose();
				((DbDataReader)(object)val2).Close();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static PlayerMissions GetPlayerMissionsDB(long OwnerId, int Mission1, int Mission2, int Mission3, int Mission4)
	{
		PlayerMissions playerMissions = null;
		if (OwnerId == 0L)
		{
			return playerMissions;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_missions WHERE owner_id=@owner";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					playerMissions = new PlayerMissions
					{
						OwnerId = OwnerId,
						ActualMission = int.Parse(((DbDataReader)(object)val3)["current_mission"].ToString()),
						Card1 = int.Parse(((DbDataReader)(object)val3)["card1"].ToString()),
						Card2 = int.Parse(((DbDataReader)(object)val3)["card2"].ToString()),
						Card3 = int.Parse(((DbDataReader)(object)val3)["card3"].ToString()),
						Card4 = int.Parse(((DbDataReader)(object)val3)["card4"].ToString()),
						Mission1 = Mission1,
						Mission2 = Mission2,
						Mission3 = Mission3,
						Mission4 = Mission4
					};
					((DbDataReader)(object)val3).GetBytes(6, 0L, playerMissions.List1, 0, 40);
					((DbDataReader)(object)val3).GetBytes(7, 0L, playerMissions.List2, 0, 40);
					((DbDataReader)(object)val3).GetBytes(8, 0L, playerMissions.List3, 0, 40);
					((DbDataReader)(object)val3).GetBytes(9, 0L, playerMissions.List4, 0, 40);
					playerMissions.UpdateSelectedCard();
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return playerMissions;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return playerMissions;
		}
	}

	public static bool CreatePlayerMissionsDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_missions(owner_id) VALUES(@owner)";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static void UpdateCurrentPlayerMissionList(long player_id, PlayerMissions mission)
	{
		byte[] currentMissionList = mission.GetCurrentMissionList();
		ComDiv.UpdateDB("player_missions", $"mission{mission.ActualMission + 1}_raw", currentMissionList, "owner_id", player_id);
	}

	public static bool DeletePlayerCharacter(long ObjectId, long OwnerId)
	{
		if (ObjectId != 0L && OwnerId != 0L)
		{
			return ComDiv.DeleteDB("player_characters", "object_id", ObjectId, "owner_id", OwnerId);
		}
		return false;
	}

	public static bool UpdatePlayerCharacter(int Slot, long ObjectId, long OwnerId)
	{
		return ComDiv.UpdateDB("player_characters", "slot", Slot, "object_id", ObjectId, "owner_id", OwnerId);
	}

	public static bool UpdateEquipedPlayerTitle(long player_id, int index, int titleId)
	{
		return ComDiv.UpdateDB("player_titles", $"equip_slot{index + 1}", titleId, "owner_id", player_id);
	}

	public static void UpdatePlayerTitlesFlags(long player_id, long flags)
	{
		ComDiv.UpdateDB("player_titles", "flags", flags, "owner_id", player_id);
	}

	public static void UpdatePlayerTitleRequi(long player_id, int medalhas, int insignias, int ordens_azuis, int broche)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@pid", (object)player_id);
				obj.Parameters.AddWithValue("@broche", (object)broche);
				obj.Parameters.AddWithValue("@insignias", (object)insignias);
				obj.Parameters.AddWithValue("@medalhas", (object)medalhas);
				obj.Parameters.AddWithValue("@ordensazuis", (object)ordens_azuis);
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = "UPDATE accounts SET ribbon=@broche, ensign=@insignias, medal=@medalhas, master_medal=@ordensazuis WHERE player_id=@pid";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static bool UpdatePlayerMissionId(long player_id, int value, int index)
	{
		return ComDiv.UpdateDB("accounts", $"mission_id{index + 1}", value, "player_id", player_id);
	}

	public static int GetUsedTicket(long OwnerId, string Token)
	{
		int result = 0;
		if (OwnerId != 0L && !string.IsNullOrEmpty(Token))
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					obj.Parameters.AddWithValue("@player", (object)OwnerId);
					obj.Parameters.AddWithValue("@token", (object)Token);
					((DbCommand)(object)obj).CommandText = "SELECT used_count FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
					((DbCommand)(object)obj).CommandType = CommandType.Text;
					NpgsqlDataReader val2 = obj.ExecuteReader(CommandBehavior.Default);
					if (((DbDataReader)(object)val2).Read())
					{
						result = int.Parse(((DbDataReader)(object)val2)["used_count"].ToString());
					}
					((Component)(object)obj).Dispose();
					((DbDataReader)(object)val2).Close();
					((DbConnection)(object)val).Close();
					return result;
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return result;
			}
		}
		return result;
	}

	public static bool IsTicketUsedByPlayer(long OwnerId, string Token)
	{
		bool result = false;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@player", (object)OwnerId);
				obj.Parameters.AddWithValue("@token", (object)Token);
				((DbCommand)(object)obj).CommandText = "SELECT * FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				result = Convert.ToBoolean(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerRedeemHistory(long OwnerId, string Token, int Used)
	{
		if (OwnerId != 0L && !string.IsNullOrEmpty(Token) && Used != 0)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand obj = val.CreateCommand();
					((DbConnection)(object)val).Open();
					obj.Parameters.AddWithValue("@owner", (object)OwnerId);
					obj.Parameters.AddWithValue("@token", (object)Token);
					obj.Parameters.AddWithValue("@used", (object)Used);
					((DbCommand)(object)obj).CommandText = "INSERT INTO base_redeem_history(owner_id, used_token, used_count) VALUES(@owner, @token, @used)";
					((DbCommand)(object)obj).CommandType = CommandType.Text;
					((DbCommand)(object)obj).ExecuteNonQuery();
					((Component)(object)obj).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static PlayerVip GetPlayerVIP(long OwnerId)
	{
		PlayerVip result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@ownerId", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "SELECT * FROM player_vip WHERE owner_id=@ownerId";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				NpgsqlDataReader val2 = obj.ExecuteReader(CommandBehavior.Default);
				if (((DbDataReader)(object)val2).Read())
				{
					result = new PlayerVip
					{
						OwnerId = OwnerId,
						Address = ((DbDataReader)(object)val2)["registered_ip"].ToString(),
						Benefit = ((DbDataReader)(object)val2)["last_benefit"].ToString(),
						Expirate = uint.Parse(((DbDataReader)(object)val2)["expirate"].ToString())
					};
				}
				((Component)(object)obj).Dispose();
				((DbDataReader)(object)val2).Close();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static PlayerReport GetPlayerReportDB(long OwnerId)
	{
		PlayerReport result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					val2.Parameters.AddWithValue("@owner", (object)OwnerId);
					((DbCommand)(object)val2).CommandText = "SELECT * FROM player_reports WHERE owner_id=@owner";
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
					try
					{
						while (((DbDataReader)(object)val3).Read())
						{
							result = new PlayerReport
							{
								OwnerId = OwnerId,
								TicketCount = int.Parse(((DbDataReader)(object)val3)["ticket_count"].ToString()),
								ReportedCount = int.Parse(((DbDataReader)(object)val3)["reported_count"].ToString())
							};
						}
						((DbDataReader)(object)val3).Close();
						((DbConnection)(object)val).Close();
						return result;
					}
					finally
					{
						((IDisposable)val3)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerReportDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					val2.Parameters.AddWithValue("@owner", (object)OwnerId);
					((DbCommand)(object)val2).CommandText = "INSERT INTO player_reports(owner_id) VALUES(@owner)";
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool CreatePlayerReportHistory(long OwnerId, long SenderId, string OwnerNick, string SenderNick, ReportType Type, string Message)
	{
		RHistoryModel rHistoryModel = new RHistoryModel
		{
			OwnerId = OwnerId,
			OwnerNick = OwnerNick,
			SenderId = SenderId,
			SenderNick = SenderNick,
			Date = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
			Type = Type,
			Message = Message
		};
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					val2.Parameters.AddWithValue("@OwnerId", (object)rHistoryModel.OwnerId);
					val2.Parameters.AddWithValue("@OwnerNick", (object)rHistoryModel.OwnerNick);
					val2.Parameters.AddWithValue("@SenderId", (object)rHistoryModel.SenderId);
					val2.Parameters.AddWithValue("@SenderNick", (object)rHistoryModel.SenderNick);
					val2.Parameters.AddWithValue("@Date", (object)(long)rHistoryModel.Date);
					val2.Parameters.AddWithValue("@Type", (object)(int)rHistoryModel.Type);
					val2.Parameters.AddWithValue("@Message", (object)rHistoryModel.Message);
					((DbCommand)(object)val2).CommandText = "INSERT INTO base_report_history(date, owner_id, owner_nick, sender_id, sender_nick, type, message) VALUES(@Date, @OwnerId, @OwnerNick, @SenderId, @SenderNick, @Type, @Message)";
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
					return true;
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static PlayerBattlepass GetPlayerBattlepassDB(long OwnerId)
	{
		PlayerBattlepass result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_battlepass WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new PlayerBattlepass
					{
						Id = int.Parse(((DbDataReader)(object)val3)["id"].ToString()),
						Level = int.Parse(((DbDataReader)(object)val3)["level"].ToString()),
						IsPremium = bool.Parse(((DbDataReader)(object)val3)["premium"].ToString()),
						TotalPoints = int.Parse(((DbDataReader)(object)val3)["total_points"].ToString()),
						DailyPoints = int.Parse(((DbDataReader)(object)val3)["daily_points"].ToString()),
						LastRecord = uint.Parse(((DbDataReader)(object)val3)["last_record"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static PlayerCompetitive GetPlayerCompetitiveDB(long OwnerId)
	{
		PlayerCompetitive result = null;
		if (OwnerId == 0L)
		{
			return result;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				((DbConnection)(object)val).Open();
				val2.Parameters.AddWithValue("@id", (object)OwnerId);
				((DbCommand)(object)val2).CommandText = "SELECT * FROM player_competitive WHERE owner_id=@id";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					result = new PlayerCompetitive
					{
						OwnerId = OwnerId,
						Level = int.Parse(((DbDataReader)(object)val3)["level"].ToString()),
						Points = int.Parse(((DbDataReader)(object)val3)["points"].ToString())
					};
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool CreatePlayerBattlepassDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_battlepass VALUES(@owner);";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool CreatePlayerCompetitiveDB(long OwnerId)
	{
		if (OwnerId == 0L)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				obj.Parameters.AddWithValue("@owner", (object)OwnerId);
				((DbCommand)(object)obj).CommandText = "INSERT INTO player_competitive VALUES(@owner);";
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}
}
