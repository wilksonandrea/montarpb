using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Plugin.Core.SQL
{
	public static class DaoManagerSQL
	{
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
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@name", Name);
					npgsqlCommand.get_Parameters().AddWithValue("@date", (long)((ulong)CreateDate));
					npgsqlCommand.get_Parameters().AddWithValue("@info", ClanInfo);
					npgsqlCommand.get_Parameters().AddWithValue("@best", "0-0");
					npgsqlCommand.CommandText = "INSERT INTO system_clan (name, owner_id, create_date, info, best_exp, best_participants, best_wins, best_kills, best_headshots) VALUES (@name, @owner, @date, @info, @best, @best, @best, @best, @best) RETURNING id";
					ClanId = (int)npgsqlCommand.ExecuteScalar();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				ClanId = -1;
				flag = false;
			}
			return flag;
		}

		public static bool CreateClanInviteInDB(ClanInvite invite)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", invite.Id);
					npgsqlCommand.get_Parameters().AddWithValue("@player", invite.PlayerId);
					npgsqlCommand.get_Parameters().AddWithValue("@date", (long)((ulong)invite.InviteDate));
					npgsqlCommand.get_Parameters().AddWithValue("@text", invite.Text);
					npgsqlCommand.CommandText = "INSERT INTO system_clan_invites(clan_id, player_id, invite_date, text)VALUES(@clan,@player,@date,@text)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreateMessage(long OwnerId, MessageModel Message)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@sendid", Message.SenderId);
					npgsqlCommand.get_Parameters().AddWithValue("@clan", Message.ClanId);
					npgsqlCommand.get_Parameters().AddWithValue("@sendname", Message.SenderName);
					npgsqlCommand.get_Parameters().AddWithValue("@text", Message.Text);
					npgsqlCommand.get_Parameters().AddWithValue("@type", (int)Message.Type);
					npgsqlCommand.get_Parameters().AddWithValue("@state", (int)Message.State);
					npgsqlCommand.get_Parameters().AddWithValue("@expire", Message.ExpireDate);
					npgsqlCommand.get_Parameters().AddWithValue("@cb", (int)Message.ClanNote);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire_date) VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
					Message.ObjectId = (long)npgsqlCommand.ExecuteScalar();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerBattlepassDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_battlepass VALUES(@owner);";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerBonusDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_bonus(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerCharacter(CharacterModel Chara, long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner_id", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@id", Chara.Id);
					npgsqlCommand.get_Parameters().AddWithValue("@slot", Chara.Slot);
					npgsqlCommand.get_Parameters().AddWithValue("@name", Chara.Name);
					npgsqlCommand.get_Parameters().AddWithValue("@createdate", (long)((ulong)Chara.CreateDate));
					npgsqlCommand.get_Parameters().AddWithValue("@playtime", (long)((ulong)Chara.PlayTime));
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO player_characters(owner_id, id, slot, name, create_date, playtime) VALUES(@owner_id, @id, @slot, @name, @createdate, @playtime) RETURNING object_id";
					Chara.ObjectId = (long)npgsqlCommand.ExecuteScalar();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerCompetitiveDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_competitive VALUES(@owner);";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerConfigDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_configs(owner_id) VALUES(@owner)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerEquipmentsDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_equipments(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerEventDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_events (owner_id) VALUES (@id)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

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
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@itmId", Item.Id);
					npgsqlCommand.get_Parameters().AddWithValue("@ItmNm", Item.Name);
					npgsqlCommand.get_Parameters().AddWithValue("@count", (long)((ulong)Item.Count));
					npgsqlCommand.get_Parameters().AddWithValue("@equip", (int)Item.Equip);
					npgsqlCommand.CommandText = "INSERT INTO player_items(owner_id, id, name, count, equip) VALUES(@owner, @itmId, @ItmNm, @count, @equip) RETURNING object_id";
					object obj = npgsqlCommand.ExecuteScalar();
					Item.ObjectId = (Item.Equip != ItemEquipType.Permanent ? (long)obj : Item.ObjectId);
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerMessage(long OwnerId, MessageModel Message)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@sendid", Message.SenderId);
					npgsqlCommand.get_Parameters().AddWithValue("@clan", Message.ClanId);
					npgsqlCommand.get_Parameters().AddWithValue("@sendname", Message.SenderName);
					npgsqlCommand.get_Parameters().AddWithValue("@text", Message.Text);
					npgsqlCommand.get_Parameters().AddWithValue("@type", Message.Type);
					npgsqlCommand.get_Parameters().AddWithValue("@state", Message.State);
					npgsqlCommand.get_Parameters().AddWithValue("@expire", Message.ExpireDate);
					npgsqlCommand.get_Parameters().AddWithValue("@cb", (int)Message.ClanNote);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire)VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
					Message.ObjectId = (long)npgsqlCommand.ExecuteScalar();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerMissionsDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_missions(owner_id) VALUES(@owner)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerNickHistory(long OwnerId, string OldNick, string NewNick, string Motive)
		{
			bool flag;
			NHistoryModel nHistoryModel = new NHistoryModel()
			{
				OwnerId = OwnerId,
				OldNick = OldNick,
				NewNick = NewNick,
				ChangeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
				Motive = Motive
			};
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", nHistoryModel.OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@oldnick", nHistoryModel.OldNick);
					npgsqlCommand.get_Parameters().AddWithValue("@newnick", nHistoryModel.NewNick);
					npgsqlCommand.get_Parameters().AddWithValue("@date", (long)((ulong)nHistoryModel.ChangeDate));
					npgsqlCommand.get_Parameters().AddWithValue("@motive", nHistoryModel.Motive);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "INSERT INTO base_nick_history(owner_id, old_nick, new_nick, change_date, motive) VALUES(@owner, @oldnick, @newnick, @date, @motive)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					flag = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerQuickstartsDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_quickstarts(owner_id) VALUES(@owner);";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerRedeemHistory(long OwnerId, string Token, int Used)
		{
			bool flag;
			if (OwnerId == 0 || string.IsNullOrEmpty(Token) || Used == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@token", Token);
					npgsqlCommand.get_Parameters().AddWithValue("@used", Used);
					npgsqlCommand.CommandText = "INSERT INTO base_redeem_history(owner_id, used_token, used_count) VALUES(@owner, @token, @used)";
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerReportDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerReportHistory(long OwnerId, long SenderId, string OwnerNick, string SenderNick, ReportType Type, string Message)
		{
			bool flag;
			RHistoryModel rHistoryModel = new RHistoryModel()
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
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.get_Parameters().AddWithValue("@OwnerId", rHistoryModel.OwnerId);
						npgsqlCommand.get_Parameters().AddWithValue("@OwnerNick", rHistoryModel.OwnerNick);
						npgsqlCommand.get_Parameters().AddWithValue("@SenderId", rHistoryModel.SenderId);
						npgsqlCommand.get_Parameters().AddWithValue("@SenderNick", rHistoryModel.SenderNick);
						npgsqlCommand.get_Parameters().AddWithValue("@Date", (long)((ulong)rHistoryModel.Date));
						npgsqlCommand.get_Parameters().AddWithValue("@Type", (int)rHistoryModel.Type);
						npgsqlCommand.get_Parameters().AddWithValue("@Message", rHistoryModel.Message);
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatAcemodesDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_acemodes(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatBasicDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_basics(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatBattlecupsDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_battlecups(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatClanDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_clans(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatDailiesDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_dailies(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatSeasonDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_seasons(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerStatWeaponsDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_stat_weapons(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool CreatePlayerTitlesDB(long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "INSERT INTO player_titles(owner_id) VALUES(@id)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool DeleteClanInviteDB(int ClanId, long PlayerId)
		{
			if (PlayerId == 0 || ClanId == 0)
			{
				return false;
			}
			return ComDiv.DeleteDB("system_clan_invites", "clan_id", ClanId, "player_id", PlayerId);
		}

		public static bool DeleteClanInviteDB(long PlayerId)
		{
			if (PlayerId == 0)
			{
				return false;
			}
			return ComDiv.DeleteDB("system_clan_invites", "player_id", PlayerId);
		}

		public static bool DeleteMessage(long ObjectId, long OwnerId)
		{
			if (ObjectId == 0 || OwnerId == 0)
			{
				return false;
			}
			return ComDiv.DeleteDB("player_messages", "object_id", ObjectId, "owner_id", OwnerId);
		}

		public static bool DeleteMessages(List<object> ObjectIds, long OwnerId)
		{
			if (ObjectIds.Count == 0 || OwnerId == 0)
			{
				return false;
			}
			return ComDiv.DeleteDB("player_messages", "object_id", ObjectIds.ToArray(), "owner_id", OwnerId);
		}

		public static bool DeletePlayerCharacter(long ObjectId, long OwnerId)
		{
			if (ObjectId == 0 || OwnerId == 0)
			{
				return false;
			}
			return ComDiv.DeleteDB("player_characters", "object_id", ObjectId, "owner_id", OwnerId);
		}

		public static bool DeletePlayerFriend(long friendId, long pId)
		{
			return ComDiv.DeleteDB("player_friends", "id", friendId, "owner_id", pId);
		}

		public static bool DeletePlayerInventoryItem(long ObjectId, long OwnerId)
		{
			if (ObjectId == 0 || OwnerId == 0)
			{
				return false;
			}
			return ComDiv.DeleteDB("player_items", "object_id", ObjectId, "owner_id", OwnerId);
		}

		public static BanHistory GetAccountBan(long ObjectId)
		{
			BanHistory banHistory = new BanHistory();
			if (ObjectId == 0)
			{
				return banHistory;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@obj", ObjectId);
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
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return null;
			}
			return banHistory;
		}

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
					npgsqlCommand.get_Parameters().AddWithValue("@mac", MAC);
					npgsqlCommand.get_Parameters().AddWithValue("@ip", IP4);
					npgsqlCommand.CommandText = "SELECT * FROM base_ban_history WHERE value in (@mac, @ip)";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						string str = npgsqlDataReader["type"].ToString();
						string str1 = npgsqlDataReader["value"].ToString();
						if (DateTime.Parse(npgsqlDataReader["expire_date"].ToString()) < dateTime)
						{
							continue;
						}
						if (!(str == "MAC") || !(str1 == MAC))
						{
							if (!(str == "IP4") || !(str1 == IP4))
							{
								continue;
							}
							ValidIp4 = true;
						}
						else
						{
							ValidMac = true;
						}
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static int GetClanPlayers(int ClanId)
		{
			int ınt32 = 0;
			if (ClanId == 0)
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM accounts WHERE clan_id=@clan";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static List<ClanInvite> GetClanRequestList(int ClanId)
		{
			List<ClanInvite> clanInvites = new List<ClanInvite>();
			if (ClanId == 0)
			{
				return clanInvites;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT * FROM system_clan_invites WHERE clan_id=@clan";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						clanInvites.Add(new ClanInvite()
						{
							Id = ClanId,
							PlayerId = long.Parse(npgsqlDataReader["player_id"].ToString()),
							InviteDate = uint.Parse(npgsqlDataReader["invite_date"].ToString()),
							Text = npgsqlDataReader["text"].ToString()
						});
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return clanInvites;
		}

		public static List<MessageModel> GetGiftMessages(long OwnerId)
		{
			List<MessageModel> messageModels = new List<MessageModel>();
			if (OwnerId == 0)
			{
				return messageModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						NoteMessageType noteMessageType = (NoteMessageType)int.Parse(npgsqlDataReader["type"].ToString());
						if (noteMessageType != NoteMessageType.Gift)
						{
							continue;
						}
						messageModels.Add(new MessageModel((long)uint.Parse(npgsqlDataReader["expire_date"].ToString()), DateTimeUtil.Now())
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							SenderId = long.Parse(npgsqlDataReader["sender_id"].ToString()),
							SenderName = npgsqlDataReader["sender_name"].ToString(),
							ClanId = int.Parse(npgsqlDataReader["clan_id"].ToString()),
							ClanNote = (NoteMessageClan)int.Parse(npgsqlDataReader["clan_note"].ToString()),
							Text = npgsqlDataReader["text"].ToString(),
							Type = noteMessageType,
							State = (NoteMessageState)int.Parse(npgsqlDataReader["state"].ToString())
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return messageModels;
		}

		public static List<string> GetHwIdList()
		{
			List<string> strs = new List<string>();
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
						string str = npgsqlDataReader["hardware_id"].ToString();
						if (str == null && str.Length == 0)
						{
							continue;
						}
						strs.Add(str);
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return null;
			}
			return strs;
		}

		public static MessageModel GetMessage(long ObjectId, long PlayerId)
		{
			MessageModel messageModel = null;
			if (ObjectId == 0 || PlayerId == 0)
			{
				return messageModel;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@obj", ObjectId);
					npgsqlCommand.get_Parameters().AddWithValue("@owner", PlayerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						messageModel = new MessageModel((long)uint.Parse(npgsqlDataReader["expire_date"].ToString()), DateTimeUtil.Now())
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
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return null;
			}
			return messageModel;
		}

		public static List<MessageModel> GetMessages(long OwnerId)
		{
			List<MessageModel> messageModels = new List<MessageModel>();
			if (OwnerId == 0)
			{
				return messageModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						NoteMessageType noteMessageType = (NoteMessageType)int.Parse(npgsqlDataReader["type"].ToString());
						if (noteMessageType == NoteMessageType.Gift)
						{
							continue;
						}
						messageModels.Add(new MessageModel((long)uint.Parse(npgsqlDataReader["expire_date"].ToString()), DateTimeUtil.Now())
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							SenderId = long.Parse(npgsqlDataReader["sender_id"].ToString()),
							SenderName = npgsqlDataReader["sender_name"].ToString(),
							ClanId = int.Parse(npgsqlDataReader["clan_id"].ToString()),
							ClanNote = (NoteMessageClan)int.Parse(npgsqlDataReader["clan_note"].ToString()),
							Text = npgsqlDataReader["text"].ToString(),
							Type = noteMessageType,
							State = (NoteMessageState)int.Parse(npgsqlDataReader["state"].ToString())
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return messageModels;
		}

		public static int GetMessagesCount(long OwnerId)
		{
			int ınt32 = 0;
			if (OwnerId == 0)
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static PlayerBattlepass GetPlayerBattlepassDB(long OwnerId)
		{
			PlayerBattlepass playerBattlepass = null;
			if (OwnerId == 0)
			{
				return playerBattlepass;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_battlepass WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerBattlepass = new PlayerBattlepass()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerBattlepass;
		}

		public static PlayerBonus GetPlayerBonusDB(long OwnerId)
		{
			PlayerBonus playerBonu = null;
			if (OwnerId == 0)
			{
				return playerBonu;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_bonus WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerBonu = new PlayerBonus()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerBonu;
		}

		public static List<CharacterModel> GetPlayerCharactersDB(long OwnerId)
		{
			List<CharacterModel> characterModels = new List<CharacterModel>();
			if (OwnerId == 0)
			{
				return characterModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@OwnerId", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_characters WHERE owner_id=@OwnerId ORDER BY slot ASC;";
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						characterModels.Add(new CharacterModel()
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							Id = int.Parse(npgsqlDataReader["id"].ToString()),
							Slot = int.Parse(npgsqlDataReader["slot"].ToString()),
							Name = npgsqlDataReader["name"].ToString(),
							CreateDate = uint.Parse(npgsqlDataReader["create_date"].ToString()),
							PlayTime = uint.Parse(npgsqlDataReader["playtime"].ToString())
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return characterModels;
		}

		public static PlayerCompetitive GetPlayerCompetitiveDB(long OwnerId)
		{
			PlayerCompetitive playerCompetitive = null;
			if (OwnerId == 0)
			{
				return playerCompetitive;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_competitive WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerCompetitive = new PlayerCompetitive()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerCompetitive;
		}

		public static PlayerConfig GetPlayerConfigDB(long OwnerId)
		{
			PlayerConfig playerConfig = null;
			if (OwnerId == 0)
			{
				return playerConfig;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_configs WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerConfig = new PlayerConfig()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerConfig;
		}

		public static PlayerEquipment GetPlayerEquipmentsDB(long OwnerId)
		{
			PlayerEquipment playerEquipment = null;
			if (OwnerId == 0)
			{
				return playerEquipment;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_equipments WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerEquipment = new PlayerEquipment()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerEquipment;
		}

		public static PlayerEvent GetPlayerEventDB(long OwnerId)
		{
			PlayerEvent playerEvent = null;
			if (OwnerId == 0)
			{
				return playerEvent;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_events WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerEvent = new PlayerEvent()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerEvent;
		}

		public static List<FriendModel> GetPlayerFriendsDB(long OwnerId)
		{
			List<FriendModel> friendModels = new List<FriendModel>();
			if (OwnerId == 0)
			{
				return friendModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_friends WHERE owner_id=@owner ORDER BY id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						friendModels.Add(new FriendModel(long.Parse(npgsqlDataReader["id"].ToString()))
						{
							OwnerId = OwnerId,
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							State = int.Parse(npgsqlDataReader["state"].ToString()),
							Removed = bool.Parse(npgsqlDataReader["removed"].ToString())
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return friendModels;
		}

		public static List<ItemsModel> GetPlayerInventoryItems(long OwnerId)
		{
			List<ItemsModel> ıtemsModels = new List<ItemsModel>();
			if (OwnerId == 0)
			{
				return ıtemsModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_items WHERE owner_id=@owner ORDER BY object_id ASC;";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						ıtemsModels.Add(new ItemsModel(int.Parse(npgsqlDataReader["id"].ToString()))
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							Name = npgsqlDataReader["name"].ToString(),
							Count = uint.Parse(npgsqlDataReader["count"].ToString()),
							Equip = (ItemEquipType)int.Parse(npgsqlDataReader["equip"].ToString())
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ıtemsModels;
		}

		public static string GetPlayerIP4Address(long PlayerId)
		{
			string str = "";
			if (PlayerId == 0)
			{
				return str;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@player", PlayerId);
					npgsqlCommand.CommandText = "SELECT ip4_address FROM accounts WHERE player_id=@player";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						str = npgsqlDataReader["ip4_address"].ToString();
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return str;
		}

		public static int GetPlayerMessagesCount(long OwnerId)
		{
			int ınt32 = 0;
			if (OwnerId == 0)
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static PlayerMissions GetPlayerMissionsDB(long OwnerId, int Mission1, int Mission2, int Mission3, int Mission4)
		{
			PlayerMissions playerMission = null;
			if (OwnerId == 0)
			{
				return playerMission;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_missions WHERE owner_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerMission = new PlayerMissions()
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
						npgsqlDataReader.GetBytes(6, 0L, playerMission.List1, 0, 40);
						npgsqlDataReader.GetBytes(7, 0L, playerMission.List2, 0, 40);
						npgsqlDataReader.GetBytes(8, 0L, playerMission.List3, 0, 40);
						npgsqlDataReader.GetBytes(9, 0L, playerMission.List4, 0, 40);
						playerMission.UpdateSelectedCard();
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerMission;
		}

		public static List<NHistoryModel> GetPlayerNickHistory(object Value, int Type)
		{
			List<NHistoryModel> nHistoryModels = new List<NHistoryModel>();
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					string str = (Type == 0 ? "WHERE new_nick=@valor" : "WHERE owner_id=@valor");
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@valor", Value);
					npgsqlCommand.CommandText = string.Concat("SELECT * FROM base_nick_history ", str, " ORDER BY change_date LIMIT 30");
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						nHistoryModels.Add(new NHistoryModel()
						{
							ObjectId = long.Parse(npgsqlDataReader["object_id"].ToString()),
							OwnerId = long.Parse(npgsqlDataReader["owner_id"].ToString()),
							OldNick = npgsqlDataReader["old_nick"].ToString(),
							NewNick = npgsqlDataReader["new_nick"].ToString(),
							ChangeDate = uint.Parse(npgsqlDataReader["change_date"].ToString()),
							Motive = npgsqlDataReader["motive"].ToString()
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return nHistoryModels;
		}

		public static List<QuickstartModel> GetPlayerQuickstartsDB(long OwnerId)
		{
			List<QuickstartModel> quickstartModels = new List<QuickstartModel>();
			if (OwnerId == 0)
			{
				return quickstartModels;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_quickstarts WHERE owner_id=@owner;";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						quickstartModels.Add(new QuickstartModel()
						{
							MapId = byte.Parse(npgsqlDataReader["list0_map_id"].ToString()),
							Rule = byte.Parse(npgsqlDataReader["list0_map_rule"].ToString()),
							StageOptions = byte.Parse(npgsqlDataReader["list0_map_stage"].ToString()),
							Type = byte.Parse(npgsqlDataReader["list0_map_type"].ToString())
						});
						quickstartModels.Add(new QuickstartModel()
						{
							MapId = byte.Parse(npgsqlDataReader["list1_map_id"].ToString()),
							Rule = byte.Parse(npgsqlDataReader["list1_map_rule"].ToString()),
							StageOptions = byte.Parse(npgsqlDataReader["list1_map_stage"].ToString()),
							Type = byte.Parse(npgsqlDataReader["list1_map_type"].ToString())
						});
						quickstartModels.Add(new QuickstartModel()
						{
							MapId = byte.Parse(npgsqlDataReader["list2_map_id"].ToString()),
							Rule = byte.Parse(npgsqlDataReader["list2_map_rule"].ToString()),
							StageOptions = byte.Parse(npgsqlDataReader["list2_map_stage"].ToString()),
							Type = byte.Parse(npgsqlDataReader["list2_map_type"].ToString())
						});
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
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return quickstartModels;
		}

		public static PlayerReport GetPlayerReportDB(long OwnerId)
		{
			PlayerReport playerReport = null;
			if (OwnerId == 0)
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
						npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
						npgsqlCommand.CommandText = "SELECT * FROM player_reports WHERE owner_id=@owner";
						npgsqlCommand.CommandType = CommandType.Text;
						using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default))
						{
							while (npgsqlDataReader.Read())
							{
								playerReport = new PlayerReport()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerReport;
		}

		public static StatisticAcemode GetPlayerStatAcemodesDB(long OwnerId)
		{
			StatisticAcemode statisticAcemode = null;
			if (OwnerId == 0)
			{
				return statisticAcemode;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_acemodes WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticAcemode = new StatisticAcemode()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticAcemode;
		}

		public static StatisticTotal GetPlayerStatBasicDB(long OwnerId)
		{
			StatisticTotal statisticTotal = null;
			if (OwnerId == 0)
			{
				return statisticTotal;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_basics WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticTotal = new StatisticTotal()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticTotal;
		}

		public static StatisticBattlecup GetPlayerStatBattlecupDB(long OwnerId)
		{
			StatisticBattlecup statisticBattlecup = null;
			if (OwnerId == 0)
			{
				return statisticBattlecup;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_battlecups WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticBattlecup = new StatisticBattlecup()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticBattlecup;
		}

		public static StatisticClan GetPlayerStatClanDB(long OwnerId)
		{
			StatisticClan statisticClan = null;
			if (OwnerId == 0)
			{
				return statisticClan;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_clans WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticClan = new StatisticClan()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticClan;
		}

		public static StatisticDaily GetPlayerStatDailiesDB(long OwnerId)
		{
			StatisticDaily statisticDaily = null;
			if (OwnerId == 0)
			{
				return statisticDaily;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_dailies WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticDaily = new StatisticDaily()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticDaily;
		}

		public static StatisticSeason GetPlayerStatSeasonDB(long OwnerId)
		{
			StatisticSeason statisticSeason = null;
			if (OwnerId == 0)
			{
				return statisticSeason;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_seasons WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticSeason = new StatisticSeason()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticSeason;
		}

		public static StatisticWeapon GetPlayerStatWeaponsDB(long OwnerId)
		{
			StatisticWeapon statisticWeapon = null;
			if (OwnerId == 0)
			{
				return statisticWeapon;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_stat_weapons WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						statisticWeapon = new StatisticWeapon()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return statisticWeapon;
		}

		public static PlayerTitles GetPlayerTitlesDB(long OwnerId)
		{
			PlayerTitles playerTitle = null;
			if (OwnerId == 0)
			{
				return playerTitle;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_titles WHERE owner_id=@id";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						playerTitle = new PlayerTitles()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerTitle;
		}

		public static PlayerVip GetPlayerVIP(long OwnerId)
		{
			PlayerVip playerVip = null;
			if (OwnerId == 0)
			{
				return playerVip;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@ownerId", OwnerId);
					npgsqlCommand.CommandText = "SELECT * FROM player_vip WHERE owner_id=@ownerId";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						playerVip = new PlayerVip()
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return playerVip;
		}

		public static int GetRequestClanCount(int ClanId)
		{
			int ınt32 = 0;
			if (ClanId == 0)
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static int GetRequestClanId(long OwnerId)
		{
			int ınt32 = 0;
			if (OwnerId == 0)
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT clan_id FROM system_clan_invites WHERE player_id=@owner";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						ınt32 = int.Parse(npgsqlDataReader["clan_id"].ToString());
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static int GetRequestClanInviteCount(int clanId)
		{
			int ınt32 = 0;
			if (clanId == 0)
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", clanId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static string GetRequestClanInviteText(int ClanId, long PlayerId)
		{
			string str = null;
			if (ClanId == 0 || PlayerId == 0)
			{
				return str;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.get_Parameters().AddWithValue("@player", PlayerId);
					npgsqlCommand.CommandText = "SELECT text FROM system_clan_invites WHERE clan_id=@clan AND player_id=@player";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						str = npgsqlDataReader["text"].ToString();
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return str;
		}

		public static int GetUsedTicket(long OwnerId, string Token)
		{
			int ınt32 = 0;
			if (OwnerId == 0 || string.IsNullOrEmpty(Token))
			{
				return ınt32;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@player", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@token", Token);
					npgsqlCommand.CommandText = "SELECT used_count FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					if (npgsqlDataReader.Read())
					{
						ınt32 = int.Parse(npgsqlDataReader["used_count"].ToString());
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static bool IsPlayerNameExist(string Nickname)
		{
			bool flag;
			if (string.IsNullOrEmpty(Nickname))
			{
				return true;
			}
			try
			{
				int ınt32 = 0;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@name", Nickname);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM accounts WHERE nickname=@name";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = ınt32 > 0;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool IsTicketUsedByPlayer(long OwnerId, string Token)
		{
			bool flag = false;
			if (OwnerId == 0)
			{
				return flag;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@player", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@token", Token);
					npgsqlCommand.CommandText = "SELECT * FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
					npgsqlCommand.CommandType = CommandType.Text;
					flag = Convert.ToBoolean(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return flag;
		}

		public static bool MessageExists(long ObjectId, long OwnerId)
		{
			bool flag;
			if (ObjectId == 0 || OwnerId == 0)
			{
				return false;
			}
			try
			{
				int ınt32 = 0;
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@obj", ObjectId);
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.CommandText = "SELECT COUNT(*) FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
					ınt32 = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = ınt32 > 0;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return false;
			}
			return flag;
		}

		public static void RecycleMessages(long OwnerId, List<MessageModel> Messages)
		{
			List<object> objs = new List<object>();
			for (int i = 0; i < Messages.Count; i++)
			{
				MessageModel ıtem = Messages[i];
				if (ıtem.DaysRemaining == 0)
				{
					objs.Add(ıtem.ObjectId);
					int ınt32 = i;
					i = ınt32 - 1;
					Messages.RemoveAt(ınt32);
				}
			}
			DaoManagerSQL.DeleteMessages(objs, OwnerId);
		}

		public static bool SaveAutoBan(long PlayerId, string Username, string Nickname, string Type, string Time, string Address, string HackType)
		{
			bool flag;
			if (PlayerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@player_id", PlayerId);
					npgsqlCommand.get_Parameters().AddWithValue("@login", Username);
					npgsqlCommand.get_Parameters().AddWithValue("@player_name", Nickname);
					npgsqlCommand.get_Parameters().AddWithValue("@type", Type);
					npgsqlCommand.get_Parameters().AddWithValue("@time", Time);
					npgsqlCommand.get_Parameters().AddWithValue("@ip", Address);
					npgsqlCommand.get_Parameters().AddWithValue("@hack_type", HackType);
					npgsqlCommand.CommandText = "INSERT INTO base_auto_ban(owner_id, username, nickname, type, time, ip4_address, hack_type) VALUES(@player_id, @login, @player_name, @type, @time, @ip, @hack_type)";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static BanHistory SaveBanHistory(long PlayerId, string Type, string Value, DateTime EndDate)
		{
			BanHistory banHistory;
			BanHistory banHistory1 = new BanHistory()
			{
				PlayerId = PlayerId,
				Type = Type,
				Value = Value,
				EndDate = EndDate
			};
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@provider", banHistory1.PlayerId);
					npgsqlCommand.get_Parameters().AddWithValue("@type", banHistory1.Type);
					npgsqlCommand.get_Parameters().AddWithValue("@value", banHistory1.Value);
					npgsqlCommand.get_Parameters().AddWithValue("@reason", banHistory1.Reason);
					npgsqlCommand.get_Parameters().AddWithValue("@start", banHistory1.StartDate);
					npgsqlCommand.get_Parameters().AddWithValue("@end", banHistory1.EndDate);
					npgsqlCommand.CommandText = "INSERT INTO base_ban_history(owner_id, type, value, reason, start_date, expire_date) VALUES(@provider, @type, @value, @reason, @start, @end) RETURNING object_id";
					banHistory1.ObjectId = (long)npgsqlCommand.ExecuteScalar();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
					banHistory = banHistory1;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				banHistory = null;
			}
			return banHistory;
		}

		public static bool SaveBanReason(long ObjectId, string Reason)
		{
			if (ObjectId == 0)
			{
				return false;
			}
			return ComDiv.UpdateDB("base_ban_history", "reason", Reason, "object_id", ObjectId);
		}

		public static bool UpdateAccountCash(long OwnerId, int Cash)
		{
			bool flag;
			if (OwnerId != 0)
			{
				if (Cash != -1)
				{
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
							npgsqlCommand.get_Parameters().AddWithValue("@cash", Cash);
							npgsqlCommand.CommandText = "UPDATE accounts SET cash=@cash WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		public static bool UpdateAccountGold(long OwnerId, int Gold)
		{
			bool flag;
			if (OwnerId != 0)
			{
				if (Gold != -1)
				{
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
							npgsqlCommand.get_Parameters().AddWithValue("@gold", Gold);
							npgsqlCommand.CommandText = "UPDATE accounts SET gold=@gold WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		public static bool UpdateAccountTags(long OwnerId, int Tags)
		{
			bool flag;
			if (OwnerId != 0)
			{
				if (Tags != -1)
				{
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
							npgsqlCommand.get_Parameters().AddWithValue("@tag", Tags);
							npgsqlCommand.CommandText = "UPDATE accounts SET tags=@tag WHERE player_id=@owner";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		public static bool UpdateAccountValuable(long PlayerId, int Gold, int Cash, int Tags)
		{
			bool flag;
			if (PlayerId != 0)
			{
				if (Gold != -1 || Cash != -1 || Tags != -1)
				{
					try
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.get_Parameters().AddWithValue("@owner", PlayerId);
							string str = "";
							if (Gold > -1)
							{
								npgsqlCommand.get_Parameters().AddWithValue("@gold", Gold);
								str = string.Concat(str, "gold=@gold");
							}
							if (Cash > -1)
							{
								npgsqlCommand.get_Parameters().AddWithValue("@cash", Cash);
								str = string.Concat(str, (str != "" ? ", " : ""), "cash=@cash");
							}
							if (Tags > -1)
							{
								npgsqlCommand.get_Parameters().AddWithValue("@tags", Tags);
								str = string.Concat(str, (str != "" ? ", " : ""), "tags=@tags");
							}
							npgsqlCommand.CommandText = string.Concat("UPDATE accounts SET ", str, " WHERE player_id=@owner");
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						flag = true;
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
						flag = false;
					}
					return flag;
				}
			}
			return false;
		}

		public static bool UpdateClanBattles(int ClanId, int Matches, int Wins, int Loses)
		{
			bool flag;
			if (ClanId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@clan", ClanId);
					npgsqlCommand.get_Parameters().AddWithValue("@partidas", Matches);
					npgsqlCommand.get_Parameters().AddWithValue("@vitorias", Wins);
					npgsqlCommand.get_Parameters().AddWithValue("@derrotas", Loses);
					npgsqlCommand.CommandText = "UPDATE system_clan SET matches=@partidas, match_wins=@vitorias, match_loses=@derrotas WHERE id=@clan";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static void UpdateClanBestPlayers(ClanModel Clan)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@id", Clan.Id);
					npgsqlCommand.get_Parameters().AddWithValue("@bp1", Clan.BestPlayers.Exp.GetSplit());
					npgsqlCommand.get_Parameters().AddWithValue("@bp2", Clan.BestPlayers.Participation.GetSplit());
					npgsqlCommand.get_Parameters().AddWithValue("@bp3", Clan.BestPlayers.Wins.GetSplit());
					npgsqlCommand.get_Parameters().AddWithValue("@bp4", Clan.BestPlayers.Kills.GetSplit());
					npgsqlCommand.get_Parameters().AddWithValue("@bp5", Clan.BestPlayers.Headshots.GetSplit());
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "UPDATE system_clan SET best_exp=@bp1, best_participants=@bp2, best_wins=@bp3, best_kills=@bp4, best_headshots=@bp5 WHERE id=@id";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static bool UpdateClanExp(int ClanId, int Exp)
		{
			if (ClanId == 0)
			{
				return false;
			}
			return ComDiv.UpdateDB("system_clan", "exp", Exp, "id", ClanId);
		}

		public static bool UpdateClanInfo(int ClanId, int Authority, int RankLimit, int MinAge, int MaxAge, int JoinType)
		{
			bool flag;
			if (ClanId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@ClanId", ClanId);
					npgsqlCommand.get_Parameters().AddWithValue("@Authority", Authority);
					npgsqlCommand.get_Parameters().AddWithValue("@RankLimit", RankLimit);
					npgsqlCommand.get_Parameters().AddWithValue("@MinAge", MinAge);
					npgsqlCommand.get_Parameters().AddWithValue("@MaxAge", MaxAge);
					npgsqlCommand.get_Parameters().AddWithValue("@JoinType", JoinType);
					npgsqlCommand.CommandText = "UPDATE system_clan SET authority=@Authority, rank_limit=@RankLimit, min_age_limit=@MinAge, max_age_limit=@MaxAge, join_permission=@JoinType WHERE id=@ClanId";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdateClanLogo(int ClanId, uint logo)
		{
			if (ClanId == 0)
			{
				return false;
			}
			return ComDiv.UpdateDB("system_clan", "logo", (long)((ulong)logo), "id", ClanId);
		}

		public static bool UpdateClanPoints(int ClanId, float Gold)
		{
			if (ClanId == 0)
			{
				return false;
			}
			return ComDiv.UpdateDB("system_clan", "gold", Gold, "id", ClanId);
		}

		public static bool UpdateClanRank(int ClanId, int Rank)
		{
			if (ClanId == 0)
			{
				return false;
			}
			return ComDiv.UpdateDB("system_clan", "rank", Rank, "id", ClanId);
		}

		public static void UpdateCouponEffect(long PlayerId, CouponEffects Effects)
		{
			if (PlayerId == 0)
			{
				return;
			}
			ComDiv.UpdateDB("accounts", "coupon_effect", (long)Effects, "player_id", PlayerId);
		}

		public static void UpdateCurrentPlayerMissionList(long player_id, PlayerMissions mission)
		{
			byte[] currentMissionList = mission.GetCurrentMissionList();
			ComDiv.UpdateDB("player_missions", string.Format("mission{0}_raw", mission.ActualMission + 1), currentMissionList, "owner_id", player_id);
		}

		public static bool UpdateEquipedPlayerTitle(long player_id, int index, int titleId)
		{
			return ComDiv.UpdateDB("player_titles", string.Format("equip_slot{0}", index + 1), titleId, "owner_id", player_id);
		}

		public static void UpdateExpireDate(long ObjectId, long OwnerId, uint Date)
		{
			ComDiv.UpdateDB("player_messages", "expire_date", (long)((ulong)Date), "object_id", ObjectId, "owner_id", OwnerId);
		}

		public static void UpdatePlayerBonus(long PlayerId, int Bonuses, int FreePass)
		{
			if (PlayerId == 0)
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
					npgsqlCommand.get_Parameters().AddWithValue("@id", PlayerId);
					npgsqlCommand.get_Parameters().AddWithValue("@bonuses", Bonuses);
					npgsqlCommand.get_Parameters().AddWithValue("@freepass", FreePass);
					npgsqlCommand.CommandText = "UPDATE player_bonus SET bonuses=@bonuses, free_pass=@freepass WHERE owner_id=@id";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static bool UpdatePlayerCharacter(int Slot, long ObjectId, long OwnerId)
		{
			return ComDiv.UpdateDB("player_characters", "slot", Slot, "object_id", ObjectId, "owner_id", OwnerId);
		}

		public static void UpdatePlayerFriendBlock(long OwnerId, FriendModel Friend)
		{
			ComDiv.UpdateDB("player_friends", "removed", Friend.Removed, "owner_id", OwnerId, "id", Friend.PlayerId);
		}

		public static void UpdatePlayerFriendState(long ownerId, FriendModel friend)
		{
			ComDiv.UpdateDB("player_friends", "state", friend.State, "owner_id", ownerId, "id", friend.PlayerId);
		}

		public static bool UpdatePlayerKD(long OwnerId, int Kills, int Deaths, int Headshots, int Totals)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@deaths", Deaths);
					npgsqlCommand.get_Parameters().AddWithValue("@kills", Kills);
					npgsqlCommand.get_Parameters().AddWithValue("@hs", Headshots);
					npgsqlCommand.get_Parameters().AddWithValue("@total", Totals);
					npgsqlCommand.CommandText = "UPDATE player_stat_seasons SET kills_count=@kills, deaths_count=@deaths, headshots_count=@hs, total_kills=@total WHERE owner_id=@owner";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdatePlayerMatches(int Matches, int MatchWins, int MatchLoses, int MatchDraws, int Totals, long OwnerId)
		{
			bool flag;
			if (OwnerId == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.get_Parameters().AddWithValue("@owner", OwnerId);
					npgsqlCommand.get_Parameters().AddWithValue("@partidas", Matches);
					npgsqlCommand.get_Parameters().AddWithValue("@ganhas", MatchWins);
					npgsqlCommand.get_Parameters().AddWithValue("@perdidas", MatchLoses);
					npgsqlCommand.get_Parameters().AddWithValue("@empates", MatchDraws);
					npgsqlCommand.get_Parameters().AddWithValue("@todaspartidas", Totals);
					npgsqlCommand.CommandText = "UPDATE player_stat_seasons SET matches=@partidas, match_wins=@ganhas, match_loses=@perdidas, match_draws=@empates, total_matches=@todaspartidas WHERE owner_id=@owner";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdatePlayerMissionId(long player_id, int value, int index)
		{
			return ComDiv.UpdateDB("accounts", string.Format("mission_id{0}", index + 1), value, "player_id", player_id);
		}

		public static void UpdatePlayerTitleRequi(long player_id, int medalhas, int insignias, int ordens_azuis, int broche)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.get_Parameters().AddWithValue("@pid", player_id);
					npgsqlCommand.get_Parameters().AddWithValue("@broche", broche);
					npgsqlCommand.get_Parameters().AddWithValue("@insignias", insignias);
					npgsqlCommand.get_Parameters().AddWithValue("@medalhas", medalhas);
					npgsqlCommand.get_Parameters().AddWithValue("@ordensazuis", ordens_azuis);
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = "UPDATE accounts SET ribbon=@broche, ensign=@insignias, medal=@medalhas, master_medal=@ordensazuis WHERE player_id=@pid";
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static void UpdatePlayerTitlesFlags(long player_id, long flags)
		{
			ComDiv.UpdateDB("player_titles", "flags", flags, "owner_id", player_id);
		}

		public static void UpdateState(long ObjectId, long OwnerId, int Value)
		{
			ComDiv.UpdateDB("player_messages", "state", Value, "object_id", ObjectId, "owner_id", OwnerId);
		}
	}
}