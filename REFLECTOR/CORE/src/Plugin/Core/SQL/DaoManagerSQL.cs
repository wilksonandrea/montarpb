namespace Plugin.Core.SQL
{
    using Npgsql;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class DaoManagerSQL
    {
        public static bool CreateClan(out int ClanId, string Name, long OwnerId, string ClanInfo, uint CreateDate)
        {
            try
            {
                ClanId = -1;
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@name", Name);
                    command1.Parameters.AddWithValue("@date", (long) CreateDate);
                    command1.Parameters.AddWithValue("@info", ClanInfo);
                    command1.Parameters.AddWithValue("@best", "0-0");
                    command1.CommandText = "INSERT INTO system_clan (name, owner_id, create_date, info, best_exp, best_participants, best_wins, best_kills, best_headshots) VALUES (@name, @owner, @date, @info, @best, @best, @best, @best, @best) RETURNING id";
                    object obj2 = command1.ExecuteScalar();
                    ClanId = (int) obj2;
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                ClanId = -1;
                return false;
            }
        }

        public static bool CreateClanInviteInDB(ClanInvite invite)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@clan", invite.Id);
                    command1.Parameters.AddWithValue("@player", invite.PlayerId);
                    command1.Parameters.AddWithValue("@date", (long) invite.InviteDate);
                    command1.Parameters.AddWithValue("@text", invite.Text);
                    command1.CommandText = "INSERT INTO system_clan_invites(clan_id, player_id, invite_date, text)VALUES(@clan,@player,@date,@text)";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreateMessage(long OwnerId, MessageModel Message)
        {
            bool flag;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@sendid", Message.SenderId);
                    command1.Parameters.AddWithValue("@clan", Message.ClanId);
                    command1.Parameters.AddWithValue("@sendname", Message.SenderName);
                    command1.Parameters.AddWithValue("@text", Message.Text);
                    command1.Parameters.AddWithValue("@type", (int) Message.Type);
                    command1.Parameters.AddWithValue("@state", (int) Message.State);
                    command1.Parameters.AddWithValue("@expire", Message.ExpireDate);
                    command1.Parameters.AddWithValue("@cb", (int) Message.ClanNote);
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire_date) VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
                    object obj2 = command1.ExecuteScalar();
                    Message.ObjectId = (long) obj2;
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                flag = false;
            }
            return flag;
        }

        public static bool CreatePlayerBattlepassDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.CommandText = "INSERT INTO player_battlepass VALUES(@owner);";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerBonusDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_bonus(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
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
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner_id", OwnerId);
                    command1.Parameters.AddWithValue("@id", Chara.Id);
                    command1.Parameters.AddWithValue("@slot", Chara.Slot);
                    command1.Parameters.AddWithValue("@name", Chara.Name);
                    command1.Parameters.AddWithValue("@createdate", (long) Chara.CreateDate);
                    command1.Parameters.AddWithValue("@playtime", (long) Chara.PlayTime);
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = "INSERT INTO player_characters(owner_id, id, slot, name, create_date, playtime) VALUES(@owner_id, @id, @slot, @name, @createdate, @playtime) RETURNING object_id";
                    object obj2 = command1.ExecuteScalar();
                    Chara.ObjectId = (long) obj2;
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                flag = false;
            }
            return flag;
        }

        public static bool CreatePlayerCompetitiveDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.CommandText = "INSERT INTO player_competitive VALUES(@owner);";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerConfigDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.CommandText = "INSERT INTO player_configs(owner_id) VALUES(@owner)";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerEquipmentsDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_equipments(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerEventDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_events (owner_id) VALUES (@id)";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerInventoryItem(ItemsModel Item, long OwnerId)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@itmId", Item.Id);
                    command1.Parameters.AddWithValue("@ItmNm", Item.Name);
                    command1.Parameters.AddWithValue("@count", (long) Item.Count);
                    command1.Parameters.AddWithValue("@equip", (int) Item.Equip);
                    command1.CommandText = "INSERT INTO player_items(owner_id, id, name, count, equip) VALUES(@owner, @itmId, @ItmNm, @count, @equip) RETURNING object_id";
                    object obj2 = command1.ExecuteScalar();
                    Item.ObjectId = (Item.Equip != ItemEquipType.Permanent) ? ((long) obj2) : Item.ObjectId;
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerMessage(long OwnerId, MessageModel Message)
        {
            bool flag;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@sendid", Message.SenderId);
                    command1.Parameters.AddWithValue("@clan", Message.ClanId);
                    command1.Parameters.AddWithValue("@sendname", Message.SenderName);
                    command1.Parameters.AddWithValue("@text", Message.Text);
                    command1.Parameters.AddWithValue("@type", Message.Type);
                    command1.Parameters.AddWithValue("@state", Message.State);
                    command1.Parameters.AddWithValue("@expire", Message.ExpireDate);
                    command1.Parameters.AddWithValue("@cb", (int) Message.ClanNote);
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire)VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
                    object obj2 = command1.ExecuteScalar();
                    Message.ObjectId = (long) obj2;
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                flag = false;
            }
            return flag;
        }

        public static bool CreatePlayerMissionsDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.CommandText = "INSERT INTO player_missions(owner_id) VALUES(@owner)";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerNickHistory(long OwnerId, string OldNick, string NewNick, string Motive)
        {
            bool flag;
            NHistoryModel model1 = new NHistoryModel();
            model1.OwnerId = OwnerId;
            model1.OldNick = OldNick;
            model1.NewNick = NewNick;
            model1.ChangeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            model1.Motive = Motive;
            NHistoryModel model = model1;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", model.OwnerId);
                    command1.Parameters.AddWithValue("@oldnick", model.OldNick);
                    command1.Parameters.AddWithValue("@newnick", model.NewNick);
                    command1.Parameters.AddWithValue("@date", (long) model.ChangeDate);
                    command1.Parameters.AddWithValue("@motive", model.Motive);
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = "INSERT INTO base_nick_history(owner_id, old_nick, new_nick, change_date, motive) VALUES(@owner, @oldnick, @newnick, @date, @motive)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                flag = false;
            }
            return flag;
        }

        public static bool CreatePlayerQuickstartsDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.CommandText = "INSERT INTO player_quickstarts(owner_id) VALUES(@owner);";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerRedeemHistory(long OwnerId, string Token, int Used)
        {
            if ((OwnerId == 0) || (string.IsNullOrEmpty(Token) || (Used == 0)))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@token", Token);
                    command1.Parameters.AddWithValue("@used", Used);
                    command1.CommandText = "INSERT INTO base_redeem_history(owner_id, used_token, used_count) VALUES(@owner, @token, @used)";
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerReportDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "INSERT INTO player_reports(owner_id) VALUES(@owner)";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerReportHistory(long OwnerId, long SenderId, string OwnerNick, string SenderNick, ReportType Type, string Message)
        {
            bool flag;
            RHistoryModel model1 = new RHistoryModel();
            model1.OwnerId = OwnerId;
            model1.OwnerNick = OwnerNick;
            model1.SenderId = SenderId;
            model1.SenderNick = SenderNick;
            model1.Date = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            model1.Type = Type;
            model1.Message = Message;
            RHistoryModel model = model1;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@OwnerId", model.OwnerId);
                        command.Parameters.AddWithValue("@OwnerNick", model.OwnerNick);
                        command.Parameters.AddWithValue("@SenderId", model.SenderId);
                        command.Parameters.AddWithValue("@SenderNick", model.SenderNick);
                        command.Parameters.AddWithValue("@Date", (long) model.Date);
                        command.Parameters.AddWithValue("@Type", (int) model.Type);
                        command.Parameters.AddWithValue("@Message", model.Message);
                        command.CommandText = "INSERT INTO base_report_history(date, owner_id, owner_nick, sender_id, sender_nick, type, message) VALUES(@Date, @OwnerId, @OwnerNick, @SenderId, @SenderNick, @Type, @Message)";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Dispose();
                        connection.Close();
                        flag = true;
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                flag = false;
            }
            return flag;
        }

        public static bool CreatePlayerStatAcemodesDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_acemodes(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerStatBasicDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_basics(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerStatBattlecupsDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_battlecups(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerStatClanDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_clans(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerStatDailiesDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_dailies(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerStatSeasonDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_seasons(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerStatWeaponsDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_stat_weapons(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool CreatePlayerTitlesDB(long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@id", OwnerId);
                    command1.CommandText = "INSERT INTO player_titles(owner_id) VALUES(@id)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool DeleteClanInviteDB(long PlayerId) => 
            (PlayerId != 0) ? ComDiv.DeleteDB("system_clan_invites", "player_id", PlayerId) : false;

        public static bool DeleteClanInviteDB(int ClanId, long PlayerId) => 
            (PlayerId != 0) && ((ClanId != 0) && ComDiv.DeleteDB("system_clan_invites", "clan_id", ClanId, "player_id", PlayerId));

        public static bool DeleteMessage(long ObjectId, long OwnerId) => 
            (ObjectId != 0) && ((OwnerId != 0) && ComDiv.DeleteDB("player_messages", "object_id", ObjectId, "owner_id", OwnerId));

        public static bool DeleteMessages(List<object> ObjectIds, long OwnerId) => 
            (ObjectIds.Count != 0) && ((OwnerId != 0) && ComDiv.DeleteDB("player_messages", "object_id", ObjectIds.ToArray(), "owner_id", OwnerId));

        public static bool DeletePlayerCharacter(long ObjectId, long OwnerId) => 
            (ObjectId != 0) && ((OwnerId != 0) && ComDiv.DeleteDB("player_characters", "object_id", ObjectId, "owner_id", OwnerId));

        public static bool DeletePlayerFriend(long friendId, long pId) => 
            ComDiv.DeleteDB("player_friends", "id", friendId, "owner_id", pId);

        public static bool DeletePlayerInventoryItem(long ObjectId, long OwnerId) => 
            (ObjectId != 0) && ((OwnerId != 0) && ComDiv.DeleteDB("player_items", "object_id", ObjectId, "owner_id", OwnerId));

        public static BanHistory GetAccountBan(long ObjectId)
        {
            BanHistory history = new BanHistory();
            if (ObjectId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@obj", ObjectId);
                        command.CommandText = "SELECT * FROM base_ban_history WHERE object_id=@obj";
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            history.ObjectId = long.Parse(reader["object_id"].ToString());
                            history.PlayerId = long.Parse(reader["owner_id"].ToString());
                            history.Type = reader["type"].ToString();
                            history.Value = reader["value"].ToString();
                            history.Reason = reader["reason"].ToString();
                            history.StartDate = DateTime.Parse(reader["start_date"].ToString());
                            history.EndDate = DateTime.Parse(reader["expire_date"].ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                    return null;
                }
            }
            return history;
        }

        public static void GetBanStatus(string MAC, string IP4, out bool ValidMac, out bool ValidIp4)
        {
            ValidMac = false;
            ValidIp4 = false;
            try
            {
                DateTime time = DateTimeUtil.Now();
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@mac", MAC);
                    command.Parameters.AddWithValue("@ip", IP4);
                    command.CommandText = "SELECT * FROM base_ban_history WHERE value in (@mac, @ip)";
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        string str = reader["type"].ToString();
                        string str2 = reader["value"].ToString();
                        if (DateTime.Parse(reader["expire_date"].ToString()) >= time)
                        {
                            if ((str == "MAC") && (str2 == MAC))
                            {
                                ValidMac = true;
                                continue;
                            }
                            if ((str == "IP4") && (str2 == IP4))
                            {
                                ValidIp4 = true;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static int GetClanPlayers(int ClanId)
        {
            int num = 0;
            if (ClanId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@clan", ClanId);
                        command1.CommandText = "SELECT COUNT(*) FROM accounts WHERE clan_id=@clan";
                        num = Convert.ToInt32(command1.ExecuteScalar());
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
        }

        public static List<ClanInvite> GetClanRequestList(int ClanId)
        {
            List<ClanInvite> list = new List<ClanInvite>();
            if (ClanId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@clan", ClanId);
                        command.CommandText = "SELECT * FROM system_clan_invites WHERE clan_id=@clan";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Close();
                                break;
                            }
                            ClanInvite invite1 = new ClanInvite();
                            invite1.Id = ClanId;
                            invite1.PlayerId = long.Parse(reader["player_id"].ToString());
                            invite1.InviteDate = uint.Parse(reader["invite_date"].ToString());
                            invite1.Text = reader["text"].ToString();
                            ClanInvite item = invite1;
                            list.Add(item);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static List<MessageModel> GetGiftMessages(long OwnerId)
        {
            List<MessageModel> list = new List<MessageModel>();
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            NoteMessageType type = (NoteMessageType) int.Parse(reader["type"].ToString());
                            if (type == NoteMessageType.Gift)
                            {
                                MessageModel item = new MessageModel((long) uint.Parse(reader["expire_date"].ToString()), DateTimeUtil.Now());
                                item.ObjectId = long.Parse(reader["object_id"].ToString());
                                item.SenderId = long.Parse(reader["sender_id"].ToString());
                                item.SenderName = reader["sender_name"].ToString();
                                item.ClanId = int.Parse(reader["clan_id"].ToString());
                                item.ClanNote = (NoteMessageClan) int.Parse(reader["clan_note"].ToString());
                                item.Text = reader["text"].ToString();
                                item.Type = type;
                                item.State = (NoteMessageState) int.Parse(reader["state"].ToString());
                                list.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static List<string> GetHwIdList()
        {
            List<string> list = new List<string>();
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "SELECT * FROM base_ban_hwid";
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        string item = reader["hardware_id"].ToString();
                        if ((item != null) || (item.Length != 0))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return null;
            }
            return list;
        }

        public static MessageModel GetMessage(long ObjectId, long PlayerId)
        {
            MessageModel model = null;
            if ((ObjectId != 0) && (PlayerId != 0))
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@obj", ObjectId);
                        command.Parameters.AddWithValue("@owner", PlayerId);
                        command.CommandText = "SELECT * FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            MessageModel model1 = new MessageModel((long) uint.Parse(reader["expire_date"].ToString()), DateTimeUtil.Now());
                            model1.ObjectId = ObjectId;
                            model1.SenderId = long.Parse(reader["sender_id"].ToString());
                            model1.SenderName = reader["sender_name"].ToString();
                            model1.ClanId = int.Parse(reader["clan_id"].ToString());
                            model1.ClanNote = (NoteMessageClan) int.Parse(reader["clan_note"].ToString());
                            model1.Text = reader["text"].ToString();
                            model1.Type = (NoteMessageType) int.Parse(reader["type"].ToString());
                            model1.State = (NoteMessageState) int.Parse(reader["state"].ToString());
                            model = model1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                    return null;
                }
            }
            return model;
        }

        public static List<MessageModel> GetMessages(long OwnerId)
        {
            List<MessageModel> list = new List<MessageModel>();
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            NoteMessageType type = (NoteMessageType) int.Parse(reader["type"].ToString());
                            if (type != NoteMessageType.Gift)
                            {
                                MessageModel item = new MessageModel((long) uint.Parse(reader["expire_date"].ToString()), DateTimeUtil.Now());
                                item.ObjectId = long.Parse(reader["object_id"].ToString());
                                item.SenderId = long.Parse(reader["sender_id"].ToString());
                                item.SenderName = reader["sender_name"].ToString();
                                item.ClanId = int.Parse(reader["clan_id"].ToString());
                                item.ClanNote = (NoteMessageClan) int.Parse(reader["clan_note"].ToString());
                                item.Text = reader["text"].ToString();
                                item.Type = type;
                                item.State = (NoteMessageState) int.Parse(reader["state"].ToString());
                                list.Add(item);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static int GetMessagesCount(long OwnerId)
        {
            int num = 0;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@owner", OwnerId);
                        command1.CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
                        num = Convert.ToInt32(command1.ExecuteScalar());
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
        }

        public static PlayerBattlepass GetPlayerBattlepassDB(long OwnerId)
        {
            PlayerBattlepass battlepass = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_battlepass WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerBattlepass battlepass1 = new PlayerBattlepass();
                            battlepass1.Id = int.Parse(reader["id"].ToString());
                            battlepass1.Level = int.Parse(reader["level"].ToString());
                            battlepass1.IsPremium = bool.Parse(reader["premium"].ToString());
                            battlepass1.TotalPoints = int.Parse(reader["total_points"].ToString());
                            battlepass1.DailyPoints = int.Parse(reader["daily_points"].ToString());
                            battlepass1.LastRecord = uint.Parse(reader["last_record"].ToString());
                            battlepass = battlepass1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return battlepass;
        }

        public static PlayerBonus GetPlayerBonusDB(long OwnerId)
        {
            PlayerBonus bonus = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_bonus WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerBonus bonus1 = new PlayerBonus();
                            bonus1.OwnerId = OwnerId;
                            bonus1.Bonuses = int.Parse(reader["bonuses"].ToString());
                            bonus1.CrosshairColor = int.Parse(reader["crosshair_color"].ToString());
                            bonus1.FreePass = int.Parse(reader["free_pass"].ToString());
                            bonus1.FakeRank = int.Parse(reader["fake_rank"].ToString());
                            bonus1.FakeNick = reader["fake_nick"].ToString();
                            bonus1.MuzzleColor = int.Parse(reader["muzzle_color"].ToString());
                            bonus1.NickBorderColor = int.Parse(reader["nick_border_color"].ToString());
                            bonus = bonus1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return bonus;
        }

        public static List<CharacterModel> GetPlayerCharactersDB(long OwnerId)
        {
            List<CharacterModel> list = new List<CharacterModel>();
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@OwnerId", OwnerId);
                        command.CommandText = "SELECT * FROM player_characters WHERE owner_id=@OwnerId ORDER BY slot ASC;";
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            CharacterModel model1 = new CharacterModel();
                            model1.ObjectId = long.Parse(reader["object_id"].ToString());
                            model1.Id = int.Parse(reader["id"].ToString());
                            model1.Slot = int.Parse(reader["slot"].ToString());
                            model1.Name = reader["name"].ToString();
                            model1.CreateDate = uint.Parse(reader["create_date"].ToString());
                            model1.PlayTime = uint.Parse(reader["playtime"].ToString());
                            CharacterModel item = model1;
                            list.Add(item);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static PlayerCompetitive GetPlayerCompetitiveDB(long OwnerId)
        {
            PlayerCompetitive competitive = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_competitive WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerCompetitive competitive1 = new PlayerCompetitive();
                            competitive1.OwnerId = OwnerId;
                            competitive1.Level = int.Parse(reader["level"].ToString());
                            competitive1.Points = int.Parse(reader["points"].ToString());
                            competitive = competitive1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return competitive;
        }

        public static PlayerConfig GetPlayerConfigDB(long OwnerId)
        {
            PlayerConfig config = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_configs WHERE owner_id=@owner";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerConfig config1 = new PlayerConfig();
                            config1.OwnerId = OwnerId;
                            config1.Config = int.Parse(reader["configs"].ToString());
                            config1.ShowBlood = int.Parse(reader["show_blood"].ToString());
                            config1.Crosshair = int.Parse(reader["crosshair"].ToString());
                            config1.HandPosition = int.Parse(reader["hand_pos"].ToString());
                            config1.AudioSFX = int.Parse(reader["audio_sfx"].ToString());
                            config1.AudioBGM = int.Parse(reader["audio_bgm"].ToString());
                            config1.AudioEnable = int.Parse(reader["audio_enable"].ToString());
                            config1.Sensitivity = int.Parse(reader["sensitivity"].ToString());
                            config1.PointOfView = int.Parse(reader["pov_size"].ToString());
                            config1.InvertMouse = int.Parse(reader["invert_mouse"].ToString());
                            config1.EnableInviteMsg = int.Parse(reader["enable_invite"].ToString());
                            config1.EnableWhisperMsg = int.Parse(reader["enable_whisper"].ToString());
                            config1.Macro = int.Parse(reader["macro_enable"].ToString());
                            config1.Macro1 = reader["macro1"].ToString();
                            config1.Macro2 = reader["macro2"].ToString();
                            config1.Macro3 = reader["macro3"].ToString();
                            config1.Macro4 = reader["macro4"].ToString();
                            config1.Macro5 = reader["macro5"].ToString();
                            config1.Nations = int.Parse(reader["nations"].ToString());
                            config = config1;
                            reader.GetBytes(0x13, 0L, config.KeyboardKeys, 0, 0xeb);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return config;
        }

        public static PlayerEquipment GetPlayerEquipmentsDB(long OwnerId)
        {
            PlayerEquipment equipment = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_equipments WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerEquipment equipment1 = new PlayerEquipment();
                            equipment1.OwnerId = OwnerId;
                            equipment1.WeaponPrimary = int.Parse(reader["weapon_primary"].ToString());
                            equipment1.WeaponSecondary = int.Parse(reader["weapon_secondary"].ToString());
                            equipment1.WeaponMelee = int.Parse(reader["weapon_melee"].ToString());
                            equipment1.WeaponExplosive = int.Parse(reader["weapon_explosive"].ToString());
                            equipment1.WeaponSpecial = int.Parse(reader["weapon_special"].ToString());
                            equipment1.CharaRedId = int.Parse(reader["chara_red_side"].ToString());
                            equipment1.CharaBlueId = int.Parse(reader["chara_blue_side"].ToString());
                            equipment1.DinoItem = int.Parse(reader["dino_item_chara"].ToString());
                            equipment1.PartHead = int.Parse(reader["part_head"].ToString());
                            equipment1.PartFace = int.Parse(reader["part_face"].ToString());
                            equipment1.PartJacket = int.Parse(reader["part_jacket"].ToString());
                            equipment1.PartPocket = int.Parse(reader["part_pocket"].ToString());
                            equipment1.PartGlove = int.Parse(reader["part_glove"].ToString());
                            equipment1.PartBelt = int.Parse(reader["part_belt"].ToString());
                            equipment1.PartHolster = int.Parse(reader["part_holster"].ToString());
                            equipment1.PartSkin = int.Parse(reader["part_skin"].ToString());
                            equipment1.BeretItem = int.Parse(reader["beret_item_part"].ToString());
                            equipment1.AccessoryId = int.Parse(reader["accesory_id"].ToString());
                            equipment1.SprayId = int.Parse(reader["spray_id"].ToString());
                            equipment1.NameCardId = int.Parse(reader["namecard_id"].ToString());
                            equipment = equipment1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return equipment;
        }

        public static PlayerEvent GetPlayerEventDB(long OwnerId)
        {
            PlayerEvent event2 = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_events WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerEvent event1 = new PlayerEvent();
                            event1.OwnerId = OwnerId;
                            event1.LastVisitCheckDay = int.Parse(reader["last_visit_check_day"].ToString());
                            event1.LastVisitSeqType = int.Parse(reader["last_visit_seq_type"].ToString());
                            event1.LastVisitDate = uint.Parse(reader["last_visit_date"].ToString());
                            event1.LastXmasDate = uint.Parse(reader["last_xmas_date"].ToString());
                            event1.LastPlaytimeDate = uint.Parse(reader["last_playtime_date"].ToString());
                            event1.LastPlaytimeValue = int.Parse(reader["last_playtime_value"].ToString());
                            event1.LastPlaytimeFinish = int.Parse(reader["last_playtime_finish"].ToString());
                            event1.LastLoginDate = uint.Parse(reader["last_login_date"].ToString());
                            event1.LastQuestDate = uint.Parse(reader["last_quest_date"].ToString());
                            event1.LastQuestFinish = int.Parse(reader["last_quest_finish"].ToString());
                            event2 = event1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return event2;
        }

        public static List<FriendModel> GetPlayerFriendsDB(long OwnerId)
        {
            List<FriendModel> list = new List<FriendModel>();
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_friends WHERE owner_id=@owner ORDER BY id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            FriendModel model1 = new FriendModel(long.Parse(reader["id"].ToString()));
                            model1.OwnerId = OwnerId;
                            model1.ObjectId = long.Parse(reader["object_id"].ToString());
                            model1.State = int.Parse(reader["state"].ToString());
                            model1.Removed = bool.Parse(reader["removed"].ToString());
                            FriendModel item = model1;
                            list.Add(item);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static List<ItemsModel> GetPlayerInventoryItems(long OwnerId)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_items WHERE owner_id=@owner ORDER BY object_id ASC;";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            ItemsModel model1 = new ItemsModel(int.Parse(reader["id"].ToString()));
                            model1.ObjectId = long.Parse(reader["object_id"].ToString());
                            model1.Name = reader["name"].ToString();
                            model1.Count = uint.Parse(reader["count"].ToString());
                            model1.Equip = (ItemEquipType) int.Parse(reader["equip"].ToString());
                            ItemsModel item = model1;
                            list.Add(item);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static string GetPlayerIP4Address(long PlayerId)
        {
            string str = "";
            if (PlayerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@player", PlayerId);
                        command1.CommandText = "SELECT ip4_address FROM accounts WHERE player_id=@player";
                        command1.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command1.ExecuteReader(CommandBehavior.Default);
                        if (reader.Read())
                        {
                            str = reader["ip4_address"].ToString();
                        }
                        command1.Dispose();
                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return str;
        }

        public static int GetPlayerMessagesCount(long OwnerId)
        {
            int num = 0;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@owner", OwnerId);
                        command1.CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
                        num = Convert.ToInt32(command1.ExecuteScalar());
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
        }

        public static PlayerMissions GetPlayerMissionsDB(long OwnerId, int Mission1, int Mission2, int Mission3, int Mission4)
        {
            PlayerMissions missions = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_missions WHERE owner_id=@owner";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerMissions missions1 = new PlayerMissions();
                            missions1.OwnerId = OwnerId;
                            missions1.ActualMission = int.Parse(reader["current_mission"].ToString());
                            missions1.Card1 = int.Parse(reader["card1"].ToString());
                            missions1.Card2 = int.Parse(reader["card2"].ToString());
                            missions1.Card3 = int.Parse(reader["card3"].ToString());
                            missions1.Card4 = int.Parse(reader["card4"].ToString());
                            missions1.Mission1 = Mission1;
                            missions1.Mission2 = Mission2;
                            missions1.Mission3 = Mission3;
                            missions1.Mission4 = Mission4;
                            missions = missions1;
                            reader.GetBytes(6, 0L, missions.List1, 0, 40);
                            reader.GetBytes(7, 0L, missions.List2, 0, 40);
                            reader.GetBytes(8, 0L, missions.List3, 0, 40);
                            reader.GetBytes(9, 0L, missions.List4, 0, 40);
                            missions.UpdateSelectedCard();
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return missions;
        }

        public static List<NHistoryModel> GetPlayerNickHistory(object Value, int Type)
        {
            List<NHistoryModel> list = new List<NHistoryModel>();
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    string str = (Type == 0) ? "WHERE new_nick=@valor" : "WHERE owner_id=@valor";
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@valor", Value);
                    command.CommandText = "SELECT * FROM base_nick_history " + str + " ORDER BY change_date LIMIT 30";
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            command.Dispose();
                            reader.Close();
                            connection.Dispose();
                            connection.Close();
                            break;
                        }
                        NHistoryModel model1 = new NHistoryModel();
                        model1.ObjectId = long.Parse(reader["object_id"].ToString());
                        model1.OwnerId = long.Parse(reader["owner_id"].ToString());
                        model1.OldNick = reader["old_nick"].ToString();
                        model1.NewNick = reader["new_nick"].ToString();
                        model1.ChangeDate = uint.Parse(reader["change_date"].ToString());
                        model1.Motive = reader["motive"].ToString();
                        NHistoryModel item = model1;
                        list.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
            return list;
        }

        public static List<QuickstartModel> GetPlayerQuickstartsDB(long OwnerId)
        {
            List<QuickstartModel> list = new List<QuickstartModel>();
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@owner", OwnerId);
                        command.CommandText = "SELECT * FROM player_quickstarts WHERE owner_id=@owner;";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            QuickstartModel model1 = new QuickstartModel();
                            model1.MapId = byte.Parse(reader["list0_map_id"].ToString());
                            model1.Rule = byte.Parse(reader["list0_map_rule"].ToString());
                            model1.StageOptions = byte.Parse(reader["list0_map_stage"].ToString());
                            model1.Type = byte.Parse(reader["list0_map_type"].ToString());
                            QuickstartModel item = model1;
                            list.Add(item);
                            QuickstartModel model4 = new QuickstartModel();
                            model4.MapId = byte.Parse(reader["list1_map_id"].ToString());
                            model4.Rule = byte.Parse(reader["list1_map_rule"].ToString());
                            model4.StageOptions = byte.Parse(reader["list1_map_stage"].ToString());
                            model4.Type = byte.Parse(reader["list1_map_type"].ToString());
                            QuickstartModel model2 = model4;
                            list.Add(model2);
                            QuickstartModel model5 = new QuickstartModel();
                            model5.MapId = byte.Parse(reader["list2_map_id"].ToString());
                            model5.Rule = byte.Parse(reader["list2_map_rule"].ToString());
                            model5.StageOptions = byte.Parse(reader["list2_map_stage"].ToString());
                            model5.Type = byte.Parse(reader["list2_map_type"].ToString());
                            QuickstartModel model3 = model5;
                            list.Add(model3);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return list;
        }

        public static PlayerReport GetPlayerReportDB(long OwnerId)
        {
            PlayerReport report = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        using (NpgsqlCommand command = connection.CreateCommand())
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@owner", OwnerId);
                            command.CommandText = "SELECT * FROM player_reports WHERE owner_id=@owner";
                            command.CommandType = CommandType.Text;
                            using (NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default))
                            {
                                while (true)
                                {
                                    if (!reader.Read())
                                    {
                                        reader.Close();
                                        connection.Close();
                                        break;
                                    }
                                    PlayerReport report1 = new PlayerReport();
                                    report1.OwnerId = OwnerId;
                                    report1.TicketCount = int.Parse(reader["ticket_count"].ToString());
                                    report1.ReportedCount = int.Parse(reader["reported_count"].ToString());
                                    report = report1;
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return report;
        }

        public static StatisticAcemode GetPlayerStatAcemodesDB(long OwnerId)
        {
            StatisticAcemode acemode = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_acemodes WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticAcemode acemode1 = new StatisticAcemode();
                            acemode1.OwnerId = OwnerId;
                            acemode1.Matches = int.Parse(reader["matches"].ToString());
                            acemode1.MatchWins = int.Parse(reader["match_wins"].ToString());
                            acemode1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                            acemode1.Kills = int.Parse(reader["kills_count"].ToString());
                            acemode1.Deaths = int.Parse(reader["deaths_count"].ToString());
                            acemode1.Headshots = int.Parse(reader["headshots_count"].ToString());
                            acemode1.Assists = int.Parse(reader["assists_count"].ToString());
                            acemode1.Escapes = int.Parse(reader["escapes_count"].ToString());
                            acemode1.Winstreaks = int.Parse(reader["winstreaks_count"].ToString());
                            acemode = acemode1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return acemode;
        }

        public static StatisticTotal GetPlayerStatBasicDB(long OwnerId)
        {
            StatisticTotal total = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_basics WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticTotal total1 = new StatisticTotal();
                            total1.OwnerId = OwnerId;
                            total1.Matches = int.Parse(reader["matches"].ToString());
                            total1.MatchWins = int.Parse(reader["match_wins"].ToString());
                            total1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                            total1.MatchDraws = int.Parse(reader["match_draws"].ToString());
                            total1.KillsCount = int.Parse(reader["kills_count"].ToString());
                            total1.DeathsCount = int.Parse(reader["deaths_count"].ToString());
                            total1.HeadshotsCount = int.Parse(reader["headshots_count"].ToString());
                            total1.AssistsCount = int.Parse(reader["assists_count"].ToString());
                            total1.EscapesCount = int.Parse(reader["escapes_count"].ToString());
                            total1.MvpCount = int.Parse(reader["mvp_count"].ToString());
                            total1.TotalMatchesCount = int.Parse(reader["total_matches"].ToString());
                            total1.TotalKillsCount = int.Parse(reader["total_kills"].ToString());
                            total = total1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return total;
        }

        public static StatisticBattlecup GetPlayerStatBattlecupDB(long OwnerId)
        {
            StatisticBattlecup battlecup = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_battlecups WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticBattlecup battlecup1 = new StatisticBattlecup();
                            battlecup1.OwnerId = OwnerId;
                            battlecup1.Matches = int.Parse(reader["matches"].ToString());
                            battlecup1.MatchWins = int.Parse(reader["match_wins"].ToString());
                            battlecup1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                            battlecup1.KillsCount = int.Parse(reader["kills_count"].ToString());
                            battlecup1.DeathsCount = int.Parse(reader["deaths_count"].ToString());
                            battlecup1.HeadshotsCount = int.Parse(reader["headshots_count"].ToString());
                            battlecup1.AssistsCount = int.Parse(reader["assists_count"].ToString());
                            battlecup1.EscapesCount = int.Parse(reader["escapes_count"].ToString());
                            battlecup = battlecup1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return battlecup;
        }

        public static StatisticClan GetPlayerStatClanDB(long OwnerId)
        {
            StatisticClan clan = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_clans WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticClan clan1 = new StatisticClan();
                            clan1.OwnerId = OwnerId;
                            clan1.Matches = int.Parse(reader["clan_matches"].ToString());
                            clan1.MatchWins = int.Parse(reader["clan_match_wins"].ToString());
                            clan1.MatchLoses = int.Parse(reader["clan_match_loses"].ToString());
                            clan = clan1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return clan;
        }

        public static StatisticDaily GetPlayerStatDailiesDB(long OwnerId)
        {
            StatisticDaily daily = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_dailies WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticDaily daily1 = new StatisticDaily();
                            daily1.OwnerId = OwnerId;
                            daily1.Matches = int.Parse(reader["matches"].ToString());
                            daily1.MatchWins = int.Parse(reader["match_wins"].ToString());
                            daily1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                            daily1.MatchDraws = int.Parse(reader["match_draws"].ToString());
                            daily1.KillsCount = int.Parse(reader["kills_count"].ToString());
                            daily1.DeathsCount = int.Parse(reader["deaths_count"].ToString());
                            daily1.HeadshotsCount = int.Parse(reader["headshots_count"].ToString());
                            daily1.ExpGained = int.Parse(reader["exp_gained"].ToString());
                            daily1.PointGained = int.Parse(reader["point_gained"].ToString());
                            daily1.Playtime = uint.Parse($"{reader["playtime"]}");
                            daily = daily1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return daily;
        }

        public static StatisticSeason GetPlayerStatSeasonDB(long OwnerId)
        {
            StatisticSeason season = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_seasons WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticSeason season1 = new StatisticSeason();
                            season1.OwnerId = OwnerId;
                            season1.Matches = int.Parse(reader["matches"].ToString());
                            season1.MatchWins = int.Parse(reader["match_wins"].ToString());
                            season1.MatchLoses = int.Parse(reader["match_loses"].ToString());
                            season1.MatchDraws = int.Parse(reader["match_draws"].ToString());
                            season1.KillsCount = int.Parse(reader["kills_count"].ToString());
                            season1.DeathsCount = int.Parse(reader["deaths_count"].ToString());
                            season1.HeadshotsCount = int.Parse(reader["headshots_count"].ToString());
                            season1.AssistsCount = int.Parse(reader["assists_count"].ToString());
                            season1.EscapesCount = int.Parse(reader["escapes_count"].ToString());
                            season1.MvpCount = int.Parse(reader["mvp_count"].ToString());
                            season1.TotalMatchesCount = int.Parse(reader["total_matches"].ToString());
                            season1.TotalKillsCount = int.Parse(reader["total_kills"].ToString());
                            season = season1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return season;
        }

        public static StatisticWeapon GetPlayerStatWeaponsDB(long OwnerId)
        {
            StatisticWeapon weapon = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_stat_weapons WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            StatisticWeapon weapon1 = new StatisticWeapon();
                            weapon1.OwnerId = OwnerId;
                            weapon1.AssaultKills = int.Parse(reader["assault_rifle_kills"].ToString());
                            weapon1.AssaultDeaths = int.Parse(reader["assault_rifle_deaths"].ToString());
                            weapon1.SmgKills = int.Parse(reader["sub_machine_gun_kills"].ToString());
                            weapon1.SmgDeaths = int.Parse(reader["sub_machine_gun_deaths"].ToString());
                            weapon1.SniperKills = int.Parse(reader["sniper_rifle_kills"].ToString());
                            weapon1.SniperDeaths = int.Parse(reader["sniper_rifle_deaths"].ToString());
                            weapon1.MachinegunKills = int.Parse(reader["machine_gun_kills"].ToString());
                            weapon1.MachinegunDeaths = int.Parse(reader["machine_gun_deaths"].ToString());
                            weapon1.ShotgunKills = int.Parse(reader["shot_gun_kills"].ToString());
                            weapon1.ShotgunDeaths = int.Parse(reader["shot_gun_deaths"].ToString());
                            weapon = weapon1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return weapon;
        }

        public static PlayerTitles GetPlayerTitlesDB(long OwnerId)
        {
            PlayerTitles titles = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        NpgsqlCommand command = connection.CreateCommand();
                        connection.Open();
                        command.Parameters.AddWithValue("@id", OwnerId);
                        command.CommandText = "SELECT * FROM player_titles WHERE owner_id=@id";
                        command.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.Default);
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                command.Dispose();
                                reader.Close();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            PlayerTitles titles1 = new PlayerTitles();
                            titles1.OwnerId = OwnerId;
                            titles1.Equiped1 = int.Parse(reader["equip_slot1"].ToString());
                            titles1.Equiped2 = int.Parse(reader["equip_slot2"].ToString());
                            titles1.Equiped3 = int.Parse(reader["equip_slot3"].ToString());
                            titles1.Flags = long.Parse(reader["flags"].ToString());
                            titles1.Slots = int.Parse(reader["slots"].ToString());
                            titles = titles1;
                        }
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return titles;
        }

        public static PlayerVip GetPlayerVIP(long OwnerId)
        {
            PlayerVip vip = null;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@ownerId", OwnerId);
                        command1.CommandText = "SELECT * FROM player_vip WHERE owner_id=@ownerId";
                        command1.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command1.ExecuteReader(CommandBehavior.Default);
                        if (reader.Read())
                        {
                            PlayerVip vip1 = new PlayerVip();
                            vip1.OwnerId = OwnerId;
                            vip1.Address = reader["registered_ip"].ToString();
                            vip1.Benefit = reader["last_benefit"].ToString();
                            vip1.Expirate = uint.Parse(reader["expirate"].ToString());
                            vip = vip1;
                        }
                        command1.Dispose();
                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return vip;
        }

        public static int GetRequestClanCount(int ClanId)
        {
            int num = 0;
            if (ClanId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@clan", ClanId);
                        command1.CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
                        num = Convert.ToInt32(command1.ExecuteScalar());
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
        }

        public static int GetRequestClanId(long OwnerId)
        {
            int num = 0;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@owner", OwnerId);
                        command1.CommandText = "SELECT clan_id FROM system_clan_invites WHERE player_id=@owner";
                        command1.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command1.ExecuteReader(CommandBehavior.Default);
                        if (reader.Read())
                        {
                            num = int.Parse(reader["clan_id"].ToString());
                        }
                        command1.Dispose();
                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
        }

        public static int GetRequestClanInviteCount(int clanId)
        {
            int num = 0;
            if (clanId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@clan", clanId);
                        command1.CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
                        num = Convert.ToInt32(command1.ExecuteScalar());
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
        }

        public static string GetRequestClanInviteText(int ClanId, long PlayerId)
        {
            string str = null;
            if ((ClanId != 0) && (PlayerId != 0))
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@clan", ClanId);
                        command1.Parameters.AddWithValue("@player", PlayerId);
                        command1.CommandText = "SELECT text FROM system_clan_invites WHERE clan_id=@clan AND player_id=@player";
                        command1.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command1.ExecuteReader(CommandBehavior.Default);
                        if (reader.Read())
                        {
                            str = reader["text"].ToString();
                        }
                        command1.Dispose();
                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return str;
        }

        public static int GetUsedTicket(long OwnerId, string Token)
        {
            int num = 0;
            if ((OwnerId != 0) && !string.IsNullOrEmpty(Token))
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@player", OwnerId);
                        command1.Parameters.AddWithValue("@token", Token);
                        command1.CommandText = "SELECT used_count FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
                        command1.CommandType = CommandType.Text;
                        NpgsqlDataReader reader = command1.ExecuteReader(CommandBehavior.Default);
                        if (reader.Read())
                        {
                            num = int.Parse(reader["used_count"].ToString());
                        }
                        command1.Dispose();
                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return num;
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
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@name", Nickname);
                    command1.CommandText = "SELECT COUNT(*) FROM accounts WHERE nickname=@name";
                    num = Convert.ToInt32(command1.ExecuteScalar());
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return (num > 0);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool IsTicketUsedByPlayer(long OwnerId, string Token)
        {
            bool flag = false;
            if (OwnerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.Parameters.AddWithValue("@player", OwnerId);
                        command1.Parameters.AddWithValue("@token", Token);
                        command1.CommandText = "SELECT * FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
                        command1.CommandType = CommandType.Text;
                        flag = Convert.ToBoolean(command1.ExecuteScalar());
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
            return flag;
        }

        public static bool MessageExists(long ObjectId, long OwnerId)
        {
            bool flag;
            if ((ObjectId == 0) || (OwnerId == 0))
            {
                return false;
            }
            try
            {
                int num = 0;
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@obj", ObjectId);
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.CommandText = "SELECT COUNT(*) FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
                    num = Convert.ToInt32(command1.ExecuteScalar());
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                flag = num > 0;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
            return flag;
        }

        public static void RecycleMessages(long OwnerId, List<MessageModel> Messages)
        {
            List<object> objectIds = new List<object>();
            for (int i = 0; i < Messages.Count; i++)
            {
                MessageModel model = Messages[i];
                if (model.DaysRemaining == 0)
                {
                    objectIds.Add(model.ObjectId);
                    Messages.RemoveAt(i--);
                }
            }
            DeleteMessages(objectIds, OwnerId);
        }

        public static bool SaveAutoBan(long PlayerId, string Username, string Nickname, string Type, string Time, string Address, string HackType)
        {
            if (PlayerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@player_id", PlayerId);
                    command1.Parameters.AddWithValue("@login", Username);
                    command1.Parameters.AddWithValue("@player_name", Nickname);
                    command1.Parameters.AddWithValue("@type", Type);
                    command1.Parameters.AddWithValue("@time", Time);
                    command1.Parameters.AddWithValue("@ip", Address);
                    command1.Parameters.AddWithValue("@hack_type", HackType);
                    command1.CommandText = "INSERT INTO base_auto_ban(owner_id, username, nickname, type, time, ip4_address, hack_type) VALUES(@player_id, @login, @player_name, @type, @time, @ip, @hack_type)";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static BanHistory SaveBanHistory(long PlayerId, string Type, string Value, DateTime EndDate)
        {
            BanHistory history2;
            BanHistory history1 = new BanHistory();
            history1.PlayerId = PlayerId;
            history1.Type = Type;
            history1.Value = Value;
            history1.EndDate = EndDate;
            BanHistory history = history1;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@provider", history.PlayerId);
                    command1.Parameters.AddWithValue("@type", history.Type);
                    command1.Parameters.AddWithValue("@value", history.Value);
                    command1.Parameters.AddWithValue("@reason", history.Reason);
                    command1.Parameters.AddWithValue("@start", history.StartDate);
                    command1.Parameters.AddWithValue("@end", history.EndDate);
                    command1.CommandText = "INSERT INTO base_ban_history(owner_id, type, value, reason, start_date, expire_date) VALUES(@provider, @type, @value, @reason, @start, @end) RETURNING object_id";
                    history.ObjectId = (long) command1.ExecuteScalar();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                    history2 = history;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                history2 = null;
            }
            return history2;
        }

        public static bool SaveBanReason(long ObjectId, string Reason) => 
            (ObjectId != 0) ? ComDiv.UpdateDB("base_ban_history", "reason", Reason, "object_id", ObjectId) : false;

        public static bool UpdateAccountCash(long OwnerId, int Cash)
        {
            if ((OwnerId == 0) || (Cash == -1))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@cash", Cash);
                    command1.CommandText = "UPDATE accounts SET cash=@cash WHERE player_id=@owner";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateAccountGold(long OwnerId, int Gold)
        {
            if ((OwnerId == 0) || (Gold == -1))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@gold", Gold);
                    command1.CommandText = "UPDATE accounts SET gold=@gold WHERE player_id=@owner";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateAccountTags(long OwnerId, int Tags)
        {
            if ((OwnerId == 0) || (Tags == -1))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@tag", Tags);
                    command1.CommandText = "UPDATE accounts SET tags=@tag WHERE player_id=@owner";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateAccountValuable(long PlayerId, int Gold, int Cash, int Tags)
        {
            if ((PlayerId == 0) || (((Gold == -1) && (Cash == -1)) && (Tags == -1)))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@owner", PlayerId);
                    string str = "";
                    if (Gold > -1)
                    {
                        command.Parameters.AddWithValue("@gold", Gold);
                        str = str + "gold=@gold";
                    }
                    if (Cash > -1)
                    {
                        command.Parameters.AddWithValue("@cash", Cash);
                        str = str + ((str != "") ? ", " : "") + "cash=@cash";
                    }
                    if (Tags > -1)
                    {
                        command.Parameters.AddWithValue("@tags", Tags);
                        str = str + ((str != "") ? ", " : "") + "tags=@tags";
                    }
                    command.CommandText = "UPDATE accounts SET " + str + " WHERE player_id=@owner";
                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateClanBattles(int ClanId, int Matches, int Wins, int Loses)
        {
            if (ClanId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@clan", ClanId);
                    command1.Parameters.AddWithValue("@partidas", Matches);
                    command1.Parameters.AddWithValue("@vitorias", Wins);
                    command1.Parameters.AddWithValue("@derrotas", Loses);
                    command1.CommandText = "UPDATE system_clan SET matches=@partidas, match_wins=@vitorias, match_loses=@derrotas WHERE id=@clan";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static void UpdateClanBestPlayers(ClanModel Clan)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@id", Clan.Id);
                    command1.Parameters.AddWithValue("@bp1", Clan.BestPlayers.Exp.GetSplit());
                    command1.Parameters.AddWithValue("@bp2", Clan.BestPlayers.Participation.GetSplit());
                    command1.Parameters.AddWithValue("@bp3", Clan.BestPlayers.Wins.GetSplit());
                    command1.Parameters.AddWithValue("@bp4", Clan.BestPlayers.Kills.GetSplit());
                    command1.Parameters.AddWithValue("@bp5", Clan.BestPlayers.Headshots.GetSplit());
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = "UPDATE system_clan SET best_exp=@bp1, best_participants=@bp2, best_wins=@bp3, best_kills=@bp4, best_headshots=@bp5 WHERE id=@id";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static bool UpdateClanExp(int ClanId, int Exp) => 
            (ClanId != 0) ? ComDiv.UpdateDB("system_clan", "exp", Exp, "id", ClanId) : false;

        public static bool UpdateClanInfo(int ClanId, int Authority, int RankLimit, int MinAge, int MaxAge, int JoinType)
        {
            if (ClanId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@ClanId", ClanId);
                    command1.Parameters.AddWithValue("@Authority", Authority);
                    command1.Parameters.AddWithValue("@RankLimit", RankLimit);
                    command1.Parameters.AddWithValue("@MinAge", MinAge);
                    command1.Parameters.AddWithValue("@MaxAge", MaxAge);
                    command1.Parameters.AddWithValue("@JoinType", JoinType);
                    command1.CommandText = "UPDATE system_clan SET authority=@Authority, rank_limit=@RankLimit, min_age_limit=@MinAge, max_age_limit=@MaxAge, join_permission=@JoinType WHERE id=@ClanId";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateClanLogo(int ClanId, uint logo) => 
            (ClanId != 0) ? ComDiv.UpdateDB("system_clan", "logo", (long) logo, "id", ClanId) : false;

        public static bool UpdateClanPoints(int ClanId, float Gold) => 
            (ClanId != 0) ? ComDiv.UpdateDB("system_clan", "gold", Gold, "id", ClanId) : false;

        public static bool UpdateClanRank(int ClanId, int Rank) => 
            (ClanId != 0) ? ComDiv.UpdateDB("system_clan", "rank", Rank, "id", ClanId) : false;

        public static void UpdateCouponEffect(long PlayerId, CouponEffects Effects)
        {
            if (PlayerId != 0)
            {
                ComDiv.UpdateDB("accounts", "coupon_effect", (long) Effects, "player_id", PlayerId);
            }
        }

        public static void UpdateCurrentPlayerMissionList(long player_id, PlayerMissions mission)
        {
            byte[] currentMissionList = mission.GetCurrentMissionList();
            ComDiv.UpdateDB("player_missions", $"mission{mission.ActualMission + 1}_raw", currentMissionList, "owner_id", player_id);
        }

        public static bool UpdateEquipedPlayerTitle(long player_id, int index, int titleId) => 
            ComDiv.UpdateDB("player_titles", $"equip_slot{index + 1}", titleId, "owner_id", player_id);

        public static void UpdateExpireDate(long ObjectId, long OwnerId, uint Date)
        {
            ComDiv.UpdateDB("player_messages", "expire_date", (long) Date, "object_id", ObjectId, "owner_id", OwnerId);
        }

        public static void UpdatePlayerBonus(long PlayerId, int Bonuses, int FreePass)
        {
            if (PlayerId != 0)
            {
                try
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.CommandType = CommandType.Text;
                        command1.Parameters.AddWithValue("@id", PlayerId);
                        command1.Parameters.AddWithValue("@bonuses", Bonuses);
                        command1.Parameters.AddWithValue("@freepass", FreePass);
                        command1.CommandText = "UPDATE player_bonus SET bonuses=@bonuses, free_pass=@freepass WHERE owner_id=@id";
                        command1.ExecuteNonQuery();
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                }
            }
        }

        public static bool UpdatePlayerCharacter(int Slot, long ObjectId, long OwnerId) => 
            ComDiv.UpdateDB("player_characters", "slot", Slot, "object_id", ObjectId, "owner_id", OwnerId);

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
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@deaths", Deaths);
                    command1.Parameters.AddWithValue("@kills", Kills);
                    command1.Parameters.AddWithValue("@hs", Headshots);
                    command1.Parameters.AddWithValue("@total", Totals);
                    command1.CommandText = "UPDATE player_stat_seasons SET kills_count=@kills, deaths_count=@deaths, headshots_count=@hs, total_kills=@total WHERE owner_id=@owner";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdatePlayerMatches(int Matches, int MatchWins, int MatchLoses, int MatchDraws, int Totals, long OwnerId)
        {
            if (OwnerId == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.Parameters.AddWithValue("@owner", OwnerId);
                    command1.Parameters.AddWithValue("@partidas", Matches);
                    command1.Parameters.AddWithValue("@ganhas", MatchWins);
                    command1.Parameters.AddWithValue("@perdidas", MatchLoses);
                    command1.Parameters.AddWithValue("@empates", MatchDraws);
                    command1.Parameters.AddWithValue("@todaspartidas", Totals);
                    command1.CommandText = "UPDATE player_stat_seasons SET matches=@partidas, match_wins=@ganhas, match_loses=@perdidas, match_draws=@empates, total_matches=@todaspartidas WHERE owner_id=@owner";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdatePlayerMissionId(long player_id, int value, int index) => 
            ComDiv.UpdateDB("accounts", $"mission_id{index + 1}", value, "player_id", player_id);

        public static void UpdatePlayerTitleRequi(long player_id, int medalhas, int insignias, int ordens_azuis, int broche)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.Parameters.AddWithValue("@pid", player_id);
                    command1.Parameters.AddWithValue("@broche", broche);
                    command1.Parameters.AddWithValue("@insignias", insignias);
                    command1.Parameters.AddWithValue("@medalhas", medalhas);
                    command1.Parameters.AddWithValue("@ordensazuis", ordens_azuis);
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = "UPDATE accounts SET ribbon=@broche, ensign=@insignias, medal=@medalhas, master_medal=@ordensazuis WHERE player_id=@pid";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
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

