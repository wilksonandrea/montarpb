// Decompiled with JetBrains decompiler
// Type: dummy_ptr.{1a8343ff-a99e-485e-ad67-094168725faa}
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

#nullable disable
namespace dummy_ptr;

internal abstract class \u007B1a8343ff\u002Da99e\u002D485e\u002Dad67\u002D094168725faa\u007D
{
  static \u007B1a8343ff\u002Da99e\u002D485e\u002Dad67\u002D094168725faa\u007D()
  {
    AccountManager.Accounts = new SortedList<long, Account>();
  }

  public static ClanModel GetClanDB(object id, int noUseDB)
  {
    ClanModel clanDb = new ClanModel();
    if (noUseDB == 1 && (int) id <= 0 || noUseDB == 0 && string.IsNullOrEmpty(id.ToString()))
      return clanDb;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        string str = noUseDB == 0 ? "name" : nameof (id);
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@valor", id);
        ((DbCommand) command).CommandText = $"SELECT * FROM system_clan WHERE {str}=@valor";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          clanDb.Id = int.Parse(((DbDataReader) npgsqlDataReader)[nameof (id)].ToString());
          clanDb.Rank = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
          clanDb.Name = ((DbDataReader) npgsqlDataReader)["name"].ToString();
          clanDb.OwnerId = long.Parse(((DbDataReader) npgsqlDataReader)["owner_id"].ToString());
          clanDb.Logo = uint.Parse(((DbDataReader) npgsqlDataReader)["logo"].ToString());
          clanDb.NameColor = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["name_color"].ToString());
          clanDb.Effect = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["effects"].ToString());
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return clanDb.Id == 0 ? new ClanModel() : clanDb;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return new ClanModel();
    }
  }

  public static List<Account> GetClanPlayers([Out] int Player, long Username)
  {
    List<Account> clanPlayers = new List<Account>();
    if (Player <= 0)
      return clanPlayers;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) Player);
        ((DbCommand) command).CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          long key = long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString());
          if (key != Username)
          {
            Account account = new Account()
            {
              PlayerId = key,
              Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString(),
              Rank = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString()),
              IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString())
            };
            account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
            account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
            account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
            account.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), key);
            if (account.IsOnline && !AccountManager.Accounts.ContainsKey(key))
            {
              account.SetOnlineStatus(false);
              account.Status.ResetData(account.PlayerId);
            }
            clanPlayers.Add(account);
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
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return clanPlayers;
  }

  public static List<Account> GetClanPlayers([In] int obj0, [In] long obj1, bool Password)
  {
    List<Account> clanPlayers = new List<Account>();
    if (obj0 <= 0)
      return clanPlayers;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) obj0);
        command.Parameters.AddWithValue("@on", (object) Password);
        ((DbCommand) command).CommandText = "SELECT player_id, nickname, rank, online, status FROM accounts WHERE clan_id=@clan AND online=@on";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          long key = long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString());
          if (key != obj1)
          {
            Account account = new Account()
            {
              PlayerId = key,
              Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString(),
              Rank = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString()),
              IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString())
            };
            account.Bonus = DaoManagerSQL.GetPlayerBonusDB(account.PlayerId);
            account.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account.PlayerId);
            account.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account.PlayerId);
            account.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), key);
            if (account.IsOnline && !AccountManager.Accounts.ContainsKey(key))
            {
              account.SetOnlineStatus(false);
              account.Status.ResetData(account.PlayerId);
            }
            clanPlayers.Add(account);
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
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return clanPlayers;
  }

  public abstract void mp000001();

  public abstract void mp000002();

  public abstract void mp000003();

  public abstract void mp000004();

  public abstract void mp000005();
}
