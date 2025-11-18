// Decompiled with JetBrains decompiler
// Type: Server.Game.GameXender
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game;

public class GameXender
{
  public Account SearchActiveClient(long socket_0)
  {
    if (GameXender.SocketSessions.Count == 0)
      return (Account) null;
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      Account player = gameClient.Player;
      if (player != null && player.PlayerId == socket_0)
        return player;
    }
    return (Account) null;
  }

  public Account SearchActiveClient([In] uint obj0)
  {
    if (GameXender.SocketSessions.Count == 0)
      return (Account) null;
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      if (gameClient.Player != null && (long) gameClient.SessionId == (long) obj0)
        return gameClient.Player;
    }
    return (Account) null;
  }

  public int KickActiveClient(double Client)
  {
    int num = 0;
    DateTime dateTime = DateTimeUtil.Now();
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      Account player = gameClient.Player;
      if (player != null && player.Room == null && player.ChannelId > -1 && !player.IsGM() && (dateTime - player.LastLobbyEnter).TotalHours >= Client)
      {
        ++num;
        player.Close(5000, false);
      }
    }
    return num;
  }

  public int KickCountActiveClient(double Client)
  {
    int num = 0;
    DateTime dateTime = DateTimeUtil.Now();
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      Account player = gameClient.Player;
      if (player != null && player.Room == null && player.ChannelId > -1 && !player.IsGM() && (dateTime - player.LastLobbyEnter).TotalHours >= Client)
        ++num;
    }
    return num;
  }

  public static GameSync Sync { get; set; }

  public static GameManager Client { get; set; }

  public static ConcurrentDictionary<int, GameClient> SocketSessions { get; set; }

  public static ConcurrentDictionary<string, int> SocketConnections { get; [CompilerGenerated, SpecialName] set; }
}
