// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_CHANGE_SLOT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_SLOT_REQ : GameClientPacket
{
  private int int_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.LeaderSlot != player.SlotId || !(room.Password != ((PROTOCOL_ROOM_CHANGE_PASSWD_REQ) this).string_0))
        return;
      room.Password = ((PROTOCOL_ROOM_CHANGE_PASSWD_REQ) this).string_0;
      using (PROTOCOL_ROOM_CHANGE_PASSWD_ACK Player = (PROTOCOL_ROOM_CHANGE_PASSWD_ACK) new PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(((PROTOCOL_ROOM_CHANGE_PASSWD_REQ) this).string_0))
        room.SendPacketToPlayers((GameServerPacket) Player);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_ROOM_CHANGE_SLOT_REQ()
  {
  }

  public virtual void Read()
  {
    this.ReadD();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_0 = this.ReadU(46);
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).mapIdEnum_0 = (MapIdEnum) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).mapRules_0 = (MapRules) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).stageOptions_0 = (StageOptions) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomCondition_0 = (RoomCondition) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomState_0 = (RoomState) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_4 = (int) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_2 = (int) this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomWeaponsFlag_0 = (RoomWeaponsFlag) this.ReadH();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomStageFlag_0 = (RoomStageFlag) this.ReadD();
    int num1 = (int) this.ReadH();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_0 = this.ReadD();
    int num2 = (int) this.ReadH();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_1 = this.ReadU(66);
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_3 = this.ReadD();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_2 = this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_3 = this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).teamBalance_0 = (TeamBalance) this.ReadH();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_0 = this.ReadB(24);
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_6 = this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_1 = this.ReadB(4);
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_7 = this.ReadC();
    int num3 = (int) this.ReadH();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_4 = this.ReadC();
    ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_5 = this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || room.LeaderSlot != player.SlotId)
        return;
      bool flag1 = !room.Name.Equals(((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_0);
      bool flag2 = room.Rule != ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).mapRules_0 || room.Stage != ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).stageOptions_0 || room.RoomType != ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomCondition_0;
      room.Name = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_0;
      room.MapId = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).mapIdEnum_0;
      room.Rule = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).mapRules_0;
      room.Stage = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).stageOptions_0;
      room.RoomType = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomCondition_0;
      room.Ping = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_2;
      room.Flag = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomStageFlag_0;
      room.NewInt = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_0;
      room.KillTime = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_3;
      room.Limit = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_2;
      room.WatchRuleFlag = room.RoomType == RoomCondition.Ace ? (byte) 142 : ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_3;
      room.BalanceType = room.RoomType == RoomCondition.Ace ? TeamBalance.None : ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).teamBalance_0;
      room.BalanceType = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).teamBalance_0;
      room.RandomMaps = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_0;
      room.CountdownIG = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_6;
      room.LeaderAddr = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_1;
      room.KillCam = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_7;
      room.AiCount = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_4;
      room.AiLevel = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).byte_5;
      room.SetSlotCount(((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_1, false, true);
      room.CountPlayers = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_4;
      if (((((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomState_0 < RoomState.READY ? 1 : (((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_1.Equals("") ? 1 : (!((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_1.Equals(player.Nickname) ? 1 : 0))) | (flag1 ? 1 : 0) | (flag2 ? 1 : 0)) != 0 || ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomWeaponsFlag_0 != room.WeaponsFlag || ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_1 != room.CountMaxSlots)
      {
        room.State = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomState_0 < RoomState.READY ? RoomState.READY : ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomState_0;
        room.LeaderName = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_1.Equals("") || !((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_1.Equals(player.Nickname) ? player.Nickname : ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).string_1;
        room.WeaponsFlag = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).roomWeaponsFlag_0;
        room.CountMaxSlots = ((PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ) this).int_1;
        room.CountdownIG = (byte) 0;
        if (room.ResetReadyPlayers() > 0)
          room.UpdateSlotsInfo();
      }
      room.UpdateRoomInfo();
      using (PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK Player = (PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) new PROTOCOL_ROOM_CHATTING_ACK(room))
        room.SendPacketToPlayers((GameServerPacket) Player);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BATTLE_CHANGE_ROOMINFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_ROOM_CHANGE_SLOT_REQ()
  {
  }
}
