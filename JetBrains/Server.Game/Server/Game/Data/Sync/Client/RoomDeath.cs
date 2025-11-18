// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.RoomDeath
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class RoomDeath
{
  public static void Load(SyncClientPacket int_0)
  {
    int num = (int) int_0.ReadC();
    ServerConfig config = ServerConfigJSON.GetConfig(num);
    if (config == null || config.ConfigId <= 0)
      return;
    GameXender.Client.Config = config;
    CLogger.Print($"Configuration (Database) Refills; Config: {num}", LoggerType.Command, (Exception) null);
  }

  public static void Load(SyncClientPacket C)
  {
    int match = (int) C.ReadH();
    int num1 = (int) C.ReadH();
    int roomModel_0 = (int) C.ReadH();
    int num2 = (int) C.ReadC();
    int int_0 = (int) C.ReadC();
    byte num3 = 0;
    ushort num4 = 0;
    float num5 = 0.0f;
    float num6 = 0.0f;
    float num7 = 0.0f;
    switch (num2)
    {
      case 0:
        num3 = C.ReadC();
        num5 = C.ReadT();
        num6 = C.ReadT();
        num7 = C.ReadT();
        num4 = C.ReadUH();
        if (C.ToArray().Length > 25)
        {
          CLogger.Print($"Invalid Bomb (Length > 25): {C.ToArray().Length}", LoggerType.Warning, (Exception) null);
          break;
        }
        break;
      case 1:
        if (C.ToArray().Length > 10)
        {
          CLogger.Print($"Invalid Bomb Type[1] (Length > 10): {C.ToArray().Length}", LoggerType.Warning, (Exception) null);
          break;
        }
        break;
    }
    int list_0 = num1;
    ChannelModel channel = AllUtils.GetChannel(roomModel_0, list_0);
    if (channel == null)
      return;
    RoomModel room = ((MatchModel) channel).GetRoom(match);
    if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
      return;
    SlotModel slot = room.GetSlot(int_0);
    if (slot == null || slot.State != SlotState.BATTLE)
      return;
    if (num2 == 0)
    {
      RoomDeath.InstallBomb(room, slot, num3, num4, num5, num6, num7);
    }
    else
    {
      if (num2 != 1)
        return;
      RoomDeath.UninstallBomb(room, slot);
    }
  }

  public static void InstallBomb(
    RoomModel C,
    SlotModel Slot,
    [In] byte obj2,
    [In] ushort obj3,
    [In] float obj4,
    [In] float obj5,
    [In] float obj6)
  {
    if (C.ActiveC4)
      return;
    using (PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK missionBombInstallAck = (PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK) new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(Slot.Id, obj2, obj3, obj4, obj5, obj6))
      C.SendPacketToPlayers((GameServerPacket) missionBombInstallAck, SlotState.BATTLE, 0);
    if (C.RoomType != RoomCondition.Tutorial)
    {
      C.ActiveC4 = true;
      ++Slot.Objects;
      AllUtils.CompleteMission(C, Slot, MissionType.C4_PLANT, 0);
      C.StartBomb();
    }
    else
      C.ActiveC4 = true;
  }

  public static void UninstallBomb([In] RoomModel obj0, [In] SlotModel obj1)
  {
    if (!obj0.ActiveC4)
      return;
    using (PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK bombUninstallAck = (PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK) new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(obj1.Id))
      obj0.SendPacketToPlayers((GameServerPacket) bombUninstallAck, SlotState.BATTLE, 0);
    if (obj0.RoomType != RoomCondition.Tutorial)
    {
      ++obj1.Objects;
      if (obj0.SwapRound)
        ++obj0.FRRounds;
      else
        ++obj0.CTRounds;
      AllUtils.CompleteMission(obj0, obj1, MissionType.C4_DEFUSE, 0);
      AllUtils.BattleEndRound(obj0, obj0.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM, RoundEndType.Uninstall);
    }
    else
      obj0.ActiveC4 = false;
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    int match = (int) obj0.ReadH();
    int num1 = (int) obj0.ReadH();
    int roomModel_0 = (int) obj0.ReadH();
    byte num2 = obj0.ReadC();
    byte int_0_1 = obj0.ReadC();
    int num3 = obj0.ReadD();
    float num4 = obj0.ReadT();
    float num5 = obj0.ReadT();
    float num6 = obj0.ReadT();
    byte num7 = obj0.ReadC();
    byte num8 = obj0.ReadC();
    int num9 = (int) num2 * 25;
    if (obj0.ToArray().Length > 28 + num9)
      CLogger.Print($"Invalid Death (Length > 53): {obj0.ToArray().Length}", LoggerType.Warning, (Exception) null);
    int list_0 = num1;
    ChannelModel channel = AllUtils.GetChannel(roomModel_0, list_0);
    if (channel == null)
      return;
    RoomModel room = ((MatchModel) channel).GetRoom(match);
    if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
      return;
    SlotModel slot1 = room.GetSlot((int) int_0_1);
    if (slot1 == null || slot1.State != SlotState.BATTLE)
      return;
    FragInfos fragInfos = new FragInfos()
    {
      KillsCount = num2,
      KillerSlot = int_0_1,
      WeaponId = num3,
      X = num4,
      Y = num5,
      Z = num6,
      Flag = num7,
      Unk = num8
    };
    bool flag = false;
    for (int index = 0; index < (int) num2; ++index)
    {
      byte int_0_2 = obj0.ReadC();
      byte num10 = obj0.ReadC();
      byte num11 = obj0.ReadC();
      float num12 = obj0.ReadT();
      float num13 = obj0.ReadT();
      float num14 = obj0.ReadT();
      byte num15 = obj0.ReadC();
      byte num16 = obj0.ReadC();
      byte[] numArray = obj0.ReadB(8);
      SlotModel slot2 = room.GetSlot((int) int_0_2);
      if (slot2 != null && slot2.State == SlotState.BATTLE)
      {
        FragModel fragModel = new FragModel()
        {
          VictimSlot = int_0_2,
          WeaponClass = num10,
          HitspotInfo = num11,
          X = num12,
          Y = num13,
          Z = num14,
          AssistSlot = num15,
          Unk = num16,
          Unks = numArray
        };
        if ((int) fragInfos.KillerSlot == (int) int_0_2)
          flag = true;
        fragInfos.Frags.Add(fragModel);
      }
    }
    fragInfos.KillsCount = (byte) fragInfos.Frags.Count;
    EquipmentSync.GenDeath(room, slot1, fragInfos, flag);
  }
}
