// Decompiled with JetBrains decompiler
// Type: Server.Game.GameManager
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Game;

public class GameManager
{
  private readonly string string_0;
  private readonly int int_0;
  public readonly int ServerId;
  public ServerConfig Config;
  public Socket MainSocket;
  public bool ServerIsClosed;

  public GameManager()
  {
  }

  internal void method_0(object object_0)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      ((GameServerPacket) ((GameClient.Class1) this).gameClientPacket_0).Run();
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      // ISSUE: reference to a compiler-generated field
      ((GameClient.Class1) this).gameClient_0.Close(50, true, false);
    }
  }

  public GameManager(int object_0, [In] string obj1, [In] int obj2)
  {
    this.string_0 = obj1;
    this.int_0 = obj2;
    this.ServerId = object_0;
  }

  public bool Start()
  {
    try
    {
      this.Config = ServerConfigJSON.GetConfig(ConfigLoader.ConfigId);
      this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
      this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
      this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
      this.MainSocket.DontFragment = false;
      this.MainSocket.NoDelay = true;
      this.MainSocket.Bind((EndPoint) localEP);
      this.MainSocket.Listen(ConfigLoader.BackLog);
      CLogger.Print($"Game Serv Address {this.string_0}:{this.int_0}", LoggerType.Info, (Exception) null);
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((GameXender) this).method_3));
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  private void method_0()
  {
    try
    {
      this.MainSocket.BeginAccept(new AsyncCallback(this.method_1), (object) this.MainSocket);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_1(IAsyncResult object_0)
  {
    if (this.ServerIsClosed)
      return;
    Socket asyncState = object_0.AsyncState as Socket;
    Socket object_0_1 = (Socket) null;
    try
    {
      object_0_1 = asyncState.EndAccept(object_0);
    }
    catch (Exception ex)
    {
      CLogger.Print($"Accept Callback Date: {DateTimeUtil.Now()}; Exception: {ex.Message}", LoggerType.Error, (Exception) null);
    }
    this.method_2(object_0_1, asyncState);
  }

  private void method_2(Socket object_0, [In] Socket obj1)
  {
    try
    {
      Thread.Sleep(5);
      this.method_0();
      if (object_0 == null)
        return;
      this.AddSession(new GameClient(this.ServerId, object_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public void AddSession([In] GameClient obj0)
  {
    try
    {
      if (obj0 == null)
      {
        CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, (Exception) null);
      }
      else
      {
        DateTime dateTime = DateTimeUtil.Now();
        for (int index = 1; index < 100000; ++index)
        {
          if (!GameXender.SocketSessions.ContainsKey(index) && GameXender.SocketSessions.TryAdd(index, obj0))
          {
            obj0.SessionDate = dateTime;
            obj0.SessionId = index;
            obj0.SessionSeed = (ushort) new Random(dateTime.Millisecond).Next(index, (int) short.MaxValue);
            obj0.StartSession();
            return;
          }
        }
        CLogger.Print($"Unable to add session list. IPAddress: {obj0.GetIPAddress()}; Date: {dateTime}", LoggerType.Warning, (Exception) null);
        obj0.Close(500, true, false);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public bool RemoveSession([In] GameClient obj0)
  {
    try
    {
      if (obj0 == null || obj0.SessionId == 0)
        return false;
      if (GameXender.SocketSessions.ContainsKey(obj0.SessionId) && GameXender.SocketSessions.TryRemove(obj0.SessionId, out GameClient _))
        return true;
      obj0 = (GameClient) null;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return false;
  }

  public int SendPacketToAllClients(GameServerPacket iasyncResult_0)
  {
    int allClients = 0;
    if (GameXender.SocketSessions.Count == 0)
      return allClients;
    byte[] completeBytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) iasyncResult_0).GetCompleteBytes("GameManager.SendPacketToAllClients");
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      Account player = gameClient.Player;
      if (player != null && player.IsOnline)
      {
        player.SendCompletePacket(completeBytes, iasyncResult_0.GetType().Name);
        ++allClients;
      }
    }
    return allClients;
  }
}
