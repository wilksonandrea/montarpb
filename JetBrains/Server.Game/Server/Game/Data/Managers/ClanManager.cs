// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Managers.ClanManager
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Managers;

public static class ClanManager
{
  public static List<ClanModel> Clans;

  public static Account GetAccount(long acc, [In] int obj1)
  {
    if (acc == 0L)
      return (Account) null;
    try
    {
      Account account;
      return AccountManager.Accounts.TryGetValue(acc, out account) ? account : AccountManager.GetAccountDB((object) acc, 2, obj1);
    }
    catch
    {
      return (Account) null;
    }
  }

  public static Account GetAccount([In] long obj0, bool type)
  {
    if (obj0 == 0L)
      return (Account) null;
    try
    {
      Account account;
      return AccountManager.Accounts.TryGetValue(obj0, out account) ? account : (type ? (Account) null : AccountManager.GetAccountDB((object) obj0, 2, 7455));
    }
    catch
    {
      return (Account) null;
    }
  }

  public static Account GetAccount(string System, int searchFlag, [In] int obj2)
  {
    if (string.IsNullOrEmpty(System))
      return (Account) null;
    foreach (Account account in (IEnumerable<Account>) AccountManager.Accounts.Values)
    {
      if (account != null && (searchFlag == 1 && account.Nickname == System && account.Nickname.Length > 0 || searchFlag == 0 && string.Compare(account.Username, System) == 0))
        return account;
    }
    return AccountManager.GetAccountDB((object) System, searchFlag, obj2);
  }

  public static bool UpdatePlayerName(string id, long noUseDB)
  {
    return ComDiv.UpdateDB("accounts", "nickname", (object) id, "player_id", (object) noUseDB);
  }

  static ClanManager() => AccountManager.Accounts = new SortedList<long, Account>();

