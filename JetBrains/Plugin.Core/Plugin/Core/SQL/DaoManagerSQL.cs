// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SQL.DaoManagerSQL
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.SQL;

public static class DaoManagerSQL
{
  static DaoManagerSQL()
  {
    Translation.Strings = new SortedList<string, string>();
    DaoManagerSQL.smethod_0();
  }

  private static void smethod_0()
  {
    string path = "Config/Translate/Strings.ini";
    if (File.Exists(path))
    {
      DaoManagerSQL.smethod_1(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
  }

  private static void smethod_1([In] string obj0)
  {
    try
    {
      using (StreamReader streamReader = new StreamReader(obj0))
      {
        string str1;
        while ((str1 = streamReader.ReadLine()) != null)
        {
          int length = str1.IndexOf(" = ");
          if (length >= 0)
          {
            string key = str1.Substring(0, length);
            string str2 = str1.Substring(length + 3);
            Translation.Strings.Add(key, str2);
          }
        }
        streamReader.Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Translation: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static string GetLabel(string string_7)
  {
    try
    {
      string str;
      return Translation.Strings.TryGetValue(string_7, out str) ? str.Replace("\\n", '\n'.ToString()) : string_7;
    }
    catch
    {
      return string_7;
    }
  }

  public static string GetLabel([In] string obj0, object[] exception_0)
  {
    return string.Format(DaoManagerSQL.GetLabel(obj0), exception_0);
  }

  public static List<ItemsModel> GetPlayerInventoryItems(long string_0)
  {
    List<ItemsModel> playerInventoryItems = new List<ItemsModel>();
    if (string_0 == 0L)
      return playerInventoryItems;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) string_0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_items WHERE owner_id=@owner ORDER BY object_id ASC;";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerBonus playerBonus = new PlayerBonus(int.Parse(((DbDataReader) npgsqlDataReader)["id"].ToString()));
          ((ItemsModel) playerBonus).ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
          ((ItemsModel) playerBonus).Name = ((DbDataReader) npgsqlDataReader)["name"].ToString();
          ((ItemsModel) playerBonus).Count = uint.Parse(((DbDataReader) npgsqlDataReader)["count"].ToString());
          ((ItemsModel) playerBonus).Equip = (ItemEquipType) int.Parse(((DbDataReader) npgsqlDataReader)["equip"].ToString());
          ItemsModel itemsModel = (ItemsModel) playerBonus;
          playerInventoryItems.Add(itemsModel);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerInventoryItems;
  }

  public static bool CreatePlayerInventoryItem(ItemsModel string_0, [In] long obj1)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@owner", (object) obj1);
        command.Parameters.AddWithValue("@itmId", (object) string_0.Id);
        command.Parameters.AddWithValue("@ItmNm", (object) string_0.Name);
        command.Parameters.AddWithValue("@count", (object) (long) string_0.Count);
        command.Parameters.AddWithValue("@equip", (object) (int) string_0.Equip);
        ((DbCommand) command).CommandText = "INSERT INTO player_items(owner_id, id, name, count, equip) VALUES(@owner, @itmId, @ItmNm, @count, @equip) RETURNING object_id";
        object obj = ((DbCommand) command).ExecuteScalar();
        string_0.ObjectId = string_0.Equip != ItemEquipType.Permanent ? (long) obj : string_0.ObjectId;
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool DeletePlayerInventoryItem(long Title, params long Argumens)
  {
    return Title != 0L && Argumens != 0L && ComDiv.DeleteDB("player_items", "object_id", (object) Title, "owner_id", (object) Argumens);
  }

  public static BanHistory GetAccountBan(long OwnerId)
  {
    BanHistory accountBan = (BanHistory) new CouponFlag();
    if (OwnerId == 0L)
      return accountBan;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@obj", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM base_ban_history WHERE object_id=@obj";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          accountBan.ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
          accountBan.PlayerId = long.Parse(((DbDataReader) npgsqlDataReader)["owner_id"].ToString());
          accountBan.Type = ((DbDataReader) npgsqlDataReader)["type"].ToString();
          accountBan.Value = ((DbDataReader) npgsqlDataReader)["value"].ToString();
          accountBan.Reason = ((DbDataReader) npgsqlDataReader)["reason"].ToString();
          ((CouponFlag) accountBan).set_StartDate(DateTime.Parse(((DbDataReader) npgsqlDataReader)["start_date"].ToString()));
          ((CouponFlag) accountBan).set_EndDate(DateTime.Parse(((DbDataReader) npgsqlDataReader)["expire_date"].ToString()));
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return (BanHistory) null;
    }
    return accountBan;
  }

  public static List<string> GetHwIdList()
  {
    List<string> hwIdList = new List<string>();
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandText = "SELECT * FROM base_ban_hwid";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          string str = ((DbDataReader) npgsqlDataReader)["hardware_id"].ToString();
          if (str != null || str.Length != 0)
            hwIdList.Add(str);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return (List<string>) null;
    }
    return hwIdList;
  }

  public static void GetBanStatus(string Item, string OwnerId, [In] ref bool obj2, [In] ref bool obj3)
  {
    obj2 = false;
    obj3 = false;
    try
    {
      DateTime dateTime = DBQuery.Now();
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@mac", (object) Item);
        command.Parameters.AddWithValue("@ip", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM base_ban_history WHERE value in (@mac, @ip)";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          string str1 = ((DbDataReader) npgsqlDataReader)["type"].ToString();
          string str2 = ((DbDataReader) npgsqlDataReader)["value"].ToString();
          if (!(DateTime.Parse(((DbDataReader) npgsqlDataReader)["expire_date"].ToString()) < dateTime))
          {
            if (str1 == "MAC" && str2 == Item)
              obj2 = true;
            else if (str1 == "IP4" && str2 == OwnerId)
              obj3 = true;
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static BanHistory SaveBanHistory(
    long ObjectId,
    string IP4,
    [Out] string ValidMac,
    [In] DateTime obj3)
  {
    CouponFlag couponFlag = new CouponFlag();
    ((BanHistory) couponFlag).PlayerId = ObjectId;
    ((BanHistory) couponFlag).Type = IP4;
    ((BanHistory) couponFlag).Value = ValidMac;
    couponFlag.set_EndDate(obj3);
    BanHistory banHistory = (BanHistory) couponFlag;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@provider", (object) banHistory.PlayerId);
        command.Parameters.AddWithValue("@type", (object) banHistory.Type);
        command.Parameters.AddWithValue("@value", (object) banHistory.Value);
        command.Parameters.AddWithValue("@reason", (object) banHistory.Reason);
        command.Parameters.AddWithValue("@start", (object) ((CouponFlag) banHistory).get_StartDate());
        command.Parameters.AddWithValue("@end", (object) ((CouponFlag) banHistory).get_EndDate());
        ((DbCommand) command).CommandText = "INSERT INTO base_ban_history(owner_id, type, value, reason, start_date, expire_date) VALUES(@provider, @type, @value, @reason, @start, @end) RETURNING object_id";
        object obj = ((DbCommand) command).ExecuteScalar();
        banHistory.ObjectId = (long) obj;
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
        return banHistory;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return (BanHistory) null;
    }
  }

  public static bool SaveAutoBan(
    [In] long obj0,
    [In] string obj1,
    [In] string obj2,
    [Out] string ValidIp4,
    [In] string obj4,
    [In] string obj5,
    [In] string obj6)
  {
    if (obj0 == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@player_id", (object) obj0);
        command.Parameters.AddWithValue("@login", (object) obj1);
        command.Parameters.AddWithValue("@player_name", (object) obj2);
        command.Parameters.AddWithValue("@type", (object) ValidIp4);
        command.Parameters.AddWithValue("@time", (object) obj4);
        command.Parameters.AddWithValue("@ip", (object) obj5);
        command.Parameters.AddWithValue("@hack_type", (object) obj6);
        ((DbCommand) command).CommandText = "INSERT INTO base_auto_ban(owner_id, username, nickname, type, time, ip4_address, hack_type) VALUES(@player_id, @login, @player_name, @type, @time, @ip, @hack_type)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool SaveBanReason([In] long obj0, [In] string obj1)
  {
    return obj0 != 0L && ComDiv.UpdateDB("base_ban_history", "reason", (object) obj1, "object_id", (object) obj0);
  }

  public static bool CreateClan([In] ref int obj0, [In] string obj1, [In] long obj2, [In] string obj3, uint Time)
  {
    try
    {
      obj0 = -1;
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@owner", (object) obj2);
        command.Parameters.AddWithValue("@name", (object) obj1);
        command.Parameters.AddWithValue("@date", (object) (long) Time);
        command.Parameters.AddWithValue("@info", (object) obj3);
        command.Parameters.AddWithValue("@best", (object) "0-0");
        ((DbCommand) command).CommandText = "INSERT INTO system_clan (name, owner_id, create_date, info, best_exp, best_participants, best_wins, best_kills, best_headshots) VALUES (@name, @owner, @date, @info, @best, @best, @best, @best, @best) RETURNING id";
        object obj = ((DbCommand) command).ExecuteScalar();
        obj0 = (int) obj;
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      obj0 = -1;
      return false;
    }
  }

  public static bool UpdateClanInfo(
    [Out] int ClanId,
    int Name,
    int OwnerId,
    int ClanInfo,
    int CreateDate,
    [In] int obj5)
  {
    if (ClanId == 0)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@ClanId", (object) ClanId);
        command.Parameters.AddWithValue("@Authority", (object) Name);
        command.Parameters.AddWithValue("@RankLimit", (object) OwnerId);
        command.Parameters.AddWithValue("@MinAge", (object) ClanInfo);
        command.Parameters.AddWithValue("@MaxAge", (object) CreateDate);
        command.Parameters.AddWithValue("@JoinType", (object) obj5);
        ((DbCommand) command).CommandText = "UPDATE system_clan SET authority=@Authority, rank_limit=@RankLimit, min_age_limit=@MinAge, max_age_limit=@MaxAge, join_permission=@JoinType WHERE id=@ClanId";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static void UpdateClanBestPlayers([In] ClanModel obj0)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) obj0.Id);
        command.Parameters.AddWithValue("@bp1", (object) ((RHistoryModel) obj0.BestPlayers.Exp).GetSplit());
        command.Parameters.AddWithValue("@bp2", (object) ((RHistoryModel) obj0.BestPlayers.Participation).GetSplit());
        command.Parameters.AddWithValue("@bp3", (object) ((RHistoryModel) obj0.BestPlayers.Wins).GetSplit());
        command.Parameters.AddWithValue("@bp4", (object) ((RHistoryModel) obj0.BestPlayers.Kills).GetSplit());
        command.Parameters.AddWithValue("@bp5", (object) ((RHistoryModel) obj0.BestPlayers.Headshots).GetSplit());
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = "UPDATE system_clan SET best_exp=@bp1, best_participants=@bp2, best_wins=@bp3, best_kills=@bp4, best_headshots=@bp5 WHERE id=@id";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static bool UpdateClanLogo([In] int obj0, [In] uint obj1)
  {
    return obj0 != 0 && ComDiv.UpdateDB("system_clan", "logo", (object) (long) obj1, "id", (object) obj0);
  }

  public static bool UpdateClanPoints([In] int obj0, [In] float obj1)
  {
    return obj0 != 0 && ComDiv.UpdateDB("system_clan", "gold", (object) obj1, "id", (object) obj0);
  }

  public static bool UpdateClanExp(int Clan, [In] int obj1)
  {
    return Clan != 0 && ComDiv.UpdateDB("system_clan", "exp", (object) obj1, "id", (object) Clan);
  }

  public static bool UpdateClanRank([In] int obj0, int logo)
  {
    return obj0 != 0 && ComDiv.UpdateDB("system_clan", "rank", (object) logo, "id", (object) obj0);
  }

  public static bool UpdateClanBattles([In] int obj0, int Gold, [In] int obj2, [In] int obj3)
  {
    if (obj0 == 0)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@clan", (object) obj0);
        command.Parameters.AddWithValue("@partidas", (object) Gold);
        command.Parameters.AddWithValue("@vitorias", (object) obj2);
        command.Parameters.AddWithValue("@derrotas", (object) obj3);
        ((DbCommand) command).CommandText = "UPDATE system_clan SET matches=@partidas, match_wins=@vitorias, match_loses=@derrotas WHERE id=@clan";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static int GetClanPlayers([In] int obj0)
  {
    int clanPlayers = 0;
    if (obj0 == 0)
      return clanPlayers;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM accounts WHERE clan_id=@clan";
        clanPlayers = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return clanPlayers;
  }

  public static MessageModel GetMessage(long ClanId, long Matches)
  {
    MessageModel message = (MessageModel) null;
    if (ClanId != 0L)
    {
      if (Matches != 0L)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            command.Parameters.AddWithValue("@obj", (object) ClanId);
            command.Parameters.AddWithValue("@owner", (object) Matches);
            ((DbCommand) command).CommandText = "SELECT * FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
            ((DbCommand) command).CommandType = CommandType.Text;
            NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
            while (((DbDataReader) npgsqlDataReader).Read())
            {
              CharacterModel characterModel = new CharacterModel((long) uint.Parse(((DbDataReader) npgsqlDataReader)["expire_date"].ToString()), DBQuery.Now());
              ((MessageModel) characterModel).ObjectId = ClanId;
              ((MessageModel) characterModel).SenderId = long.Parse(((DbDataReader) npgsqlDataReader)["sender_id"].ToString());
              ((MessageModel) characterModel).SenderName = ((DbDataReader) npgsqlDataReader)["sender_name"].ToString();
              ((MessageModel) characterModel).ClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
              ((MessageModel) characterModel).ClanNote = (NoteMessageClan) int.Parse(((DbDataReader) npgsqlDataReader)["clan_note"].ToString());
              ((MessageModel) characterModel).Text = ((DbDataReader) npgsqlDataReader)["text"].ToString();
              ((MessageModel) characterModel).Type = (NoteMessageType) int.Parse(((DbDataReader) npgsqlDataReader)["type"].ToString());
              ((MessageModel) characterModel).State = (NoteMessageState) int.Parse(((DbDataReader) npgsqlDataReader)["state"].ToString());
              message = (MessageModel) characterModel;
            }
            ((Component) command).Dispose();
            ((DbDataReader) npgsqlDataReader).Close();
            ((Component) npgsqlConnection).Dispose();
            ((DbConnection) npgsqlConnection).Close();
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
          return (MessageModel) null;
        }
        return message;
      }
    }
    return message;
  }

  public static List<MessageModel> GetGiftMessages([In] long obj0)
  {
    List<MessageModel> giftMessages = new List<MessageModel>();
    if (obj0 == 0L)
      return giftMessages;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          NoteMessageType noteMessageType = (NoteMessageType) int.Parse(((DbDataReader) npgsqlDataReader)["type"].ToString());
          if (noteMessageType == NoteMessageType.Gift)
          {
            CharacterModel characterModel = new CharacterModel((long) uint.Parse(((DbDataReader) npgsqlDataReader)["expire_date"].ToString()), DBQuery.Now());
            ((MessageModel) characterModel).ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
            ((MessageModel) characterModel).SenderId = long.Parse(((DbDataReader) npgsqlDataReader)["sender_id"].ToString());
            ((MessageModel) characterModel).SenderName = ((DbDataReader) npgsqlDataReader)["sender_name"].ToString();
            ((MessageModel) characterModel).ClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
            ((MessageModel) characterModel).ClanNote = (NoteMessageClan) int.Parse(((DbDataReader) npgsqlDataReader)["clan_note"].ToString());
            ((MessageModel) characterModel).Text = ((DbDataReader) npgsqlDataReader)["text"].ToString();
            ((MessageModel) characterModel).Type = noteMessageType;
            ((MessageModel) characterModel).State = (NoteMessageState) int.Parse(((DbDataReader) npgsqlDataReader)["state"].ToString());
            MessageModel messageModel = (MessageModel) characterModel;
            giftMessages.Add(messageModel);
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return giftMessages;
  }

  public static List<MessageModel> GetMessages([In] long obj0)
  {
    List<MessageModel> messages = new List<MessageModel>();
    if (obj0 == 0L)
      return messages;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_messages WHERE owner_id=@owner";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          NoteMessageType noteMessageType = (NoteMessageType) int.Parse(((DbDataReader) npgsqlDataReader)["type"].ToString());
          if (noteMessageType != NoteMessageType.Gift)
          {
            CharacterModel characterModel = new CharacterModel((long) uint.Parse(((DbDataReader) npgsqlDataReader)["expire_date"].ToString()), DBQuery.Now());
            ((MessageModel) characterModel).ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
            ((MessageModel) characterModel).SenderId = long.Parse(((DbDataReader) npgsqlDataReader)["sender_id"].ToString());
            ((MessageModel) characterModel).SenderName = ((DbDataReader) npgsqlDataReader)["sender_name"].ToString();
            ((MessageModel) characterModel).ClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
            ((MessageModel) characterModel).ClanNote = (NoteMessageClan) int.Parse(((DbDataReader) npgsqlDataReader)["clan_note"].ToString());
            ((MessageModel) characterModel).Text = ((DbDataReader) npgsqlDataReader)["text"].ToString();
            ((MessageModel) characterModel).Type = noteMessageType;
            ((MessageModel) characterModel).State = (NoteMessageState) int.Parse(((DbDataReader) npgsqlDataReader)["state"].ToString());
            MessageModel messageModel = (MessageModel) characterModel;
            messages.Add(messageModel);
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return messages;
  }

  public static bool MessageExists(long ClanId, [In] long obj1)
  {
    if (ClanId != 0L)
    {
      if (obj1 != 0L)
      {
        try
        {
          int num = 0;
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            command.Parameters.AddWithValue("@obj", (object) ClanId);
            command.Parameters.AddWithValue("@owner", (object) obj1);
            ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM player_messages WHERE object_id=@obj AND owner_id=@owner";
            num = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
            ((Component) command).Dispose();
            ((Component) npgsqlConnection).Dispose();
            ((DbConnection) npgsqlConnection).Close();
          }
          return num > 0;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
        return false;
      }
    }
    return false;
  }

  public static int GetMessagesCount([In] long obj0)
  {
    int messagesCount = 0;
    if (obj0 == 0L)
      return messagesCount;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
        messagesCount = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return messagesCount;
  }

  public static bool CreateMessage(long OwnerId, [In] MessageModel obj1)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        command.Parameters.AddWithValue("@sendid", (object) obj1.SenderId);
        command.Parameters.AddWithValue("@clan", (object) obj1.ClanId);
        command.Parameters.AddWithValue("@sendname", (object) obj1.SenderName);
        command.Parameters.AddWithValue("@text", (object) obj1.Text);
        command.Parameters.AddWithValue("@type", (object) (int) obj1.Type);
        command.Parameters.AddWithValue("@state", (object) (int) obj1.State);
        command.Parameters.AddWithValue("@expire", (object) obj1.ExpireDate);
        command.Parameters.AddWithValue("@cb", (object) (int) obj1.ClanNote);
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire_date) VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
        object obj = ((DbCommand) command).ExecuteScalar();
        obj1.ObjectId = (long) obj;
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
        return true;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static void UpdateState(long ObjectId, long OwnerId, [In] int obj2)
  {
    ComDiv.UpdateDB("player_messages", "state", (object) obj2, "object_id", (object) ObjectId, "owner_id", (object) OwnerId);
  }

  public static void UpdateExpireDate(long OwnerId, long Message, [In] uint obj2)
  {
    ComDiv.UpdateDB("player_messages", "expire_date", (object) (long) obj2, "object_id", (object) OwnerId, "owner_id", (object) Message);
  }

  public static bool DeleteMessage([In] long obj0, long OwnerId)
  {
    return obj0 != 0L && OwnerId != 0L && ComDiv.DeleteDB("player_messages", "object_id", (object) obj0, "owner_id", (object) OwnerId);
  }

  public static bool DeleteMessages(List<object> ObjectId, long OwnerId)
  {
    return ObjectId.Count != 0 && OwnerId != 0L && ComDiv.DeleteDB("player_messages", "object_id", ObjectId.ToArray(), "owner_id", (object) OwnerId);
  }

  public static void RecycleMessages([In] long obj0, [In] List<MessageModel> obj1)
  {
    List<object> ObjectId = new List<object>();
    for (int index = 0; index < obj1.Count; ++index)
    {
      MessageModel messageModel = obj1[index];
      if (messageModel.DaysRemaining == 0)
      {
        ObjectId.Add((object) messageModel.ObjectId);
        obj1.RemoveAt(index--);
      }
    }
    DaoManagerSQL.DeleteMessages(ObjectId, obj0);
  }

  public static PlayerEquipment GetPlayerEquipmentsDB([In] long obj0)
  {
    PlayerEquipment playerEquipmentsDb = (PlayerEquipment) null;
    if (obj0 == 0L)
      return playerEquipmentsDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_equipments WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerMissions playerMissions = new PlayerMissions();
          ((PlayerEquipment) playerMissions).OwnerId = obj0;
          ((PlayerEquipment) playerMissions).WeaponPrimary = int.Parse(((DbDataReader) npgsqlDataReader)["weapon_primary"].ToString());
          ((PlayerEquipment) playerMissions).WeaponSecondary = int.Parse(((DbDataReader) npgsqlDataReader)["weapon_secondary"].ToString());
          ((PlayerEquipment) playerMissions).WeaponMelee = int.Parse(((DbDataReader) npgsqlDataReader)["weapon_melee"].ToString());
          ((PlayerEquipment) playerMissions).WeaponExplosive = int.Parse(((DbDataReader) npgsqlDataReader)["weapon_explosive"].ToString());
          ((PlayerEquipment) playerMissions).WeaponSpecial = int.Parse(((DbDataReader) npgsqlDataReader)["weapon_special"].ToString());
          ((PlayerEquipment) playerMissions).CharaRedId = int.Parse(((DbDataReader) npgsqlDataReader)["chara_red_side"].ToString());
          ((PlayerEquipment) playerMissions).CharaBlueId = int.Parse(((DbDataReader) npgsqlDataReader)["chara_blue_side"].ToString());
          ((PlayerEquipment) playerMissions).DinoItem = int.Parse(((DbDataReader) npgsqlDataReader)["dino_item_chara"].ToString());
          ((PlayerEquipment) playerMissions).PartHead = int.Parse(((DbDataReader) npgsqlDataReader)["part_head"].ToString());
          ((PlayerEquipment) playerMissions).PartFace = int.Parse(((DbDataReader) npgsqlDataReader)["part_face"].ToString());
          ((PlayerEquipment) playerMissions).PartJacket = int.Parse(((DbDataReader) npgsqlDataReader)["part_jacket"].ToString());
          ((PlayerEquipment) playerMissions).PartPocket = int.Parse(((DbDataReader) npgsqlDataReader)["part_pocket"].ToString());
          ((PlayerEquipment) playerMissions).PartGlove = int.Parse(((DbDataReader) npgsqlDataReader)["part_glove"].ToString());
          ((PlayerEquipment) playerMissions).PartBelt = int.Parse(((DbDataReader) npgsqlDataReader)["part_belt"].ToString());
          ((PlayerEquipment) playerMissions).PartHolster = int.Parse(((DbDataReader) npgsqlDataReader)["part_holster"].ToString());
          ((PlayerEquipment) playerMissions).PartSkin = int.Parse(((DbDataReader) npgsqlDataReader)["part_skin"].ToString());
          ((PlayerEquipment) playerMissions).BeretItem = int.Parse(((DbDataReader) npgsqlDataReader)["beret_item_part"].ToString());
          ((PlayerEquipment) playerMissions).AccessoryId = int.Parse(((DbDataReader) npgsqlDataReader)["accesory_id"].ToString());
          playerMissions.set_SprayId(int.Parse(((DbDataReader) npgsqlDataReader)["spray_id"].ToString()));
          playerMissions.set_NameCardId(int.Parse(((DbDataReader) npgsqlDataReader)["namecard_id"].ToString()));
          playerEquipmentsDb = (PlayerEquipment) playerMissions;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerEquipmentsDb;
  }

  public static bool CreatePlayerEquipmentsDB(long ObjectIds)
  {
    if (ObjectIds == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) ObjectIds);
        ((DbCommand) command).CommandText = "INSERT INTO player_equipments(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static List<CharacterModel> GetPlayerCharactersDB([In] long obj0)
  {
    List<CharacterModel> playerCharactersDb = new List<CharacterModel>();
    if (obj0 == 0L)
      return playerCharactersDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@OwnerId", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_characters WHERE owner_id=@OwnerId ORDER BY slot ASC;";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerFriends playerFriends = new PlayerFriends();
          ((CharacterModel) playerFriends).ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
          ((CharacterModel) playerFriends).Id = int.Parse(((DbDataReader) npgsqlDataReader)["id"].ToString());
          ((CharacterModel) playerFriends).Slot = int.Parse(((DbDataReader) npgsqlDataReader)["slot"].ToString());
          ((CharacterModel) playerFriends).Name = ((DbDataReader) npgsqlDataReader)["name"].ToString();
          playerFriends.set_CreateDate(uint.Parse(((DbDataReader) npgsqlDataReader)["create_date"].ToString()));
          playerFriends.set_PlayTime(uint.Parse(((DbDataReader) npgsqlDataReader)["playtime"].ToString()));
          CharacterModel characterModel = (CharacterModel) playerFriends;
          playerCharactersDb.Add(characterModel);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerCharactersDb;
  }

  public static bool CreatePlayerCharacter(CharacterModel OwnerId, long Messages)
  {
    if (Messages == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner_id", (object) Messages);
        command.Parameters.AddWithValue("@id", (object) OwnerId.Id);
        command.Parameters.AddWithValue("@slot", (object) OwnerId.Slot);
        command.Parameters.AddWithValue("@name", (object) OwnerId.Name);
        command.Parameters.AddWithValue("@createdate", (object) (long) ((PlayerFriends) OwnerId).get_CreateDate());
        command.Parameters.AddWithValue("@playtime", (object) (long) ((PlayerFriends) OwnerId).get_PlayTime());
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = "INSERT INTO player_characters(owner_id, id, slot, name, create_date, playtime) VALUES(@owner_id, @id, @slot, @name, @createdate, @playtime) RETURNING object_id";
        object obj = ((DbCommand) command).ExecuteScalar();
        OwnerId.ObjectId = (long) obj;
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
        return true;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticTotal GetPlayerStatBasicDB(long OwnerId)
  {
    StatisticTotal playerStatBasicDb = (StatisticTotal) null;
    if (OwnerId == 0L)
      return playerStatBasicDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_basics WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          StatisticWeapon statisticWeapon = new StatisticWeapon();
          ((StatisticTotal) statisticWeapon).OwnerId = OwnerId;
          ((StatisticTotal) statisticWeapon).Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString());
          ((StatisticTotal) statisticWeapon).MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString());
          ((StatisticTotal) statisticWeapon).MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString());
          ((StatisticTotal) statisticWeapon).MatchDraws = int.Parse(((DbDataReader) npgsqlDataReader)["match_draws"].ToString());
          ((StatisticTotal) statisticWeapon).KillsCount = int.Parse(((DbDataReader) npgsqlDataReader)["kills_count"].ToString());
          ((StatisticTotal) statisticWeapon).DeathsCount = int.Parse(((DbDataReader) npgsqlDataReader)["deaths_count"].ToString());
          ((StatisticTotal) statisticWeapon).HeadshotsCount = int.Parse(((DbDataReader) npgsqlDataReader)["headshots_count"].ToString());
          ((StatisticTotal) statisticWeapon).AssistsCount = int.Parse(((DbDataReader) npgsqlDataReader)["assists_count"].ToString());
          ((StatisticTotal) statisticWeapon).EscapesCount = int.Parse(((DbDataReader) npgsqlDataReader)["escapes_count"].ToString());
          ((StatisticTotal) statisticWeapon).MvpCount = int.Parse(((DbDataReader) npgsqlDataReader)["mvp_count"].ToString());
          statisticWeapon.set_TotalMatchesCount(int.Parse(((DbDataReader) npgsqlDataReader)["total_matches"].ToString()));
          statisticWeapon.set_TotalKillsCount(int.Parse(((DbDataReader) npgsqlDataReader)["total_kills"].ToString()));
          playerStatBasicDb = (StatisticTotal) statisticWeapon;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatBasicDb;
  }

  public static bool CreatePlayerStatBasicDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_basics(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticSeason GetPlayerStatSeasonDB(long OwnerId)
  {
    StatisticSeason playerStatSeasonDb = (StatisticSeason) null;
    if (OwnerId == 0L)
      return playerStatSeasonDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_seasons WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          StatisticTotal statisticTotal = new StatisticTotal();
          ((StatisticSeason) statisticTotal).OwnerId = OwnerId;
          ((StatisticSeason) statisticTotal).Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString());
          ((StatisticSeason) statisticTotal).MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString());
          ((StatisticSeason) statisticTotal).MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString());
          ((StatisticSeason) statisticTotal).MatchDraws = int.Parse(((DbDataReader) npgsqlDataReader)["match_draws"].ToString());
          ((StatisticSeason) statisticTotal).KillsCount = int.Parse(((DbDataReader) npgsqlDataReader)["kills_count"].ToString());
          ((StatisticSeason) statisticTotal).DeathsCount = int.Parse(((DbDataReader) npgsqlDataReader)["deaths_count"].ToString());
          ((StatisticSeason) statisticTotal).HeadshotsCount = int.Parse(((DbDataReader) npgsqlDataReader)["headshots_count"].ToString());
          ((StatisticSeason) statisticTotal).AssistsCount = int.Parse(((DbDataReader) npgsqlDataReader)["assists_count"].ToString());
          ((StatisticSeason) statisticTotal).EscapesCount = int.Parse(((DbDataReader) npgsqlDataReader)["escapes_count"].ToString());
          ((StatisticSeason) statisticTotal).MvpCount = int.Parse(((DbDataReader) npgsqlDataReader)["mvp_count"].ToString());
          statisticTotal.set_TotalMatchesCount(int.Parse(((DbDataReader) npgsqlDataReader)["total_matches"].ToString()));
          statisticTotal.set_TotalKillsCount(int.Parse(((DbDataReader) npgsqlDataReader)["total_kills"].ToString()));
          playerStatSeasonDb = (StatisticSeason) statisticTotal;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatSeasonDb;
  }

  public static bool CreatePlayerStatSeasonDB(long Chara)
  {
    if (Chara == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) Chara);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_seasons(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticClan GetPlayerStatClanDB([In] long obj0)
  {
    StatisticClan playerStatClanDb = (StatisticClan) null;
    if (obj0 == 0L)
      return playerStatClanDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_clans WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          StatisticDaily statisticDaily = new StatisticDaily();
          ((StatisticClan) statisticDaily).OwnerId = obj0;
          ((StatisticClan) statisticDaily).Matches = int.Parse(((DbDataReader) npgsqlDataReader)["clan_matches"].ToString());
          statisticDaily.set_MatchWins(int.Parse(((DbDataReader) npgsqlDataReader)["clan_match_wins"].ToString()));
          statisticDaily.set_MatchLoses(int.Parse(((DbDataReader) npgsqlDataReader)["clan_match_loses"].ToString()));
          playerStatClanDb = (StatisticClan) statisticDaily;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatClanDb;
  }

  public static bool CreatePlayerStatClanDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_clans(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticDaily GetPlayerStatDailiesDB(long OwnerId)
  {
    StatisticDaily playerStatDailiesDb = (StatisticDaily) null;
    if (OwnerId == 0L)
      return playerStatDailiesDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_dailies WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          StatisticSeason statisticSeason = new StatisticSeason();
          ((StatisticDaily) statisticSeason).OwnerId = OwnerId;
          ((StatisticDaily) statisticSeason).Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString());
          ((StatisticDaily) statisticSeason).MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString());
          ((StatisticDaily) statisticSeason).MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString());
          ((StatisticDaily) statisticSeason).MatchDraws = int.Parse(((DbDataReader) npgsqlDataReader)["match_draws"].ToString());
          ((StatisticDaily) statisticSeason).KillsCount = int.Parse(((DbDataReader) npgsqlDataReader)["kills_count"].ToString());
          ((StatisticDaily) statisticSeason).DeathsCount = int.Parse(((DbDataReader) npgsqlDataReader)["deaths_count"].ToString());
          ((StatisticDaily) statisticSeason).HeadshotsCount = int.Parse(((DbDataReader) npgsqlDataReader)["headshots_count"].ToString());
          ((StatisticDaily) statisticSeason).ExpGained = int.Parse(((DbDataReader) npgsqlDataReader)["exp_gained"].ToString());
          statisticSeason.set_PointGained(int.Parse(((DbDataReader) npgsqlDataReader)["point_gained"].ToString()));
          statisticSeason.set_Playtime(uint.Parse($"{((DbDataReader) npgsqlDataReader)["playtime"]}"));
          playerStatDailiesDb = (StatisticDaily) statisticSeason;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatDailiesDb;
  }

  public static bool CreatePlayerStatDailiesDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_dailies(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticWeapon GetPlayerStatWeaponsDB(long OwnerId)
  {
    StatisticWeapon playerStatWeaponsDb = (StatisticWeapon) null;
    if (OwnerId == 0L)
      return playerStatWeaponsDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_weapons WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          Synchronize synchronize = new Synchronize();
          ((StatisticWeapon) synchronize).OwnerId = OwnerId;
          ((StatisticWeapon) synchronize).AssaultKills = int.Parse(((DbDataReader) npgsqlDataReader)["assault_rifle_kills"].ToString());
          ((StatisticWeapon) synchronize).AssaultDeaths = int.Parse(((DbDataReader) npgsqlDataReader)["assault_rifle_deaths"].ToString());
          ((StatisticWeapon) synchronize).SmgKills = int.Parse(((DbDataReader) npgsqlDataReader)["sub_machine_gun_kills"].ToString());
          ((StatisticWeapon) synchronize).SmgDeaths = int.Parse(((DbDataReader) npgsqlDataReader)["sub_machine_gun_deaths"].ToString());
          ((StatisticWeapon) synchronize).SniperKills = int.Parse(((DbDataReader) npgsqlDataReader)["sniper_rifle_kills"].ToString());
          ((StatisticWeapon) synchronize).SniperDeaths = int.Parse(((DbDataReader) npgsqlDataReader)["sniper_rifle_deaths"].ToString());
          ((StatisticWeapon) synchronize).MachinegunKills = int.Parse(((DbDataReader) npgsqlDataReader)["machine_gun_kills"].ToString());
          ((StatisticWeapon) synchronize).MachinegunDeaths = int.Parse(((DbDataReader) npgsqlDataReader)["machine_gun_deaths"].ToString());
          ((StatisticWeapon) synchronize).ShotgunKills = int.Parse(((DbDataReader) npgsqlDataReader)["shot_gun_kills"].ToString());
          ((StatisticWeapon) synchronize).ShotgunDeaths = int.Parse(((DbDataReader) npgsqlDataReader)["shot_gun_deaths"].ToString());
          playerStatWeaponsDb = (StatisticWeapon) synchronize;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatWeaponsDb;
  }

  public static bool CreatePlayerStatWeaponsDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_weapons(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticAcemode GetPlayerStatAcemodesDB(long OwnerId)
  {
    StatisticAcemode playerStatAcemodesDb = (StatisticAcemode) null;
    if (OwnerId == 0L)
      return playerStatAcemodesDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_acemodes WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          StatisticBattlecup statisticBattlecup = new StatisticBattlecup();
          ((StatisticAcemode) statisticBattlecup).OwnerId = OwnerId;
          ((StatisticAcemode) statisticBattlecup).Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString());
          ((StatisticAcemode) statisticBattlecup).MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString());
          ((StatisticAcemode) statisticBattlecup).MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString());
          ((StatisticAcemode) statisticBattlecup).Kills = int.Parse(((DbDataReader) npgsqlDataReader)["kills_count"].ToString());
          ((StatisticAcemode) statisticBattlecup).Deaths = int.Parse(((DbDataReader) npgsqlDataReader)["deaths_count"].ToString());
          ((StatisticAcemode) statisticBattlecup).Headshots = int.Parse(((DbDataReader) npgsqlDataReader)["headshots_count"].ToString());
          ((StatisticAcemode) statisticBattlecup).Assists = int.Parse(((DbDataReader) npgsqlDataReader)["assists_count"].ToString());
          statisticBattlecup.set_Escapes(int.Parse(((DbDataReader) npgsqlDataReader)["escapes_count"].ToString()));
          statisticBattlecup.set_Winstreaks(int.Parse(((DbDataReader) npgsqlDataReader)["winstreaks_count"].ToString()));
          playerStatAcemodesDb = (StatisticAcemode) statisticBattlecup;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatAcemodesDb;
  }

  public static bool CreatePlayerStatAcemodesDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_acemodes(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static StatisticBattlecup GetPlayerStatBattlecupDB(long OwnerId)
  {
    StatisticBattlecup playerStatBattlecupDb = (StatisticBattlecup) null;
    if (OwnerId == 0L)
      return playerStatBattlecupDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_stat_battlecups WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          StatisticClan statisticClan = new StatisticClan();
          ((StatisticBattlecup) statisticClan).OwnerId = OwnerId;
          ((StatisticBattlecup) statisticClan).Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString());
          ((StatisticBattlecup) statisticClan).MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString());
          ((StatisticBattlecup) statisticClan).MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString());
          ((StatisticBattlecup) statisticClan).KillsCount = int.Parse(((DbDataReader) npgsqlDataReader)["kills_count"].ToString());
          ((StatisticBattlecup) statisticClan).DeathsCount = int.Parse(((DbDataReader) npgsqlDataReader)["deaths_count"].ToString());
          ((StatisticBattlecup) statisticClan).HeadshotsCount = int.Parse(((DbDataReader) npgsqlDataReader)["headshots_count"].ToString());
          ((StatisticBattlecup) statisticClan).AssistsCount = int.Parse(((DbDataReader) npgsqlDataReader)["assists_count"].ToString());
          ((StatisticBattlecup) statisticClan).EscapesCount = int.Parse(((DbDataReader) npgsqlDataReader)["escapes_count"].ToString());
          playerStatBattlecupDb = (StatisticBattlecup) statisticClan;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerStatBattlecupDb;
  }

  public static bool CreatePlayerStatBattlecupsDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_stat_battlecups(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static PlayerTitles GetPlayerTitlesDB(long OwnerId)
  {
    PlayerTitles playerTitlesDb = (PlayerTitles) null;
    if (OwnerId == 0L)
      return playerTitlesDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_titles WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerVip playerVip = new PlayerVip();
          ((PlayerTitles) playerVip).OwnerId = OwnerId;
          ((PlayerTitles) playerVip).Equiped1 = int.Parse(((DbDataReader) npgsqlDataReader)["equip_slot1"].ToString());
          ((PlayerTitles) playerVip).Equiped2 = int.Parse(((DbDataReader) npgsqlDataReader)["equip_slot2"].ToString());
          ((PlayerTitles) playerVip).Equiped3 = int.Parse(((DbDataReader) npgsqlDataReader)["equip_slot3"].ToString());
          ((PlayerTitles) playerVip).Flags = long.Parse(((DbDataReader) npgsqlDataReader)["flags"].ToString());
          ((PlayerTitles) playerVip).Slots = int.Parse(((DbDataReader) npgsqlDataReader)["slots"].ToString());
          playerTitlesDb = (PlayerTitles) playerVip;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerTitlesDb;
  }

  public static bool CreatePlayerTitlesDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_titles(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static PlayerBonus GetPlayerBonusDB(long OwnerId)
  {
    PlayerBonus playerBonusDb = (PlayerBonus) null;
    if (OwnerId == 0L)
      return playerBonusDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_bonus WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
          playerBonusDb = new PlayerBonus()
          {
            OwnerId = OwnerId,
            Bonuses = int.Parse(((DbDataReader) npgsqlDataReader)["bonuses"].ToString()),
            CrosshairColor = int.Parse(((DbDataReader) npgsqlDataReader)["crosshair_color"].ToString()),
            FreePass = int.Parse(((DbDataReader) npgsqlDataReader)["free_pass"].ToString()),
            FakeRank = int.Parse(((DbDataReader) npgsqlDataReader)["fake_rank"].ToString()),
            FakeNick = ((DbDataReader) npgsqlDataReader)["fake_nick"].ToString(),
            MuzzleColor = int.Parse(((DbDataReader) npgsqlDataReader)["muzzle_color"].ToString()),
            NickBorderColor = int.Parse(((DbDataReader) npgsqlDataReader)["nick_border_color"].ToString())
          };
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerBonusDb;
  }

  public static bool CreatePlayerBonusDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_bonus(owner_id) VALUES(@id)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static PlayerConfig GetPlayerConfigDB(long OwnerId)
  {
    PlayerConfig playerConfigDb = (PlayerConfig) null;
    if (OwnerId == 0L)
      return playerConfigDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_configs WHERE owner_id=@owner";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerEvent playerEvent = new PlayerEvent();
          ((PlayerConfig) playerEvent).OwnerId = OwnerId;
          ((PlayerConfig) playerEvent).Config = int.Parse(((DbDataReader) npgsqlDataReader)["configs"].ToString());
          ((PlayerConfig) playerEvent).ShowBlood = int.Parse(((DbDataReader) npgsqlDataReader)["show_blood"].ToString());
          ((PlayerConfig) playerEvent).Crosshair = int.Parse(((DbDataReader) npgsqlDataReader)["crosshair"].ToString());
          ((PlayerConfig) playerEvent).HandPosition = int.Parse(((DbDataReader) npgsqlDataReader)["hand_pos"].ToString());
          ((PlayerConfig) playerEvent).AudioSFX = int.Parse(((DbDataReader) npgsqlDataReader)["audio_sfx"].ToString());
          ((PlayerConfig) playerEvent).AudioBGM = int.Parse(((DbDataReader) npgsqlDataReader)["audio_bgm"].ToString());
          ((PlayerConfig) playerEvent).AudioEnable = int.Parse(((DbDataReader) npgsqlDataReader)["audio_enable"].ToString());
          ((PlayerConfig) playerEvent).Sensitivity = int.Parse(((DbDataReader) npgsqlDataReader)["sensitivity"].ToString());
          ((PlayerConfig) playerEvent).PointOfView = int.Parse(((DbDataReader) npgsqlDataReader)["pov_size"].ToString());
          ((PlayerConfig) playerEvent).InvertMouse = int.Parse(((DbDataReader) npgsqlDataReader)["invert_mouse"].ToString());
          ((PlayerConfig) playerEvent).EnableInviteMsg = int.Parse(((DbDataReader) npgsqlDataReader)["enable_invite"].ToString());
          ((PlayerConfig) playerEvent).EnableWhisperMsg = int.Parse(((DbDataReader) npgsqlDataReader)["enable_whisper"].ToString());
          ((PlayerConfig) playerEvent).Macro = int.Parse(((DbDataReader) npgsqlDataReader)["macro_enable"].ToString());
          ((PlayerConfig) playerEvent).Macro1 = ((DbDataReader) npgsqlDataReader)["macro1"].ToString();
          ((PlayerConfig) playerEvent).Macro2 = ((DbDataReader) npgsqlDataReader)["macro2"].ToString();
          ((PlayerConfig) playerEvent).Macro3 = ((DbDataReader) npgsqlDataReader)["macro3"].ToString();
          ((PlayerConfig) playerEvent).Macro4 = ((DbDataReader) npgsqlDataReader)["macro4"].ToString();
          playerEvent.set_Macro5(((DbDataReader) npgsqlDataReader)["macro5"].ToString());
          ((PlayerConfig) playerEvent).Nations = int.Parse(((DbDataReader) npgsqlDataReader)["nations"].ToString());
          playerConfigDb = (PlayerConfig) playerEvent;
          ((DbDataReader) npgsqlDataReader).GetBytes(19, 0L, ((PlayerEvent) playerConfigDb).get_KeyboardKeys(), 0, 235);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerConfigDb;
  }

  public static bool CreatePlayerConfigDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_configs(owner_id) VALUES(@owner)";
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static PlayerEvent GetPlayerEventDB(long OwnerId)
  {
    PlayerEvent playerEventDb = (PlayerEvent) null;
    if (OwnerId == 0L)
      return playerEventDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_events WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerInfo playerInfo = new PlayerInfo();
          ((PlayerEvent) playerInfo).OwnerId = OwnerId;
          ((PlayerEvent) playerInfo).LastVisitCheckDay = int.Parse(((DbDataReader) npgsqlDataReader)["last_visit_check_day"].ToString());
          ((PlayerEvent) playerInfo).LastVisitSeqType = int.Parse(((DbDataReader) npgsqlDataReader)["last_visit_seq_type"].ToString());
          ((PlayerEvent) playerInfo).LastVisitDate = uint.Parse(((DbDataReader) npgsqlDataReader)["last_visit_date"].ToString());
          playerInfo.set_LastXmasDate(uint.Parse(((DbDataReader) npgsqlDataReader)["last_xmas_date"].ToString()));
          ((PlayerEvent) playerInfo).LastPlaytimeDate = uint.Parse(((DbDataReader) npgsqlDataReader)["last_playtime_date"].ToString());
          ((PlayerEvent) playerInfo).LastPlaytimeValue = (long) int.Parse(((DbDataReader) npgsqlDataReader)["last_playtime_value"].ToString());
          ((PlayerEvent) playerInfo).LastPlaytimeFinish = int.Parse(((DbDataReader) npgsqlDataReader)["last_playtime_finish"].ToString());
          ((PlayerEvent) playerInfo).LastLoginDate = uint.Parse(((DbDataReader) npgsqlDataReader)["last_login_date"].ToString());
          playerInfo.set_LastQuestDate(uint.Parse(((DbDataReader) npgsqlDataReader)["last_quest_date"].ToString()));
          ((PlayerEvent) playerInfo).LastQuestFinish = int.Parse(((DbDataReader) npgsqlDataReader)["last_quest_finish"].ToString());
          playerEventDb = (PlayerEvent) playerInfo;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerEventDb;
  }

  public static bool CreatePlayerEventDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_events (owner_id) VALUES (@id)";
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static List<FriendModel> GetPlayerFriendsDB(long OwnerId)
  {
    List<FriendModel> playerFriendsDb = new List<FriendModel>();
    if (OwnerId == 0L)
      return playerFriendsDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_friends WHERE owner_id=@owner ORDER BY id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          MessageModel messageModel = new MessageModel(long.Parse(((DbDataReader) npgsqlDataReader)["id"].ToString()));
          ((FriendModel) messageModel).OwnerId = OwnerId;
          ((FriendModel) messageModel).ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
          ((FriendModel) messageModel).State = int.Parse(((DbDataReader) npgsqlDataReader)["state"].ToString());
          ((FriendModel) messageModel).Removed = bool.Parse(((DbDataReader) npgsqlDataReader)["removed"].ToString());
          FriendModel friendModel = (FriendModel) messageModel;
          playerFriendsDb.Add(friendModel);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerFriendsDb;
  }

  public static void UpdatePlayerBonus(long OwnerId, [In] int obj1, [In] int obj2)
  {
    if (OwnerId == 0L)
      return;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@id", (object) OwnerId);
        command.Parameters.AddWithValue("@bonuses", (object) obj1);
        command.Parameters.AddWithValue("@freepass", (object) obj2);
        ((DbCommand) command).CommandText = "UPDATE player_bonus SET bonuses=@bonuses, free_pass=@freepass WHERE owner_id=@id";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static List<QuickstartModel> GetPlayerQuickstartsDB(long OwnerId)
  {
    List<QuickstartModel> playerQuickstartsDb = new List<QuickstartModel>();
    if (OwnerId == 0L)
      return playerQuickstartsDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_quickstarts WHERE owner_id=@owner;";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          GoodsItem goodsItem1 = new GoodsItem();
          ((QuickstartModel) goodsItem1).MapId = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["list0_map_id"].ToString());
          ((QuickstartModel) goodsItem1).Rule = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["list0_map_rule"].ToString());
          goodsItem1.set_StageOptions((int) byte.Parse(((DbDataReader) npgsqlDataReader)["list0_map_stage"].ToString()));
          goodsItem1.set_Type((int) byte.Parse(((DbDataReader) npgsqlDataReader)["list0_map_type"].ToString()));
          QuickstartModel quickstartModel1 = (QuickstartModel) goodsItem1;
          playerQuickstartsDb.Add(quickstartModel1);
          GoodsItem goodsItem2 = new GoodsItem();
          ((QuickstartModel) goodsItem2).MapId = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["list1_map_id"].ToString());
          ((QuickstartModel) goodsItem2).Rule = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["list1_map_rule"].ToString());
          goodsItem2.set_StageOptions((int) byte.Parse(((DbDataReader) npgsqlDataReader)["list1_map_stage"].ToString()));
          goodsItem2.set_Type((int) byte.Parse(((DbDataReader) npgsqlDataReader)["list1_map_type"].ToString()));
          QuickstartModel quickstartModel2 = (QuickstartModel) goodsItem2;
          playerQuickstartsDb.Add(quickstartModel2);
          GoodsItem goodsItem3 = new GoodsItem();
          ((QuickstartModel) goodsItem3).MapId = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["list2_map_id"].ToString());
          ((QuickstartModel) goodsItem3).Rule = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["list2_map_rule"].ToString());
          goodsItem3.set_StageOptions((int) byte.Parse(((DbDataReader) npgsqlDataReader)["list2_map_stage"].ToString()));
          goodsItem3.set_Type((int) byte.Parse(((DbDataReader) npgsqlDataReader)["list2_map_type"].ToString()));
          QuickstartModel quickstartModel3 = (QuickstartModel) goodsItem3;
          playerQuickstartsDb.Add(quickstartModel3);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerQuickstartsDb;
  }

  public static bool CreatePlayerQuickstartsDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_quickstarts(owner_id) VALUES(@owner);";
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool IsPlayerNameExist(string PlayerId)
  {
    if (string.IsNullOrEmpty(PlayerId))
      return true;
    try
    {
      int num = 0;
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@name", (object) PlayerId);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM accounts WHERE nickname=@name";
        num = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return num > 0;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static List<NHistoryModel> GetPlayerNickHistory([In] object obj0, int Bonuses)
  {
    List<NHistoryModel> playerNickHistory = new List<NHistoryModel>();
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        string str = Bonuses == 0 ? "WHERE new_nick=@valor" : "WHERE owner_id=@valor";
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@valor", obj0);
        ((DbCommand) command).CommandText = $"SELECT * FROM base_nick_history {str} ORDER BY change_date LIMIT 30";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PassBoxModel passBoxModel = new PassBoxModel();
          ((NHistoryModel) passBoxModel).ObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["object_id"].ToString());
          passBoxModel.set_OwnerId(long.Parse(((DbDataReader) npgsqlDataReader)["owner_id"].ToString()));
          ((NHistoryModel) passBoxModel).OldNick = ((DbDataReader) npgsqlDataReader)["old_nick"].ToString();
          ((NHistoryModel) passBoxModel).NewNick = ((DbDataReader) npgsqlDataReader)["new_nick"].ToString();
          passBoxModel.set_ChangeDate(uint.Parse(((DbDataReader) npgsqlDataReader)["change_date"].ToString()));
          ((NHistoryModel) passBoxModel).Motive = ((DbDataReader) npgsqlDataReader)["motive"].ToString();
          NHistoryModel nhistoryModel = (NHistoryModel) passBoxModel;
          playerNickHistory.Add(nhistoryModel);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerNickHistory;
  }

  public static bool CreatePlayerNickHistory(long OwnerId, [In] string obj1, [In] string obj2, [In] string obj3)
  {
    PassBoxModel passBoxModel = new PassBoxModel();
    passBoxModel.set_OwnerId(OwnerId);
    ((NHistoryModel) passBoxModel).OldNick = obj1;
    ((NHistoryModel) passBoxModel).NewNick = obj2;
    passBoxModel.set_ChangeDate(uint.Parse(DBQuery.Now("yyMMddHHmm")));
    ((NHistoryModel) passBoxModel).Motive = obj3;
    NHistoryModel nhistoryModel = (NHistoryModel) passBoxModel;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) ((PassBoxModel) nhistoryModel).get_OwnerId());
        command.Parameters.AddWithValue("@oldnick", (object) nhistoryModel.OldNick);
        command.Parameters.AddWithValue("@newnick", (object) nhistoryModel.NewNick);
        command.Parameters.AddWithValue("@date", (object) (long) ((PassBoxModel) nhistoryModel).get_ChangeDate());
        command.Parameters.AddWithValue("@motive", (object) nhistoryModel.Motive);
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = "INSERT INTO base_nick_history(owner_id, old_nick, new_nick, change_date, motive) VALUES(@owner, @oldnick, @newnick, @date, @motive)";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
        return true;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool UpdateAccountValuable([In] long obj0, int Type, int NewNick, [In] int obj3)
  {
    if (obj0 == 0L || Type == -1 && NewNick == -1 && obj3 == -1)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@owner", (object) obj0);
        string str = "";
        if (Type > -1)
        {
          command.Parameters.AddWithValue("@gold", (object) Type);
          str += "gold=@gold";
        }
        if (NewNick > -1)
        {
          command.Parameters.AddWithValue("@cash", (object) NewNick);
          str = $"{str}{(str != "" ? ", " : "")}cash=@cash";
        }
        if (obj3 > -1)
        {
          command.Parameters.AddWithValue("@tags", (object) obj3);
          str = $"{str}{(str != "" ? ", " : "")}tags=@tags";
        }
        ((DbCommand) command).CommandText = $"UPDATE accounts SET {str} WHERE player_id=@owner";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool UpdatePlayerKD([In] long obj0, [In] int obj1, [In] int obj2, int Motive, [In] int obj4)
  {
    if (obj0 == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@owner", (object) obj0);
        command.Parameters.AddWithValue("@deaths", (object) obj2);
        command.Parameters.AddWithValue("@kills", (object) obj1);
        command.Parameters.AddWithValue("@hs", (object) Motive);
        command.Parameters.AddWithValue("@total", (object) obj4);
        ((DbCommand) command).CommandText = "UPDATE player_stat_seasons SET kills_count=@kills, deaths_count=@deaths, headshots_count=@hs, total_kills=@total WHERE owner_id=@owner";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool UpdatePlayerMatches(
    int OwnerId,
    int Kills,
    int Deaths,
    int Headshots,
    int Totals,
    [In] long obj5)
  {
    if (obj5 == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@owner", (object) obj5);
        command.Parameters.AddWithValue("@partidas", (object) OwnerId);
        command.Parameters.AddWithValue("@ganhas", (object) Kills);
        command.Parameters.AddWithValue("@perdidas", (object) Deaths);
        command.Parameters.AddWithValue("@empates", (object) Headshots);
        command.Parameters.AddWithValue("@todaspartidas", (object) Totals);
        ((DbCommand) command).CommandText = "UPDATE player_stat_seasons SET matches=@partidas, match_wins=@ganhas, match_loses=@perdidas, match_draws=@empates, total_matches=@todaspartidas WHERE owner_id=@owner";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool UpdateAccountCash([In] long obj0, int MatchWins)
  {
    if (obj0 != 0L)
    {
      if (MatchWins != -1)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            ((DbCommand) command).CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@owner", (object) obj0);
            command.Parameters.AddWithValue("@cash", (object) MatchWins);
            ((DbCommand) command).CommandText = "UPDATE accounts SET cash=@cash WHERE player_id=@owner";
            ((DbCommand) command).ExecuteNonQuery();
            ((Component) command).Dispose();
            ((Component) npgsqlConnection).Dispose();
            ((DbConnection) npgsqlConnection).Close();
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static bool UpdateAccountGold([In] long obj0, [In] int obj1)
  {
    if (obj0 != 0L)
    {
      if (obj1 != -1)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            ((DbCommand) command).CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@owner", (object) obj0);
            command.Parameters.AddWithValue("@gold", (object) obj1);
            ((DbCommand) command).CommandText = "UPDATE accounts SET gold=@gold WHERE player_id=@owner";
            ((DbCommand) command).ExecuteNonQuery();
            ((Component) command).Dispose();
            ((Component) npgsqlConnection).Dispose();
            ((DbConnection) npgsqlConnection).Close();
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static bool UpdateAccountTags([In] long obj0, [In] int obj1)
  {
    if (obj0 != 0L)
    {
      if (obj1 != -1)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            ((DbCommand) command).CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@owner", (object) obj0);
            command.Parameters.AddWithValue("@tag", (object) obj1);
            ((DbCommand) command).CommandText = "UPDATE accounts SET tags=@tag WHERE player_id=@owner";
            ((DbCommand) command).ExecuteNonQuery();
            ((Component) command).Dispose();
            ((Component) npgsqlConnection).Dispose();
            ((DbConnection) npgsqlConnection).Close();
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static void UpdateCouponEffect([In] long obj0, CouponEffects Cash)
  {
    if (obj0 == 0L)
      return;
    ComDiv.UpdateDB("accounts", "coupon_effect", (object) (long) Cash, "player_id", (object) obj0);
  }

  public static int GetRequestClanId([In] long obj0)
  {
    int requestClanId = 0;
    if (obj0 == 0L)
      return requestClanId;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT clan_id FROM system_clan_invites WHERE player_id=@owner";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        if (((DbDataReader) npgsqlDataReader).Read())
          requestClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return requestClanId;
  }

  public static int GetRequestClanCount(int OwnerId)
  {
    int requestClanCount = 0;
    if (OwnerId == 0)
      return requestClanCount;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
        requestClanCount = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return requestClanCount;
  }

  public static List<ClanInvite> GetClanRequestList([In] int obj0)
  {
    List<ClanInvite> clanRequestList = new List<ClanInvite>();
    if (obj0 == 0)
      return clanRequestList;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM system_clan_invites WHERE clan_id=@clan";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          FriendModel friendModel = new FriendModel();
          ((ClanInvite) friendModel).Id = obj0;
          friendModel.set_PlayerId(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()));
          ((ClanInvite) friendModel).InviteDate = uint.Parse(((DbDataReader) npgsqlDataReader)["invite_date"].ToString());
          friendModel.set_Text(((DbDataReader) npgsqlDataReader)["text"].ToString());
          ClanInvite clanInvite = (ClanInvite) friendModel;
          clanRequestList.Add(clanInvite);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return clanRequestList;
  }

  public static int GetPlayerMessagesCount(long PlayerId)
  {
    int playerMessagesCount = 0;
    if (PlayerId == 0L)
      return playerMessagesCount;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) PlayerId);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM player_messages WHERE owner_id=@owner";
        playerMessagesCount = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerMessagesCount;
  }

  public static bool CreatePlayerMessage([In] long obj0, MessageModel Effects)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        command.Parameters.AddWithValue("@sendid", (object) Effects.SenderId);
        command.Parameters.AddWithValue("@clan", (object) Effects.ClanId);
        command.Parameters.AddWithValue("@sendname", (object) Effects.SenderName);
        command.Parameters.AddWithValue("@text", (object) Effects.Text);
        command.Parameters.AddWithValue("@type", (object) Effects.Type);
        command.Parameters.AddWithValue("@state", (object) Effects.State);
        command.Parameters.AddWithValue("@expire", (object) Effects.ExpireDate);
        command.Parameters.AddWithValue("@cb", (object) (int) Effects.ClanNote);
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = "INSERT INTO player_messages(owner_id, sender_id, sender_name, clan_id, clan_note, text, type, state, expire)VALUES(@owner, @sendid, @sendname, @clan, @cb, @text, @type, @state, @expire) RETURNING object_id";
        object obj = ((DbCommand) command).ExecuteScalar();
        Effects.ObjectId = (long) obj;
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
        return true;
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static bool DeletePlayerFriend(long ClanId, [In] long obj1)
  {
    return ComDiv.DeleteDB("player_friends", "id", (object) ClanId, "owner_id", (object) obj1);
  }

  public static void UpdatePlayerFriendState(long OwnerId, [In] FriendModel obj1)
  {
    ComDiv.UpdateDB("player_friends", "state", (object) obj1.State, "owner_id", (object) OwnerId, "id", (object) obj1.PlayerId);
  }

  public static void UpdatePlayerFriendBlock([In] long obj0, FriendModel Message)
  {
    ComDiv.UpdateDB("player_friends", "removed", (object) Message.Removed, "owner_id", (object) obj0, "id", (object) Message.PlayerId);
  }

  public static bool DeleteClanInviteDB([In] int obj0, long pId)
  {
    return pId != 0L && obj0 != 0 && ComDiv.DeleteDB("system_clan_invites", "clan_id", (object) obj0, "player_id", (object) pId);
  }

  public static bool DeleteClanInviteDB([In] long obj0)
  {
    return obj0 != 0L && ComDiv.DeleteDB("system_clan_invites", "player_id", (object) obj0);
  }

  public static bool CreateClanInviteInDB(ClanInvite OwnerId)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) OwnerId.Id);
        command.Parameters.AddWithValue("@player", (object) ((FriendModel) OwnerId).get_PlayerId());
        command.Parameters.AddWithValue("@date", (object) (long) OwnerId.InviteDate);
        command.Parameters.AddWithValue("@text", (object) ((FriendModel) OwnerId).get_Text());
        ((DbCommand) command).CommandText = "INSERT INTO system_clan_invites(clan_id, player_id, invite_date, text)VALUES(@clan,@player,@date,@text)";
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static int GetRequestClanInviteCount([In] int obj0)
  {
    int requestClanInviteCount = 0;
    if (obj0 == 0)
      return requestClanInviteCount;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM system_clan_invites WHERE clan_id=@clan";
        requestClanInviteCount = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return requestClanInviteCount;
  }

  public static string GetRequestClanInviteText(int ClanId, long PlayerId)
  {
    string requestClanInviteText = (string) null;
    if (ClanId != 0)
    {
      if (PlayerId != 0L)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            command.Parameters.AddWithValue("@clan", (object) ClanId);
            command.Parameters.AddWithValue("@player", (object) PlayerId);
            ((DbCommand) command).CommandText = "SELECT text FROM system_clan_invites WHERE clan_id=@clan AND player_id=@player";
            ((DbCommand) command).CommandType = CommandType.Text;
            NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
            if (((DbDataReader) npgsqlDataReader).Read())
              requestClanInviteText = ((DbDataReader) npgsqlDataReader)["text"].ToString();
            ((Component) command).Dispose();
            ((DbDataReader) npgsqlDataReader).Close();
            ((DbConnection) npgsqlConnection).Close();
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
        return requestClanInviteText;
      }
    }
    return requestClanInviteText;
  }

  public static string GetPlayerIP4Address(long PlayerId)
  {
    string playerIp4Address = "";
    if (PlayerId == 0L)
      return playerIp4Address;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@player", (object) PlayerId);
        ((DbCommand) command).CommandText = "SELECT ip4_address FROM accounts WHERE player_id=@player";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        if (((DbDataReader) npgsqlDataReader).Read())
          playerIp4Address = ((DbDataReader) npgsqlDataReader)["ip4_address"].ToString();
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerIp4Address;
  }

  public static PlayerMissions GetPlayerMissionsDB(
    long invite,
    int PlayerId,
    [In] int obj2,
    [In] int obj3,
    [In] int obj4)
  {
    PlayerMissions playerMissionsDb = (PlayerMissions) null;
    if (invite == 0L)
      return playerMissionsDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) invite);
        ((DbCommand) command).CommandText = "SELECT * FROM player_missions WHERE owner_id=@owner";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          playerMissionsDb = new PlayerMissions()
          {
            OwnerId = invite,
            ActualMission = int.Parse(((DbDataReader) npgsqlDataReader)["current_mission"].ToString()),
            Card1 = int.Parse(((DbDataReader) npgsqlDataReader)["card1"].ToString()),
            Card2 = int.Parse(((DbDataReader) npgsqlDataReader)["card2"].ToString()),
            Card3 = int.Parse(((DbDataReader) npgsqlDataReader)["card3"].ToString()),
            Card4 = int.Parse(((DbDataReader) npgsqlDataReader)["card4"].ToString()),
            Mission1 = PlayerId,
            Mission2 = obj2,
            Mission3 = obj3,
            Mission4 = obj4
          };
          ((DbDataReader) npgsqlDataReader).GetBytes(6, 0L, playerMissionsDb.List1, 0, 40);
          ((DbDataReader) npgsqlDataReader).GetBytes(7, 0L, playerMissionsDb.List2, 0, 40);
          ((DbDataReader) npgsqlDataReader).GetBytes(8, 0L, playerMissionsDb.List3, 0, 40);
          ((DbDataReader) npgsqlDataReader).GetBytes(9, 0L, playerMissionsDb.List4, 0, 40);
          ((PlayerQuickstart) playerMissionsDb).UpdateSelectedCard();
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerMissionsDb;
  }

  public static bool CreatePlayerMissionsDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) OwnerId);
        ((DbCommand) command).CommandText = "INSERT INTO player_missions(owner_id) VALUES(@owner)";
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static void UpdateCurrentPlayerMissionList([In] long obj0, PlayerMissions Mission1)
  {
    byte[] currentMissionList = ((PlayerQuickstart) Mission1).GetCurrentMissionList();
    ComDiv.UpdateDB("player_missions", $"mission{Mission1.ActualMission + 1}_raw", (object) currentMissionList, "owner_id", (object) obj0);
  }

  public static bool DeletePlayerCharacter([In] long obj0, [In] long obj1)
  {
    return obj0 != 0L && obj1 != 0L && ComDiv.DeleteDB("player_characters", "object_id", (object) obj0, "owner_id", (object) obj1);
  }

  public static bool UpdatePlayerCharacter(int OwnerId, long mission, [In] long obj2)
  {
    return ComDiv.UpdateDB("player_characters", "slot", (object) OwnerId, "object_id", (object) mission, "owner_id", (object) obj2);
  }

  public static bool UpdateEquipedPlayerTitle(long ObjectId, int OwnerId, [In] int obj2)
  {
    return ComDiv.UpdateDB("player_titles", $"equip_slot{OwnerId + 1}", (object) obj2, "owner_id", (object) ObjectId);
  }

  public static void UpdatePlayerTitlesFlags([In] long obj0, long ObjectId)
  {
    ComDiv.UpdateDB("player_titles", "flags", (object) ObjectId, "owner_id", (object) obj0);
  }

  public static void UpdatePlayerTitleRequi(
    long player_id,
    int index,
    int titleId,
    [In] int obj3,
    [In] int obj4)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@pid", (object) player_id);
        command.Parameters.AddWithValue("@broche", (object) obj4);
        command.Parameters.AddWithValue("@insignias", (object) titleId);
        command.Parameters.AddWithValue("@medalhas", (object) index);
        command.Parameters.AddWithValue("@ordensazuis", (object) obj3);
        ((DbCommand) command).CommandType = CommandType.Text;
        ((DbCommand) command).CommandText = "UPDATE accounts SET ribbon=@broche, ensign=@insignias, medal=@medalhas, master_medal=@ordensazuis WHERE player_id=@pid";
        ((DbCommand) command).ExecuteNonQuery();
        ((Component) command).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static bool UpdatePlayerMissionId(long player_id, int medalhas, int insignias)
  {
    return ComDiv.UpdateDB("accounts", $"mission_id{insignias + 1}", (object) medalhas, nameof (player_id), (object) player_id);
  }

  public static int GetUsedTicket([In] long obj0, [In] string obj1)
  {
    int usedTicket = 0;
    if (obj0 != 0L)
    {
      if (!string.IsNullOrEmpty(obj1))
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            command.Parameters.AddWithValue("@player", (object) obj0);
            command.Parameters.AddWithValue("@token", (object) obj1);
            ((DbCommand) command).CommandText = "SELECT used_count FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
            ((DbCommand) command).CommandType = CommandType.Text;
            NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
            if (((DbDataReader) npgsqlDataReader).Read())
              usedTicket = int.Parse(((DbDataReader) npgsqlDataReader)["used_count"].ToString());
            ((Component) command).Dispose();
            ((DbDataReader) npgsqlDataReader).Close();
            ((DbConnection) npgsqlConnection).Close();
          }
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
        }
        return usedTicket;
      }
    }
    return usedTicket;
  }

  public static bool IsTicketUsedByPlayer(long player_id, string value)
  {
    bool flag = false;
    if (player_id == 0L)
      return flag;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@player", (object) player_id);
        command.Parameters.AddWithValue("@token", (object) value);
        ((DbCommand) command).CommandText = "SELECT * FROM base_redeem_history WHERE used_token=@token AND owner_id=@player";
        ((DbCommand) command).CommandType = CommandType.Text;
        flag = Convert.ToBoolean(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return flag;
  }

  public static bool CreatePlayerRedeemHistory([In] long obj0, [In] string obj1, int index)
  {
    if (obj0 != 0L && !string.IsNullOrEmpty(obj1))
    {
      if (index != 0)
      {
        try
        {
          using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
          {
            NpgsqlCommand command = npgsqlConnection.CreateCommand();
            ((DbConnection) npgsqlConnection).Open();
            command.Parameters.AddWithValue("@owner", (object) obj0);
            command.Parameters.AddWithValue("@token", (object) obj1);
            command.Parameters.AddWithValue("@used", (object) index);
            ((DbCommand) command).CommandText = "INSERT INTO base_redeem_history(owner_id, used_token, used_count) VALUES(@owner, @token, @used)";
            ((DbCommand) command).CommandType = CommandType.Text;
            ((DbCommand) command).ExecuteNonQuery();
            ((Component) command).Dispose();
            ((Component) npgsqlConnection).Dispose();
            ((DbConnection) npgsqlConnection).Close();
          }
          return true;
        }
        catch (Exception ex)
        {
          // ISSUE: reference to a compiler-generated method
          CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
          return false;
        }
      }
    }
    return false;
  }

  public static PlayerVip GetPlayerVIP(long OwnerId)
  {
    PlayerVip playerVip = (PlayerVip) null;
    if (OwnerId == 0L)
      return playerVip;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@ownerId", (object) OwnerId);
        ((DbCommand) command).CommandText = "SELECT * FROM player_vip WHERE owner_id=@ownerId";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        if (((DbDataReader) npgsqlDataReader).Read())
        {
          RecordInfo recordInfo = new RecordInfo();
          ((PlayerVip) recordInfo).OwnerId = OwnerId;
          ((PlayerVip) recordInfo).Address = ((DbDataReader) npgsqlDataReader)["registered_ip"].ToString();
          recordInfo.set_Benefit(((DbDataReader) npgsqlDataReader)["last_benefit"].ToString());
          recordInfo.set_Expirate(uint.Parse(((DbDataReader) npgsqlDataReader)["expirate"].ToString()));
          playerVip = (PlayerVip) recordInfo;
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerVip;
  }

  public static PlayerReport GetPlayerReportDB([In] long obj0)
  {
    PlayerReport playerReportDb = (PlayerReport) null;
    if (obj0 == 0L)
      return playerReportDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          command.Parameters.AddWithValue("@owner", (object) obj0);
          ((DbCommand) command).CommandText = "SELECT * FROM player_reports WHERE owner_id=@owner";
          ((DbCommand) command).CommandType = CommandType.Text;
          using (NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default))
          {
            while (((DbDataReader) npgsqlDataReader).Read())
            {
              PlayerTitles playerTitles = new PlayerTitles();
              ((PlayerReport) playerTitles).OwnerId = obj0;
              playerTitles.set_TicketCount(int.Parse(((DbDataReader) npgsqlDataReader)["ticket_count"].ToString()));
              playerTitles.set_ReportedCount(int.Parse(((DbDataReader) npgsqlDataReader)["reported_count"].ToString()));
              playerReportDb = (PlayerReport) playerTitles;
            }
            ((DbDataReader) npgsqlDataReader).Close();
            ((DbConnection) npgsqlConnection).Close();
          }
        }
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
    }
    return playerReportDb;
  }

  public static bool CreatePlayerReportDB(long OwnerId)
  {
    if (OwnerId == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          command.Parameters.AddWithValue("@owner", (object) OwnerId);
          ((DbCommand) command).CommandText = "INSERT INTO player_reports(owner_id) VALUES(@owner)";
          ((DbCommand) command).CommandType = CommandType.Text;
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((Component) npgsqlConnection).Dispose();
          ((DbConnection) npgsqlConnection).Close();
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }
}
