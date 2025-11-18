// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Managers.CommandManager
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Managers;

public static class CommandManager
{
  private static readonly Dictionary<string, ICommand> dictionary_0;

  public static bool RemoveClan(ClanModel Packet)
  {
    lock (ClanManager.Clans)
      return ClanManager.Clans.Remove(Packet);
  }

  public static void AddClan([In] ClanModel obj0)
  {
    lock (ClanManager.Clans)
      ClanManager.Clans.Add(obj0);
  }

  public static bool IsClanNameExist([In] string obj0)
  {
    if (string.IsNullOrEmpty(obj0))
      return true;
    try
    {
      int num = 0;
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@name", (object) obj0);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM system_clan WHERE name=@name";
        num = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return num > 0;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return true;
    }
  }

  public static bool IsClanLogoExist([In] uint obj0)
  {
    try
    {
      int num = 0;
      using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
      {
        NpgsqlCommand command = npgsqlConnection.CreateCommand();
        ((DbConnection) npgsqlConnection).Open();
        ((DbCommand) command).CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@logo", (object) (long) obj0);
        ((DbCommand) command).CommandText = "SELECT COUNT(*) FROM system_clan WHERE logo=@logo";
        num = Convert.ToInt32(((DbCommand) command).ExecuteScalar());
        ((Component) command).Dispose();
        ((Component) npgsqlConnection).Dispose();
        ((DbConnection) npgsqlConnection).Close();
      }
      return num > 0;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return true;
    }
  }

  static CommandManager() => ClanManager.Clans = new List<ClanModel>();
}
