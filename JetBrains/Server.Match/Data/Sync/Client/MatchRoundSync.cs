// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Sync.Client.MatchRoundSync
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Sync.Client;

public class MatchRoundSync
{
  public static void SendDeathSync(
    [In] RoomModel obj0,
    [In] PlayerModel obj1,
    int Portal,
    int BombArea,
    [In] List<DeathServerData> obj4)
  {
    try
    {
      IPEndPoint connection = SynchronizeXML.GetServer((int) obj0.Server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 3);
        syncServerPacket.WriteH((short) obj0.RoomId);
        syncServerPacket.WriteH((short) obj0.ChannelId);
        syncServerPacket.WriteH((short) obj0.ServerId);
        syncServerPacket.WriteC((byte) obj4.Count);
        syncServerPacket.WriteC((byte) obj1.Slot);
        syncServerPacket.WriteD(BombArea);
        syncServerPacket.WriteTV(obj1.Position);
        syncServerPacket.WriteC((byte) Portal);
        syncServerPacket.WriteC((byte) 0);
        foreach (DeathServerData deathServerData in obj4)
        {
          syncServerPacket.WriteC((byte) ((DeffectModel) deathServerData).get_Player().Slot);
          syncServerPacket.WriteC((byte) ComDiv.GetIdStatics(BombArea, 2));
          syncServerPacket.WriteC((byte) ((int) deathServerData.DeathType * 16 /*0x10*/ + ((DeffectModel) deathServerData).get_Player().Slot));
          syncServerPacket.WriteTV(((DeffectModel) deathServerData).get_Player().Position);
          syncServerPacket.WriteC((byte) ((DeffectModel) deathServerData).get_AssistSlot());
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteB(new byte[8]);
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

  public static void SendHitMarkerSync(
    RoomModel Room,
    PlayerModel Killer,
    CharaDeath ObjectId,
    HitType WeaponId,
    int Deaths)
  {
    try
    {
      IPEndPoint connection = SynchronizeXML.GetServer((int) Room.Server.Port).Connection;
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 4);
        syncServerPacket.WriteH((short) Room.RoomId);
        syncServerPacket.WriteH((short) Room.ChannelId);
        syncServerPacket.WriteH((short) Room.ServerId);
        syncServerPacket.WriteC((byte) Killer.Slot);
        syncServerPacket.WriteC((byte) ObjectId);
        syncServerPacket.WriteC((byte) WeaponId);
        syncServerPacket.WriteD(Deaths);
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
