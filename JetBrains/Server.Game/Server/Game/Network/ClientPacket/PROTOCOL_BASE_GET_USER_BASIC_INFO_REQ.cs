// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ
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

public class PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ : GameClientPacket
{
  private uint uint_0;
  private long long_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      SlotModel slotModel;
      if (room == null || !room.GetSlot(((PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ) this).int_0, ref slotModel))
        return;
      Account playerBySlot = room.GetPlayerBySlot(slotModel);
      if (playerBySlot == null)
        return;
      if (player.Nickname != playerBySlot.Nickname)
        player.FindPlayer = playerBySlot.Nickname;
      int num = int.MaxValue;
      switch (room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam))
      {
        case TeamEnum.FR_TEAM:
          num = playerBySlot.Equipment.CharaRedId;
          break;
        case TeamEnum.CT_TEAM:
          num = playerBySlot.Equipment.CharaBlueId;
          break;
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_MATCH_CLAN_SEASON_ACK(0U, playerBySlot, num));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ) this).long_0 = this.ReadQ();
}
