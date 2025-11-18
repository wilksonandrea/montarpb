// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Sync.Client.RemovePlayerSync
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
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Sync.Client;

public class RemovePlayerSync
{
  public static void SendSabotageSync(
    RoomModel Room,
    PlayerModel Player,
    int DeathType,
    int HitEnum)
  {
    try
    {
      IPEndPoint connection = SynchronizeXML.GetServer((int) Room.Server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 5);
        syncServerPacket.WriteH((short) Room.RoomId);
        syncServerPacket.WriteH((short) Room.ChannelId);
        syncServerPacket.WriteH((short) Room.ServerId);
        syncServerPacket.WriteC((byte) Player.Slot);
        syncServerPacket.WriteH((ushort) Room.Bar1);
        syncServerPacket.WriteH((ushort) Room.Bar2);
        syncServerPacket.WriteC((byte) HitEnum);
        syncServerPacket.WriteH((ushort) DeathType);
        // ISSUE: reference to a compiler-generated method
        ((MatchSync.Class0) MatchXender.Sync).SendPacket(syncServerPacket.ToArray(), connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static void SendPingSync([In] RoomModel obj0, [In] PlayerModel obj1)
  {
    try
    {
      IPEndPoint connection = SynchronizeXML.GetServer((int) obj0.Server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 6);
        syncServerPacket.WriteH((short) obj0.RoomId);
        syncServerPacket.WriteH((short) obj0.ChannelId);
        syncServerPacket.WriteH((short) obj0.ServerId);
        syncServerPacket.WriteC((byte) obj1.Slot);
        syncServerPacket.WriteC((byte) obj1.Ping);
        syncServerPacket.WriteH((ushort) obj1.Latency);
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
