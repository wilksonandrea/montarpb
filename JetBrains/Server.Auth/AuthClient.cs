// Decompiled with JetBrains decompiler
// Type: Server.Auth.AuthClient
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Client;
using Server.Auth.Data.XML;
using Server.Auth.Network;
using Server.Auth.Network.ClientPacket;
using Server.Auth.Network.ServerPacket;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Auth;

public class AuthClient : IDisposable
{
  public int ServerId;
  public Socket Client;
  public Account Player;
  public DateTime SessionDate;
  public int SessionId;
  public ushort SessionSeed;
  private ushort ushort_0;
  public int SessionShift;
  public int FirstPacketId;
  private bool bool_0;
  private bool bool_1;
  private readonly SafeHandle safeHandle_0 = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
  private readonly ushort[] ushort_1 = new ushort[3]
  {
    (ushort) 1030,
    (ushort) 1099,
    (ushort) 2499
  };

  public abstract void m000001();

  public abstract void m000002();

  public abstract void m000003();

  public abstract void m000004();

  public abstract void m000005();

  public AuthClient([In] int obj0, [In] Socket obj1)
  {
    this.ServerId = obj0;
    this.Client = obj1;
  }

  public void Dispose()
  {
    try
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  protected virtual void Dispose([In] bool obj0)
  {
    try
    {
      if (this.bool_0)
        return;
      this.Player = (Account) null;
      if (this.Client != null)
      {
        this.Client.Dispose();
        this.Client = (Socket) null;
      }
      if (obj0)
        this.safeHandle_0.Dispose();
      this.bool_0 = true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public void StartSession()
  {
    try
    {
      this.ushort_0 = this.SessionSeed;
      this.SessionShift = (this.SessionId + Bitwise.CRYPTO[0]) % 7 + 1;
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthClient.Class0) this).method_8));
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthClient.Class0) this).method_9));
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthClient.Class1) this).method_10));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      this.Close(0, true);
    }
  }

  private void method_0()
  {
    Thread.Sleep(10000);
    if (this.Client == null || this.FirstPacketId != 0)
      return;
    CLogger.Print("Connection destroyed due to no responses. IPAddress: " + this.GetIPAddress(), LoggerType.Hack, (Exception) null);
    this.Close(0, true);
  }

  public void HeartBeatCounter()
  {
    // ISSUE: variable of a compiler-generated type
    AuthClient.Class0 class0 = (AuthClient.Class0) new AuthManager();
    // ISSUE: reference to a compiler-generated field
    class0.authClient_0 = this;
    // ISSUE: reference to a compiler-generated field
    class0.timerState_0 = new TimerState();
    // ISSUE: reference to a compiler-generated field
    class0.timerState_0.StartTimer(TimeSpan.FromMinutes(20.0), new TimerCallback(((AuthManager) class0).method_0));
  }

  public string GetIPAddress()
  {
    try
    {
      return this.Client != null && this.Client.RemoteEndPoint != null ? (this.Client.RemoteEndPoint as IPEndPoint).Address.ToString() : "";
    }
    catch
    {
      return "";
    }
  }

  public IPAddress GetAddress()
  {
    try
    {
      return this.Client != null && this.Client.RemoteEndPoint != null ? (this.Client.RemoteEndPoint as IPEndPoint).Address : (IPAddress) null;
    }
    catch
    {
      return (IPAddress) null;
    }
  }

  private void method_1()
  {
    this.SendPacket((AuthServerPacket) new PROTOCOL_BASE_GAMEGUARD_ACK(this));
  }

  public void SendCompletePacket([In] byte[] obj0, [In] string obj1)
  {
    try
    {
      byte[] bytes = BitConverter.GetBytes((ushort) (obj0.Length + 2));
      byte[] numArray1 = new byte[obj0.Length + bytes.Length];
      Array.Copy((Array) bytes, 0, (Array) numArray1, 0, bytes.Length);
      Array.Copy((Array) obj0, 0, (Array) numArray1, 2, obj0.Length);
      byte[] numArray2 = new byte[numArray1.Length + 5];
      Array.Copy((Array) numArray1, 0, (Array) numArray2, 0, numArray1.Length);
      if (ConfigLoader.DebugMode)
      {
        ushort uint16 = BitConverter.ToUInt16(numArray2, 2);
        Bitwise.ToByteString(numArray2);
        CLogger.Print($"{obj1}; Address: {this.Client.RemoteEndPoint}; Opcode: [{uint16}]", LoggerType.Debug, (Exception) null);
      }
      Bitwise.ProcessPacket(numArray2, 2, 5, this.ushort_1);
      if (this.Client != null && numArray2.Length != 0)
        this.Client.BeginSend(numArray2, 0, numArray2.Length, SocketFlags.None, new AsyncCallback(this.method_2), (object) this.Client);
    }
    catch
    {
      this.Close(0, true);
    }
  }

  public void SendPacket(byte[] int_0, string socket_0)
  {
    try
    {
      if (int_0.Length < 2)
        return;
      this.SendCompletePacket(int_0, socket_0);
    }
    catch
    {
      this.Close(0, true);
    }
  }

  public void SendPacket(AuthServerPacket disposing)
  {
    try
    {
      using (disposing)
      {
        this.SendPacket(((AuthClientPacket) disposing).GetBytes("AuthClient.SendPacket"), disposing.GetType().Name);
        ((AuthClientPacket) disposing).Dispose();
      }
    }
    catch
    {
      this.Close(0, true);
    }
  }

  private void method_2(IAsyncResult OriginalData)
  {
    try
    {
      if (!(OriginalData.AsyncState is Socket asyncState) || !asyncState.Connected)
        return;
      asyncState.EndSend(OriginalData);
    }
    catch
    {
      this.Close(0, true);
    }
  }

  private void method_3()
  {
    try
    {
      StateObject state = new StateObject()
      {
        WorkSocket = this.Client,
        RemoteEP = (EndPoint) new IPEndPoint(IPAddress.Any, 0)
      };
      this.Client.BeginReceive(state.TcpBuffer, 0, 8912, SocketFlags.None, new AsyncCallback(this.method_4), (object) state);
    }
    catch
    {
      this.Close(0, true);
    }
  }

  public void Close([In] int obj0, bool PacketName)
  {
    if (this.bool_1)
      return;
    try
    {
      this.bool_1 = true;
      ((AuthXender) AuthXender.Client).RemoveSession(this);
      Account player = this.Player;
      if (PacketName)
      {
        if (player != null)
        {
          player.SetOnlineStatus(false);
          if (player.Status.ServerId == (byte) 0)
            AccountInfo.RefreshAccount(player, false);
          player.Status.ResetData(player.PlayerId);
          player.SimpleClear();
          player.UpdateCacheInfo();
          this.Player = (Account) null;
        }
        if (this.Client != null)
          this.Client.Close(obj0);
        Thread.Sleep(obj0);
        this.Dispose();
      }
      else if (player != null)
      {
        player.SimpleClear();
        player.UpdateCacheInfo();
        this.Player = (Account) null;
      }
      ChannelCache.RefreshSChannel(this.ServerId);
    }
    catch (Exception ex)
    {
      CLogger.Print("AuthClient.Close: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_4([In] IAsyncResult obj0)
  {
    StateObject asyncState = obj0.AsyncState as StateObject;
    try
    {
      int length1 = asyncState.WorkSocket.EndReceive(obj0);
      if (length1 <= 0)
        return;
      if (length1 > 8912)
      {
        CLogger.Print("Received data exceeds buffer size. IP: " + this.GetIPAddress(), LoggerType.Error, (Exception) null);
        this.Close(0, true);
      }
      else
      {
        byte[] numArray = new byte[length1];
        Array.Copy((Array) asyncState.TcpBuffer, 0, (Array) numArray, 0, length1);
        int length2 = (int) BitConverter.ToUInt16(numArray, 0) & (int) short.MaxValue;
        if (length2 > 0 && length2 <= length1 - 2)
        {
          byte[] destinationArray = new byte[length2];
          Array.Copy((Array) numArray, 2, (Array) destinationArray, 0, destinationArray.Length);
          byte[] FirstLength = Bitwise.Decrypt(destinationArray, this.SessionShift);
          ushort uint16_1 = BitConverter.ToUInt16(FirstLength, 0);
          ushort uint16_2 = BitConverter.ToUInt16(FirstLength, 2);
          this.method_5(uint16_1);
          if (!this.CheckSeed(uint16_2, true))
          {
            this.Close(0, true);
          }
          else
          {
            if (this.bool_1)
              return;
            this.method_7(uint16_1, FirstLength, "REQ");
            this.CheckOut(numArray, length2);
            // ISSUE: reference to a compiler-generated method
            ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthClient.Class1) this).method_11));
          }
        }
        else
        {
          CLogger.Print($"Invalid PacketLength. IP: {this.GetIPAddress()}; Length: {length2}; RawBytes: {length1}", LoggerType.Hack, (Exception) null);
          this.Close(0, true);
        }
      }
    }
    catch
    {
      this.Close(0, true);
    }
  }

  public void CheckOut(byte[] Packet, [In] int obj1)
  {
    int length1 = Packet.Length;
    try
    {
      if (length1 <= obj1 + 3)
        return;
      byte[] numArray = new byte[length1 - obj1 - 3];
      Array.Copy((Array) Packet, obj1 + 3, (Array) numArray, 0, numArray.Length);
      if (numArray.Length == 0)
        return;
      int length2 = (int) BitConverter.ToUInt16(numArray, 0) & (int) short.MaxValue;
      if (length2 > 0 && length2 <= numArray.Length - 2)
      {
        byte[] destinationArray = new byte[length2];
        Array.Copy((Array) numArray, 2, (Array) destinationArray, 0, destinationArray.Length);
        byte[] FirstLength = Bitwise.Decrypt(destinationArray, this.SessionShift);
        ushort uint16 = BitConverter.ToUInt16(FirstLength, 0);
        if (!this.CheckSeed(BitConverter.ToUInt16(FirstLength, 2), false))
        {
          this.Close(0, true);
        }
        else
        {
          this.method_7(uint16, FirstLength, "REQ");
          this.CheckOut(numArray, length2);
        }
      }
      else
      {
        CLogger.Print($"Invalid PacketLength in CheckOut. IP: {this.GetIPAddress()}; Length: {length2}; RawBytes: {numArray.Length}", LoggerType.Hack, (Exception) null);
        this.Close(0, true);
      }
    }
    catch
    {
      this.Close(0, true);
    }
  }

  private void method_5(ushort TimeMS)
  {
    if (this.FirstPacketId != 0)
      return;
    this.FirstPacketId = (int) TimeMS;
    if (TimeMS == (ushort) 1281 || TimeMS == (ushort) 2309)
      return;
    CLogger.Print($"Connection destroyed due to unknown first packet. Opcode: {TimeMS}; IPAddress: {this.GetIPAddress()}", LoggerType.Hack, (Exception) null);
    this.Close(0, true);
  }

  public bool CheckSeed([In] ushort obj0, bool DestroyConnection)
  {
    if ((int) obj0 == (int) this.method_6())
      return true;
    CLogger.Print($"Connection blocked. IP: {this.GetIPAddress()}; Date: {DateTimeUtil.Now()}; SessionId: {this.SessionId}; PacketSeed: {obj0} / NextSessionSeed: {this.ushort_0}; PrimarySeed: {this.SessionSeed}", LoggerType.Hack, (Exception) null);
    if (DestroyConnection)
    {
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthManager) this).method_12));
    }
    return false;
  }

  private ushort method_6()
  {
    this.ushort_0 = (ushort) ((int) this.ushort_0 * 214013 + 2531011 >> 16 /*0x10*/ & (int) short.MaxValue);
    return this.ushort_0;
  }

  private void method_7(ushort BufferTotal, byte[] FirstLength, [In] string obj2)
  {
    // ISSUE: variable of a compiler-generated type
    AuthClient.Class1 class1 = (AuthClient.Class1) new AuthManager();
    // ISSUE: reference to a compiler-generated field
    class1.authClient_0 = this;
    try
    {
      // ISSUE: reference to a compiler-generated field
      class1.authClientPacket_0 = (AuthClientPacket) null;
      switch (BufferTotal)
      {
        case 1057:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_DAILY_RECORD_REQ();
          break;
        case 1281:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_LOGOUT_REQ();
          break;
        case 2307:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_OPTION_SAVE_REQ();
          break;
        case 2309:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new Mission();
          break;
        case 2311:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
          break;
        case 2314:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_LOGIN_REQ();
          break;
        case 2316:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_LOGIN_WAIT_REQ();
          break;
        case 2318:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GET_OPTION_REQ();
          break;
        case 2320:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GET_USER_INFO_REQ();
          break;
        case 2322:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_MATCH_SERVER_IDX_REQ();
          break;
        case 2328:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new ChannelsXML();
          break;
        case 2332:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GET_MAP_INFO_REQ();
          break;
        case 2399:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GET_INVEN_INFO_REQ();
          break;
        case 2414:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
          break;
        case 2459:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GET_OPTION_REQ();
          break;
        case 2489:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_GAMEGUARD_REQ();
          break;
        case 2516:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_MATCH_CLAN_SEASON_REQ();
          break;
        case 7681:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new ChannelsXML();
          break;
        case 7699:
          // ISSUE: reference to a compiler-generated field
          class1.authClientPacket_0 = (AuthClientPacket) new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
          break;
        default:
          CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{BufferTotal}] | {obj2}", FirstLength), LoggerType.Opcode, (Exception) null);
          break;
      }
      // ISSUE: reference to a compiler-generated field
      if (class1.authClientPacket_0 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      using (class1.authClientPacket_0)
      {
        if (ConfigLoader.DebugMode)
        {
          // ISSUE: reference to a compiler-generated field
          CLogger.Print($"{class1.authClientPacket_0.GetType().Name}; Address: {this.Client.RemoteEndPoint}; Opcode: [{BufferTotal}]", LoggerType.Debug, (Exception) null);
        }
        // ISSUE: reference to a compiler-generated field
        ((PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK) class1.authClientPacket_0).Makeme(this, FirstLength);
        ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthManager) class1).method_0));
        // ISSUE: reference to a compiler-generated field
        ((PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK) class1.authClientPacket_0).Dispose();
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
