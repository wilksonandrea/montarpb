// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.ServerCache
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using System;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class ServerCache
{
  public static void Load(SyncClientPacket C)
  {
    long system = C.ReadQ();
    int num1 = (int) C.ReadC();
    int num2 = (int) C.ReadC();
    int num3 = C.ReadD();
    int num4 = C.ReadD();
    int num5 = C.ReadD();
    Account account = ClanManager.GetAccount(system, true);
    if (account == null || num1 != 0)
      return;
    account.Rank = num2;
    account.Gold = num3;
    account.Cash = num4;
    account.Tags = num5;
  }

  public static void Load(SyncClientPacket int_0)
  {
    int num = (int) int_0.ReadC();
    ServerConfig config = ServerConfigJSON.GetConfig(num);
    if (config == null || config.ConfigId <= 0)
      return;
    AuthXender.Client.Config = config;
    CLogger.Print($"Configuration (Database) Refills; Config: {num}", LoggerType.Command, (Exception) null);
  }
}
