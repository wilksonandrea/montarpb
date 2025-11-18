// Decompiled with JetBrains decompiler
// Type: Server.Game.GameClient
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ClientPacket;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Game;

public class GameClient : IDisposable
{
  public int ServerId;
  public long PlayerId;
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
  private readonly SafeHandle safeHandle_0;
  private readonly ushort[] ushort_1;

  [SpecialName]
  public T1 get_inx() => ((Class0<T0, T1>) this).gparam_1;

  [DebuggerHidden]
  public GameClient([In] T0 obj0, [In] T1 obj1)
  {
    // ISSUE: reference to a compiler-generated field
    ((Class0<T0, T1>) this).gparam_0 = obj0;
    // ISSUE: reference to a compiler-generated field
    ((Class0<T0, T1>) this).gparam_1 = obj1;
  }

  [DebuggerHidden]
  public virtual bool System\u002EObject\u002EEquals([In] object obj0)
  {
    // ISSUE: variable of a compiler-generated type
    Class0<T0, T1> class0 = obj0 as Class0<T0, T1>;
    if (this == class0)
      return true;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return class0 != null && EqualityComparer<T0>.Default.Equals(((Class0<T0, T1>) this).gparam_0, class0.gparam_0) && EqualityComparer<T1>.Default.Equals(((Class0<T0, T1>) this).gparam_1, class0.gparam_1);
  }

