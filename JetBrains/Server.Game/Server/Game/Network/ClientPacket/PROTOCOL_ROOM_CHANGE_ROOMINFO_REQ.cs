// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ
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

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ : GameClientPacket
{
  private string string_0;
  private string string_1;
  private MapIdEnum mapIdEnum_0;
  private MapRules mapRules_0;
  private StageOptions stageOptions_0;
  private TeamBalance teamBalance_0;
  private byte[] byte_0;
  private byte[] byte_1;
  private int int_0;
  private int int_1;
  private int int_2;
  private int int_3;
  private int int_4;
  private byte byte_2;
  private byte byte_3;
  private byte byte_4;
  private byte byte_5;
  private byte byte_6;
  private byte byte_7;
  private RoomCondition roomCondition_0;
  private RoomState roomState_0;
  private RoomWeaponsFlag roomWeaponsFlag_0;
  private RoomStageFlag roomStageFlag_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      RoomModel room = player.Room;
      if (room == null || ((PROTOCOL_ROOM_CHANGE_COSTUME_REQ) this).teamEnum_0 != TeamEnum.FR_TEAM && ((PROTOCOL_ROOM_CHANGE_COSTUME_REQ) this).teamEnum_0 != TeamEnum.CT_TEAM)
        return;
      SlotModel slot = room.GetSlot(player.SlotId);
      if (slot != null && slot.State == SlotState.NORMAL && AllUtils.ChangeCostume(slot, ((PROTOCOL_ROOM_CHANGE_COSTUME_REQ) this).teamEnum_0))
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(slot));
      room.UpdateSlotsInfo();
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_ROOM_CHANGE_COSTUME_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_ROOM_CHANGE_PASSWD_REQ) this).string_0 = this.ReadS(4);
}
