// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.GameSync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Sync.Update;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Game.Data.Sync;

public class GameSync
{
  protected Socket ClientSocket;
  private bool bool_0;

  internal bool method_0(SlotModel slotModel_0)
  {
    // ISSUE: reference to a compiler-generated field
    return slotModel_0.Team == ((AllUtils.Class7) this).slotModel_0.Team && slotModel_0.State != SlotState.CLOSE;
  }

  public GameSync()
  {
  }

  internal bool method_0(SlotModel slotModel_0)
  {
    // ISSUE: reference to a compiler-generated field
    return slotModel_0.Team == ((AllUtils.Class8) this).slotModel_0.Team && slotModel_0.State != SlotState.CLOSE;
  }

  public GameSync()
  {
  }

  internal void method_0(object slotModel_0)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    AllUtils.smethod_2(((AllUtils.Class9) this).roomModel_0, ((AllUtils.Class9) this).teamEnum_0, ((AllUtils.Class9) this).bool_0, ((AllUtils.Class9) this).fragInfos_0, ((AllUtils.Class9) this).slotModel_0);
    lock (slotModel_0)
    {
      // ISSUE: reference to a compiler-generated field
      ((AllUtils.Class9) this).roomModel_0.MatchEndTime.StopJob();
    }
  }

  public GameSync(IPEndPoint slotModel_0)
  {
    this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    this.ClientSocket.Bind((EndPoint) slotModel_0);
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
      CLogger.Print($"Game Sync Address {localEndPoint.Address}:{localEndPoint.Port}", LoggerType.Info, (Exception) null);
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((BattleLeaveSync) this).method_4));
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
      CLogger.Print("GameSync socket disposed during StartReceive.", LoggerType.Warning, (Exception) null);
      ((KillFragInfo) this).Close();
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in StartReceive: " + ex.Message, LoggerType.Error, ex);
      ((KillFragInfo) this).Close();
    }
  }

  private void method_1(IAsyncResult competitiveRank_0)
  {
    // ISSUE: variable of a compiler-generated type
    GameSync.Class10 class10 = (GameSync.Class10) new BattleLeaveSync();
    // ISSUE: reference to a compiler-generated field
    class10.gameSync_0 = this;
    if (this.bool_0 || GameXender.Client == null || GameXender.Client.ServerIsClosed)
      return;
    StateObject asyncState = competitiveRank_0.AsyncState as StateObject;
    try
    {
      int from = this.ClientSocket.EndReceiveFrom(competitiveRank_0, ref asyncState.RemoteEP);
      if (from <= 0)
        return;
      // ISSUE: reference to a compiler-generated field
      class10.byte_0 = new byte[from];
      // ISSUE: reference to a compiler-generated field
      Array.Copy((Array) asyncState.UdpBuffer, 0, (Array) class10.byte_0, 0, from);
      ThreadPool.QueueUserWorkItem(new WaitCallback(((EquipmentSync) class10).method_0));
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("GameSync socket disposed during ReceiveCallback.", LoggerType.Warning, (Exception) null);
      ((KillFragInfo) this).Close();
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

  private void method_2(byte[] slotModel_1)
  {
    try
    {
      SyncClientPacket syncClientPacket = new SyncClientPacket(slotModel_1);
      short num = syncClientPacket.ReadH();
      switch (num)
      {
        case 1:
          RoomSadeSync.Load(syncClientPacket);
          break;
        case 2:
          RoomDeath.Load(syncClientPacket);
          break;
        case 3:
          RoomDeath.Load(syncClientPacket);
          break;
        case 4:
          RoomSadeSync.Load(syncClientPacket);
          break;
        case 5:
          ServerMessage.Load(syncClientPacket);
          break;
        case 6:
          ServerCache.Load(syncClientPacket);
          break;
        case 10:
          EventInfo.Load(syncClientPacket);
          break;
        case 11:
          InventorySync.Load(syncClientPacket);
          break;
        case 13:
          ClanServersSync.Load(syncClientPacket);
          break;
        case 15:
          ServerWarning.Load(syncClientPacket);
          break;
        case 16 /*0x10*/:
          FriendInfo.Load(syncClientPacket);
          break;
        case 17:
          ReloadConfig.Load(syncClientPacket);
          break;
        case 18:
          RoomBombC4.Load(syncClientPacket);
          break;
        case 19:
          RoomBombC4.Load(syncClientPacket);
          break;
        case 20:
          Account.LoadGMWarning(syncClientPacket);
          break;
        case 21:
          EventInfo.Load(syncClientPacket);
          break;
        case 22:
          Account.LoadShopRestart(syncClientPacket);
          break;
        case 23:
          Account.LoadServerUpdate(syncClientPacket);
          break;
        case 24:
          Account.LoadShutdown(syncClientPacket);
          break;
        case 31 /*0x1F*/:
          FriendSync.LoadEventInfo(syncClientPacket);
          break;
        case 32 /*0x20*/:
          RoomDeath.Load(syncClientPacket);
          break;
        case 7171:
          ServerWarning.Load(syncClientPacket);
          break;
        default:
          CLogger.Print(Bitwise.ToHexData($"Game - Opcode Not Found: [{num}]", syncClientPacket.ToArray()), LoggerType.Opcode, (Exception) null);
          break;
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public SChannelModel GetServer(AccountStatus slotModel_1)
  {
    return this.GetServer((int) slotModel_1.ServerId);
  }

  public SChannelModel GetServer(int object_0)
  {
    return object_0 != (int) byte.MaxValue && object_0 != GameXender.Client.ServerId ? SChannelXML.GetServer(object_0) : (SChannelModel) null;
  }

  public void SendBytes(long ipendPoint_0, [In] GameServerPacket obj1, [In] int obj2)
  {
    try
    {
      if (obj1 == null)
        return;
      SChannelModel server = this.GetServer(obj2);
      if (server == null)
        return;
      string name = obj1.GetType().Name;
      byte[] bytes = ((PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK) obj1).GetBytes("GameSync.SendBytes");
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 13);
        syncServerPacket.WriteQ(ipendPoint_0);
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteC((byte) (name.Length + 1));
        syncServerPacket.WriteS(name, name.Length + 1);
        syncServerPacket.WriteH((ushort) bytes.Length);
        syncServerPacket.WriteB(bytes);
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) this).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public void SendBytes(long status, string Packet, [In] byte[] obj2, [In] int obj3)
  {
    try
    {
      if (obj2.Length == 0)
        return;
      SChannelModel server = this.GetServer(obj3);
      if (server == null)
        return;
      IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 13);
        syncServerPacket.WriteQ(status);
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteC((byte) (Packet.Length + 1));
        syncServerPacket.WriteS(Packet, Packet.Length + 1);
        syncServerPacket.WriteH((ushort) obj2.Length);
        syncServerPacket.WriteB(obj2);
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) this).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
