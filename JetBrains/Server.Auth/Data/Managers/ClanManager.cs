// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Managers.ClanManager
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Managers;

public class ClanManager
{
  public static Account GetAccount([In] long obj0, [In] int obj1)
  {
    if (obj0 == 0L)
      return (Account) null;
    try
    {
      Account account;
      return AccountManager.Accounts.TryGetValue(obj0, out account) ? account : AccountManager.GetAccountDB((object) obj0, (object) null, 1, obj1);
    }
    catch
    {
      return (Account) null;
    }
  }

  public static Account GetAccount(long system, [In] bool obj1)
  {
    if (system == 0L)
      return (Account) null;
    try
    {
      Account account;
      return AccountManager.Accounts.TryGetValue(system, out account) ? account : (obj1 ? (Account) null : AccountManager.GetAccountDB((object) system, (object) null, 1, 6175));
    }
    catch
    {
      return (Account) null;
    }
  }

  public static bool CreateAccount([In] ref Account obj0, string isOnline, [In] string obj2)
  {
    try
    {
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        command.Parameters.AddWithValue("@login", (object) isOnline);
        command.Parameters.AddWithValue("@pass", (object) obj2);
        ((DbCommand) command).CommandText = "INSERT INTO accounts (username, password, country_flag) VALUES (@login, @pass, 0)";
        ((DbCommand) command).ExecuteNonQuery();
        ((DbCommand) command).CommandText = "SELECT * FROM accounts WHERE username=@login";
        NpgsqlDataReader npgsqlDataReader = command.ExecuteReader(CommandBehavior.Default);
        Account account = new Account();
        while (((DbDataReader) npgsqlDataReader).Read())
        {
          account.Username = ((DbDataReader) npgsqlDataReader)["username"].ToString();
          account.Password = ((DbDataReader) npgsqlDataReader)["password"].ToString();
          account.SetPlayerId(long.Parse(((DbDataReader) npgsqlDataReader)["player_id"].ToString()), 95);
          account.Email = ((DbDataReader) npgsqlDataReader)["email"].ToString();
          account.Age = int.Parse(((DbDataReader) npgsqlDataReader)["age"].ToString());
          account.MacAddress = (PhysicalAddress) ((DbDataReader) npgsqlDataReader).GetValue(6);
          account.Nickname = ((DbDataReader) npgsqlDataReader)["nickname"].ToString();
          account.NickColor = int.Parse(((DbDataReader) npgsqlDataReader)["nick_color"].ToString());
          account.Rank = int.Parse(((DbDataReader) npgsqlDataReader)["rank"].ToString());
          account.Exp = int.Parse(((DbDataReader) npgsqlDataReader)["experience"].ToString());
          account.Gold = int.Parse(((DbDataReader) npgsqlDataReader)["gold"].ToString());
          account.Cash = int.Parse(((DbDataReader) npgsqlDataReader)["cash"].ToString());
          account.CafePC = (CafeEnum) int.Parse(((DbDataReader) npgsqlDataReader)["pc_cafe"].ToString());
          account.Access = (AccessLevel) int.Parse(((DbDataReader) npgsqlDataReader)["access_level"].ToString());
          account.IsOnline = bool.Parse(((DbDataReader) npgsqlDataReader)["online"].ToString());
          account.ClanId = int.Parse(((DbDataReader) npgsqlDataReader)["clan_id"].ToString());
          account.ClanAccess = int.Parse(((DbDataReader) npgsqlDataReader)["clan_access"].ToString());
          account.Effects = (CouponEffects) long.Parse(((DbDataReader) npgsqlDataReader)["coupon_effect"].ToString());
          account.Status.SetData(uint.Parse(((DbDataReader) npgsqlDataReader)["status"].ToString()), account.PlayerId);
          account.LastRankUpDate = uint.Parse(((DbDataReader) npgsqlDataReader)["last_rank_update"].ToString());
          account.BanObjectId = long.Parse(((DbDataReader) npgsqlDataReader)["ban_object_id"].ToString());
          account.Ribbon = int.Parse(((DbDataReader) npgsqlDataReader)["ribbon"].ToString());
          account.Ensign = int.Parse(((DbDataReader) npgsqlDataReader)["ensign"].ToString());
          account.Medal = int.Parse(((DbDataReader) npgsqlDataReader)["medal"].ToString());
          account.MasterMedal = int.Parse(((DbDataReader) npgsqlDataReader)["master_medal"].ToString());
          account.Mission.Mission1 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id1"].ToString());
          account.Mission.Mission2 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id2"].ToString());
          account.Mission.Mission3 = int.Parse(((DbDataReader) npgsqlDataReader)["mission_id3"].ToString());
          account.Tags = int.Parse(((DbDataReader) npgsqlDataReader)["tags"].ToString());
          account.InventoryPlus = int.Parse(((DbDataReader) npgsqlDataReader)["inventory_plus"].ToString());
          account.Country = (CountryFlags) int.Parse(((DbDataReader) npgsqlDataReader)["country_flag"].ToString());
        }
        obj0 = account;
        AccountManager.AddAccount(account);
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
        return true;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("[AccountManager.CreateAccount] " + ex.Message, LoggerType.Error, ex);
      obj0 = (Account) null;
      return false;
    }
  }
}
