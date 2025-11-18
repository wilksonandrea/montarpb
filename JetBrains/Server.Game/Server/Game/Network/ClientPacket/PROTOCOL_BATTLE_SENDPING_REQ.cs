// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BATTLE_SENDPING_REQ
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

public class PROTOCOL_BATTLE_SENDPING_REQ : GameClientPacket
{
  private byte[] byte_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.State != RoomState.BATTLE || player.SlotId != room.LeaderSlot)
        return;
      SlotModel slot = room.GetSlot(((PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ) this).int_0);
      if (slot != null)
      {
        slot.AiLevel = (int) room.IngameAiLevel;
        ++room.SpawnsCount;
      }
      using (PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK battleRespawnForAiAck = (PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK) new PROTOCOL_BATTLE_STARTBATTLE_ACK(((PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ) this).int_0))
        room.SendPacketToPlayers((GameServerPacket) battleRespawnForAiAck, SlotState.BATTLE, 0);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0 = new int[16 /*0x10*/];
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[0] = this.ReadD();
    int num1 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[1] = this.ReadD();
    int num2 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[2] = this.ReadD();
    int num3 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[3] = this.ReadD();
    int num4 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[4] = this.ReadD();
    int num5 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[5] = this.ReadD();
    int num6 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[6] = this.ReadD();
    int num7 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[7] = this.ReadD();
    int num8 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[8] = this.ReadD();
    int num9 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[9] = this.ReadD();
    int num10 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[10] = this.ReadD();
    int num11 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[11] = this.ReadD();
    int num12 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[12] = this.ReadD();
    int num13 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[13] = this.ReadD();
    int num14 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[14] = this.ReadD();
    int num15 = (int) this.ReadUD();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_1 = (int) this.ReadH();
    ((PROTOCOL_BATTLE_RESPAWN_REQ) this).int_0[15] = this.ReadD();
    int num16 = (int) this.ReadUD();
  }
}
