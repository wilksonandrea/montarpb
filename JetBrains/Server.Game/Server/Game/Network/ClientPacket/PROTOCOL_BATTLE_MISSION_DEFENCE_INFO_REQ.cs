// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ : GameClientPacket
{
  private ushort ushort_0;
  private ushort ushort_1;
  private List<ushort> list_0;
  private List<ushort> list_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || room.ActiveC4)
        return;
      SlotModel slot = room.GetSlot(((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).int_0);
      if (slot == null || slot.State != SlotState.BATTLE)
        return;
      RoomDeath.InstallBomb(room, slot, ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).byte_0, ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).int_1 == 0 ? (ushort) 42 : (ushort) 0, ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).float_0, ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).float_1, ((PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ) this).float_2);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ) this).int_0 = this.ReadD();
  }
}
