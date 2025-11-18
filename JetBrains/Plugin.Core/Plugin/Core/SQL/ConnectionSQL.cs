// Decompiled with JetBrains decompiler
// Type: Plugin.Core.SQL.ConnectionSQL
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

#nullable disable
namespace Plugin.Core.SQL;

[Synchronization]
public class ConnectionSQL
{
  private static ConnectionSQL connectionSQL_0;
  protected NpgsqlConnectionStringBuilder ConnBuilder;

  public static bool CreatePlayerReportHistory(
    [In] long obj0,
    long Token,
    string Used,
    [In] string obj3,
    [In] ReportType obj4,
    [In] string obj5)
  {
    BattlePassModel battlePassModel = new BattlePassModel();
    ((RHistoryModel) battlePassModel).OwnerId = obj0;
    ((RHistoryModel) battlePassModel).OwnerNick = Used;
    ((RHistoryModel) battlePassModel).SenderId = Token;
    ((RHistoryModel) battlePassModel).SenderNick = obj3;
    ((RHistoryModel) battlePassModel).Date = uint.Parse(DBQuery.Now("yyMMddHHmm"));
    battlePassModel.set_Type(obj4);
    battlePassModel.set_Message(obj5);
    RHistoryModel rhistoryModel = (RHistoryModel) battlePassModel;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        using (NpgsqlCommand command = npgsqlConnection.CreateCommand())
        {
          ((DbConnection) npgsqlConnection).Open();
          command.Parameters.AddWithValue("@OwnerId", (object) rhistoryModel.OwnerId);
          command.Parameters.AddWithValue("@OwnerNick", (object) rhistoryModel.OwnerNick);
          command.Parameters.AddWithValue("@SenderId", (object) rhistoryModel.SenderId);
          command.Parameters.AddWithValue("@SenderNick", (object) rhistoryModel.SenderNick);
          command.Parameters.AddWithValue("@Date", (object) (long) rhistoryModel.Date);
          command.Parameters.AddWithValue("@Type", (object) (int) ((BattlePassModel) rhistoryModel).get_Type());
          command.Parameters.AddWithValue("@Message", (object) ((BattlePassModel) rhistoryModel).get_Message());
          ((DbCommand) command).CommandText = "INSERT INTO base_report_history(date, owner_id, owner_nick, sender_id, sender_nick, type, message) VALUES(@Date, @OwnerId, @OwnerNick, @SenderId, @SenderNick, @Type, @Message)";
          ((DbCommand) command).CommandType = CommandType.Text;
          ((DbCommand) command).ExecuteNonQuery();
          ((Component) command).Dispose();
          ((Component) npgsqlConnection).Dispose();
          ((DbConnection) npgsqlConnection).Close();
          return true;
        }
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static PlayerBattlepass GetPlayerBattlepassDB([In] long obj0)
  {
    PlayerBattlepass playerBattlepassDb = (PlayerBattlepass) null;
    if (obj0 == 0L)
      return playerBattlepassDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_battlepass WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerCharacters playerCharacters = new PlayerCharacters();
          ((PlayerBattlepass) playerCharacters).Id = int.Parse(((DbDataReader) npgsqlDataReader)["id"].ToString());
          ((PlayerBattlepass) playerCharacters).Level = int.Parse(((DbDataReader) npgsqlDataReader)["level"].ToString());
          ((PlayerBattlepass) playerCharacters).IsPremium = bool.Parse(((DbDataReader) npgsqlDataReader)["premium"].ToString());
          ((PlayerBattlepass) playerCharacters).TotalPoints = int.Parse(((DbDataReader) npgsqlDataReader)["total_points"].ToString());
          playerCharacters.set_DailyPoints(int.Parse(((DbDataReader) npgsqlDataReader)["daily_points"].ToString()));
          playerCharacters.set_LastRecord(uint.Parse(((DbDataReader) npgsqlDataReader)["last_record"].ToString()));
          playerBattlepassDb = (PlayerBattlepass) playerCharacters;
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
    return playerBattlepassDb;
  }

  public static PlayerCompetitive GetPlayerCompetitiveDB([In] long obj0)
  {
    PlayerCompetitive playerCompetitiveDb = (PlayerCompetitive) null;
    if (obj0 == 0L)
      return playerCompetitiveDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@id", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT * FROM player_competitive WHERE owner_id=@id";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          PlayerEquipment playerEquipment = new PlayerEquipment();
          ((PlayerCompetitive) playerEquipment).OwnerId = obj0;
          playerEquipment.set_Level(int.Parse(((DbDataReader) npgsqlDataReader)["level"].ToString()));
          playerEquipment.set_Points(int.Parse(((DbDataReader) npgsqlDataReader)["points"].ToString()));
          playerCompetitiveDb = (PlayerCompetitive) playerEquipment;
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
    return playerCompetitiveDb;
  }

  public static bool CreatePlayerBattlepassDB([In] long obj0)
  {
    if (obj0 == 0L)
      return false;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ((BattleBoxXML) BattleBoxXML.GetInstance()).Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@owner", (object) obj0);
        ((DbCommand) command).CommandText = "INSERT INTO player_battlepass VALUES(@owner);";
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
