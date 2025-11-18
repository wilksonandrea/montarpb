// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Sync.MatchSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Sync.Server;
using System;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Match.Data.Sync;

public class MatchSync
{
  protected Socket ClientSocket;
  private bool bool_0;

  public static void RemoveHit([In] IList obj0, [In] int obj1) => obj0.RemoveAt(obj1);

  public static void CheckDataFlags([In] ActionModel obj0, PacketModel Hit)
  {
    UdpGameEvent flag = obj0.Flag;
    if (!flag.HasFlag((System.Enum) UdpGameEvent.WeaponSync) || Hit.Opcode == 4 || (flag & (UdpGameEvent.DropWeapon | UdpGameEvent.GetWeaponForClient)) <= (UdpGameEvent) 0)
      return;
    obj0.Flag -= UdpGameEvent.WeaponSync;
  }

  public static int PingTime(
    string Packet,
    byte[] Idx,
    [In] int obj2,
    [In] int obj3,
    [In] bool obj4,
    [In] ref int obj5)
  {
    int num = 0;
    try
    {
      PingOptions options = new PingOptions()
      {
        Ttl = obj2,
        DontFragment = obj4
      };
      using (Ping ping = new Ping())
      {
        PingReply pingReply = ping.Send(Packet, obj3, Idx, options);
        if (pingReply.Status == IPStatus.Success)
          num = Convert.ToInt32(pingReply.RoundtripTime);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    obj5 = (int) MatchSync.smethod_0(num);
    return num;
  }

  private static byte smethod_0([In] int obj0)
  {
    if (obj0 <= 100)
      return 5;
    if (obj0 >= 100 && obj0 <= 200)
      return 4;
    if (obj0 >= 200 && obj0 <= 300)
      return 3;
    if (obj0 >= 300 && obj0 <= 400)
      return 2;
    return obj0 >= 400 && obj0 <= 500 ? (byte) 1 : (byte) 0;
  }

  public static TeamEnum GetSwappedTeam([In] PlayerModel obj0, [In] RoomModel obj1)
  {
    if (obj0 == null || obj1 == null)
      return TeamEnum.TEAM_DRAW;
    TeamEnum swappedTeam = obj0.Team;
    if (obj1.IsTeamSwap)
      swappedTeam = swappedTeam == TeamEnum.FR_TEAM ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM;
    return swappedTeam;
  }

  public MatchSync([In] IPEndPoint obj0)
  {
    this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    this.ClientSocket.Bind((EndPoint) obj0);
    this.ClientSocket.IOControl(-1744830452 /*0x9800000C*/, new byte[1]
    {
      Convert.ToByte(false)
    }, (byte[]) null);
  }

  public bool Start()
  {
    try
    {
      IPEndPoint localEndPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
      CLogger.Print($"Match Sync Address {localEndPoint.Address}:{localEndPoint.Port}", LoggerType.Info, (Exception) null);
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((SendMatchInfo) this).method_4));
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
    if (this.bool_0)
      return;
    try
    {
      StateObject state = new StateObject()
      {
        WorkSocket = this.ClientSocket,
        RemoteEP = (EndPoint) new IPEndPoint(IPAddress.Any, 8000)
      };
      this.ClientSocket.BeginReceiveFrom(state.UdpBuffer, 0, 8096, SocketFlags.None, ref state.RemoteEP, new AsyncCallback(this.method_1), (object) state);
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("MatchSync socket disposed during StartReceive.", LoggerType.Warning, (Exception) null);
      ((SendMatchInfo) this).Close();
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in StartReceive: " + ex.Message, LoggerType.Error, ex);
      ((SendMatchInfo) this).Close();
    }
  }

  private void method_1([In] IAsyncResult obj0)
  {
    // ISSUE: variable of a compiler-generated type
    MatchSync.Class0 class0 = (MatchSync.Class0) new SendMatchInfo();
    // ISSUE: reference to a compiler-generated field
    class0.matchSync_0 = this;
    if (this.bool_0 || MatchXender.Client == null || MatchXender.Client.ServerIsClosed)
      return;
    StateObject asyncState = obj0.AsyncState as StateObject;
    try
    {
      int from = this.ClientSocket.EndReceiveFrom(obj0, ref asyncState.RemoteEP);
      if (from <= 0)
        return;
      // ISSUE: reference to a compiler-generated field
      class0.byte_0 = new byte[from];
      // ISSUE: reference to a compiler-generated field
      Array.Copy((Array) asyncState.UdpBuffer, 0, (Array) class0.byte_0, 0, from);
      ThreadPool.QueueUserWorkItem(new WaitCallback(((SendMatchInfo) class0).method_0));
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("MatchSync socket disposed during ReceiveCallback.", LoggerType.Warning, (Exception) null);
      ((SendMatchInfo) this).Close();
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in ReceiveCallback: " + ex.Message, LoggerType.Error, ex);
    }
    finally
    {
      this.method_0();
    }
  }
}
