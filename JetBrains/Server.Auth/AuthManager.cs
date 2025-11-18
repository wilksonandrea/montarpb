// Decompiled with JetBrains decompiler
// Type: Server.Auth.AuthManager
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Auth.Network.ServerPacket;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Auth;

public class AuthManager
{
  private readonly string string_0;
  private readonly int int_0;
  public readonly int ServerId;
  public ServerConfig Config;
  public Socket MainSocket;
  public bool ServerIsClosed;

  public AuthManager()
  {
  }

  internal void method_0(object object_0)
  {
    // ISSUE: reference to a compiler-generated field
    if (!((AuthClient.Class0) this).authClient_0.bool_1)
    {
      // ISSUE: reference to a compiler-generated field
      CLogger.Print("Connection destroyed due to no response for 20 minutes (HeartBeat). IPAddress: " + ((AuthClient.Class0) this).authClient_0.GetIPAddress(), LoggerType.Hack, (Exception) null);
      // ISSUE: reference to a compiler-generated field
      ((AuthClient.Class0) this).authClient_0.Close(0, true);
    }
    lock (object_0)
    {
      // ISSUE: reference to a compiler-generated field
      ((AuthClient.Class0) this).timerState_0.StopJob();
    }
  }

  public AuthManager()
  {
  }

  internal void method_0(object object_0)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      ((PROTOCOL_CS_MEDAL_INFO_ACK) ((AuthClient.Class1) this).authClientPacket_0).Run();
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      // ISSUE: reference to a compiler-generated field
      ((AuthClient.Class1) this).authClient_0.Close(50, true);
    }
  }

  public AuthManager(int object_0, [In] string obj1, [In] int obj2)
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
      CLogger.Print($"Auth Serv Address {this.string_0}:{this.int_0}", LoggerType.Info, (Exception) null);
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthXender) this).method_3));
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
    this.method_2(object_0_1);
  }

  private void method_2(Socket object_0)
  {
    try
    {
      Thread.Sleep(5);
      this.method_0();
      if (object_0 == null)
        return;
      ((AuthXender) this).AddSession(new AuthClient(this.ServerId, object_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
