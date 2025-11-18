// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Managers.AccountManager
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Managers;

public class AccountManager
{
  public static SortedList<long, Account> Accounts;

  [CompilerGenerated]
  [SpecialName]
  public int get_CashBonus() => ((ChannelModel) this).int_6;

  [CompilerGenerated]
  [SpecialName]
  public void set_CashBonus(int value) => ((ChannelModel) this).int_6 = value;

  [CompilerGenerated]
  [SpecialName]
  public string get_Password() => ((ChannelModel) this).string_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_Password(string value) => ((ChannelModel) this).string_0 = value;

  public AccountManager(int value) => ((ChannelModel) this).ServerId = value;

  public static bool AddAccount(Account value)
  {
    lock (AccountManager.Accounts)
    {
      if (!AccountManager.Accounts.ContainsKey(value.PlayerId))
      {
        AccountManager.Accounts.Add(value.PlayerId, value);
        return true;
      }
    }
    return false;
  }

  public static Account GetAccountDB(object value, [In] object obj1, [In] int obj2, [In] int obj3)
  {
    if (obj2 == 0 && (string) value == "" || obj2 == 1 && (long) value == 0L || obj2 == 2 && (string.IsNullOrEmpty((string) value) || string.IsNullOrEmpty((string) obj1)))
      return (Account) null;
    Account accountDb = (Account) null;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@valor", value);
        switch (obj2)
        {
          case 0:
            ((DbCommand) command).CommandText = "SELECT * FROM accounts WHERE username=@valor LIMIT 1";
            break;
          case 1:
            ((DbCommand) command).CommandText = "SELECT * FROM accounts WHERE player_id=@valor LIMIT 1";
            break;
          case 2:
            command.Parameters.AddWithValue("@valor2", obj1);
            ((DbCommand) command).CommandText = "SELECT * FROM accounts WHERE username=@valor AND password=@valor2 LIMIT 1";
            break;
        }
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          accountDb = new Account()
          {
            Username = ((DbDataReader) npgsqlDataReader)["username"].ToString(),
            Password = ((DbDataReader) npgsqlDataReader)["password"].ToString()
          };
          accountDb.SetPlayerId(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()), obj3);
          accountDb.Email = ((DbDataReader) npgsqlDataReader)["email"].ToString();
          accountDb.Age = int.Parse(((DbDataReader) npgsqlDataReader)["age"].ToString());
          accountDb.MacAddress = (PhysicalAddress) ((DbDataReader) npgsqlDataReader).GetValue(6);
          accountDb.Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString();
          accountDb.NickColor = int.Parse(((DbDataReader) npgsqlDataReader)["nick_color"].ToString());
          accountDb.Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
          accountDb.Exp = int.Parse(((DbDataReader) npgsqlDataReader)["experience"].ToString());
          accountDb.Gold = int.Parse(((DbDataReader) npgsqlDataReader)["gold"].ToString());
          accountDb.Cash = int.Parse(((DbDataReader) npgsqlDataReader)["cash"].ToString());
          accountDb.CafePC = (CafeEnum) int.Parse(((DbDataReader) npgsqlDataReader)["pc_cafe"].ToString());
          accountDb.Access = (AccessLevel) int.Parse(((DbDataReader) npgsqlDataReader)["access_level"].ToString());
          accountDb.IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString());
          accountDb.ClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
          accountDb.ClanAccess = int.Parse(((DbDataReader) npgsqlDataReader)["clan_access"].ToString());
          accountDb.Effects = (CouponEffects) long.Parse(((DbDataReader) npgsqlDataReader)["coupon_effect"].ToString());
          accountDb.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), accountDb.PlayerId);
          accountDb.LastRankUpDate = uint.Parse(((DbDataReader) npgsqlDataReader)["last_rank_update"].ToString());
          accountDb.BanObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["ban_object_id"].ToString());
          accountDb.Ribbon = int.Parse(((DbDataReader) npgsqlDataReader)["ribbon"].ToString());
          accountDb.Ensign = int.Parse(((DbDataReader) npgsqlDataReader)["ensign"].ToString());
          accountDb.Medal = int.Parse(((DbDataReader) npgsqlDataReader)["medal"].ToString());
          accountDb.MasterMedal = int.Parse(((DbDataReader) npgsqlDataReader)["master_medal"].ToString());
          accountDb.Mission.Mission1 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id1"].ToString());
          accountDb.Mission.Mission2 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id2"].ToString());
          accountDb.Mission.Mission3 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id3"].ToString());
          accountDb.Tags = int.Parse(((DbDataReader) npgsqlDataReader)["tags"].ToString());
          accountDb.InventoryPlus = int.Parse(((DbDataReader) npgsqlDataReader)["inventory_plus"].ToString());
          accountDb.Country = (CountryFlags) int.Parse(((DbDataReader) npgsqlDataReader)["country_flag"].ToString());
          if (AccountManager.AddAccount(accountDb) && accountDb.IsOnline)
            accountDb.SetOnlineStatus(false);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("was a problem loading accounts! " + ex.Message, LoggerType.Error, ex);
    }
    return accountDb;
  }

  public static void GetFriendlyAccounts(PlayerFriends acc)
  {
    if (acc == null)
      return;
    if (acc.Friends.Count == 0)
      return;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        List<string> stringList = new List<string>();
        for (int index = 0; index < acc.Friends.Count; ++index)
        {
          FriendModel friend = acc.Friends[index];
          string parameterName = "@valor" + index.ToString();
          command.Parameters.AddWithValue(parameterName, (object) friend.get_PlayerId());
          stringList.Add(parameterName);
        }
        string str = string.Join(",", stringList.ToArray());
        ((DbCommand) command).CommandText = $"SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in ({str}) ORDER BY player_id";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          FriendModel friend = acc.GetFriend(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()));
          if (friend != null)
          {
            friend.Info.Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString();
            friend.Info.Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
            friend.Info.IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString());
            friend.Info.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), friend.get_PlayerId());
            if (friend.Info.IsOnline && !AccountManager.Accounts.ContainsKey(friend.get_PlayerId()))
            {
              friend.Info.SetOnlineStatus(false);
              friend.Info.Status.ResetData(friend.get_PlayerId());
            }
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("was a problem loading (FriendAccounts)! " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void GetFriendlyAccounts(PlayerFriends valor, bool valor2)
  {
    if (valor == null)
      return;
    if (valor.Friends.Count == 0)
      return;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        List<string> stringList = new List<string>();
        for (int index = 0; index < valor.Friends.Count; ++index)
        {
          FriendModel friend = valor.Friends[index];
          if (friend.State > 0)
            return;
          string parameterName = "@valor" + index.ToString();
          command.Parameters.AddWithValue(parameterName, (object) friend.get_PlayerId());
          stringList.Add(parameterName);
        }
        string str = string.Join(",", stringList.ToArray());
        if (str == "")
          return;
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@on", (object) valor2);
        ((DbCommand) command).CommandText = $"SELECT nickname, player_id, rank, status FROM accounts WHERE player_id in ({str}) AND online=@on ORDER BY player_id";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          FriendModel friend = valor.GetFriend(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()));
          if (friend != null)
          {
            friend.Info.Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString();
            friend.Info.Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
            friend.Info.IsOnline = valor2;
            friend.Info.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), friend.get_PlayerId());
            if (valor2 && !AccountManager.Accounts.ContainsKey(friend.get_PlayerId()))
            {
              friend.Info.SetOnlineStatus(false);
              friend.Info.Status.ResetData(friend.get_PlayerId());
            }
          }
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("was a problem loading (FriendAccounts2)! " + ex.Message, LoggerType.Error, ex);
    }
  }
}
