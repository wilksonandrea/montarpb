// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Server.RoundSync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Server;

public class RoundSync
{
  public static void SendUDPPlayerLeave(RoomModel object_0, [In] int obj1)
  {
    try
    {
      if (object_0 == null)
        return;
      int playingPlayers = object_0.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.BATTLE, 0, obj1);
      using (SyncServerPacket syncServerPacket = new SyncServerPacket())
      {
        syncServerPacket.WriteH((short) 2);
        syncServerPacket.WriteD(object_0.UniqueRoomId);
        syncServerPacket.WriteD(object_0.Seed);
        syncServerPacket.WriteC((byte) obj1);
        syncServerPacket.WriteC((byte) playingPlayers);
        // ISSUE: reference to a compiler-generated method
        ((GameSync.Class10) GameXender.Sync).SendPacket(syncServerPacket.ToArray(), object_0.UdpServer.Connection);
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
