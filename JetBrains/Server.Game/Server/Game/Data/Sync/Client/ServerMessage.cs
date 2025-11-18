// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.ServerMessage
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public static class ServerMessage
{
  public static void Load(SyncClientPacket C)
  {
    int match = (int) C.ReadH();
    int num1 = (int) C.ReadH();
    int roomModel_0 = (int) C.ReadH();
    byte account_0 = C.ReadC();
    ushort num2 = C.ReadUH();
    ushort num3 = C.ReadUH();
    int num4 = (int) C.ReadC();
    ushort num5 = C.ReadUH();
    if (C.ToArray().Length > 16 /*0x10*/)
      CLogger.Print($"Invalid Sabotage (Length > 16): {C.ToArray().Length}", LoggerType.Warning, (Exception) null);
    int list_0 = num1;
    ChannelModel channel = AllUtils.GetChannel(roomModel_0, list_0);
    if (channel == null)
      return;
    RoomModel room = ((MatchModel) channel).GetRoom(match);
    SlotModel slotModel;
    if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || !room.GetSlot((int) account_0, ref slotModel))
      return;
    room.Bar1 = (int) num2;
    room.Bar2 = (int) num3;
    RoomCondition roomType = room.RoomType;
    int num6 = 0;
    switch (num4)
    {
      case 1:
        slotModel.DamageBar1 += num5;
        num6 += (int) slotModel.DamageBar1 / 600;
        break;
      case 2:
        slotModel.DamageBar2 += num5;
        num6 += (int) slotModel.DamageBar2 / 600;
        break;
    }
    slotModel.EarnedEXP = num6;
    switch (roomType)
    {
      case RoomCondition.Destroy:
        using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK generatorInfoAck = (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(room))
          room.SendPacketToPlayers((GameServerPacket) generatorInfoAck, SlotState.BATTLE, 0);
        if (room.Bar1 == 0)
        {
          ServerWarning.EndRound(room, !room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
          break;
        }
        if (room.Bar2 != 0)
          break;
        ServerWarning.EndRound(room, !room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
        break;
      case RoomCondition.Defense:
        using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK missionDefenceInfoAck = (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK) new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(room))
          room.SendPacketToPlayers((GameServerPacket) missionDefenceInfoAck, SlotState.BATTLE, 0);
        if (room.Bar1 != 0 || room.Bar2 != 0)
          break;
        ServerWarning.EndRound(room, !room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
        break;
    }
  }
}
