// Decompiled with JetBrains decompiler
// Type: Server.Match.MatchManager
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Match.Data.Sync;
using Server.Match.Network.Packets;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Match;

public class MatchManager
{
  private readonly string string_0;
  private readonly int int_0;
  public Socket MainSocket;
  public bool ServerIsClosed;

  [CompilerGenerated]
  [SpecialName]
  public static void set_SpamConnections(ConcurrentDictionary<string, int> Data)
  {
    // ISSUE: reference to a compiler-generated field
    MatchXender.concurrentDictionary_0 = Data;
  }

  [CompilerGenerated]
  [SpecialName]
  public static ConcurrentDictionary<IPEndPoint, Socket> get_UdpClients()
  {
    // ISSUE: reference to a compiler-generated field
    return MatchXender.concurrentDictionary_1;
  }

  [CompilerGenerated]
  [SpecialName]
  public static void set_UdpClients([In] ConcurrentDictionary<IPEndPoint, Socket> obj0)
  {
    // ISSUE: reference to a compiler-generated field
    MatchXender.concurrentDictionary_1 = obj0;
  }

  public static bool GetPlugin(string iasyncResult_0, [In] int obj1)
  {
    try
    {
      MatchManager.set_SpamConnections(new ConcurrentDictionary<string, int>());
      MatchManager.set_UdpClients(new ConcurrentDictionary<IPEndPoint, Socket>());
      MatchXender.Sync = new MatchSync(SynchronizeXML.GetServer(obj1).Connection);
      MatchXender.Client = new MatchManager(iasyncResult_0, obj1);
      MatchXender.Sync.Start();
      MatchXender.Client.Start();
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public MatchManager()
  {
  }

  public MatchManager(string value, [In] int obj1)
  {
    this.string_0 = value;
    this.int_0 = obj1;
  }

  public bool Start()
  {
    try
    {
      this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      this.MainSocket.IOControl(-1744830452 /*0x9800000C*/, new byte[1]
      {
        Convert.ToByte(false)
      }, (byte[]) null);
      IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
      this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
      this.MainSocket.DontFragment = false;
      this.MainSocket.Ttl = (short) 128 /*0x80*/;
      this.MainSocket.Bind((EndPoint) localEP);
      CLogger.Print($"Match Serv Address {localEP.Address}:{localEP.Port}", LoggerType.Info, (Exception) null);
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((PROTOCOL_EVENTS_ACTION) this).method_5));
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
      StateObject state = new StateObject()
      {
        WorkSocket = this.MainSocket,
        RemoteEP = (EndPoint) new IPEndPoint(IPAddress.Any, 0)
      };
      this.MainSocket.BeginReceiveFrom(state.UdpBuffer, 0, 8096, SocketFlags.None, ref state.RemoteEP, new AsyncCallback(this.method_1), (object) state);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_1(IAsyncResult value)
  {
    if (!value.IsCompleted)
      CLogger.Print("IAsyncResult is not completed.", LoggerType.Warning, (Exception) null);
    StateObject asyncState = value.AsyncState as StateObject;
    Socket workSocket = asyncState.WorkSocket;
    IPEndPoint remoteEp = asyncState.RemoteEP as IPEndPoint;
    try
    {
      EndPoint endPoint = (EndPoint) new IPEndPoint(IPAddress.Any, 0);
      int from = workSocket.EndReceiveFrom(value, ref endPoint);
      if (from <= 0)
        return;
      byte[] numArray = new byte[from];
      Buffer.BlockCopy((Array) asyncState.UdpBuffer, 0, (Array) numArray, 0, from);
      if (numArray.Length >= 22 && numArray.Length <= 8096)
        this.BeginReceive(new MatchClient(workSocket, endPoint as IPEndPoint), numArray);
      else
        CLogger.Print($"Invalid Buffer Length: {numArray.Length}; IP: {remoteEp}", LoggerType.Hack, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error during EndReceiveCallback: " + ex.Message, LoggerType.Error, ex);
      ((PROTOCOL_BOTS_ACTION) this).method_2(remoteEp);
      ((PROTOCOL_BOTS_ACTION) this).method_3($"{remoteEp.Address}");
    }
    finally
    {
      this.method_0();
    }
  }

  public void BeginReceive(MatchClient Host, byte[] Port)
  {
    try
    {
      if (Host == null)
        CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, (Exception) null);
      else
        Host.BeginReceive(Port, DateTimeUtil.Now());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
