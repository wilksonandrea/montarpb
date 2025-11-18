// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CHAR_CREATE_CHARA_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CHAR_CREATE_CHARA_REQ : GameClientPacket
{
  private string string_0;
  private List<CartGoods> list_0;

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
      player.Sight = ((PROTOCOL_BATTLE_USER_SOPETYPE_REQ) this).int_0;
      using ((PROTOCOL_BATTLE_USER_SOPETYPE_ACK) new PROTOCOL_CHAR_CHANGE_EQUIP_ACK(player))
        room.SendPacketToPlayers((GameServerPacket) new PROTOCOL_CHAR_CHANGE_EQUIP_ACK(player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_USER_SOPETYPE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_0 = this.ReadD();
    int num1 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_0 = this.ReadC() == (byte) 1;
    byte num2 = this.ReadC();
    for (byte key = 0; (int) key < (int) num2; ++key)
    {
      int num3 = this.ReadD();
      ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_0.Add((int) key, num3);
    }
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_1 = this.ReadC() == (byte) 1;
    int num4 = (int) this.ReadC();
    byte num5 = this.ReadC();
    for (byte key = 0; (int) key < (int) num5; ++key)
    {
      int num6 = this.ReadD();
      ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_1.Add((int) key, num6);
    }
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_2 = this.ReadC() == (byte) 1;
    int num7 = (int) this.ReadC();
    int num8 = (int) this.ReadC();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[0] = this.ReadD();
    int num9 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[1] = this.ReadD();
    int num10 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[2] = this.ReadD();
    int num11 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[3] = this.ReadD();
    int num12 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[4] = this.ReadD();
    int num13 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_1 = this.ReadD();
    int num14 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[5] = this.ReadD();
    int num15 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[6] = this.ReadD();
    int num16 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[7] = this.ReadD();
    int num17 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[8] = this.ReadD();
    int num18 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[9] = this.ReadD();
    int num19 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[10] = this.ReadD();
    int num20 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[11] = this.ReadD();
    int num21 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[12] = this.ReadD();
    int num22 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3[13] = this.ReadD();
    int num23 = (int) this.ReadUD();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_3 = this.ReadC() == (byte) 1;
    byte num24 = this.ReadC();
    for (byte key = 0; (int) key < (int) num24; ++key)
    {
      int num25 = this.ReadD();
      int num26 = (int) this.ReadUD();
      ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_2.Add((int) key, num25);
    }
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_4 = this.ReadC() == (byte) 1;
    int num27 = (int) this.ReadC();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_2[0] = (int) this.ReadC();
    ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_2[1] = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Character.Characters.Count > 0)
      {
        if (((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_0)
          AllUtils.ValidateAccesoryEquipment(player, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_0);
        if (((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_1)
          AllUtils.ValidateDisabledCoupon(player, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_0);
        if (((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_2)
          AllUtils.ValidateEnabledCoupon(player, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_1);
        if (((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_3)
          AllUtils.ValidateCharacterEquipment(player, player.Equipment, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_3, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_1, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_2);
        if (((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).bool_4)
          AllUtils.ValidateItemEquipment(player, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).sortedList_2);
        AllUtils.ValidateCharacterSlot(player, player.Equipment, ((PROTOCOL_CHAR_CHANGE_EQUIP_REQ) this).int_2);
      }
      RoomModel room = player.Room;
      if (room != null)
        AllUtils.UpdateSlotEquips(player, room);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CHAR_CREATE_CHARA_ACK(0U));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
