// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
        return;
      SlotModel slot1 = room.GetSlot(player.SlotId);
      if (slot1 == null || slot1.State != SlotState.BATTLE)
        return;
      room.Bar1 = (int) ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).ushort_0;
      room.Bar2 = (int) ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).ushort_1;
      for (int index = 0; index < 18; ++index)
      {
        SlotModel slot2 = room.Slots[index];
        if (slot2.PlayerId > 0L && slot2.State == SlotState.BATTLE)
        {
          slot2.DamageBar1 = ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).list_0[index];
          slot2.DamageBar2 = ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).list_1[index];
        }
      }
      using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK missionDefenceInfoAck = (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(room))
        room.SendPacketToPlayers((GameServerPacket) missionDefenceInfoAck, SlotState.BATTLE, 0);
      if (((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).ushort_0 != (ushort) 0 || ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).ushort_1 != (ushort) 0)
        return;
      ServerWarning.EndRound(room, !room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ()
  {
    ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).list_0 = new List<ushort>();
    ((PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ) this).list_1 = new List<ushort>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ) this).ushort_0 = this.ReadUH();
    ((PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ) this).ushort_1 = this.ReadUH();
    for (int index = 0; index < 18; ++index)
      ((PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ) this).list_0.Add(this.ReadUH());
  }
}