  public static void Load()
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandText = "SELECT * FROM system_clan";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          long num = long.Parse(((DbDataReader) npgsqlDataReader)["owner_id"].ToString());
          if (num != 0L)
          {
            ClanModel clanModel = new ClanModel()
            {
              Id = int.Parse(((DbDataReader) npgsqlDataReader)["id"].ToString()),
              Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString()),
              Name = ((DbDataReader) npgsqlDataReader)["name"].ToString(),
              OwnerId = num,
              Logo = uint.Parse(((DbDataReader) npgsqlDataReader)["logo"].ToString()),
              NameColor = int.Parse(((DbDataReader) npgsqlDataReader)["name_color"].ToString()),
              Info = ((DbDataReader) npgsqlDataReader)["info"].ToString(),
              News = ((DbDataReader) npgsqlDataReader)["news"].ToString(),
              CreationDate = uint.Parse(((DbDataReader) npgsqlDataReader)["create_date"].ToString()),
              Authority = int.Parse(((DbDataReader) npgsqlDataReader)["authority"].ToString()),
              RankLimit = int.Parse(((DbDataReader) npgsqlDataReader)["rank_limit"].ToString()),
              MinAgeLimit = int.Parse(((DbDataReader) npgsqlDataReader)["min_age_limit"].ToString()),
              MaxAgeLimit = int.Parse(((DbDataReader) npgsqlDataReader)["max_age_limit"].ToString()),
              JoinType = (JoinClanType) int.Parse(((DbDataReader) npgsqlDataReader)["join_permission"].ToString()),
              Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString()),
              MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString()),
              MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString()),
              Points = (float) int.Parse(((DbDataReader) npgsqlDataReader)["point"].ToString()),
              MaxPlayers = int.Parse(((DbDataReader) npgsqlDataReader)["max_players"].ToString()),
              Exp = int.Parse(((DbDataReader) npgsqlDataReader)["exp"].ToString()),
              Effect = int.Parse(((DbDataReader) npgsqlDataReader)["effects"].ToString())
            };
            string str1 = ((DbDataReader) npgsqlDataReader)["best_exp"].ToString();
            string str2 = ((DbDataReader) npgsqlDataReader)["best_participants"].ToString();
            string str3 = ((DbDataReader) npgsqlDataReader)["best_wins"].ToString();
            string str4 = ((DbDataReader) npgsqlDataReader)["best_kills"].ToString();
            string str5 = ((DbDataReader) npgsqlDataReader)["best_headshots"].ToString();
            clanModel.BestPlayers.SetPlayers(str1, str2, str3, str4, str5);
            ClanManager.Clans.Add(clanModel);
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
  }

  public static List<ClanModel> GetClanListPerPage(int text)
  {
    List<ClanModel> clanListPerPage = new List<ClanModel>();
    if (text == 0)
      return clanListPerPage;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@page", (object) (170 * text));
        ((DbCommand) command).CommandText = "SELECT * FROM system_clan ORDER BY id DESC OFFSET @page LIMIT 170";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          long num = long.Parse(((DbDataReader) npgsqlDataReader)["owner_id"].ToString());
          if (num != 0L)
          {
            ClanModel clanModel = new ClanModel()
            {
              Id = int.Parse(((DbDataReader) npgsqlDataReader)["id"].ToString()),
              Rank = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString()),
              Name = ((DbDataReader) npgsqlDataReader)["name"].ToString(),
              OwnerId = num,
              Logo = uint.Parse(((DbDataReader) npgsqlDataReader)["logo"].ToString()),
              NameColor = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["name_color"].ToString()),
              Info = ((DbDataReader) npgsqlDataReader)["info"].ToString(),
              News = ((DbDataReader) npgsqlDataReader)["news"].ToString(),
              CreationDate = uint.Parse(((DbDataReader) npgsqlDataReader)["create_date"].ToString()),
              Authority = int.Parse(((DbDataReader) npgsqlDataReader)["authority"].ToString()),
              RankLimit = int.Parse(((DbDataReader) npgsqlDataReader)["rank_limit"].ToString()),
              MinAgeLimit = int.Parse(((DbDataReader) npgsqlDataReader)["age_limit_start"].ToString()),
              MaxAgeLimit = int.Parse(((DbDataReader) npgsqlDataReader)["age_limit_end"].ToString()),
              JoinType = (JoinClanType) int.Parse(((DbDataReader) npgsqlDataReader)["join_permission"].ToString()),
              Matches = int.Parse(((DbDataReader) npgsqlDataReader)["matches"].ToString()),
              MatchWins = int.Parse(((DbDataReader) npgsqlDataReader)["match_wins"].ToString()),
              MatchLoses = int.Parse(((DbDataReader) npgsqlDataReader)["match_loses"].ToString()),
              Points = (float) int.Parse(((DbDataReader) npgsqlDataReader)["point"].ToString()),
              MaxPlayers = int.Parse(((DbDataReader) npgsqlDataReader)["max_players"].ToString()),
              Exp = int.Parse(((DbDataReader) npgsqlDataReader)["exp"].ToString()),
              Effect = (int) byte.Parse(((DbDataReader) npgsqlDataReader)["effects"].ToString())
            };
            string str1 = ((DbDataReader) npgsqlDataReader)["best_exp"].ToString();
            string str2 = ((DbDataReader) npgsqlDataReader)["best_participants"].ToString();
            string str3 = ((DbDataReader) npgsqlDataReader)["best_wins"].ToString();
            string str4 = ((DbDataReader) npgsqlDataReader)["best_kills"].ToString();
            string str5 = ((DbDataReader) npgsqlDataReader)["best_headshots"].ToString();
            clanModel.BestPlayers.SetPlayers(str1, str2, str3, str4, str5);
            clanListPerPage.Add(clanModel);
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
    return clanListPerPage;
  }

  public static ClanModel GetClan([In] int obj0)
  {
    if (obj0 == 0)
      return new ClanModel();
    lock (ClanManager.Clans)
    {
      foreach (ClanModel clan in ClanManager.Clans)
      {
        if (clan.Id == obj0)
          return clan;
      }
    }
    return new ClanModel();
  }

  public static List<Account> GetClanPlayers([In] int obj0, [In] long obj1, bool searchFlag)
  {
    List<Account> clanPlayers = new List<Account>();
    if (obj0 == 0)
      return clanPlayers;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT player_id, nickname, nick_color, rank, online, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          long num = long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString());
          if (num != obj1)
          {
            Account account1 = new Account()
            {
              PlayerId = num,
              Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString(),
              NickColor = int.Parse(((DbDataReader) npgsqlDataReader)["nick_color"].ToString()),
              Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString()),
              IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString()),
              ClanId = obj0,
              ClanAccess = int.Parse(((DbDataReader) npgsqlDataReader)["clan_access"].ToString()),
              ClanDate = uint.Parse(((DbDataReader) npgsqlDataReader)["clan_date"].ToString())
            };
            account1.Bonus = DaoManagerSQL.GetPlayerBonusDB(account1.PlayerId);
            account1.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account1.PlayerId);
            account1.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account1.PlayerId);
            account1.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), account1.PlayerId);
            if (searchFlag)
            {
              Account account2 = ClanManager.GetAccount(account1.PlayerId, true);
              if (account2 != null)
                account1.Connection = account2.Connection;
            }
            clanPlayers.Add(account1);
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

  public static List<Account> GetClanPlayers(int Page, long Exception, [In] bool obj2, [In] bool obj3)
  {
    List<Account> clanPlayers = new List<Account>();
    if (Page == 0)
      return clanPlayers;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@clan", (object) Page);
        command.Parameters.AddWithValue("@on", (object) obj3);
        ((DbCommand) command).CommandText = "SELECT player_id, nickname, nick_color, rank, clan_access, clan_date, status FROM accounts WHERE clan_id=@clan AND online=@on";
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          long int64 = ((DbDataReader) npgsqlDataReader).GetInt64(0);
          if (int64 != Exception)
          {
            Account account1 = new Account()
            {
              PlayerId = int64,
              Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString(),
              NickColor = int.Parse(((DbDataReader) npgsqlDataReader)["nick_color"].ToString()),
              Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString()),
              IsOnline = obj3,
              ClanId = Page,
              ClanAccess = int.Parse(((DbDataReader) npgsqlDataReader)["clan_access"].ToString()),
              ClanDate = uint.Parse(((DbDataReader) npgsqlDataReader)["clan_date"].ToString())
            };
            account1.Bonus = DaoManagerSQL.GetPlayerBonusDB(account1.PlayerId);
            account1.Equipment = DaoManagerSQL.GetPlayerEquipmentsDB(account1.PlayerId);
            account1.Statistic.Clan = DaoManagerSQL.GetPlayerStatClanDB(account1.PlayerId);
            account1.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), account1.PlayerId);
            if (obj2)
            {
              Account account2 = ClanManager.GetAccount(account1.PlayerId, true);
              if (account2 != null)
                account1.Connection = account2.Connection;
            }
            clanPlayers.Add(account1);
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

  public static void SendPacket([In] GameServerPacket obj0, [In] List<Account> obj1)
  {
    if (obj1.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("ClanManager.SendPacket");
    foreach (Account account in obj1)
      account.SendCompletePacket(completeBytes, obj0.GetType().Name, false);
  }

  public static void SendPacket([In] GameServerPacket obj0, List<Account> Exception, long UseCache)
  {
    if (Exception.Count == 0)
      return;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj0).GetCompleteBytes("ClanManager.SendPacket");
    foreach (Account account in Exception)
    {
      if (account.PlayerId != UseCache)
        account.SendCompletePacket(completeBytes, obj0.GetType().Name, false);
    }
  }

  public static void SendPacket(
    GameServerPacket Packet,
    int Players,
    long Exception,
    [In] bool obj3,
    [In] bool obj4)
  {
    ClanManager.SendPacket(Packet, ClanManager.GetClanPlayers(Players, Exception, obj3, obj4));
  }
}
