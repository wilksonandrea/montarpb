// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_SEASON_CHALLENGE_INFO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_SEASON_CHALLENGE_INFO_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null)
        return;
      Account playerBySlot = room.GetPlayerBySlot((int) ((PROTOCOL_GM_KICK_COMMAND_REQ) this).byte_0);
      if (playerBySlot == null || playerBySlot.IsGM())
        return;
      room.RemovePlayer(playerBySlot, true, 0);
    }
    catch (Exception ex)
    {
      CLogger.Print($"{this.GetType().Name}: {ex.Message}", LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ) this).viewerType_0 = (ViewerType) this.ReadC();
  }
}
