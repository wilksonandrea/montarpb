// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ : GameClientPacket
{
  private ushort ushort_0;
  private ushort ushort_1;
  private List<ushort> list_0;

  public virtual void Run()
  {
    Account player = this.Client.Player;
    if (player == null)
      return;
    RoomModel room = player.Room;
    if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || !room.ActiveC4)
      return;
    SlotModel slot = room.GetSlot(((PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ) this).int_0);
    if (slot == null || slot.State != SlotState.BATTLE)
      return;
    RoomDeath.UninstallBomb(room, slot);
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).ushort_0 = this.ReadUH();
    ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).ushort_1 = this.ReadUH();
    for (int index = 0; index < 18; ++index)
      ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).list_0.Add(this.ReadUH());
    for (int index = 0; index < 18; ++index)
      ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).list_1.Add(this.ReadUH());
  }
}
