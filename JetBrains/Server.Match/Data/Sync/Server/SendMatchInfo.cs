// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Sync.Server.SendMatchInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Match.Data.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Sync.Server;

public class SendMatchInfo
{
  private void method_3(IAsyncResult ipendPoint_0)
  {
    try
    {
      if (!(ipendPoint_0.AsyncState is Socket asyncState) || !asyncState.Connected)
        return;
      asyncState.EndSend(ipendPoint_0);
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("MatchSync socket disposed during SendCallback.", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in SendCallback: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public void Close()
  {
    if (((MatchSync) this).bool_0)
      return;
    ((MatchSync) this).bool_0 = true;
    try
    {
      if (((MatchSync) this).ClientSocket == null)
        return;
      ((MatchSync) this).ClientSocket.Close();
      ((MatchSync) this).ClientSocket.Dispose();
      ((MatchSync) this).ClientSocket = (Socket) null;
    }
    catch (Exception ex)
    {
      CLogger.Print("Error closing MatchSync: " + ex.Message, LoggerType.Error, ex);
    }
  }

  internal void method_0(object byte_0)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      ((MatchSync.Class0) ((MatchSync.Class0) this).matchSync_0).method_2(((MatchSync.Class0) this).byte_0);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static void SendPortalPass(RoomModel Data, PlayerModel Address, [In] int obj2)
  {
    try
    {
      if (Data.RoomType != RoomCondition.Boss)
        return;
      Address.Life = Address.MaxLife;
      IPEndPoint connection = SynchronizeXML.GetServer((int) Data.Server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 1);
        syncServerPacket.WriteH((short) Data.RoomId);
        syncServerPacket.WriteH((short) Data.ChannelId);
        syncServerPacket.WriteH((short) Data.ServerId);
        syncServerPacket.WriteC((byte) Address.Slot);
        syncServerPacket.WriteC((byte) obj2);
        // ISSUE: reference to a compiler-generated method
        ((MatchSync.Class0) MatchXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static void SendBombSync(RoomModel object_0, PlayerModel Player, [In] int obj2, [In] int obj3)
  {
    try
    {
      IPEndPoint connection = SynchronizeXML.GetServer((int) object_0.Server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 2);
        syncServerPacket.WriteH((short) object_0.RoomId);
        syncServerPacket.WriteH((short) object_0.ChannelId);
        syncServerPacket.WriteH((short) object_0.ServerId);
        syncServerPacket.WriteC((byte) obj2);
        syncServerPacket.WriteC((byte) Player.Slot);
        if (obj2 == 0)
        {
          syncServerPacket.WriteC((byte) obj3);
          syncServerPacket.WriteTV(Player.Position);
          syncServerPacket.WriteH((short) 42);
          object_0.BombPosition = Player.Position;
        }
        // ISSUE: reference to a compiler-generated method
        ((MatchSync.Class0) MatchXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
