// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_GIVEUPBATTLE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_GIVEUPBATTLE_REQ : GameClientPacket
{
  private bool bool_0;
  private long long_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.State != RoomState.BATTLE || room.IngameAiLevel >= (byte) 10)
        return;
      SlotModel slot = room.GetSlot(player.SlotId);
      if (slot == null || slot.State != SlotState.BATTLE)
        return;
      if (room.IngameAiLevel <= (byte) 9)
        ++room.IngameAiLevel;
      using (PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK difficultyLevelAck = (PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK) new PROTOCOL_BATTLE_DEATH_ACK(room))
        room.SendPacketToPlayers((GameServerPacket) difficultyLevelAck, SlotState.READY, 1);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0 = new FragInfos()
    {
      KillingType = (CharaKillType) this.ReadC(),
      KillsCount = this.ReadC(),
      KillerSlot = this.ReadC(),
      WeaponId = this.ReadD(),
      X = this.ReadT(),
      Y = this.ReadT(),
      Z = this.ReadT(),
      Flag = this.ReadC(),
      Unk = this.ReadC()
    };
    for (int index = 0; index < (int) ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0.KillsCount; ++index)
    {
      FragModel fragModel = new FragModel()
      {
        VictimSlot = this.ReadC(),
        WeaponClass = this.ReadC(),
        HitspotInfo = this.ReadC(),
        KillFlag = (KillingMessage) this.ReadH(),
        Unk = this.ReadC(),
        X = this.ReadT(),
        Y = this.ReadT(),
        Z = this.ReadT(),
        AssistSlot = this.ReadC(),
        Unks = this.ReadB(8)
      };
      ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0.Frags.Add(fragModel);
      if ((int) fragModel.VictimSlot == (int) ((PROTOCOL_BATTLE_DEATH_REQ) this).fragInfos_0.KillerSlot)
        ((PROTOCOL_BATTLE_DEATH_REQ) this).bool_0 = true;
    }
  }
}
