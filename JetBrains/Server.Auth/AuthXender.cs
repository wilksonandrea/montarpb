// Decompiled with JetBrains decompiler
// Type: Server.Auth.AuthXender
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync;
using Server.Auth.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth;

public class AuthXender
{
  public void AddSession(AuthClient int_1)
  {
    try
    {
      if (int_1 == null)
      {
        CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, (Exception) null);
      }
      else
      {
        DateTime dateTime = DateTimeUtil.Now();
        for (int index = 1; index < 100000; ++index)
        {
          if (!AuthXender.SocketSessions.ContainsKey(index) && AuthXender.SocketSessions.TryAdd(index, int_1))
          {
            int_1.SessionDate = dateTime;
            int_1.SessionId = index;
            int_1.SessionSeed = (ushort) new Random(dateTime.Millisecond).Next(index, (int) short.MaxValue);
            int_1.StartSession();
            return;
          }
        }
        CLogger.Print($"Unable to add session list. IPAddress: {int_1.GetIPAddress()}; Date: {dateTime}", LoggerType.Warning, (Exception) null);
        int_1.Close(500, true);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public bool RemoveSession([In] AuthClient obj0)
  {
    try
    {
      if (obj0 == null || obj0.SessionId == 0)
        return false;
      if (AuthXender.SocketSessions.ContainsKey(obj0.SessionId) && AuthXender.SocketSessions.TryRemove(obj0.SessionId, out AuthClient _))
        return true;
      obj0 = (AuthClient) null;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return false;
  }

  public int SendPacketToAllClients([In] AuthServerPacket obj0)
  {
    int allClients = 0;
    if (AuthXender.SocketSessions.Count == 0)
      return allClients;
    byte[] completeBytes = ((AuthClientPacket) obj0).GetCompleteBytes("AuthManager.SendPacketToAllClients");
    foreach (AuthClient authClient in (IEnumerable<AuthClient>) AuthXender.SocketSessions.Values)
    {
      Account player = authClient.Player;
      if (player != null && player.IsOnline)
      {
        player.SendCompletePacket(completeBytes, obj0.GetType().Name);
        ++allClients;
      }
    }
    return allClients;
  }

  public Account SearchActiveClient(long iasyncResult_0)
  {
    if (AuthXender.SocketSessions.Count == 0)
      return (Account) null;
    foreach (AuthClient authClient in (IEnumerable<AuthClient>) AuthXender.SocketSessions.Values)
    {
      Account player = authClient.Player;
      if (player != null && player.PlayerId == iasyncResult_0)
        return player;
    }
    return (Account) null;
  }

  public static AuthSync Sync { get; set; }

  public static AuthManager Client { get; set; }

  public static ConcurrentDictionary<int, AuthClient> SocketSessions { get; [CompilerGenerated, SpecialName] set; }

  public static ConcurrentDictionary<string, int> SocketConnections { [CompilerGenerated, SpecialName] get; [CompilerGenerated, SpecialName] set; }
}
