// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Managers.AccountManager
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Managers;

public static class AccountManager
{
  public static SortedList<long, Account> Accounts;

  public AccountManager()
  {
  }

  internal void method_0(object object_0)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!((RoomModel.Class12) this).slotModel_0.FirstInactivityOff && ((RoomModel.Class12) this).slotModel_0.State < SlotState.BATTLE && ((RoomModel.Class12) this).slotModel_0.IsPlaying == 0)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((RoomModel.Class12) this).roomModel_0.method_3(((RoomModel.Class12) this).eventErrorEnum_0, ((RoomModel.Class12) this).account_0, ((RoomModel.Class12) this).slotModel_0);
    }
    lock (object_0)
    {
      // ISSUE: reference to a compiler-generated field
      if (((RoomModel.Class12) this).slotModel_0 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      ((RoomModel.Class12) this).slotModel_0.StopTiming();
    }
  }

  public AccountManager()
  {
  }

  internal bool method_0(int object_0) => object_0 == ((RoomModel.Class13) this).int_0;

  internal bool method_1(int slotModel_0) => slotModel_0 == ((RoomModel.Class13) this).int_0;

  public static void AddAccount(Account slotModel_0)
  {
    lock (AccountManager.Accounts)
    {
      if (AccountManager.Accounts.ContainsKey(slotModel_0.PlayerId))
        return;
      AccountManager.Accounts.Add(slotModel_0.PlayerId, slotModel_0);
    }
  }

  public static Account GetAccountDB(object slotModel_0, [In] int obj1, [In] int obj2)
  {
    if (obj1 == 2 && (long) slotModel_0 == 0L || (obj1 == 0 || obj1 == 1) && (string) slotModel_0 == "")
      return (Account) null;
    Account slotModel_0_1 = (Account) null;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@value", slotModel_0);
        NpgsqlCommand npgsqlCommand = command;
        string str1;
        switch (obj1)
        {
          case 0:
            str1 = "username";
            break;
          case 1:
            str1 = "nickname";
            break;
          default:
            str1 = "player_id";
            break;
        }
        string str2 = $"SELECT * FROM accounts WHERE {str1}=@value";
        ((DbCommand) npgsqlCommand).CommandText = str2;
        ((DbCommand) command).CommandType = CommandType.Text;
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          slotModel_0_1 = new Account()
          {
            Username = ((DbDataReader) npgsqlDataReader)["username"].ToString(),
            Password = ((DbDataReader) npgsqlDataReader)["password"].ToString()
          };
          slotModel_0_1.SetPlayerId(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()), obj2);
          slotModel_0_1.Email = ((DbDataReader) npgsqlDataReader)["email"].ToString();
          slotModel_0_1.Age = int.Parse(((DbDataReader) npgsqlDataReader)["age"].ToString());
          slotModel_0_1.SetPublicIP(((DbDataReader) npgsqlDataReader).GetString(5));
          slotModel_0_1.Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString();
          slotModel_0_1.NickColor = int.Parse(((DbDataReader) npgsqlDataReader)["nick_color"].ToString());
          slotModel_0_1.Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
          slotModel_0_1.Exp = int.Parse(((DbDataReader) npgsqlDataReader)["experience"].ToString());
          slotModel_0_1.Gold = int.Parse(((DbDataReader) npgsqlDataReader)["gold"].ToString());
          slotModel_0_1.Cash = int.Parse(((DbDataReader) npgsqlDataReader)["cash"].ToString());
          slotModel_0_1.CafePC = (CafeEnum) int.Parse(((DbDataReader) npgsqlDataReader)["pc_cafe"].ToString());
          slotModel_0_1.Access = (AccessLevel) int.Parse(((DbDataReader) npgsqlDataReader)["access_level"].ToString());
          slotModel_0_1.IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString());
          slotModel_0_1.ClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
          slotModel_0_1.ClanAccess = int.Parse(((DbDataReader) npgsqlDataReader)["clan_access"].ToString());
          slotModel_0_1.Effects = (CouponEffects) long.Parse(((DbDataReader) npgsqlDataReader)["coupon_effect"].ToString());
          slotModel_0_1.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), slotModel_0_1.PlayerId);
          slotModel_0_1.LastRankUpDate = uint.Parse(((DbDataReader) npgsqlDataReader)["last_rank_update"].ToString());
          slotModel_0_1.BanObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["ban_object_id"].ToString());
          slotModel_0_1.Ribbon = int.Parse(((DbDataReader) npgsqlDataReader)["ribbon"].ToString());
          slotModel_0_1.Ensign = int.Parse(((DbDataReader) npgsqlDataReader)["ensign"].ToString());
          slotModel_0_1.Medal = int.Parse(((DbDataReader) npgsqlDataReader)["medal"].ToString());
          slotModel_0_1.MasterMedal = int.Parse(((DbDataReader) npgsqlDataReader)["master_medal"].ToString());
          slotModel_0_1.Mission.Mission1 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id1"].ToString());
          slotModel_0_1.Mission.Mission2 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id2"].ToString());
          slotModel_0_1.Mission.Mission3 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id3"].ToString());
          slotModel_0_1.Tags = int.Parse(((DbDataReader) npgsqlDataReader)["tags"].ToString());
          slotModel_0_1.InventoryPlus = int.Parse(((DbDataReader) npgsqlDataReader)["inventory_plus"].ToString());
          slotModel_0_1.Country = (CountryFlags) int.Parse(((DbDataReader) npgsqlDataReader)["country_flag"].ToString());
          AccountManager.AddAccount(slotModel_0_1);
        }
        ((Component) command).Dispose();
        ((DbDataReader) npgsqlDataReader).Close();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("was a problem loading accounts! " + ex.Message, LoggerType.Error, ex);
    }
    return slotModel_0_1;
  }

  public static void GetFriendlyAccounts(PlayerFriends int_1)
  {
    if (int_1 == null)
      return;
    if (int_1.Friends.Count == 0)
      return;
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        List<string> stringList = new List<string>();
        for (int index = 0; index < int_1.Friends.Count; ++index)
        {
          FriendModel friend = int_1.Friends[index];
          string parameterName = "@valor" + index.ToString();
          command.Parameters.AddWithValue(parameterName, (object) friend.get_PlayerId());
          stringList.Add(parameterName);
        }
        string str = string.Join(",", stringList.ToArray());
        ((DbCommand) command).CommandText = $"SELECT nickname, player_id, rank, online, status FROM accounts WHERE player_id in ({str}) ORDER BY player_id";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          FriendModel friend = int_1.GetFriend(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()));
          if (friend != null)
          {
            friend.Info.Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString();
            friend.Info.Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
            friend.Info.IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString());
            friend.Info.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), friend.get_PlayerId());
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
      CLogger.Print("was a problem loading (FriendlyAccounts); " + ex.Message, LoggerType.Error, ex);
    }
  }
}