  [DebuggerHidden]
  public virtual int System\u002EObject\u002EGetHashCode()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (EqualityComparer<T0>.Default.GetHashCode(((Class0<T0, T1>) this).gparam_0) - 1959725626) * -1521134295 + EqualityComparer<T1>.Default.GetHashCode(((Class0<T0, T1>) this).gparam_1);
  }

  [DebuggerHidden]
  public virtual string System\u002EObject\u002EToString()
  {
    object[] objArray = new object[2];
    // ISSUE: reference to a compiler-generated field
    T0 gparam0 = ((Class0<T0, T1>) this).gparam_0;
    ref T0 local1 = ref gparam0;
    objArray[0] = (object) ((object) local1 != null ? local1.ToString() : (string) null);
    // ISSUE: reference to a compiler-generated field
    T1 gparam1 = ((Class0<T0, T1>) this).gparam_1;
    ref T1 local2 = ref gparam1;
    objArray[1] = (object) ((object) local2 != null ? local2.ToString() : (string) null);
    return string.Format((IFormatProvider) null, "{{ item = {0}, inx = {1} }}", objArray);
  }

  public GameClient([In] int obj0, [In] Socket obj1)
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

  protected virtual void Dispose(bool gparam_2)
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
      this.PlayerId = 0L;
      if (gparam_2)
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
      ThreadPool.QueueUserWorkItem(new WaitCallback(((GameClient.Class1) this).method_8));
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((GameClient.Class1) this).method_9));
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((GameManager) this).method_10));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      this.Close(0, true, false);
    }
  }

  private void method_0()
  {
    Thread.Sleep(10000);
    if (this.Client == null || this.FirstPacketId != 0)
      return;
    CLogger.Print("Connection destroyed due to no responses. IPAddress: " + this.GetIPAddress(), LoggerType.Hack, (Exception) null);
    this.Close(0, true, false);
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
    this.SendPacket((GameServerPacket) new PROTOCOL_BASE_DAILY_RECORD_ACK(this));
  }

  public void SendCompletePacket([In] byte[] obj0, string gparam_3)
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
        CLogger.Print($"{gparam_3}; Address: {this.Client.RemoteEndPoint}; Opcode: [{uint16}]", LoggerType.Debug, (Exception) null);
      }
      Bitwise.ProcessPacket(numArray2, 2, 5, this.ushort_1);
      if (this.Client != null && numArray2.Length != 0)
        this.Client.BeginSend(numArray2, 0, numArray2.Length, SocketFlags.None, new AsyncCallback(this.method_2), (object) this.Client);
    }
    catch
    {
      this.Close(0, true, false);
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
      this.Close(0, true, false);
    }
  }

  public void SendPacket(GameServerPacket disposing)
  {
    try
    {
      using (disposing)
      {
        this.SendPacket(((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) disposing).GetBytes("GameClient.SendPacket"), disposing.GetType().Name);
        ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) disposing).Dispose();
      }
    }
    catch
    {
      this.Close(0, true, false);
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
      this.Close(0, true, false);
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
      this.Close(0, true, false);
    }
  }

  private void method_4([In] IAsyncResult obj0)
  {
    StateObject asyncState = obj0.AsyncState as StateObject;
    try
    {
      int length = asyncState.WorkSocket.EndReceive(obj0);
      if (length <= 0)
        return;
      if (length > 8912)
      {
        CLogger.Print("Received data exceeds buffer size. IP: " + this.GetIPAddress(), LoggerType.Error, (Exception) null);
        this.Close(0, true, false);
      }
      else
      {
        byte[] numArray = new byte[length];
        Array.Copy((Array) asyncState.TcpBuffer, 0, (Array) numArray, 0, length);
        int PacketName = (int) BitConverter.ToUInt16(numArray, 0) & (int) short.MaxValue;
        if (PacketName > 0 && PacketName <= length - 2)
        {
          byte[] destinationArray = new byte[PacketName];
          Array.Copy((Array) numArray, 2, (Array) destinationArray, 0, destinationArray.Length);
          byte[] DestroyConnection = Bitwise.Decrypt(destinationArray, this.SessionShift);
          ushort uint16_1 = BitConverter.ToUInt16(DestroyConnection, 0);
          ushort uint16_2 = BitConverter.ToUInt16(DestroyConnection, 2);
          this.method_5(uint16_1);
          if (!this.CheckSeed(uint16_2, true))
          {
            this.Close(0, true, false);
          }
          else
          {
            if (this.bool_1)
              return;
            this.method_7(uint16_1, DestroyConnection, "REQ");
            this.CheckOut(numArray, PacketName);
            // ISSUE: reference to a compiler-generated method
            ThreadPool.QueueUserWorkItem(new WaitCallback(((GameManager) this).method_11));
          }
        }
        else
        {
          CLogger.Print($"Invalid PacketLength. IP: {this.GetIPAddress()}; Length: {PacketName}; RawBytes: {length}", LoggerType.Hack, (Exception) null);
          this.Close(0, true, false);
        }
      }
    }
    catch
    {
      this.Close(0, true, false);
    }
  }

  public void CheckOut(byte[] OriginalData, int PacketName)
  {
    int length = OriginalData.Length;
    try
    {
      if (length <= PacketName + 3)
        return;
      byte[] numArray = new byte[length - PacketName - 3];
      Array.Copy((Array) OriginalData, PacketName + 3, (Array) numArray, 0, numArray.Length);
      if (numArray.Length == 0)
        return;
      int PacketName1 = (int) BitConverter.ToUInt16(numArray, 0) & (int) short.MaxValue;
      if (PacketName1 > 0 && PacketName1 <= numArray.Length - 2)
      {
        byte[] destinationArray = new byte[PacketName1];
        Array.Copy((Array) numArray, 2, (Array) destinationArray, 0, destinationArray.Length);
        byte[] DestroyConnection = Bitwise.Decrypt(destinationArray, this.SessionShift);
        ushort uint16 = BitConverter.ToUInt16(DestroyConnection, 0);
        if (!this.CheckSeed(BitConverter.ToUInt16(DestroyConnection, 2), false))
        {
          this.Close(0, true, false);
        }
        else
        {
          this.method_7(uint16, DestroyConnection, "REQ");
          this.CheckOut(numArray, PacketName1);
        }
      }
      else
      {
        CLogger.Print($"Invalid PacketLength in CheckOut. IP: {this.GetIPAddress()}; Length: {PacketName1}; RawBytes: {numArray.Length}", LoggerType.Hack, (Exception) null);
        this.Close(0, true, false);
      }
    }
    catch
    {
      this.Close(0, true, false);
    }
  }

  public void Close(int Packet, [In] bool obj1, [In] bool obj2)
  {
    if (this.bool_1)
      return;
    try
    {
      this.bool_1 = true;
      GameXender.Client.RemoveSession(this);
      Account player = this.Player;
      if (obj1)
      {
        if (this.PlayerId > 0L && player != null)
        {
          player.SetOnlineStatus(false);
          player.Room?.RemovePlayer(player, false, obj2 ? 1 : 0);
          ((RoomModel) player.Match)?.RemovePlayer(player);
          ((MatchModel) player.GetChannel())?.RemovePlayer(player);
          player.Status.ResetData(this.PlayerId);
          AllUtils.SyncPlayerToFriends(player, false);
          AllUtils.SyncPlayerToClanMembers(player);
          player.SimpleClear();
          player.UpdateCacheInfo();
          this.Player = (Account) null;
        }
        this.PlayerId = 0L;
        if (this.Client != null)
          this.Client.Close(Packet);
        Thread.Sleep(Packet);
        this.Dispose();
      }
      else if (player != null)
      {
        player.SimpleClear();
        player.UpdateCacheInfo();
        this.Player = (Account) null;
      }
      AuthLogin.RefreshSChannel(this.ServerId);
    }
    catch (Exception ex)
    {
      CLogger.Print("GameClient.Close: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_5(ushort BufferTotal)
  {
    if (this.FirstPacketId != 0)
      return;
    this.FirstPacketId = (int) BufferTotal;
    if (BufferTotal == (ushort) 2330 || BufferTotal == (ushort) 2309)
      return;
    CLogger.Print($"Connection destroyed due to unknown first packet. Opcode: {BufferTotal}; IPAddress: {this.GetIPAddress()}", LoggerType.Hack, (Exception) null);
    this.Close(0, true, false);
  }

  public bool CheckSeed([In] ushort obj0, bool FirstLength)
  {
    if ((int) obj0 == (int) this.method_6())
      return true;
    CLogger.Print($"Connection blocked. IP: {this.GetIPAddress()}; Date: {DateTimeUtil.Now()}; SessionId: {this.SessionId}; PacketSeed: {obj0} / NextSessionSeed: {this.ushort_0}; PrimarySeed: {this.SessionSeed}", LoggerType.Hack, (Exception) null);
    if (FirstLength)
    {
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((GameManager) this).method_12));
    }
    return false;
  }

  private ushort method_6()
  {
    this.ushort_0 = (ushort) ((int) this.ushort_0 * 214013 + 2531011 >> 16 /*0x10*/ & (int) short.MaxValue);
    return this.ushort_0;
  }

  private void method_7([In] ushort obj0, byte[] DestroyConnection, string Kicked = false)
  {
    // ISSUE: variable of a compiler-generated type
    GameClient.Class1 class1 = (GameClient.Class1) new GameManager();
    // ISSUE: reference to a compiler-generated field
    class1.gameClient_0 = this;
    try
    {
      // ISSUE: reference to a compiler-generated field
      class1.gameClientPacket_0 = (GameClientPacket) null;
      switch (obj0)
      {
        case 769:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CLOSE_CLAN_REQ();
          goto case 2392;
        case 771:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_COMMISSION_MASTER_REQ();
          goto case 2392;
        case 800:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_INVITE_REQ();
          goto case 2392;
        case 802:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_NOTE_REQ();
          goto case 2392;
        case 804:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_PAGE_CHATTING_REQ();
          goto case 2392;
        case 806:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_DEPORTATION_REQ();
          goto case 2392;
        case 808:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_COMMISSION_REGULAR_REQ();
          goto case 2392;
        case 810:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ();
          goto case 2392;
        case 812:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_MEMBER_LIST_REQ();
          goto case 2392;
        case 814:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CHECK_DUPLICATE_REQ();
          goto case 2392;
        case 816:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_REQUEST_LIST_REQ();
          goto case 2392;
        case 818:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_SECESSION_CLAN_REQ();
          goto case 2392;
        case 820:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_ROOM_INVITED_REQ();
          goto case 2392;
        case 822:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CHATTING_REQ();
          goto case 2392;
        case 825:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_DETAIL_INFO_REQ();
          goto case 2392;
        case 828:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GMCHAT_START_CHAT_REQ();
          goto case 2392;
        case 830:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_INVITE_ACCEPT_REQ();
          goto case 2392;
        case 833:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_COMMISSION_STAFF_REQ();
          goto case 2392;
        case 836:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CREATE_CLAN_REQ();
          goto case 2392;
        case 839:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ();
          goto case 2392;
        case 854:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ();
          goto case 2392;
        case 856:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ();
          goto case 2392;
        case 858:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_REQUEST_INFO_REQ();
          goto case 2392;
        case 860:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_REPLACE_NOTICE_REQ();
          goto case 2392;
        case 868:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_REQUEST_CONTEXT_REQ();
          goto case 2392;
        case 877:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GMCHAT_END_CHAT_REQ();
          goto case 2392;
        case 886:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_REPLACE_MANAGEMENT_REQ();
          goto case 2392;
        case 888:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_MEMBER_CONTEXT_REQ();
          goto case 2392;
        case 890:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_INVITE_REQ();
          goto case 2392;
        case 892:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_REPLACE_INTRO_REQ();
          goto case 2392;
        case 914:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_DENIAL_REQUEST_REQ();
          goto case 2392;
        case 916:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CHECK_MARK_REQ();
          goto case 2392;
        case 997:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CLIENT_LEAVE_REQ();
          goto case 2392;
        case 999:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ();
          goto case 2392;
        case 1025:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SHOP_LEAVE_REQ();
          goto case 2392;
        case 1027:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new ChannelsXML();
          goto case 2392;
        case 1029:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SHOP_REPAIR_REQ();
          goto case 2392;
        case 1041:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ();
          goto case 2392;
        case 1043:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ();
          goto case 2392;
        case 1045:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SEND_WHISPER_REQ();
          goto case 2392;
        case 1047:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ();
          goto case 2392;
        case 1049:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
          goto case 2392;
        case 1053:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ();
          goto case 2392;
        case 1055:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ();
          goto case 2392;
        case 1057:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_EXTEND_REQ();
          goto case 2392;
        case 1061:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_ATTENDANCE_REQ();
          goto case 2392;
        case 1075:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_RECV_WHISPER_REQ();
          goto case 2392;
        case 1076:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new ChannelsXML();
          goto case 2392;
        case 1084:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ();
          goto case 2392;
        case 1087:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ();
          goto case 2392;
        case 1811:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
          goto case 2392;
        case 1816:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQ();
          goto case 2392;
        case 1818:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_FRIEND_ACCEPT_REQ();
          goto case 2392;
        case 1820:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ();
          goto case 2392;
        case 1826:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ();
          goto case 2392;
        case 1828:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ();
          goto case 2392;
        case 1921:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CHANGE_PASSWD_REQ();
          goto case 2392;
        case 1926:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ();
          goto case 2392;
        case 1928:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_MESSENGER_NOTE_SEND_REQ();
          goto case 2392;
        case 1930:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_REQ();
          goto case 2392;
        case 2307:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_OPTION_SAVE_REQ();
          goto case 2392;
        case 2309:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GUIDE_COMPLETE_REQ();
          goto case 2392;
        case 2312:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
          goto case 2392;
        case 2322:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ();
          goto case 2392;
        case 2326:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_ENTER_PASS_REQ();
          goto case 2392;
        case 2328:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ();
          goto case 2392;
        case 2330:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_COMMUNITY_USER_REPORT_REQ();
          goto case 2392;
        case 2332:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ();
          goto case 2392;
        case 2334:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_TICKET_UPDATE_REQ();
          goto case 2392;
        case 2336:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_CHATTING_REQ();
          goto case 2392;
        case 2338:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
          goto case 2392;
        case 2350:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ();
          goto case 2392;
        case 2360:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ();
          goto case 2392;
        case 2364:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_USER_SUBTASK_REQ();
          goto case 2392;
        case 2366:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_RANDOMBOX_LIST_REQ();
          goto case 2392;
        case 2376:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_USER_TITLE_RELEASE_REQ();
          goto case 2392;
        case 2378:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLEBOX_AUTH_REQ();
          goto case 2392;
        case 2380:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLEBOX_AUTH_REQ();
          goto case 2392;
        case 2384:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_DAILY_RECORD_REQ();
          goto case 2392;
        case 2392:
        case 2518:
        case 2519:
        case 5145:
          // ISSUE: reference to a compiler-generated field
          if (class1.gameClientPacket_0 == null)
            break;
          // ISSUE: reference to a compiler-generated field
          using (class1.gameClientPacket_0)
          {
            if (ConfigLoader.DebugMode)
            {
              // ISSUE: reference to a compiler-generated field
              CLogger.Print($"{class1.gameClientPacket_0.GetType().Name}; Address: {this.Client.RemoteEndPoint}; Opcode: [{obj0}]", LoggerType.Debug, (Exception) null);
            }
            // ISSUE: reference to a compiler-generated field
            ((GameServerPacket) class1.gameClientPacket_0).Makeme(this, DestroyConnection);
            ThreadPool.QueueUserWorkItem(new WaitCallback(((GameManager) class1).method_0));
            // ISSUE: reference to a compiler-generated field
            ((GameServerPacket) class1.gameClientPacket_0).Dispose();
            break;
          }
        case 2399:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ();
          goto case 2392;
        case 2401:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GAMEGUARD_REQ();
          goto case 2392;
        case 2414:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_EVENT_PORTAL_REQ();
          goto case 2392;
        case 2422:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_MATCH_SERVER_IDX_REQ();
          goto case 2392;
        case 2424:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ();
          goto case 2392;
        case 2425:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_UNKNOWN_3635_REQ();
          goto case 2392;
        case 2426:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_AUTH_FRIEND_DELETE_REQ();
          goto case 2392;
        case 2447:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_UNKNOWN_PACKET_REQ();
          goto case 2392;
        case 2465:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_USER_ENTER_REQ();
          goto case 2392;
        case 2489:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_CREATE_NICK_REQ();
          goto case 2392;
        case 2491:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ();
          goto case 2392;
        case 2498:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_SELECT_CHANNEL_REQ();
          goto case 2392;
        case 2508:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_USER_LEAVE_REQ();
          goto case 2392;
        case 2510:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
          goto case 2392;
        case 2561:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ();
          goto case 2392;
        case 2567:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
          goto case 2392;
        case 2583:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
          goto case 2392;
        case 2587:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_NEW_MYINFO_REQ();
          goto case 2392;
        case 3083:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
          goto case 2392;
        case 3329:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_INVENTORY_USE_ITEM_REQ();
          goto case 2392;
        case 3331:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_LOBBY_ENTER_REQ();
          goto case 2392;
        case 3585:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ();
          goto case 2392;
        case 3592:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_GET_PLAYERINFO_REQ();
          goto case 2392;
        case 3596:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_INFO_ENTER_REQ();
          goto case 2392;
        case 3602:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CHANGE_SLOT_REQ();
          goto case 2392;
        case 3604:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CREATE_REQ();
          goto case 2392;
        case 3609:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ();
          goto case 2392;
        case 3611:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ();
          goto case 2392;
        case 3613:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_REQUEST_MAIN_REQ();
          goto case 2392;
        case 3615:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_REQUEST_MAIN_REQ();
          goto case 2392;
        case 3617:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ();
          goto case 2392;
        case 3619:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SHOP_ENTER_REQ();
          goto case 2392;
        case 3631:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_LOADING_START_REQ();
          goto case 2392;
        case 3635:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GMCHAT_SEND_CHAT_REQ();
          goto case 2392;
        case 3639:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CHANGE_SLOT_REQ();
          goto case 2392;
        case 3643:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SEASON_CHALLENGE_INFO_REQ();
          goto case 2392;
        case 3647:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ();
          goto case 2392;
        case 3657:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new Class3();
          goto case 2392;
        case 3665:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_INFO_LEAVE_REQ();
          goto case 2392;
        case 3671:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ();
          goto case 2392;
        case 3673:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_JOIN_REQ();
          goto case 2392;
        case 3675:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ();
          goto case 2392;
        case 3677:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ();
          goto case 2392;
        case 3679:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ();
          goto case 2392;
        case 3680:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_LOGOUT_REQ();
          goto case 2392;
        case 3682:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ();
          goto case 2392;
        case 3686:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_KEEP_ALIVE_REQ();
          goto case 2392;
        case 3850:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_USER_TITLE_CHANGE_REQ();
          goto case 2392;
        case 3852:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_USER_TITLE_EQUIP_REQ();
          goto case 2392;
        case 5123:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_RESPAWN_REQ();
          goto case 2392;
        case 5129:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ();
          goto case 2392;
        case 5131:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ();
          goto case 2392;
        case 5133:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ();
          goto case 2392;
        case 5135:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ();
          goto case 2392;
        case 5137:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_STARTBATTLE_REQ();
          goto case 2392;
        case 5146:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_START_KICKVOTE_REQ();
          goto case 2392;
        case 5156:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ();
          goto case 2392;
        case 5158:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ();
          goto case 2392;
        case 5166:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ();
          goto case 2392;
        case 5168:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_USER_SOPETYPE_REQ();
          goto case 2392;
        case 5172:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_GIVEUPBATTLE_REQ();
          goto case 2392;
        case 5174:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_SENDPING_REQ();
          goto case 2392;
        case 5180:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ();
          goto case 2392;
        case 5182:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ();
          goto case 2392;
        case 5188:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_REQ();
          goto case 2392;
        case 5262:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ();
          goto case 2392;
        case 5276:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CHAR_CREATE_CHARA_REQ();
          goto case 2392;
        case 5292:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BASE_URL_LIST_REQ();
          goto case 2392;
        case 5377:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_MATCH_CLAN_SEASON_REQ();
          goto case 2392;
        case 6145:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ();
          goto case 2392;
        case 6149:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CHAR_DELETE_CHARA_REQ();
          goto case 2392;
        case 6151:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ();
          goto case 2392;
        case 6657:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GM_LOG_LOBBY_REQ();
          goto case 2392;
        case 6659:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GM_KICK_COMMAND_REQ();
          goto case 2392;
        case 6661:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GMCHAT_APPLY_PENALTY_REQ();
          goto case 2392;
        case 6663:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GM_LOG_ROOM_REQ();
          goto case 2392;
        case 6665:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_GM_EXIT_COMMAND_REQ();
          goto case 2392;
        case 6965:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_CS_ACCEPT_REQUEST_REQ();
          goto case 2392;
        case 7429:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_BATTLE_DEATH_REQ();
          goto case 2392;
        case 7681:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_MESSENGER_NOTE_DELETE_REQ();
          goto case 2392;
        case 7699:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ();
          goto case 2392;
        case 8449:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ();
          goto case 2392;
        case 8453:
          // ISSUE: reference to a compiler-generated field
          class1.gameClientPacket_0 = (GameClientPacket) new PROTOCOL_SHOP_GET_SAILLIST_REQ();
          goto case 2392;
        default:
          CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{obj0}] | {Kicked}", DestroyConnection), LoggerType.Opcode, (Exception) null);
          goto case 2392;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
