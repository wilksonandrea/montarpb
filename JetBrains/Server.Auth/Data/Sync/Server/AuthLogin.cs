// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Server.AuthLogin
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Data.Models;
using System;

#nullable disable
namespace Server.Auth.Data.Sync.Server;

public class AuthLogin
{
  internal void method_0(object byte_0)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      ((AuthSync.Class3) ((AuthSync.Class3) this).authSync_0).method_2(((AuthSync.Class3) this).byte_0);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void AddMember(Account Data, Account Address)
  {
    lock (Data.ClanPlayers)
      Data.ClanPlayers.Add(Address);
  }
}
